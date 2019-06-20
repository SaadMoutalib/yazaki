using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using yazaki.Data;

namespace yazaki.UserInterfaces
{
    /// <summary>
    /// Interaction logic for TestCutting.xaml
    /// </summary>
    public partial class TestCutting : Window
    {
        private DispatcherTimer timer;
        private Operateurs operateur;
        private Formateurs formateur;
        private String niveau;
        private LinearGradientBrush brush;
        private List<partNumber> partNumbers;
        private int Score = 0;
        private int time;

        public TestCutting(Operateurs op, Formateurs form)
        {
            InitializeComponent();
            formateur = form;
            operateur = op;
            nomLbl.Content = op.FullName;
            IDLbl.Content = op.Id;
            brush = pgBar.Foreground as LinearGradientBrush;
            pgBar.Maximum = time;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (time > 0)
            {
                time--;
                pgBar.Value++;
                startButton.Content = (TimeSpan.FromSeconds(time)).ToString();
            }
            else
            {



            }
            LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush();
            myLinearGradientBrush.StartPoint = brush.StartPoint;
            myLinearGradientBrush.EndPoint = brush.EndPoint;
            double progress = pgBar.Value / (pgBar.Maximum - pgBar.Minimum);
            foreach (GradientStop stop in brush.GradientStops)
            {
                myLinearGradientBrush.GradientStops.Add(new GradientStop(stop.Color, (stop.Offset * (1.0d / progress))));
            }

            pgBar.Foreground = myLinearGradientBrush;
        }
            private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            CheckLoginWindow option = new CheckLoginWindow(formateur);
            option.Owner = this;
            if (option.ShowDialog() == true)
            {
                this.Close();
            }
        }
        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            
            InputTextBox.Focus();
        }
    }

      
    
}
