using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Interfaces
{
    public interface ILogicaMail
    {

        void Modificar(Mail unMail, Usuario pUsuLog);

        Mail Buscar(string pCorreo, Usuario pUsuLog);

        List<Mail> ListarMails(Administrador usuLog);

    }

}
