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
    internal class PersistenciaMensajeMotor : IPersistenciaMensajeMotor
    {


        //singleton
        private static PersistenciaMensajeMotor _instancia = null;
        private PersistenciaMensajeMotor() { }
        public static PersistenciaMensajeMotor GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaMensajeMotor();
            return _instancia;
        }



        //Operaciones
        public void Alta(MensajeMotor unMensaje, Administrador adminLog)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(adminLog));

            SqlCommand _comando = new SqlCommand("MensajeMotorAlta", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Excepcion", unMensaje.Excepcion);
            _comando.Parameters.AddWithValue("@Mensaje", unMensaje.Mensaje);
            _comando.Parameters.AddWithValue("@MetodoOrigen", unMensaje.MetodoOrigen);
            _comando.Parameters.AddWithValue("@EstadoVariables", unMensaje.EstadoVariables);
            _comando.Parameters.AddWithValue("@FechaGenerado", unMensaje.FechaGenerado);
            _comando.Parameters.AddWithValue("@Tipo", unMensaje.Tipo);

            SqlParameter _ParmRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _ParmRetorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_ParmRetorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_ParmRetorno.Value == -1)
                    throw new Exception("Fallo al intentar generar el MensajeMotor");

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



        public void Baja(MensajeMotor unMensaje, Administrador adminLog)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(adminLog));

            SqlCommand _comando = new SqlCommand("MensajeMotorBaja", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@IdMensaje", unMensaje.IDMensaje);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();
                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe un MensajeMotor con ese ID");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Fallo al intentar eliminar MensajeMotor");

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



        public List<MensajeMotor> ListarXTodos(Administrador adminLog)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(adminLog));
            MensajeMotor unMensaje = null;
            List<MensajeMotor> _lista = new List<MensajeMotor>();

            SqlCommand _comando = new SqlCommand("MensajeMotorListarTodos", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        unMensaje = new MensajeMotor
                            ((int)_lector["IdMensaje"],
                            (string)_lector["Excepcion"],
                            (string)_lector["Mensaje"],
                            (string)_lector["MetodoOrigen"],
                            (string)_lector["EstadoVariables"],
                            (DateTime)_lector["FechaGenerado"],
                            (string)_lector["Tipo"]);

                        _lista.Add(unMensaje);
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
