using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Interfaces
{
    public interface ILogicaUsuario
    {

        Usuario Logueo(string pUsuID, string pContra);

        void Alta(Usuario unUsuario, Administrador pAdmLogueado);

        void Modificar(Usuario unUsuario, Administrador pAdmLogueado);

        void Baja(Usuario unUsuario, Administrador pAdmLogueado);

        Usuario Buscar(string pUsu, Usuario pUsuLogueado);

        List<Operador> ListadoOperadores(Administrador pAdmLogueado);

        List<Administrador> ListadoAdministradores(Administrador pAdmLogueado);

    }
}
