using APIRest.Security;
using Logica;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using EntidadesCompartidas;

namespace APIRest.Controllers
{
    [ApiController]
    [Route("api/escaneoPuertos")]
    public class EscaneoPuertosController : ControllerBase
    {
        private IConfiguration _config;
        private TokenService _tokenService;

        private readonly IMemoryCache _cache;

        public EscaneoPuertosController(IConfiguration config, TokenService tokenService, IMemoryCache memoryCache)
        {
            _config = config;
            _tokenService = tokenService;
            _cache = memoryCache;
        }



        //----------------------------------------------------------------------------------------------------
        //Metodos --------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------

        [HttpGet("idEscaneo={idEscaneo}")]
        public IActionResult GetEscaneoPuertos(int idEscaneo)
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

                usuarioLog = _cache.Get("Usuario-" + token) as Usuario;

                if (usuarioLog == null)
                {
                    return Unauthorized();
                }

                // Actualizar el tiempo de expiración del token en la caché (Lo hago a lo ultimo por si falla antes de terminar el metodo)
                _cache.Set("Usuario-" + token, usuarioLog, TimeSpan.FromMinutes(30));

                EscaneoPuertos unEscaneo = FabricaLogica.GetLogicaEscaneoPuertos().Buscar(idEscaneo, (Administrador)usuarioLog);

                return Ok(unEscaneo);
            }

            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error al buscar Escaneo de Puertos en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpGet("xTodos")]
        public IActionResult GetEscaneoPuertosTodos()
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

                usuarioLog = _cache.Get("Usuario-" + token) as Usuario;

                if (usuarioLog == null)
                {
                    return Unauthorized();
                }

                // Actualizar el tiempo de expiración del token en la caché (Lo hago a lo ultimo por si falla antes de terminar el metodo)
                _cache.Set("Usuario-" + token, usuarioLog, TimeSpan.FromMinutes(30));

                // Devolver el usuario como un objeto JSON
                List<EscaneoPuertos> listaEscaneoPuertos = FabricaLogica.GetLogicaEscaneoPuertos().ListadoEscaneoPuertosTodos((Administrador)usuarioLog);

                return Ok(listaEscaneoPuertos);
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error al buscar los Escaneo de Puertos en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpPost]
        public IActionResult PostEscaneoPuertos([FromBody] EscaneoPuertos unEscaneo)
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

                usuarioLog = _cache.Get("Usuario-" + token) as Usuario;

                if (usuarioLog == null)
                {
                    return Unauthorized();
                }

                // Actualizar el tiempo de expiración del token en la caché (Lo hago a lo ultimo por si falla antes de terminar el metodo)
                _cache.Set("Usuario-" + token, usuarioLog, TimeSpan.FromMinutes(30));

                FabricaLogica.GetLogicaEscaneoPuertos().Alta(unEscaneo, (Administrador)usuarioLog);

                return Ok();
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error mientras se da de alta el Escaneo de Puertos en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpPut("")]
        public IActionResult PutEscaneoPuertos([FromBody] EscaneoPuertos unEscaneo)
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

                usuarioLog = _cache.Get("Usuario-" + token) as Usuario;

                if (usuarioLog == null)
                {
                    return Unauthorized();
                }

                // Actualizar el tiempo de expiración del token en la caché (Lo hago a lo ultimo por si falla antes de terminar el metodo)
                _cache.Set("Usuario-" + token, usuarioLog, TimeSpan.FromMinutes(30));

                FabricaLogica.GetLogicaEscaneoPuertos().Actualizar(unEscaneo, (Administrador)usuarioLog);

                return Ok();
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error al modificar el Escaneo de Puertos en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpDelete("")]
        public IActionResult DeleteEscaneoPuertos([FromBody] EscaneoPuertos unEscaneo)
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

                usuarioLog = _cache.Get("Usuario-" + token) as Usuario;

                if (usuarioLog == null)
                {
                    return Unauthorized();
                }

                // Actualizar el tiempo de expiración del token en la caché (Lo hago a lo ultimo por si falla antes de terminar el metodo)
                _cache.Set("Usuario-" + token, usuarioLog, TimeSpan.FromMinutes(30));

                FabricaLogica.GetLogicaEscaneoPuertos().Cancelar(unEscaneo, (Administrador)usuarioLog);

                return Ok();
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error al intentar eliminar el Escaneo de Puertos en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }




    }
}
