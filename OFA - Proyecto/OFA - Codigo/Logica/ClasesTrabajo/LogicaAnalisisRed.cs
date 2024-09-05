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
    internal class LogicaAnalisisRed : ILogicaAnalisisRed
    {

        //Singleton
        private static LogicaAnalisisRed _instancia = null;
        private LogicaAnalisisRed() { }
        public static LogicaAnalisisRed GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaAnalisisRed();
            return _instancia;
        }



        //Operaciones
        public void Alta(AnalisisRed unAnalisis, Administrador pUsuLogueado)
        {
            FabricaPersistencia.GetPersistenciaAnalisisRed().Alta(unAnalisis, pUsuLogueado);
        }

        public void Actualizar(AnalisisRed unAnalisis, Administrador pUsuLogueado)
        {
            FabricaPersistencia.GetPersistenciaAnalisisRed().Actualizar(unAnalisis, pUsuLogueado);
        }

        public void Cancelar(AnalisisRed unAnalisis, Administrador pUsuLogueado)
        {
            FabricaPersistencia.GetPersistenciaAnalisisRed().Cancelar(unAnalisis, pUsuLogueado);
        }

        public AnalisisRed Buscar(int analisisID, Administrador pUsuLogueado)
        {
            return FabricaPersistencia.GetPersistenciaAnalisisRed().Buscar(analisisID, pUsuLogueado);
        }

        public List<AnalisisRed> ListadoAnalisisRedTodos(Administrador pUsuLogueado)
        {
            return FabricaPersistencia.GetPersistenciaAnalisisRed().ListadoAnalisisRedTodos(pUsuLogueado);
        }

        public List<AnalisisRed> ListadoAnalisisRedPendientes(Administrador pUsuLogueado)
        {
            return FabricaPersistencia.GetPersistenciaAnalisisRed().ListadoAnalisisRedPendientes(pUsuLogueado);
        }

    }

}
