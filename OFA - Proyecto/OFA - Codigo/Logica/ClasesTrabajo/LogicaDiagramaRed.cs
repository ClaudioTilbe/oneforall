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
    internal class LogicaDiagramaRed : ILogicaDiagramaRed
    {

        //Singleton
        private static LogicaDiagramaRed _instancia = null;
        private LogicaDiagramaRed() { }
        public static LogicaDiagramaRed GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaDiagramaRed();
            return _instancia;
        }

        //Operaciones
        public void Alta(DiagramaRed unDiagrama, Usuario pUsuLog)
        {
            FabricaPersistencia.GetPersistenciaDiagramaRed().Alta(unDiagrama, pUsuLog);
        }

        public void Baja(int nroSucursal, Usuario pUsuLog)
        {
            FabricaPersistencia.GetPersistenciaDiagramaRed().Baja(nroSucursal, pUsuLog);
        }

        public DiagramaRed Buscar(int nroSucursal, Usuario pUsuLog)
        {
            return FabricaPersistencia.GetPersistenciaDiagramaRed().Buscar(nroSucursal, pUsuLog);
        }

        public List<DiagramaRed> ListadoTodos(Usuario pUsuLog)
        {
            return FabricaPersistencia.GetPersistenciaDiagramaRed().ListadoTodos(pUsuLog);
        }

    }

}
