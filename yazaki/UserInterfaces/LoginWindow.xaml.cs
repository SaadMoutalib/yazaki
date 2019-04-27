using System;
using System.Windows;
using yazaki.Data;

namespace yazaki.UserInterfaces
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
            errormessage.Text = "";
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

                Formateur formateur;

                using (var unitOfWork = new UnitOfWork(new yazakiDBEntities()))
                {
                    formateur = unitOfWork.Formateurs.Find(s => s.Id == id && s.password == password);
                }

                if (formateur == null)
                {
                    errormessage.Text = "Identifiant ou Mot de passe incorrect";
                }
                else
                {
                    new HomeWindow(formateur).Show();
                    this.Close();
                }
            }
            
        }
    }
}
