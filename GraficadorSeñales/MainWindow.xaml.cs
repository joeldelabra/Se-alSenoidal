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

            for (double i = TiempoInicial; i<=TiempoFinal; i+= periodoMuestreo)
            {
                plnGrafica.Points.Add(new Point(i*Scroll.Width, (senal.evaluar(i)*((Scroll.Height/2.0)-30) * -1) + (Scroll.Height / 2.0)));
            }
        }
    }
}
