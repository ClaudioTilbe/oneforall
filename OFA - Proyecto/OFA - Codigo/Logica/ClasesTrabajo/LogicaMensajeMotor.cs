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
    internal class LogicaMensajeMotor : ILogicaMensajeMotor
    {


        //Singleton
        private static LogicaMensajeMotor _instancia = null;
        private LogicaMensajeMotor() { }
        public static LogicaMensajeMotor GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaMensajeMotor();
            return _instancia;
        }



        //Operaciones
        public void Alta(MensajeMotor unMensaje, Administrador adminLog)
        {
            FabricaPersistencia.GetPersistenciaMensajeAPI().Alta(unMensaje, adminLog);
        }

        public void Baja(MensajeMotor unReporte, Administrador adminLog)
        {
            FabricaPersistencia.GetPersistenciaMensajeAPI().Baja(unReporte, adminLog);
        }

        public List<MensajeMotor> ListarXTodos(Administrador adminLog)
        {
            return FabricaPersistencia.GetPersistenciaMensajeAPI().ListarXTodos(adminLog);
        }

    }
}
