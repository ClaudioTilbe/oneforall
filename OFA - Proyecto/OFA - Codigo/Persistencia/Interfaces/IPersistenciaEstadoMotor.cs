using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Interfaces
{
    public interface IPersistenciaEstadoMotor
    {

        void Alta(EstadoMotor estadoMotor, Administrador pUsuLogueado);
        EstadoMotor BuscarUltimo(Administrador pUsuLogueado);

    }
}
