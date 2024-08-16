using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Interfaces
{
    public interface ILogicaMensajeVisor
    {

        void Alta(MensajeVisor unMensaje, Administrador pAdmLog);

        List<MensajeVisor> ListarMensajeVisorXDispositivo(Dispositivo unDispositivo, Usuario pUsuLogueado);

        List<MensajeVisor> ListarMensajeVisorXDispositivoUltimaH(Dispositivo unDispositivo, Usuario pUsuLogueado);

        //List<MensajeVisor> ListarMensajeVisorXUsuarioUltimaH(Usuario pUsuLogueado);

        void BajaXTiempo(Administrador pUsuLogueado);
    }
}
