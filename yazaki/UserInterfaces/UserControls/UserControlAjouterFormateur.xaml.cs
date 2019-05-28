using System;
using System.Windows;
using System.Windows.Controls;
using yazaki.Data;

namespace yazaki.UserInterfaces.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UserControlAjouterF.xaml
    /// </summary>
    public partial class UserControlAjouterFormateur : UserControl
    {
        public UserControlAjouterFormateur()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Formateurs formateur = new Formateurs();
            if (nomTextBox.Text.Length == 0)
            {
                errormessage.Text = "Entrez un Nom";
                nomTextBox.Focus();
            }
            else if (prenomTextBox.Text.Length == 0)
            {
                errormessage.Text = "Entrez un Prenom";
                prenomTextBox.Focus();
            }else if(passwordTextBox.Password.Length == 0)
            {
                errormessage.Text = "Entrez un Mot de passe";
                passwordTextBox.Focus();
            }
            else
            {
                formateur.nom = nomTextBox.Text;
                formateur.prenom = prenomTextBox.Text;
                formateur.password = passwordTextBox.Password;

                using (var unitOfWork = new UnitOfWork(new yazakiDBEntities()))
                {
                    unitOfWork.Formateurs.Add(formateur);
                    try
                    {
                        unitOfWork.Formateurs.Add(formateur);
                        unitOfWork.Save();
                        succesmessage.Text = "Formateur ajouter avec succès avec l'ID : "+formateur.Id;
                    }
                    catch (Exception ex)
                    {
                        errormessage.Text = "Une érreur c'est produite impossible d'ajouter le formateur";
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            
        }
    }
}
