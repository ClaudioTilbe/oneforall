using EntidadesCompartidas;
using Logica.Interfaces;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.ClasesTrabajo
{
    internal class LogicaReporte : ILogicaReporte
    {


        //Singleton
        private static LogicaReporte _instancia = null;
        private LogicaReporte() { }
        public static LogicaReporte GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaReporte();
            return _instancia;
        }



        //Operaciones
        public void Alta(Reporte unReporte, Usuario pUsuLog)
        {
            FabricaPersistencia.GetPersistenciaReporte().Alta(unReporte, pUsuLog);
        }

        public void Baja(Reporte unReporte, Usuario pUsuLog)
        {
            FabricaPersistencia.GetPersistenciaReporte().Baja(unReporte, pUsuLog);
        }

        public void Modificar(Reporte unReporte, Usuario pUsuLogueado)
        {
            FabricaPersistencia.GetPersistenciaReporte().Modificar(unReporte, pUsuLogueado);
        }

        public Reporte Buscar(Mail pMail, Dispositivo pDispositivo, Usuario pUsuLogueado)
        {
            return FabricaPersistencia.GetPersistenciaReporte().Buscar(pMail, pDispositivo, pUsuLogueado);
        }

        public List<Reporte> ListadoReportesXCorreo(Mail unMail, Usuario pUsuLogueado)
        {
            return FabricaPersistencia.GetPersistenciaReporte().ListadoReportesXCorreo(unMail, pUsuLogueado);
        }

        public List<Reporte> ListadoReportesTodos(Administrador adminLog)
        {
            return FabricaPersistencia.GetPersistenciaReporte().ListadoReportesTodos(adminLog);
        }



        public List<Reporte> ListadoReportesXIP(Dispositivo unDispositivo, Administrador adminLog)
        {
            return FabricaPersistencia.GetPersistenciaReporte().ListadoReportesXIP(unDispositivo, adminLog);
        }

    }

}
