using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class Mail
    {

        //Atributos
        private string correo;
        private string contraseña;
        private string hostServidor;
        private int puerto;



        //Propiedades
        public string Correo
        {
            get { return correo; }
            set 
            {
                Regex exp = new Regex(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$");

                if (exp.IsMatch(value.Trim()) == false)
                    throw new Exception("El Correo de Mail no tiene un formato válido");
                else
                    correo = value;
            }
        }

        public string Contraseña
        {
            get { return contraseña; }
            set
            {
                if ((value.Trim().Length > 30) || (value.Trim().Length < 8))
                    throw new Exception("Error en la Contraseña de Mail");
                else
                    contraseña = value;
            }
        }

        public string HostServidor
        {
            get { return hostServidor; }
            set
            {
                if ((value.Trim().Length > 50) || (value.Trim().Length < 4))
                    throw new Exception("Error en el Host Servidor de Mail");
                else
                    hostServidor = value;
            }
        }

        public int Puerto
        {
            get { return puerto; }
            set
            {
                if ((value > 65535) || (value <= 0))
                    throw new Exception("Error en el Puerto de Mail");
                else
                    puerto = value;
            }
        }



        //Constructores
        public Mail(string pCorreo, string pContraseña, string pHostServidor, int pPuerto)
        {
            Correo = pCorreo;
            Contraseña = pContraseña;
            HostServidor = pHostServidor;
            Puerto = pPuerto;
        }

        public Mail(string pCorreo)
        {
            Correo = pCorreo;
        }

        public Mail()
        {

        }

    }

}
