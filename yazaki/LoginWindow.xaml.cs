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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace yazaki
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (idTextBox.Text.Length == 0)
            {
                errormessage.Text = "Entrez un Identifiant";
                idTextBox.Focus();
            }
            else if (passwordBox.Password.Length == 0)
            {
                errormessage.Text = "Entrez un Mot de passe";
                passwordBox.Focus();
            }
            else
            {
                int id = Convert.ToInt32(idTextBox.Text);
                string password = passwordBox.Password;
                bool loggedIn = false;

                Formateur formateur;

                using (var context = new YazakiContext())
                {
                Repository<Formateur> form = new Repository<Formateur>(context);
                formateur = form.Find(s => s.Id == id && s.password == password);
                }

                if (formateur == null)
                {
                    errormessage.Text = "Identifiant ou Mot de passe incorrect";
                }
                else
                {
                    loggedIn = true;
                    new HomeWindow().Show();
                    this.Close();
                }
            }
            
        }
    }
}
