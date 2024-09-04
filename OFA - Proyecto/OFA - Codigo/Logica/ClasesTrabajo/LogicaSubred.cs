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
    internal class LogicaSubred : ILogicaSubred
    {

        //Singleton
        private static LogicaSubred _instancia = null;
        private LogicaSubred() { }
        public static LogicaSubred GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaSubred();
            return _instancia;
        }



        //Operaciones
        public List<Subred> ListadoSubredes(Administrador pUsuLogueado)
        {
            return FabricaPersistencia.GetPersistenciaSubred().ListadoSubredes(pUsuLogueado);
        }

    }

}
