using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public abstract class Usuario
    {

        //Atributos
        private string usuarioID;
        private string contraseña;

        private Mail mailAsignado;



        //Propiedades
        public string UsuarioID
        {
            get { return usuarioID; }
            set 
            {
                if ((value.Trim().Length > 20) || (value.Trim().Length < 4))
                    throw new Exception("Error en el ID del Usuario");
                else
                    usuarioID = value;
            }
        }

        public string Contraseña
        {
            get { return contraseña; }
            set 
            {
                if ((value.Trim().Length > 20) || (value.Trim().Length < 12))
                    throw new Exception("Error en la Contraseña del Usuario");
                else
                    contraseña = value;
            }
        }

        public Mail MailAsignado
        {
            get { return mailAsignado; }
            set 
            {
                if (value == null)
                    throw new Exception("Error en el Mail del Usuario");
                else
                    mailAsignado = value;
            }
        }


        //constructores
        public Usuario(string pUsuarioID, string pContraseña, Mail pMailAsignado)
        {
            UsuarioID = pUsuarioID;
            Contraseña = pContraseña;
            MailAsignado = pMailAsignado;
        }

        public Usuario()
        {

        }


    }


}
