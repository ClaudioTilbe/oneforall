using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Interfaces
{
    public interface ILogicaEstadoMotor
    {

        void Alta(EstadoMotor estadoMotor, Administrador pUsuLog);

        EstadoMotor BuscarUltimo(Administrador pUsuLog);

    }
}
