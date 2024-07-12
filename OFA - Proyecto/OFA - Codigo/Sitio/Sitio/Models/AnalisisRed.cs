using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Sitio.Utility;
using System.Net;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace Sitio.Models
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
            set { idAnalisis = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Razon
        {
            get { return razon; }
            set { razon = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Prioridad
        {
            get { return prioridad; }
            set { prioridad = value; }
        }

        public int NuevosDispositivos
        {
            get { return nuevosDispositivos; }
            set { nuevosDispositivos = value; }
        }

        public DateTime FechaGenerado
        {
            get { return fechaGenerado; }
            set { fechaGenerado = value; }
        }

        public DateTime? FechaFinalizado
        {
            get { return fechaFinalizado; }
            set { fechaFinalizado = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string SubredAnalizada
        {
            get { return subredAnalizada; }
            set { subredAnalizada = value; }
        }

        public Administrador AdministradorReg
        {
            get { return administradorReg; }
            set { administradorReg = value; }
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



        //validar Contenido de Objeto
        public void Validar()
        {
            Regex exp = new Regex(@"^(\d{1,3}\.){2}\d{1,3}$"); // Expresión regular para validar xxx.xxx.xxx

            if (this.IDAnalisis < 0)
                throw new Exception("Error en el id de analisis de red. Debe ser mayor a 0.");

            if ((this.Razon.Trim().Length > 200) || (this.Razon.Trim().Length < 5))
                throw new Exception("Error en la razon de analisis de red. Debe tener entre 5 y 200 caracteres.");

            if ((this.Estado.Trim().ToLower() != "pendiente") && (this.Estado.Trim().ToLower() != "ejecutandose")
                  && (this.Estado.Trim().ToLower() != "cancelado") && (this.Estado.Trim().ToLower() != "finalizado"))
                throw new Exception("Error en el estado de analisis de red.");

            if ((this.Prioridad.Trim().ToLower() != "alta") && (this.Prioridad.Trim().ToLower() != "media")
                    && (this.Prioridad.Trim().ToLower() != "baja"))
                throw new Exception("Error en la prioridad de analisis de red.");

            if (this.NuevosDispositivos < 0)
                throw new Exception("Error en los nuevos dispositivos de analisis de red. Debe ser mayor a 0.");

            if (this.FechaGenerado > DateTime.Now)
                throw new Exception("Error en fecha de generado de analisis de red. Debe ser anterior a la actual.");

            if (this.FechaFinalizado != null)
            {
                if (this.FechaFinalizado > DateTime.Now)
                    throw new Exception("Error en fecha de finalizado de analisis de red. Debe ser anterior a la actual.");
            }

            if (!exp.IsMatch(this.SubredAnalizada.Trim()))
            {
                throw new Exception("Error en la subred de analisis de red. Formato incorrecto.");
            }

            if (this.AdministradorReg == null)
                throw new Exception("Error en el administrados registrado de analisis de red.");

        }



    }




    public class AnalisisRedAPI
    {

        public List<AnalisisRed> GetAnalisisRedXTodos(Administrador usuLog)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, ConexionAPIRest.ConnexionAPI + "analisisred/xTodos"));

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

                    return JsonConvert.DeserializeObject<List<AnalisisRed>>(content);
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }




        public void PostAnalisisRed(Administrador usuLog, AnalisisRed unAnalisisRed)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    client.BaseAddress = new Uri(ConexionAPIRest.ConnexionAPI + "AnalisisRed");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<AnalisisRed>("AnalisisRed", unAnalisisRed);
                    postTask.Wait();

                    var result = postTask.Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        if (result.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new Exception($"La petición a la API falló: El usuario no esta autorizado para esta accion");
                        }
                        else
                        {
                            //Visualizar Excepcion en servidor
                            var errorResponse = result.Content.ReadAsStringAsync().Result;
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



        public void DeleteAnalisisRed(Administrador usuLog, AnalisisRed unAnalisisRed)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(ConexionAPIRest.ConnexionAPI + "AnalisisRed"),
                        Content = new StringContent(JsonConvert.SerializeObject(unAnalisisRed), Encoding.UTF8, "application/json")
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




    }//Fin API


}
