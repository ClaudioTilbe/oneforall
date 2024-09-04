using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Interfaces
{
    public interface IPersistenciaMail
    {

        void Modificar(Mail unMail, Usuario pUsuLogueado);

        Mail Buscar(string pCorreo, Usuario pUsuLogueado);

        List<Mail> ListarMails(Administrador pUsuLogueado);

    }
}
