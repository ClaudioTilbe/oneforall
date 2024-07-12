using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Interfaces
{
    public interface ILogicaGrupo
    {

        void Alta(Grupo unGrupo, Usuario pUsuLog);


        void Modificar(Grupo unGrupo, Usuario pUsuLog);

        void Baja(Grupo unGrupo, Usuario pUsuLog);

        List<Grupo> ListadoGruposXUsuario(Usuario pUsuLog);


    }
}
