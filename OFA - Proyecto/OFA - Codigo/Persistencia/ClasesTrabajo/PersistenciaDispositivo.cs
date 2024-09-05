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
    internal class PersistenciaDispositivo : IPersistenciaDispositivo
    {

        //Singleton
        private static PersistenciaDispositivo _instancia = null;
        private PersistenciaDispositivo() { }
        public static PersistenciaDispositivo GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaDispositivo();
            return _instancia;
        }



        //Operaciones
        public void Alta(Dispositivo unDispositivo, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("DispositivoAlta", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@IP", unDispositivo.IP);
            _comando.Parameters.AddWithValue("@Nombre", unDispositivo.Nombre);
            _comando.Parameters.AddWithValue("@Tipo", unDispositivo.Tipo);
            _comando.Parameters.AddWithValue("@Conectado", unDispositivo.Conectado);
            _comando.Parameters.AddWithValue("@Accesible", unDispositivo.Accesible);
            _comando.Parameters.AddWithValue("@Sector", unDispositivo.Sector);
            _comando.Parameters.AddWithValue("@NroSucursal", unDispositivo.UbSucursal.NroSucursal);

            _comando.Parameters.AddWithValue("@Prioridad", unDispositivo.Prioridad);
            _comando.Parameters.AddWithValue("@Permanencia", unDispositivo.Permanencia);
            _comando.Parameters.AddWithValue("@UltimaConexion", unDispositivo.Ultimaconexion);
            _comando.Parameters.AddWithValue("@UltimaNotificacion", unDispositivo.Ultimaconexion);

            SqlParameter _ParmRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _ParmRetorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_ParmRetorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_ParmRetorno.Value == -1)
                    throw new Exception("Ya Existe un Dispositivo con esa IP");
                else if ((int)_ParmRetorno.Value == -2)
                    throw new Exception("Fallo al intentar dar de alta el dispositivo");

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



        public void Baja(string dispositivoIP, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("DispositivoBaja", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@IP", dispositivoIP);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);


            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe dispositivo con esa IP");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Fallo al intentar eliminar los mensajes visor de ese dispositivo");
                else if ((int)_retorno.Value == -3)
                    throw new Exception("Fallo al intentar eliminar los escaneos de puertos de ese dispositivo");
                else if ((int)_retorno.Value == -4)
                    throw new Exception("Fallo al intentar eliminar los reportes de ese dispositivo");
                else if ((int)_retorno.Value == -5)
                    throw new Exception("Fallo al intentar retirar el dispositivo de los grupos");
                else if ((int)_retorno.Value == -6)
                    throw new Exception("Fallo al intentar eliminar el dispositivo");
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



        public void Modificar(Dispositivo unDispositivo, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("DispositivoModificar", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@IP", unDispositivo.IP);
            _comando.Parameters.AddWithValue("@Nombre", unDispositivo.Nombre);
            _comando.Parameters.AddWithValue("@Tipo", unDispositivo.Tipo);
            _comando.Parameters.AddWithValue("@Conectado", unDispositivo.Conectado);
            _comando.Parameters.AddWithValue("@Accesible", unDispositivo.Accesible);
            _comando.Parameters.AddWithValue("@Sector", unDispositivo.Sector);
            _comando.Parameters.AddWithValue("@NroSucursal", unDispositivo.UbSucursal.NroSucursal);

            _comando.Parameters.AddWithValue("@Prioridad", unDispositivo.Prioridad);
            _comando.Parameters.AddWithValue("@Permanencia", unDispositivo.Permanencia);
            //_comando.Parameters.AddWithValue("@UltimaConexion", unDispositivo.Ultimaconexion);
            //_comando.Parameters.AddWithValue("@UltimaNotificacion", unDispositivo.UltimaNotificacion);


            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe un dispositivo con esa IP");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("No existe una sucursal con ese numero de sucursal");
                else if ((int)_retorno.Value == -3)
                    throw new Exception("Fallo al intentar modificar los datos del dispositivo");

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



        public Dispositivo Buscar(string pIP, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            Dispositivo _unDispositivo = null;

            SqlCommand _comando = new SqlCommand("DispositivoBuscar", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@IP", pIP);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    _lector.Read();
                    _unDispositivo = new Dispositivo
                        (pIP,
                        (string)_lector["Nombre"],
                        (string)_lector["Tipo"],
                        (bool)_lector["Conectado"],
                        (bool)_lector["Accesible"],
                        (string)_lector["Sector"],
                        (string)_lector["Prioridad"],
                        (bool)_lector["Permanencia"],
                        (DateTime)_lector["UltimaConexion"],
                        (DateTime)_lector["UltimaNotificacion"],
                        PersistenciaSucursal.GetInstancia().Buscar((int)_lector["NumeroSucursal"], pUsuLogueado));
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
            return _unDispositivo;
        }



        public void ActualizarEstadoConexion(Dispositivo unDispositivo, Administrador pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("DispositivoActualizarEstadoConexion", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@IP", unDispositivo.IP);
            _comando.Parameters.AddWithValue("@Conectado", unDispositivo.Conectado);
            _comando.Parameters.AddWithValue("@UltimaConexion", unDispositivo.Ultimaconexion);
            //_comando.Parameters.AddWithValue("@UltimaNotificacion", unDispositivo.UltimaNotificacion);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe un dispositivo con esa IP");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Fallo al intentar actualizar el estado del dispositivo");

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



        public void ActualizarEstadoNotificacion(Dispositivo unDispositivo, Administrador pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("DispositivoActualizarEstadoNotificacion", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@IP", unDispositivo.IP);
            _comando.Parameters.AddWithValue("@Conectado", unDispositivo.Conectado);
            //_comando.Parameters.AddWithValue("@UltimaConexion", unDispositivo.Ultimaconexion);
            _comando.Parameters.AddWithValue("@UltimaNotificacion", unDispositivo.UltimaNotificacion);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe un dispositivo con esa IP");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Fallo al intentar actualizar el estado del Dispositivo");

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



        public List<Dispositivo> ListadoTodos(Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            Dispositivo _unDispositivo = null;
            List<Dispositivo> _lista = new List<Dispositivo>();

            SqlCommand _comando = new SqlCommand("ListadoDispositivos", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {

                        _unDispositivo = new Dispositivo
                            ((string)_lector["IP"],
                            (string)_lector["Nombre"],
                            (string)_lector["Tipo"],
                            (bool)_lector["Conectado"],
                            (bool)_lector["Accesible"],
                            (string)_lector["Sector"],
                            (string)_lector["Prioridad"],
                            (bool)_lector["Permanencia"],
                            (DateTime)_lector["UltimaConexion"],
                            (DateTime)_lector["UltimaNotificacion"],
                            PersistenciaSucursal.GetInstancia().Buscar((int)_lector["NumeroSucursal"], pUsuLogueado)
                            //nroSucursal != -1 ? PersistenciaSucursal.GetInstancia().Buscar(nroSucursal, pUsuLogueado) : null
                            );

                        _lista.Add(_unDispositivo);
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
                //SqlConnection.ClearAllPools();
                SqlConnection.ClearPool(_cnn);
            }

            return _lista;
        }



        public List<Dispositivo> ListadoDispositivosXGrupo(int codigoGrupo, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            Dispositivo _unDispositivo = null;
            List<Dispositivo> _lista = new List<Dispositivo>();

            SqlCommand _comando = new SqlCommand("ListadoDispositivosXGrupo", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@CodigoGrupo", codigoGrupo);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        _unDispositivo = new Dispositivo
                            ((string)_lector["IP"],
                            (string)_lector["Nombre"],
                            (string)_lector["Tipo"],
                            (bool)_lector["Conectado"],
                            (bool)_lector["Accesible"],
                            (string)_lector["Sector"],
                            (string)_lector["Prioridad"],
                            (bool)_lector["Permanencia"],
                            (DateTime)_lector["UltimaConexion"],
                            (DateTime)_lector["UltimaNotificacion"],
                            PersistenciaSucursal.GetInstancia().Buscar((int)_lector["NumeroSucursal"], pUsuLogueado)
                            );

                        _lista.Add(_unDispositivo);
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
                //SqlConnection.ClearAllPools();
                SqlConnection.ClearPool(_cnn);
            }

            return _lista;
        }



        public List<Dispositivo> ListadoDispositivosXSubred(string subred, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            Dispositivo _unDispositivo = null;
            List<Dispositivo> _lista = new List<Dispositivo>();

            SqlCommand _comando = new SqlCommand("ListadoDispositivosXSubred", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Subred", subred);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        _unDispositivo = new Dispositivo
                            ((string)_lector["IP"],
                            (string)_lector["Nombre"],
                            (string)_lector["Tipo"],
                            (bool)_lector["Conectado"],
                            (bool)_lector["Accesible"],
                            (string)_lector["Sector"],
                            (string)_lector["Prioridad"],
                            (bool)_lector["Permanencia"],
                            (DateTime)_lector["UltimaConexion"],
                            (DateTime)_lector["UltimaNotificacion"],
                            PersistenciaSucursal.GetInstancia().Buscar((int)_lector["NumeroSucursal"], pUsuLogueado)
                            );

                        _lista.Add(_unDispositivo);
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
                //SqlConnection.ClearAllPools();
                SqlConnection.ClearPool(_cnn);
            }

            return _lista;
        }



        public List<Dispositivo> ListadoDispositivosXSucursal(Sucursal unaSucursal, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            Dispositivo _unDispositivo = null;
            List<Dispositivo> _lista = new List<Dispositivo>();

            SqlCommand _comando = new SqlCommand("ListadoDispositivosXSucursal", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@NumeroSucursal", unaSucursal.NroSucursal);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        _unDispositivo = new Dispositivo
                            ((string)_lector["IP"],
                            (string)_lector["Nombre"],
                            (string)_lector["Tipo"],
                            (bool)_lector["Conectado"],
                            (bool)_lector["Accesible"],
                            (string)_lector["Sector"],
                            (string)_lector["Prioridad"],
                            (bool)_lector["Permanencia"],
                            (DateTime)_lector["UltimaConexion"],
                            (DateTime)_lector["UltimaNotificacion"],
                            PersistenciaSucursal.GetInstancia().Buscar((int)_lector["NumeroSucursal"], pUsuLogueado)
                            );

                        _lista.Add(_unDispositivo);
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
                //SqlConnection.ClearAllPools();
                SqlConnection.ClearPool(_cnn);
            }

            return _lista;
        }







    }


}

