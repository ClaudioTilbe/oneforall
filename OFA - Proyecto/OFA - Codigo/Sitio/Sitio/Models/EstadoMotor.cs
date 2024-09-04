using System.Net.Http.Headers;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using Sitio.Utility;
using System.Net.Http.Json;
using System.Net;

namespace Sitio.Models
{
    public class EstadoMotor
    {

        //Atributos
        private int idEstado;
        private bool activo;
        private DateTime ultimaModificacion;

        private string usuarioRegistra;



        //Propiedades
        public int IDEstado
        {
            get { return idEstado; }
            set { idEstado = value; }
        }

        public bool Activo
        {
            get { return activo; }
            set { activo = value; }
        }

        public DateTime UltimaModificacion
        {
            get { return ultimaModificacion; }
            set { ultimaModificacion = value; }
        }

        public string UsuarioRegistra
        {
            get { return usuarioRegistra; }
            set { usuarioRegistra = value; }
        }



        //Constructores
        public EstadoMotor(int pIDEstado, bool pActivo, DateTime pUltimaModificacion, string pUsuarioRegistra)
        {
            IDEstado = pIDEstado;
            Activo = pActivo;
            UltimaModificacion = pUltimaModificacion;

            UsuarioRegistra = pUsuarioRegistra;
        }


        public EstadoMotor(int pIDEstado, bool pActivo, DateTime pUltimaModificacion)
        {
            IDEstado = pIDEstado;
            Activo = pActivo;
            UltimaModificacion = pUltimaModificacion;
        }

        public EstadoMotor(bool pActivo)
        {
            Activo = pActivo;
        }


        public EstadoMotor()
        {

        }



        //Validar Contenido de Objeto
        public void Validar()
        {
            if (this.IDEstado < 0)
                throw new Exception("Error en el id de estado motor. Debe ser mayor a 0.");

            if (this.UltimaModificacion > DateTime.Now)
                throw new Exception("Error en ultima modificacion de estado motor. Debe ser anterior a la actual.");

            if (this.usuarioRegistra == null || (this.usuarioRegistra.Trim().Length > 50) || (this.usuarioRegistra.Trim().Length < 2))
                throw new Exception("Error en el usuario registrado de estado motor.");
        }


    }



    public class EstadoMotorAPI
    {
        public EstadoMotor StartAPI(Administrador usuLog)
        {
            try
            {

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, ConexionAPIRest.ConnexionAPI + "EstadoMotor/Start"));

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

                    return JsonConvert.DeserializeObject<EstadoMotor>(content);
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public void StopAPI(Administrador usuLog)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, ConexionAPIRest.ConnexionAPI + "EstadoMotor/Stop"));

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
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public EstadoMotor BuscarEstadoMotorAPI(Administrador usuLog)
        {
            try
            {

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, ConexionAPIRest.ConnexionAPI + "EstadoMotor/BuscarEstadoMotor"));

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

                    return JsonConvert.DeserializeObject<EstadoMotor>(content);
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }//Fin clase API



}
