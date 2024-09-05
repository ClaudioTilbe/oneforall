using EntidadesCompartidas;
using System.Dynamic;

namespace APIRest.Utility
{
    public static class SetTypeToUsuario
    {

        public static ExpandoObject SetType(Usuario unUsuario, string token)
        {

            dynamic unUsuarioExtendido = new ExpandoObject();

            if (unUsuario is Operador)
            {
                unUsuarioExtendido.Token = token;
                unUsuarioExtendido.Tipo = unUsuario.GetType().Name;
                unUsuarioExtendido.UsuarioID = unUsuario.UsuarioID;
                unUsuarioExtendido.Contraseña = unUsuario.Contraseña;
                unUsuarioExtendido.MailAsignado = unUsuario.MailAsignado;
                unUsuarioExtendido.NombreSupervisor = ((Operador)unUsuario).NombreSupervisor;
                unUsuarioExtendido.NumeroFuncionarioSupervisor = ((Operador)unUsuario).NumeroFuncionarioSupervisor;
                unUsuarioExtendido.SucursalesAsignadas = ((Operador)unUsuario).SucursalesAsignadas;
            }

            else if (unUsuario is Administrador)
            {
                unUsuarioExtendido.Token = token;
                unUsuarioExtendido.Tipo = unUsuario.GetType().Name;
                unUsuarioExtendido.UsuarioID = unUsuario.UsuarioID;
                unUsuarioExtendido.Contraseña = unUsuario.Contraseña;
                unUsuarioExtendido.MailAsignado = unUsuario.MailAsignado;
                unUsuarioExtendido.Nombre = ((Administrador)unUsuario).Nombre;
                unUsuarioExtendido.NumeroFuncionario = ((Administrador)unUsuario).NumeroFuncionario;
                unUsuarioExtendido.Cargo = ((Administrador)unUsuario).Cargo;
            }

            return unUsuarioExtendido;

        }

    }
}
