using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sitio.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using Sitio.Utility;

namespace Sitio.Controllers
{
    public class EscaneoPuertosController : Controller
    {
        //Variables -----------------------------------------------------
        string mensaje = "";

        Usuario usuLog = new Usuario();
        EscaneoPuertos unEscaneo = new EscaneoPuertos();
        List<EscaneoPuertos> listaEscaneos = new List<EscaneoPuertos>();
        List<Dispositivo> dispositivosDisponibles = new List<Dispositivo>();



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
        public ActionResult FormEscaneosPuertosListar(string mensaje = "", string mensajeError = "")
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

                listaEscaneos = new EscaneoPuertosAPI().GetEscaneoPuertosXTodos((Administrador)usuLog);

                HttpContext.Session.SetObjectAsJson("listaEscaneosPuertos", listaEscaneos);

                listaEscaneos = listaEscaneos.OrderByDescending(e => e.FechaGenerado).ToList();

                //Ingreso mensaje
                if (mensaje != "")
                {
                    ViewBag.MensajeAccion = mensaje;
                }
                else if (mensajeError != "")
                {
                    ViewBag.MensajeError = mensajeError;
                }


                if (listaEscaneos.Count > 0)//si hay algo para mostrar
                {
                    ViewBag.Mensaje = "Tiene " + listaEscaneos.Count + " Escaneos de puertos registrados.";

                    return View(listaEscaneos);
                }
                else
                {
                    ViewBag.Mensaje = "No hay Escaneos de puertos registrados para mostrar";
                    return View(listaEscaneos);
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
                return View(new List<EscaneoPuertos>());
            }
        }



        [HttpGet]
        public ActionResult FormEscaneoPuertosVisualizar(int idescaneo)
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


                listaEscaneos = HttpContext.Session.GetObjectFromJson<List<EscaneoPuertos>>("listaEscaneosPuertos");

                unEscaneo = listaEscaneos.FirstOrDefault(analisis => analisis.IDEscaneo == idescaneo);

                return View(unEscaneo);
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
                return RedirectToAction("FormEscaneoPuertosListar", "EscaneoPuertos", new { mensajeError = ex.Message });
            }
        }



        public ActionResult EscaneoPuertosCancelar(int idEscaneo)
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

                listaEscaneos = HttpContext.Session.GetObjectFromJson<List<EscaneoPuertos>>("listaEscaneosPuertos");

                //Obtengo el AnalisisRed de mi lista en Session
                unEscaneo = listaEscaneos.FirstOrDefault(escaneo => escaneo.IDEscaneo == idEscaneo);

                new EscaneoPuertosAPI().DeleteEscaneoPuertos((Administrador)usuLog, unEscaneo);

                //MensajeAccion
                mensaje = "Escaneo de puertos con el ID " + unEscaneo.IDEscaneo.ToString() + " cancelado con exito.";

                return RedirectToAction("FormEscaneosPuertosListar", "EscaneoPuertos", new { mensaje = mensaje });
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
                return RedirectToAction("FormEscaneoPuertosListar", "EscaneoPuertos", new { mensajeError = ex.Message });
            }
        }



        [HttpGet]
        public ActionResult FormEscaneoPuertosGenerar()
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

                dispositivosDisponibles = new DispositivosAPI().GetDispositivosXUsuario(usuLog);

                if (dispositivosDisponibles == null || dispositivosDisponibles.Count == 0)
                {
                    //MensajeAccion
                    mensaje = "No hay dispositivos visibles al usuario. Necesita al menos un dispositivo visible para poder generar un escaneo de puertos.";

                    return RedirectToAction("FormEscaneosPuertosListar", "EscaneoPuertos", new { mensaje = mensaje });
                }


                dispositivosDisponibles = new Dispositivo().OrdenoIPs(dispositivosDisponibles);

                HttpContext.Session.SetObjectAsJson("DispositivosDisponibles", dispositivosDisponibles);

                ViewBag.DispositivosDisp = new SelectList(dispositivosDisponibles);

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
                ViewBag.DispositivosDisp = new SelectList(dispositivosDisponibles);
                return View();
            }
        }



        [HttpPost]
        public ActionResult FormEscaneoPuertosGenerar(EscaneoPuertos unEscaneo)
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

                unEscaneo.Estado = "Pendiente";
                unEscaneo.AdministradorReg = (Administrador)usuLog;
                unEscaneo.CadenaSalida = "Escaneo pendiente.";
                unEscaneo.FechaGenerado = DateTime.Now;

                dispositivosDisponibles = HttpContext.Session.GetObjectFromJson<List<Dispositivo>>("DispositivosDisponibles");

                unEscaneo.DispositivoObjetivo = dispositivosDisponibles.FirstOrDefault(disp => disp.IP == unEscaneo.DispositivoObjetivo.IP);

                unEscaneo.Validar();

                new EscaneoPuertosAPI().PostEscaneoPuertos((Administrador)usuLog, unEscaneo);

                //MensajeAccion
                mensaje = "Escaneo de puertos generado con exito.";

                //No hubo error, alta correcto
                return RedirectToAction("FormEscaneosPuertosListar", "EscaneoPuertos", new { mensaje = mensaje });
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
                ViewBag.DispositivosDisp = new SelectList(dispositivosDisponibles);
                return View(unEscaneo);
            }
        }


    }
}
