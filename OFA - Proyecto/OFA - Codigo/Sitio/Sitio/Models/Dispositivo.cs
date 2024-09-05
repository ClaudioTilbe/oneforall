using Newtonsoft.Json;
using Sitio.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace Sitio.Models
{
    public class Dispositivo
    {

        //Atributos
        private string ip;
        private string nombre;
        private string tipo;
        private bool conectado;
        private bool accesible;
        private string sector;

        private string prioridad;
        private bool permanencia;
        private DateTime ultimaConexion;
        private DateTime ultimaNotificacion;

        private Sucursal ubSucursal;



        //Propiedades
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public bool Conectado
        {
            get { return conectado; }
            set { conectado = value; }
        }

        public bool Accesible
        {
            get { return accesible; }
            set { accesible = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Sector
        {
            get { return sector; }
            set { sector = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Prioridad
        {
            get { return prioridad; }
            set { prioridad = value; }
        }

        public bool Permanencia
        {
            get { return permanencia; }
            set { permanencia = value; }
        }

        public DateTime Ultimaconexion
        {
            get { return ultimaConexion; }
            set { ultimaConexion = value; }
        }

        public DateTime UltimaNotificacion
        {
            get { return ultimaNotificacion; }
            set { ultimaNotificacion = value; }
        }

        public Sucursal UbSucursal
        {
            get { return ubSucursal; }
            set { ubSucursal = value; }
        }



        //Constructores
        public Dispositivo(string pIP, string pNombre, string pTipo, bool pConectado, bool pAccesible,
            string pSector, string pPrioridad, bool pPermanencia, DateTime pUltimaConexion,
            DateTime pUltimaNotificacion, Sucursal pUbSucursal)
        {
            IP = pIP;
            Nombre = pNombre;
            Tipo = pTipo;
            Conectado = pConectado;
            Accesible = pAccesible;
            Sector = pSector;
            Prioridad = pPrioridad;
            Permanencia = pPermanencia;
            Ultimaconexion = pUltimaConexion;
            UltimaNotificacion = pUltimaNotificacion;

            UbSucursal = pUbSucursal;
        }

        public Dispositivo()
        {

        }


        public override string ToString()
        {
            return (this.ip.ToString());
        }



        //Validar Contenido de Objeto
        public void Validar()
        {
            Regex exp = new Regex(@"^(\d{1,3}\.){3}\d{1,3}$");

            if (exp.IsMatch(this.IP.Trim()) == false)
                throw new Exception("Error en la ip del dispositivo. Formato incorrecto.");

            if ((this.Nombre.Trim().Length > 50) || (this.Nombre.Trim().Length < 2))
                throw new Exception("Error en el nombre de dispositivo. Debe tener entre 2 y 50 caracteres.");

            if ((this.Tipo.Trim().Length > 30) || (this.Tipo.Trim().Length < 2))
                throw new Exception("Error en el tipo de dispositivo. Debe tener entre 2 y 30 caracteres.");

            if ((this.Sector.Trim().Length > 50) || (this.Sector.Trim().Length < 2))
                throw new Exception("Error en el sector de dispositivo. Debe tener entre 2 y 50 caracteres.");

            if ((this.Prioridad.Trim().ToLower() != "alta") && (this.Prioridad.Trim().ToLower() != "media")
                    && (this.Prioridad.Trim().ToLower() != "baja") && (this.Prioridad.Trim().ToLower() != "indefinida"))
                throw new Exception("Error en la prioridad de dispositivo.");

            if (this.Ultimaconexion > DateTime.Now)
                throw new Exception("Error en ultima conexion de dispositivo. Debe ser anterior a la actual.");

            if (this.UltimaNotificacion > DateTime.Now)
                throw new Exception("Error en ultima notificacion de dispositivo. Debe ser anterior a la actual.");

            if (this.UbSucursal == null)
                throw new Exception("Error en la sucursal ubicada de dispositivo.");
        }


        //Ordeno lista de dispositivos por IP
        public List<Dispositivo> OrdenoIPs(List<Dispositivo> listaDispositivos)
        {
            List<string> ips = listaDispositivos.Select(d => d.IP).ToList();
            List<string> ipsOrdenadas = ips.OrderBy(ip => ip, new IPComparer()).ToList(); //Comparer en clase Utility

            List<Dispositivo> dispositivosOrdenados = ipsOrdenadas
                                                      .Select(ip => listaDispositivos.First(d => d.IP == ip))
                                                      .ToList();

            return dispositivosOrdenados;
        }









    }



    public class DispositivosAPI
    {

        public List<Dispositivo> GetDispositivosXUsuario(Usuario usuLog)
        {
            try
            {
                List<Dispositivo> listaObtenida = new List<Dispositivo>();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, ConexionAPIRest.ConnexionAPI + "dispositivos/xUsuario"));

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

                    listaObtenida = JsonConvert.DeserializeObject<List<Dispositivo>>(content);

                    if (listaObtenida != null && listaObtenida.Count > 0)
                    {
                        listaObtenida = new Dispositivo().OrdenoIPs(listaObtenida);
                    }

                    return listaObtenida;
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public List<Dispositivo> GetDispositivosXUsuario(string token)
        {
            try
            {
                List<Dispositivo> listaObtenida = new List<Dispositivo>();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, ConexionAPIRest.ConnexionAPI + "dispositivos/xUsuario"));

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

                    listaObtenida = JsonConvert.DeserializeObject<List<Dispositivo>>(content);

                    if (listaObtenida != null && listaObtenida.Count > 0)
                    {
                        listaObtenida = new Dispositivo().OrdenoIPs(listaObtenida);
                    }

                    return listaObtenida;
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public List<Dispositivo> GetDispositivosXGrupo(Usuario usuLog, int codigo)
        {
            try
            {
                List<Dispositivo> listaDispositivos;
                HttpClient client = new HttpClient();

                string LogResponse = "";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                // Configuración de HttpClient
                client.BaseAddress = new Uri(ConexionAPIRest.ConnexionAPI + "dispositivos/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync("xGrupo", codigo).Result;

                if (response.IsSuccessStatusCode)
                {
                    LogResponse = response.Content.ReadAsStringAsync().Result;

                    listaDispositivos = JsonConvert.DeserializeObject<List<Dispositivo>>(LogResponse);
                }

                else if (!response.IsSuccessStatusCode)
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
                else
                {
                    return null;
                }

                if (listaDispositivos != null && listaDispositivos.Count > 0)
                {
                    listaDispositivos = new Dispositivo().OrdenoIPs(listaDispositivos);
                }

                return listaDispositivos;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }



        public List<Dispositivo> GetDispositivosXGrupo(string token, int codigo)
        {
            try
            {
                List<Dispositivo> listaDispositivos;
                HttpClient client = new HttpClient();

                string LogResponse = "";

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Configuración de HttpClient
                client.BaseAddress = new Uri(ConexionAPIRest.ConnexionAPI + "dispositivos/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync("xGrupo", codigo).Result;

                if (response.IsSuccessStatusCode)
                {
                    LogResponse = response.Content.ReadAsStringAsync().Result;

                    listaDispositivos = JsonConvert.DeserializeObject<List<Dispositivo>>(LogResponse);
                }

                else if (!response.IsSuccessStatusCode)
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
                else
                {
                    return null;
                }

                if (listaDispositivos != null && listaDispositivos.Count > 0)
                {
                    listaDispositivos = new Dispositivo().OrdenoIPs(listaDispositivos);
                }

                return listaDispositivos;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }




        public List<Tuple<string, int>> GetDispositivosCategorizados(Usuario usuLog)
        {
            try
            {
                List<Dispositivo> listaDispositivos = new List<Dispositivo>();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, ConexionAPIRest.ConnexionAPI + "dispositivos/xUsuario"));

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

                    listaDispositivos = JsonConvert.DeserializeObject<List<Dispositivo>>(content);


                    //Comienzo a Categorizarlos
                    List<Tuple<string, int>> listaResultados = new List<Tuple<string, int>>();

                    //Conectados ----------------------------------------------------------------------
                    int conectados = (from unDisp in listaDispositivos
                                      where unDisp.Conectado == true
                                      select unDisp).Count();

                    listaResultados.Add(new Tuple<string, int>("Conectados", conectados));

                    //Criticos ------------------------------------------------------------------------
                    int criticos = (from unDisp in listaDispositivos
                                    where unDisp.Conectado == false && unDisp.Permanencia == true
                                    select unDisp).Count();

                    listaResultados.Add(new Tuple<string, int>("Criticos", criticos));

                    //Alerta ---------------------------------------------------------------------------
                    int alerta = (from unDisp in listaDispositivos
                                  where unDisp.Conectado == false && unDisp.Permanencia == false
                                  select unDisp).Count();

                    listaResultados.Add(new Tuple<string, int>("Alerta", alerta));

                    //Desconocidos ---------------------------------------------------------------------
                    int desconocidos = (from unDisp in listaDispositivos
                                        where unDisp.Nombre == "Desconocido"
                                        select unDisp).Count();

                    listaResultados.Add(new Tuple<string, int>("Desconocidos", desconocidos));

                    //Todos ----------------------------------------------------------------------------
                    listaResultados.Add(new Tuple<string, int>("Todos", listaDispositivos.Count()));


                    return listaResultados;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public List<Tuple<string, int>> GetDispositivosCategorizados(string token)
        {
            try
            {
                List<Dispositivo> listaDispositivos = new List<Dispositivo>();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, ConexionAPIRest.ConnexionAPI + "dispositivos/xUsuario"));

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

                    listaDispositivos = JsonConvert.DeserializeObject<List<Dispositivo>>(content);


                    //Comienzo a Categorizarlos
                    List<Tuple<string, int>> listaResultados = new List<Tuple<string, int>>();

                    //Conectados ----------------------------------------------------------------------
                    int conectados = (from unDisp in listaDispositivos
                                      where unDisp.Conectado == true
                                      select unDisp).Count();

                    listaResultados.Add(new Tuple<string, int>("Conectados", conectados));

                    //Criticos ------------------------------------------------------------------------
                    int criticos = (from unDisp in listaDispositivos
                                    where unDisp.Conectado == false && unDisp.Permanencia == true
                                    select unDisp).Count();

                    listaResultados.Add(new Tuple<string, int>("Criticos", criticos));

                    //Alerta ---------------------------------------------------------------------------
                    int alerta = (from unDisp in listaDispositivos
                                  where unDisp.Conectado == false && unDisp.Permanencia == false
                                  select unDisp).Count();

                    listaResultados.Add(new Tuple<string, int>("Alerta", alerta));

                    //Desconocidos ---------------------------------------------------------------------
                    int desconocidos = (from unDisp in listaDispositivos
                                        where unDisp.Nombre == "Desconocido"
                                        select unDisp).Count();

                    listaResultados.Add(new Tuple<string, int>("Desconocidos", desconocidos));

                    //Todos ----------------------------------------------------------------------------
                    listaResultados.Add(new Tuple<string, int>("Todos", listaDispositivos.Count()));


                    return listaResultados;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }






        public void PostDispositivo(Usuario usuLog, Dispositivo unDisp)
        {
            try
            {
                using (var client = new HttpClient())
                {
                  
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    unDisp.UltimaNotificacion = DateTime.Now;
                    unDisp.Ultimaconexion = DateTime.Now;

                    client.BaseAddress = new Uri(ConexionAPIRest.ConnexionAPI + "dispositivos");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<Dispositivo>("dispositivos", unDisp);
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


        public void PutDispositivo(Usuario usuLog, Dispositivo unDisp)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    client.BaseAddress = new Uri(ConexionAPIRest.ConnexionAPI + "dispositivos");

                    //HTTP PUT
                    var putTask = client.PutAsJsonAsync<Dispositivo>("dispositivos/IP=" + unDisp.IP, unDisp);

                    putTask.Wait();

                    var response = putTask.Result;

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



        public void DeleteDispositivo(Usuario usuLog, Dispositivo unDisp)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(ConexionAPIRest.ConnexionAPI + "dispositivos/IP=" + unDisp.IP),
                        Content = new StringContent(JsonConvert.SerializeObject(unDisp), Encoding.UTF8, "application/json")
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
