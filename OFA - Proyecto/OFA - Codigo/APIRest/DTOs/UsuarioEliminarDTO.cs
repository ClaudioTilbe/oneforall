using EntidadesCompartidas;
using System.Collections.Generic;

namespace APIRest.DTOs
{
    public class UsuarioEliminarDTO
    {
        public string UsuarioID { get; set; }
        public string Contraseña { get; set; }
        public Mail MailAsignado { get; set; }


        //Atributo de Tipo
        public string Tipo { get; set; }


        //Atributos Administrador
        public string Nombre { get; set; }
        public int NumeroFuncionario { get; set; }
        public string Cargo { get; set; }


        //Atributos Operador
        public int NumeroFuncionarioSupervisor { get; set; }
        public string NombreSupervisor { get; set; }
        public List<Sucursal> SucursalesAsignadas { get; set; }
    }


}
