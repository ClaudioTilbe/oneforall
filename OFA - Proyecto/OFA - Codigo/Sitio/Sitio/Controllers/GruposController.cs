using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sitio.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Sitio.Controllers
{
    public class GruposController : Controller
    {

        //Variables -----------------------------------------------------
        string mensaje = "";

        Usuario usuLog = new Usuario();
        Grupo unGrupo = new Grupo();
        List<Grupo> listaGrupos = new List<Grupo>();
        List<Dispositivo> dispositivosDisponibles = new List<Dispositivo>();



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
        public ActionResult FormGruposListar(string mensaje = "", string mensajeError = "")
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

                listaGrupos = new GruposAPI().GetGruposXUsuario(usuLog);

                //Ingreso mensaje
                if (mensaje != "")
                {
                    ViewBag.MensajeAccion = mensaje;
                }
                else if (mensajeError != "")
                {
                    ViewBag.MensajeError = mensajeError;
                }


                if (listaGrupos.Count > 0)
                {
                    HttpContext.Session.SetObjectAsJson("listaGrupos", listaGrupos);

                    ViewBag.Mensaje = "Tiene " + listaGrupos.Count + " Grupos activos.";

                    return View(listaGrupos);
                }
                else
                {
                    ViewBag.Mensaje = "No hay Grupos para mostrar";
                    return View(new List<Grupo>());
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
                return View(new List<Grupo>());
            }
        }



        [HttpGet]
        public ActionResult FormGrupoVisualizar(int codigo)
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

                listaGrupos = HttpContext.Session.GetObjectFromJson<List<Grupo>>("listaGrupos");

                unGrupo = listaGrupos.FirstOrDefault(r => r.Codigo == codigo);

                return View(unGrupo);
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
                return RedirectToAction("FormGruposListar", "Grupos", new { mensajeError = ex.Message });
            }
        }



        [HttpGet]
        public ActionResult FormGrupoModificar(int codigo)
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

                listaGrupos = HttpContext.Session.GetObjectFromJson<List<Grupo>>("listaGrupos");

                unGrupo = listaGrupos.FirstOrDefault(r => r.Codigo == codigo);


                //Traigo mis dispositivos disponibles para mostrar en el formulario
                dispositivosDisponibles = new DispositivosAPI().GetDispositivosXUsuario(usuLog);


                if (dispositivosDisponibles != null || dispositivosDisponibles.Count > 0)
                {
                    dispositivosDisponibles = new Dispositivo().OrdenoIPs(dispositivosDisponibles);
                }

                HttpContext.Session.SetObjectAsJson("DispositivosDisponibles", dispositivosDisponibles);

                return View(unGrupo);
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
                return RedirectToAction("FormGruposListar", "Grupos", new { mensajeError = ex.Message });
            }
        }



        [HttpPost]
        public ActionResult FormGrupoModificar(string[] direccionesIP, Grupo unGrupo)
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

                dispositivosDisponibles = HttpContext.Session.GetObjectFromJson<List<Dispositivo>>("DispositivosDisponibles");

                //Identifico que IPs de la lista de dispositivos disponibles coinciden con las seleccionadas (LinQ)
                unGrupo.Dispositivos = dispositivosDisponibles.Where(d => direccionesIP.Contains(d.IP)).ToList();

                unGrupo.Validar();

                new GruposAPI().PutGrupo(usuLog, unGrupo);

                //MensajeAccion
                mensaje = "Grupo de dispositivos " + unGrupo.NombreGrupo + " modificado con exito.";

                return RedirectToAction("FormGruposListar", "Grupos", new { mensaje = mensaje });
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
                return View(unGrupo);
            }
        }



        [HttpGet]
        public ActionResult FormGrupoEliminar(int codigo)
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

                listaGrupos = HttpContext.Session.GetObjectFromJson<List<Grupo>>("listaGrupos");

                unGrupo = listaGrupos.FirstOrDefault(x => x.Codigo == codigo);

                HttpContext.Session.SetObjectAsJson("GrupoSeleccionado", unGrupo);

                if (unGrupo.Dispositivos != null || unGrupo.Dispositivos.Count > 0)
                {
                    unGrupo.Dispositivos = new Dispositivo().OrdenoIPs(unGrupo.Dispositivos);
                }

                return View(unGrupo);
            }
            catch (Exception ex)
            {
                //En caso de error en GET vuelvo al listado
                return RedirectToAction("FormGruposListar", "Grupos", new { mensajeError = ex.Message });
            }
        }



        [HttpPost]
        public ActionResult FormGrupoEliminar(Grupo unGrupo)
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

                unGrupo = HttpContext.Session.GetObjectFromJson<Grupo>("GrupoSeleccionado");

                unGrupo._Usuario = null;

                new GruposAPI().DeleteGrupo(usuLog, unGrupo);

                //MensajeAccion
                mensaje = "El Grupo con el codigo " + unGrupo.Codigo.ToString() + " fue eliminado con exito.";

                return RedirectToAction("FormGruposListar", "Grupos", new { mensaje = mensaje });
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
                return View(unGrupo);
            }
        }



        [HttpGet]
        public ActionResult FormGrupoAgregar()
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

                dispositivosDisponibles = new DispositivosAPI().GetDispositivosXUsuario(usuLog);

                if (dispositivosDisponibles == null || dispositivosDisponibles.Count == 0)
                {
                    //MensajeAccion
                    mensaje = "No hay dispositivos visibles al usuario. Necesita al menos 1 dispositivo visible para poder generar un Grupo.";

                    return RedirectToAction("FormGruposListar", "Grupos", new { mensaje = mensaje });
                }

                dispositivosDisponibles = new Dispositivo().OrdenoIPs(dispositivosDisponibles);

                HttpContext.Session.SetObjectAsJson("DispositivosDisponibles", dispositivosDisponibles);

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
        public ActionResult FormGrupoAgregar(string[] direccionesIP, Grupo unGrupo)
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

                dispositivosDisponibles = HttpContext.Session.GetObjectFromJson<List<Dispositivo>>("DispositivosDisponibles");

                unGrupo.Dispositivos = dispositivosDisponibles.Where(d => direccionesIP.Contains(d.IP)).ToList();
                unGrupo._Usuario = usuLog;

                unGrupo.Validar();

                new GruposAPI().PostGrupo(usuLog, unGrupo);

                //MensajeAccion
                mensaje = "Grupo " + unGrupo.NombreGrupo + " generado con exito.";

                return RedirectToAction("FormGruposListar", "Grupos", new { mensaje = mensaje });
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
                return View(unGrupo);
            }
        }



    }
}