using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class Grupo
    {
        //Atributos
        private int codigo;
        private string nombreGrupo;
        private string descripcion;

        private Usuario usuario;
        private List<Dispositivo> dispositivos;



        //Propiedades
        public int Codigo
        {
            get { return codigo; }
            set
            {
                if (value < 0)
                    throw new Exception("Error en el Codigo de Grupo");
                else
                    codigo = value;
            }
        }

        public string NombreGrupo
        {
            get { return nombreGrupo; }
            set
            {
                if ((value.Trim().Length > 30) || (value.Trim().Length < 2))
                    throw new Exception("Error en el Nombre de Grupo");
                else
                    nombreGrupo = value;
            }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                if ((value.Trim().Length > 100) || (value.Trim().Length < 10))
                    throw new Exception("Error en el Nombre de Administrador");
                else
                    descripcion = value;
            }
        }


        public Usuario _Usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;
            }
        }

        public List<Dispositivo> Dispositivos
        {
            get { return dispositivos; }
            set { dispositivos = value; }
        }



        //Constructores
        public Grupo(int pcodigo, string pNombreGrupo, string pDescripcion, Usuario pUsuario, List<Dispositivo> pDispositivos)
        {
            Codigo = pcodigo;
            NombreGrupo = pNombreGrupo;
            Descripcion = pDescripcion;

            _Usuario = pUsuario;
            Dispositivos = pDispositivos;
        }

        public Grupo()
        {

        }
    }

}
