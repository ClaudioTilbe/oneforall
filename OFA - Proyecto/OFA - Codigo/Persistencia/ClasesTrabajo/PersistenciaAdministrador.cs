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
    internal class PersistenciaAdministrador : IPersistenciaAdministrador
    {

        //singleton
        private static PersistenciaAdministrador _instancia = null;
        private PersistenciaAdministrador() { }
        public static PersistenciaAdministrador GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaAdministrador();
            return _instancia;
        }




        public Administrador Logueo(string pUsuID, string pContra)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn());
            Administrador _unAdministrador = null;

            SqlCommand _comando = new SqlCommand("LogueoAdministrador", _cnn);
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

                    _unAdministrador = new Administrador(pUsuID,
                        pContra,
                        new Mail((string)_lector["Correo"], (string)_lector["ContraseñaMail"], (string)_lector["HostServidor"], (int)_lector["Puerto"]),
                        (string)_lector["Nombre"],
                        (int)_lector["NumeroFuncionario"],
                        (string)_lector["Cargo"]);

                    //Si no traia mail directo y lo hacia de esta manera me dejaba una conexion abierta y luego de un
                    //Logueo no podia eliminar el usuario, quedaba la conexion colgada
                    //_unAdministrador.MailAsignado = PersistenciaMail.GetInstancia().Buscar((string)_lector["Correo"], _unAdministrador);
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
            return _unAdministrador;
        }



        public void Alta(Administrador unAdministrador, Administrador pAdmLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pAdmLogueado));

            SqlCommand _comando = new SqlCommand("AdministradorAlta", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@UsuID", unAdministrador.UsuarioID);
            _comando.Parameters.AddWithValue("@Contraseña", unAdministrador.Contraseña);
            _comando.Parameters.AddWithValue("@Correo", unAdministrador.MailAsignado.Correo);
            _comando.Parameters.AddWithValue("@Nombre", unAdministrador.Nombre);
            _comando.Parameters.AddWithValue("@NumeroFuncionario", unAdministrador.NumeroFuncionario);
            _comando.Parameters.AddWithValue("@Cargo", unAdministrador.Cargo);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                //Revisar
                if ((int)_retorno.Value == -1)
                    throw new Exception("Ya existe un Usuario con ese ID");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Ya existe un Usuario con ese Correo asignado");
                else if ((int)_retorno.Value == -3)
                    throw new Exception("Fallo al intentar crear el Mail para el Usuario");
                else if ((int)_retorno.Value == -4)
                    throw new Exception("Fallo al intentar crear el usuario a nivel de Servidor");
                else if ((int)_retorno.Value == -5)
                    throw new Exception("Fallo al intentar crear el usuario a nivel de BD");
                else if ((int)_retorno.Value == -6)
                    throw new Exception("Fallo al intentar crear el Usuario");
                else if ((int)_retorno.Value == -7)
                    throw new Exception("Fallo al intentar crear el Administrador");
                else if ((int)_retorno.Value == -8)
                    throw new Exception("Fallo al intentar otorgar roles al Usuario");
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


        public void Baja(Administrador unAdministrador, Administrador pAdmLogueado)
        {
            SqlConnection _cnnActual = new SqlConnection(Conexion.Cnn(pAdmLogueado));

            SqlCommand _comando = new SqlCommand("AdministradorBaja", _cnnActual);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@UsuID", unAdministrador.UsuarioID);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            SqlTransaction _miTransaccion = null;//Va por fuera del try para poder acceder a try y catch


            //Cierro conexiones de ese usuario
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(unAdministrador));
            SqlConnection.ClearPool(_cnn);

            try
            {
                _cnnActual.Open();

                //Determino que voy a trabajar en una unica transaccion
                _miTransaccion = _cnnActual.BeginTransaction();

                //Ejecuto comando de alta del cliente en la transaccion
                _comando.Transaction = _miTransaccion;

                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe un Administrador con ese ID");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Fallo al intentar eliminar el usuario a nivel de Servidor");
                else if ((int)_retorno.Value == -3)
                    throw new Exception("Fallo al intentar eliminar el usuario a nivel de BD");
                else if ((int)_retorno.Value == -4)
                    throw new Exception("Fallo al intentar eliminar los Analisis de red asociados al Administrador");
                else if ((int)_retorno.Value == -5)
                    throw new Exception("Fallo al intentar eliminar los Escaneos de puertos asociados al Administrador");
                else if ((int)_retorno.Value == -6)
                    throw new Exception("Fallo al intentar desasignar los dispositivos en grupos");
                else if ((int)_retorno.Value == -7)
                    throw new Exception("Fallo al intentar eliminar los grupos");
                else if ((int)_retorno.Value == -8)
                    throw new Exception("Fallo al intentar eliminar el Administrador");
                else if ((int)_retorno.Value == -9)
                    throw new Exception("Fallo al intentar eliminar el Usuario");


                PersistenciaMail.GetInstancia().BajaXCorreo(unAdministrador.MailAsignado.Correo, _miTransaccion);

                //Si llegue aca es porque no hubo problemas con los telefonos
                _miTransaccion.Commit();//Guarda todos los datos fisicamente en la base de datos
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



        public void Modificar(Administrador unAdministrador, Administrador pAdmLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pAdmLogueado));

            SqlCommand _comando = new SqlCommand("AdministradorModificar", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@UsuarioID", unAdministrador.UsuarioID);
            _comando.Parameters.AddWithValue("@Contraseña", unAdministrador.Contraseña);
            _comando.Parameters.AddWithValue("@Correo", unAdministrador.MailAsignado.Correo);
            _comando.Parameters.AddWithValue("@Nombre", unAdministrador.Nombre);
            _comando.Parameters.AddWithValue("@NumeroFuncionario", unAdministrador.NumeroFuncionario);
            _comando.Parameters.AddWithValue("@Cargo", unAdministrador.Cargo);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe un Administrador con ese ID");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Fallo al intentar modificar los datos del Administrador");
                else if ((int)_retorno.Value == -3)
                    throw new Exception("Fallo al intentar modificar contraseña a nivel de Servidor");
                else if ((int)_retorno.Value == -4)
                    throw new Exception("Fallo al intentar modificar contraseña de Usuario");
                else if ((int)_retorno.Value == -4)
                    throw new Exception("Fallo al intentar modificar los datos de Administrador");

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



        public Administrador Buscar(string pUsu, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            Administrador _unAdministrador = null;

            SqlCommand _comando = new SqlCommand("BuscarAdministrador", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@UsuID", pUsu);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                    _unAdministrador = new Administrador(pUsu,
                        (string)_lector["Contraseña"],
                        PersistenciaMail.GetInstancia().Buscar((string)_lector["Correo"], pUsuLogueado),
                        (string)_lector["Nombre"],
                        (int)_lector["NumeroFuncionario"],
                        (string)_lector["Cargo"]);
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
            return _unAdministrador;
        }



        public List<Administrador> ListadoAdministradores(Administrador pAdmLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pAdmLogueado));
            Administrador _unAdministrador = null;
            List<Administrador> _lista = new List<Administrador>();

            SqlCommand _comando = new SqlCommand("ListadoAdministradores", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        _unAdministrador = new Administrador((string)_lector["UsuarioID"],
                            (string)_lector["Contraseña"],
                            PersistenciaMail.GetInstancia().Buscar((string)_lector["Correo"], pAdmLogueado),
                            (string)_lector["Nombre"],
                            (int)_lector["NumeroFuncionario"],
                            (string)_lector["Cargo"]);

                        _lista.Add(_unAdministrador);
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
