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

        
    }
}
