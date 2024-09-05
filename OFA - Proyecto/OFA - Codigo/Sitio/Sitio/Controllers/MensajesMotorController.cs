using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sitio.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitio.Controllers
{
    public class MensajesMotorController : Controller
    {

        //Variables -----------------------------------------------------
        Usuario usuLog = new Usuario();
        EstadoMotor estadoActivo = new EstadoMotor();
        List<MensajeMotor> listaMensajesMotor = new List<MensajeMotor>();



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
        public ActionResult FormMensajesMotorListar(string mensaje = "")
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

                listaMensajesMotor = new MensajeMotorAPI().GetMensajesMotorAPI((Administrador)usuLog);

                //Ingreso mensaje
                if (mensaje != "")
                {
                    ViewBag.MensajeAccion = mensaje;
                }


                if (listaMensajesMotor.Count > 0)//si hay algo para mostrar
                {
                    ViewBag.Mensaje = "Tiene " + listaMensajesMotor.Count + " Mensajes Motor para Visualizar.";

                    listaMensajesMotor = listaMensajesMotor.OrderByDescending(mensaje => mensaje.IDMensaje).ToList();

                    return View(listaMensajesMotor);
                }
                else
                {
                    ViewBag.Mensaje = "No hay Mensajes de error en la API para mostrar.";
                    return View(listaMensajesMotor);
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
                return View(new List<MensajeMotor>());
            }
        }



    }
}
