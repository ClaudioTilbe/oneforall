using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sitio.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Sitio.Controllers
{
    public class AnalisisRedController : Controller
    {

        //Variables -----------------------------------------------------
        string mensaje = "";

        Usuario usuLog = new Usuario();
        AnalisisRed unAnalisis = new AnalisisRed();
        List<AnalisisRed> listaAnalisis = new List<AnalisisRed>();


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
        public ActionResult FormAnalisisRedListar(string mensaje = "", string mensajeError = "")
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


                List<AnalisisRed> listaAnalisis = new AnalisisRedAPI().GetAnalisisRedXTodos((Administrador)usuLog);

                HttpContext.Session.SetObjectAsJson("AnalisisRedTodos", listaAnalisis);

                listaAnalisis = listaAnalisis.OrderByDescending(e => e.FechaGenerado).ToList();

                //Ingreso mensaje
                if (mensaje != "")
                {
                    ViewBag.MensajeAccion = mensaje;
                }
                else if (mensajeError != "")
                {
                    ViewBag.MensajeError = mensajeError;
                }


                if (listaAnalisis.Count > 0)//si hay algo para mostrar
                {
                    ViewBag.Mensaje = "Tiene " + listaAnalisis.Count + " Analisis de Red registrados.";

                    return View(listaAnalisis);
                }
                else
                {
                    ViewBag.Mensaje = "No hay Analisis de Red registrados para mostrar";
                    return View(listaAnalisis);
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
                return View(new List<AnalisisRed>());
            }
        }



        [HttpGet]
        public ActionResult FormAnalisisRedVisualizar(int idanalisis)
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


                listaAnalisis = HttpContext.Session.GetObjectFromJson<List<AnalisisRed>>("AnalisisRedTodos");

                unAnalisis = listaAnalisis.FirstOrDefault(analisis => analisis.IDAnalisis == idanalisis);

                return View(unAnalisis);
            }
            catch (Exception ex)
            {
                //Esto se activa cuando un usuario en funciones fue eliminado o modificado
                if (ex.Message.Contains("La petición a la API falló: Error de inicio de sesión"))
                {
                    HttpContext.Session.SetObjectAsJson("ViewBagMensaje", "Ocurrio un error. El estado de su usuario pudo haber sido alterado.");
                    return RedirectToAction("Logueo", "Home");
                }

                //En caso de error en GET vuelvo al listado
                return RedirectToAction("FormAnalisisRedListar", "AnalisisRed", new { mensajeError = ex.Message });
            }
        }



        public ActionResult AnalisisRedCancelar(int idAnalisis)
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


                listaAnalisis = HttpContext.Session.GetObjectFromJson<List<AnalisisRed>>("AnalisisRedTodos");

                //Obtengo el AnalisisRed de mi lista en Session
                unAnalisis = listaAnalisis.FirstOrDefault(analisis => analisis.IDAnalisis == idAnalisis);

                new AnalisisRedAPI().DeleteAnalisisRed((Administrador)usuLog, unAnalisis);

                //MensajeAccion
                mensaje = "Analisis de red con el ID " + unAnalisis.IDAnalisis.ToString() + " cancelado con exito.";

                return RedirectToAction("FormAnalisisRedListar", "AnalisisRed", new { mensaje = mensaje });
            }
            catch (Exception ex)
            {
                //Esto se activa cuando un usuario en funciones fue eliminado o modificado
                if (ex.Message.Contains("La petición a la API falló: Error de inicio de sesión"))
                {
                    HttpContext.Session.SetObjectAsJson("ViewBagMensaje", "Ocurrio un error. El estado de su usuario pudo haber sido alterado.");
                    return RedirectToAction("Logueo", "Home");
                }

                //En caso de error en GET vuelvo al listado
                return RedirectToAction("FormAnalisisRedListar", "AnalisisRed", new { mensajeError = ex.Message });
            }
        }



        [HttpGet]
        public ActionResult FormAnalisisRedGenerar()
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


                ViewBag.Prioridad = new SelectList(new List<string> { "Baja", "Media", "Alta" });

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
                ViewBag.Prioridad = new SelectList(new List<string> { "Baja", "Media", "Alta" });
                return View();
            }

        }



        [HttpPost]
        public ActionResult FormAnalisisRedGenerar(AnalisisRed unAnalisis)
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


                unAnalisis.AdministradorReg = (Administrador)usuLog;
                unAnalisis.Estado = "Pendiente";
                unAnalisis.FechaGenerado = DateTime.Now;
                unAnalisis.FechaFinalizado = DateTime.Now;

                unAnalisis.Validar();

                new AnalisisRedAPI().PostAnalisisRed((Administrador)usuLog, unAnalisis);

                //MensajeAccion
                mensaje = "Analisis de Red Generado con exito.";

                //No hubo error, alta correcto
                return RedirectToAction("FormAnalisisRedListar", "AnalisisRed", new { mensaje = mensaje });
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
                ViewBag.Prioridad = new SelectList(new List<string> { "Baja", "Media", "Alta" });
                return View(unAnalisis);
            }
        }


    }
}
