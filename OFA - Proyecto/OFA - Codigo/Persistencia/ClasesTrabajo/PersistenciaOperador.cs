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
    internal class PersistenciaOperador : IPersistenciaOperador
    {

        //singleton
        private static PersistenciaOperador _instancia = null;
        private PersistenciaOperador() { }
        public static PersistenciaOperador GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaOperador();
            return _instancia;
        }
        


        public Operador Logueo(string pUsuID, string pContra)
        {

            SqlConnection _cnn = new SqlConnection(Conexion.Cnn());
            Operador _unOperador = null;

            SqlCommand _comando = new SqlCommand("LogueoOperador", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@UsuID", pUsuID);
            _comando.Parameters.AddWithValue("@Contraseña", pContra);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();

                    _unOperador = new Operador(pUsuID,
                        pContra,
                        new Mail((string)_lector["Correo"], (string)_lector["ContraseñaMail"], (string)_lector["HostServidor"], (int)_lector["Puerto"]),
                        (int)_lector["NumeroFuncionarioSupervisor"],
                        (string)_lector["NombreSupervisor"],
                        new List<Sucursal>());

                    if (_unOperador != null)
                    {
                        _unOperador.SucursalesAsignadas = PersistenciaSucursal.GetInstancia().ListarSucursalesXUsuario(_unOperador, _unOperador);
                    }
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
            return _unOperador;
        }



        public void Alta(Operador unOperador, Administrador pAdmLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pAdmLogueado));

            SqlCommand _comando = new SqlCommand("OperadorAlta", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@UsuID", unOperador.UsuarioID);
            _comando.Parameters.AddWithValue("@Contraseña", unOperador.Contraseña);
            _comando.Parameters.AddWithValue("@Correo", unOperador.MailAsignado.Correo);
            _comando.Parameters.AddWithValue("@NumeroFuncionarioSupervisor", unOperador.NumeroFuncionarioSupervisor);
            _comando.Parameters.AddWithValue("@NombreSupervisor", unOperador.NombreSupervisor);

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
                    throw new Exception("Ya existe un usuario con ese ID");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Ya existe un usuario con ese correo asociado");
                else if ((int)_retorno.Value == -3)
                    throw new Exception("Fallo al intentar dar de alta el mail asignado al usuario");
                else if ((int)_retorno.Value == -4)
                    throw new Exception("Fallo al intentar crear el usuario a nivel de Servidor");
                else if ((int)_retorno.Value == -5)
                    throw new Exception("Fallo al intentar crear el usuario a nivel de BD");
                else if ((int)_retorno.Value == -6)
                    throw new Exception("Fallo al intentar crear el usuario");
                else if ((int)_retorno.Value == -7)
                    throw new Exception("Fallo al intentar crear el operador");
                else if ((int)_retorno.Value == -8)
                    throw new Exception("Fallo al intentar asignar los roles al usuario");

                if (unOperador.SucursalesAsignadas != null && unOperador.SucursalesAsignadas.Count() > 0)
                {
                    foreach (Sucursal unaSucursal in unOperador.SucursalesAsignadas)
                    {
                        PersistenciaSucursal.GetInstancia().AsignarSucursalAOperador(unOperador.UsuarioID, unaSucursal.NroSucursal, _miTransaccion);
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



        public void Baja(Operador unOperador, Administrador pAdmLogueado)
        {
            SqlConnection _cnnActual = new SqlConnection(Conexion.Cnn(pAdmLogueado));

            SqlCommand _comando = new SqlCommand("OperadorBaja", _cnnActual);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@UsuID", unOperador.UsuarioID);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            SqlTransaction _miTransaccion = null;

            //Cierro conexiones de ese usuario
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unOperador));
            SqlConnection.ClearPool(_cnn);

            try
            {
                _cnnActual.Open();

                _miTransaccion = _cnnActual.BeginTransaction();

                _comando.Transaction = _miTransaccion;

                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe un Usuario con ese ID");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Fallo al intentar eliminar el usuario a nivel de Servidor");
                else if ((int)_retorno.Value == -3)
                    throw new Exception("Fallo al intentar eliminar el usuario a nivel de BD");
                else if ((int)_retorno.Value == -4)
                    throw new Exception("Fallo al intentar desasociar las sucursales del operador");
                else if ((int)_retorno.Value == -5)
                    throw new Exception("Fallo al intentar eliminar el operador");
                else if ((int)_retorno.Value == -6)
                    throw new Exception("Fallo al intentar eliminar el usuario");

                PersistenciaMail.GetInstancia().BajaXCorreo(unOperador.MailAsignado.Correo, _miTransaccion);

                _miTransaccion.Commit();
            }
            catch (Exception ex)
            {
                _miTransaccion.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnnActual.Close();
            }
        }



        public void Modificar(Operador unOperador, Administrador pAdmLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pAdmLogueado));

            SqlCommand _comando = new SqlCommand("OperadorModificar", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@UsuarioID", unOperador.UsuarioID);
            _comando.Parameters.AddWithValue("@Contraseña", unOperador.Contraseña);
            _comando.Parameters.AddWithValue("@Correo", unOperador.MailAsignado.Correo);
            _comando.Parameters.AddWithValue("@NumeroFuncionarioSupervisor", unOperador.NumeroFuncionarioSupervisor);
            _comando.Parameters.AddWithValue("@NombreSupervisor", unOperador.NombreSupervisor);

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
                    throw new Exception("No existe un operador con ese ID");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Fallo al intentar modificar los datos de operador a nivel de Servidor");
                else if ((int)_retorno.Value == -3)
                    throw new Exception("Fallo al intentar modificar los datos del usuario");
                else if ((int)_retorno.Value == -4)
                    throw new Exception("Fallo al intentar modificar los datos del operador");

                PersistenciaSucursal.GetInstancia().BajaSucursalesOperador(unOperador, _miTransaccion);

                if (unOperador.SucursalesAsignadas != null && unOperador.SucursalesAsignadas.Count() > 0)
                {
                    for (int i = 0; i < unOperador.SucursalesAsignadas.Count; i++)
                    {
                        PersistenciaSucursal.GetInstancia().AsignarSucursalAOperador(unOperador.UsuarioID, unOperador.SucursalesAsignadas[i].NroSucursal, _miTransaccion);
                    }
                }

                _miTransaccion.Commit();
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


        public Operador Buscar(string pUsu, Usuario pAdmLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pAdmLogueado));
            Operador _unOperador = null;

            SqlCommand _comando = new SqlCommand("BuscarOperador", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@UsuID", pUsu);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                    _unOperador = new Operador(pUsu,
                        (string)_lector["Contraseña"],
                        PersistenciaMail.GetInstancia().Buscar((string)_lector["Correo"], pAdmLogueado),
                        (int)_lector["NumeroFuncionarioSupervisor"],
                        (string)_lector["NombreSupervisor"],
                        null);

                    if (_unOperador != null)
                    {
                        _unOperador.SucursalesAsignadas = PersistenciaSucursal.GetInstancia().ListarSucursalesXUsuario(_unOperador, pAdmLogueado);
                    }

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
            return _unOperador;
        }


        public List<Operador> ListadoOperadores(Administrador pAdmLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pAdmLogueado));
            Operador _unOperador = null;
            List<Operador> _lista = new List<Operador>();

            SqlCommand _comando = new SqlCommand("ListadoOperadores", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        _unOperador = new Operador((string)_lector["UsuarioID"],
                            (string)_lector["Contraseña"],
                            PersistenciaMail.GetInstancia().Buscar((string)_lector["Correo"], pAdmLogueado),
                            (int)_lector["NumeroFuncionarioSupervisor"],
                            (string)_lector["NombreSupervisor"],
                            null);

                        if (_unOperador != null)
                        {
                            _unOperador.SucursalesAsignadas = PersistenciaSucursal.GetInstancia().ListarSucursalesXUsuario(_unOperador, pAdmLogueado);
                        }

                        _lista.Add(_unOperador);
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

