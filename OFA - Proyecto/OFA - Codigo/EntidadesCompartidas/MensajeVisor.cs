﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class MensajeVisor
    {

        //Atributos
        private int id;
        private string dispositivoIP;
        private DateTime fechaGenerado;
        private string contenido;

        //private Dispositivo provieneDispositivo;
        //private Usuario _usuario;



        //Propiedades
        public int Id
        {
            get { return id; }
            set
            {
                if (value < 0)
                    throw new Exception("Error en el ID de Mensaje Visor");
                else
                    id = value;
            }
        }

        public string DispositivoIP
        {
            get { return dispositivoIP; }
            set
            {
                Regex exp = new Regex(@"^(\d{1,3}\.){3}\d{1,3}$");

                if (exp.IsMatch(value.Trim()) == false)
                    throw new Exception("Error en la IP del Dispositivo");
                else
                    dispositivoIP = value;
            }
        }

        public DateTime FechaGenerado
        {
            get { return fechaGenerado; }
            set
            {
                if (value > DateTime.Now)
                    throw new Exception("Error en Fecha generado de Mensaje Visor");
                else
                    fechaGenerado = value;
            }
        }

        public string Contenido
        {
            get { return contenido; }
            set
            {
                if ((value.Trim().Length > 200) || (value.Trim().Length < 10))
                    throw new Exception("Error en el Contenido de Mensaje Visor");
                else
                    contenido = value;
            }
        }

        /*
        public Dispositivo ProvieneDispositivo
        {
            get { return provieneDispositivo; }
            set 
            {
                if (value == null)
                    throw new Exception("Error en el Dispositivo asignado de Mensaje Visor");
                else
                    provieneDispositivo = value; 
            }
        }

        public Usuario _Usuario
        {
            get { return _usuario; }
            set 
            {
                if (value == null)
                    throw new Exception("Error en el Usuario de Mensaje Visor");
                else
                    _usuario = value;
            }
        }
        */


        //Constructores
        public MensajeVisor(int pId, string pDispositivoIP, DateTime pFechaGenerado, string pContenido)
        {
            Id = pId;
            DispositivoIP = pDispositivoIP;
            FechaGenerado = pFechaGenerado;
            Contenido = pContenido;
            //ProvieneDispositivo = pProvieneDispositivo;
            //_Usuario = pusuario;
        }

        public MensajeVisor()
        {

        }



    }

}
