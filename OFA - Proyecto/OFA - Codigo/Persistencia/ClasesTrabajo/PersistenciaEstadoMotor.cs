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
    internal class PersistenciaEstadoMotor : IPersistenciaEstadoMotor
    {

        //Singleton
        private static PersistenciaEstadoMotor _instancia = null;
        private PersistenciaEstadoMotor() { }
        public static PersistenciaEstadoMotor GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaEstadoMotor();
            return _instancia;
        }



        //Operaciones
        public void Alta(EstadoMotor estadoMotor, Administrador pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("EstadoMotorAlta", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Activo", estadoMotor.Activo);
            _comando.Parameters.AddWithValue("@UltimaModificacion", estadoMotor.UltimaModificacion);
            _comando.Parameters.AddWithValue("@UsuarioID", pUsuLogueado.UsuarioID);

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
                    throw new Exception("Fallo al intentar dar de alta el estado motor");

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



        public EstadoMotor BuscarUltimo(Administrador pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            EstadoMotor unEstadoMotor = null;

            SqlCommand _comando = new SqlCommand("BuscarUltimoEstadoMotor", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    _lector.Read();
                    unEstadoMotor = new EstadoMotor
                        ((int)_lector["IDEstado"],
                        (bool)_lector["Activo"],
                        (DateTime)_lector["UltimaModificacion"],
                        (string)_lector["UsuarioID"]);
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
            return unEstadoMotor;
        }


    }
}

