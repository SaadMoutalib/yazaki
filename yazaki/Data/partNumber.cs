using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yazaki.Data
{
    public class partNumber
    {
        public string SN_fil { get; set; }
        public string reference_Terminal_Gauche { get; set; }
        public string reference_Terminal_Droite { get; set; }
        public string reference_Applicateur_Gauche { get; set; }
        public string reference_Applicateur_Droite { get; set; }
        public string reference_Fil { get; set; }


        public partNumber(string _SN_fil, string _reference_Terminal_Gauche, string _reference_Terminal_Droite, string _reference_Applicateur_Gauche, string _reference_Applicateur_Droite, string _reference_Fil)
        {
            SN_fil = _SN_fil;
            reference_Terminal_Gauche = _reference_Terminal_Gauche;
            reference_Terminal_Droite = _reference_Terminal_Droite;
            reference_Applicateur_Gauche = _reference_Applicateur_Gauche;
            reference_Applicateur_Droite = _reference_Applicateur_Droite;
            reference_Fil = _reference_Fil;
        }
        public partNumber() { }   
    }
}
