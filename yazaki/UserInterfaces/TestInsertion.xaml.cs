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
        public TestInsertion()
        {
            InitializeComponent();
            port = new SerialPort();
        }
        private int time = 3600;
        void timer_Tick(object sender, EventArgs e)
        {
            if(time>0)
                time--;
            lblTimer.Content = (TimeSpan.FromSeconds(time)).ToString();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                port.BaudRate = 9600;
                port.PortName = "COM8";
                port.Open();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message");
            }
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string test = port.ReadLine();
            MessageBox.Show(test);

        }
    }
}
