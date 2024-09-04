using Persistencia.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCompartidas;

namespace Persistencia.ClasesTrabajo
{
    internal class PersistenciaEscaneoPuertos : IPersistenciaEscaneoPuertos
    {

        //Singleton
        private static PersistenciaEscaneoPuertos _instancia = null;
        private PersistenciaEscaneoPuertos() { }
        public static PersistenciaEscaneoPuertos GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaEscaneoPuertos();
            return _instancia;
        }



        //Operaciones
        public void Alta(EscaneoPuertos unEscaneo, Administrador pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("EscaneoPuertosAlta", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@DispositivoIP", unEscaneo.DispositivoObjetivo.IP);
            _comando.Parameters.AddWithValue("@Razon", unEscaneo.Razon);
            _comando.Parameters.AddWithValue("@Estado", unEscaneo.Estado);
            _comando.Parameters.AddWithValue("@Prioridad", unEscaneo.Prioridad);
            _comando.Parameters.AddWithValue("@CadenaSalida", unEscaneo.CadenaSalida);
            _comando.Parameters.AddWithValue("@UsuarioID", pUsuLogueado.UsuarioID);
            _comando.Parameters.AddWithValue("@FechaGenerado", DateTime.Now);

            SqlParameter _ParmRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _ParmRetorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_ParmRetorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_ParmRetorno.Value == -1)
                    throw new Exception("No existe un Administrador con ese ID");
                else if ((int)_ParmRetorno.Value == -2)
                    throw new Exception("No existe un dispositivo con esa IP");
                else if ((int)_ParmRetorno.Value == -2)
                    throw new Exception("Fallo al intentar generar el escaneo de puertos");

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



        public void Actualizar(EscaneoPuertos unEscaneo, Administrador pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("EscaneoPuertosActualizar", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@IdEscaneo", unEscaneo.IDEscaneo);
            _comando.Parameters.AddWithValue("@Estado", unEscaneo.Estado);
            _comando.Parameters.AddWithValue("@CadenaSalida", unEscaneo.CadenaSalida);
            _comando.Parameters.AddWithValue("@FechaFinalizado", DateTime.Now);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe un escaneo de puertos con ese ID");
                else if ((int)_retorno.Value == -2)
                {
                    MensajeMotor mensaje = new MensajeMotor();

                    mensaje.Excepcion = "Excepcion controlada";
                    mensaje.Mensaje = "El escaneo de puertos a sido cancelado previamente";
                    mensaje.MetodoOrigen = "ProcesosEscaneosPuertos";
                    mensaje.EstadoVariables = unEscaneo.IDEscaneo.ToString();
                    mensaje.FechaGenerado = DateTime.Now;
                    mensaje.Tipo = "Informativo";

                    PersistenciaMensajeMotor.GetInstancia().Alta(mensaje, pUsuLogueado);
                }
                else if ((int)_retorno.Value == -3)
                    throw new Exception("Error al intentar actualizar el escaneo de puertos");

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



        public void Cancelar(EscaneoPuertos unEscaneo, Administrador pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("EscaneoPuertosCancelar", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@IdEscaneo", unEscaneo.IDEscaneo);
            _comando.Parameters.AddWithValue("@FechaFinalizado", DateTime.Now);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();

                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe escaneo de puertos con ese ID");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Fallo al intentar cancelar el escaneo de puertos");

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


        public EscaneoPuertos Buscar(int escaneoID, Administrador pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            EscaneoPuertos unEscaneo = null;

            SqlCommand _comando = new SqlCommand("EscaneoPuertosBuscar", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@IdEscaneo", escaneoID);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    _lector.Read();
                    unEscaneo = new EscaneoPuertos
                        (escaneoID,
                        PersistenciaDispositivo.GetInstancia().Buscar((string)_lector["DispositivoIP"], pUsuLogueado),
                        (string)_lector["Razon"],
                        (string)_lector["Estado"],
                        (string)_lector["Prioridad"],
                        (string)_lector["CadenaSalida"],
                        (DateTime)_lector["FechaGenerado"],
                        //(DateTime)_lector["FechaFinalizado"],
                        _lector["FechaFinalizado"] != DBNull.Value ? (DateTime?)_lector["FechaFinalizado"] : null,
                        PersistenciaAdministrador.GetInstancia().Buscar((string)_lector["UsuarioID"], pUsuLogueado)
                        );
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
            return unEscaneo;
        }



        public List<EscaneoPuertos> ListadoEscaneoPuertosTodos(Administrador pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            EscaneoPuertos unEscaneo = null;
            List<EscaneoPuertos> _lista = new List<EscaneoPuertos>();

            SqlCommand _comando = new SqlCommand("ListadoEscaneoPuertosTodos", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        unEscaneo = new EscaneoPuertos
                                    ((int)_lector["IdEscaneo"],
                                    PersistenciaDispositivo.GetInstancia().Buscar((string)_lector["DispositivoIP"], pUsuLogueado),
                                    (string)_lector["Razon"],
                                    (string)_lector["Estado"],
                                    (string)_lector["Prioridad"],
                                    (string)_lector["CadenaSalida"],
                                    (DateTime)_lector["FechaGenerado"],
                                    //(DateTime)_lector["FechaFinalizado"],
                                    _lector["FechaFinalizado"] != DBNull.Value ? (DateTime?)_lector["FechaFinalizado"] : null,
                                    PersistenciaAdministrador.GetInstancia().Buscar((string)_lector["UsuarioID"], pUsuLogueado)
                                    );

                        _lista.Add(unEscaneo);
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



        public List<EscaneoPuertos> ListadoEscaneoPuertosPendientes(Administrador pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            EscaneoPuertos unEscaneo = null;
            List<EscaneoPuertos> _lista = new List<EscaneoPuertos>();

            SqlCommand _comando = new SqlCommand("ListadoEscaneoPuertosPendientes", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        unEscaneo = new EscaneoPuertos
                                        ((int)_lector["IdEscaneo"],
                                        PersistenciaDispositivo.GetInstancia().Buscar((string)_lector["DispositivoIP"], pUsuLogueado),
                                        (string)_lector["Razon"],
                                        (string)_lector["Estado"],
                                        (string)_lector["Prioridad"],
                                        (string)_lector["CadenaSalida"],
                                        (DateTime)_lector["FechaGenerado"],
                                        //(DateTime)_lector["FechaFinalizado"],
                                        _lector["FechaFinalizado"] != DBNull.Value ? (DateTime?)_lector["FechaFinalizado"] : null,
                                        PersistenciaAdministrador.GetInstancia().Buscar((string)_lector["UsuarioID"], pUsuLogueado)
                                        );

                        _lista.Add(unEscaneo);
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
