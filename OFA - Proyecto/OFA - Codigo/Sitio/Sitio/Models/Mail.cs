using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Sitio.Utility;
using System.Net;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace Sitio.Models
{
    public class Mail
    {

        //Atributos
        private string correo;
        private string contraseña;
        private string hostServidor;
        private int puerto;



        //Propiedades
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Contraseña
        {
            get { return contraseña; }
            set { contraseña = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string HostServidor
        {
            get { return hostServidor; }
            set { hostServidor = value; }
        }

        public int Puerto
        {
            get { return puerto; }
            set { puerto = value; }
        }



        //constructores
        public Mail(string pCorreo, string pContraseña, string pHostServidor, int pPuerto)
        {
            Correo = pCorreo;
            Contraseña = pContraseña;
            HostServidor = pHostServidor;
            Puerto = pPuerto;
        }

        public Mail(string pCorreo)
        {
            Correo = pCorreo;
            Contraseña = "";
            HostServidor = "";
            Puerto = 0;
        }

        public Mail()
        {

        }

        //Para la parte de "Agregar Dispositivo"
        public override string ToString()
        {
            return (this.correo.ToString());
        }



        //Validar Contenido de Objeto
        public void Validar()
        {
            Regex exp = new Regex(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$");

            if (exp.IsMatch(this.Correo.Trim()) == false)
                throw new Exception("Error en el correo de mail. Formato incorrecto.");

            if ((this.Contraseña.Trim().Length > 30) || (this.Contraseña.Trim().Length < 8))
                throw new Exception("Error en la contraseña de mail. Debe tener entre 10 y 20 caracteres.");

            if ((this.HostServidor.Trim().Length > 50) || (this.HostServidor.Trim().Length < 4))
                throw new Exception("Error en el host servidor de mail. Debe tener entre 4 y 50 caracteres.");

            if ((this.Puerto > 65535) || (this.Puerto <= 0))
                throw new Exception("Error en el puerto de mail. Debe ser mayor a 0 y menor a 65535");
        }

    }



    public class MailsAPI
    {
 

        public void PutMail(Usuario usuLog, Mail unMail)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usuLog.Token);

                    client.BaseAddress = new Uri(ConexionAPIRest.ConnexionAPI + "mails");

                    //HTTP PUT
                    var putTask = client.PutAsJsonAsync<Mail>("mails/Correo=" + unMail.Correo, unMail);

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




    }


}
