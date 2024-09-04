using System.Collections.Concurrent;

namespace Sitio.Hubs
{
    public class DatosCompartidos
    {

        //Singleton
        private static DatosCompartidos _instancia = null;
        private DatosCompartidos() { }
        public static DatosCompartidos GetInstancia()
        {
            if (_instancia == null)
                _instancia = new DatosCompartidos();
            return _instancia;
        }



        private readonly ConcurrentDictionary<string, string> variables = new ConcurrentDictionary<string, string>();

        public void SetearString(string nombreVariable, string valor)
        {
            variables[nombreVariable] = valor;
        }

        public object ObtenerString(string nombreVariable)
        {
            return variables.GetOrAdd(nombreVariable, _ => null);
        }






        //Compartir Usuario ====================================================================================
        private readonly ConcurrentDictionary<string, UsuarioXEstadoSignalR> usuarioSignalR = new ConcurrentDictionary<string, UsuarioXEstadoSignalR>();

        public void SetearUsuarioXEstado(string nombreVariable, UsuarioXEstadoSignalR adminMotor)
        {
            usuarioSignalR[nombreVariable] = adminMotor;
        }

        public UsuarioXEstadoSignalR ObtenerUsuarioXEstado(string nombreVariable)
        {
            return usuarioSignalR.GetOrAdd(nombreVariable, _ => new UsuarioXEstadoSignalR());
        }





    }
}
