using EntidadesCompartidas;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Motor.HilosProcesos
{
    internal class ProcesosAnalisisRutinario
    {

        //Variables
        Administrador adminRoot = new Administrador();
        EstadoMotor estadoMotor = new EstadoMotor();

        DatosCompartidos datosCompartidos = DatosCompartidos.GetInstancia();


        internal async Task EjecutoAnalisisRutinarios()
        {

            try
            {
                adminRoot = datosCompartidos.ObtenerAdministrador("AdministradorMotor");

                //Seteo variable
                estadoMotor.Activo = Convert.ToBoolean(datosCompartidos.ObtenerString("MotorActivo"));

                while (estadoMotor.Activo == true)
                {
                    List<Subred> listaSubredesActivas = FabricaLogica.GetLogicaSubred().ListadoSubredes(adminRoot);
                    List<string> listaSubredesActivasString = new List<string>();

                    if (listaSubredesActivas.Count > 0 && listaSubredesActivas != null)
                    {
                        foreach (Subred unaSubred in listaSubredesActivas)
                        {
                            listaSubredesActivasString.Add(unaSubred.Rango);
                        }

                        for (int i = 0; i < listaSubredesActivas.Count; i++)
                        {
                            string subred = listaSubredesActivasString[i];

                            List<string> listadoIPs = DeterminoIPsXSubred(subred);

                            List<Dispositivo> dispositivosEnSubred = FabricaLogica.GetLogicaDispositivo().
                                ListadoDispositivosXSubred(subred, adminRoot);

                            List<string> direccionesIPFiltradas = listadoIPs
                                                                  .Where(ip => !dispositivosEnSubred.Any(d => d.IP == ip))
                                                                  .ToList();

                            await SondeoListadoXIPAsync(direccionesIPFiltradas); 

                            //Chequeo Motor
                            estadoMotor.Activo = Convert.ToBoolean(datosCompartidos.ObtenerString("MotorActivo"));
                            if (estadoMotor.Activo == false)
                            {
                                break;
                            }
                        }
                    }
                    else //No hay AnalisisRed de momento
                    {
                        Thread.Sleep(5000); // Espero 5 segundos antes de volver a hacer una consulta
                    }

                    estadoMotor.Activo = Convert.ToBoolean(datosCompartidos.ObtenerString("MotorActivo"));

                }//Fin While
            }
            catch (Exception ex)
            {
                MensajeMotor mensaje = new MensajeMotor();

                mensaje.Excepcion = ex.Message;
                mensaje.Mensaje = "ProcesosAnalisisRutinario a dejado de funcionar.";
                mensaje.MetodoOrigen = "ProcesosAnalisisRutinario";
                mensaje.EstadoVariables = "Sin informacion de variables";
                mensaje.FechaGenerado = DateTime.Now;
                mensaje.Tipo = "No identificado";

                FabricaLogica.GetLogicaMensajeMotor().Alta(mensaje, adminRoot);
            }
        }



        //Nivel 1 - Utilizado por procesos en hilos ===============================================================
        private List<string> DeterminoIPsXSubred(string subred)
        {
            List<string> listaDirecciones = new List<string>();

            for (int i = 1; i < 255; i++)
            {
                listaDirecciones.Add(subred + "." + i); //Solo se le pasa por string la IP compuesta
            }

            return listaDirecciones;
        }



        private async Task SondeoListadoXIPAsync(List<string> direccionesIp)
        {
            // Realizar ping a cada dirección IP en paralelo utilizando tareas asincrónicas
            var tareasPing = new List<Task>();

            foreach (var direccionIp in direccionesIp)
            {
                tareasPing.Add(PingDescubrirAsync(direccionIp));
            }

            await Task.WhenAll(tareasPing);
        }



        //Nivel 2 - Utilizado por Nivel 1 =========================================================================
        private async Task PingDescubrirAsync(string direccionIp)
        {
            Ping pingSender = new Ping();
            PingReply respuestaPing = await pingSender.SendPingAsync(direccionIp);

            if (respuestaPing.Status == IPStatus.Success)
            {
                try
                {
                    //Genero alta de Dispositivo y MensajeVisor
                    GenerarDispositivoYMensajeVisor(direccionIp);
                }
                catch (Exception)
                {
                    //Solo quiero que termine el hilo. No va nada aqui
                }
            }
        }



        //Nivel 3 - Utilizado por nivel 2 =========================================================================
        private void GenerarDispositivoYMensajeVisor(string direccionIp)
        {
            //Alta de Dispositivo -------------------------------------------------------------------------

            Dispositivo unDisp = new Dispositivo(direccionIp);

            string[] subred = direccionIp.Split('.');
            string rango = $"{subred[0]}.{subred[1]}.{subred[2]}";

            unDisp.UbSucursal = FabricaLogica.GetLogicaSucursal().BuscarXRango(rango, adminRoot);

            if (unDisp.UbSucursal != null)
            {
                unDisp.Sector = "Desconocido";
                unDisp.Accesible = false;
                unDisp.Conectado = true;
                unDisp.Prioridad = "Indefinida";
                unDisp.Nombre = "Desconocido";
                unDisp.Permanencia = false;
                unDisp.Tipo = "Desconocido";
                unDisp.Ultimaconexion = DateTime.Now;
                unDisp.UltimaNotificacion = DateTime.Now; 

                FabricaLogica.GetLogicaDispositivo().Alta(unDisp, adminRoot);


                //Alta de MENSAJEVISOR -------------------------------------------------------------------------
                string mensaje = "Se a localizado un nuevo Dispositivo en la siguiente IP " + direccionIp + " ";

                MensajeVisor unMensaje = new MensajeVisor(0, DateTime.Now, mensaje, unDisp, adminRoot);

                FabricaLogica.GetLogicaMensajeVisor().Alta(unMensaje, adminRoot);
                //-----------------------------------------------------------------------------------------------  
            }
            else
            {
                MensajeMotor mensaje = new MensajeMotor();

                mensaje.Excepcion = "Error controlado";
                mensaje.Mensaje = "No se encontro una subred con el Rango " + rango + ". La misma pudo haber" +
                                  " sido eliminada antes de que se ejecutara el Analisis de Red rutinario.";
                mensaje.MetodoOrigen = "ProcesosAnalisisRutinario - GenerarDispositivoYMensajeVisor";
                mensaje.EstadoVariables = unDisp.ToString() + ".";
                mensaje.FechaGenerado = DateTime.Now;
                mensaje.Tipo = "Advertencia";

                FabricaLogica.GetLogicaMensajeMotor().Alta(mensaje, adminRoot); 
            }
        }


    }
}
