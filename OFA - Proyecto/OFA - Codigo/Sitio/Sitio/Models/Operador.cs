using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sitio.Models
{
    public class Operador : Usuario
    {

        //Atributos
        private int numeroFuncionarioSupervisor;
        private string nombreSupervisor;

        private List<Sucursal> sucursalesAsignadas;



        //Propiedades
        public int NumeroFuncionarioSupervisor
        {
            get { return numeroFuncionarioSupervisor; }
            set { numeroFuncionarioSupervisor = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string NombreSupervisor
        {
            get { return nombreSupervisor; }
            set { nombreSupervisor = value; }
        }

        public List<Sucursal> SucursalesAsignadas
        {
            get { return sucursalesAsignadas; }
            set { sucursalesAsignadas = value; }
        }



        //Constructores
        public Operador(string pUsuarioID, string pContraseña, string pConfirmContra, Mail pMail, string pToken, string pTipo,
            int pNumeroFuncionarioSupervisor, string pNombreSupervisor, List<Sucursal> psucursalesAsignadas)
            : base(pUsuarioID, pContraseña, pConfirmContra, pToken, pTipo, pMail)
        {
            NumeroFuncionarioSupervisor = pNumeroFuncionarioSupervisor;
            NombreSupervisor = pNombreSupervisor;
            SucursalesAsignadas = psucursalesAsignadas;
        }

        public Operador()
        {

        }



        //Validar Contenido de Objeto
        public void Validar()
        {
            if ((this.UsuarioID.Trim().Length > 50) || (this.UsuarioID.Trim().Length < 2))
                throw new Exception("Error en el nombre de administrador. Debe tener entre 2 y 50 caracteres.");

            if ((this.Contraseña.Trim().Length > 20) || (this.Contraseña.Trim().Length < 12))
                throw new Exception("Error en la contraseña del usuario. Debe tener entre 12 y 20 caracteres.");

            //if ((this.ConfirmContra.Trim().Length > 20) || (this.ConfirmContra.Trim().Length < 12))
            //    throw new Exception("Error en la confirmacion de contraseña del usuario.");

            if (this.MailAsignado == null)
                throw new Exception("Error en el mail del usuario. El correo no tiene un mail asignado.");

            if (this.NumeroFuncionarioSupervisor < 1 || this.NumeroFuncionarioSupervisor > 99999999)
                throw new Exception("Error en el numero funcionario supervisor de operador. Debe ser mayor a 1 y menor a 100000000");

            if ((this.NombreSupervisor.Trim().Length > 50) || (this.NombreSupervisor.Trim().Length < 2))
                throw new Exception("Error en el nombre de administrador. Debe tener entre 2 y 50 caracteres.");
        }


    }


}
