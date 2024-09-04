using Microsoft.AspNetCore.SignalR;
using Sitio.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using System.Threading;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Irony.Parsing;

namespace Sitio.Hubs
{
    public class MonitoreoRed
    {
        #region Tengo el codigo comentado porque esta en desuso
        ////Variables
        //List<Dispositivo> dispositivos = null;

        //DatosCompartidos datosCompartidosMonitoreo = DatosCompartidos.GetInstancia();

        ////Variables de SignalR
        //private readonly object _sondeoStateLock = new object();
        //private readonly object _updateSondeoConexionLock = new object();
        //private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(2000); //2000
        //private Timer _timer;
        //private volatile bool _updatingSondeoConexiones;
        //private volatile MonitoreoRedState _monitoreoRedState;

        ////Acceso a Session
        //private readonly IHttpContextAccessor _httpContextAccessor;

        ////string signalRConnectionID;
        //UsuarioXEstadoSignalR usuarioSignalR = new UsuarioXEstadoSignalR();



        ////Constructores - Propiedades
        //public MonitoreoRed(IHubContext<MonitoreoRedHub> hub, IHttpContextAccessor httpContextAccessor)
        //{
        //    Hub = hub;
        //    _httpContextAccessor = httpContextAccessor;
        //}

        //private IHubContext<MonitoreoRedHub> Hub
        //{
        //    get;
        //    set;
        //}

        //public MonitoreoRedState MonitoreoRedState
        //{
        //    get { return _monitoreoRedState; }
        //    private set { _monitoreoRedState = value; }
        //}




        ////==============================================================================================
        ////Metodos ======================================================================================
        ////==============================================================================================


        //private Usuario ObtengoUsuarioSession()
        //{
        //    try
        //    {
        //        var httpContext = _httpContextAccessor.HttpContext;
        //        return httpContext.Session.GetObjectFromJson<Usuario>("UsuarioLog");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}




        //public void OpenSondeo(string connectionID)
        //{
        //    Usuario usuLog = ObtengoUsuarioSession();

        //    usuarioSignalR.ConnectionID = connectionID;
        //    usuarioSignalR.SondeoOpen = false;
        //    usuarioSignalR.token = usuLog.Token;

        //    datosCompartidosMonitoreo.SetearUsuarioXEstado("UsuarioSignalRMonitoreo", usuarioSignalR);


        //    //Lo primero que  realizamos es asegurarnos que la ejecucion de la logica dentro de este metodo
        //    //finalice antes de que venga la otra instancia o la otra llamada al metodo. Es para lo que utilizamos
        //    //el objeto "lock"
        //    lock (_sondeoStateLock)
        //    {





        //        if (usuarioSignalR.SondeoOpen != true)
        //        {
        //            //Es decir, cada 2 segundos llama al metodo "UpdateMonitoreo"
        //            usuarioSignalR.TimerMonitoreo = new Timer(UpdateMonitoreo, connectionID, _updateInterval, _updateInterval);

        //            //Mercado Abierto
        //            usuarioSignalR.SondeoOpen = true;

        //            datosCompartidosMonitoreo.SetearUsuarioXEstado("UsuarioSignalRMonitoreo", usuarioSignalR);

        //            //Hacemos un Broadcast para notificar que el mercado se encuentra abierto
        //            BroadcastMonitoreoRedStateChange(usuarioSignalR.ConnectionID, true);
        //        }
        //    }
        //}


        //public void CloseSondeo()
        //{
        //    usuarioSignalR = datosCompartidosMonitoreo.ObtenerUsuarioXEstado("UsuarioSignalRMonitoreo");

        //    lock (_sondeoStateLock)
        //    {
        //        //Si la conexion o el mercado se encuentra abierto y el timer es diferente de null...
        //        if (usuarioSignalR.SondeoOpen == true)
        //        {
        //            usuarioSignalR.TimerMonitoreo?.Dispose();

        //            //Cambiamos estado a cerrado
        //            usuarioSignalR.SondeoOpen = false;

        //            datosCompartidosMonitoreo.SetearUsuarioXEstado("UsuarioSignalRMonitoreo", usuarioSignalR);

        //            //Notificamos a todos los clientes que el mercado fue cerrado
        //            BroadcastMonitoreoRedStateChange(usuarioSignalR.ConnectionID, false);
        //        }
        //    }
        //}



        ////Este metodo trae las actualizaciones
        //private void UpdateMonitoreo(object state)
        //{

        //    string connectionID = (string)state;
        //    usuarioSignalR = datosCompartidosMonitoreo.ObtenerUsuarioXEstado("UsuarioSignalRMonitoreo");

        //    // Función que se ejecuta repetidamente según el intervalo definido en el Timer.
        //    lock (_updateSondeoConexionLock)
        //    {
        //        if (!_updatingSondeoConexiones)
        //        {
        //            _updatingSondeoConexiones = true;

        //            //Llamo metodos del Cliente que actualizan datos
        //            BroadcastPanelEstado(connectionID);
        //            BroadcastVisorMensajes(connectionID);

        //            dispositivos = new DispositivosAPI().GetDispositivosXUsuario(usuarioSignalR.token);

        //            //Comenzamos a enviar la informacion de todos los registros hacia el cliente
        //            foreach (var dispositivo in dispositivos)
        //            {
        //                BroadcastMonitoreoRed(connectionID, dispositivo);
        //            }

        //            _updatingSondeoConexiones = false;
        //        }
        //    }

        //}



        //private void BroadcastMonitoreoRedStateChange(string connectionID, bool isOpen)
        //{

        //    if (isOpen)
        //    {
        //        Hub.Clients.Client(connectionID).SendAsync("monitoreoRedOpened");
        //    }
        //    else
        //    {
        //        Hub.Clients.Client(connectionID).SendAsync("monitoreoRedClosed");
        //    }
        //}




        //private void BroadcastMonitoreoRed(string connectionID, Dispositivo dispositivo)
        //{
        //    Hub.Clients.Client(connectionID).SendAsync("updateMonitoreoRed", dispositivo);
        //}



        //private void BroadcastPanelEstado(string connectionID)
        //{
        //    List<Tuple<string, int>> listaResultados = new DispositivosAPI().GetDispositivosCategorizados(usuarioSignalR.token);

        //    Hub.Clients.Client(connectionID).SendAsync("actualizoPanelEstado", listaResultados);
        //}



        //private void BroadcastVisorMensajes(string connectionID)
        //{
        //    List<MensajeVisor> mensajes = new MensajeVisorAPI().GetMensajeVisorXUsuarioUltimaH(usuarioSignalR.token);

        //    mensajes = mensajes.OrderByDescending(m => m.Id).ToList();

        //    Hub.Clients.Client(connectionID).SendAsync("actualizoVisorMensajes", mensajes);
        //}



        //public List<MensajeVisor> CargoMensajesVisor()
        //{
        //    Usuario usuLog = ObtengoUsuarioSession();

        //    List<MensajeVisor> listaMensajes = new MensajeVisorAPI().GetMensajeVisorXUsuarioUltimaH(usuLog.Token);

        //    listaMensajes = listaMensajes.OrderByDescending(m => m.Id).ToList();

        //    return listaMensajes;
        //}



        //public List<Tuple<string, int>> CargoPanelEstado()
        //{
        //    Usuario usuLog = ObtengoUsuarioSession();

        //    List<Tuple<string, int>> listaResultados = new DispositivosAPI().GetDispositivosCategorizados(usuLog.Token);

        //    return listaResultados;
        //}



        //public List<Dispositivo> ActualizoDispositivosXGrupo(string cadenaGrupo)
        //{
        //    usuarioSignalR = datosCompartidosMonitoreo.ObtenerUsuarioXEstado("UsuarioSignalRMonitoreo");

        //    if (cadenaGrupo == "")
        //    {
        //        dispositivos = new DispositivosAPI().GetDispositivosXUsuario(usuarioSignalR.token);
        //    }
        //    else
        //    {
        //        //Proceso Cadena
        //        int codigoGrupo = Convert.ToInt32(Regex.Match(cadenaGrupo, @"\d+").Value);

        //        dispositivos = new DispositivosAPI().GetDispositivosXGrupo(usuarioSignalR.token, codigoGrupo);
        //    }

        //    return dispositivos;
        //}





        ////==========================================================================================================
        ////Codigo Paginas General (Panel Estado) ====================================================================
        ////==========================================================================================================

        ////VER SI PUEDO HACER MEJOR TODO EN EL MISMO OBJETO CON VARIAS PROPIEDADES OPEN Y CLOSE

        ////Variables
        //private readonly object _panelStateLock = new object();
        //private Timer _timerPanelEstado;
        //private volatile PanelEstadoState _panelEstadoState;
        //private volatile bool _updatingPanelEstado;

        //UsuarioXEstadoSignalR usuarioSignalRPanel = new UsuarioXEstadoSignalR();


        //public PanelEstadoState PanelEstadoState
        //{
        //    get { return _panelEstadoState; }
        //    private set { _panelEstadoState = value; }
        //}



        ////Metodos
        //public void OpenPanelEstado(string connectionIDPanel)
        //{
        //    Usuario usuLog = ObtengoUsuarioSession();

        //    usuarioSignalRPanel.ConnectionID = connectionIDPanel;
        //    usuarioSignalRPanel.PanelOpen = false;
        //    usuarioSignalRPanel.token = usuLog.Token;

        //    datosCompartidosMonitoreo.SetearUsuarioXEstado("UsuarioSignalRMonitoreoPanel", usuarioSignalRPanel);


        //    lock (_panelStateLock)
        //    {
        //        if (usuarioSignalRPanel.SondeoOpen != true)
        //        {
        //            _timerPanelEstado = new Timer(UpdatePanelEstado, null, _updateInterval, _updateInterval);

        //            usuarioSignalRPanel.SondeoOpen = true;

        //            datosCompartidosMonitoreo.SetearUsuarioXEstado("UsuarioSignalRMonitoreoPanel", usuarioSignalRPanel);

        //        }
        //    }
        //}



        //public void ClosePanelEstado()
        //{
        //    usuarioSignalRPanel = datosCompartidosMonitoreo.ObtenerUsuarioXEstado("UsuarioSignalRMonitoreoPanel");

        //    lock (_panelStateLock)
        //    {
        //        if (usuarioSignalRPanel.SondeoOpen == true)
        //        {
        //            if (_timerPanelEstado != null)
        //            {
        //                _timerPanelEstado.Dispose();
        //            }

        //            usuarioSignalRPanel.SondeoOpen = false;
        //        }
        //    }
        //}



        //private void UpdatePanelEstado(object state)
        //{
        //    usuarioSignalRPanel = datosCompartidosMonitoreo.ObtenerUsuarioXEstado("UsuarioSignalRMonitoreoPanel");

        //    lock (_panelStateLock)
        //    {
        //        if (!_updatingPanelEstado)
        //        {
        //            _updatingPanelEstado = true;

        //            //Llamo metodos del Cliente que actualizan datos
        //            BroadcastPanelEstadoPanel();


        //            _updatingPanelEstado = false;
        //        }
        //    }
        //}


        //private void BroadcastPanelEstadoPanel()
        //{
        //    List<Tuple<string, int>> listaResultados = new DispositivosAPI().GetDispositivosCategorizados(usuarioSignalRPanel.token);

        //    Hub.Clients.Client(usuarioSignalRPanel.ConnectionID).SendAsync("actualizoPanelEstado", listaResultados);
        //}


        #endregion

    }


}
