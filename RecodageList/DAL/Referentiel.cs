using System;
using System.Collections.Generic;
using System.Text;

namespace RecodageList.DAL
{
    public class Referentiel
    {
        public string Type { get; set; }
        public string TypeItem { get; set; }
        public string Code { get; set; }
        public string Lib { get; set; }
        public string CodeOrigine { get; set; }
        public bool InActif { get; set; }
        public string Cpl { get; set; }
        public string Cpl1 { get; set; }
        public string Cpl2 { get; set; }
        public string Cpl3 { get; set; }
        public string DateFinValidite { get; set; }
        public int IndiceLevenshtein { get; set; }
        public string Canonical { get; set; }
    }
}
