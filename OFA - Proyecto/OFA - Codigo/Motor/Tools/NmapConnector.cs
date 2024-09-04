using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motor.Tools
{
    public class NmapConnector
    {

        public static string EjecutoEscaneoRapido(string targetIp)
        {
            try
            {
                string directorioNmap = "C:\\Program Files (x86)\\Nmap\\nmap.exe";
                string comandoNmap = "-sV ";

                ProcessStartInfo procStartInfo = new ProcessStartInfo(directorioNmap, comandoNmap + targetIp);

                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;

                procStartInfo.CreateNoWindow = true; //Define si se ve o no la ventana

                // Utiliza UTF-8 como la codificación para la salida estándar
                procStartInfo.StandardOutputEncoding = Encoding.UTF8;

                //procStartInfo.StandardOutputEncoding = Encoding.GetEncoding(850); //Esta me daba un error
                //Porque Encoding?
                //https://stackoverflow.com/questions/16803748/how-to-decode-cmd-output-correctly

                Process proc = new Process();

                proc.StartInfo = procStartInfo;

                proc.Start();


                string result = proc.StandardOutput.ReadToEnd();

                proc.WaitForExit();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error durante EjecutoEscaneoRapido: " + ex.Message);
            }
        }




    }
}
