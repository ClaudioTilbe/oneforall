using APIRest.Security;
using Logica;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Dynamic;
using System;
using EntidadesCompartidas;
using APIRest.DTOs;

namespace APIRest.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {

        //Generacion y uso de Token (Variables y Constructor)
        private IConfiguration _config;
        private TokenService _tokenService;

        //Memoria Cache
        private readonly IMemoryCache _cache;

        //Constructor
        public UsuarioController(IConfiguration config, TokenService tokenService, IMemoryCache memoryCache)
        {
            _config = config;
            _tokenService = tokenService;
            _cache = memoryCache;
        }



        [HttpPost("Login")]
        public IActionResult GetLogueoUsuario([FromBody] UsuarioLoginDTO dto)
        {
            try
            {
                Usuario unUsuario = FabricaLogica.GetLogicaUsuario().Logueo(dto.UsuarioID, dto.Contraseña);

                if (unUsuario == null)
                {
                    return Unauthorized();
                }

                string token = _tokenService.GenerateToken(unUsuario);

                _cache.Set("Usuario-" + token, unUsuario, TimeSpan.FromMinutes(30));

                //Agrego parametro de Tipo
                ExpandoObject unUsuarioExtendido = Utility.SetTypeToUsuario.SetType(unUsuario, token);

                return Ok(unUsuarioExtendido);
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error durante el Logueo en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }

        }



        [HttpGet("UsuarioID={usuarioID}")]
        public IActionResult GetUsuario(string usuarioID)
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

                Usuario unUsuario = FabricaLogica.GetLogicaUsuario().Buscar(usuarioID, usuarioLog);

                //Agrego parametro de Tipo
                ExpandoObject unUsuarioExtendido = Utility.SetTypeToUsuario.SetType(unUsuario, null);

                return Ok(unUsuarioExtendido);
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error al buscar el Usuario en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpGet("operadores")]
        public IActionResult GetOperadores()
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

                List<Operador> listaOperadores = FabricaLogica.GetLogicaUsuario().ListadoOperadores((Administrador)usuarioLog);

                return Ok(listaOperadores);
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error al buscar Operadores en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpGet("administradores")]
        public IActionResult GetAdministradores()
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

                List<Administrador> listaAdministradores = FabricaLogica.GetLogicaUsuario().ListadoAdministradores((Administrador)usuarioLog);

                return Ok(listaAdministradores);
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error al buscar Administradores en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpPost("")]
        public IActionResult PostUsuario([FromBody] UsuarioAltaDTO dto)
        {
            Usuario usuarioLog;
            Usuario usuAlta;

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

                if (dto.Tipo == "Administrador")
                {
                    usuAlta = new Administrador
                    {
                        UsuarioID = dto.UsuarioID,
                        Contraseña = dto.Contraseña,
                        MailAsignado = dto.MailAsignado,
                        Nombre = dto.Nombre,
                        NumeroFuncionario = dto.NumeroFuncionario,
                        Cargo = dto.Cargo
                    };

                }

                else if (dto.Tipo == "Operador")
                {
                    usuAlta = new Operador
                    {
                        UsuarioID = dto.UsuarioID,
                        Contraseña = dto.Contraseña,
                        MailAsignado = dto.MailAsignado,
                        NumeroFuncionarioSupervisor = dto.NumeroFuncionarioSupervisor,
                        NombreSupervisor = dto.NombreSupervisor,
                        SucursalesAsignadas = dto.SucursalesAsignadas
                    };
                }

                else
                {
                    return Unauthorized(); 
                }


                FabricaLogica.GetLogicaUsuario().Alta(usuAlta, (Administrador)usuarioLog);

                return Ok();

            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error mientras se daba de alta en Usuario en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpPut("")]
        public IActionResult PutUsuario([FromBody] UsuarioModificarDTO dto)
        {
            Usuario usuarioLog;
            Usuario usuAlta;

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


                if (dto.Tipo == "Administrador")
                {
                    usuAlta = new Administrador
                    {
                        UsuarioID = dto.UsuarioID,
                        Contraseña = dto.Contraseña,
                        MailAsignado = dto.MailAsignado,
                        Nombre = dto.Nombre,
                        NumeroFuncionario = dto.NumeroFuncionario,
                        Cargo = dto.Cargo
                    };

                }

                else if (dto.Tipo == "Operador")
                {
                    usuAlta = new Operador
                    {
                        UsuarioID = dto.UsuarioID,
                        Contraseña = dto.Contraseña,
                        MailAsignado = dto.MailAsignado,
                        NumeroFuncionarioSupervisor = dto.NumeroFuncionarioSupervisor,
                        NombreSupervisor = dto.NombreSupervisor,
                        SucursalesAsignadas = dto.SucursalesAsignadas
                    };
                }

                else
                {
                    return Unauthorized(); 
                }

                FabricaLogica.GetLogicaUsuario().Modificar(usuAlta, (Administrador)usuarioLog);

                return Ok();
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error mientras se modificaba el Usuario en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



        [HttpDelete("")]
        public IActionResult DeleteUsuario([FromBody] UsuarioEliminarDTO dto)
        {
            Usuario usuarioLog;
            Usuario usuAlta;

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

                if (dto.Tipo == "Administrador")
                {
                    usuAlta = new Administrador
                    {
                        UsuarioID = dto.UsuarioID,
                        Contraseña = dto.Contraseña,
                        MailAsignado = dto.MailAsignado,
                        Nombre = dto.Nombre,
                        NumeroFuncionario = dto.NumeroFuncionario,
                        Cargo = dto.Cargo
                    };

                }

                else if (dto.Tipo == "Operador")
                {
                    usuAlta = new Operador
                    {
                        UsuarioID = dto.UsuarioID,
                        Contraseña = dto.Contraseña,
                        MailAsignado = dto.MailAsignado,
                        NumeroFuncionarioSupervisor = dto.NumeroFuncionarioSupervisor,
                        NombreSupervisor = dto.NombreSupervisor,
                        SucursalesAsignadas = dto.SucursalesAsignadas
                    };
                }

                else
                {
                    return Unauthorized();
                }

                FabricaLogica.GetLogicaUsuario().Baja(usuAlta, (Administrador)usuarioLog);

                return Ok();
            }
            catch (Exception ex)
            {
                // Crear una respuesta de error personalizada
                var response = new
                {
                    errorType = "Internal Server Error",
                    message = "Ocurrió un error mientras se daba de baja el Usuario en la API Rest.",
                    details = ex.Message
                };

                return StatusCode(926, response);
            }
        }



    }


}

