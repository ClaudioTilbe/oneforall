using EntidadesCompartidas;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Motor.HilosProcesos
{
    internal class ProcesosGestionMensajesVisor
    {

        //Variables
        Administrador adminRoot = new Administrador();
        EstadoMotor estadoMotor = new EstadoMotor();

        DatosCompartidos datosCompartidos = DatosCompartidos.GetInstancia();


        internal Task EjecutoBajaMensajesVisorXDia()
        {
            try
            {
                adminRoot = datosCompartidos.ObtenerAdministrador("AdministradorMotor");

                //Seteo variable
                estadoMotor.Activo = Convert.ToBoolean(datosCompartidos.ObtenerString("MotorActivo"));

                while (estadoMotor.Activo == true)
                {
                    FabricaLogica.GetLogicaMensajeVisor().BajaXTiempo(adminRoot);

                    Thread.Sleep(60000); // Espero 60 segundos antes de volver a hacer una consulta

                    estadoMotor.Activo = Convert.ToBoolean(datosCompartidos.ObtenerString("MotorActivo"));

                }//Fin While
            }
            catch (Exception ex)
            {
                MensajeMotor mensaje = new MensajeMotor();

                mensaje.Excepcion = ex.Message;
                mensaje.Mensaje = "ProcesosGestionMensajesVisor a dejado de funcionar.";
                mensaje.MetodoOrigen = "ProcesosGestionMensajesVisor";
                mensaje.EstadoVariables = "Sin informacion de variables";
                mensaje.FechaGenerado = DateTime.Now;
                mensaje.Tipo = "Fallo durante la eliminacion de Mensajes Visor por dia.";

                FabricaLogica.GetLogicaMensajeMotor().Alta(mensaje, adminRoot);
            }

            return Task.CompletedTask;
        }
    }
}
