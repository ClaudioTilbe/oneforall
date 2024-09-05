using System;
using System.ComponentModel.DataAnnotations;

namespace Sitio.Models
{
    public class Administrador : Usuario
    {

        //Atributos
        private string nombre;
        private int numeroFuncionario;
        private string cargo;



        //Propiedades
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public int NumeroFuncionario
        {
            get { return numeroFuncionario; }
            set { numeroFuncionario = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Cargo
        {
            get { return cargo; }
            set { cargo = value; }
        }



        //Constructores
        public Administrador(string pUsuarioID, string pContraseña, string pConfirmContra, Mail pMail, string pToken, string pTipo,
            string pNombre, int pNumeroFuncionario, string pCargo)
            : base(pUsuarioID, pContraseña, pConfirmContra, pToken, pTipo, pMail)
        {
            Nombre = pNombre;
            NumeroFuncionario = pNumeroFuncionario;
            Cargo = pCargo;
        }

        public Administrador()
        {

        }



        //validar Contenido de Objeto
        public void Validar()
        {
            if ((this.UsuarioID.Trim().Length > 50) || (this.UsuarioID.Trim().Length < 2))
                throw new Exception("Error en el nombre de administrador. Debe tener entre 2 y 50 caracteres.");

            if ((this.Contraseña.Trim().Length > 20) || (this.Contraseña.Trim().Length < 12))
                throw new Exception("Error en la contraseña del usuario. Debe tener entre 12 y 20 caracteres.");

            //if ((this.ConfirmContra.Trim().Length > 20) || (this.ConfirmContra.Trim().Length < 12))
            //    throw new Exception("Error en la confirmacion de contraseña del usuario.");

            if (this.MailAsignado == null)
                throw new Exception("Error en el mail del usuario. No existe mail asignado a ese correo.");

            if (this.NumeroFuncionario < 1 || this.NumeroFuncionario > 99999999)
                throw new Exception("Error en el numero funcionario de administrador. Debe ser mayor a 0 y no mayor a 100000000");

            if ((this.Cargo.Trim().Length > 50) || (this.Cargo.Trim().Length < 2))
                throw new Exception("Error en el cargo de administrador. Debe tener entre 2 y 50 caracteres.");
        }

    }




}
