using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class MensajeMotor
    {
        //Atributos
        private int idMensaje;
        private string excepcion;
        private string mensaje;
        private string metodoOrigen;
        private string estadoVariables;
        private DateTime fechaGenerado;
        private string tipo;



        //Propiedades
        public int IDMensaje
        {
            get { return idMensaje; }
            set
            {
                if (value < 0)
                    throw new Exception("Error en el ID de Mensaje API");
                else
                    idMensaje = value;
            }
        }

        public string Excepcion
        {
            get { return excepcion; }
            set
            {
                if ((value.Trim().Length > 5000) || (value.Trim().Length < 5))
                    throw new Exception("Error en la Excepcion de Mensaje API");
                else
                    excepcion = value;
            }
        }

        public string Mensaje
        {
            get { return mensaje; }
            set
            {
                if ((value.Trim().Length > 300) || (value.Trim().Length < 5))
                    throw new Exception("Error en Mensaje de Mensaje API");
                else
                    mensaje = value;
            }
        }

        public string MetodoOrigen
        {
            get { return metodoOrigen; }
            set
            {
                if ((value.Trim().Length > 300) || (value.Trim().Length < 5))
                    throw new Exception("Error en el Metodo de Origen de Mensaje API");
                else
                    metodoOrigen = value;
            }
        }

        public string EstadoVariables
        {
            get { return estadoVariables; }
            set
            {
                if ((value.Trim().Length > 1000) || (value.Trim().Length < 5))
                    throw new Exception("Error en el Estado de variables de Mensaje API");
                else
                    estadoVariables = value;
            }
        }

        public DateTime FechaGenerado
        {
            get { return fechaGenerado; }
            set
            {
                if (value > DateTime.Now)
                    throw new Exception("Error en Fecha generado de Mensaje API");
                else
                    fechaGenerado = value;
            }
        }

        public string Tipo
        {
            get { return tipo; }
            set
            {
                if ((value.Trim().Length > 30) || (value.Trim().Length < 2))
                    throw new Exception("Error en el Tipo de Mensaje API");
                else
                    tipo = value;
            }
        }



        //Constructores
        public MensajeMotor(int pIDMensaje, string pExcepcion, string pMensaje, string pMetodoOrigen, string pEstadoVariables,
                          DateTime pFechaGenerado, string pTipo)
        {
            IDMensaje = pIDMensaje;
            Excepcion = pExcepcion;
            Mensaje = pMensaje;
            MetodoOrigen = pMetodoOrigen;
            EstadoVariables = pEstadoVariables;
            FechaGenerado = pFechaGenerado;
            Tipo = pTipo;
        }

        public MensajeMotor()
        {

        }
    }


}
