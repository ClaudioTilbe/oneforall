using EntidadesCompartidas;
using Logica.Interfaces;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.ClasesTrabajo
{
    internal class LogicaEscaneoPuertos : ILogicaEscaneoPuertos
    {

        //Singleton
        private static LogicaEscaneoPuertos _instancia = null;
        private LogicaEscaneoPuertos() { }
        public static LogicaEscaneoPuertos GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaEscaneoPuertos();
            return _instancia;
        }



        //Operaciones
        public void Alta(EscaneoPuertos unEscaneo, Administrador pUsuLogueado)
        {
            FabricaPersistencia.GetPersistenciaEscaneoPuertos().Alta(unEscaneo, pUsuLogueado);
        }

        public void Actualizar(EscaneoPuertos unEscaneo, Administrador pUsuLogueado)
        {
            FabricaPersistencia.GetPersistenciaEscaneoPuertos().Actualizar(unEscaneo, pUsuLogueado);
        }

        public void Cancelar(EscaneoPuertos unEscaneo, Administrador pUsuLogueado)
        {
            FabricaPersistencia.GetPersistenciaEscaneoPuertos().Cancelar(unEscaneo, pUsuLogueado);
        }

        public EscaneoPuertos Buscar(int escaneoID, Administrador pUsuLogueado)
        {
            return FabricaPersistencia.GetPersistenciaEscaneoPuertos().Buscar(escaneoID, pUsuLogueado);
        }

        public List<EscaneoPuertos> ListadoEscaneoPuertosTodos(Administrador pUsuLogueado)
        {
            return FabricaPersistencia.GetPersistenciaEscaneoPuertos().ListadoEscaneoPuertosTodos(pUsuLogueado);
        }

        public List<EscaneoPuertos> ListadoEscaneoPuertosPendientes(Administrador pUsuLogueado)
        {
            return FabricaPersistencia.GetPersistenciaEscaneoPuertos().ListadoEscaneoPuertosPendientes(pUsuLogueado);
        }



    }
}
