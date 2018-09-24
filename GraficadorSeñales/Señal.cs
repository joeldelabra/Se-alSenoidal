using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
     abstract class Señal
    {
        public List<Muestra> muestras { get; set; }
        public double AmplitudMaxima { get; set; }
        public double TiempoInicial { get; set; }
        public double TiempoFinal { get; set; }
        public double FrecuenciaMuestreo { get; set; }

    public abstract double evaluar(double tiempo);

        public void ConstruirSeñalDigital()
        {
            double periodoMuestreo = 1 / FrecuenciaMuestreo;

            for (double i = TiempoInicial;
                i <= TiempoFinal;
                i += periodoMuestreo)
            {
                double Muestra = evaluar(i);
                if (Math.Abs(Muestra) > AmplitudMaxima)
                {
                    AmplitudMaxima = Math.Abs(Muestra);
                }
            muestras.Add(new Muestra(i, evaluar(i)));


            }
        }


    }
}
