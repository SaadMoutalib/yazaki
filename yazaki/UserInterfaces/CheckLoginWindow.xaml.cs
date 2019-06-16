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
    public partial class CheckLoginWindow : Window
    {
        private Formateurs formateur;
        public CheckLoginWindow(Formateurs _formateur)
        {
            formateur = _formateur;
            InitializeComponent();
            passwordBox.Focus();
        }

        private void validerButton_Click(object sender, RoutedEventArgs e)
        {
            if(passwordBox.Password == formateur.password)
            {
                this.DialogResult = true;
            }
            else{
                errormessage.Text = "Mot de passe incorrect";
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
