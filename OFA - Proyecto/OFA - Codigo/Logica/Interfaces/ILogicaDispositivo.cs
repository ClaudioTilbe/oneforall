using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Interfaces
{
    public interface ILogicaDispositivo
    {
        void Alta(Dispositivo unDispositivo, Usuario pUsuLog);

        void Baja(string dispositivoIP, Usuario pUsuLog);

        void Modificar(Dispositivo unDispositivo, Usuario pUsuLog);

        Dispositivo Buscar(string pIP, Usuario pUsuLog);

        void ActualizarEstadoConexion(Dispositivo unDispositivo, Administrador pUsuLogueado);

        void ActualizarEstadoNotificacion(Dispositivo unDispositivo, Administrador pUsuLogueado);

        List<Dispositivo> ListadoTodos(Usuario pUsuLog);

    
        List<Tuple<string, int>> ListadoTodosCategorizo(Usuario pUsuLog);

        List<Dispositivo> ListadoDispositivosXSubred(string subred, Usuario pUsuLogueado);

        List<Dispositivo> ListadoDispositivosXGrupo(int codigoGrupo, Usuario pUsuLogueado);

        List<Dispositivo> ListadoDispositivosXSucursal(Sucursal unaSucursal, Usuario pUsuLogueado);

        List<Dispositivo> ListadoDispositivosXUsuario(Usuario pUsuLogueado);

    }
}
