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

namespace yazaki.UserInterfaces
{
    /// <summary>
    /// Logique d'interaction pour TestInsertion.xaml
    /// </summary>
    public partial class TestInsertion : Window
    {
        private SerialPort port;
        DispatcherTimer timer;
        Operateurs operateur;
        private int time = 3600;

        public TestInsertion(String niveau,Operateurs op)
        {
            InitializeComponent();
            operateur = op;
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

            switch (test)
            {
                case "1":
                    this.Dispatcher.Invoke(() =>
                    {
                        rect1.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                    });
                    break;
                case "2":
                    this.Dispatcher.Invoke(() =>
                    {
                        rect2.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                    });
                    break;
                case "3":
                    this.Dispatcher.Invoke(() =>
                    {
                        rect3.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                    });
                    break;
                case "4":
                    this.Dispatcher.Invoke(() =>
                    {
                        rect4.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                    });
                    break;
                case "5":
                    this.Dispatcher.Invoke(() =>
                    {
                        rect5.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                    }); 
                    break;
                case "6":
                    this.Dispatcher.Invoke(() =>
                    {
                        rect6.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                    });
                    break;
                case "7":
                    this.Dispatcher.Invoke(() =>
                    {
                        rect7.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                    });
                    break;
                case "8":
                    this.Dispatcher.Invoke(() =>
                    {
                        rect8.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                    });
                    break;
                case "9":
                    this.Dispatcher.Invoke(() =>
                    {
                        rect9.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                    });
                    break;
                case "10":
                    this.Dispatcher.Invoke(() =>
                    {
                        rect10.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                    });
                    break;
            }

        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
