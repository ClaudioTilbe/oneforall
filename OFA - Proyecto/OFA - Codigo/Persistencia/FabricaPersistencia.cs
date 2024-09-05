using Persistencia.ClasesTrabajo;
using Persistencia.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class FabricaPersistencia
    {
        public static IPersistenciaDispositivo GetPersistenciaDispositivo()
        {
            return (PersistenciaDispositivo.GetInstancia());
        }

        public static IPersistenciaSucursal GetPersistenciaSucursal()
        {
            return (PersistenciaSucursal.GetInstancia());
        }

        public static IPersistenciaGrupo GetPersistenciaGrupo()
        {
            return (PersistenciaGrupo.GetInstancia());
        }

        public static IPersistenciaAdministrador GetPersistenciaAdministrador()
        {
            return (PersistenciaAdministrador.GetInstancia());
        }

        public static IPersistenciaOperador GetPersistenciaOperador()
        {
            return (PersistenciaOperador.GetInstancia());
        }

        public static IPersistenciaMail GetPersistenciaMail()
        {
            return (PersistenciaMail.GetInstancia());
        }

        public static IPersistenciaReporte GetPersistenciaReporte()
        {
            return (PersistenciaReporte.GetInstancia());
        }

        public static IPersistenciaMensajeVisor GetPersistenciaMensajeVisor()
        {
            return (PersistenciaMensajeVisor.GetInstancia());
        }

        public static IPersistenciaEstadoMotor GetPersistenciaEstadoMotor()
        {
            return (PersistenciaEstadoMotor.GetInstancia());
        }

        public static IPersistenciaAnalisisRed GetPersistenciaAnalisisRed()
        {
            return (PersistenciaAnalisisRed.GetInstancia());
        }

        public static IPersistenciaSubred GetPersistenciaSubred()
        {
            return (PersistenciaSubred.GetInstancia());
        }

        public static IPersistenciaDiagramaRed GetPersistenciaDiagramaRed()
        {
            return (PersistenciaDiagramaRed.GetInstancia());
        }

        public static IPersistenciaEscaneoPuertos GetPersistenciaEscaneoPuertos()
        {
            return (PersistenciaEscaneoPuertos.GetInstancia());
        }

        public static IPersistenciaMensajeMotor GetPersistenciaMensajeAPI()
        {
            return (PersistenciaMensajeMotor.GetInstancia());
        }



    }
}
