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
    internal class LogicaMensajeVisor : ILogicaMensajeVisor
    {

        //Singleton
        private static LogicaMensajeVisor _instancia = null;
        private LogicaMensajeVisor() { }
        public static LogicaMensajeVisor GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaMensajeVisor();
            return _instancia;
        }


        public void Alta(MensajeVisor unMensaje, Administrador pAdmLog)
        {
            FabricaPersistencia.GetPersistenciaMensajeVisor().Alta(unMensaje, pAdmLog);
        }

        public List<MensajeVisor> ListarMensajeVisorXDispositivo(Dispositivo unDispositivo, Usuario pUsuLogueado)
        {
            return FabricaPersistencia.GetPersistenciaMensajeVisor().ListarMensajeVisorXDispositivo(unDispositivo, pUsuLogueado);
        }

        public List<MensajeVisor> ListarMensajeVisorXDispositivoUltimaH(Dispositivo unDispositivo, Usuario pUsuLogueado)
        {
            return FabricaPersistencia.GetPersistenciaMensajeVisor().ListarMensajeVisorXDispositivoUltimaH(unDispositivo, pUsuLogueado);
        }

        //public List<MensajeVisor> ListarMensajeVisorXUsuarioUltimaH(Usuario pUsuLogueado)
        //{
        //    return FabricaPersistencia.GetPersistenciaMensajeVisor().ListarMensajeVisorXUsuarioUltimaH(pUsuLogueado);
        //}

        public void BajaXTiempo(Administrador pUsuLogueado)
        {
            FabricaPersistencia.GetPersistenciaMensajeVisor().BajaXTiempo(pUsuLogueado);
        }

    }

}
