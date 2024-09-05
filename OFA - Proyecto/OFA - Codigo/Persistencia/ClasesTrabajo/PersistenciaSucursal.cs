using EntidadesCompartidas;
using Persistencia.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.ClasesTrabajo
{
    internal class PersistenciaSucursal : IPersistenciaSucursal
    {
        //Singleton
        private static PersistenciaSucursal _instancia = null;
        private PersistenciaSucursal() { }
        public static PersistenciaSucursal GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaSucursal();
            return _instancia;
        }

        

        //Operaciones
        public void Alta(Sucursal unaSucursal, Administrador pAdmLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pAdmLogueado));

            SqlCommand _comando = new SqlCommand("SucursalAlta", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@NroSucursal", unaSucursal.NroSucursal);
            _comando.Parameters.AddWithValue("@Tipo", unaSucursal.Tipo);
            _comando.Parameters.AddWithValue("@Departamento", unaSucursal.Departamento);
            _comando.Parameters.AddWithValue("@Calle", unaSucursal.Calle);
            _comando.Parameters.AddWithValue("@NumeroLocal", unaSucursal.NumeroLocal);

            SqlParameter _ParmRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _ParmRetorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_ParmRetorno);

            SqlTransaction _miTransaccion = null;

            try
            {
                _cnn.Open();

                _miTransaccion = _cnn.BeginTransaction();

                _comando.Transaction = _miTransaccion;
                _comando.ExecuteNonQuery(); 

                if ((int)_ParmRetorno.Value == -1)
                    throw new Exception("Ya Existe una sucursal con ese numero de sucursal");
                else if ((int)_ParmRetorno.Value == -2)
                    throw new Exception("Fallo al intentar agregar la sucursal");


                if (unaSucursal.ListaSubred != null && unaSucursal.ListaSubred.Count > 0)
                {
                    foreach (Subred unaSubred in unaSucursal.ListaSubred)
                    {
                        PersistenciaSubred.GetInstancia().Alta(unaSubred, unaSucursal.NroSucursal, _miTransaccion);
                    }
                }

                _miTransaccion.Commit();

            }
            catch (Exception ex)
            {
                _miTransaccion.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
        }



        internal void AsignarSucursalAOperador(string UsuarioID, int pNumeroSucursal, SqlTransaction _pTransaccion)
        {
            SqlCommand _comando = new SqlCommand("AsignarSucursalAOperador", _pTransaccion.Connection);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@NroSucursal", pNumeroSucursal);
            _comando.Parameters.AddWithValue("@UsuarioID", UsuarioID);

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
                    throw new Exception("La sucursal ya esta asignada al usuario");
                else if (retorno == -3)
                    throw new Exception("Fallo al intentar asignar la sucursal al usuario");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }



        public void BajaSucursalesOperador(Operador unOperador, SqlTransaction _pTransaccion)
        {

            SqlCommand _comando = new SqlCommand("BajaSucursalesOperador", _pTransaccion.Connection);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@UsuarioID", unOperador.UsuarioID);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _comando.Transaction = _pTransaccion;

                _comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public void Baja(Sucursal unaSucursal, Administrador pAdmLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pAdmLogueado));

            SqlCommand _comando = new SqlCommand("SucursalBaja", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@NroSucursal", unaSucursal.NroSucursal);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            SqlTransaction _miTransaccion = null;

            try
            {
                _cnn.Open();

                _miTransaccion = _cnn.BeginTransaction();

                _comando.Transaction = _miTransaccion;

                for (int i = 0; unaSucursal.ListaSubred.Count > i; i++)
                {
                    PersistenciaSubred.GetInstancia().BajaXRango(unaSucursal.NroSucursal,
                                                      unaSucursal.ListaSubred[i].Rango, _miTransaccion);
                }

                _comando.ExecuteNonQuery(); 

                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe una sucursal con ese numero de sucursal");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Fallo al intentar eliminar el diagrama de red asociado");
                else if ((int)_retorno.Value == -3)
                    throw new Exception("Fallo al intentar eliminar grupos con dispositivos que esten relacionados a la sucursal");
                else if ((int)_retorno.Value == -4)
                    throw new Exception("Fallo al intentar eliminar reportes con dispositivos que esten relacionados a la sucursal");
                else if ((int)_retorno.Value == -5)
                    throw new Exception("Fallo al intentar eliminar escaneo de puertos de dispositivos que esten relacionados a la sucursal");
                else if ((int)_retorno.Value == -6)
                    throw new Exception("Fallo al intentar eliminar mensajes visor de dispositivos que esten relacionados a la sucursal");
                else if ((int)_retorno.Value == -7)
                    throw new Exception("Fallo al intentar eliminar dispositivos asociados a la sucursal");
                else if ((int)_retorno.Value == -8)
                    throw new Exception("Fallo al intentar desasociar la sucursal de los operadores");
                else if ((int)_retorno.Value == -9)
                    throw new Exception("Fallo al intentar eliminar la sucursal");


                _miTransaccion.Commit();
            }
            catch (Exception ex)
            {
                _miTransaccion.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
        }



        public void Modificar(Sucursal unaSucursal, Administrador pAdmLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pAdmLogueado));

            SqlCommand _comando = new SqlCommand("SucursalModificar", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@NroSucursal", unaSucursal.NroSucursal);
            _comando.Parameters.AddWithValue("@Tipo", unaSucursal.Tipo);
            _comando.Parameters.AddWithValue("@Departamento", unaSucursal.Departamento);
            _comando.Parameters.AddWithValue("@Calle", unaSucursal.Calle);
            _comando.Parameters.AddWithValue("@NumeroLocal", unaSucursal.NumeroLocal);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            SqlTransaction _miTransaccion = null;

            try
            {
                _cnn.Open();

                _miTransaccion = _cnn.BeginTransaction();

                _comando.Transaction = _miTransaccion;


                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe una sucursal con ese numero de sucursal");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Fallo al intentar modificar los datos de sucursal");

                PersistenciaSubred.GetInstancia().BajaSubredesXSucursal(unaSucursal.NroSucursal, _miTransaccion);

                if (unaSucursal.ListaSubred != null && unaSucursal.ListaSubred.Count > 0)
                {
                    foreach (Subred subNueva in unaSucursal.ListaSubred)
                    {
                         PersistenciaSubred.GetInstancia().Alta(subNueva, unaSucursal.NroSucursal, _miTransaccion);
                    }
                }

                _miTransaccion.Commit();
            }
            catch (Exception ex)
            {
                _miTransaccion.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
        }



        public Sucursal Buscar(int pNroSucursal, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            Sucursal unaSucursal = null;

            SqlCommand _comando = new SqlCommand("SucursalBuscar", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@NroSucursal", pNroSucursal);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    _lector.Read();
                    unaSucursal = new Sucursal(pNroSucursal, (string)_lector["Tipo"], (string)_lector["Calle"],
                        (string)_lector["Departamento"], (int)_lector["NumeroLocal"],
                        PersistenciaSubred.GetInstancia().ListarSubredes(pNroSucursal, pUsuLogueado));
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
            return unaSucursal;
        }



        public Sucursal BuscarXRango(string rango, Administrador adminLog)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(adminLog));
            Sucursal unaSucursal = null;

            SqlCommand _comando = new SqlCommand("SucursalBuscarXRango", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Rango", rango);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    _lector.Read();
                    unaSucursal = new Sucursal(
                                    (int)_lector["NumeroSucursal"],
                                    (string)_lector["Tipo"],
                                    (string)_lector["Departamento"],
                                    (string)_lector["Calle"],
                                    (int)_lector["NumeroLocal"],
                                    PersistenciaSubred.GetInstancia().ListarSubredes((int)_lector["NumeroSucursal"], adminLog));
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
            return unaSucursal;
        }



        public List<Sucursal> ListarSucursales(Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            Sucursal unaSucursal = null;
            List<Sucursal> _lista = new List<Sucursal>();

            SqlCommand _comando = new SqlCommand("ListadoSucursales", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        unaSucursal = new Sucursal
                            ((int)_lector["NumeroSucursal"],
                            (string)_lector["Tipo"],
                            (string)_lector["Departamento"],
                            (string)_lector["Calle"],
                            (int)_lector["NumeroLocal"],
                            PersistenciaSubred.GetInstancia().ListarSubredes((int)_lector["NumeroSucursal"], pUsuLogueado));

                        _lista.Add(unaSucursal);
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



        public List<Sucursal> ListarSucursalesXUsuario(Usuario pUsu, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            Sucursal unaSucursal = null;
            List<Sucursal> _lista = new List<Sucursal>();

            SqlCommand _comando = new SqlCommand("ListadoSucursalesXUsuario", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@UsuarioID", pUsu.UsuarioID);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        unaSucursal = new Sucursal
                            ((int)_lector["NumeroSucursal"],
                            (string)_lector["Tipo"],
                            (string)_lector["Departamento"],
                            (string)_lector["Calle"],
                            (int)_lector["NumeroLocal"],
                            PersistenciaSubred.GetInstancia().ListarSubredes((int)_lector["NumeroSucursal"], pUsuLogueado));

                        _lista.Add(unaSucursal);
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



        public List<Subred> ListarSubredesExistentes(Administrador pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            List<Subred> listadoSubredes = new List<Subred>();
            Subred unaSubred = new Subred();

            SqlCommand _comando = new SqlCommand("ListadoSubredesExistentes", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        unaSubred = new Subred
                            ((string)_lector["Rango"]);

                        listadoSubredes.Add(unaSubred);
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

            return listadoSubredes;
        }



    }


}

