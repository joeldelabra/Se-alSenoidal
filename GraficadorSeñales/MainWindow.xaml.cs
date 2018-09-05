using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraficadorSeñales
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();



        }

        private void btnGraficar_Click(object sender, RoutedEventArgs e)
        {
            double Amplitud = double.Parse(txtAmplitud.Text);
            double Fase = double.Parse(txtFase1.Text);
            double Frecuencia = double.Parse(txtFrecuencia.Text);
            double TiempoInicial = double.Parse(txtTiempoInicial.Text);
            double TiempoFinal = double.Parse(txtTiempoFinal.Text);
            double Muestreo = double.Parse(txtMuestreo.Text);

            SenalSenoidal senal = new SenalSenoidal(Amplitud, Fase, Frecuencia);

            double periodoMuestreo = 1 / Muestreo;
            plnGrafica.Points.Clear();

            for (double i = TiempoInicial;
                i <= TiempoFinal;
                i += periodoMuestreo)
            {
                double Muestra = senal.evaluar(i);
                if (Math.Abs(Muestra) > senal.AmplitudMaxima)
                {
                senal.AmplitudMaxima = Math.Abs(Muestra);
                }
                senal.muestras.Add(new Muestra(i, senal.evaluar(i)));

              
            }

            // Recorrer una coleccion o arreglo
            foreach (Muestra muestra in senal.muestras)
            {
                plnGrafica.Points.Add(new Point(muestra.X * Scroll.Width, (muestra.Y / senal.AmplitudMaxima *
                    ((Scroll.Height / 2)) - 30) * -1 + (Scroll.Height / 2)));
            }

            lblAmplitudMaximaPositiva.Text = senal.AmplitudMaxima.ToString();
            lblAmplitudMaximaNegativa.Text = " - " + senal.AmplitudMaxima.ToString();
        }

        private void btnRampa_Click(object sender, RoutedEventArgs e)
        {
            double tiempoInicial = double.Parse(txtTiempoInicial.Text);
            double tiempoFinal = double.Parse(txtTiempoFinal.Text);
            double frecuenciaMuestreo = double.Parse(txtMuestreo.Text);

            Rampa Rampa = new Rampa();

            plnGrafica.Points.Clear();

            double periodoMuestreo = 1 / frecuenciaMuestreo;
            for (double i = tiempoInicial; i <= tiempoFinal; i += periodoMuestreo)
            {
                double valorMuestra = Rampa.evaluar(i);

                if (Math.Abs(valorMuestra) > Rampa.AmplitudMaxima)
                {
                    Rampa.AmplitudMaxima = Math.Abs(valorMuestra);
                }

                Rampa.Muestras.Add(new Muestra(i, valorMuestra));

            }


            //Recorrer una colección o arreglo
            //La variable muestra guarda cada elemento de la colección de: señal.Muestra
            foreach (Muestra muestra in Rampa.Muestras)
            {
                plnGrafica.Points.Add(new Point(muestra.X * Scroll.Width, (muestra.Y *
                    ((Scroll.Height / 2)) - 30) * -1 + (Scroll.Height / 2)));
            }
        }
    }
}
