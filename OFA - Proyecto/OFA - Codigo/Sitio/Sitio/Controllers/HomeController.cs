using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sitio.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sitio.Controllers
{
    public class HomeController : Controller
    {
        //Variables
        private IHttpContextAccessor Accessor;
        private readonly IConfiguration _configuration;


        //Variables -----------------------------------------------------
        Usuario usuLog = new Usuario();
        List<Grupo> listaGrupos = new List<Grupo>();


        public HomeController(IHttpContextAccessor _accessor, IConfiguration config)
        {
            this.Accessor = _accessor;
            _configuration = config;

        }


        //Metodos -------------------------------------------------------
        private void VerificoUsuario()
        {
            try
            {
                usuLog = HttpContext.Session.GetObjectFromJson<Usuario>("UsuarioLog");

                if (usuLog != null)
                {
                    if (usuLog.Tipo == "Administrador")
                    {
                        usuLog = HttpContext.Session.GetObjectFromJson<Administrador>("UsuarioLog");
                    }
                    else if (usuLog.Tipo == "Operador")
                    {
                        usuLog = HttpContext.Session.GetObjectFromJson<Operador>("UsuarioLog");
                    }
                }
                else
                {
                    HttpContext.Session.Remove("UsuarioLog");
                    HttpContext.Session.SetObjectAsJson("ViewBagMensaje", "Su sesión a caducado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        [HttpGet]
        public ActionResult IndexMonitoreo()
        {
            try
            {
                // Verificacion de Usuario ======================================================================
                VerificoUsuario();

                if (usuLog == null)
                {
                    return RedirectToAction("Logueo", "Home");
                }
                // ==============================================================================================

                List<Dispositivo> _lista = new DispositivosAPI().GetDispositivosXUsuario(usuLog);

                if (_lista != null && _lista.Count > 0) 
                {
                    _lista = new Dispositivo().OrdenoIPs(_lista);
                }

                List<Grupo> listaGrupos = new GruposAPI().GetGruposXUsuario(usuLog);

                ViewBag.Grupos = new SelectList(listaGrupos);


                if (_lista.Count > 0)//si hay algo para mostrar
                {
                    return View(_lista);
                }
                else
                {
                    throw new Exception("No hay Dispositivos para mostrar");
                }
            }
            catch (Exception ex)
            {
                //Esto se activa cuando un usuario en funciones fue eliminado o modificado
                if (ex.Message.Contains("La petición a la API falló: Error de inicio de sesión"))
                {
                    HttpContext.Session.SetObjectAsJson("ViewBagMensaje", "Ocurrio un error. El estado de su usuario pudo haber sido alterado.");
                    return RedirectToAction("Logueo", "Home");
                }

                ViewBag.Mensaje = ex.Message;
                ViewBag.Grupos = new SelectList(new List<Grupo>());
                return View(new List<Dispositivo>());
            }
        }



        public ActionResult Logueo()
        {
            HttpContext.Session.Remove("UsuarioLog");

            return View();
        }



        [HttpPost]
        public ActionResult Logueo(Usuario unUsu)
        {
            try
            {
                unUsu.ValidarLogueo();

                Usuario unUsuario = new UsuariosAPI().Logueo(unUsu.UsuarioID, unUsu.Contraseña);

                if (unUsuario is Operador)
                {
                    //Guardo en Session usuario Logueado
                    HttpContext.Session.SetObjectAsJson("UsuarioLog", unUsuario);
                }

                else if (unUsuario is Administrador)
                {
                    HttpContext.Session.SetObjectAsJson("UsuarioLog", unUsuario);
                }


                return RedirectToAction("IndexMonitoreo", "Home");
            }
            catch (Exception ex)
            {
                //Esto se activa cuando un usuario en funciones fue eliminado o modificado
                if (ex.Message.Contains("La petición a la API falló: Error de inicio de sesión"))
                {
                    HttpContext.Session.SetObjectAsJson("ViewBagMensaje", "Ocurrio un error. El estado de su usuario pudo haber sido alterado.");
                    return RedirectToAction("Logueo", "Home");
                }

                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }



        [HttpPost]
        public ActionResult DesLogueo()
        {
            HttpContext.Session.Remove("UsuarioLog");
            return RedirectToAction("Logueo", "Home");
        }



        [HttpPost]
        public ActionResult ActualizoUsuarioSession()
        {
            VerificoUsuario();

            return Ok();
        }


    }
}
