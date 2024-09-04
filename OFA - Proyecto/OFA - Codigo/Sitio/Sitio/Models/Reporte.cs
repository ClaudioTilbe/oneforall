using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System;
using Newtonsoft.Json;
using Sitio.Utility;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace Sitio.Models
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
            set { codigo = value; }
        }

        public Mail MailOrigen
        {
            get { return mailOrigen; }
            set { mailOrigen = value; }
        }

        public Dispositivo DispositivoAsociado
        {
            get { return dispositivoAsociado; }
            set { dispositivoAsociado = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Asunto
        {
            get { return asunto; }
            set { asunto = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Destino
        {
            get { return destino; }
            set { destino = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Mensaje
        {
            get { return mensaje; }
            set { mensaje = value; }
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



        //validar Contenido de Objeto
        public void Validar()
        {
            Regex exp = new Regex(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$");

            if (this.Codigo < 0)
                throw new Exception("Error en el codigo de reporte. Debe ser mayor a 0.");

            if (this.MailOrigen == null)
                throw new Exception("Error en el mail de origen de reporte.");

            if (this.DispositivoAsociado == null)
                throw new Exception("Error en el dispositivo de reporte.");

            if ((this.Asunto.Trim().Length > 50) || (this.Asunto.Trim().Length < 10))
                throw new Exception("Error en el asunto de reporte. Debe tener entre 10 y 50 caracteres.");

            if (exp.IsMatch(this.Destino.Trim()) == false)
                throw new Exception("Error en el correo de reporte. Formato incorrecto.");

            if ((this.Mensaje.Trim().Length > 500) || (this.Mensaje.Trim().Length < 10))
                throw new Exception("Error en el mensaje de reporte. Debe tener entre 10 y 500 caracteres.");
        }

    }



    public class ReportesAPI
    {

        public List<Reporte> GetReportesXCorreo(Usuario usuLog, Mail unMail)
        {
            try
            {
                List<Reporte> listaReportes;
                HttpClient client = new HttpClient();

                string LogResponse = "";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                // Configuración de HttpClient
                client.BaseAddress = new Uri(ConexionAPIRest.ConnexionAPI + "reportes/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync("xCorreo", unMail).Result;

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

                LogResponse = response.Content.ReadAsStringAsync().Result;

                listaReportes = JsonConvert.DeserializeObject<List<Reporte>>(LogResponse);

                return listaReportes;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public void PostReporte(Usuario usuLog, Reporte unReporte)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    client.BaseAddress = new Uri(ConexionAPIRest.ConnexionAPI + "reportes");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<Reporte>("reportes", unReporte);
                    postTask.Wait();

                    var response = postTask.Result;

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
                }

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }



        public void PutReporte(Usuario usuLog, Reporte unReporte)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    client.BaseAddress = new Uri(ConexionAPIRest.ConnexionAPI + "reportes");

                    //HTTP POST
                    var postTask = client.PutAsJsonAsync<Reporte>("reportes", unReporte);
                    postTask.Wait();

                    var response = postTask.Result;

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
                }

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public void DeleteReporte(Usuario usuLog, Reporte unReporte)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(ConexionAPIRest.ConnexionAPI + "reportes"),
                        Content = new StringContent(JsonConvert.SerializeObject(unReporte), Encoding.UTF8, "application/json")
                    };

                    var response = client.Send(request);

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
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }





    }//FIN API

}
