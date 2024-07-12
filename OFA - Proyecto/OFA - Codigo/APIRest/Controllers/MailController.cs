using APIRest.Security;
using EntidadesCompartidas;
using Logica;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;

namespace APIRest.Controllers
{
    [ApiController]
    [Route("api/mails")]
    public class MailController : ControllerBase
    {

        //Generacion y uso de Token (Variables y Constructor)
        private IConfiguration _config;
        private TokenService _tokenService;

        private readonly IMemoryCache _cache;

        public MailController(IConfiguration config, TokenService tokenService, IMemoryCache memoryCache)
        {
            _config = config;
            _tokenService = tokenService;
            _cache = memoryCache;
        }



        [HttpPut("Correo={pCorreo}")]
        public IActionResult PutMail(Mail unMail)
        {
            Usuario usuarioLog;

            try
            {
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

                FabricaLogica.GetLogicaMail().Modificar(unMail, usuarioLog);

                return Ok();
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error al intentar modificar el Mail en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



    }



}
