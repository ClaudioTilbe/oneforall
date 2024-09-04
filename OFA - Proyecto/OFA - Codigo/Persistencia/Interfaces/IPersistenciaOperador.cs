using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Interfaces
{
    public interface IPersistenciaOperador
    {

        Operador Logueo(string pUsuID, string pContra);

        void Alta(Operador unOperador, Administrador pAdmLogueado);

        void Baja(Operador unOperador, Administrador pAdmLogueado);

        void Modificar(Operador unOperador, Administrador pAdmLogueado);

        Operador Buscar(string pUsu, Usuario pUsuLogueado);

        List<Operador> ListadoOperadores(Administrador pAdmLogueado);

    }
}
