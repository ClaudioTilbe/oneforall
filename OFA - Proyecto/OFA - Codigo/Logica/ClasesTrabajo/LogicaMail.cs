using EntidadesCompartidas;
using Logica.Interfaces;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.ClasesTrabajo
{
    internal class LogicaMail : ILogicaMail
    {

        //Singleton
        private static LogicaMail _instancia = null;
        private LogicaMail() { }
        public static LogicaMail GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaMail();
            return _instancia;
        }



        public void Modificar(Mail unMail, Usuario pUsuLog)
        {
            FabricaPersistencia.GetPersistenciaMail().Modificar(unMail, pUsuLog);
        }

        public Mail Buscar(string pCorreo, Usuario pUsuLog)
        {
            return FabricaPersistencia.GetPersistenciaMail().Buscar(pCorreo, pUsuLog);
        }

        public List<Mail> ListarMails(Administrador usuLog)
        {
            return FabricaPersistencia.GetPersistenciaMail().ListarMails(usuLog);
        }

    }

}
