using System;
using System.Collections.Generic;
using System.IO.Ports;
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

namespace yazaki.UserInterfaces
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {

        public OptionsWindow()
        {
            InitializeComponent();
            string[] ports = SerialPort.GetPortNames();
            portsCombo.ItemsSource = ports;
        }

        public string Port { set; get; }

        private void validerButton_Click(object sender, RoutedEventArgs e)
        {
            Port = portsCombo.Text;
            this.DialogResult = true;
        }
    }
}
