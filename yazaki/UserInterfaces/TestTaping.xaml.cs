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
          
            OpenPort();
            Essaies.Content = tries;
            if (niveau == "Debutant")
            {
                target = 110;
                time = 3600;
                Niveau.Content = "Débutant";
                Objectif.Content = "110 tour";

            }
            else if (niveau == "Intérmediaire")
            {
                target = 130;
                time = 3600;
                Niveau.Content = "Intérmediaire";
                Objectif.Content = "130 tour";
            }
            else
            {
                target = 180;
                time = 3600;
                Niveau.Content = "Avancé";
                Objectif.Content = "180 tour";
            }
            pgBar.Maximum = time;
        }

        void OpenPort()
        {
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
                port = null;
                MessageBox.Show(ex.Message, "Message");
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
                port.Close();
                Start = false;
                timer.Stop();
                NextCandidat.Visibility = Visibility.Visible;
                startButton.Visibility = Visibility.Collapsed;
                if (Score >= target)
                {
                    restartButton.Visibility = Visibility.Collapsed;
                    addResult();
                    if (niveau != "Avancé")
                    { next.Visibility = Visibility.Visible; }
                }
                if (Score < target && tries > 0)
                {
                    restartButton.Visibility = Visibility.Visible;
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
            if (port != null)
            {
                //port.Write("START");
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(1);
                timer.Tick += timer_Tick;
                timer.Start();

                startButton.Background = Brushes.Red;
                startButton.IsEnabled = false;
                pgBar.Maximum = time;
            }
            else
            {
                MessageBox.Show("Error : Port is null");
            }
           

        }
        public void Resettest()
        {
            port.Open();
            startButton.IsEnabled = true;
            startButton.Visibility = Visibility.Visible;
            startButton.Content = "Start";
            startButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2196F3")); ;
            time = 3600;
            pgBar.Value = 0;
            pgBar.Maximum = time;
            Score = 0;
            tries--;
            Essaies.Content = tries;
            lblVitesse.Content = Score;
            NextCandidat.Visibility = Visibility.Collapsed;
        }



        void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string test = port.ReadLine();
            test = test.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
            if (Start == false)
            {
                this.Dispatcher.Invoke(() =>
                {
                    startButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                });
                Start = true;
            }
            if (test == "OUT")
            {
                Score++;
            }

            this.Dispatcher.Invoke(() =>
            {
                lblVitesse.Content = Score;
            });


        }

        private void addResult()
        {
            double res  = (Score / (double)target) * 100;
            Test test = new Test();
            test.date = DateTime.Now;
            test.type = "Taping";
            test.id_form = formateur.Id;
            test.id_op = operateur.Id;
            test.nom_test = "Taping";
            test.resultat = Math.Round(res,2);
                //(float)Math.Round(res * 100f,2) / 100f;
            if (res > 100)
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
            CheckLoginWindow option = new CheckLoginWindow(formateur);
            option.Owner = this;
            if (option.ShowDialog() == true)
            {
                //port.Write("STOP");
                this.Close();
                Start = false;
                if (port != null)
                    port.Close();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            NewTestWindow newTest = new NewTestWindow();
            newTest.Owner = this;
            if (newTest.ShowDialog() == true)
            {
                port.Close();
                new TestTaping(newTest.level, newTest.operateur, formateur).Show();
                this.Close();
            }
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            Resettest();
            Start = false;
            restartButton.Visibility = Visibility.Collapsed;
        }

        private void NextLvl_Click(object sender, RoutedEventArgs e)
        {
            if(niveau == "Debutant")
            {
                new TestTaping("Intérmediaire", operateur, formateur).Show();
            }
            if (niveau == "Intérmediaire")
            {
                new TestTaping("Avancé", operateur, formateur).Show();
            }
            port.Close();        
            this.Close();
        }
    }
}