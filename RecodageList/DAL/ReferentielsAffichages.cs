using System;
using System.Collections.Generic;
using System.Text;

namespace RecodageList.DAL
{
    class ReferentielsAffichages
    {
        public List<ReferentielAffichage> ObtenirListeReferentielAffichage(List<Referentiel> TableReferentielFULL)
        {
            List<ReferentielAffichage> TableReferentielAffichage = new List<ReferentielAffichage>(TableReferentielFULL.Count);
            for (int i = 0; i < TableReferentielFULL.Count; i++)
            {
                ReferentielAffichage RefAffichage = new ReferentielAffichage();
                RefAffichage.Code = TableReferentielFULL[i].Code;
                RefAffichage.Lib = TableReferentielFULL[i].Lib;
                TableReferentielAffichage.Add(RefAffichage);
            }
            return (TableReferentielAffichage);
        }
    }
}
