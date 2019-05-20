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
    public partial class TestCramping : Window
    {
        private SerialPort port;
        private DispatcherTimer timer;
        private Operateurs operateur;
        private Formateurs formateur;
        private String niveau;

        private int time = 3600;
        private int Score = 0;
        private int tries = 0;

        public TestCramping(String _niveau,Operateurs op,Formateurs form)
        {
            InitializeComponent();
            operateur = op;
            formateur = form;
            niveau = _niveau;
            nomLbl.Content = op.FullName;
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
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                port = new SerialPort();
                port.BaudRate = 9600;
                port.PortName = "COM8";

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
            tries++;
            if (test == "OKAY")
            {
                Score++;
                this.Dispatcher.Invoke(() =>
                {
                    resultat.Content = Score + "/10";
                });
            }

            if(tries == 10)
            {
                port.Close();
                timer.Stop();
                addResult();
            }
            

        }

        private void addResult()
        {
            Test test = new Test();
            test.date = DateTime.Today;
            test.type = "Cramping";
            test.nom_test = "Cramping";
            test.id_form = formateur.Id;
            test.id_op = operateur.Id;
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
        }
    }
}
