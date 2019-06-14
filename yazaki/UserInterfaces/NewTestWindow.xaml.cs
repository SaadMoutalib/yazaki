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
using yazaki.Data;

namespace yazaki.UserInterfaces
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class NewTestWindow : Window
    {
        public Operateurs operateur { set; get; }
        public String level { set; get; }

        public NewTestWindow()
        {
            InitializeComponent();
            string[] ports = SerialPort.GetPortNames();
            bindComboBox();
        }

        private void bindComboBox()
        {
            using (var unitOfWork = new UnitOfWork(new yazakiDBEntities()))
            {
                IEnumerable<Operateurs> ops = unitOfWork.Operateurs.GetAll();
                DataContext = ops;
            }
        }

        private void validerButton_Click(object sender, RoutedEventArgs e)
        {
            if (opCombo.SelectedIndex == -1)
            {
                errormessage.Text = "Inserez tout les champs";

            }
            else if (niveau.SelectedIndex == -1)
            {
                errormessage.Text = "Inserez tout les champs";
            }
            else
            {
                operateur = opCombo.SelectedItem as Operateurs;
                level = niveau.Text;
                this.DialogResult = true;
            }

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
