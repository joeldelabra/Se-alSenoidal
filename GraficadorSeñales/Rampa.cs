﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    class Rampa : Señal
    {

        public List<Muestra> Muestras { get; set; }
        public double AmplitudMaxima { get; set; }

        public Rampa()
        {
            Muestras = new List<Muestra>();
            AmplitudMaxima = 0.0;
        }
        public Rampa(double amplitud, double fase, double frecuencia)
        {
            Muestras = new List<Muestra>();
            AmplitudMaxima = 0.0;
        }

        override public double evaluar(double tiempo)
        {
            double resultado;

            if (tiempo >= 0)
            {
                resultado = tiempo;
            }
            else
            {
                resultado = 0;
            }

            return resultado;
        }
    }
}