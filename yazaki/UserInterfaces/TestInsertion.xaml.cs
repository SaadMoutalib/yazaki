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
using System.Windows.Shapes;
using System.IO.Ports;
using System.Windows.Threading;
using yazaki.Data;

namespace yazaki.UserInterfaces
{
    /// <summary>
    /// Logique d'interaction pour TestInsertion.xaml
    /// </summary>
    public partial class TestInsertion : Window
    {
        private SerialPort port;
        private DispatcherTimer timer;
        private Operateurs operateur;
        private Formateurs formateur;
        private String niveau;
        private LinearGradientBrush brush;

        private int Score = 0;
        private int time;

        public TestInsertion(String _niveau,Operateurs op,Formateurs form)
        {
            InitializeComponent();
            niveau = _niveau;
            formateur = form;
            operateur = op;
            nomLbl.Content = op.FullName;
            IDLbl.Content = op.Id;
            brush = pgBar.Foreground as LinearGradientBrush;


            if (niveau == "Debutant")
            {
                time = 3600;
            }else if(niveau == "Intérmediare")
            {
                time = 2400;
            }
            else{
                time = 1800;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (time > 0) { 
                time--;
                pgBar.Value++;
                startButton.Content = (TimeSpan.FromSeconds(time)).ToString();
            }
            else
            {
                timer.Stop();
                port.Close();
                addResult();
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

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                port = new SerialPort();
                port.BaudRate = 9600;
                port.PortName = Options.PORT;

                port.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
                port.Open();

                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(1);
                timer.Tick += timer_Tick;
                timer.Start();

                startButton.Background = Brushes.Red;
                startButton.IsEnabled = false;
                pgBar.Maximum = time;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message");
            }
            
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string test = port.ReadLine();
            test = test.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

            switch (test)
            {
                case "1":
                    this.Dispatcher.Invoke(() =>
                    {
                        rect1.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                        Score++;
                    });
                    break;
                case "2":
                    this.Dispatcher.Invoke(() =>
                    {
                        rect2.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                        Score++;
                    });
                    break;
                case "3":
                    this.Dispatcher.Invoke(() =>
                    {
                        rect3.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                        Score++;
                    });
                    break;
                case "4":
                    this.Dispatcher.Invoke(() =>
                    {
                        rect4.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                        Score++;
                    });
                    break;
                case "5":
                    this.Dispatcher.Invoke(() =>
                    {
                        rect5.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                        Score++;
                    }); 
                    break;
                case "6":
                    this.Dispatcher.Invoke(() =>
                    {
                        rect6.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                        Score++;
                    });
                    break;
                case "7":
                    this.Dispatcher.Invoke(() =>
                    {
                        rect7.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                        Score++;
                    });
                    break;
                case "8":
                    this.Dispatcher.Invoke(() =>
                    {
                        rect8.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                        Score++;
                    });
                    break;
                case "9":
                    this.Dispatcher.Invoke(() =>
                    {
                        rect9.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                        Score++;
                    });
                    break;
                case "10":
                    this.Dispatcher.Invoke(() =>
                    {
                        rect10.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                        Score++;
                    });
                    break;
            }

            this.Dispatcher.Invoke(() =>
            {
                lblResultat.Content = Score + "/10";
            });

            if (Score == 10)
            {
                port.Close();
                timer.Stop();
            }

        }

        private void addResult()
        {
            Test test = new Test();
            test.date = DateTime.Today;
            test.type = "Insertion";
            test.id_form = formateur.Id;
            test.id_op = operateur.Id;
            test.nom_test = "Insertion";
            test.resultat = Score;
            if (Score > 5)
            {
                test.passed = true;
            }
            else
            {
                test.passed = false;
            }
            test.niveau = niveau;

            using (var unitOfWork = new UnitOfWork(new yazakiDBEntities()))
            {
                unitOfWork.Tests.Add(test);
                try
                {
                    unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            if (port != null)
                port.Close();
        }
    }
}
