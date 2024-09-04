using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Interfaces
{
    public interface IPersistenciaDiagramaRed
    {

        void Alta(DiagramaRed unDiagrama, Usuario pUsuLogueado);
        void Baja(int nroSucursal, Usuario pUsuLogueado);
        DiagramaRed Buscar(int nroSucursal, Usuario pUsuLogueado);
        List<DiagramaRed> ListadoTodos(Usuario pUsuLogueado);

    }
}
