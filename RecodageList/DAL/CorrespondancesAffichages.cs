using System;
using System.Collections.Generic;
using System.Text;

namespace RecodageList.DAL
{
    class CorrespondancesAffichages
    {
        public List<CorrespondanceAffichage> ObtenirListeCorrespondanceAffichage(List<Correspondance> TableCorrespFULL)
        {
            List<CorrespondanceAffichage> TableCorrespAffichage = new List<CorrespondanceAffichage>(TableCorrespFULL.Count);
            for(int i=0; i<TableCorrespFULL.Count;i++)
            {
                CorrespondanceAffichage CorrespAffichage = new CorrespondanceAffichage();
                CorrespAffichage.Ancien_Code = TableCorrespFULL[i].Ancien_Code;
                CorrespAffichage.Libelle_Ancien_Code = TableCorrespFULL[i].Libelle_Ancien_Code;
                CorrespAffichage.Nouveau_Code = TableCorrespFULL[i].Nouveau_Code;
                CorrespAffichage.Libelle_Nouveau_Code = TableCorrespFULL[i].Libelle_Nouveau_Code;
                CorrespAffichage.Occurrence = TableCorrespFULL[i].Occurrence;
                TableCorrespAffichage.Add(CorrespAffichage);
            }
            return (TableCorrespAffichage);
        }
    }
}
