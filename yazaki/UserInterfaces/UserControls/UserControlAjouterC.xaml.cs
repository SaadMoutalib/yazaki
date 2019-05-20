using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using yazaki.Data;

namespace yazaki.UserInterfaces.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UserControlAjouter.xaml
    /// </summary>
    public partial class UserControlAjouterC : UserControl
    {
        public UserControlAjouterC()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            succesmessage.Text = "";
            errormessage.Text = "";
             if (nomTextBox.Text.Length == 0)
            {
                errormessage.Text = "Entrez un Nom";
                nomTextBox.Focus();
            }
            else if (prenomTextBox.Text.Length == 0)
            {
                errormessage.Text = "Entrez un Prenom";
                prenomTextBox.Focus();
            }
            else { 
                Operateurs oper = new Operateurs();
                Coordonnees cord = new Coordonnees();
                cord.Adresse = adresseTextBox.Text;
                if (male.IsChecked == true)
                {
                    cord.Sexe = "M";
                }
                else
                {
                    cord.Sexe = "F";
                }
                
                cord.civilStatus = civilStatus.Text;
                cord.dateOfBirth = dateOfBirth.SelectedDate;
                cord.dateOfEmployment = dateOfEmployment.SelectedDate;
                cord.dateOfStartingWorkInDep = dateOfStartingWorking.SelectedDate;
                cord.departementName = departementName.Text;
                cord.graduationDate = graduationDate.SelectedDate;
                cord.educationLevel = educationlevel.Text;
                cord.Ville = villeTextBox.Text;
                cord.Email = emailTextBox.Text;
                cord.Tel = telTextBox.Text;
                cord.postalCode = Int32.Parse(postalCode.Text);
                cord.jobTitle = jobTitle.Text;
                cord.Operateur = oper;
                oper.nom = nomTextBox.Text;
                oper.prenom = prenomTextBox.Text;
                oper.Coordonnee = cord;
                using (var unitOfWork = new UnitOfWork(new yazakiDBEntities()))
                {
                    unitOfWork.Operateurs.Add(oper);
                    try
                    {
                        unitOfWork.Save();
                        succesmessage.Text = "Opérateur ajouter avec succès";
                    }
                    catch(Exception ex)
                    {
                        errormessage.Text = "Une érreur c'est produite impossible d'ajouter l'opérateur";
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }
    }
}
