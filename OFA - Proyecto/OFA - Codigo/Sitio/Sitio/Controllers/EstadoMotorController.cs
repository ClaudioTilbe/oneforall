using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sitio.Models;
using System.Collections.Generic;
using System;

namespace Sitio.Controllers
{
    public class EstadoMotorController : Controller
    {
        //Variables -----------------------------------------------------
        Usuario usuLog = new Usuario();
        List<Sucursal> sucursalesDisponibles = new List<Sucursal>();
        EstadoMotor estadoActivo = new EstadoMotor();



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



        public IActionResult FormEstadoMotorPanel()
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


                //Aqui codigo para iniciar API Rest
                estadoActivo = new EstadoMotorAPI().BuscarEstadoMotorAPI((Administrador)usuLog);

                if (estadoActivo != null)
                {
                    HttpContext.Session.SetObjectAsJson("estadoMotor", estadoActivo);

                    return View("FormEstadoMotorPanel", estadoActivo);
                }
                else
                {
                    estadoActivo = new EstadoMotor(); //Inicializo para devolverlo y no me de error si no hay registros de estado motor
                    estadoActivo.Activo = false;

                    return View("FormEstadoMotorPanel", estadoActivo);
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
                return View("FormEstadoMotorPanel", new EstadoMotor(false));
            }
        }



        public IActionResult InicioMotorAPI()
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

                //Aqui codigo para iniciar API Rest
                estadoActivo = new EstadoMotorAPI().StartAPI((Administrador)usuLog);

                ViewBag.Mensaje = "El motor se a iniciado correctamente.";

                //Me falta pasar mensaje en el ViewBag de Exito
                return View("FormEstadoMotorPanel", estadoActivo);
            }
            catch (Exception ex)
            {
                //Esto se activa cuando un usuario en funciones fue eliminado o modificado
                if (ex.Message.Contains("La petición a la API falló: Error de inicio de sesión"))
                {
                    HttpContext.Session.SetObjectAsJson("ViewBagMensaje", "Ocurrio un error. El estado de su usuario pudo haber sido alterado.");
                    return RedirectToAction("Logueo", "Home");
                }

                estadoActivo = HttpContext.Session.GetObjectFromJson<EstadoMotor>("estadoMotor");

                ViewBag.Mensaje = ex.Message;
                return View("FormEstadoMotorPanel", estadoActivo);
            }
        }



        public IActionResult DetengoMotorAPI()
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

                estadoActivo = new EstadoMotorAPI().BuscarEstadoMotorAPI((Administrador)usuLog);

                if (estadoActivo == null)
                {
                    ViewBag.Mensaje = "El motor debe estar iniciado para posteriormente poder ser detenido.";
                    return View("FormEstadoMotorPanel", new EstadoMotor(false));
                }

                //Aqui codigo para iniciar API Rest
                new EstadoMotorAPI().StopAPI((Administrador)usuLog);

                //Actualizo estado
                estadoActivo = new EstadoMotorAPI().BuscarEstadoMotorAPI((Administrador)usuLog);

                ViewBag.Mensaje = "El motor se a detenido correctamente.";

                return View("FormEstadoMotorPanel", estadoActivo);
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
                return View("FormEstadoMotorPanel", new EstadoMotor(false));
            }
        }



      





    }


}
