using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Motor.Tools
{
    public class MailSend
    {

        public void EnviarMail(Mail unMail, Reporte unReporte)
        {
            Send(unMail.Correo, unMail.Contraseña, unReporte.Destino, unReporte.Asunto,
                unReporte.Mensaje, unMail.HostServidor, unMail.Puerto);
        }



        internal void Send(string CorreoSalida, string contraseña, string CorreoDestino,
            string asunto, string mensaje, string host, int puerto)
        {
            try
            {
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(CorreoSalida, "One for all", Encoding.UTF8);//Correo de salida
                correo.To.Add(CorreoDestino); //Correo destino?
                correo.Subject = asunto; //Asunto
                correo.Body = mensaje; //Mensaje del correo

                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;

                smtp.Host = host; //Host del servidor de correo "outlook.live.com"
                smtp.Port = puerto; //Puerto de salida
                smtp.Credentials = new NetworkCredential(CorreoSalida, contraseña);//Cuenta de correo
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.EnableSsl = true;//True si el servidor de correo permite ssl

                smtp.Send(correo);
            }
            catch (Exception ex) 
            {
                throw new Exception("Error durante MailSend: " + ex.Message);
            }
        }



    }
}
