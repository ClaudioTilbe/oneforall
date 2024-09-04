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
    internal class PersistenciaDiagramaRed : IPersistenciaDiagramaRed
    {

        //Singleton
        private static PersistenciaDiagramaRed _instancia = null;
        private PersistenciaDiagramaRed() { }
        public static PersistenciaDiagramaRed GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaDiagramaRed();
            return _instancia;
        }




        //Operaciones
        public void Alta(DiagramaRed unDiagrama, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("DiagramaRedAlta", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@NumeroSucursal", unDiagrama._Sucursal.NroSucursal);
            _comando.Parameters.AddWithValue("@Nombre", unDiagrama.Nombre);
            _comando.Parameters.AddWithValue("@DiagramaImagen", unDiagrama.DiagramaImagen);
            _comando.Parameters.AddWithValue("@FechaSubida", unDiagrama.FechaSubida);

            SqlParameter _ParmRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _ParmRetorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_ParmRetorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_ParmRetorno.Value == -1)
                    throw new Exception("No existe una sucursal con ese Numero de Sucursal");
                else if ((int)_ParmRetorno.Value == -2)
                    throw new Exception("Ya existe un diagrama de red asignado a esa Sucursal");
                else if ((int)_ParmRetorno.Value == -3)
                    throw new Exception("Fallo al intentar crear el Diagrama de Red");

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



        public void Baja(int nroSucursal, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("DiagramaRedBaja", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@NumeroSucursal", nroSucursal);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();
                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe un Diagrama de red asignado a ese numero de sucursal");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Fallo al intentar eliminar el diagrama de red");

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



        public DiagramaRed Buscar(int nroSucursal, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            DiagramaRed unDiagrama = null;

            SqlCommand _comando = new SqlCommand("DiagramaRedBuscar", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@NumeroSucursal", nroSucursal);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    _lector.Read();
                    unDiagrama = new DiagramaRed
                        (PersistenciaSucursal.GetInstancia().Buscar(nroSucursal, pUsuLogueado),
                        (string)_lector["Nombre"],
                        (DateTime)_lector["FechaSubida"],
                        (byte[])_lector["DiagramaImagen"]);
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
            return unDiagrama;
        }



        public List<DiagramaRed> ListadoTodos(Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            DiagramaRed unDiagrama = null;
            List<DiagramaRed> _lista = new List<DiagramaRed>();

            SqlCommand _comando = new SqlCommand("ListarDiagramaRed", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        unDiagrama = new DiagramaRed
                        (PersistenciaSucursal.GetInstancia().Buscar((int)_lector["NumeroSucursal"], pUsuLogueado),
                        (string)_lector["Nombre"],
                        (DateTime)_lector["FechaSubida"],
                        (byte[])_lector["DiagramaImagen"]);

                        _lista.Add(unDiagrama);
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
