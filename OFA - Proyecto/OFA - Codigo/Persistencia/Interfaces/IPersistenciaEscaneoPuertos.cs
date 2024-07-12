using EntidadesCompartidas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Interfaces
{
    public interface IPersistenciaEscaneoPuertos
    {

        void Alta(EscaneoPuertos unEscaneo, Administrador pUsuLogueado);
        void Actualizar(EscaneoPuertos unEscaneo, Administrador pUsuLogueado);
        void Cancelar(EscaneoPuertos unEscaneo, Administrador pUsuLogueado);
        EscaneoPuertos Buscar(int escaneoID, Administrador pUsuLogueado);
        List<EscaneoPuertos> ListadoEscaneoPuertosTodos(Administrador pUsuLogueado);
        List<EscaneoPuertos> ListadoEscaneoPuertosPendientes(Administrador pUsuLogueado);


    }
}
