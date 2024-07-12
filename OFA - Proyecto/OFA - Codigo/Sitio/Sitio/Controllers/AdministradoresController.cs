using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sitio.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Sitio.Controllers
{
    public class AdministradoresController : Controller
    {
        //Variables -----------------------------------------------------
        string mensaje = "";

        Usuario usuLog = new Usuario();
        Administrador unAdmin = new Administrador();
        List<Mail> mailsDisponibles = new List<Mail>();
        List<Administrador> listaAdministradores = new List<Administrador>();



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
                        usuLog = null; //Lo seteo como null para que no permita seguir en la validacion de usuLog
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
        public ActionResult FormAdministradoresListar(string mensaje = "", string mensajeError = "")
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

                listaAdministradores = new UsuariosAPI().GetAdministradores((Administrador)usuLog);

                //Ingreso mensaje
                if (mensaje != "")
                {
                    ViewBag.MensajeAccion = mensaje;
                }
                else if (mensajeError != "")
                {
                    ViewBag.MensajeError = mensajeError;
                }


                if (listaAdministradores.Count > 0)
                {
                    ViewBag.Mensaje = "Tiene " + listaAdministradores.Count + " Administradores registrados.";

                    //Guardo lista de administradores en Session
                    HttpContext.Session.SetObjectAsJson("listaAdministradores", listaAdministradores);

                    return View(listaAdministradores);
                }
                else
                {
                    //throw new Exception("No hay Administradores para mostrar");
                    ViewBag.Mensaje = "No hay Administradores para mostrar";
                    return View(new List<Administrador>());
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
                return View(new List<Administrador>());

            }
        }



        [HttpGet]
        public ActionResult FormAdministradorVisualizar(string usuarioID)
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

                listaAdministradores = HttpContext.Session.GetObjectFromJson<List<Administrador>>("listaAdministradores");

                unAdmin = listaAdministradores.FirstOrDefault(admin => admin.UsuarioID == usuarioID);

                return View(unAdmin);
            }
            catch (Exception ex)
            {
                //En caso de error en GET vuelvo al listado
                return RedirectToAction("FormAdministradoresListar", "Administradores", new { mensajeError = ex.Message });
            }
        }



        [HttpGet]
        public ActionResult FormAdministradorVisualizarPerfil()
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

                return View((Administrador)usuLog);
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
                return RedirectToAction("FormAdministradoresListar", "Administradores", new { mensajeError = ex.Message });
            }
        }



        [HttpGet]
        public ActionResult FormAdministradorAgregar()
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



        [HttpPost]
        public ActionResult FormAdministradorAgregar(Administrador unAdministrador)
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


                unAdministrador.Validar();

                if (unAdministrador.Contraseña != unAdministrador.ConfirmContra)
                {
                    ViewBag.Mensaje = "La contraseña no coincide con su confirmacion.";
                    return View(unAdministrador);
                }

                new UsuariosAPI().PostUsuario((Administrador)usuLog, unAdministrador);

                //MensajeAccion
                mensaje = "Administrador " + unAdministrador.UsuarioID + " creado con exito.";

                //No hubo error, alta correcto
                return RedirectToAction("FormAdministradoresListar", "Administradores", new { mensaje = mensaje });
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
                return View(unAdministrador);
            }
        }



        [HttpGet]
        public ActionResult FormAdministradorModificar(string usuarioID)
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

                listaAdministradores = HttpContext.Session.GetObjectFromJson<List<Administrador>>("listaAdministradores");
                unAdmin = listaAdministradores.FirstOrDefault(admin => admin.UsuarioID == usuarioID);
                unAdmin.ConfirmContra = unAdmin.Contraseña;

                return View(unAdmin);
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
                return RedirectToAction("FormAdministradoresListar", "Administradores", new { mensajeError = ex.Message });
            }
        }



        [HttpPost]
        public ActionResult FormAdministradorModificar(Administrador unAdministrador)
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

                //Traigo su informacion anterior para pasar entidad mail
                listaAdministradores = HttpContext.Session.GetObjectFromJson<List<Administrador>>("listaAdministradores");
                unAdmin = listaAdministradores.FirstOrDefault(admin => admin.UsuarioID == unAdministrador.UsuarioID);

                unAdministrador.MailAsignado = unAdmin.MailAsignado;

                unAdministrador.Validar();

                if (unAdministrador.Contraseña != unAdministrador.ConfirmContra)
                {
                    ViewBag.Mensaje = "La contraseña no coincide con su confirmacion.";
                    return View(unAdministrador);
                }

                new UsuariosAPI().PutUsuario((Administrador)usuLog, unAdministrador);

                //MensajeAccion
                mensaje = "Administrador " + unAdministrador.UsuarioID + " modificado con exito.";

                //No hubo error, alta correcto
                return RedirectToAction("FormAdministradoresListar", "Administradores", new { mensaje = mensaje });
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
                return View(unAdministrador);
            }
        }



        [HttpGet]
        public ActionResult FormAdministradorEliminar(string usuarioID)
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

                if (usuarioID == usuLog.UsuarioID)
                {
                    return RedirectToAction("FormAdministradoresListar", "Administradores", new { mensajeError = "No es posible eliminar tu propio usuario. Contacte a otro Administrador para realizar la gestión." });
                }

                listaAdministradores = HttpContext.Session.GetObjectFromJson<List<Administrador>>("listaAdministradores");

                unAdmin = listaAdministradores.FirstOrDefault(admin => admin.UsuarioID == usuarioID);

                HttpContext.Session.SetObjectAsJson("AdministradorSeleccionado", unAdmin);

                return View(unAdmin);
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
                return RedirectToAction("FormAdministradoresListar", "Administradores", new { mensajeError = ex.Message });
            }
        }



        [HttpPost]
        public ActionResult FormAdministradorEliminar()
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

                unAdmin = HttpContext.Session.GetObjectFromJson<Administrador>("AdministradorSeleccionado");

                new UsuariosAPI().DeleteUsuario((Administrador)usuLog, unAdmin);

                //MensajeAccion
                mensaje = "Administrador " + unAdmin.UsuarioID + " eliminado con exito.";

                return RedirectToAction("FormAdministradoresListar", "Administradores", new { mensaje = mensaje });
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
                return View(unAdmin);
            }
        }

















    }
}
