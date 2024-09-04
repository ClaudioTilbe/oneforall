using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class Sucursal
    {
        //Atributos
        private int nroSucursal;
        private string tipo;
        private string departamento;
        private string calle;
        private int numeroLocal;

        private List<Subred> listaSubred;



        //Propiedades
        public int NroSucursal
        {
            get { return nroSucursal; }
            set
            {
                if ((value > 9999) || (value < 0))
                    throw new Exception("Error en el Numero de Sucursal");
                else
                    nroSucursal = value;
            }
        }

        public string Tipo
        {
            get { return tipo; }
            set
            {
                if ((value.Trim().Length > 50) || (value.Trim().Length < 4))
                    throw new Exception("Error en el Tipo de Sucursal");
                else
                    tipo = value;
            }
        }

        public string Departamento
        {
            get { return departamento; }
            set
            {
                if ((value.Trim().Length > 50) || (value.Trim().Length < 2))
                    throw new Exception("Error en el Departamento de Sucursal");
                else
                    departamento = value;
            }
        }

        public string Calle
        {
            get { return calle; }
            set
            {
                if ((value.Trim().Length > 50) || (value.Trim().Length < 4))
                    throw new Exception("Error en la Calle de Sucursal");
                else
                    calle = value;
            }
        }

        public int NumeroLocal
        {
            get { return numeroLocal; }
            set
            {
                if ((value > 99999999) || (value < 0))
                    throw new Exception("Error en el Numero de Local de Sucursal");
                else
                    numeroLocal = value;
            }
        }

        public List<Subred> ListaSubred
        {
            get { return listaSubred; }
            set { listaSubred = value; }
        }



        //Constructores
        public Sucursal(int pNroSucursal, string pTipo, string pDepartamento, string pCalle, int pNumeroLocal, List<Subred> pListaSubred)
        {
            NroSucursal = pNroSucursal;
            Tipo = pTipo;
            Departamento = pDepartamento;
            Calle = pCalle;
            NumeroLocal = pNumeroLocal;
            ListaSubred = pListaSubred;
        }

        public Sucursal()
        {

        }
    }

}
