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
           
            double TiempoInicial = double.Parse(txtTiempoInicial.Text);
            double TiempoFinal = double.Parse(txtTiempoFinal.Text);
            double Muestreo = double.Parse(txtMuestreo.Text);

            Señal senal;
            Señal segundasenal;

            switch(cbTipoSeñal.SelectedIndex)
            {
                // Senoidal
                case 0:

                    double Amplitud =
                        double.Parse(((ConfiguracionSenalSenoidal)PanelConfiguracion.Children[0]).txtAmplitud.Text);

                    double Fase =
                        double.Parse(((ConfiguracionSenalSenoidal)PanelConfiguracion.Children[0]).txtFase1.Text);

                    double Frecuencia =
                        double.Parse(((ConfiguracionSenalSenoidal)PanelConfiguracion.Children[0]).txtFrecuencia.Text);

                    senal =  new SenalSenoidal(Amplitud, Fase, Frecuencia);
                    break;
                case 1: senal = new Rampa();
                    break;
                case 2:
                    double Alpha =
                        double.Parse(((ConfiguracionExponencial)PanelConfiguracion.Children[0]).txtAlpha.Text);

                    senal = new Exponencial (Alpha);
                    
                    break;

                default:

                    senal = null;
                    break;
            }
             // SWITCH 2
            switch (cbTipoSeñal.SelectedIndex)
            {
                // Senoidal
                case 0:

                    double Amplitud =
                        double.Parse(((ConfiguracionSenalSenoidal)PanelConfiguracion2.Children[0]).txtAmplitud.Text);

                    double Fase =
                        double.Parse(((ConfiguracionSenalSenoidal)PanelConfiguracion2.Children[0]).txtFase1.Text);

                    double Frecuencia =
                        double.Parse(((ConfiguracionSenalSenoidal)PanelConfiguracion2.Children[0]).txtFrecuencia.Text);

                    segundasenal = new SenalSenoidal(Amplitud, Fase, Frecuencia);
                    break;
                case 1:
                    segundasenal = new Rampa();
                    break;
                case 2:
                    double Alpha =
                        double.Parse(((ConfiguracionExponencial)PanelConfiguracion2.Children[0]).txtAlpha.Text);

                    segundasenal = new Exponencial(Alpha);

                    break;

                default:

                    segundasenal = null;
                    break;
            }

            senal.TiempoInicial = TiempoInicial;
            senal.TiempoFinal = TiempoFinal;
            senal.FrecuenciaMuestreo = Muestreo;

            // segunda señal
            segundasenal.TiempoInicial = TiempoInicial;
            segundasenal.TiempoFinal = TiempoFinal;
            segundasenal.FrecuenciaMuestreo = Muestreo;

            senal.ConstruirSeñalDigital();
            segundasenal.ConstruirSeñalDigital();

            // Truncar
            if ((bool)CheckUmbral.IsChecked)
            {
                double n = double.Parse(txtUmbral.Text);
                senal.Truncar(n);
            }

            // Escalar
            if ((bool)CheckEscalar.IsChecked)
            {
                double factorEscala = double.Parse(txtAmplitud.Text);
                senal.escalar(factorEscala);
            }
                        
            // Desplazar.
              if ((bool)CheckDesplazamiento.IsChecked)
            {
                double factorDesplazamiento = double.Parse(txtDesplazamiento.Text);
                senal.desplazar(factorDesplazamiento);
                senal.actualizarAmplitudMaxima();
            }

            // Truncar2
            if ((bool)CheckUmbral2.IsChecked)
            {
                double n = double.Parse(txtUmbral1.Text);
                senal.Truncar(n);
            }

            // Escalar2
            if ((bool)CheckEscalar2.IsChecked)
            {
                double factorEscala = double.Parse(txtAmplitud.Text);
                senal.escalar(factorEscala);
            }

            // Desplazar2.
            if ((bool)CheckDesplazamiento2.IsChecked)
            {
                double factorDesplazamiento = double.Parse(txtDesplazamiento2.Text);
                senal.desplazar(factorDesplazamiento);
                senal.actualizarAmplitudMaxima();
            }

            plnGrafica.Points.Clear();

            if (senal != null)
            {
             // Recorrer una coleccion o arreglo
            foreach (Muestra muestra in senal.muestras)
                 {
                plnGrafica.Points.Add(new Point((muestra.X - TiempoInicial) * Scroll.Width, (muestra.Y / senal.AmplitudMaxima 
                    * ((Scroll.Height / 2) - 30) * -1 + (Scroll.Height / 2))));
                 }

            }


                    
           

            plnEjeX.Points.Clear();
            // Punto de inicio
            plnEjeX.Points.Add(new Point(0, (Scroll.Height / 2)));
            // Punto final.
            plnEjeX.Points.Add(new Point((TiempoFinal - TiempoInicial) * Scroll.Width, (Scroll.Height / 2)));


         

            lblAmplitudMaximaPositiva.Text = senal.AmplitudMaxima.ToString("F");
            lblAmplitudMaximaNegativa.Text = " - " + senal.AmplitudMaxima.ToString("F");
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

        private void cbTipoSeñal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PanelConfiguracion != null)
            {


            }
            PanelConfiguracion.Children.Clear();
            switch (cbTipoSeñal.SelectedIndex)
            {
                case 0:
                    PanelConfiguracion.Children.Add(
                        new ConfiguracionSenalSenoidal()
                        );
                    break;
                case 1:
                    
                    break;
                case 2:
                    PanelConfiguracion.Children.Add(new ConfiguracionExponencial());
                    break;
                default:
                    break;
            }
        }

        private void cbTipoSeñal2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PanelConfiguracion2.Children.Clear();
            switch (cbTipoSeñal2.SelectedIndex) {
                case 0: //Senoidal
                    PanelConfiguracion2.Children.Add(new ConfiguracionSenalSenoidal());
                    break;
                case 1: // Rampa
                    break;
                case 2: //Exponencial
                    PanelConfiguracion2.Children.Add(new ConfiguracionExponencial());
                    break;
                default:
                    break;
            }
        }
    }
}
