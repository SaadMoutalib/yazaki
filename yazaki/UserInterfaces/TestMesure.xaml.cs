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

        public TestMesure(Operateurs op, Formateurs form)
        {
            InitializeComponent();
            formateur = form;
            operateur = op;
            nomLbl.Content = op.FullName;
            IDLbl.Content = op.Id;
            

            produits = new List<Produit>();
            Produit p1 = new Produit("7116-4288-(02) SB", "1308 E025", "0.35", 1.85, 1.65 , 1.1 , 1.04);
            Produit p2 = new Produit("7116-4285-(02) SB", "1308 F025", "0.35", 1.85, 0.9, 1.1, 0.9);

            produits.Add(p1);
            produits.Add(p2);
            DataContext = produits;
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            StartMethod();
        }

        private void StartMethod()
        {
            errormessage.Text = "";
            if (refCombo.Text == "")
            {
                refCombo.Focus();
                errormessage.Text = "Entrez Reference Terminal";
            }
            else if(codeCombo.Text == "")
            {
                codeCombo.Focus();
                errormessage.Text = "Entrez Code inventaire d'applicateur";
            }
            else if (secCombo.Text == "")
            {
                secCombo.Focus();
                errormessage.Text = "Entrez Section de fil";
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
                Produit prod = refCombo.SelectedItem as Produit;

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
                    resultat.Content = affichage;
                    addResult();
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
            test.niveau = "Debutant";

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
            atoTextBox.Focusable = false;
            atoTextBox.IsReadOnly = true;
        }

        private void MaiTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            maiTextBox.Focusable = false;
            maiTextBox.IsReadOnly = true;
        }
    }
}
