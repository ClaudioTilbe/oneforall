using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Interfaces
{
    public interface IPersistenciaGrupo
    {

        void Alta(Grupo unGrupo, Usuario pUsuLogueado);


        void Modificar(Grupo unGrupo, Usuario pUsuLogueado);

        void Baja(Grupo unGrupo, Usuario pUsuLogueado);

        List<Grupo> ListadoGruposXUsuario(Usuario pUsuLog);


    }
}
