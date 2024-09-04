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
    internal class PersistenciaMail : IPersistenciaMail
    {
        //singleton
        private static PersistenciaMail _instancia = null;
        private PersistenciaMail() { }
        public static PersistenciaMail GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaMail();
            return _instancia;
        }



        public void Modificar(Mail unMail, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("MailModificar", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Correo", unMail.Correo);
            _comando.Parameters.AddWithValue("@Contraseña", unMail.Contraseña);
            _comando.Parameters.AddWithValue("@HostServidor", unMail.HostServidor);
            _comando.Parameters.AddWithValue("@Puerto", unMail.Puerto);


            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe un mail con ese correo");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Fallo al intentar modificar el Mail");

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



        public void BajaXCorreo(string correo, SqlTransaction _pTransaccion)
        {

            SqlCommand _comando = new SqlCommand("MailBaja", _pTransaccion.Connection);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Correo", correo);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _comando.Transaction = _pTransaccion;

                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe mail con ese correo");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Error al intentar eliminar los reportes asociados al Mail");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Error al intentar eliminar el mail");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public Mail Buscar(string pCorreo, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            Mail unMail = null;

            SqlCommand _comando = new SqlCommand("MailBuscar", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Correo", pCorreo);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();

                if (_lector.HasRows)
                {
                    _lector.Read();

                    unMail = new Mail
                        (pCorreo,
                        (string)_lector["Contraseña"],
                        (string)_lector["HostServidor"],
                        (int)_lector["Puerto"]);
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
            return unMail;
        }



        public List<Mail> ListarMails(Administrador pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            Mail unMail = null;
            List<Mail> _listaMails = new List<Mail>();

            SqlCommand _comando = new SqlCommand("ListadoMails", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        unMail = new Mail
                            ((string)_lector["Correo"],
                            (string)_lector["Contraseña"],
                            (string)_lector["HostServidor"],
                             (int)_lector["Puerto"]);

                        _listaMails.Add(unMail);
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

            return _listaMails;
        }





    }


}

