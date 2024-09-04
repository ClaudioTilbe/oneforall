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
    internal class PersistenciaAnalisisRed : IPersistenciaAnalisisRed
    {

        //Singleton
        private static PersistenciaAnalisisRed _instancia = null;
        private PersistenciaAnalisisRed() { }
        public static PersistenciaAnalisisRed GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaAnalisisRed();
            return _instancia;
        }



        //Operaciones
        public void Alta(AnalisisRed unAnalisis, Administrador pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("AnalisisRedAlta", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@RangoSubred", unAnalisis.SubredAnalizada);
            _comando.Parameters.AddWithValue("@Razon", unAnalisis.Razon);
            _comando.Parameters.AddWithValue("@Estado", unAnalisis.Estado);
            _comando.Parameters.AddWithValue("@Prioridad", unAnalisis.Prioridad);
            _comando.Parameters.AddWithValue("@NuevosDispositivos", unAnalisis.NuevosDispositivos);
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
                    throw new Exception("No existe una Subred con ese Rango");
                else if ((int)_ParmRetorno.Value == -3)
                    throw new Exception("Fallo al intentar crear el Analisis de Red");

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



        public void Actualizar(AnalisisRed unAnalisis, Administrador pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("AnalisisRedActualizar", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@IdAnalisis", unAnalisis.IDAnalisis);
            _comando.Parameters.AddWithValue("@Estado", unAnalisis.Estado);
            _comando.Parameters.AddWithValue("@NuevosDispositivos", unAnalisis.NuevosDispositivos);
            _comando.Parameters.AddWithValue("@FechaFinalizado", unAnalisis.FechaFinalizado);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe un Analisis de red con ese ID");
                else if ((int)_retorno.Value == -2)
                {
                    MensajeMotor mensaje = new MensajeMotor();

                    mensaje.Excepcion = "Excepcion controlada";
                    mensaje.Mensaje = "El analisis de red a sido cancelado previamente";
                    mensaje.MetodoOrigen = "ProcesosAnalisisSolicitados";
                    mensaje.EstadoVariables = unAnalisis.IDAnalisis.ToString();
                    mensaje.FechaGenerado = DateTime.Now;
                    mensaje.Tipo = "Informativo";

                    PersistenciaMensajeMotor.GetInstancia().Alta(mensaje, pUsuLogueado);
                }
                else if ((int)_retorno.Value == -3)
                    throw new Exception("Error al intentar actualizar el Analisis de red");

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



        public void Cancelar(AnalisisRed unAnalisis, Administrador pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("AnalisisRedCancelar", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@IdAnalisis", unAnalisis.IDAnalisis);
            _comando.Parameters.AddWithValue("@FechaFinalizado", DateTime.Now);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();

                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe Analisis de Red con ese ID");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Error al intentar cancelar el Analisis de Red");

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



        public AnalisisRed Buscar(int analisisID, Administrador pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            AnalisisRed unAnalisis = null;

            SqlCommand _comando = new SqlCommand("AnalisisBuscar", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@IdAnalisis", analisisID);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    _lector.Read();
                    unAnalisis = new AnalisisRed
                        (analisisID,
                        (string)_lector["Razon"],
                        (string)_lector["Estado"],
                        (string)_lector["Prioridad"],
                        (int)_lector["NuevosDispositivos"],
                        (DateTime)_lector["FechaGenerado"],
                        //(DateTime)_lector["FechaFinalizado"],
                        _lector["FechaFinalizado"] != DBNull.Value ? (DateTime?)_lector["FechaFinalizado"] : null,
                        (string)_lector["RangoSubred"],
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
            return unAnalisis;
        }



        public List<AnalisisRed> ListadoAnalisisRedTodos(Administrador pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            AnalisisRed unAnalisis = null;
            List<AnalisisRed> _lista = new List<AnalisisRed>();

            SqlCommand _comando = new SqlCommand("ListadoAnalisisRedTodos", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        unAnalisis = new AnalisisRed
                            ((int)_lector["IdAnalisis"],
                            (string)_lector["Razon"],
                            (string)_lector["Estado"],
                            (string)_lector["Prioridad"],
                            (int)_lector["NuevosDispositivos"],
                            (DateTime)_lector["FechaGenerado"],
                            //(DateTime)_lector["FechaFinalizado"],
                            _lector["FechaFinalizado"] != DBNull.Value ? (DateTime?)_lector["FechaFinalizado"] : null,
                            (string)_lector["RangoSubred"],
                            PersistenciaAdministrador.GetInstancia().Buscar((string)_lector["UsuarioID"], pUsuLogueado)
                            );

                        _lista.Add(unAnalisis);
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



        public List<AnalisisRed> ListadoAnalisisRedPendientes(Administrador pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            AnalisisRed unAnalisis = null;
            List<AnalisisRed> _lista = new List<AnalisisRed>();

            SqlCommand _comando = new SqlCommand("ListadoAnalisisRedPendientes", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        unAnalisis = new AnalisisRed
                            ((int)_lector["IdAnalisis"],
                            (string)_lector["Razon"],
                            (string)_lector["Estado"],
                            (string)_lector["Prioridad"],
                            (int)_lector["NuevosDispositivos"],
                            (DateTime)_lector["FechaGenerado"],
                            //(DateTime)_lector["FechaFinalizado"],
                            _lector["FechaFinalizado"] != DBNull.Value ? (DateTime?)_lector["FechaFinalizado"] : null,
                            (string)_lector["RangoSubred"],
                            PersistenciaAdministrador.GetInstancia().Buscar((string)_lector["UsuarioID"], pUsuLogueado)
                            );

                        _lista.Add(unAnalisis);
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
