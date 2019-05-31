using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yazaki.Data
{
    public class Produit
    {
        public int reference { get; set; }
        public double max_ato { get; set; }
        public double min_ato { get; set; }
        public double max_mai { get; set; }
        public double min_mai { get; set; }

        public Produit(int _reference, double _max_ato, double _min_ato, double _max_mai, double _min_mai)
        {
            reference = _reference;
            max_ato = _max_ato;
            min_ato = _min_ato;
            max_mai = _max_mai;
            min_mai = _min_mai;
        }

        public Produit() { }

    }
}
