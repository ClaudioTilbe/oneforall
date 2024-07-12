using EntidadesCompartidas;
using Persistencia.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.ClasesTrabajo
{
    internal class PersistenciaReporte : IPersistenciaReporte
    {

        //Singleton
        private static PersistenciaReporte _instancia = null;
        private PersistenciaReporte() { }
        public static PersistenciaReporte GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaReporte();
            return _instancia;
        }



        //Operaciones
        public void Alta(Reporte unReporte, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("ReporteAlta", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Correo", unReporte.MailOrigen.Correo);
            _comando.Parameters.AddWithValue("@DispositivoIP", unReporte.DispositivoAsociado.IP);
            _comando.Parameters.AddWithValue("@Asunto", unReporte.Asunto);
            _comando.Parameters.AddWithValue("@Destino", unReporte.Destino);
            _comando.Parameters.AddWithValue("@Mensaje", unReporte.Mensaje);

            SqlParameter _ParmRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _ParmRetorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_ParmRetorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_ParmRetorno.Value == -1)
                    throw new Exception("Ya existe un reporte asociado entre ese correo y dispositivo");
                else if ((int)_ParmRetorno.Value == -2)
                    throw new Exception("No existe un mail con ese correo");
                else if ((int)_ParmRetorno.Value == -3)
                    throw new Exception("No existe un dispositivo con esa IP");
                else if ((int)_ParmRetorno.Value == -3)
                    throw new Exception("Fallo al intentar crear el reporte");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
        }



        public void Baja(Reporte unReporte, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("ReporteBaja", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Codigo", unReporte.Codigo);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();
                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe un reporte con ese codigo");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Fallo al intentar eliminar el reporte");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
        }


        public void Modificar(Reporte unReporte, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("ReporteModificar", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Codigo", unReporte.Codigo);
            _comando.Parameters.AddWithValue("@Correo", unReporte.MailOrigen.Correo);
            _comando.Parameters.AddWithValue("@DispositivoIP", unReporte.DispositivoAsociado.IP);
            _comando.Parameters.AddWithValue("@Asunto", unReporte.Asunto);
            _comando.Parameters.AddWithValue("@Destino", unReporte.Destino);
            _comando.Parameters.AddWithValue("@Mensaje", unReporte.Mensaje);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe un reporte con ese codigo");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("No existe un Dispositivo con esa IP");
                else if ((int)_retorno.Value == -3)
                    throw new Exception("No existe un Mail con ese correo asociado al usuario");
                else if ((int)_retorno.Value == -4)
                    throw new Exception("Fallo al intentar modificar los datos del reporte");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
        }


        public Reporte Buscar(Mail pMail, Dispositivo pDispositivo, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            Reporte unReporte = null;

            SqlCommand _comando = new SqlCommand("ReporteBuscar", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Correo", pMail.Correo);
            _comando.Parameters.AddWithValue("@DispositivoIP", pDispositivo.IP);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    _lector.Read();
                    unReporte = new Reporte
                        ((int)_lector["Codigo"],
                        pMail,
                        pDispositivo,
                        (string)_lector["Asunto"],
                        (string)_lector["Destino"],
                        (string)_lector["Mensaje"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
            return unReporte;
        }



        public List<Reporte> ListadoReportesXCorreo(Mail unMail, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            Reporte unReporte = null;
            List<Reporte> _lista = new List<Reporte>();

            SqlCommand _comando = new SqlCommand("ListadoReportesXCorreo", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Correo", unMail.Correo);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        unReporte = new Reporte
                            ((int)_lector["Codigo"],
                            unMail,
                            FabricaPersistencia.GetPersistenciaDispositivo().Buscar((string)_lector["DispositivoIP"], pUsuLogueado),
                            (string)_lector["Asunto"],
                            (string)_lector["Destino"],
                            (string)_lector["Mensaje"]);

                        _lista.Add(unReporte);
                    }
                }
                _lector.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
            return _lista;
        }



        public List<Reporte> ListadoReportesXIP(Dispositivo unDispositivo, Administrador adminLog)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(adminLog));
            Reporte unReporte = null;
            List<Reporte> _lista = new List<Reporte>();

            SqlCommand _comando = new SqlCommand("ListadoReportesXIP", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@IP", unDispositivo.IP);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        unReporte = new Reporte
                            ((int)_lector["Codigo"],
                            FabricaPersistencia.GetPersistenciaMail().Buscar((string)_lector["Correo"], adminLog),
                            unDispositivo,
                            (string)_lector["Asunto"],
                            (string)_lector["Destino"],
                            (string)_lector["Mensaje"]);

                        _lista.Add(unReporte);
                    }
                }
                _lector.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Codigo EX12: " + ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
            return _lista;
        }



        public List<Reporte> ListadoReportesTodos(Administrador adminLog)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(adminLog));
            Reporte unReporte = null;
            List<Reporte> _lista = new List<Reporte>();

            SqlCommand _comando = new SqlCommand("ListadoReportesTodos", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        unReporte = new Reporte
                            ((int)_lector["Codigo"],
                            FabricaPersistencia.GetPersistenciaMail().Buscar((string)_lector["Correo"], adminLog),
                            FabricaPersistencia.GetPersistenciaDispositivo().Buscar((string)_lector["DispositivoIP"], adminLog),
                            (string)_lector["Asunto"],
                            (string)_lector["Destino"],
                            (string)_lector["Mensaje"]);

                        _lista.Add(unReporte);
                    }
                }
                _lector.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
            return _lista;
        }


    }


}

