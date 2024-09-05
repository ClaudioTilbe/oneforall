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
    [Route("api/diagramasRed")]
    public class DiagramaRedController : ControllerBase
    {

        //Generacion y uso de Token (Variables y Constructor)
        private IConfiguration _config;
        private TokenService _tokenService;

        private readonly IMemoryCache _cache;

        public DiagramaRedController(IConfiguration config, TokenService tokenService, IMemoryCache memoryCache)
        {
            _config = config;
            _tokenService = tokenService;
            _cache = memoryCache;
        }


        //Metodos =======================================================================================

        [HttpGet("NroSucursal={nroSucursal}")]
        public IActionResult GetDiagramaRed(int nroSucursal)
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

                DiagramaRed unDiagrama = FabricaLogica.GetLogicaDiagramaRed().Buscar(nroSucursal, usuarioLog);


                return Ok(unDiagrama);
            }

            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error al buscar el Diagrama de Red en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpGet("")]
        public IActionResult GetDiagramasRed()
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

                List<DiagramaRed> listaDiagramas = FabricaLogica.GetLogicaDiagramaRed().ListadoTodos(usuarioLog);

                return Ok(listaDiagramas);
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error al buscar los Diagramas de Red en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpPost] 
        public IActionResult PostDiagramaRed([FromBody] DiagramaRed unDiagrama)
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

                FabricaLogica.GetLogicaDiagramaRed().Alta(unDiagrama, usuarioLog);

                return Ok();
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error mientras se intentaba agregar el Diagrama de Red en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpDelete("NroSucursal={nroSucursal}")]
        public IActionResult DeleteDiagramaRed(int nroSucursal)
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

                FabricaLogica.GetLogicaDiagramaRed().Baja(nroSucursal, usuarioLog);

                return Ok();
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error mientras se intentaba eliminar el Diagrama de Red en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }


        }
    }
}
