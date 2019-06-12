using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yazaki.Data
{
    public class Produit
    {
        public string reference_Terminal { get; set; }
        public string Code_Inventaire_Applicateur { get; set; }
        public string Section_fil { get; set; }
        public double max_ato { get; set; }
        public double min_ato { get; set; }
        public double max_mai { get; set; }
        public double min_mai { get; set; }

        public Produit(string _reference_Terminal, string _Code_Inventaire_Applicateur, string _Section_fil , double _max_ato, double _min_ato, double _max_mai, double _min_mai)
        {
            reference_Terminal = _reference_Terminal;
            Section_fil = _Section_fil;
            Code_Inventaire_Applicateur = _Code_Inventaire_Applicateur;
            max_ato = _max_ato;
            min_ato = _min_ato;
            max_mai = _max_mai;
            min_mai = _min_mai;
        }

        public Produit() { }

    }
}
