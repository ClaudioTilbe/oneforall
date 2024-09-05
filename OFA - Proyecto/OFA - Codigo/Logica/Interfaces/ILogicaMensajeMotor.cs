using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Interfaces
{
    public interface ILogicaMensajeMotor
    {
        void Alta(MensajeMotor unMensaje, Administrador adminLog);
        void Baja(MensajeMotor unMensaje, Administrador adminLog);
        List<MensajeMotor> ListarXTodos(Administrador adminLog);

    }
}
