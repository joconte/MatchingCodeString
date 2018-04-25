using System;
using System.Collections.Generic;
using System.Text;
using RecodageList.DAL;
using System.Windows.Forms;

namespace RecodageList.BLL
{
    class Initialisation
    {
        public List<Correspondance> InitTableCorresp()
        {
            
            Correspondances Corr = new Correspondances();
            List<Correspondance> TableCorresp = Corr.ObtenirListeCorrespondance_SQLITE();
            
            return TableCorresp;
        }

        public List<CorrespondanceAffichage> InitTableCorrespAffichage(List<Correspondance> TableCorresp)
        {
            var source = new BindingSource();
            CorrespondancesAffichages CorrAffichage = new CorrespondancesAffichages();
            List<CorrespondanceAffichage> TableCorrespAffichage = CorrAffichage.ObtenirListeCorrespondanceAffichage(TableCorresp);
            
            return TableCorrespAffichage;
        }
    }
}
