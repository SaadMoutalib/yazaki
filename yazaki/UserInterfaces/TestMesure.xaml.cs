using System;
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
    public partial class TestMesure : Window
    {
        

        private Operateurs operateur;
        private Formateurs formateur;
        private int Score = 0;
        private string affichage= "";
        private List<Produit> produits;
        private int tries;

        public TestMesure(Operateurs op, Formateurs form)
        {
            InitializeComponent();
            formateur = form;
            operateur = op;
            nomLbl.Content = op.FullName;
            IDLbl.Content = op.Id;

            produits = new List<Produit>();
            Produit p1 = new Produit("7116-4288-(02) SB", "1308 E025", "0.35", 1.85, 1.65 , 1.1 , 1.04);
            Produit p2 = new Produit("7116-4225-(02) SB", "1308 F026", "0.36", 2.85, 1.59, 1.1, 0.9);
            Produit p3 = new Produit("7116-4878-(05) SB", "1308 E027", "0.39", 1.85, 1.65, 1.1, 1.04);
            Produit p4 = new Produit("7116-4285-(02) SB", "1308 F028", "0.37", 2.85, 1.59, 1.1, 0.9);
            Produit p5 = new Produit("7116-4785-(02) SB", "1308 F029", "0.42", 2.85, 1.59, 1.1, 0.9);

            produits.Add(p1);
            produits.Add(p2);
            produits.Add(p3);
            produits.Add(p4);
            produits.Add(p5);
            list.ItemsSource = produits;
            list.SelectedItem = p1;
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            StartMethod();
        }

        private void StartMethod()
        {
            
            errormessage.Text = "";
            if (list.SelectedItem as Produit == null)
            {
                errormessage.Text = "Selectionner un terminal";
            }
            else if (atoTextBox.Text == "")
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
                Produit prod = list.SelectedItem as Produit;
                ListBoxItem item = (ListBoxItem)list.ItemContainerGenerator.ContainerFromIndex(list.SelectedIndex);

                tries++;

                if (mai <= prod.max_mai && mai > prod.min_mai)
                {
                    if (ato <= prod.max_ato && ato > prod.min_ato)
                    {
                        affichage = "Valide";
                        Score++;
                    }
                    else
                    {
                        affichage = "Non Valide";
                    }
                }
                else
                {
                    affichage = "Non Valide";
                }
                
                if(affichage == "Valide")
                {
                    item.IsEnabled = false;
                    item.Background = Brushes.MediumSeaGreen;
                    valide.Foreground = Brushes.ForestGreen;
                }
                else
                {
                    item.IsEnabled = false;
                    item.Background = Brushes.IndianRed;
                    valide.Foreground = Brushes.OrangeRed;
                }

                valide.Content = affichage;
                resultat.Content = Score;
                if(tries < 5)
                    list.SelectedItem = list.Items[tries];
                if (tries == 5)
                {
                    addResult();
                    startButton.IsEnabled = false;
                }
                    
            }
            
        }

        private Produit GetProduit(string reference_Terminal)
        {
            Produit produit = new Produit();
            
            foreach(Produit prod in produits)
            {
                if (prod.reference_Terminal == reference_Terminal)
                {
                    produit = prod;
                    return produit;
                }
            }
            return null;
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
            CheckLoginWindow option = new CheckLoginWindow(formateur);
            option.Owner = this;
            if (option.ShowDialog() == true)
            {
                this.Close();
            }
        }

        private void AtoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            atoTextBox.IsReadOnly = true;
            if(maiTextBox.Text=="")
                maiTextBox.Focus();
            atoTextBox.Focusable = false;
        }

        private void MaiTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            maiTextBox.IsReadOnly = true;
            if (atoTextBox.Text == "")
                atoTextBox.Focus();
            maiTextBox.Focusable = false;
        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            maiTextBox.Text = "";
            atoTextBox.Text = "";
            atoTextBox.IsReadOnly = false;
            maiTextBox.IsReadOnly = false;
            atoTextBox.Focusable = true;
            maiTextBox.Focusable = true;
            atoTextBox.Focus();
        }
    }
}
