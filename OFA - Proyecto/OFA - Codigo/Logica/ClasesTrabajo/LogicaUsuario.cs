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
    internal class LogicaUsuario : ILogicaUsuario
    {

        //Singleton
        private static LogicaUsuario _instancia = null;
        private LogicaUsuario() { }
        public static LogicaUsuario GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaUsuario();
            return _instancia;
        }


        public Usuario Logueo(string pUsuID, string pContra)
        {
            Usuario _unUsuario = null;

            _unUsuario = FabricaPersistencia.GetPersistenciaOperador().Logueo(pUsuID, pContra);
            if (_unUsuario == null)
                _unUsuario = FabricaPersistencia.GetPersistenciaAdministrador().Logueo(pUsuID, pContra);

            return _unUsuario;
        }


        public void Alta(Usuario unUsuario, Administrador pAdmLogueado)
        {
            if (unUsuario is Operador)
                FabricaPersistencia.GetPersistenciaOperador().Alta((Operador)unUsuario, pAdmLogueado);
            else
                FabricaPersistencia.GetPersistenciaAdministrador().Alta((Administrador)unUsuario, pAdmLogueado);
        }

        public void Modificar(Usuario unUsuario, Administrador pAdmLogueado)
        {
            if (unUsuario is Operador)
                FabricaPersistencia.GetPersistenciaOperador().Modificar((Operador)unUsuario, pAdmLogueado);
            else
                FabricaPersistencia.GetPersistenciaAdministrador().Modificar((Administrador)unUsuario, pAdmLogueado);
        }

        public void Baja(Usuario unUsuario, Administrador pAdmLogueado)
        {
            if (unUsuario is Operador)
                FabricaPersistencia.GetPersistenciaOperador().Baja((Operador)unUsuario, pAdmLogueado);
            else
                FabricaPersistencia.GetPersistenciaAdministrador().Baja((Administrador)unUsuario, pAdmLogueado);
        }

        public Usuario Buscar(string pUsu, Usuario pUsuLogueado)
        {
            Usuario _unUsuario = null;

            _unUsuario = FabricaPersistencia.GetPersistenciaOperador().Buscar(pUsu, pUsuLogueado);

            if (_unUsuario == null)
                _unUsuario = FabricaPersistencia.GetPersistenciaAdministrador().Buscar(pUsu, pUsuLogueado);
            return _unUsuario;
        }

        public List<Operador> ListadoOperadores(Administrador pAdmLogueado)
        {
            return FabricaPersistencia.GetPersistenciaOperador().ListadoOperadores(pAdmLogueado);
        }

        public List<Administrador> ListadoAdministradores(Administrador pAdmLogueado)
        {
            return FabricaPersistencia.GetPersistenciaAdministrador().ListadoAdministradores(pAdmLogueado);
        }



    }

}
