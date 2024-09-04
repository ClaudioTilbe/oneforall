using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class Administrador : Usuario
    {

        //Atributos
        private string nombre;
        private int numeroFuncionario;
        private string cargo;



        //Propiedades
        public string Nombre
        {
            get { return nombre; }
            set
            {
                if ((value.Trim().Length > 50) || (value.Trim().Length < 2))
                    throw new Exception("Error en el Nombre de Administrador");
                else
                    nombre = value;
            }
        }

        public int NumeroFuncionario
        {
            get { return numeroFuncionario; }
            set 
            {
                if (value < 1 || value > 99999999)
                    throw new Exception("Error en el Numero funcionario de Administrador");
                else
                    numeroFuncionario = value;
            }
        }

        public string Cargo
        {
            get { return cargo; } 
            set 
            {
                if ((value.Trim().Length > 50) || (value.Trim().Length < 2))
                    throw new Exception("Error en el Cargo de Administrador");
                else
                    cargo = value;
            }
        }



        //Constructores
        public Administrador(string pUsuarioID, string pContraseña, Mail pMail,
            string pNombre, int pNumeroFuncionario, string pCargo)
            : base(pUsuarioID, pContraseña, pMail)
        {
            Nombre = pNombre;
            NumeroFuncionario = pNumeroFuncionario;
            Cargo = pCargo;
        }

        public Administrador()
        {

        }


    }
}
