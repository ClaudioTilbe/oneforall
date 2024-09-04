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
    internal class PersistenciaSubred : IPersistenciaSubred
    {

        //Singleton
        private static PersistenciaSubred _instancia = null;
        private PersistenciaSubred() { }
        public static PersistenciaSubred GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaSubred();
            return _instancia;
        }



        internal void Alta(Subred unaSubred, int pNumeroSucursal, SqlTransaction _pTransaccion)
        {
            SqlCommand _comando = new SqlCommand("SubredAlta", _pTransaccion.Connection);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@NumeroSucursal", pNumeroSucursal);
            _comando.Parameters.AddWithValue("@Rango", unaSubred.Rango);

            SqlParameter _ParmRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _ParmRetorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_ParmRetorno);


            try
            {
                _comando.Transaction = _pTransaccion;

                _comando.ExecuteNonQuery();

                int retorno = Convert.ToInt32(_ParmRetorno.Value);
                if (retorno == -1)
                    throw new Exception("No existe una sucursal con ese numero de sucursal");
                else if (retorno == -2)
                    throw new Exception("Ya existe una subred con ese rango");
                else if (retorno == -3)
                    throw new Exception("Fallo al intentar agregar la subred");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public void BajaXRango(int nroSucursal, string rango, SqlTransaction _pTransaccion) 
        {

            SqlCommand _comando = new SqlCommand("SubredBaja", _pTransaccion.Connection);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@NumeroSucursal", nroSucursal);
            _comando.Parameters.AddWithValue("@rango", rango);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _comando.Transaction = _pTransaccion;

                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe una subred con ese rango asignado a ese numero de sucursal");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Fallo al intentar eliminar los analisis de red asociados a ese rango");
                else if ((int)_retorno.Value == -3)
                    throw new Exception("Fallo al intentar eliminar la subred");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        //Metodo de uso interno, no se expone en IPersistenciaSubred
        internal List<Subred> ListarSubredes(int pNumeroSucursal, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("ListadoSubredXSucursal", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@NumeroSucursal", pNumeroSucursal);

            List<Subred> _ListaTels = new List<Subred>();

            try
            {
                _cnn.Open();

                SqlDataReader _lector = _comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        _ListaTels.Add(new Subred((string)_lector["Rango"]));
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

            return _ListaTels;
        }



        internal void BajaSubredesXSucursal(int nroSucursal, SqlTransaction _pTransaccion)
        {
            SqlCommand _comando = new SqlCommand("SubredBajaXSucursal", _pTransaccion.Connection);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@NumeroSucursal", nroSucursal);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);


            try
            {
                _comando.Transaction = _pTransaccion;

                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe una subred con ese rango asignado a ese numero de sucursal");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Fallo al intentar eliminar la subredes asignadas a la sucursal");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public List<Subred> ListadoSubredes(Administrador pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            List<Subred> _lista = new List<Subred>();
            Subred subred = new Subred();

            SqlCommand _comando = new SqlCommand("ListadoSubredes", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        subred = new Subred((string)_lector["Rango"]);

                        _lista.Add(subred);
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

