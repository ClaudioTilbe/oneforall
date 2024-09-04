using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class EstadoMotor
    {

        //Atributos
        private int idEstado;
        private bool activo;
        private DateTime ultimaModificacion;

        private string usuarioRegistra;



        //Propiedades
        public int IDEstado
        {
            get { return idEstado; }
            set
            {
                if (value < 0)
                    throw new Exception("Error en el ID de Estado Motor");
                else
                    idEstado = value;
            }
        }

        public bool Activo
        {
            get { return activo; }
            set { activo = value; }
        }

        public DateTime UltimaModificacion
        {
            get { return ultimaModificacion; }
            set
            {
                if (value > DateTime.Now)
                    throw new Exception("Error en Ultima modificacion de Estado Motor");
                else
                    ultimaModificacion = value;
            }
        }

        public string UsuarioRegistra
        {
            get { return usuarioRegistra; }
            set
            {
                if ((value.Trim().Length > 20) || (value.Trim().Length < 4))
                    throw new Exception("Error en el Usuario registrado de EstadoMotor");
                else
                    usuarioRegistra = value;
            }
        }



        //Constructores
        public EstadoMotor(int pIDEstado, bool pActivo, DateTime pUltimaModificacion, string pUsuarioRegistra)
        {
            IDEstado = pIDEstado;
            Activo = pActivo;
            UltimaModificacion = pUltimaModificacion;

            UsuarioRegistra = pUsuarioRegistra;
        }

        public EstadoMotor(int pIDEstado, bool pActivo, DateTime pUltimaModificacion)
        {
            IDEstado = pIDEstado;
            Activo = pActivo;
            UltimaModificacion = pUltimaModificacion;
        }

        public EstadoMotor()
        {

        }

    }

}
