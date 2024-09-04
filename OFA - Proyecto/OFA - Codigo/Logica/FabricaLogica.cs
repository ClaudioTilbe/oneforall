using Logica.ClasesTrabajo;
using Logica.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class FabricaLogica
    {
        public static ILogicaDispositivo GetLogicaDispositivo()
        {
            return (LogicaDispositivo.GetInstancia());
        }

        public static ILogicaSucursal GetLogicaSucursal()
        {
            return (LogicaSucursal.GetInstancia());
        }

        public static ILogicaUsuario GetLogicaUsuario()
        {
            return (LogicaUsuario.GetInstancia());
        }

        public static ILogicaGrupo GetLogicaGrupo()
        {
            return (LogicaGrupo.GetInstancia());
        }

        public static ILogicaMail GetLogicaMail()
        {
            return (LogicaMail.GetInstancia());
        }

        public static ILogicaReporte GetLogicaReporte()
        {
            return (LogicaReporte.GetInstancia());
        }

        public static ILogicaMensajeVisor GetLogicaMensajeVisor()
        {
            return (LogicaMensajeVisor.GetInstancia());
        }

        public static ILogicaEstadoMotor GetLogicaEstadoMotor()
        {
            return (LogicaEstadoMotor.GetInstancia());
        }

        public static ILogicaAnalisisRed GetLogicaAnalisisRed()
        {
            return (LogicaAnalisisRed.GetInstancia());
        }

        public static ILogicaSubred GetLogicaSubred()
        {
            return (LogicaSubred.GetInstancia());
        }

        public static ILogicaDiagramaRed GetLogicaDiagramaRed()
        {
            return (LogicaDiagramaRed.GetInstancia());
        }

        public static ILogicaEscaneoPuertos GetLogicaEscaneoPuertos()
        {
            return (LogicaEscaneoPuertos.GetInstancia());
        }

        public static ILogicaMensajeMotor GetLogicaMensajeMotor()
        {
            return (LogicaMensajeMotor.GetInstancia());
        }

    }
}
