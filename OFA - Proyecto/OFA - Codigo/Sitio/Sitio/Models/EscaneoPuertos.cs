using Newtonsoft.Json;
using Sitio.Utility;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace Sitio.Models
{
    public class EscaneoPuertos
    {

        //Atributos
        private int idEscaneo;

        private Dispositivo dispositivoObjetivo;

        private string razon;
        private string estado;
        private string prioridad;
        private string cadenaSalida;

        private DateTime fechaGenerado;
        private DateTime? fechaFinalizado;

        private Administrador administradorReg;



        //Propiedades
        public int IDEscaneo
        {
            get { return idEscaneo; }
            set { idEscaneo = value; }
        }

        public Dispositivo DispositivoObjetivo
        {
            get { return dispositivoObjetivo; }
            set { dispositivoObjetivo = value; }
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

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CadenaSalida
        {
            get { return cadenaSalida; }
            set { cadenaSalida = value; }
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

        public Administrador AdministradorReg
        {
            get { return administradorReg; }
            set { administradorReg = value; }
        }



        //Constructores
        public EscaneoPuertos(int pIDEscaneo, Dispositivo pDispositivoObjetivo, string pRazon, string pEstado, string pPrioridad,
            string pCadenaSalida, DateTime pFechaGenerado, DateTime? pFechaFinalizado,
            Administrador pAdministradorReg)
        {
            IDEscaneo = pIDEscaneo;
            DispositivoObjetivo = pDispositivoObjetivo;
            Razon = pRazon;
            Estado = pEstado;
            Prioridad = pPrioridad;
            CadenaSalida = pCadenaSalida;
            FechaGenerado = pFechaGenerado;
            FechaFinalizado = pFechaFinalizado;

            AdministradorReg = pAdministradorReg;
        }

        public EscaneoPuertos()
        {

        }



        //validar Contenido de Objeto
        public void Validar()
        {
            if (this.IDEscaneo < 0)
                throw new Exception("Error en el id de escaneo de puertos. Debe ser mayor a 0");

            if (this.DispositivoObjetivo == null)
                throw new Exception("Error en el dispositivo objetivo de escaneo de puertos.");

            if ((this.Razon.Trim().Length > 200) || (this.Razon.Trim().Length < 5))
                throw new Exception("Error en la razon de escaneo de puerto. Debe tener entre 5 y 200 caracteres.");

            if ((this.Estado.Trim().ToLower() != "pendiente") && (this.Estado.Trim().ToLower() != "ejecutandose")
                   && (this.Estado.Trim().ToLower() != "cancelado") && (this.Estado.Trim().ToLower() != "finalizado"))
                throw new Exception("Error en el estado de escaneo de puertos.");

            if ((this.Prioridad.Trim().ToLower() != "alta") && (this.Prioridad.Trim().ToLower() != "media")
                     && (this.Prioridad.Trim().ToLower() != "baja"))
                throw new Exception("Error en la prioridad de escaneo de puertos.");

            if (this.CadenaSalida.Trim().Length > 3000)
                throw new Exception("Error en la razon de escaneo de puerto. Debe tener menos de 3000 caracteres.");

            if (this.FechaGenerado > DateTime.Now)
                throw new Exception("Error en fecha generado de escaneo de puertos. Debe ser anterior a la actual.");

            if (this.FechaFinalizado != null)
            {
                if (this.FechaFinalizado > DateTime.Now)
                    throw new Exception("Error en fecha de finalizado de escaneo de puertos. Debe ser anterior a la actual.");
            }

            if (this.AdministradorReg == null)
                throw new Exception("Error en el administrados registrado de escaneo de puertos.");
        }



    }



    public class EscaneoPuertosAPI
    {

        public List<EscaneoPuertos> GetEscaneoPuertosXTodos(Administrador usuLog)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, ConexionAPIRest.ConnexionAPI + "escaneoPuertos/xTodos"));

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

                    return JsonConvert.DeserializeObject<List<EscaneoPuertos>>(content);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void PostEscaneoPuertos(Administrador usuLog, EscaneoPuertos unEscaneo)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    unEscaneo.IDEscaneo = 100;
                    unEscaneo.FechaFinalizado = DateTime.Now;

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    client.BaseAddress = new Uri(ConexionAPIRest.ConnexionAPI + "EscaneoPuertos");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<EscaneoPuertos>("EscaneoPuertos", unEscaneo);
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



        public void DeleteEscaneoPuertos(Administrador usuLog, EscaneoPuertos unEscaneo)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(ConexionAPIRest.ConnexionAPI + "EscaneoPuertos"),
                        Content = new StringContent(JsonConvert.SerializeObject(unEscaneo), Encoding.UTF8, "application/json")
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
