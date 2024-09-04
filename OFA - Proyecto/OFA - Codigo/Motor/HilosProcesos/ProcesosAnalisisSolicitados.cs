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
    internal class ProcesosAnalisisSolicitados
    {

        //Variables
        Administrador adminRoot = new Administrador();
        List<AnalisisRed> listaAnalisisRed = new List<AnalisisRed>();
        EstadoMotor estadoMotor = new EstadoMotor();

        DatosCompartidos datosCompartidos = DatosCompartidos.GetInstancia();

        AnalisisRed analisisRedEjectuado = new AnalisisRed();




        internal async Task EjecutoAnalisisSolicitados()
        {
            try
            {
                adminRoot = datosCompartidos.ObtenerAdministrador("AdministradorMotor");

                //Seteo variable
                estadoMotor.Activo = Convert.ToBoolean(datosCompartidos.ObtenerString("MotorActivo"));

                while (estadoMotor.Activo == true)
                {
                    listaAnalisisRed = FabricaLogica.GetLogicaAnalisisRed().ListadoAnalisisRedPendientes(adminRoot);

                    if (listaAnalisisRed.Count > 0 && listaAnalisisRed != null)
                    {

                        // Ordenar la lista por Prioridad (Alta, Media, Baja)
                        List<AnalisisRed> listaOrdenada = listaAnalisisRed
                            .OrderByDescending(escaneo => escaneo.Prioridad == "Alta")
                            .ThenByDescending(escaneo => escaneo.Prioridad == "Media")
                            .ThenBy(escaneo => escaneo.Prioridad == "Baja")
                            .ToList();


                        int conteoDispositivosInicial = 0;
                        int conteoDispositivosFinal = 0;

                        //Seteo el AnalisisRed actual para poder generar el mensaje de error mas adelante
                        analisisRedEjectuado = new AnalisisRed();
                        analisisRedEjectuado = listaOrdenada[0];

                        string subred = listaOrdenada[0].SubredAnalizada;

                        List<string> listadoIPs = DeterminoIPsXSubred(subred);

                        List<Dispositivo> dispositivosEnSubred = FabricaLogica.GetLogicaDispositivo().
                           ListadoDispositivosXSubred(subred, adminRoot);

                        conteoDispositivosInicial = dispositivosEnSubred.Count;

                        List<string> direccionesIPFiltradas = listadoIPs
                                                             .Where(ip => !dispositivosEnSubred.Any(d => d.IP == ip))
                                                             .ToList();

                        await SondeoListadoXIPAsync(direccionesIPFiltradas);

                        //Luego de ya haber analisado la red hago el conteo
                        dispositivosEnSubred = new List<Dispositivo>();
                        dispositivosEnSubred = FabricaLogica.GetLogicaDispositivo().
                           ListadoDispositivosXSubred(subred, adminRoot);

                        conteoDispositivosFinal = dispositivosEnSubred.Count;

                        if (conteoDispositivosFinal >= conteoDispositivosInicial)
                        {
                            analisisRedEjectuado.NuevosDispositivos = conteoDispositivosFinal - conteoDispositivosInicial;
                        }


                        //Chequeo si la subred que envie analizar sigue ahi (La pudo haber borrado un usuario)
                        List<Subred> subredesActivas = FabricaLogica.GetLogicaSubred().ListadoSubredes(adminRoot);

                        bool subredEncontrada = subredesActivas.Any(s => s.Rango == subred);

                        if (subredEncontrada == true)
                        {
                            analisisRedEjectuado.Estado = "Finalizado";
                        }
                        else //false
                        {
                            analisisRedEjectuado.Estado = "Cancelado";
                            analisisRedEjectuado.NuevosDispositivos = 0;
                        }

                        //Actualizo el Analisis de Red, lo paso a Finalizado
                        analisisRedEjectuado.FechaFinalizado = DateTime.Now;


                        FabricaLogica.GetLogicaAnalisisRed().Actualizar(analisisRedEjectuado, adminRoot);

                        //Chequeo Motor
                        estadoMotor.Activo = Convert.ToBoolean(datosCompartidos.ObtenerString("MotorActivo"));
                        if (estadoMotor.Activo == false)
                        {
                            break;
                        }

                    }
                    else //No hay AnalisisRed de momento
                    {
                        Thread.Sleep(5000); // Espero 5 segundos antes de volver a hacer una consulta
                    }

                    estadoMotor.Activo = Convert.ToBoolean(datosCompartidos.ObtenerString("MotorActivo"));

                }//Fin while

            }
            catch (Exception ex)
            {
                MensajeMotor mensaje = new MensajeMotor();

                mensaje.Excepcion = ex.Message;
                mensaje.Mensaje = "ProcesosAnalisisSolicitados a dejado de funcionar.";
                mensaje.MetodoOrigen = "ProcesosAnalisisSolicitados";
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
                listaDirecciones.Add(subred + "." + i); //Solo agrega un string con la IP generada
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

                MensajeVisor unMensaje = new MensajeVisor(0, unDisp.IP, DateTime.Now, mensaje);

                FabricaLogica.GetLogicaMensajeVisor().Alta(unMensaje, adminRoot);
                //-----------------------------------------------------------------------------------------------  
            }
            else
            {
                MensajeMotor mensaje = new MensajeMotor();

                mensaje.Excepcion = "Error controlado";
                mensaje.Mensaje = "No se encontro una subred con el Rango " + rango + ". La misma pudo haber" +
                    " sido eliminada antes de que se ejecutara el Analisis de Red de ID " + analisisRedEjectuado.IDAnalisis.ToString();
                mensaje.MetodoOrigen = "ProcesosAnalisisSolicitados - GenerarDispositivoYMensajeVisor";
                mensaje.EstadoVariables = unDisp.ToString() + ".";
                mensaje.FechaGenerado = DateTime.Now;
                mensaje.Tipo = "Advertencia";

                FabricaLogica.GetLogicaMensajeMotor().Alta(mensaje, adminRoot);
            }

        }

    }
}
