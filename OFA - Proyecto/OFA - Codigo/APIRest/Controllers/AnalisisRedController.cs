using EntidadesCompartidas;
using Logica;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using APIRest.Security;

namespace APIRest.Controllers
{
    [ApiController]
    [Route("api/analisisred")]
    public class AnalisisRedController : ControllerBase
    {
        private IConfiguration _config;
        private TokenService _tokenService;

        private readonly IMemoryCache _cache;

        public AnalisisRedController(IConfiguration config, TokenService tokenService, IMemoryCache memoryCache)
        {
            _config = config;
            _tokenService = tokenService;
            _cache = memoryCache;
        }



        //----------------------------------------------------------------------------------------------------
        //Metodos --------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------

        [HttpGet("idAnalisis={idAnalisis}")]
        public IActionResult GetAnalisisRed(int idAnalisis)
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

                AnalisisRed unAnalisis = FabricaLogica.GetLogicaAnalisisRed().Buscar(idAnalisis, (Administrador)usuarioLog);


                return Ok(unAnalisis);
            }

            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error al buscar Analisis de Red en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpGet("xTodos")]
        public IActionResult GetAnalisisRedTodos()
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
                List<AnalisisRed> listaAnalisisRed = FabricaLogica.GetLogicaAnalisisRed().ListadoAnalisisRedTodos((Administrador)usuarioLog);

                return Ok(listaAnalisisRed);
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error al buscar los Analisis de Red en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpPost]
        public IActionResult PostAnalisisRed([FromBody] AnalisisRed unAnalisis)
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

                FabricaLogica.GetLogicaAnalisisRed().Alta(unAnalisis, (Administrador)usuarioLog);

                return Ok();
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error mientras se da de alta el Analisis de Red en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpPut("")]
        public IActionResult PutAnalisisRed([FromBody] AnalisisRed unAnalisis)
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

                FabricaLogica.GetLogicaAnalisisRed().Actualizar(unAnalisis, (Administrador)usuarioLog);

                return Ok();
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error al modificar el Analisis de Red en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpDelete("")]
        public IActionResult DeleteAnalisisRed([FromBody] AnalisisRed unAnalisis)
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

                FabricaLogica.GetLogicaAnalisisRed().Cancelar(unAnalisis, (Administrador)usuarioLog);

                return Ok();
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error al intentar eliminar el Analisis de Red en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



    }



}
