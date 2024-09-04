using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EntidadesCompartidas
{
    public class Subred
    {
        private string rango;


        public string Rango
        {
            get { return rango; }
            set
            {
                Regex exp = new Regex(@"^(\d{1,3}\.){2}\d{1,3}$"); // Expresión regular para validar xxx.xxx.xxx

                if (!exp.IsMatch(value.Trim()))
                    throw new Exception("Error en el Rango de la Subred");
                else
                    rango = value;
            }
        }


        public Subred(string pRango)
        {
            Rango = pRango;
        }

        public Subred()
        {

        }


    }

}
