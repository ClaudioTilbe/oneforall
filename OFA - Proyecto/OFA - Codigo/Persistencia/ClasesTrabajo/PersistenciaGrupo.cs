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
    internal class PersistenciaGrupo : IPersistenciaGrupo
    {

        //singleton
        private static PersistenciaGrupo _instancia = null;
        private PersistenciaGrupo() { }
        public static PersistenciaGrupo GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaGrupo();
            return _instancia;
        }



        public void Alta(Grupo unGrupo, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("GrupoAlta", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;

            _comando.Parameters.AddWithValue("@NombreGrupo", unGrupo.NombreGrupo);
            _comando.Parameters.AddWithValue("@Descripcion", unGrupo.Descripcion);
            _comando.Parameters.AddWithValue("@UsuarioID", unGrupo._Usuario.UsuarioID);

            SqlParameter _retorno = new SqlParameter("@Retorno", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);


            // Antes de ejecutar el comando "GrupoAlta"
            SqlParameter _grupoIDParam = new SqlParameter("@Codigo", System.Data.SqlDbType.Int);
            _grupoIDParam.Direction = System.Data.ParameterDirection.Output;
            _comando.Parameters.Add(_grupoIDParam);

            SqlTransaction _miTransaccion = null;


            try
            {
                _cnn.Open();

                _miTransaccion = _cnn.BeginTransaction();

                _comando.Transaction = _miTransaccion;
                _comando.ExecuteNonQuery(); 

                int CodInternoRetorno = Convert.ToInt32(_retorno.Value);

                if (CodInternoRetorno == -1)
                    throw new Exception("No existe un Usuario con ese ID");
                else if (CodInternoRetorno == -2)
                    throw new Exception("Fallo al intentar agregar el grupo");

                unGrupo.Codigo = Convert.ToInt32(_grupoIDParam.Value);

                foreach (Dispositivo unDisp in unGrupo.Dispositivos)
                {
                    GetInstancia().AsignoDispositivoAGrupo(unDisp, unGrupo, _miTransaccion);
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



        public void AsignoDispositivoAGrupo(Dispositivo unDispositivo, Grupo unGrupo, SqlTransaction _pTransaccion)
        {
            SqlCommand _comando = new SqlCommand("AsignoDispositivoAGrupo", _pTransaccion.Connection);
            _comando.CommandType = CommandType.StoredProcedure;

            _comando.Parameters.AddWithValue("@CodigoGrupo", unGrupo.Codigo);
            _comando.Parameters.AddWithValue("@DispositivoIP", unDispositivo.IP);

            SqlParameter _ParmRetorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _ParmRetorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_ParmRetorno);

            try
            {
                _comando.Transaction = _pTransaccion;

                _comando.ExecuteNonQuery();

                if ((int)_ParmRetorno.Value == -1)
                    throw new Exception("No existe un dispositivo con esa IP");
                else if ((int)_ParmRetorno.Value == -2)
                    throw new Exception("No existe un grupo con ese codigo");
                else if ((int)_ParmRetorno.Value == -3)
                    throw new Exception("Ya existe el dispositivo en ese grupo");
                else if ((int)_ParmRetorno.Value == -4)
                    throw new Exception("Fallo al intentar agregar el grupo");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public void Modificar(Grupo unGrupo, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("GrupoModificar", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Codigo", unGrupo.Codigo);
            _comando.Parameters.AddWithValue("@NombreGrupo", unGrupo.NombreGrupo);
            _comando.Parameters.AddWithValue("@Descripcion", unGrupo.Descripcion);
            _comando.Parameters.AddWithValue("@UsuarioID", pUsuLogueado.UsuarioID);


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
                    throw new Exception("No existe un grupo con ese codigo");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Fallo al intentar agregar el grupo");



                GetInstancia().QuitoDispositivosEnGrupo(unGrupo, _miTransaccion);

                foreach (Dispositivo unDisp in unGrupo.Dispositivos)
                {
                    GetInstancia().AsignoDispositivoAGrupo(unDisp, unGrupo, _miTransaccion);
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





        public void Baja(Grupo unGrupo, Usuario pUsuLogueado)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLogueado));

            SqlCommand _comando = new SqlCommand("GrupoBaja", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Codigo", unGrupo.Codigo);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            SqlTransaction _miTransaccion = null;

            try
            {
                _cnn.Open();

                _miTransaccion = _cnn.BeginTransaction();

                _comando.Transaction = _miTransaccion;


                GetInstancia().QuitoDispositivosEnGrupo(unGrupo, _miTransaccion);

                _comando.ExecuteNonQuery();

                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe grupo con ese codigo");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Fallo al intentar eliminar el grupo");

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


        public List<Grupo> ListadoGruposXUsuario(Usuario pUsuLog)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn(pUsuLog));
            Grupo _unGrupo = null;
            List<Grupo> _lista = new List<Grupo>();

            SqlCommand _comando = new SqlCommand("ListarGruposXUsuario", _cnn);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@UsuarioID", pUsuLog.UsuarioID);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {

                    while (_lector.Read())
                    {
                        _unGrupo = new Grupo((int)_lector["Codigo"],
                            (string)_lector["NombreGrupo"],
                            (string)_lector["Descripcion"],
                            pUsuLog,
                            FabricaPersistencia.GetPersistenciaDispositivo().ListadoDispositivosXGrupo((int)_lector["Codigo"], pUsuLog)
                            );

                        _lista.Add(_unGrupo);
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




        public void QuitoDispositivosEnGrupo(Grupo unGrupo, SqlTransaction _pTransaccion)
        {
            SqlCommand _comando = new SqlCommand("QuitoDispositivosEnGrupo", _pTransaccion.Connection);
            _comando.CommandType = CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Codigo", unGrupo.Codigo);

            SqlParameter _retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _retorno.Direction = ParameterDirection.ReturnValue;
            _comando.Parameters.Add(_retorno);

            try
            {
                _comando.Transaction = _pTransaccion;

                _comando.ExecuteNonQuery();
                if ((int)_retorno.Value == -1)
                    throw new Exception("No existe un Grupo con ese codigo");
                else if ((int)_retorno.Value == -2)
                    throw new Exception("Fallo al intentar retirar los dispositivos de ese grupo");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }


}

