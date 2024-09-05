using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sitio.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace Sitio.Controllers
{
    public class SucursalesController : Controller
    {

        //Variables -----------------------------------------------------
        string mensaje = "";

        Usuario usuLog = new Usuario();
        Sucursal unaSucursal = new Sucursal();
        List<Sucursal> listaSucursales = new List<Sucursal>();



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
        public ActionResult FormSucursalesListar(string mensaje = "", string mensajeError = "")
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

                listaSucursales = new SucursalesAPI().GetSucursalesXUsuario(usuLog, usuLog);

                //Ingreso mensaje
                if (mensaje != "")
                {
                    ViewBag.MensajeAccion = mensaje;
                }
                else if (mensajeError != "")
                {
                    ViewBag.MensajeError = mensajeError;
                }


                if (listaSucursales.Count > 0)
                {
                    HttpContext.Session.SetObjectAsJson("Sucursales", listaSucursales);

                    ViewBag.Mensaje = "Tiene " + listaSucursales.Count + " Sucursales activas.";

                    return View(listaSucursales);
                }
                else
                {
                    ViewBag.Mensaje = "No hay Sucursales para mostrar";
                    return View(new List<Sucursal>());
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
                return View(new List<Sucursal>());
            }
        }



        [HttpGet]
        public ActionResult FormSucursalModificar(int nroSucursal)
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

                listaSucursales = HttpContext.Session.GetObjectFromJson<List<Sucursal>>("Sucursales");

                unaSucursal = listaSucursales.FirstOrDefault(r => r.NroSucursal == nroSucursal);

                HttpContext.Session.SetObjectAsJson("SubredesSucursalSeleccionada", unaSucursal.ListaSubred);
                HttpContext.Session.SetObjectAsJson("SucursalSeleccionada", unaSucursal);

                return View(unaSucursal);
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
                return RedirectToAction("FormSucursalesListar", "Sucursales", new { mensajeError = ex.Message });
            }
        }



        [HttpPost]
        public ActionResult FormSucursalModificar(Sucursal unaSucursal, string listaSubredes)
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


                if (listaSubredes == null || listaSubredes.Count() <= 0)
                {
                    unaSucursal.ListaSubred = null;
                }
                else
                {
                    List<Subred> subredesPreSeleccion = HttpContext.Session.GetObjectFromJson<List<Subred>>("SubredesSucursalSeleccionada");

                    //Separo string de Subredes
                    string[] arregloSubredes = listaSubredes.Split(',').Select(s => s.Trim()).ToArray();

                    //La inicializo porque si uso Add a null se rompe
                    unaSucursal.ListaSubred = new List<Subred>();

                    foreach (string unaSubred in arregloSubredes)
                    {
                        unaSucursal.ListaSubred.Add(new Subred(unaSubred));
                    }
                }


                unaSucursal.Validar();

                new SucursalesAPI().PutSucursal((Administrador)usuLog, unaSucursal);

                //MensajeAccion
                mensaje = "Sucursal " + unaSucursal.NroSucursal.ToString() + " modificada con exito.";

                return RedirectToAction("FormSucursalesListar", "Sucursales", new { mensaje = mensaje });
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

                unaSucursal = HttpContext.Session.GetObjectFromJson<Sucursal>("SucursalSeleccionada");
                return View(unaSucursal);
            }
        }



        [HttpGet]
        public ActionResult FormSucursalEliminar(int nroSucursal)
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

                listaSucursales = HttpContext.Session.GetObjectFromJson<List<Sucursal>>("Sucursales");

                //Ver que hago sino lo encuentro, no deberia pasar, pero por las dudas...
                unaSucursal = listaSucursales.FirstOrDefault(r => r.NroSucursal == nroSucursal);

                HttpContext.Session.SetObjectAsJson("SucursalSeleccionada", unaSucursal);

                return View(unaSucursal);
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
                return RedirectToAction("FormSucursalesListar", "Sucursales", new { mensajeError = ex.Message });
            }
        }



        [HttpPost]
        public ActionResult FormSucursalEliminar(Sucursal unaSucursal)
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

                unaSucursal = HttpContext.Session.GetObjectFromJson<Sucursal>("SucursalSeleccionada");

                new SucursalesAPI().DeleteSucursal((Administrador)usuLog, unaSucursal);

                //MensajeAccion
                mensaje = "Sucursal " + unaSucursal.NroSucursal.ToString() + " eliminada con exito.";

                return RedirectToAction("FormSucursalesListar", "Sucursales", new { mensaje = mensaje });
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
                return View(unaSucursal);
            }
        }



        [HttpGet]
        public ActionResult FormSucursalAgregar()
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
        public ActionResult FormSucursalAgregar(Sucursal unaSucursal, string listaSubredes)
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


                if (listaSubredes == null)
                {
                    unaSucursal.ListaSubred = null;
                }
                else
                {
                    //Separo string de Subredes
                    string[] arregloSubredes = listaSubredes.Split(',');

                    unaSucursal.ListaSubred = new List<Subred>();
                    List<Subred> listaSubredesActivas = new SubredesAPI().GetSubredes((Administrador)usuLog);

                    foreach (string unaSubred in arregloSubredes)
                    {
                        if (listaSubredesActivas.Any(x => x.Rango == unaSubred))
                        {
                            ViewBag.Mensaje = "Ya hay una Subred con ese valor asignada a una Sucursal (" + unaSubred + ").";
                            return View();
                        }

                        unaSucursal.ListaSubred.Add(new Subred(unaSubred));
                    }

                }


                unaSucursal.Validar();

                new SucursalesAPI().PostSucursal((Administrador)usuLog, unaSucursal);

                //MensajeAccion
                mensaje = "Sucursal " + unaSucursal.NroSucursal.ToString() + " agregada con exito.";

                return RedirectToAction("FormSucursalesListar", "Sucursales", new { mensaje = mensaje });
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
                return View(unaSucursal);
            }
        }



    }


}
