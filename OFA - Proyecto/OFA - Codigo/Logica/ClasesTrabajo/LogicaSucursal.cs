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
    internal class LogicaSucursal : ILogicaSucursal
    {
        //Singleton
        private static LogicaSucursal _instancia = null;
        private LogicaSucursal() { }
        public static LogicaSucursal GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaSucursal();
            return _instancia;
        }



        //Operaciones
        public void Alta(Sucursal unaSucursal, Administrador pAdmLogueado)
        {
            FabricaPersistencia.GetPersistenciaSucursal().Alta(unaSucursal, pAdmLogueado);
        }

        public void Baja(Sucursal unaSucursal, Administrador pAdmLogueado)
        {
            FabricaPersistencia.GetPersistenciaSucursal().Baja(unaSucursal, pAdmLogueado);
        }

        public void Modificar(Sucursal unaSucursal, Administrador pAdmLogueado)
        {
            FabricaPersistencia.GetPersistenciaSucursal().Modificar(unaSucursal, pAdmLogueado);
        }

        public Sucursal Buscar(int pNroSucursal, Usuario pUsuLogueado)
        {
            return (FabricaPersistencia.GetPersistenciaSucursal().Buscar(pNroSucursal, pUsuLogueado));
        }

        public Sucursal BuscarXRango(string rango, Administrador adminLog)
        {
            return (FabricaPersistencia.GetPersistenciaSucursal().BuscarXRango(rango, adminLog));
        }

        public List<Sucursal> ListadoTodas(Usuario pUsuLogueado)
        {
            return (FabricaPersistencia.GetPersistenciaSucursal().ListarSucursales(pUsuLogueado));
        }


        public List<Sucursal> ListarSucursalesXUsuario(Usuario pUsu, Usuario pUsuLogueado)
        {
            List<Sucursal> listaSucursales = new List<Sucursal>();

            if (pUsu is Operador)
            {
                listaSucursales = FabricaPersistencia.GetPersistenciaSucursal().ListarSucursalesXUsuario(pUsu, pUsuLogueado);
            }
            else
            {
                listaSucursales = FabricaPersistencia.GetPersistenciaSucursal().ListarSucursales(pUsu);
            }


            return listaSucursales;
        }

        public List<Subred> ListarSubredesExistentes(Administrador pAdmLogueado)
        {
            return (FabricaPersistencia.GetPersistenciaSucursal().ListarSubredesExistentes(pAdmLogueado));
        }

    }

}
