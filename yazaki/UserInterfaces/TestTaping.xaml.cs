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
    /// Interaction logic for TestTaping.xaml
    /// </summary>
    public partial class TestTaping : Window
    {

        private SerialPort port;
        private DispatcherTimer timer;
        private Operateurs operateur;
        private Formateurs formateur;
        private String niveau;
        private LinearGradientBrush brush;
        private bool Start = false;
        private int Score = 0;
        private int time;
        private int target;
        private int tries = 5;
        //private double time2;
        //private double Vitesse;

        public TestTaping(String _niveau, Operateurs op, Formateurs form)
        {
            

            InitializeComponent();
            niveau = _niveau;
            formateur = form;
            operateur = op;
            nomLbl.Content = op.FullName;
            IDLbl.Content = op.Id;
            brush = pgBar.Foreground as LinearGradientBrush;

            try
            {
                port = new SerialPort();
                port.BaudRate = 9600;
                port.PortName = Options.PORT;

                port.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
                port.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message");
            }

            if (niveau == "Debutant")
            {
                target = 110;
                time = 3600;
            }
            else if (niveau == "Intérmediare")
            {
                target = 130;
                time = 3600;
            }
            else
            {
                target = 180;
                time = 3600;
            }
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
                Start = false;
                restartButton.Visibility = Visibility.Visible;
                timer.Stop();
                port.Close();
                tries--;
                if(Score >= target && niveau != "Avancé")
                {
                    addResult();
                    next.Visibility = Visibility.Visible;
                }
                
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
            StartMethod();
        }

        public void StartMethod()
        {
            
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            startButton.Background = Brushes.Red;
            startButton.IsEnabled = false;
            pgBar.Maximum = time;

        }

       


        void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string test = port.ReadLine();
            test = test.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
            if (Start == false )
            {
                this.Dispatcher.Invoke(() =>
                {
                    startButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                });
                Start = true;
            }
            if(test == "OUT")
            {
                Score++;
                //Vitesse = 1 / (time2/6000) ;
                //time2 = 0;
            }

            this.Dispatcher.Invoke(() =>
            {
                lblVitesse.Content = Score;
            });


        }

        private void addResult()
        {
            Test test = new Test();
            test.date = DateTime.Today;
            test.type = "Taping";
            test.id_form = formateur.Id;
            test.id_op = operateur.Id;
            test.nom_test = "Taping";
            test.resultat = (Score/target)*100;
            if ((Score / target) * 100 > 100)
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
            //port.Write("STOP");
            this.Close();
            Start = false; 
            if (port != null)
             port.Close(); 
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (niveau == "Debutant")
            {
                new TestInsertion("Intérmediare", operateur, formateur);
            }
            else if (niveau == "Intérmediare")
            {
                new TestInsertion("Avancé", operateur, formateur);
            }
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                startButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            });
            restartButton.Visibility = Visibility.Collapsed;
        }
    }
}
