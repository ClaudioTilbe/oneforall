using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Interfaces
{
    public interface ILogicaAnalisisRed
    {

        void Alta(AnalisisRed unAnalisis, Administrador pUsuLogueado);
        void Actualizar(AnalisisRed unAnalisis, Administrador pUsuLogueado);
        void Cancelar(AnalisisRed unAnalisis, Administrador pUsuLogueado);
        AnalisisRed Buscar(int analisisID, Administrador pUsuLogueado);
        List<AnalisisRed> ListadoAnalisisRedTodos(Administrador pUsuLogueado);
        List<AnalisisRed> ListadoAnalisisRedPendientes(Administrador pUsuLogueado);

    }
}
