using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System;
using Newtonsoft.Json;
using Sitio.Utility;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace Sitio.Models
{
    public class Subred
    {

        //Atributos
        private string rango;



        //Propiedades
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Rango
        {
            get { return rango; }
            set { rango = value; }
        }



        //Constructores
        public Subred(string pRango)
        {
            Rango = pRango;
        }

        public Subred()
        {

        }



        //validar Contenido de Objeto
        public void Validar()
        {
            Regex exp = new Regex(@"^(\d{1,3}\.){2}\d{1,3}$"); // Expresión regular para validar xxx.xxx.xxx

            if (!exp.IsMatch(this.Rango.Trim()))
                throw new Exception("Error en el rango de la subred. Formato incorrecto.");
        }


    }



    public class SubredesAPI
    {


        public List<Subred> GetSubredes(Administrador usuLog)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, ConexionAPIRest.ConnexionAPI + "subredes"));

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

                    return JsonConvert.DeserializeObject<List<Subred>>(content);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




    }


}
