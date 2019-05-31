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
        private List<Produit> produits;

        public TestMesure(Operateurs op, Formateurs form)
        {
            InitializeComponent();
            formateur = form;
            operateur = op;
            nomLbl.Content = op.FullName;
            IDLbl.Content = op.Id;

            produits = new List<Produit>();
            Produit p1 = new Produit(1, 1, 0.5, 0.7, 0.3);
            Produit p2 = new Produit(2, 0.8, 0.6, 0.8, 0.4);
            Produit p3 = new Produit(3, 0.9, 0.3, 0.6, 0.2);
            Produit p4 = new Produit(4, 0.7, 0.4, 0.9, 0.5);
            Produit p5 = new Produit(5, 0.6, 0.5, 1, 0.4);
            produits.Add(p1);
            produits.Add(p2);
            produits.Add(p3);
            produits.Add(p4);
            produits.Add(p5);

            atoTextBox.IsReadOnly = true;
            maiTextBox.IsReadOnly = true;
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            StartMethod();
        }

        private void StartMethod()
        {
            if (refTextBox.Text == "")
            {
                refTextBox.Focus();

                errormessage.Text = "Entrez une réference";
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
                double mai = Convert.ToDouble(maiTextBox.Text);
                double ato = Convert.ToDouble(atoTextBox.Text);
                int reference = Convert.ToInt32(refTextBox.Text);

                Produit prod = GetProduit(reference);
                if(prod == null)
                {
                    errormessage.Text = "Réference introuvable";
                }
                else
                {
                    if (mai <= prod.max_mai && mai > prod.min_mai)
                    {
                        if (ato <= prod.max_ato && ato > prod.min_ato)
                        {
                            Score++;
                        }
                    }
                    resultat.Content = Score + "/5";
                    if (Score == 5)
                        addResult();
                }
               
            }
            
        }

        private Produit GetProduit(int reference)
        {
            Produit produit = new Produit();
            
            foreach(Produit prod in produits)
            {
                if (prod.reference == reference)
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
            if (Score > 2)
            {
                test.passed = true;
            }
            else
            {
                test.passed = false;
            }

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

        private void IntValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AtoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            atoTextBox.Focusable = false;
        }

        private void MaiTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            maiTextBox.Focusable = false;
        }
    }
}
