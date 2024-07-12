using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using Sitio.Utility;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace Sitio.Models
{
    public class MensajeVisor
    {

        //Atributos
        private int id;
        private DateTime fechaGenerado;
        private string contenido;

        private Dispositivo provieneDispositivo;



        //Propiedades
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime FechaGenerado
        {
            get { return fechaGenerado; }
            set { fechaGenerado = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Contenido
        {
            get { return contenido; }
            set { contenido = value; }
        }

        public Dispositivo ProvieneDispositivo
        {
            get { return provieneDispositivo; }
            set { provieneDispositivo = value; }
        }



        //Constructores
        public MensajeVisor(int pId, DateTime pFechaGenerado, string pContenido, Dispositivo pProvieneDispositivo)
        {
            Id = pId;
            FechaGenerado = pFechaGenerado;
            Contenido = pContenido;
            ProvieneDispositivo = pProvieneDispositivo;
        }

        public MensajeVisor()
        {

        }


        //Validar Contenido de Objeto
        public void Validar()
        {
            if (this.Id < 0)
                throw new Exception("Error en el id de mensaje visor");

            if (this.FechaGenerado > DateTime.Now)
                throw new Exception("Error en fecha generado de mensaje visor");

            if ((this.Contenido.Trim().Length > 200) || (this.Contenido.Trim().Length < 10))
                throw new Exception("Error en el contenido de mensaje visor");

            if (this.ProvieneDispositivo == null)
                throw new Exception("Error en el dispositivo asignado de mensaje visor.");
        }

    }



    public class MensajeVisorAPI
    {


        public List<MensajeVisor> GetMensajeVisorXUsuarioUltimaH(Usuario usuLog)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, ConexionAPIRest.ConnexionAPI + "mensajesvisor/xUsuarioUltimaH"));

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

                    return JsonConvert.DeserializeObject<List<MensajeVisor>>(content);
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public List<MensajeVisor> GetMensajeVisorXUsuarioUltimaH(string token)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, ConexionAPIRest.ConnexionAPI + "mensajesvisor/xUsuarioUltimaH"));

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

                    return JsonConvert.DeserializeObject<List<MensajeVisor>>(content);
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }





    }


}
