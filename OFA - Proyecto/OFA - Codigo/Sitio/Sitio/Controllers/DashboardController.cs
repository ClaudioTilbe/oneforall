using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sitio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sitio.Controllers
{
    public class DashboardController : Controller
    {
        //Variables -----------------------------------------------------
        Usuario usuLog = new Usuario();
        List<Sucursal> sucursalesDisponibles = new List<Sucursal>();



        //Metodos -------------------------------------------------------
        private void VerificoAdministrador()
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
                        usuLog = null; 
                        HttpContext.Session.Remove("UsuarioLog");
                        HttpContext.Session.SetObjectAsJson("ViewBagMensaje", "No tiene permisos para ingresar a esa funcion.");
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
        public ActionResult FormDispositivosDashboard()
        {
            try
            {
                // Verificacion de Usuario ======================================================================
                VerificoAdministrador();

                if (usuLog == null)
                {
                    return RedirectToAction("Logueo", "Home");
                }
                // ==============================================================================================

                List<Dispositivo> _lista = new DispositivosAPI().GetDispositivosXUsuario(usuLog);

                HttpContext.Session.SetObjectAsJson("listaDispositivos", _lista);


                if (_lista.Count > 0)//si hay algo para mostrar
                {
                    ViewBag.Mensaje = "Tiene " + _lista.Count + " Dispositivos activos.";
                }
                else
                {
                    ViewBag.Mensaje = "No hay Dispositivos para mostrar.";
                }

                return View();
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















    }
}
