using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Interfaces
{
    public interface IPersistenciaReporte
    {

        void Alta(Reporte unReporte, Usuario pUsuLogueado);

        void Baja(Reporte unReporte, Usuario pUsuLogueado);

        void Modificar(Reporte unReporte, Usuario pUsuLogueado);

        Reporte Buscar(Mail pMail, Dispositivo pDispositivo, Usuario pUsuLogueado);

        List<Reporte> ListadoReportesXCorreo(Mail unMail, Usuario pUsuLogueado);

        List<Reporte> ListadoReportesTodos(Administrador adminLog);

        List<Reporte> ListadoReportesXIP(Dispositivo unDispositivo, Administrador adminLog);

    }
}
