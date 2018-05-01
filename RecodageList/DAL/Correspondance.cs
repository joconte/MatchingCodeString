using System;
using System.Collections.Generic;
using System.Text;

namespace RecodageList.DAL
{
    public class Correspondance
    {
        public string TypeItem { get; set; }
        public string Ancien_Code { get; set; }
        public string Libelle_Ancien_Code { get; set; }
        public bool AncienCodeActif { get; set; }
        public string Nouveau_Code { get; set; }
        public string Libelle_Nouveau_Code { get; set; }
        public bool Code_utilise { get; set; }
        public int Occurrence { get; set; }
        public string NomSchema { get; set; }
        public DateTime DateRecensement { get; set; }
        public DateTime DateMAJ { get; set; }
        public string TypeRecodage { get; set; }
        public bool NouveauCodeInactif { get; set; }
        public string UtilisateurCreation { get; set; }
        public string TypeRef { get; set; }
        public string NomRef { get; set; }
        public string Cpl { get; set; }
        public string Cpl1 { get; set; }
        public string Cpl2 { get; set; }
        public string Canonical { get; set; }
        public int FlagReferentiel { get; set; }


    }

}
