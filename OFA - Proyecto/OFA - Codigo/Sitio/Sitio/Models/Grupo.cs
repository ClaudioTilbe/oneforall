using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System;
using Newtonsoft.Json;
using Sitio.Utility;
using System.Net.Http.Json;
using System.ComponentModel.DataAnnotations;

namespace Sitio.Models
{
    public class Grupo
    {

        //Atributos
        private int codigo;
        private string nombreGrupo;
        private string descripcion;

        private Usuario usuario;
        private List<Dispositivo> dispositivos;



        //Propiedades
        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string NombreGrupo
        {
            get { return nombreGrupo; }
            set { nombreGrupo = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public Usuario _Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public List<Dispositivo> Dispositivos
        {
            get { return dispositivos; }
            set { dispositivos = value; }
        }



        //Constructores
        public Grupo(int pcodigo, string pNombreGrupo, string pDescripcion, Usuario pUsuario, List<Dispositivo> pDispositivos)
        {
            Codigo = pcodigo;
            NombreGrupo = pNombreGrupo;
            Descripcion = pDescripcion;

            _Usuario = pUsuario;
            Dispositivos = pDispositivos;
        }

        public Grupo()
        {

        }

        //Para que funcione SelectList (IndexMonitoreo)
        public override string ToString()
        {
            return (this.Codigo.ToString() + " - " + this.NombreGrupo);
        }



        //Validar Contenido de Objeto
        public void Validar()
        {
            if (this.Codigo < 0)
                throw new Exception("Error en el codigo de grupo. Debe ser mayor a 0.");

            if ((this.NombreGrupo.Trim().Length > 30) || (this.NombreGrupo.Trim().Length < 2))
                throw new Exception("Error en el nombre de grupo. Debe tener entre 2 y 30 caracteres.");

            if ((this.Descripcion.Trim().Length > 100) || (this.Descripcion.Trim().Length < 10))
                throw new Exception("Error en la descripcion de grupo. Debe tener entre 10 y 100 caracteres.");


        }

    }


    public class GruposAPI
    {

        public List<Grupo> GetGruposXUsuario(Usuario usuLog)
        {
            try
            {

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, ConexionAPIRest.ConnexionAPI + "grupos/xUsuario"));


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

                    return JsonConvert.DeserializeObject<List<Grupo>>(content);

                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public void PostGrupo(Usuario usuLog, Grupo unGrupo)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    unGrupo._Usuario = null;

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    client.BaseAddress = new Uri(ConexionAPIRest.ConnexionAPI + "grupos");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<Grupo>("grupos", unGrupo);
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



        public void PutGrupo(Usuario usuLog, Grupo unGrupo)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    client.BaseAddress = new Uri(ConexionAPIRest.ConnexionAPI + "grupos");

                    //HTTP POST
                    var postTask = client.PutAsJsonAsync<Grupo>("grupos", unGrupo);
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



        public void DeleteGrupo(Usuario usuLog, Grupo unGrupo)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(ConexionAPIRest.ConnexionAPI + "grupos"),
                        Content = new StringContent(JsonConvert.SerializeObject(unGrupo), Encoding.UTF8, "application/json")
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





    } // Fin de Clase


}
