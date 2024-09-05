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
    internal class LogicaDispositivo : ILogicaDispositivo
    {

        //Singleton
        private static LogicaDispositivo _instancia = null;
        private LogicaDispositivo() { }
        public static LogicaDispositivo GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaDispositivo();
            return _instancia;
        }



        //Operaciones
        public void Alta(Dispositivo unDispositivo, Usuario pUsuLog)
        {
            FabricaPersistencia.GetPersistenciaDispositivo().Alta(unDispositivo, pUsuLog);
        }



        public void Baja(string dispositivoIP, Usuario pUsuLog)
        {
            FabricaPersistencia.GetPersistenciaDispositivo().Baja(dispositivoIP, pUsuLog);
        }



        public void Modificar(Dispositivo unDispositivo, Usuario pUsuLog)
        {
            FabricaPersistencia.GetPersistenciaDispositivo().Modificar(unDispositivo, pUsuLog);
        }



        public Dispositivo Buscar(string pIP, Usuario pUsuLog)
        {
            return FabricaPersistencia.GetPersistenciaDispositivo().Buscar(pIP, pUsuLog);
        }



        public void ActualizarEstadoConexion(Dispositivo unDispositivo, Administrador pUsuLogueado)
        {
            FabricaPersistencia.GetPersistenciaDispositivo().ActualizarEstadoConexion(unDispositivo, pUsuLogueado);
        }



        public void ActualizarEstadoNotificacion(Dispositivo unDispositivo, Administrador pUsuLogueado)
        {
            FabricaPersistencia.GetPersistenciaDispositivo().ActualizarEstadoNotificacion(unDispositivo, pUsuLogueado);
        }



        public List<Dispositivo> ListadoTodos(Usuario pUsuLog)
        {
            return FabricaPersistencia.GetPersistenciaDispositivo().ListadoTodos(pUsuLog);
        }



        //Metodo para Categorizar los Dispositivos
        public List<Tuple<string, int>> ListadoTodosCategorizo(Usuario pUsuLog)
        {
            List<Dispositivo> listaDispositivos = null;

            listaDispositivos = GetInstancia().ListadoDispositivosXUsuario(pUsuLog);

            List<Tuple<string, int>> listaResultados = new List<Tuple<string, int>>();

            //Conectados ----------------------------------------------------------------------
            int conectados = (from unDisp in listaDispositivos
                              where unDisp.Conectado == true
                              select unDisp).Count();

            listaResultados.Add(new Tuple<string, int>("Conectados", conectados));

            //Criticos ------------------------------------------------------------------------
            int criticos = (from unDisp in listaDispositivos
                            where unDisp.Conectado == false && unDisp.Permanencia == true
                            select unDisp).Count();

            listaResultados.Add(new Tuple<string, int>("Criticos", criticos));

            //Alerta ---------------------------------------------------------------------------
            int alerta = (from unDisp in listaDispositivos
                          where unDisp.Conectado == false && unDisp.Permanencia == false
                          select unDisp).Count();

            listaResultados.Add(new Tuple<string, int>("Alerta", alerta));

            //Desconocidos ---------------------------------------------------------------------
            int desconocidos = (from unDisp in listaDispositivos
                                where unDisp.Nombre == "Desconocido"
                                select unDisp).Count();

            listaResultados.Add(new Tuple<string, int>("Desconocidos", desconocidos));

            //Todos ----------------------------------------------------------------------------
            listaResultados.Add(new Tuple<string, int>("Todos", listaDispositivos.Count()));


            return listaResultados;
        }



        public List<Dispositivo> ListadoDispositivosXSubred(string subred, Usuario pUsuLogueado)
        {
            return FabricaPersistencia.GetPersistenciaDispositivo().ListadoDispositivosXSubred(subred, pUsuLogueado);
        }



        public List<Dispositivo> ListadoDispositivosXGrupo(int codigoGrupo, Usuario pUsuLogueado)
        {
            return FabricaPersistencia.GetPersistenciaDispositivo().ListadoDispositivosXGrupo(codigoGrupo, pUsuLogueado);
        }



        public List<Dispositivo> ListadoDispositivosXSucursal(Sucursal unaSucursal, Usuario pUsuLogueado)
        {
            return FabricaPersistencia.GetPersistenciaDispositivo().ListadoDispositivosXSucursal(unaSucursal, pUsuLogueado);
        }



        public List<Dispositivo> ListadoDispositivosXUsuario(Usuario pUsuLogueado)
        {
            List<Dispositivo> listaDispositivos = new List<Dispositivo>();

            if (pUsuLogueado is Operador)
            {
                foreach (Sucursal unaSucu in ((Operador)pUsuLogueado).SucursalesAsignadas)
                {
                    listaDispositivos.AddRange(FabricaPersistencia.GetPersistenciaDispositivo().ListadoDispositivosXSucursal(unaSucu, pUsuLogueado));
                }
            }
            else if (pUsuLogueado is Administrador)
            {
                listaDispositivos = FabricaPersistencia.GetPersistenciaDispositivo().ListadoTodos(pUsuLogueado);
            }

            return listaDispositivos;
        }




    }


}
