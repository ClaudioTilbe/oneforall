using Sitio.Models;

namespace Sitio.Utility
{
    public static class LayoutForViewHelper
    {

        public static string GetLayoutForView(Usuario usuarioLogueado)
        {
            if (usuarioLogueado.Tipo == "Operador")
            {
                return "MPOperador";
            }

            if (usuarioLogueado.Tipo == "Administrador")
            {
                return "MPAdministrador";
            }

            if (usuarioLogueado.Tipo == null)
            {
                return "MPLogueo";
            }

            return "";
        }

    }


}
