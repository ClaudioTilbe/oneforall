using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class AnalisisRed
    {

        //Atributos
        private int idAnalisis;
        private string razon;
        private string estado;
        private string prioridad;
        private int nuevosDispositivos;

        private DateTime fechaGenerado;
        private DateTime? fechaFinalizado;

        private string subredAnalizada;

        private Administrador administradorReg;



        //Propiedades
        public int IDAnalisis
        {
            get { return idAnalisis; }
            set
            {
                if (value < 0)
                    throw new Exception("Error en el ID de Analisis de Red");
                else
                    idAnalisis = value;
            }
        }

        public string Razon
        {
            get { return razon; }
            set 
            {
                if ((value.Trim().Length > 200) || (value.Trim().Length < 5))
                    throw new Exception("Error en la Razon de Analisis de Red");
                else
                    razon = value;
            }
        }

        public string Estado
        {
            get { return estado; }
            set 
            {
                if ((value.Trim().ToLower() != "pendiente") && (value.Trim().ToLower() != "ejecutandose") 
                    && (value.Trim().ToLower() != "cancelado") && (value.Trim().ToLower() != "finalizado"))

                    throw new Exception("Error en el Estado de Analisis de Red");
                else
                    estado = value;
            }
        }

        public string Prioridad
        {
            get { return prioridad; }
            set 
            {
                if ((value.Trim().ToLower() != "alta") && (value.Trim().ToLower() != "media")
                     && (value.Trim().ToLower() != "baja"))

                    throw new Exception("Error en la Prioridad de Analisis de Red");
                else
                    prioridad = value;
            }
        }

        public int NuevosDispositivos
        {
            get { return nuevosDispositivos; }
            set 
            {
                if (value < 0)
                    throw new Exception("Error en los Nuevos Dispositivos de Analisis de Red");
                else
                    nuevosDispositivos = value; 
            }
        }

        public DateTime FechaGenerado
        {
            get { return fechaGenerado; }
            set
            {
                if (value > DateTime.Now)
                    throw new Exception("Error en Fecha de generado de Analisis de Red");
                else
                    fechaGenerado = value;
            }
        }

        public DateTime? FechaFinalizado
        {
            get { return fechaFinalizado; }
            set
            {
                if (value == null)
                    fechaFinalizado = null;

                else if (value > DateTime.Now)
                    throw new Exception("Error en Fecha de finalizado de Analisis de Red");
                else
                    fechaFinalizado  = value;
            }
        }

        public string SubredAnalizada
        {
            get { return subredAnalizada; }
            set
            {
                Regex exp = new Regex(@"^(\d{1,3}\.){2}\d{1,3}$"); // Expresión regular para validar xxx.xxx.xxx

                if (!exp.IsMatch(value.Trim()))
                    throw new Exception("Error en la subred analizada de el Analisis de red");
                else
                    subredAnalizada = value;
            }
        }

        public Administrador AdministradorReg
        {
            get { return administradorReg; }
            set
            {
                if (value == null)
                    throw new Exception("Error en el Administrados Registrado de Analisis de Red");
                else
                    administradorReg = value;
            }
        }



        //Constructores
        public AnalisisRed(int pIDAnalisis, string pRazon, string pEstado, string pPrioridad,
            int pNuevosDispositivos, DateTime pFechaGenerado, DateTime? pFechaFinalizado, string pSubredAnalizada,
            Administrador pAdministradorReg)
        {
            IDAnalisis = pIDAnalisis;
            Razon = pRazon;
            Estado = pEstado;
            Prioridad = pPrioridad;
            NuevosDispositivos = pNuevosDispositivos;
            FechaGenerado = pFechaGenerado;
            FechaFinalizado = pFechaFinalizado;
            SubredAnalizada = pSubredAnalizada;

            AdministradorReg = pAdministradorReg;
        }

        public AnalisisRed()
        {

        }


    }


}
