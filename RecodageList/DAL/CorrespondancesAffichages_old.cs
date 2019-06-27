using System;
using System.Collections.Generic;
using System.Text;

namespace RecodageList.DAL
{
    class CorrespondancesAffichages_old
    {
        public List<CorrespondanceAffichage> ObtenirListeCorrespondanceAffichage(List<Correspondance> TableCorrespFULL)
        {
            Console.WriteLine("J'arrive dans l'affichage");
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

        public List<CorrespondanceAffichage> RetourneListeCorrespondanceAffichageUpdater(List<CorrespondanceAffichage> CorrespAffichageNonUpdater,
            Correspondance ligneAUpdater,string NomRef)
        {
            List<CorrespondanceAffichage> CorrespUpdater = new List<CorrespondanceAffichage>();
            for(int i = 0; i<CorrespAffichageNonUpdater.Count;i++)
            {
                if(CorrespAffichageNonUpdater[i].Ancien_Code == ligneAUpdater.Ancien_Code && 
                    CorrespAffichageNonUpdater[i].Libelle_Ancien_Code == ligneAUpdater.Libelle_Ancien_Code &&
                    NomRef == ligneAUpdater.NomRef)
                {
                    CorrespAffichageNonUpdater[i].Nouveau_Code = ligneAUpdater.Nouveau_Code;
                    CorrespAffichageNonUpdater[i].Libelle_Nouveau_Code = ligneAUpdater.Libelle_Nouveau_Code;
                }
            }
            CorrespUpdater = CorrespAffichageNonUpdater;
            return CorrespUpdater;
        }

    }
}
