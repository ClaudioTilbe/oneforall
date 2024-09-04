using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class EscaneoPuertos
    {

        //Atributos
        private int idEscaneo;

        private Dispositivo dispositivoObjetivo;

        private string razon;
        private string estado;
        private string prioridad;
        private string cadenaSalida;

        private DateTime fechaGenerado;
        private DateTime? fechaFinalizado;

        private Administrador administradorReg;



        //Propiedades
        public int IDEscaneo
        {
            get { return idEscaneo; }
            set 
            {
                if (value < 0)
                    throw new Exception("Error en el ID de Escaneo de Puertos");
                else
                    idEscaneo = value; 
            }
        }

        public Dispositivo DispositivoObjetivo
        {
            get { return dispositivoObjetivo; }
            set 
            {
                if (value == null)
                    throw new Exception("Error en el Dispositivo Objetico de Escaneo de Puertos");
                else
                    dispositivoObjetivo = value;
            }
        }

        public string Razon
        {
            get { return razon; }
            set 
            {
                if ((value.Trim().Length > 200) || (value.Trim().Length < 5))
                    throw new Exception("Error en la Razon de Escaneo de Puerto");
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

                    throw new Exception("Error en el Estado de Escaneo de Puertos");
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

        public string CadenaSalida
        {
            get { return cadenaSalida; }
            set
            {
                if (value.Trim().Length > 3000)
                    throw new Exception("Error en la Razon de Escaneo de Puerto");
                else
                    cadenaSalida = value;
            }
        }

        public DateTime FechaGenerado
        {
            get { return fechaGenerado; }
            set
            {
                if (value > DateTime.Now)
                    throw new Exception("Error en Fecha generado de Escaneo de Puertos");
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
                    fechaFinalizado = value;
            }
        }

        public Administrador AdministradorReg
        {
            get { return administradorReg; }
            set
            {
                if (value == null)
                    throw new Exception("Error en el Administrados Registrado de Escaneo de Puertos");
                else
                    administradorReg = value;
            }
        }


        //Constructores
        public EscaneoPuertos(int pIDEscaneo, Dispositivo pDispositivoObjetivo, string pRazon, string pEstado, string pPrioridad,
            string pCadenaSalida, DateTime pFechaGenerado, DateTime? pFechaFinalizado,
            Administrador pAdministradorReg)
        {
            IDEscaneo = pIDEscaneo;
            DispositivoObjetivo = pDispositivoObjetivo;
            Razon = pRazon;
            Estado = pEstado;
            Prioridad = pPrioridad;
            CadenaSalida = pCadenaSalida;
            FechaGenerado = pFechaGenerado;
            FechaFinalizado = pFechaFinalizado;

            AdministradorReg = pAdministradorReg;
        }

        public EscaneoPuertos()
        {

        }

    }

}
