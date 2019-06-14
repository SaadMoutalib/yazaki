﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using yazaki.Data;

namespace yazaki.UserInterfaces
{
    /// <summary>
    /// Interaction logic for TestTaping.xaml
    /// </summary>
    public partial class TestCrimpingSetup : Window
    {
        

        private Operateurs operateur;
        private Formateurs formateur;
        private int Score = 10;
        private string affichage= "";
        private Produit produit;
        private int tries;
        private bool isValide;

        public TestCrimpingSetup(Operateurs op, Formateurs form)
        {
            InitializeComponent();
            formateur = form;
            operateur = op;
            nomLbl.Content = op.FullName;
            IDLbl.Content = op.Id;

            produit = new Produit("7116-4288-(02) SB", "1308 E025", "0.35", 1.85, 1.65 , 1.1 , 1.04);
            this.DataContext = produit;
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            StartMethod();
        }

        private void StartMethod()
        {
            errormessage.Text = "";
            if (atoTextBox.Text == "")
            {
                atoTextBox.Focus();
            }
            else if (maiTextBox.Text == "")
            {
                maiTextBox.Focus();
            }
            else
            {
                double mai = Convert.ToDouble(maiTextBox.Text.Replace('.',','));
                double ato = Convert.ToDouble(atoTextBox.Text.Replace('.', ','));

                tries++;
                if (mai <= produit.max_mai && mai > produit.min_mai)
                {
                    if (ato <= produit.max_ato && ato > produit.min_ato)
                    {
                        isValide = true;
                        
                    }
                    else
                    {
                        isValide = false;
                        Score--;
                    }
                }
                else
                {
                    isValide = false;
                    Score--;
                }
                
                if(isValide)
                {
                    valide.Foreground = Brushes.ForestGreen;
                    valide.Content = "Validé";
                }
                else
                {
                    valide.Foreground = Brushes.OrangeRed;
                    valide.Content = "Non valide";
                }

                resultat.Content = Score;

                if(isValide || tries == 10)
                    addResult();
            }
            
        }

        private void addResult()
        {
            Test test = new Test();
            test.date = DateTime.Today;
            test.type = "Mesure";
            test.id_form = formateur.Id;
            test.id_op = operateur.Id;
            test.nom_test = "Mesure";
            test.resultat = Score;
            if (affichage == "Valide" )
            {
                test.passed = true;
            }
            else
            {
                test.passed = false;
            }
            test.niveau = "-";

            using (var unitOfWork = new UnitOfWork(new yazakiDBEntities()))
            {
                unitOfWork.Tests.Add(test);
                try
                {
                    unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AtoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            atoTextBox.IsReadOnly = true;
            atoTextBox.Focusable = false;
        }

        private void MaiTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            atoTextBox.IsReadOnly = true;
            maiTextBox.Focusable = false;
        }

    }
}