using EntidadesCompartidas;
using Persistencia.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motor.HilosProcesos
{
    internal class DatosCompartidos
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









        //Compartir Administrador ====================================================================================
        private readonly ConcurrentDictionary<string, Administrador> administradorMotor = new ConcurrentDictionary<string, Administrador>();

        public void SetearAdministrador(string nombreVariable, Administrador adminMotor)
        {
            administradorMotor[nombreVariable] = adminMotor;
        }

        public Administrador ObtenerAdministrador(string nombreVariable)
        {
            return administradorMotor.GetOrAdd(nombreVariable, _ => new Administrador());
        }


    }
}
