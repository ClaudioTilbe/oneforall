using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntidadesCompartidas;

namespace Persistencia
{
    internal class Conexion
    {

        internal static string Cnn(Usuario pUsu = null)
        {
            if (pUsu == null)
                return "Data Source =.; Initial Catalog = OFA; Integrated Security = true";
            else
                return "Data Source =.; Initial Catalog = OFA; User=" + pUsu.UsuarioID + "; Password='" + pUsu.Contraseña + "'";
        }

    }
}
