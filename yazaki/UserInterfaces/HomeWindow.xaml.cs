using System;
using System.Windows;
using System.Windows.Controls;
using yazaki.Data;
using yazaki.UserInterfaces.UserControls;

namespace yazaki.UserInterfaces
{
    /// <summary>
    /// Logique d'interaction pour HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private Formateurs user;

        public HomeWindow(Formateurs f)
        {
            InitializeComponent();
            GridMain.Children.Add(new UserControlAjouterOperateur(f));
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
                    GridMain.Children.Add(new UserControlAjouterOperateur(user));
                    break;
                case 1:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new UserControlTest(user));
                    break;
                case 2:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new UserControlListOperateurs());
                    break;
                case 3:
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new UserControlAjouterFormateur());
                    break;
            }
        }

        private void logOutButton_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }

        private void optionsButton_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow option = new OptionsWindow();
            if (option.ShowDialog() == true)
            {
                Options.PORT = option.Port;
            }
        }
    }
}
