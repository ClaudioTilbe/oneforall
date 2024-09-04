using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sitio.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Sitio.Controllers
{
    public class ReportesController : Controller
    {

        //Variables -----------------------------------------------------
        string mensaje = "";

        Usuario usuLog = new Usuario();
        Reporte unReporte = new Reporte();
        List<Reporte> listaReportes = new List<Reporte>();
        List<Dispositivo> listaDispositivos = new List<Dispositivo>();



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
        public ActionResult FormReportesListar(string mensaje = "", string mensajeError = "")
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

                listaReportes = new ReportesAPI().GetReportesXCorreo(usuLog, usuLog.MailAsignado);

                //Ingreso mensaje
                if (mensaje != "")
                {
                    ViewBag.MensajeAccion = mensaje;
                }
                else if (mensajeError != "")
                {
                    ViewBag.MensajeError = mensajeError;
                }


                if (listaReportes.Count > 0)
                {
                    //Guardo lista de Reportes en Session
                    HttpContext.Session.SetObjectAsJson("listaReportes", listaReportes);

                    ViewBag.Mensaje = "Tiene " + listaReportes.Count + " Reportes activos.";

                    return View(listaReportes);
                }
                else
                {
                    ViewBag.Mensaje = "No hay Reportes para mostrar";
                    return View(new List<Reporte>());
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
                return View(new List<Reporte>());
            }
        }



        [HttpGet]
        public ActionResult FormReporteVisualizar(int codigo)
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

                List<Reporte> listaReportes = HttpContext.Session.GetObjectFromJson<List<Reporte>>("listaReportes");

                unReporte = listaReportes.FirstOrDefault(r => r.Codigo == codigo);

                return View(unReporte);
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
                return RedirectToAction("FormReportesListar", "Reportes", new { mensajeError = ex.Message });
            }
        }



        [HttpGet]
        public ActionResult FormReporteModificar(int codigo)
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

                List<Reporte> listaReportes = HttpContext.Session.GetObjectFromJson<List<Reporte>>("listaReportes");

                unReporte = listaReportes.FirstOrDefault(r => r.Codigo == codigo);

                listaDispositivos = new DispositivosAPI().GetDispositivosXUsuario(usuLog);

                HttpContext.Session.SetObjectAsJson("DispositivosDisponibles", listaDispositivos);

                ViewBag.DispositivosDisp = new SelectList(listaDispositivos);

                return View(unReporte);
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
                return RedirectToAction("FormReportesListar", "Reportes", new { mensajeError = ex.Message });
            }
        }



        [HttpPost]
        public ActionResult FormReporteModificar(Reporte unReporte)
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

                unReporte.MailOrigen = usuLog.MailAsignado;

                listaDispositivos = HttpContext.Session.GetObjectFromJson<List<Dispositivo>>("DispositivosDisponibles");

                unReporte.DispositivoAsociado = listaDispositivos.FirstOrDefault(r => r.IP == unReporte.DispositivoAsociado.IP);

                unReporte.Validar();

                new ReportesAPI().PutReporte(usuLog, unReporte);

                //MensajeAccion
                mensaje = "Reporte " + unReporte.Codigo.ToString() + " modificado con exito.";

                return RedirectToAction("FormReportesListar", "Reportes", new { mensaje = mensaje });
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
                ViewBag.DispositivosDisp = new SelectList(listaDispositivos);
                return View(unReporte);
            }
        }



        [HttpGet]
        public ActionResult FormReporteEliminar(int codigo)
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

                listaReportes = HttpContext.Session.GetObjectFromJson<List<Reporte>>("listaReportes");

                unReporte = listaReportes.FirstOrDefault(reporte => reporte.Codigo == codigo);

                HttpContext.Session.SetObjectAsJson("ReporteSeleccionado", unReporte);

                return View(unReporte);
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
                return RedirectToAction("FormReportesListar", "Reportes", new { mensajeError = ex.Message });
            }
        }



        [HttpPost]
        public ActionResult FormReporteEliminar(Reporte reporte)
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

                reporte = HttpContext.Session.GetObjectFromJson<Reporte>("ReporteSeleccionado");

                new ReportesAPI().DeleteReporte(usuLog, reporte);

                //MensajeAccion
                mensaje = "Reporte del dispositivo " + reporte.DispositivoAsociado.IP + " eliminado con exito.";

                return RedirectToAction("FormReportesListar", "Reportes", new { mensaje = mensaje });
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
                return View(reporte);
            }
        }



        [HttpGet]
        public ActionResult FormReporteAgregar()
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

                unReporte.MailOrigen = usuLog.MailAsignado;

                listaDispositivos = new DispositivosAPI().GetDispositivosXUsuario(usuLog);

                if (listaDispositivos == null || listaDispositivos.Count == 0)
                {
                    //MensajeAccion
                    mensaje = "No hay dispositivos visibles al usuario. Necesita al menos un dispositivo visible para poder generar un Reporte.";

                    return RedirectToAction("FormReportesListar", "Reportes", new { mensaje = mensaje });
                }

                listaDispositivos = new Dispositivo().OrdenoIPs(listaDispositivos);

                HttpContext.Session.SetObjectAsJson("DispositivosDisponibles", listaDispositivos);

                ViewBag.DispositivosDisp = new SelectList(listaDispositivos);

                return View(unReporte);
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
                ViewBag.DispositivosDisp = new SelectList(listaDispositivos);
                return View();
            }

        }



        [HttpPost]
        public ActionResult FormReporteAgregar(Reporte unReporte)
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

                unReporte.MailOrigen = usuLog.MailAsignado;

                listaDispositivos = HttpContext.Session.GetObjectFromJson<List<Dispositivo>>("DispositivosDisponibles");

                unReporte.DispositivoAsociado = listaDispositivos.FirstOrDefault(r => r.IP == unReporte.DispositivoAsociado.IP);

                unReporte.Validar();

                new ReportesAPI().PostReporte(usuLog, unReporte);

                //MensajeAccion
                mensaje = "Reporte Generado para Dispositivo " + unReporte.DispositivoAsociado.IP + " con exito.";

                return RedirectToAction("FormReportesListar", "Reportes", new { mensaje = mensaje });
            }
            catch (Exception ex)
            {
                //Esto se activa cuando un usuario en funciones fue eliminado o modificado
                if (ex.Message.Contains("La petición a la API falló: Error de inicio de sesión"))
                {
                    HttpContext.Session.SetObjectAsJson("ViewBagMensaje", "Ocurrio un error. El estado de su usuario pudo haber sido alterado.");
                    return RedirectToAction("Logueo", "Home");
                }

                ViewBag.DispositivosDisp = new SelectList(listaDispositivos);
                ViewBag.Mensaje = ex.Message;
                return View(unReporte);
            }
        }




    }


}
