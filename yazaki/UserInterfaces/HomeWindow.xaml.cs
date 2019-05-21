using System;
using System.Windows;
using System.Windows.Controls;
using yazaki.UserInterfaces.UserControls;

namespace yazaki.UserInterfaces
{
    /// <summary>
    /// Logique d'interaction pour HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        Formateurs user;
        public HomeWindow(Formateurs f)
        {
            InitializeComponent();
            GridMain.Children.Add(new UserControlAjouterC(f));
            user = f;
            userLabel.Text = f.nom+" "+f.prenom;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            GridCursor.Margin = new Thickness(10 + (180 * index), 0, 0, 0);
           
            switch (index)
            {
                case 0:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new UserControlAjouterC(user));
                    break;
                case 1:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new UserControlTest(user));
                    break;
                case 2:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new UserControlCandidats());
                    break;
                case 3:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new UserControlAjouterF());
                    break;
            }
        }

        private void logOutButton_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }
    }
}
