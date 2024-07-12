using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sitio.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Sitio.Controllers
{
    public class OperadoresController : Controller
    {

        //Variables -----------------------------------------------------
        string mensaje = "";

        Usuario usuLog = new Usuario();
        Operador unOperador = new Operador();
        List<Operador> listaOperadores = new List<Operador>();
        List<Mail> mailsDisponibles = new List<Mail>();



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
        public ActionResult FormOperadoresListar(string mensaje = "", string mensajeError = "")
        {
            try
            {
                // Verificacion de Usuario ======================================================================
                VerificoUsuario();

                if (usuLog == null || usuLog.Tipo != "Administrador")
                {
                    return RedirectToAction("Logueo", "Home");
                }
                // ==============================================================================================

                listaOperadores = new UsuariosAPI().GetOperadores((Administrador)usuLog);

                //Ingreso mensaje
                if (mensaje != "")
                {
                    ViewBag.MensajeAccion = mensaje;
                }
                else if (mensajeError != "")
                {
                    ViewBag.MensajeError = mensajeError;
                }


                if (listaOperadores.Count > 0)
                {
                    //Guardo lista de administradores en Session
                    HttpContext.Session.SetObjectAsJson("listaOperadores", listaOperadores);

                    ViewBag.Mensaje = "Tiene " + listaOperadores.Count + " Operadores registrados.";

                    return View(listaOperadores);
                }
                else
                {
                    throw new Exception("No hay Operadores para mostrar");
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
                return View(new List<Operador>());
            }
        }



        public ActionResult FormOperadorVisualizar(string usuarioID)
        {
            try
            {
                // Verificacion de Usuario ======================================================================
                VerificoUsuario();

                if (usuLog == null || usuLog.Tipo != "Administrador")
                {
                    return RedirectToAction("Logueo", "Home");
                }
                // ==============================================================================================

                listaOperadores = HttpContext.Session.GetObjectFromJson<List<Operador>>("listaOperadores");

                unOperador = listaOperadores.FirstOrDefault(admin => admin.UsuarioID == usuarioID);

                return View(unOperador);
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
                return RedirectToAction("FormOperadoresListar", "Operadores", new { mensajeError = ex.Message });
            }
        }



        [HttpGet]
        public ActionResult FormOperadorVisualizarPerfil()
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

                List<Sucursal> listaSucursales = new SucursalesAPI().GetSucursalesXUsuario((Operador)usuLog, (Operador)usuLog);

                ViewBag.ListaSucursales = listaSucursales;

                return View((Operador)usuLog);
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
                return RedirectToAction("FormOperadoresListar", "Operadores", new { mensajeError = ex.Message });
            }
        }




        [HttpGet]
        public ActionResult FormOperadorAgregar()
        {
            try
            {
                // Verificacion de Usuario ======================================================================
                VerificoUsuario();

                if (usuLog == null || usuLog.Tipo != "Administrador")
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
        public ActionResult FormOperadorAgregar(Operador unOperador, string listaSucursales)
        {
            bool tieneSucursales = false;

            string[] numerosArray = new string[0];
            int[] numerosEnteros = new int[0];

            try
            {

                if (listaSucursales == null || listaSucursales.Count() <= 0)
                {
                    unOperador.SucursalesAsignadas = null;
                    tieneSucursales = false;
                }
                else
                {
                    numerosArray = listaSucursales.Split(',').Select(s => s.Trim()).ToArray();
                    numerosEnteros = Array.ConvertAll(numerosArray, s => int.Parse(s.Trim()));
                    tieneSucursales = true;
                }
            }
            catch
            {
                ViewBag.Mensaje = "Ocurrio un error. Debe incluir solo los numeros de sucursal o dejar el campo vacio en las sucursales asignadas.";
                return View(unOperador);
            }


            try
            {
                // Verificacion de Usuario ======================================================================
                VerificoUsuario();

                if (usuLog == null)
                {
                    return RedirectToAction("Logueo", "Home");
                }
                // ==============================================================================================

                unOperador.SucursalesAsignadas = new List<Sucursal>();


                if (tieneSucursales == false)
                {
                    unOperador.SucursalesAsignadas = null;
                }
                else
                {
                    foreach (int numeroSucu in numerosEnteros)
                    {
                        Sucursal unaSucursal = new Sucursal();
                        unaSucursal = new SucursalesAPI().GetSucursal(usuLog, numeroSucu);

                        if (unaSucursal == null)
                        {
                            ViewBag.Mensaje = "No existe ninguna Sucursal con Numero: " + numeroSucu.ToString();
                            return View(unOperador);
                        }

                        unOperador.SucursalesAsignadas.Add(unaSucursal);
                    }
                }

                unOperador.Validar();

                new UsuariosAPI().PostUsuario((Administrador)usuLog, unOperador);

                //MensajeAccion
                mensaje = "Usuario " + unOperador.UsuarioID + " creado con exito.";

                return RedirectToAction("FormOperadoresListar", "Operadores", new { mensaje = mensaje });
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
                return View(unOperador);
            }
        }



        [HttpGet]
        public ActionResult FormOperadorModificar(string usuarioID)
        {
            try
            {
                // Verificacion de Usuario ======================================================================
                VerificoUsuario();

                if (usuLog == null || usuLog.Tipo != "Administrador")
                {
                    return RedirectToAction("Logueo", "Home");
                }
                // ==============================================================================================

                listaOperadores = HttpContext.Session.GetObjectFromJson<List<Operador>>("listaOperadores");

                unOperador = listaOperadores.FirstOrDefault(operador => operador.UsuarioID == usuarioID);

                return View(unOperador);
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
                return RedirectToAction("FormOperadoresListar", "Operadores", new { mensajeError = ex.Message });
            }
        }



        [HttpPost]
        public ActionResult FormOperadorModificar(Operador unOperador, string listaSucursales)
        {
            bool tieneSucursales = false;

            string[] numerosArray = new string[0];
            int[] numerosEnteros = new int[0];

            try
            {

                if (listaSucursales == null || listaSucursales.Count() <= 0)
                {
                    unOperador.SucursalesAsignadas = null;
                    tieneSucursales = false;
                }
                else
                {
                    numerosArray = listaSucursales.Split(',').Select(s => s.Trim()).ToArray();
                    numerosEnteros = Array.ConvertAll(numerosArray, s => int.Parse(s.Trim()));
                    tieneSucursales = true;
                }
            }
            catch
            {
                ViewBag.Mensaje = "Ocurrio un error. Debe incluir solo los numeros de sucursal o dejar el campo vacio en las sucursales asignadas.";
                return View(unOperador);
            }


            try
            {
                // Verificacion de Usuario ======================================================================
                VerificoUsuario();

                if (usuLog == null || usuLog.Tipo != "Administrador")
                {
                    return RedirectToAction("Logueo", "Home");
                }
                // ==============================================================================================

                //Lo traigo para obtener mail
                listaOperadores = HttpContext.Session.GetObjectFromJson<List<Operador>>("listaOperadores");
                Operador opeTemporal = listaOperadores.FirstOrDefault(operador => operador.UsuarioID == unOperador.UsuarioID);
                unOperador.MailAsignado = opeTemporal.MailAsignado;

                unOperador.SucursalesAsignadas = new List<Sucursal>();

                if (tieneSucursales == false)
                {
                    unOperador.SucursalesAsignadas = null;
                }
                else
                {
                    foreach (int numeroSucu in numerosEnteros)
                    {
                        Sucursal unaSucursal = new Sucursal();
                        unaSucursal = new SucursalesAPI().GetSucursal(usuLog, numeroSucu);

                        if (unaSucursal == null)
                        {
                            ViewBag.Mensaje = "No existe ninguna Sucursal con Numero: " + numeroSucu.ToString();
                            return View(unOperador);
                        }

                        unOperador.SucursalesAsignadas.Add(unaSucursal);
                    }
                }

                unOperador.Validar();

                new UsuariosAPI().PutUsuario((Administrador)usuLog, unOperador);

                //MensajeAccion
                mensaje = "Usuario " + unOperador.UsuarioID + " modificado con exito.";

                return RedirectToAction("FormOperadoresListar", "Operadores", new { mensaje = mensaje });
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
                return View(unOperador);
            }
        }



        [HttpGet]
        public ActionResult FormOperadorEliminar(string usuarioID)
        {
            try
            {
                // Verificacion de Usuario ======================================================================
                VerificoUsuario();

                if (usuLog == null || usuLog.Tipo != "Administrador")
                {
                    return RedirectToAction("Logueo", "Home");
                }
                // ==============================================================================================

                listaOperadores = HttpContext.Session.GetObjectFromJson<List<Operador>>("listaOperadores");

                unOperador = listaOperadores.FirstOrDefault(operador => operador.UsuarioID == usuarioID);

                HttpContext.Session.SetObjectAsJson("OperadorSeleccionado", unOperador);

                return View(unOperador);
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
                return RedirectToAction("FormOperadoresListar", "Operadores", new { mensajeError = ex.Message });
            }
        }



        [HttpPost]
        public ActionResult FormOperadorEliminar()
        {
            try
            {
                // Verificacion de Usuario ======================================================================
                VerificoUsuario();

                if (usuLog == null || usuLog.Tipo != "Administrador")
                {
                    return RedirectToAction("Logueo", "Home");
                }
                // ==============================================================================================

                unOperador = HttpContext.Session.GetObjectFromJson<Operador>("OperadorSeleccionado");

                new UsuariosAPI().DeleteUsuario((Administrador)usuLog, unOperador);

                //MensajeAccion
                mensaje = "Operador " + unOperador.UsuarioID + " eliminado con exito.";

                return RedirectToAction("FormOperadoresListar", "Operadores", new { mensaje = mensaje });
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
                return View(unOperador);
            }
        }




    }


}
