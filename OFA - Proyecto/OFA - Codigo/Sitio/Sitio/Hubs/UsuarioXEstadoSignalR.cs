using Sitio.Models;
using System.Threading;

namespace Sitio.Hubs
{
    public class UsuarioXEstadoSignalR
    {
        public string ConnectionID { get; set; }

        public string token { get; set; }

        public bool SondeoOpen { get; set; }

        public bool PanelOpen { get; set; }

        public Timer TimerMonitoreo { get; set; } 


        public Timer TimerPanel { get; set; } 


    }
}
