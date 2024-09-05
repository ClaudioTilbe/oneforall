using APIRest.Security;
using EntidadesCompartidas;
using Logica;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace APIRest.Controllers
{
    [ApiController]
    [Route("api/mensajesvisor")]
    public class MensajeVisorController : ControllerBase
    {

        //Generacion y uso de Token (Variables y Constructor)
        private IConfiguration _config;
        private TokenService _tokenService;

        private readonly IMemoryCache _cache;

        public MensajeVisorController(IConfiguration config, TokenService tokenService, IMemoryCache memoryCache)
        {
            _config = config;
            _tokenService = tokenService;
            _cache = memoryCache;
        }



        [HttpGet("xUsuarioUltimaH")]
        public IActionResult GetMensajesVisorXUsuarioUltimaH()
        {
            Usuario usuarioLog;
            List<MensajeVisor> listaMensajesXUsuario = new List<MensajeVisor>();

            try
            {
                // Obtener el token JWT del encabezado de autorización
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                bool validacion = _tokenService.ValidateToken(token);

                if (!validacion)
                {
                    return Unauthorized();
                }

                usuarioLog = _cache.Get("Usuario-" + token) as Usuario;

                if (usuarioLog == null)
                {
                    return Unauthorized();
                }

                // Actualizar el tiempo de expiración del token en la caché (Lo hago a lo ultimo por si falla antes de terminar el metodo)
                _cache.Set("Usuario-" + token, usuarioLog, TimeSpan.FromMinutes(30));

                List<Dispositivo> dispositivosDeUsuario = FabricaLogica.GetLogicaDispositivo().ListadoDispositivosXUsuario(usuarioLog);

                foreach (Dispositivo unDisp in dispositivosDeUsuario)
                {
                    listaMensajesXUsuario.AddRange(FabricaLogica.GetLogicaMensajeVisor().ListarMensajeVisorXDispositivoUltimaH(unDisp, usuarioLog));
                }

                //Lo cambio para que me traiga los mensajes de Dispositivos relacionados al usuario conectado
                //listaMensajesXUsuario = FabricaLogica.GetLogicaMensajeVisor().ListarMensajeVisorXUsuarioUltimaH(usuarioLog);

                return Ok(listaMensajesXUsuario);
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error al buscar los Mensajes del Visor en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }




    }


}
