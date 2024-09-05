using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Interfaces
{
    public interface ILogicaSucursal
    {

        void Alta(Sucursal unaSucursal, Administrador pAdmLogueado);

        void Baja(Sucursal unaSucursal, Administrador pAdmLogueado);

        void Modificar(Sucursal unaSucursal, Administrador pAdmLogueado);

        Sucursal Buscar(int pNroSucursal, Usuario pUsuLogueado);

        Sucursal BuscarXRango(string rango, Administrador adminLog);

        List<Sucursal> ListadoTodas(Usuario pUsuLogueado);

        List<Sucursal> ListarSucursalesXUsuario(Usuario pUsu, Usuario pUsuLogueado);

        List<Subred> ListarSubredesExistentes(Administrador pAdmLogueado);

    }
}
