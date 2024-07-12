using APIRest.Security;
using EntidadesCompartidas;
using Logica;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using System.IO;

namespace APIRest.Controllers
{
    [ApiController]
    [Route("api/sucursales")]
    public class SucursalController : ControllerBase
    {

        //Generacion y uso de Token (Variables y Constructor)
        private IConfiguration _config;
        private TokenService _tokenService;

        private readonly IMemoryCache _cache;

        public SucursalController(IConfiguration config, TokenService tokenService, IMemoryCache memoryCache)
        {
            _config = config;
            _tokenService = tokenService;
            _cache = memoryCache;
        }



        [HttpGet("NumeroSucursal={nroSucursal}")]
        public IActionResult GetSucursal(int nroSucursal)
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

                Sucursal unaSucursal = FabricaLogica.GetLogicaSucursal().Buscar(nroSucursal, usuarioLog);

                return Ok(unaSucursal);
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error al buscar la Sucursal en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpPost("xUsuario")]
        public IActionResult GetSucursalesxUsuario([FromBody] string UsuarioID)
        {
            Usuario usuarioLog;
            Usuario usuarioBuscado;

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

                usuarioBuscado = FabricaLogica.GetLogicaUsuario().Buscar(UsuarioID, usuarioLog);

                List<Sucursal> listaSucursales = FabricaLogica.GetLogicaSucursal().ListarSucursalesXUsuario(usuarioBuscado, usuarioLog);

                return Ok(listaSucursales);
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error al buscar las Sucursales en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpPost("")]
        public IActionResult PostSucursal([FromBody] Sucursal unaSucursal)
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

                FabricaLogica.GetLogicaSucursal().Alta(unaSucursal, (Administrador)usuarioLog);

                return Ok();
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error mientras se intentaba dar de alta la Sucursal en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpPut("")]
        public IActionResult PutSucursal([FromBody] Sucursal unaSucursal)
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

                FabricaLogica.GetLogicaSucursal().Modificar(unaSucursal, (Administrador)usuarioLog);

                return Ok();
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error mientras se intentaba modificar la Sucursal en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpDelete("")]
        public IActionResult DeleteSucursal([FromBody] Sucursal unaSucursal)
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

                FabricaLogica.GetLogicaSucursal().Baja(unaSucursal, (Administrador)usuarioLog);

                return Ok();
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error mientras se intentaba eliminar la Sucursal en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



    }
}
