using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    class Exponencial : Señal
    {
        public double Alpha { get; set; }
     


        public Exponencial()
        {
            Alpha = 0;
            muestras = new List<Muestra>();
        }

        public Exponencial(double Alpha)
        {
            this.Alpha = Alpha;
            muestras = new List<Muestra>();
        }

        override public double evaluar(double tiempo)
        {
            double resultado;
            resultado = Math.Exp(Alpha * tiempo);
            return resultado;
        }
    }

}




