using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Interfaces
{
    public interface ILogicaDiagramaRed
    {

        void Alta(DiagramaRed unDiagrama, Usuario pUsuLog);
        void Baja(int nroSucursal, Usuario pUsuLog);
        DiagramaRed Buscar(int nroSucursal, Usuario pUsuLog);
        List<DiagramaRed> ListadoTodos(Usuario pUsuLog);

    }
}
