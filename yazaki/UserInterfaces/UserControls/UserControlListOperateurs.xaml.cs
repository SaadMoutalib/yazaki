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
                List<Operateurs> ops = unitOfWork.Operateurs.GetAllObject("Coordonnee", "Tests.Formateur") as List<Operateurs>;
               
                opGrid.ItemsSource = ops;
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var operateur = opGrid.SelectedItem as Operateurs;
            if (operateur != null)
            {
                testGrid.ItemsSource = operateur.Tests;
                testGrid.Visibility = Visibility.Visible;
               
            }
        }

    }
}
