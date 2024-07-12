using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Text;
using System;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Sitio.Utility;
using Sitio.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Sitio.Models
{
    //public abstract class Usuario //Quito "abstract" para poder iniciarlizar un usuario en las vistas
    public class Usuario
    {

        private string usuarioID;
        private string contraseña;
        private string confirmContra;
        private string token;
        private string tipo;

        private Mail mailAsignado;



        //Propiedades
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UsuarioID
        {
            get { return usuarioID; }
            set { usuarioID = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Contraseña
        {
            get { return contraseña; }
            set { contraseña = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ConfirmContra
        {
            get { return confirmContra; }
            set { confirmContra = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Token
        {
            get { return token; }
            set { token = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public Mail MailAsignado
        {
            get { return mailAsignado; }
            set { mailAsignado = value; }
        }


        //constructores
        public Usuario(string pUsuarioID, string pContraseña, string pConfirmContra, string pToken, string pTipo, Mail pMailAsignado)
        {
            UsuarioID = pUsuarioID;
            Contraseña = pContraseña;
            ConfirmContra = pConfirmContra;
            Token = pToken;
            Tipo = pTipo;
            MailAsignado = pMailAsignado;
        }

        public Usuario()
        {
            MailAsignado = new Mail();
        }




        //Validar Contenido de Objeto
        public void ValidarLogueo()
        {
            if (this.UsuarioID == null || (this.UsuarioID.Trim().Length > 50) || (this.UsuarioID.Trim().Length < 2))
                throw new Exception("Credenciales de usuario no validas.");

            if (this.Contraseña == null || (this.Contraseña.Trim().Length > 20) || (this.Contraseña.Trim().Length < 12))
                throw new Exception("Credenciales de usuario no validas.");
        }


    }




    public class UsuariosAPI
    {

        public Usuario Logueo(string usuarioID, string usuarioPass)
        {
            try
            {
                HttpClient client = new HttpClient();
                Usuario usuLog = new Usuario();
                string LogResponse = "";

                // Configuración de HttpClient
                client.BaseAddress = new Uri(ConexionAPIRest.ConnexionAPI + "usuarios/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Login en la API
                var usuarioLoginDTO = new UsuarioLoginDTO
                {
                    UsuarioID = usuarioID,
                    Contraseña = usuarioPass
                };

                HttpResponseMessage response = client.PostAsJsonAsync("Login", usuarioLoginDTO).Result;


                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Credenciales de Usuario no validas.");
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

                LogResponse = response.Content.ReadAsStringAsync().Result;

                if (LogResponse.Contains("\"Tipo\":\"Operador\""))
                {
                    usuLog = JsonConvert.DeserializeObject<Operador>(LogResponse);
                    usuLog.Tipo = "Operador";
                }

                else if (LogResponse.Contains("\"Tipo\":\"Administrador\""))
                {
                    usuLog = JsonConvert.DeserializeObject<Administrador>(LogResponse);
                    usuLog.Tipo = "Administrador";
                }

                JsonDocument doc = JsonDocument.Parse(LogResponse);
                usuLog.Token = doc.RootElement.GetProperty("Token").GetString();


                return usuLog;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public Usuario GetUsuario(Administrador usuLog, string usuId)
        {
            try
            {

                Usuario unUsuario = new Usuario();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, ConexionAPIRest.ConnexionAPI + "usuarios/UsuarioID=" + usuId));

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

                    if (content.Contains("\"Tipo\":\"Operador\""))
                    {
                        unUsuario = JsonConvert.DeserializeObject<Operador>(content);
                        unUsuario.Tipo = "Operador";
                    }

                    else if (content.Contains("\"Tipo\":\"Administrador\""))
                    {
                        unUsuario = JsonConvert.DeserializeObject<Administrador>(content);
                        unUsuario.Tipo = "Administrador";
                    }

                    return unUsuario;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public List<Administrador> GetAdministradores(Administrador usuLog)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, ConexionAPIRest.ConnexionAPI + "usuarios/administradores"));

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

                    return JsonConvert.DeserializeObject<List<Administrador>>(content);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public List<Operador> GetOperadores(Administrador usuLog)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get, ConexionAPIRest.ConnexionAPI + "usuarios/operadores"));


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

                    return JsonConvert.DeserializeObject<List<Operador>>(content);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public void PostUsuario(Administrador usuLog, Usuario usuAlta)
        {
            try
            {
                //Seteo datos que van por defecto
                usuAlta.MailAsignado.Puerto = 25;
                usuAlta.MailAsignado.Contraseña = "DefaultPassword";
                usuAlta.MailAsignado.HostServidor = "outlook.live.com";


                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    UsuarioAltaDTO dto = new UsuarioAltaDTO();

                    if (usuAlta is Administrador)
                    {
                        dto.Tipo = "Administrador";
                        dto.UsuarioID = usuAlta.UsuarioID;
                        dto.Contraseña = usuAlta.Contraseña;
                        dto.MailAsignado = usuAlta.MailAsignado;
                        dto.Nombre = ((Administrador)usuAlta).Nombre;
                        dto.NumeroFuncionario = ((Administrador)usuAlta).NumeroFuncionario;
                        dto.Cargo = ((Administrador)usuAlta).Cargo;
                    }

                    else if (usuAlta is Operador)
                    {
                        dto.Tipo = "Operador";
                        dto.UsuarioID = usuAlta.UsuarioID;
                        dto.Contraseña = usuAlta.Contraseña;
                        dto.MailAsignado = usuAlta.MailAsignado;
                        dto.NumeroFuncionarioSupervisor = ((Operador)usuAlta).NumeroFuncionarioSupervisor;
                        dto.NombreSupervisor = ((Operador)usuAlta).NombreSupervisor;
                        dto.SucursalesAsignadas = ((Operador)usuAlta).SucursalesAsignadas;
                    }

                    client.BaseAddress = new Uri(ConexionAPIRest.ConnexionAPI + "usuarios");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<UsuarioAltaDTO>("usuarios", dto);
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



        public void PutUsuario(Administrador usuLog, Usuario usuModificar)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    UsuarioModificarDTO dto = new UsuarioModificarDTO();

                    if (usuModificar is Administrador)
                    {
                        dto.Tipo = "Administrador";
                        dto.UsuarioID = usuModificar.UsuarioID;
                        dto.Contraseña = usuModificar.Contraseña;
                        dto.MailAsignado = usuModificar.MailAsignado;
                        dto.Nombre = ((Administrador)usuModificar).Nombre;
                        dto.NumeroFuncionario = ((Administrador)usuModificar).NumeroFuncionario;
                        dto.Cargo = ((Administrador)usuModificar).Cargo;
                    }

                    else if (usuModificar is Operador)
                    {
                        dto.Tipo = "Operador";
                        dto.UsuarioID = usuModificar.UsuarioID;
                        dto.Contraseña = usuModificar.Contraseña;
                        dto.MailAsignado = usuModificar.MailAsignado;
                        dto.NumeroFuncionarioSupervisor = ((Operador)usuModificar).NumeroFuncionarioSupervisor;
                        dto.NombreSupervisor = ((Operador)usuModificar).NombreSupervisor;
                        dto.SucursalesAsignadas = ((Operador)usuModificar).SucursalesAsignadas;
                    }

                    client.BaseAddress = new Uri(ConexionAPIRest.ConnexionAPI + "usuarios");

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<UsuarioModificarDTO>("usuarios", dto);
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



        public void DeleteUsuario(Administrador usuLog, Usuario unUsuario)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    UsuarioEliminarDTO dto = new UsuarioEliminarDTO();

                    if (unUsuario is Administrador)
                    {
                        dto.Tipo = "Administrador";
                        dto.UsuarioID = unUsuario.UsuarioID;
                        dto.Contraseña = unUsuario.Contraseña;
                        dto.MailAsignado = unUsuario.MailAsignado;
                        dto.Nombre = ((Administrador)unUsuario).Nombre;
                        dto.NumeroFuncionario = ((Administrador)unUsuario).NumeroFuncionario;
                        dto.Cargo = ((Administrador)unUsuario).Cargo;
                    }

                    else if (unUsuario is Operador)
                    {
                        dto.Tipo = "Operador";
                        dto.UsuarioID = unUsuario.UsuarioID;
                        dto.Contraseña = unUsuario.Contraseña;
                        dto.MailAsignado = unUsuario.MailAsignado;
                        dto.NumeroFuncionarioSupervisor = ((Operador)unUsuario).NumeroFuncionarioSupervisor;
                        dto.NombreSupervisor = ((Operador)unUsuario).NombreSupervisor;
                        dto.SucursalesAsignadas = ((Operador)unUsuario).SucursalesAsignadas;
                    }

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(ConexionAPIRest.ConnexionAPI + "usuarios"),
                        Content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json")
                    };

                    var response = client.Send(request);

                    //Agregado
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




    }

}
