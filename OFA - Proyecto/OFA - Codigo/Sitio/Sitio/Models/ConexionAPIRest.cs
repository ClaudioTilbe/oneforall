namespace Sitio.Models
{
    internal class ConexionAPIRest
    {
        private static string _conexionAPI = "http://localhost:2626/api/";

        public static string ConnexionAPI
        {
            get { return _conexionAPI; }

        }
    }
}
