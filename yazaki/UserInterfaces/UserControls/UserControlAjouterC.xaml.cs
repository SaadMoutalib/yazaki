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
                oper.nom = nomTextBox.Text;
                oper.prenom = prenomTextBox.Text;
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
                    }
                }
            }
        }
    }
}
