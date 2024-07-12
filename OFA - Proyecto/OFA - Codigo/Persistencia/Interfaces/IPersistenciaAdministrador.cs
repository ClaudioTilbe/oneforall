using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Interfaces
{
    public interface IPersistenciaAdministrador
    {

        Administrador Logueo(string pUsuID, string pContra);

        void Alta(Administrador unAdministrador, Administrador pAdmLogueado);

        void Baja(Administrador unAdministrador, Administrador pAdmLogueado);

        void Modificar(Administrador unAdministrador, Administrador pAdmLogueado);

        Administrador Buscar(string pUsu, Usuario pUsuLogueado);

        List<Administrador> ListadoAdministradores(Administrador pAdmLogueado);

    }
}
