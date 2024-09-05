using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class Reporte
    {
        //Atributos
        private int codigo;
        private Mail mailOrigen;
        private Dispositivo dispositivoAsociado;

        private string asunto;
        private string destino;
        private string mensaje;



        //Propiedades
        public int Codigo
        {
            get { return codigo; }
            set
            {
                if (value < 0)
                    throw new Exception("Error en el Codigo de Reporte");
                else
                    codigo = value;
            }
        }

        public Mail MailOrigen
        {
            get { return mailOrigen; }
            set 
            {
                if (value == null)
                    throw new Exception("Error en el Mail de origen de Reporte");
                else
                    mailOrigen = value; 
            }
        }

        public Dispositivo DispositivoAsociado
        {
            get { return dispositivoAsociado; }
            set 
            {
                if (value == null)
                    throw new Exception("Error en el Dispositivo de Reporte");
                else
                    dispositivoAsociado = value;
            }
        }

        public string Asunto
        {
            get { return asunto; }
            set 
            {
                if ((value.Trim().Length > 50) || (value.Trim().Length < 10))
                    throw new Exception("Error en el Asunto de Reporte");
                else
                    asunto = value; 
            }
        }

        public string Destino
        {
            get { return destino; }
            set
            {
                Regex exp = new Regex(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$");

                if (exp.IsMatch(value.Trim()) == false)
                    throw new Exception("El Correo de Destino de Reporte no tiene un formato válido");
                else
                    destino = value;
            }
        }

        public string Mensaje
        {
            get { return mensaje; }
            set
            {
                if ((value.Trim().Length > 500) || (value.Trim().Length < 10))
                    throw new Exception("Error en el Mensaje de Reporte");
                else
                    mensaje = value;
            }
        }



        //Constructores
        public Reporte(int pCodigo, Mail pMailOrigen, Dispositivo pDispositivoAsociado, string pAsunto, string pDestino, string pMensaje)
        {
            Codigo = pCodigo;
            MailOrigen = pMailOrigen;
            DispositivoAsociado = pDispositivoAsociado;
            Asunto = pAsunto;
            Destino = pDestino;
            Mensaje = pMensaje;
        }

        public Reporte()
        {

        }


    }

}
