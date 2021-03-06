﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using yazaki.Data;

namespace yazaki.UserInterfaces.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UserControlAjouter.xaml
    /// </summary>
    public partial class UserControlAjouterOperateur : UserControl
    {
        private Formateurs formateur;

        public UserControlAjouterOperateur(Formateurs form)
        {
            InitializeComponent();
            formateur = form;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Operateurs oper = ajouterOperateur();
        }

        private Operateurs ajouterOperateur()
        {
            succesmessage.Text = "";
            errormessage.Text = "";

            Operateurs oper = new Operateurs();
            Coordonnees cord = new Coordonnees();
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
            else if (idTextBox.Text.Length == 0)
            {
                errormessage.Text = "Entrez un Matricul";
                idTextBox.Focus();
            }
            else
            {
                oper.Id = Int32.Parse(idTextBox.Text);
                oper.nom = nomTextBox.Text;
                oper.prenom = prenomTextBox.Text;
                cord.Adresse = adresseTextBox.Text;
                if (male.IsChecked == true)
                {
                    cord.Sexe = "M";
                }
                else if (female.IsChecked == true)
                {
                    cord.Sexe = "F";
                }
                cord.id = oper.Id;
                cord.civilStatus = civilStatus.Text;
                cord.dateOfBirth = dateOfBirth.SelectedDate;
                cord.dateOfEmployment = dateOfEmployment.SelectedDate;
                cord.departementName = departementName.Text;
                cord.graduationDate = graduationDate.SelectedDate;
                cord.educationLevel = educationlevel.Text;
                cord.Ville = villeTextBox.Text;
                cord.Email = emailTextBox.Text;
                cord.Tel = telTextBox.Text;
                if(postalCode.Text!="")
                    cord.postalCode = Int32.Parse(postalCode.Text);
                cord.jobTitle = jobTitle.Text;
                cord.Operateur = oper;
                oper.Coordonnee = cord;
                using (var unitOfWork = new UnitOfWork(new yazakiDBEntities()))
                {
                    try
                    {
                        unitOfWork.Operateurs.Add(oper);
                        unitOfWork.Save();

                        succesmessage.Text = "Opérateur ajouter avec succès";

                        return oper;
                    }
                    catch (Exception ex)
                    {
                        errormessage.Text = "Une érreur c'est produite impossible d'ajouter l'opérateur";
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            return null;
        }

        private void openTestTab(Operateurs op)
        {
            HomeWindow parentWindow = Window.GetWindow(this) as HomeWindow;
            parentWindow.GridCursor.Margin = new Thickness(10 + (180 * 1), 0, 0, 0);

            parentWindow.GridMain.Children.Clear();
            parentWindow.GridMain.Children.Add(new UserControlTest(op,formateur));
                    
        }

        private void addAndStartButton_Click(object sender, RoutedEventArgs e)
        {
            Operateurs oper = ajouterOperateur();
            if(oper != null)
                openTestTab(oper);
        }

        private void IdTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
