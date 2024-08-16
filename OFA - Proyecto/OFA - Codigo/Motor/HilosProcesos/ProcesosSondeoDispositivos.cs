using EntidadesCompartidas;
using Logica;
using Motor.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Motor.HilosProcesos
{
    internal class ProcesosSondeoDispositivos
    {

        //Variables
        Administrador adminRoot = new Administrador();
        EstadoMotor estadoMotor = new EstadoMotor();
        List<Dispositivo> dispositivosSondeados = new List<Dispositivo>();
        List<Dispositivo> listaDispositivosTotal = new List<Dispositivo>();

        DatosCompartidos datosCompartidos = DatosCompartidos.GetInstancia();



        internal async Task EjecutoSondeoDispositivos()
        {
            try
            {
                adminRoot = datosCompartidos.ObtenerAdministrador("AdministradorMotor");

                //Seteo variable
                estadoMotor.Activo = Convert.ToBoolean(datosCompartidos.ObtenerString("MotorActivo"));

                while (estadoMotor.Activo == true)
                {
                    listaDispositivosTotal = new List<Dispositivo>();
                    listaDispositivosTotal = FabricaLogica.GetLogicaDispositivo().ListadoTodos(adminRoot);


                    for (int i = 0; listaDispositivosTotal.Count > i; i++)
                    {
                        //Chequeo si existe el dispositivo en la lista que estan Pingeando actualmente
                        bool existe = false;
                        existe = dispositivosSondeados.Any(dispositivo => dispositivo.IP == listaDispositivosTotal[i].IP);

                        if (existe == false)
                        {
                            dispositivosSondeados.Add(listaDispositivosTotal[i]);
                            var tareaPing = PingDispositivoAsync(listaDispositivosTotal[i]);
                        }
                    }

                    dispositivosSondeados = listaDispositivosTotal;

                    //Seteo variable
                    estadoMotor.Activo = Convert.ToBoolean(datosCompartidos.ObtenerString("MotorActivo"));

                } //Fin While

                //Vacio lista, ya que al hacer esto cada Sondeo individual al no encontrar el dispositivo aqui finaliza
                listaDispositivosTotal = new List<Dispositivo>();
            }
            catch (Exception ex)
            {
                MensajeMotor mensaje = new MensajeMotor();

                mensaje.Excepcion = ex.Message;
                mensaje.Mensaje = "ProcesosSondeoDispositivos a dejado de funcionar.";
                mensaje.MetodoOrigen = "ProcesosSondeoDispositivos";
                mensaje.EstadoVariables = "Sin informacion de variables";
                mensaje.FechaGenerado = DateTime.Now;
                mensaje.Tipo = "No identificado";

                FabricaLogica.GetLogicaMensajeMotor().Alta(mensaje, adminRoot);
            }
        }



        //Nivel 2 - Utilizado por Nivel 1 =========================================================================

        internal async Task PingDispositivoAsync(Dispositivo unDispositivo)
        {
            bool dispositivoActivo = true;
            Ping pingSender = new Ping();

            List<Reporte> reportesXDispositivo = new List<Reporte>();

            while (dispositivoActivo == true)
            {
                PingReply respuestaPing = await pingSender.SendPingAsync(unDispositivo.IP);

                reportesXDispositivo = FabricaLogica.GetLogicaReporte().ListadoReportesXIP(unDispositivo, adminRoot);



                if (respuestaPing.Status == IPStatus.Success)
                {
                    bool guardoConectividad = unDispositivo.Conectado;

                    //Actualizo ESTADO Dispositivo ------------------------------------------------------------------
                    unDispositivo.Conectado = true;
                    unDispositivo.Ultimaconexion = DateTime.Now;

                    FabricaLogica.GetLogicaDispositivo().ActualizarEstadoConexion(unDispositivo, adminRoot);
                    //----------------------------------------------------------------------------------------------- 


                    if (unDispositivo.Prioridad == "Media" && guardoConectividad == false)
                    {
                        //Alta de MENSAJEVISOR -------------------------------------------------------------------------
                        string mensaje = "La dirección IP " + unDispositivo.IP + " está conectada.";

                        MensajeVisor unMensaje = new MensajeVisor(0, unDispositivo.IP, DateTime.Today, mensaje);

                        FabricaLogica.GetLogicaMensajeVisor().Alta(unMensaje, adminRoot);
                        //-----------------------------------------------------------------------------------------------  
                    }
                }
                else
                {
                    bool guardoConectividad = unDispositivo.Conectado;

                    if (reportesXDispositivo.Count > 0 && reportesXDispositivo != null)
                    {
                        //Envio de Reportes =======================================================================
                        foreach (Reporte reporte in reportesXDispositivo)
                        {
                            MailSend enviador = new MailSend();

                            if (reporte.DispositivoAsociado.Conectado == false && reporte.DispositivoAsociado.Permanencia == true)
                            {
                                int minutosXPrioridad = ObtengoCadenciaXPrioridad(reporte.DispositivoAsociado);

                                // Calcular la diferencia en minutos entre la fecha y hora actual y 'fechaInicial'
                                TimeSpan diferencia = DateTime.Now - reporte.DispositivoAsociado.UltimaNotificacion;

                                // Obtener la diferencia en minutos como un entero
                                int minutosPasados = (int)diferencia.TotalMinutes;

                                if (minutosPasados >= minutosXPrioridad)
                                {
                                    enviador.EnviarMail(reporte.MailOrigen, reporte);

                                    //Actualizo ESTADO Dispositivo ------------------------------------------------------------------
                                    reporte.DispositivoAsociado.UltimaNotificacion = DateTime.Now;

                                    FabricaLogica.GetLogicaDispositivo()
                                        .ActualizarEstadoNotificacion(reporte.DispositivoAsociado, adminRoot);
                                    //----------------------------------------------------------------------------------------------- 
                                }
                            }

                        } //Fin Foreach

                    }//Fin If


                    //Actualizo ESTADO Dispositivo ------------------------------------------------------------------
                    unDispositivo.Conectado = false;

                    FabricaLogica.GetLogicaDispositivo().ActualizarEstadoConexion(unDispositivo, adminRoot);
                    //----------------------------------------------------------------------------------------------- 


                    if (unDispositivo.Prioridad == "Media" && guardoConectividad == true)
                    {
                        //Alta de MENSAJEVISOR -------------------------------------------------------------------------
                        string mensaje = "La dirección IP " + unDispositivo.IP + " no está conectada.";

                        MensajeVisor unMensaje = new MensajeVisor(0, unDispositivo.IP, DateTime.Today, mensaje);

                        FabricaLogica.GetLogicaMensajeVisor().Alta(unMensaje, adminRoot);
                        //-----------------------------------------------------------------------------------------------  
                    }
                }

                Dispositivo buscoDispositivo = FabricaLogica.GetLogicaDispositivo().Buscar(unDispositivo.IP, adminRoot);

                if (buscoDispositivo != null)
                {
                    //Actualizo el dispositivo (Esta el caso de que el usuario lo modifique)
                    unDispositivo = buscoDispositivo;
                    dispositivoActivo = true;

                    await Task.Delay(TimeSpan.FromSeconds(3));
                }
                else
                {
                    dispositivoActivo = false;
                }
            } //Fin While
        }



        private static int ObtengoCadenciaXPrioridad(Dispositivo unDisp)
        {
            int minutosXPrioridad = 0;

            if (unDisp.Prioridad == "Baja")
            {
                minutosXPrioridad = 30;
            }
            else if (unDisp.Prioridad == "Media")
            {
                minutosXPrioridad = 15;
            }
            else if (unDisp.Prioridad == "Alta")
            {
                minutosXPrioridad = 5;
            }

            return minutosXPrioridad;
        }







    }
}
