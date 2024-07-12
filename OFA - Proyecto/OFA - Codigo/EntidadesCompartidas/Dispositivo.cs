using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class Dispositivo
    {
        //Atributos
        private string ip;
        private string nombre;
        private string tipo;
        private bool conectado;
        private bool accesible;
        private string sector;

        private string prioridad;
        private bool permanencia;
        private DateTime ultimaConexion;
        private DateTime ultimaNotificacion;

        private Sucursal ubSucursal;
        //private Sucursal? ubSucursal;



        //Propiedades
        public string IP
        {
            get { return ip; }
            set 
            {
                Regex exp = new Regex(@"^(\d{1,3}\.){3}\d{1,3}$");

                if (exp.IsMatch(value.Trim()) == false)
                    throw new Exception("Error en la IP del Dispositivo");
                else
                    ip = value;
            }
        }

        public string Nombre
        {
            get { return nombre; }
            set 
            {
                if ((value.Trim().Length > 50) || (value.Trim().Length < 2))
                    throw new Exception("Error en el Nombre de Dispositivo");
                else
                    nombre = value;
            }
        }

        public string Tipo
        {
            get { return tipo; }
            set 
            {
                if ((value.Trim().Length > 30) || (value.Trim().Length < 2))
                    throw new Exception("Error en el Nombre de Dispositivo");
                else
                    tipo = value;
            }
        }

        public bool Conectado
        {
            get { return conectado; }
            set { conectado = value; }
        }

        public bool Accesible
        {
            get { return accesible; }
            set { accesible = value; }
        }

        public string Sector
        {
            get { return sector; }
            set 
            {
                if ((value.Trim().Length > 50) || (value.Trim().Length < 2))
                    throw new Exception("Error en el Sector de Dispositivo");
                else
                    sector = value;
            }
        }

        public string Prioridad
        {
            get { return prioridad; }
            set 
            {
                if ((value.Trim().ToLower() != "alta") && (value.Trim().ToLower() != "media")
                    && (value.Trim().ToLower() != "baja") && (value.Trim().ToLower() != "indefinida"))

                    throw new Exception("Error en la Prioridad de Dispositivo");
                else
                    prioridad = value;
            }
        }

        public bool Permanencia
        {
            get { return permanencia; }
            set { permanencia = value; }
        }

        public DateTime Ultimaconexion
        {
            get { return ultimaConexion; }
            set
            {
                if (value > DateTime.Now)
                    throw new Exception("Error en Ultima conexion de Dispositivo");
                else
                    ultimaConexion = value;
            }
        }

        public DateTime UltimaNotificacion
        {
            get { return ultimaNotificacion; }
            set
            {
                if (value > DateTime.Now)
                    throw new Exception("Error en Ultima notificacion de Dispositivo");
                else
                    ultimaNotificacion = value;
            }
        }

        public Sucursal UbSucursal
        {
            get { return ubSucursal; }
            set 
            { 
                if (value == null)
                    throw new Exception("Error en la Sucursal Ubicada de Dispositivo");
                else
                    ubSucursal = value;
            }
        }



        //Constructores
        public Dispositivo(string pIP, string pNombre, string pTipo, bool pConectado, bool pAccesible,
            string pSector, string pPrioridad, bool pPermanencia, DateTime pUltimaConexion,
            DateTime pUltimaNotificacion, Sucursal pUbSucursal)
        {
            IP = pIP;
            Nombre = pNombre;
            Tipo = pTipo;
            Conectado = pConectado;
            Accesible = pAccesible;
            Sector = pSector;
            Prioridad = pPrioridad;
            Permanencia = pPermanencia;
            Ultimaconexion = pUltimaConexion;
            UltimaNotificacion = pUltimaNotificacion;

            UbSucursal = pUbSucursal;
        }


        public Dispositivo(string pIP)
        {
            IP = pIP;
        }


        public Dispositivo()
        {

        }





    }

}
