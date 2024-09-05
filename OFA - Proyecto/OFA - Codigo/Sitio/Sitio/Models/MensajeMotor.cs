using Newtonsoft.Json;
using Sitio.Utility;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace Sitio.Models
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
            set { idMensaje = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Excepcion
        {
            get { return excepcion; }
            set { excepcion = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Mensaje
        {
            get { return mensaje; }
            set { mensaje = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MetodoOrigen
        {
            get { return metodoOrigen; }
            set { metodoOrigen = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string EstadoVariables
        {
            get { return estadoVariables; }
            set { estadoVariables = value; }
        }

        public DateTime FechaGenerado
        {
            get { return fechaGenerado; }
            set { fechaGenerado = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
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



        //validar Contenido de Objeto
        public void Validar()
        {
            if (this.IDMensaje < 0)
                throw new Exception("Error en el id de mensaje API.");

            if ((this.Excepcion.Trim().Length > 5000) || (this.Excepcion.Trim().Length < 5))
                throw new Exception("Error en la excepcion de mensaje API.");

            if ((this.Mensaje.Trim().Length > 300) || (this.Mensaje.Trim().Length < 5))
                throw new Exception("Error en mensaje de mensaje API.");

            if ((this.MetodoOrigen.Trim().Length > 300) || (this.MetodoOrigen.Trim().Length < 5))
                throw new Exception("Error en el metodo de origen de mensaje API.");

            if ((this.EstadoVariables.Trim().Length > 1000) || (this.EstadoVariables.Trim().Length < 5))
                throw new Exception("Error en el estado de variables de mensaje API.");

            if (this.FechaGenerado > DateTime.Now)
                throw new Exception("Error en fecha generado de mensaje API.");

            if ((this.Tipo.Trim().Length > 30) || (this.Tipo.Trim().Length < 2))
                throw new Exception("Error en el tipo de mensaje API.");
        }

    }




    public class MensajeMotorAPI
    {

        public List<MensajeMotor> GetMensajesMotorAPI(Administrador adminLog)
        {

            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminLog.Token);

                    var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, ConexionAPIRest.ConnexionAPI + "mensajesMotor"));

                    if (!response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new Exception($"La petición a la API falló: El usuario no esta autorizado para esta accion");
                        }
                        else
                        {
                            //Visualizar Excepcion en servidor
                            var errorResponse = response.Content.ReadAsStringAsync().Result;
                            var errormessage = JsonConvert.DeserializeObject<ErrorResponse>(errorResponse).Details;

                            throw new Exception($"La petición a la API falló: {errormessage}");
                        }
                    }

                    var content = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<List<MensajeMotor>>(content);
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }







    }


}

