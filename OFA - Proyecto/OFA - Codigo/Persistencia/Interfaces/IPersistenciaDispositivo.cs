using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Interfaces
{
    public interface IPersistenciaDispositivo
    {
        void Alta(Dispositivo unDispositivo, Usuario pUsuLogueado);

        void Baja(string dispositivoIP, Usuario pUsuLogueado);

        void Modificar(Dispositivo unDispositivo, Usuario pUsuLogueado);

        Dispositivo Buscar(string pIP, Usuario pUsuLogueado);

        void ActualizarEstadoConexion(Dispositivo unDispositivo, Administrador pUsuLogueado);

        void ActualizarEstadoNotificacion(Dispositivo unDispositivo, Administrador pUsuLogueado);

        List<Dispositivo> ListadoTodos(Usuario pUsuLogueado);

        List<Dispositivo> ListadoDispositivosXSubred(string subred, Usuario pUsuLogueado);

        List<Dispositivo> ListadoDispositivosXGrupo(int codigoGrupo, Usuario pUsuLogueado);

        List<Dispositivo> ListadoDispositivosXSucursal(Sucursal unaSucursal, Usuario pUsuLogueado);


    }
}
