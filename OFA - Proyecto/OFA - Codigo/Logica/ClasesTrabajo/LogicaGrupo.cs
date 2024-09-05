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
    internal class LogicaGrupo : ILogicaGrupo
    {

        //Singleton
        private static LogicaGrupo _instancia = null;
        private LogicaGrupo() { }
        public static LogicaGrupo GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaGrupo();
            return _instancia;
        }



        public void Alta(Grupo unGrupo, Usuario pUsuLog)
        {
            FabricaPersistencia.GetPersistenciaGrupo().Alta(unGrupo, pUsuLog);
        }


        public void Modificar(Grupo unGrupo, Usuario pUsuLog)
        {
            FabricaPersistencia.GetPersistenciaGrupo().Modificar(unGrupo, pUsuLog);
        }

        public void Baja(Grupo unGrupo, Usuario pUsuLog)
        {
            FabricaPersistencia.GetPersistenciaGrupo().Baja(unGrupo, pUsuLog);
        }

        public List<Grupo> ListadoGruposXUsuario(Usuario pUsuLog)
        {
            return FabricaPersistencia.GetPersistenciaGrupo().ListadoGruposXUsuario(pUsuLog);
        }



    }

}
