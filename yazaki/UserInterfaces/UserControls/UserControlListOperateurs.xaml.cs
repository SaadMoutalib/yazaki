using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using yazaki.Data;

namespace yazaki.UserInterfaces.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UserControlresultats.xaml
    /// </summary>
    public partial class UserControlListOperateurs : UserControl
    {
        public UserControlListOperateurs()
        {
            InitializeComponent();
            using (var unitOfWork = new UnitOfWork(new yazakiDBEntities()))
            {
                List<Operateurs> ops = unitOfWork.Operateurs.GetAllObject("Coordonnee") as List<Operateurs>;
               
                opGrid.ItemsSource = ops;
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var operateur = opGrid.SelectedItem as Operateurs;
            if (operateur != null)
            {
                using (var unitOfWork = new UnitOfWork(new yazakiDBEntities()))
                {
                    List<Test> tests = unitOfWork.Tests.GetAllQuery(s => s.id_op == operateur.Id) as List<Test>;
                    if(tests != null)
                    {
                        testGrid.ItemsSource = tests;
                        testGrid.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        testGrid.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

    }
}
