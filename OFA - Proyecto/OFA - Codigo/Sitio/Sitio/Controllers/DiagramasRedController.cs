using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sitio.Models;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Collections;

namespace Sitio.Controllers
{
    public class DiagramasRedController : Controller
    {

        //Variables -----------------------------------------------------
        string mensaje = "";

        Usuario usuLog = new Usuario();
        DiagramaRed unDiagrama = new DiagramaRed(); 
        List<Sucursal> sucursalesDisponibles = new List<Sucursal>();
        List<DiagramaRed> listaDiagramas = new List<DiagramaRed>();



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
        public ActionResult FormDiagramasRedListar(string mensaje = "", string mensajeError = "")
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

                listaDiagramas = new DiagramaRedAPI().GetDiagramasRed(usuLog);

                //Ingreso mensaje
                if (mensaje != "")
                {
                    ViewBag.MensajeAccion = mensaje;
                }
                else if (mensajeError != "")
                {
                    ViewBag.MensajeError = mensajeError;
                }


                if (listaDiagramas.Count > 0)//si hay algo para mostrar
                {
                    ViewBag.Mensaje = "Tiene " + listaDiagramas.Count + " Diagramas de Red para Visualizar.";

                    HttpContext.Session.SetObjectAsJson("listaDiagramas", listaDiagramas);

                    return View(listaDiagramas);
                }
                else
                {
                    ViewBag.Mensaje = "No hay Diagramas de Red para mostrar.";
                    return View(listaDiagramas);
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
                return View(new List<DiagramaRed>());
            }
        }




        [HttpGet]
        public ActionResult FormDiagramaRedBuscar(int nroSucursal)
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

                listaDiagramas = HttpContext.Session.GetObjectFromJson<List<DiagramaRed>>("listaDiagramas");

                unDiagrama = listaDiagramas.FirstOrDefault(diagrama => diagrama._Sucursal.NroSucursal == nroSucursal);

                return View(unDiagrama);
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
                return RedirectToAction("FormDiagramasRedListar", "DiagramasRed", new { mensajeError = ex.Message });
            }
        }



        [HttpGet]
        public ActionResult FormDiagramaRedAgregar()
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


                sucursalesDisponibles = new SucursalesAPI().GetSucursalesXUsuario(usuLog, usuLog);

                if (sucursalesDisponibles == null || sucursalesDisponibles.Count == 0)
                {
                    //MensajeAccion
                    mensaje = "No hay sucursales visibles al usuario. Necesita al menos una sucursal asignada para poder relacionarla con un Diagrama de red.";

                    return RedirectToAction("FormDiagramasRedListar", "DiagramasRed", new { mensaje = mensaje });
                }

                HttpContext.Session.SetObjectAsJson("SucursalesDisponibles", sucursalesDisponibles);
                ViewBag.SucursalesDisp = new SelectList(sucursalesDisponibles);

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
                ViewBag.SucursalesDisp = new SelectList(sucursalesDisponibles);
                return View();
            }
        }



        [HttpPost]
        public ActionResult FormDiagramaRedAgregar(DiagramaRed unDiagrama, IFormFile DiagramaImagen)
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

                sucursalesDisponibles = HttpContext.Session.GetObjectFromJson<List<Sucursal>>("SucursalesDisponibles");

                unDiagrama.FechaSubida = DateTime.Now;

                if (DiagramaImagen == null)
                {
                    ViewBag.Mensaje = "Error en el nombre de diagrama de red. El campo esta vacio.";
                    ViewBag.SucursalesDisp = new SelectList(sucursalesDisponibles);
                    return View(unDiagrama);
                }

                //Valido formato de imagen
                if (DiagramaImagen.ContentType != "image/png")
                {
                    ViewBag.Mensaje = "Ocurrio un error. La imagen del diagrama de red debe estar en formato PNG.";
                    ViewBag.SucursalesDisp = new SelectList(sucursalesDisponibles);
                    return View(unDiagrama);
                }

                //Convierto imagen a byte[]
                if (DiagramaImagen != null && DiagramaImagen.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        DiagramaImagen.CopyTo(memoryStream);
                        unDiagrama.DiagramaImagen = memoryStream.ToArray();
                    }
                }

                unDiagrama._Sucursal = sucursalesDisponibles.Find(x => x.NroSucursal == unDiagrama._Sucursal.NroSucursal);

                unDiagrama.Validar();

                new DiagramaRedAPI().PostDiagramaRed(usuLog, unDiagrama);

                //MensajeAccion
                mensaje = "Diagrama de Red para Sucursal " + unDiagrama._Sucursal.NroSucursal.ToString() + " asignado con exito.";

                //No hubo error, alta correcto
                return RedirectToAction("FormDiagramasRedListar", "DiagramasRed", new { mensaje = mensaje });
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
                ViewBag.SucursalesDisp = new SelectList(sucursalesDisponibles);
                return View(unDiagrama);
            }
        }



        [HttpGet]
        public ActionResult FormDiagramaRedEliminar(int nroSucursal)
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

                //Obtengo el Dispositivo

                listaDiagramas = HttpContext.Session.GetObjectFromJson<List<DiagramaRed>>("listaDiagramas");

                unDiagrama = listaDiagramas.FirstOrDefault(diagrama => diagrama._Sucursal.NroSucursal == nroSucursal);

                HttpContext.Session.SetObjectAsJson("DiagramaSeleccionado", unDiagrama);

                return View(unDiagrama);
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
                return RedirectToAction("FormDiagramasRedListar", "DiagramasRed", new { mensajeError = ex.Message });
            }
        }



        [HttpPost]
        public ActionResult FormDiagramaRedEliminar(DiagramaRed diagrama)
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

                diagrama = HttpContext.Session.GetObjectFromJson<DiagramaRed>("DiagramaSeleccionado");

                new DiagramaRedAPI().DeleteDiagramaRed(usuLog, diagrama);

                //MensajeAccion
                mensaje = "Diagrama de Red asignado a Sucursal " + diagrama._Sucursal.NroSucursal + " eliminado con exito.";

                return RedirectToAction("FormDiagramasRedListar", "DiagramasRed", new { mensaje = mensaje });
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
                return View(diagrama);
            }
        }





    }
}
