using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sitio.Models;
using System.Collections.Generic;
using System;


//Agregado (SelectList)
using System.Collections;
using System.Linq;
using System.IO;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Office2010.ExcelAc;

namespace Sitio.Controllers
{
    public class DispositivosController : Controller
    {

        //Variables -----------------------------------------------------
        string mensaje = "";

        Usuario usuLog = new Usuario();
        Dispositivo unDispositivo = new Dispositivo();
        List<Sucursal> sucursalesDisponibles = new List<Sucursal>();
        List<Dispositivo> listaDispFiltrada = new List<Dispositivo>();
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
                        sucursalesDisponibles = new SucursalesAPI().GetSucursalesXUsuario(usuLog, usuLog);
                    }
                    else if (usuLog.Tipo == "Operador")
                    {
                        usuLog = HttpContext.Session.GetObjectFromJson<Operador>("UsuarioLog");
                        sucursalesDisponibles = ((Operador)usuLog).SucursalesAsignadas;
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
        public ActionResult FormDispositivosListar(string mensaje = "", string mensajeError = "")
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

                listaDispositivos = new DispositivosAPI().GetDispositivosXUsuario(usuLog);

                if (listaDispositivos != null && listaDispositivos.Count > 0)
                {
                    listaDispositivos = new Dispositivo().OrdenoIPs(listaDispositivos); 
                }

                //Datos para llenar el DropDownList de Grupos disponibles
                List<Grupo> listaGrupos = new GruposAPI().GetGruposXUsuario(usuLog);
                HttpContext.Session.SetObjectAsJson("listaGrupos", listaGrupos);

                //Ingreso mensaje
                if (mensaje != "")
                {
                    ViewBag.MensajeAccion = mensaje;
                }
                else if (mensajeError != "")
                {
                    ViewBag.MensajeError = mensajeError;
                }


                if (listaDispositivos.Count > 0)//si hay algo para mostrar
                {
                    ViewBag.Mensaje = "Tiene " + listaDispositivos.Count + " Dispositivos activos.";

                    HttpContext.Session.SetObjectAsJson("listaDispositivos", listaDispositivos);

                    ViewBag.Grupos = new SelectList(listaGrupos);
                    return View(listaDispositivos);
                }
                else
                {
                    ViewBag.Mensaje = "No hay Dispositivos para mostrar";
                    ViewBag.Grupos = new SelectList(new List<Grupo>());
                    return View(new List<Dispositivo>());
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
                ViewBag.Grupos = new SelectList(new List<Grupo>());
                return View(new List<Dispositivo>());
            }
        }



        public ActionResult FiltrarDispositivos(string grupo)
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

                List<Grupo> listaGrupos = HttpContext.Session.GetObjectFromJson<List<Grupo>>("listaGrupos");

                if (grupo == "Todos")
                {
                    listaDispositivos = new DispositivosAPI().GetDispositivosXUsuario(usuLog);
                    listaDispositivos = new Dispositivo().OrdenoIPs(listaDispositivos);
                }
                else
                {
                    // Utilizamos una expresión regular para buscar el primer número en la cadena
                    Regex regex = new Regex(@"\d+");
                    Match match = regex.Match(grupo);

                    int codigoGrupo = Convert.ToInt32(match.Value);

                    Grupo grupoSeleccionado = listaGrupos.FirstOrDefault(grupo => grupo.Codigo == codigoGrupo);
                    listaDispositivos = grupoSeleccionado.Dispositivos;
                    listaDispositivos = new Dispositivo().OrdenoIPs(listaDispositivos);
                }

                HttpContext.Session.SetObjectAsJson("listaDispositivos", listaDispositivos);

                ViewBag.Grupos = new SelectList(listaGrupos);

                return PartialView("_TablaDispositivosPartial", listaDispositivos);
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
                ViewBag.Grupos = new SelectList(new List<Grupo>());
                return PartialView(new List<Dispositivo>());
            }
        }




        //Metodo utilizado por Panel de Estado
        [HttpGet]
        public ActionResult FormDispositivosListarXEstado(string categoria)
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

                listaDispositivos = new DispositivosAPI().GetDispositivosXUsuario(usuLog);

                //Conectados ---------------------------------
                if (categoria == "Conectados")
                {
                    listaDispFiltrada = (from unDisp in listaDispositivos
                                         where unDisp.Conectado == true
                                         select unDisp).ToList<Dispositivo>();
                }

                //Criticos --------------------------------------
                else if (categoria == "Criticos")
                {
                    listaDispFiltrada = (from unDisp in listaDispositivos
                                         where unDisp.Conectado == false && unDisp.Permanencia == true
                                         select unDisp).ToList<Dispositivo>();
                }

                //Alerta ----------------------------------------
                else if (categoria == "Alerta")
                {
                    listaDispFiltrada = (from unDisp in listaDispositivos
                                         where unDisp.Conectado == false && unDisp.Permanencia == false
                                         select unDisp).ToList<Dispositivo>();
                }

                //Desconocidos -------------------------------
                else if (categoria == "Desconocidos")
                {
                    listaDispFiltrada = (from unDisp in listaDispositivos
                                         where unDisp.Nombre == "Desconocido"
                                         select unDisp).ToList<Dispositivo>();
                }

                //Todos -----------------------------------
                else if (categoria == "Todos")
                {
                    listaDispFiltrada = listaDispositivos;
                }

                ViewBag.Categoria = categoria;

                if (listaDispFiltrada.Count > 0)//si hay algo para mostrar
                {
                    ViewBag.Mensaje = "Tiene " + listaDispFiltrada.Count + " Dispositivos activos.";

                    HttpContext.Session.SetObjectAsJson("ListaFiltrada", listaDispFiltrada);

                    return View(listaDispFiltrada);

                }
                else
                {
                    ViewBag.Mensaje = "No hay dispositivos para esta categoria";
                    return View(listaDispFiltrada);
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

                ViewBag.Categoria = categoria;
                ViewBag.Mensaje = ex.Message;
                return View(new List<Dispositivo>());
            }
        }



        [HttpGet]
        public ActionResult FormDispositivoBuscar(string ip)
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

                listaDispositivos = HttpContext.Session.GetObjectFromJson<List<Dispositivo>>("listaDispositivos");

                unDispositivo = listaDispositivos.FirstOrDefault(disp => disp.IP == ip);

                return View(unDispositivo);
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
                return RedirectToAction("FormDispositivosListar", "Dispositivos", new { mensajeError = ex.Message });
            }

        }



        [HttpGet]
        public ActionResult FormDispositivoModificar(string ip)
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

                listaDispositivos = HttpContext.Session.GetObjectFromJson<List<Dispositivo>>("listaDispositivos");

                //Esto se acciona si venimos del Panel de categoria practicamente
                if (listaDispositivos == null || listaDispositivos.Count < 1)
                {
                    listaDispositivos = new DispositivosAPI().GetDispositivosXUsuario(usuLog);
                }

                unDispositivo = listaDispositivos.FirstOrDefault(disp => disp.IP == ip);

                if (unDispositivo == null)
                {
                    return RedirectToAction("FormDispositivosListar", "Dispositivos", new { mensaje = "El dispositivo ya no se encuentra disponible." });
                }

                HttpContext.Session.SetObjectAsJson("DispositivoSeleccionado", unDispositivo);

                ViewBag.Prioridad = new SelectList(new List<string> { "Baja", "Media", "Alta" });

                return View(unDispositivo);
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
                return RedirectToAction("FormDispositivosListar", "Dispositivos", new { mensajeError = ex.Message });
            }
        }



        [HttpPost]
        public ActionResult FormDispositivoModificar(Dispositivo dispositivo)
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


                unDispositivo = HttpContext.Session.GetObjectFromJson<Dispositivo>("DispositivoSeleccionado");

                unDispositivo.Nombre = dispositivo.Nombre;
                unDispositivo.Tipo = dispositivo.Tipo;
                unDispositivo.Sector = dispositivo.Sector;
                unDispositivo.Prioridad = dispositivo.Prioridad;
                unDispositivo.Permanencia = dispositivo.Permanencia;
                unDispositivo.Accesible = dispositivo.Accesible;

                unDispositivo.Validar();

                new DispositivosAPI().PutDispositivo(usuLog, unDispositivo);

                //MensajeAccion
                mensaje = "Dispositivo " + unDispositivo.IP + " modificado con exito.";

                //No hubo error, alta correcto
                return RedirectToAction("FormDispositivosListar", "Dispositivos", new { mensaje = mensaje });
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

                return View(unDispositivo);
            }
        }



        [HttpGet]
        public ActionResult FormDispositivoEliminar(string ip)
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


                listaDispositivos = HttpContext.Session.GetObjectFromJson<List<Dispositivo>>("listaDispositivos");

                unDispositivo = listaDispositivos.FirstOrDefault(disp => disp.IP == ip);

                return View(unDispositivo);
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
                return RedirectToAction("FormDispositivosListar", "Dispositivos", new { mensajeError = ex.Message });
            }
        }



        [HttpPost]
        public ActionResult FormDispositivoEliminar(Dispositivo dispositivo)
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

                new DispositivosAPI().DeleteDispositivo(usuLog, dispositivo);

                //MensajeAccion
                mensaje = "Dispositivo " + dispositivo.IP + " eliminado con exito.";

                return RedirectToAction("FormDispositivosListar", "Dispositivos", new { mensaje = mensaje });
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
                return View(dispositivo);
            }
        }



        [HttpGet]
        public ActionResult FormDispositivoAgregar()
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

                if (sucursalesDisponibles == null || sucursalesDisponibles.Count == 0)
                {
                    //MensajeAccion
                    mensaje = "No hay sucursales asignadas al usuario. Necesita al menos una sucursal asignada para poder agregar un Dispositivo.";

                    return RedirectToAction("FormDispositivosListar", "Dispositivos", new { mensaje = mensaje });
                }

                ViewBag.SucursalesDisp = new SelectList(sucursalesDisponibles);
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
                ViewBag.SucursalesDisp = new SelectList(sucursalesDisponibles);
                ViewBag.Prioridad = new SelectList(new List<string> { "Baja", "Media", "Alta" });

                return View();
            }
        }



        [HttpPost]
        public ActionResult FormDispositivoAgregar(Dispositivo dispositivo)
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

                dispositivo.UbSucursal = sucursalesDisponibles.FirstOrDefault(sucursal => sucursal.NroSucursal == dispositivo.UbSucursal.NroSucursal);

                dispositivo.Validar();

                new DispositivosAPI().PostDispositivo(usuLog, dispositivo);

                //MensajeAccion
                mensaje = "Dispositivo " + dispositivo.IP + " agregado con exito.";

                return RedirectToAction("FormDispositivosListar", "Dispositivos", new { mensaje = mensaje });
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
                ViewBag.Prioridad = new SelectList(new List<string> { "Baja", "Media", "Alta" });

                return View(dispositivo);
            }
        }




        [HttpPost] 
        public ActionResult DescargarListaDispositivos()
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


                listaDispositivos = HttpContext.Session.GetObjectFromJson<List<Dispositivo>>("listaDispositivos");

                if (listaDispositivos == null || listaDispositivos.Count == 0)
                {
                    //MensajeAccion
                    mensaje = "No hay dispositivos en el listado seleccionado.";

                    return RedirectToAction("FormDispositivosListar", "Dispositivos", new { mensajeError = mensaje });
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Listado de Dispositivos");

                    // Doy formato al Titulo ======================================================================
                    // Combina las celdas de A1 a K1
                    worksheet.Range("A1:K1").Merge();

                    var titleCell = worksheet.Cell("A1");

                    //worksheet.Cell("A1").Value = "Listado de Dispositivos - " + DateTime.Now.ToString();
                    titleCell.Value = "Listado de Dispositivos - " + DateTime.Now.ToString();

                    // Aplica formato al título
                    titleCell.Style.Font.Bold = true;
                    titleCell.Style.Font.FontSize = 16;
                    titleCell.Style.Fill.BackgroundColor = XLColor.DodgerBlue;

                    titleCell.Style.Font.FontColor = XLColor.White;

                    // Aplicar formato a las celdas de datos
                    titleCell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    titleCell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    titleCell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    titleCell.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                    titleCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    titleCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    //=============================================================================================

                    // Agregar encabezados
                    int col = 1;
                    int headerRow = 2;

                    foreach (var prop in typeof(Dispositivo).GetProperties())
                    {
                        //worksheet.Cell(2, col).Value = prop.Name;
                        //col++;

                        var cell = worksheet.Cell(headerRow, col);
                        cell.Value = prop.Name;

                        // Aplicar formato a los encabezados
                        cell.Style.Font.Bold = true;
                        cell.Style.Font.FontSize = 12;

                        // Aplicar formato a las celdas de datos
                        cell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        cell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        cell.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                        cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        // Aumentar el alto de la celda
                        cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        cell.Style.Alignment.WrapText = true; // Envolver texto para ajustar el alto

                        col++;
                    }


                    // Agregar datos
                    int row = 3;
                    foreach (var dispositivo in listaDispositivos)
                    {
                        col = 1;
                        foreach (var prop in typeof(Dispositivo).GetProperties())
                        {
                            var cell = worksheet.Cell(row, col);
                            cell.Value = prop.GetValue(dispositivo)?.ToString();

                            // Aplicar formato a las celdas de datos
                            cell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            cell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                            cell.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center; // Centrar horizontalmente
                            cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center; // Centrar verticalmente

                            col++;
                        }
                        row++;
                    }


                    // Ajustar anchos de columna automáticamente
                    for (int i = 1; i <= col; i++)
                    {
                        worksheet.Column(i).AdjustToContents();
                    }


                    // Guardar el libro de trabajo en un flujo de memoria
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        workbook.SaveAs(memoryStream);
                        memoryStream.Position = 0;

                        // Devolver el contenido del archivo como un resultado de archivo
                        return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "OFA - Listado de Dispositivos.xlsx");
                    }

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

                // Manejar errores según tus necesidades
                return Content($"Error: {ex.Message}");
            }
        }







    }


}
