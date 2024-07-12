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
    public class Sucursal
    {

        //Atributos
        private int nroSucursal;
        private string tipo;
        private string departamento;
        private string calle;
        private int numeroLocal;

        private List<Subred> listaSubred;



        //Propiedades
        public int NroSucursal
        {
            get { return nroSucursal; }
            set { nroSucursal = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Departamento
        {
            get { return departamento; }
            set { departamento = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Calle
        {
            get { return calle; }
            set { calle = value; }
        }
        public int NumeroLocal
        {
            get { return numeroLocal; }
            set { numeroLocal = value; }
        }

        public List<Subred> ListaSubred
        {
            get { return listaSubred; }
            set { listaSubred = value; }
        }



        //Constructores
        public Sucursal(int pNroSucursal, string pTipo, string pDepartamento, string pCalle, int pNumeroLocal, List<Subred> pListaSubred)
        {
            NroSucursal = pNroSucursal;
            Tipo = pTipo;
            Departamento = pDepartamento;
            Calle = pCalle;
            NumeroLocal = pNumeroLocal;
            ListaSubred = pListaSubred;
        }

        public Sucursal(int pNroSucursal, string pDepartamento, string pCalle)
        {
            NroSucursal = pNroSucursal;
            Departamento = pDepartamento;
            Calle = pCalle;
        }

        public Sucursal()
        {

        }


        //Para la parte de "Agregar Dispositivo"
        public override string ToString()
        {
            return (this.nroSucursal.ToString());
        }



        //Validar Contenido de Objeto
        public void Validar()
        {
            if ((this.NroSucursal > 9999) || (this.NroSucursal < 0))
                throw new Exception("Error en el numero de sucursal. Puede estar entre 0 y 10000.");

            if ((this.Tipo.Trim().Length > 50) || (this.Tipo.Trim().Length < 4))
                throw new Exception("Error en el tipo de sucursal. Debe tener entre 4 y 50 caracteres.");

            if ((this.Departamento.Trim().Length > 50) || (this.Departamento.Trim().Length < 2))
                throw new Exception("Error en el departamento de sucursal. Debe tener entre 2 y 50 caracteres.");

            if ((this.Calle.Trim().Length > 50) || (this.Calle.Trim().Length < 4))
                throw new Exception("Error en la calle de sucursal. Debe tener entre 4 y 50 caracteres.");

            if ((this.NumeroLocal > 99999999) || (this.NumeroLocal < 0))
                throw new Exception("Error en el numero de local de sucursal. Puede estar entre 0 y 100000000.");
        }

    }



    public class SucursalesAPI
    {

        public Sucursal GetSucursal(Usuario usuLog, int nroSucursal)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, ConexionAPIRest.ConnexionAPI + "sucursales/NumeroSucursal=" + nroSucursal.ToString()));

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

                    return JsonConvert.DeserializeObject<Sucursal>(content);
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public List<Sucursal> GetSucursalesXUsuario(Usuario usuLog, Usuario unUsuario)
        {
            try
            {
                List<Sucursal> listaSucursales;
                HttpClient client = new HttpClient();

                string LogResponse = "";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                // Configuración de HttpClient
                client.BaseAddress = new Uri(ConexionAPIRest.ConnexionAPI + "sucursales/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync("xUsuario", unUsuario.UsuarioID).Result;

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

                listaSucursales = JsonConvert.DeserializeObject<List<Sucursal>>(LogResponse);

                return listaSucursales;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public void PostSucursal(Administrador usuLog, Sucursal unaSucursal)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    client.BaseAddress = new Uri(ConexionAPIRest.ConnexionAPI + "sucursales");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<Sucursal>("sucursales", unaSucursal);
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



        public void PutSucursal(Administrador usuLog, Sucursal unaSucursal)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    client.BaseAddress = new Uri(ConexionAPIRest.ConnexionAPI + "sucursales");

                    //HTTP POST
                    var postTask = client.PutAsJsonAsync<Sucursal>("sucursales", unaSucursal);
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



        public void DeleteSucursal(Administrador usuLog, Sucursal unaSucursal)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(ConexionAPIRest.ConnexionAPI + "sucursales"),
                        Content = new StringContent(JsonConvert.SerializeObject(unaSucursal), Encoding.UTF8, "application/json")
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
