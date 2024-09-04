using EntidadesCompartidas;
using Logica;
using Motor.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Motor.HilosProcesos
{
    internal class ProcesosEscaneosPuertos
    {

        //Variables
        Administrador adminRoot = new Administrador();
        List<EscaneoPuertos> listaEscaneos = new List<EscaneoPuertos>();
        EstadoMotor estadoMotor = new EstadoMotor();

        DatosCompartidos datosCompartidos = DatosCompartidos.GetInstancia();

        EscaneoPuertos escaneoPuertosEjectuado = new EscaneoPuertos();



        internal async Task EjecutoEscaneosSolicitados()
        {
            try
            {
                adminRoot = datosCompartidos.ObtenerAdministrador("AdministradorMotor");

                //Seteo variable
                estadoMotor.Activo = Convert.ToBoolean(datosCompartidos.ObtenerString("MotorActivo"));

                while (estadoMotor.Activo == true)
                {
                    listaEscaneos = FabricaLogica.GetLogicaEscaneoPuertos().ListadoEscaneoPuertosPendientes(adminRoot);

                    if (listaEscaneos.Count > 0 && listaEscaneos != null)
                    {

                        // Ordenar la lista por Prioridad (Alta, Media, Baja)
                        List<EscaneoPuertos> listaOrdenada = listaEscaneos
                            .OrderByDescending(escaneo => escaneo.Prioridad == "Alta")
                            .ThenByDescending(escaneo => escaneo.Prioridad == "Media")
                            .ThenBy(escaneo => escaneo.Prioridad == "Baja")
                            .ToList();


                        //Seteo el AnalisisRed actual para poder generar el mensaje de error mas adelante
                        escaneoPuertosEjectuado = new EscaneoPuertos();
                        escaneoPuertosEjectuado = listaOrdenada[0];

                        //Actualizo su estado
                        escaneoPuertosEjectuado.Estado = "Ejecutandose";
                        FabricaLogica.GetLogicaEscaneoPuertos().Actualizar(escaneoPuertosEjectuado, adminRoot);

                        //Envio escaneo a Nmap
                        escaneoPuertosEjectuado.CadenaSalida = NmapConnector.EjecutoEscaneoRapido
                                                               (escaneoPuertosEjectuado.DispositivoObjetivo.IP);

                        //Actualizo el Escaneo de puertos, lo paso a Finalizado
                        escaneoPuertosEjectuado.FechaFinalizado = DateTime.Now;
                        escaneoPuertosEjectuado.Estado = "Finalizado";

                        FabricaLogica.GetLogicaEscaneoPuertos().Actualizar(escaneoPuertosEjectuado, adminRoot);

                        //Chequeo Motor
                        estadoMotor.Activo = Convert.ToBoolean(datosCompartidos.ObtenerString("MotorActivo"));
                        if (estadoMotor.Activo == false)
                        {
                            break;
                        }

                    }
                    else //No hay Escaneo de puertos de momento
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
                mensaje.Mensaje = "ProcesosEscaneosPuertos a dejado de funcionar.";
                mensaje.MetodoOrigen = "ProcesosEscaneosPuertos";
                mensaje.EstadoVariables = "Sin informacion de variables";
                mensaje.FechaGenerado = DateTime.Now;
                mensaje.Tipo = "no identificado";

                FabricaLogica.GetLogicaMensajeMotor().Alta(mensaje, adminRoot);
            }
        }


    }
}
