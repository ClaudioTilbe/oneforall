using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Interfaces
{
    public interface IPersistenciaMensajeVisor
    {

        void Alta(MensajeVisor unMensaje, Administrador pAdmLogueado);

        List<MensajeVisor> ListarMensajeVisorXDispositivo(Dispositivo unDispositivo, Usuario pUsuLogueado);

        List<MensajeVisor> ListarMensajeVisorXDispositivoUltimaH(Dispositivo unDispositivo, Usuario pUsuLogueado);

        List<MensajeVisor> ListarMensajeVisorXUsuarioUltimaH(Usuario pUsuLogueado);
    }
}
