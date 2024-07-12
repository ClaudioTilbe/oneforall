using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System;
using Sitio.Models;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sitio.Hubs
{
    public class MonitoreoRedHub : Hub
    {


        //Variables
        List<Dispositivo> dispositivos = null;
        DatosCompartidos datosCompartidosMonitoreo = DatosCompartidos.GetInstancia();

        //Variables de SignalR
        private readonly object _sondeoStateLock = new object();
        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(2000); //2000

        //Acceso a Session
        private readonly IHttpContextAccessor _httpContextAccessor;



        //Constructores - Propiedades
        public MonitoreoRedHub(IHubContext<MonitoreoRedHub> hub, IHttpContextAccessor httpContextAccessor)
        {
            Hub = hub;
            _httpContextAccessor = httpContextAccessor;
        }

        private IHubContext<MonitoreoRedHub> Hub
        {
            get;
            set;
        }




        //==============================================================================================
        //Metodos MonitoreoIndex =======================================================================
        //==============================================================================================


        private Usuario ObtengoUsuarioSession()
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                return httpContext.Session.GetObjectFromJson<Usuario>("UsuarioLog");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        //Inicia la transferencia de datos en tiempo real
        public void OpenSondeo()
        {
            string connectionId = Context.ConnectionId;

            Usuario usuLog = ObtengoUsuarioSession();

            UsuarioXEstadoSignalR usuarioSignalR = new UsuarioXEstadoSignalR();

            usuarioSignalR.ConnectionID = connectionId;
            usuarioSignalR.SondeoOpen = false;
            usuarioSignalR.token = usuLog.Token;

            datosCompartidosMonitoreo.SetearUsuarioXEstado(connectionId, usuarioSignalR);

            lock (_sondeoStateLock)
            {

                if (usuarioSignalR.SondeoOpen != true)
                {
                    usuarioSignalR.TimerMonitoreo = new Timer(UpdateMonitoreo, connectionId, _updateInterval, _updateInterval);

                    usuarioSignalR.SondeoOpen = true;

                    datosCompartidosMonitoreo.SetearUsuarioXEstado(connectionId, usuarioSignalR);

                    BroadcastMonitoreoRedStateChange(usuarioSignalR.ConnectionID, true);
                }
            }
        }



        public void CloseSondeo()
        {
            UsuarioXEstadoSignalR usuarioSignalR = new UsuarioXEstadoSignalR();

            string connectionId = Context.ConnectionId;

            usuarioSignalR = datosCompartidosMonitoreo.ObtenerUsuarioXEstado(connectionId);

            lock (usuarioSignalR)
            {
                if (usuarioSignalR.SondeoOpen == true)
                {
                    usuarioSignalR.TimerMonitoreo?.Dispose();

                    //Cambiamos estado a cerrado
                    usuarioSignalR.SondeoOpen = false;

                    datosCompartidosMonitoreo.SetearUsuarioXEstado(connectionId, usuarioSignalR);

                    BroadcastMonitoreoRedStateChange(usuarioSignalR.ConnectionID, false);
                }
            }
        }


        //Este metodo trae las actualizaciones
        private void UpdateMonitoreo(object state)
        {
            UsuarioXEstadoSignalR usuarioSignalR = new UsuarioXEstadoSignalR();

            string connectionID = (string)state;

            usuarioSignalR = datosCompartidosMonitoreo.ObtenerUsuarioXEstado(connectionID);

            lock (usuarioSignalR)
            {

                //Llamo metodos del Cliente que actualizan datos
                BroadcastPanelEstado(connectionID, usuarioSignalR.token);
                BroadcastVisorMensajes(connectionID, usuarioSignalR.token);

                dispositivos = new DispositivosAPI().GetDispositivosXUsuario(usuarioSignalR.token);

                //Comenzamos a enviar la informacion de todos los registros hacia el cliente
                foreach (var dispositivo in dispositivos)
                {
                    BroadcastMonitoreoRed(connectionID, dispositivo);
                }
            }
        }



        private void BroadcastMonitoreoRedStateChange(string connectionID, bool isOpen)
        {

            if (isOpen)
            {
                Hub.Clients.Client(connectionID).SendAsync("monitoreoRedOpened");
            }
            else
            {
                Hub.Clients.Client(connectionID).SendAsync("monitoreoRedClosed");
            }
        }



        private void BroadcastMonitoreoRed(string connectionID, Dispositivo dispositivo)
        {
            Hub.Clients.Client(connectionID).SendAsync("updateMonitoreoRed", dispositivo);
        }



        private void BroadcastPanelEstado(string connectionID, string Token)
        {
            List<Tuple<string, int>> listaResultados = new DispositivosAPI().GetDispositivosCategorizados(Token);

            Hub.Clients.Client(connectionID).SendAsync("actualizoPanelEstado", listaResultados);
        }



        private void BroadcastVisorMensajes(string connectionID, string Token)
        {
            List<MensajeVisor> mensajes = new MensajeVisorAPI().GetMensajeVisorXUsuarioUltimaH(Token);

            mensajes = mensajes.OrderByDescending(m => m.Id).ToList();

            Hub.Clients.Client(connectionID).SendAsync("actualizoVisorMensajes", mensajes);
        }






        public List<MensajeVisor> CargoMensajesVisor()
        {
            Usuario usuLog = ObtengoUsuarioSession();

            List<MensajeVisor> listaMensajes = new MensajeVisorAPI().GetMensajeVisorXUsuarioUltimaH(usuLog.Token);

            listaMensajes = listaMensajes.OrderByDescending(m => m.Id).ToList();

            return listaMensajes;
        }



        public List<Tuple<string, int>> CargoPanelEstado()
        {
            Usuario usuLog = ObtengoUsuarioSession();

            List<Tuple<string, int>> listaResultados = new DispositivosAPI().GetDispositivosCategorizados(usuLog.Token);

            return listaResultados;
        }



        public List<Dispositivo> ActualizoDispositivosXGrupo(string cadenaGrupo)
        {
            Usuario usuLog = ObtengoUsuarioSession();

            if (cadenaGrupo == "")
            {
                dispositivos = new DispositivosAPI().GetDispositivosXUsuario(usuLog.Token);
            }
            else
            {
                //Proceso Cadena
                int codigoGrupo = Convert.ToInt32(Regex.Match(cadenaGrupo, @"\d+").Value);

                dispositivos = new DispositivosAPI().GetDispositivosXGrupo(usuLog.Token, codigoGrupo);
            }

            return dispositivos;
        }





        //==============================================================================================
        //Metodos Panel General ========================================================================
        //==============================================================================================


        //Variables
        private readonly object _panelStateLock = new object();
        private volatile bool _updatingPanelEstado;


        public void OpenPanelEstado()
        {
            string connectionIDPanel = Context.ConnectionId;

            Usuario usuLog = ObtengoUsuarioSession();

            UsuarioXEstadoSignalR usuarioSignalRPanel = new UsuarioXEstadoSignalR();

            usuarioSignalRPanel.ConnectionID = connectionIDPanel;
            usuarioSignalRPanel.PanelOpen = false;
            usuarioSignalRPanel.token = usuLog.Token;

            datosCompartidosMonitoreo.SetearUsuarioXEstado(connectionIDPanel, usuarioSignalRPanel);


            lock (_panelStateLock)
            {
                if (usuarioSignalRPanel.PanelOpen != true)
                {
                    usuarioSignalRPanel.TimerPanel = new Timer(UpdatePanelEstado, connectionIDPanel, _updateInterval, _updateInterval);

                    usuarioSignalRPanel.PanelOpen = true;

                    datosCompartidosMonitoreo.SetearUsuarioXEstado(connectionIDPanel, usuarioSignalRPanel);

                }
            }
        }



        public void ClosePanelEstado()
        {
            UsuarioXEstadoSignalR usuarioSignalRPanel = new UsuarioXEstadoSignalR();

            string connectionId = Context.ConnectionId;

            usuarioSignalRPanel = datosCompartidosMonitoreo.ObtenerUsuarioXEstado(connectionId);

            lock (usuarioSignalRPanel)
            {
                if (usuarioSignalRPanel.SondeoOpen == true)
                {
                    usuarioSignalRPanel.TimerPanel?.Dispose();

                    //Cambiamos estado a cerrado
                    usuarioSignalRPanel.PanelOpen = false;

                    datosCompartidosMonitoreo.SetearUsuarioXEstado(connectionId, usuarioSignalRPanel);
                }
            }
        }



        private void UpdatePanelEstado(object state)
        {
            UsuarioXEstadoSignalR usuarioSignalRPanel = new UsuarioXEstadoSignalR();

            string connectionID = (string)state;

            usuarioSignalRPanel = datosCompartidosMonitoreo.ObtenerUsuarioXEstado(connectionID);

            lock (usuarioSignalRPanel)
            {
                //Llamo metodos del Cliente que actualizan datos
                BroadcastPanelEstadoPanel(connectionID, usuarioSignalRPanel.token);
            }
        }




        private void BroadcastPanelEstadoPanel(string connectionID, string Token)
        {
            List<Tuple<string, int>> listaResultados = new DispositivosAPI().GetDispositivosCategorizados(Token);

            Hub.Clients.Client(connectionID).SendAsync("actualizoPanelEstado", listaResultados);
        }


    }
}
