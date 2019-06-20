using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using yazaki.Data;

namespace yazaki.UserInterfaces.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UserControlTest.xaml
    /// </summary>
    public partial class UserControlTest : UserControl
    {
        private Operateurs operateur;
        private Formateurs formateur;

        public UserControlTest(Operateurs op,Formateurs form)
        {
            InitializeComponent();
            IdTextBox.Text = op.Id.ToString();
            FullName.Text = op.FullName;
            formateur = form;
        }

        public UserControlTest(Formateurs form )
        {
            InitializeComponent();
            formateur = form;
        }

        private Operateurs GetOperateurs()
        {
            Operateurs op = new Operateurs();
            int id = 0;
            if (Int32.TryParse(IdTextBox.Text, out int j))
                 id = j;
            using (var unitOfWork = new UnitOfWork(new yazakiDBEntities()))
            {
                op = unitOfWork.Operateurs.Find(s => s.Id == id);
            }

            return op;
        }

        private void startButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(IdTextBox.Text == "" && IdTextBox.Text.Contains(""))
            {
                errormessage.Text = "Inserez tout les champs";
                
            }else if (type.SelectedIndex == -1)
            {
                errormessage.Text = "Inserez tout les champs";
            }else if (niveau.SelectedIndex == -1 &&  type.Text != "Mesure" && type.Text != "Crimping Setup" && type.Text != "Cutting")
            {
                errormessage.Text = "Inserez tout les champs";
            }
            else
            {
                operateur = GetOperateurs();
                if (type.Text == "Insertion")
                {
                    new TestInsertion(niveau.Text, operateur,formateur).Show();
                }else if(type.Text == "Crimping")
                {
                    new TestCrimping(niveau.Text, operateur, formateur).Show();
                }
                else if (type.Text == "Taping")
                {
                    new TestTaping(niveau.Text, operateur, formateur).Show();
                }
                else if (type.Text == "Micrometre")
                {
                    new TestMesure(operateur, formateur).Show();
                }
                else if(type.Text == "Crimping Setup")
                {
                    new TestCrimpingSetup(operateur, formateur).Show();
                }
                else if(type.Text == "Cutting")
                {
                    new TestCutting(operateur, formateur).Show();
                }
            }
            
        }

        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (e.AddedItems[0] as ComboBoxItem).Content as string;

            if(text == "Mesure" || text == "Crimping Setup" || text == "Cutting")
            {
                niveau.Visibility = Visibility.Collapsed;
            }
            else if (niveau.Visibility == Visibility.Collapsed)
            {
                niveau.Visibility = Visibility.Visible;
            }
        }

        private void IdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Operateurs op = new Operateurs();
            op = GetOperateurs();
            if (op != null)
                FullName.Text = op.FullName;
            else
                FullName.Text = "";
        }

        private void IdTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
