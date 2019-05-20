using System.Collections.Generic;
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
        public UserControlTest()
        {
            InitializeComponent();
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
            if(opCombo.SelectedIndex == -1 || type.SelectedIndex == -1 || niveau.SelectedIndex == -1)
            {
                errormessage.Text = "Inserez tout les champs";
            }
            else
            {
                var op = opCombo.SelectedItem as Operateurs;
                if (type.Text == "Insertion")
                {
                    new TestInsertion(niveau.Text, op).Show();
                }
            }
            
        }
    }
}
