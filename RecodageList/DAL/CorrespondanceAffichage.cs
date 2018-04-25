using System;
using System.Collections.Generic;
using System.Text;

namespace RecodageList.DAL
{
    class CorrespondanceAffichage
    {
        public string Ancien_Code { get; set; }
        public string Libelle_Ancien_Code { get; set; }
        public string Nouveau_Code { get; set; }
        public string Libelle_Nouveau_Code { get; set; }
        public int Occurrence { get; set; }
        public DateTime DateRecensement { get; set; }
    }
}
