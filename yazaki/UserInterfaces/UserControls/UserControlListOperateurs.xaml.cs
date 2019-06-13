using System.Collections.Generic;
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
                List<Operateurs> ops = unitOfWork.Operateurs.GetAll() as List<Operateurs>;
                List<Coordonnees> coord = new List<Coordonnees>();

                foreach (Operateurs op in ops)
                {
                    coord.Add(op.Coordonnee);
                }
                dataGrid.ItemsSource = ops;
            }
        }
    }
}
