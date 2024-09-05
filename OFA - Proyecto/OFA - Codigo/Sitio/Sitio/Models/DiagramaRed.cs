using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Sitio.Utility;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace Sitio.Models
{
    public class DiagramaRed
    {

        //Atributos
        private Sucursal _sucursal;

        private string nombre;
        private DateTime fechaSubida;
        private byte[] diagramaImagen;



        //Propiedades
        public Sucursal _Sucursal
        {
            get { return _sucursal; }
            set { _sucursal = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public DateTime FechaSubida
        {
            get { return fechaSubida; }
            set { fechaSubida = value; }
        }

        public byte[] DiagramaImagen
        {
            get { return diagramaImagen; }
            set { diagramaImagen = value; }
        }


        public string DiagramaImagenBase64 => DiagramaImagen != null ? Convert.ToBase64String(DiagramaImagen) : null;



        //Constructores
        public DiagramaRed(Sucursal pSucursal, string pNombre, DateTime pFechaSubida, byte[] pDiagramaImagen)
        {
            _Sucursal = pSucursal;

            Nombre = pNombre;
            FechaSubida = pFechaSubida;
            DiagramaImagen = pDiagramaImagen;
        }


        public DiagramaRed()
        {

        }




        //validar Contenido de Objeto
        public void Validar()
        {
            const int maximoTamanoImagen = 10 * 1024 * 1024; // 10 MB en bytes

            if (this._Sucursal == null)
                throw new Exception("Error en la subred de analisis de red.");

            if ((this.Nombre.Trim().Length > 50) || (this.Nombre.Trim().Length < 4))
                throw new Exception("Error en el nombre de diagrama de red. Debe tener entre 4 y 50 caracteres.");

            if (this.FechaSubida > DateTime.Now)
                throw new Exception("Error en fecha de subida de diagrama de red. Debe ser anterior a la actual.");

            if (this.DiagramaImagen == null || this.DiagramaImagen.Length <= 0)
                throw new Exception("Error en el nombre de diagrama de red. El campo esta vacio.");

            if (this.DiagramaImagen.Length > maximoTamanoImagen)
                throw new Exception("Error en el tamaño del diagrama de red. El tamaño no puede ser superior a 10 MB.");


        }



    }


    public class DiagramaRedAPI
    {


        public List<DiagramaRed> GetDiagramasRed(Usuario usuLog)
        {
            try
            {

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, ConexionAPIRest.ConnexionAPI + "diagramasRed/"));

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

                    return JsonConvert.DeserializeObject<List<DiagramaRed>>(content);
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public void PostDiagramaRed(Usuario usuLog, DiagramaRed unDiagrama)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    client.BaseAddress = new Uri(ConexionAPIRest.ConnexionAPI + "diagramasRed");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<DiagramaRed>("diagramasRed", unDiagrama);
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



        public void DeleteDiagramaRed(Usuario usuLog, DiagramaRed unDiagrama)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(ConexionAPIRest.ConnexionAPI + "diagramasRed/NroSucursal=" + unDiagrama._Sucursal.NroSucursal),
                        Content = new StringContent(JsonConvert.SerializeObject(unDiagrama), Encoding.UTF8, "application/json")
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


    }// Fin Metodos API


}
