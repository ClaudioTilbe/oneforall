using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
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
            set
            {
                if (value < 1 || value > 99999999)
                    throw new Exception("Error en el Numero funcionario supervisor de Operador");
                else
                    numeroFuncionarioSupervisor = value;
            }
        }

        public string NombreSupervisor
        {
            get { return nombreSupervisor; }
            set 
            {
                if ((value.Trim().Length > 50) || (value.Trim().Length < 2))
                    throw new Exception("Error en el Nombre de Administrador");
                else
                    nombreSupervisor = value;
            }
        }

        public List<Sucursal> SucursalesAsignadas
        {
            get { return sucursalesAsignadas; }
            set { sucursalesAsignadas = value; }
        }



        //Constructores
        public Operador(string pUsuarioID, string pContraseña, Mail pMail,
            int pNumeroFuncionarioSupervisor, string pNombreSupervisor, List<Sucursal> psucursalesAsignadas)
            : base(pUsuarioID, pContraseña, pMail)
        {
            NumeroFuncionarioSupervisor = pNumeroFuncionarioSupervisor;
            NombreSupervisor = pNombreSupervisor;
            SucursalesAsignadas = psucursalesAsignadas;
        }

        public Operador()
        {

        }

    }
}
