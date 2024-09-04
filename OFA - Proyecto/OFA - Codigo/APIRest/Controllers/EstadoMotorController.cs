using APIRest.Security;
using EntidadesCompartidas;
using Logica;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System;
using Motor;

namespace APIRest.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/estadoMotor")]
    public class EstadoMotorController : ControllerBase
    {

        EstadoMotor motorActivo;
        Usuario usuarioLog;


        //Generacion y uso de Token (Variables y Constructor)
        private IConfiguration _config;
        private TokenService _tokenService;

        private readonly IMemoryCache _cache;

        public EstadoMotorController(IConfiguration config, TokenService tokenService, IMemoryCache memoryCache)
        {
            _config = config;
            _tokenService = tokenService;
            _cache = memoryCache;
        }



        [HttpGet("Start")]
        public IActionResult StartMonitoring()
        {
            try
            {
                // Obtener el token JWT del encabezado de autorización
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                bool validacion = _tokenService.ValidateToken(token);

                if (!validacion)
                {
                    return Unauthorized();
                }

                usuarioLog = _cache.Get("Usuario-" + token) as Administrador;

                if (usuarioLog == null)
                {
                    return Unauthorized();
                }

                // Actualizar el tiempo de expiración del token en la caché (Lo hago a lo ultimo por si falla antes de terminar el metodo)
                _cache.Set("Usuario-" + token, usuarioLog, TimeSpan.FromMinutes(30));

                motorActivo = FabricaLogica.GetLogicaEstadoMotor().BuscarUltimo((Administrador)usuarioLog);

                if (motorActivo == null || motorActivo.Activo != true)
                {
                    motorActivo = new EstadoMotor();

                    motorActivo.Activo = true;
                    motorActivo.UsuarioRegistra = usuarioLog.UsuarioID;
                    motorActivo.UltimaModificacion = DateTime.Now;

                    FabricaLogica.GetLogicaEstadoMotor().Alta(motorActivo, (Administrador)usuarioLog);

                    motorActivo = FabricaLogica.GetLogicaEstadoMotor().BuscarUltimo((Administrador)usuarioLog);

                    new MotorController().SondeoCentral((Administrador)usuarioLog, motorActivo);

                    return Ok(motorActivo); //Devuelvo estado motor
                }
                else 
                {
                    var response = new
                    {
                        errorType = "Internal Server Error",
                        message = "Ocurrio un error interno en el servidor.",
                        details = "El motor ya esta en ejecucion."
                    };

                    return StatusCode(926, response);
                }
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error durante el inicio del Motor en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpGet("Stop")]
        public IActionResult StopMonitoring()
        {
            Usuario usuarioLog;

            try
            {
                // Obtener el token JWT del encabezado de autorización
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                bool validacion = _tokenService.ValidateToken(token);

                if (!validacion)
                {
                    return Unauthorized();
                }

                usuarioLog = _cache.Get("Usuario-" + token) as Administrador;

                if (usuarioLog == null)
                {
                    return Unauthorized();
                }

                // Actualizar el tiempo de expiración del token en la caché (Lo hago a lo ultimo por si falla antes de terminar el metodo)
                _cache.Set("Usuario-" + token, usuarioLog, TimeSpan.FromMinutes(30));

                motorActivo = FabricaLogica.GetLogicaEstadoMotor().BuscarUltimo((Administrador)usuarioLog);

                if (motorActivo.Activo == true)
                {
                    motorActivo.Activo = false;

                    FabricaLogica.GetLogicaEstadoMotor().Alta(motorActivo, (Administrador)usuarioLog);

                    return Ok("El monitoreo de red ha sido detenido.");
                }
                else
                {
                    var response = new
                    {
                        errorType = "Internal Server Error",
                        message = "Ocurrio un error interno en el servidor.",
                        details = "El motor ya esta detenido."
                    };

                    return StatusCode(926, response);
                }
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error durante la detencion del Motor en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpGet("BuscarEstadoMotor")]
        public IActionResult BuscarEstadoMotor()
        {
            Usuario usuarioLog;

            try
            {
                // Obtener el token JWT del encabezado de autorización
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                bool validacion = _tokenService.ValidateToken(token);

                if (!validacion)
                {
                    return Unauthorized();
                }

                usuarioLog = _cache.Get("Usuario-" + token) as Administrador;

                if (usuarioLog == null)
                {
                    return Unauthorized();
                }

                // Actualizar el tiempo de expiración del token en la caché (Lo hago a lo ultimo por si falla antes de terminar el metodo)
                _cache.Set("Usuario-" + token, usuarioLog, TimeSpan.FromMinutes(30));

                motorActivo = FabricaLogica.GetLogicaEstadoMotor().BuscarUltimo((Administrador)usuarioLog);

                return Ok(motorActivo); //Devuelvo
            }

            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error al buscar el Estado del Motor en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



    }


}
