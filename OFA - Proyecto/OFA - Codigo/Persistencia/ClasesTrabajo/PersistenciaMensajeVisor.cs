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
    internal class PersistenciaMensajeVisor : IPersistenciaMensajeVisor
    {

        //Singleton
        private static PersistenciaMensajeVisor _instancia = null;
        private PersistenciaMensajeVisor() { }
        public static PersistenciaMensajeVisor GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaMensajeVisor();
            return _instancia;
        }



        //Operaciones
        public void Alta(MensajeVisor unMensaje, Administrador pAdmLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pAdmLogueado));

            SqlCommand _comando = new SqlCommand("MensajeVisorAlta", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@DispositivoIP", unMensaje.DispositivoIP);
            _comando.Parameters.AddWithValue("@FechaGenerado", unMensaje.FechaGenerado);
            _comando.Parameters.AddWithValue("@Contenido", unMensaje.Contenido);
            //_comando.Parameters.AddWithValue("@UsuarioID", unMensaje._Usuario.UsuarioID);

            SqlParameter _ParmRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _ParmRetorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_ParmRetorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                //Revisar Excepciones
                if ((int)_ParmRetorno.Value == -1)
                    throw new Exception("No existe un dispositivo con esa IP");
                else if ((int)_ParmRetorno.Value == -2)
                    throw new Exception("Fallo al intentar agregar el mensaje visor");

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



        public List<MensajeVisor> ListarMensajeVisorXDispositivo(Dispositivo unDispositivo, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            MensajeVisor unMensaje = null;
            List<MensajeVisor> _listaMensajes = new List<MensajeVisor>();

            SqlCommand _comando = new SqlCommand("ListarMensajeVisorXDispositivo", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@DispositivoIP", unDispositivo.IP);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {
                        unMensaje = new MensajeVisor
                            ((int)_lector["Id"],
                            (string)_lector["DispositivoIP"],
                            (DateTime)_lector["FechaGenerado"],
                            (string)_lector["Contenido"]);

                        _listaMensajes.Add(unMensaje);
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

            return _listaMensajes;
        }



        public List<MensajeVisor> ListarMensajeVisorXDispositivoUltimaH(Dispositivo unDispositivo, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
            MensajeVisor unMensaje = null;
            List<MensajeVisor> _listaMensajes = new List<MensajeVisor>();

            SqlCommand _comando = new SqlCommand("ListarMensajeVisorXDispositivoUltimaH", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@DispositivoIP", unDispositivo.IP);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    while (_lector.Read())
                    {

                        unMensaje = new MensajeVisor
                            ((int)_lector["Id"],
                            (string)_lector["DispositivoIP"],
                            (DateTime)_lector["FechaGenerado"],
                            (string)_lector["Contenido"]);

                        _listaMensajes.Add(unMensaje);
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

            return _listaMensajes;
        }



        //public List<MensajeVisor> ListarMensajeVisorXUsuarioUltimaH(Usuario pUsuLogueado)
        //{
        //    SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));
        //    MensajeVisor unMensaje = null;
        //    List<MensajeVisor> _listaMensajes = new List<MensajeVisor>();

        //    SqlCommand _comando = new SqlCommand("ListarMensajeVisorXUsuarioUltimaH", _cnn);
        //    _comando.CommandType = CommandType.StoredProcedure;
        //    _comando.Parameters.AddWithValue("@UsuarioID", pUsuLogueado.UsuarioID);

        //    try
        //    {
        //        _cnn.Open();
        //        SqlDataReader _lector = _comando.ExecuteReader();
        //        if (_lector.HasRows)
        //        {
        //            while (_lector.Read())
        //            {
        //                unMensaje = new MensajeVisor
        //                    ((int)_lector["Id"],
        //                    (string)_lector["DispositivoIP"],
        //                    (DateTime)_lector["FechaGenerado"],
        //                    (string)_lector["Contenido"]);

        //                _listaMensajes.Add(unMensaje);
        //            }
        //        }
        //        _lector.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        _cnn.Close();
        //    }

        //    return _listaMensajes;
        //}



        public void BajaXTiempo(Administrador pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("MensajeVisorBajaXTiempo", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _cnn.Open();
                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("Fallo al intentar eliminar los Mensajes Visor");
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





    }


}

