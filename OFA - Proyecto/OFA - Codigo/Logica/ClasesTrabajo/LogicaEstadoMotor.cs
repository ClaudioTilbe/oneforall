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
    internal class LogicaEstadoMotor : ILogicaEstadoMotor
    {

        //Singleton
        private static LogicaEstadoMotor _instancia = null;
        private LogicaEstadoMotor() { }
        public static LogicaEstadoMotor GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaEstadoMotor();
            return _instancia;
        }

        //Operaciones
        public void Alta(EstadoMotor estadoMotor, Administrador pUsuLog)
        {
            FabricaPersistencia.GetPersistenciaEstadoMotor().Alta(estadoMotor, pUsuLog);
        }

        public EstadoMotor BuscarUltimo(Administrador pUsuLog)
        {
            return FabricaPersistencia.GetPersistenciaEstadoMotor().BuscarUltimo(pUsuLog);
        }


    }

}
