﻿using System.Collections.Generic;
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
            List<Operateurs> ops = new List<Operateurs>();
            ops.Add(op);
            DataContext = ops;
            opCombo.SelectedIndex = 0;
            opCombo.IsEnabled = false;
            formateur = form;
        }

        public UserControlTest(Formateurs form )
        {
            InitializeComponent();
            formateur = form;
            bindComboBox();
        }

        private void bindComboBox()
        {
            using (var unitOfWork = new UnitOfWork(new yazakiDBEntities()))
            {
                IEnumerable<Operateurs> ops = unitOfWork.Operateurs.GetAll();
                DataContext = ops;
            }
        }

        private void startButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(opCombo.SelectedIndex == -1 )
            {
                errormessage.Text = "Inserez tout les champs";
                
            }else if (type.SelectedIndex == -1)
            {
                errormessage.Text = "Inserez tout les champs";
            }else if (niveau.SelectedIndex == -1 &&  type.Text != "Mesure")
            {
                errormessage.Text = "Inserez tout les champs";
            }
            else
            {
                operateur = opCombo.SelectedItem as Operateurs;
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
                else if (type.Text == "Mesure")
                {
                    new TestMesure(operateur, formateur).Show();
                }
            }
            
        }

        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (e.AddedItems[0] as ComboBoxItem).Content as string;

            if(text == "Mesure")
            {
                niveau.Visibility = Visibility.Collapsed;
            }else if (niveau.Visibility == Visibility.Collapsed)
            {
                niveau.Visibility = Visibility.Visible;
            }
        }
    }
}
