using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class DiagramaRed
    {

        //Atributos
        private Sucursal _sucursal;

        private string nombre;
        private DateTime fechaSubida;
        private byte[] diagramaImagen;


        //Propiedades
        public Sucursal _Sucursal
        {
            get { return _sucursal; }
            set 
            {
                if (value == null)
                    throw new Exception("Error en la Subred de Analisis de Red");
                else
                    _sucursal = value;
            }
        }

        public string Nombre
        {
            get { return nombre; }
            set 
            {
                if ((value.Trim().Length > 50) || (value.Trim().Length < 4))
                    throw new Exception("Error en el Nombre de Diagrama de Red");
                else
                    nombre = value;
            }
        }

        public DateTime FechaSubida
        {
            get { return fechaSubida; }
            set
            {
                if (value > DateTime.Now)
                    throw new Exception("Error en Fecha de subida de Diagrama de Red");
                else
                    fechaSubida = value;
            }
        }

        public byte[] DiagramaImagen
        {
            get { return diagramaImagen; }
            set
            {
                if (value == null || value.Length <= 0)
                    throw new Exception("Error en el Nombre de Diagrama de Red");
                else
                    diagramaImagen = value;
            }
        }



        //Constructores
        public DiagramaRed(Sucursal pSucursal, string pNombre, DateTime pFechaSubida, byte[] pDiagramaImagen)
        {
            _Sucursal = pSucursal;

            Nombre = pNombre;
            FechaSubida = pFechaSubida;
            DiagramaImagen = pDiagramaImagen;
        }


        public DiagramaRed()
        {

        }

    }


}
