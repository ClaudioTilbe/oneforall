using EntidadesCompartidas;
using Logica;
using Motor.HilosProcesos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Motor
{
    public class MotorController
    {
        //Variables
        Administrador administradorLog;
        EstadoMotor motorActivo;

        DatosCompartidos datosCompartidos = DatosCompartidos.GetInstancia();



        public void SondeoCentral(Administrador adminLog, EstadoMotor estadoMotor)
        {
            administradorLog = adminLog;

            motorActivo = estadoMotor;

            datosCompartidos.SetearAdministrador("AdministradorMotor", administradorLog);


            //Gestion de hilos
            Thread chequeoMotor = new Thread(DoWorkChequeoMotor); //Hilo 1
            chequeoMotor.Start();

            //Escaneo de puertos
            Thread hiloEscaneosSolicitado = new Thread(DoWorkEscaneosSolicitados);//Hilo 2
            hiloEscaneosSolicitado.Start();

            //Sondeo por analisis Solicitado
            Thread hiloAnalisisSolicitado = new Thread(DoWorkAnalisisSolicitados);//Hilo 3
            hiloAnalisisSolicitado.Start();

            //Sondeo de Dispositivos Descubiertos 
            Thread hiloSondeoDispositivos = new Thread(DoWorkSondeoDispositivos);//Hilo 4
            hiloSondeoDispositivos.Start();

            //Sondeo subreds por Rutina
            Thread hiloAnalisisDeRutina = new Thread(DoWorkAnalisisRutinario);//Hilo 5
            hiloAnalisisDeRutina.Start();

            //Envio de Reportes
            //Thread hiloEnvioReportes = new Thread(DoWorkEnvioReportes);//Hilo 6
            //hiloEnvioReportes.Start();
        }





        //Thread ========================================================================================

        //Hilo 1
        private void DoWorkChequeoMotor()
        {
            while (motorActivo.Activo == true)
            {
                datosCompartidos.SetearString("MotorActivo", "true"); 

                motorActivo = FabricaLogica.GetLogicaEstadoMotor().BuscarUltimo(administradorLog);

                if (motorActivo.Activo == true)
                {
                    Thread.Sleep(5000); // Espero 5 segundos antes de volver a hacer una consulta
                }
                else if (motorActivo.Activo == false)
                {
                    datosCompartidos.SetearString("MotorActivo", motorActivo.Activo.ToString());
                }
            }
        }



        //Hilo 2
        private async void DoWorkEscaneosSolicitados()
        {
            //Chequeada ----------------------------------------------------------------------------

            ProcesosEscaneosPuertos hiloEscaneosSolicitados = new ProcesosEscaneosPuertos();

            await hiloEscaneosSolicitados.EjecutoEscaneosSolicitados();
        }



        //Hilo 3
        private async void DoWorkAnalisisSolicitados()
        {
            //Chequeada ----------------------------------------------------------------------------

            ProcesosAnalisisSolicitados hiloAnalisisSolicitados = new ProcesosAnalisisSolicitados();

            await hiloAnalisisSolicitados.EjecutoAnalisisSolicitados();
        }



        //Hilo 4
        private async void DoWorkSondeoDispositivos()
        {
            //Chequeada ----------------------------------------------------------------------------

            ProcesosSondeoDispositivos hiloSondeosDispositivos = new ProcesosSondeoDispositivos();

            await hiloSondeosDispositivos.EjecutoSondeoDispositivos();
        }



        //Hilo 5
        private async void DoWorkAnalisisRutinario()
        {
            //Chequeada ----------------------------------------------------------------------------

            ProcesosAnalisisRutinario hiloAnalisisRutinarios = new ProcesosAnalisisRutinario();

            await hiloAnalisisRutinarios.EjecutoAnalisisRutinarios();
        }



        //Hilo 6 (Se mueve la funcionabilidad para el Hilo 4, Sondeo de Dispositivos
        //private async void DoWorkEnvioReportes()
        //{
        //    //ProcesosEnvioReportes hiloEnvioReportes = new ProcesosEnvioReportes();

        //    //await hiloEnvioReportes.EjecutoEnvioReportes();
        //}



        // INFORMACION INTERESANTE =====================================================================

        //Metodo para fijarme la maxima cantidad de Threads que puedo tener en este equipo
        //32767
        //int max, c = 0;
        //ThreadPool.GetMaxThreads(out max, out c);




    }
}
