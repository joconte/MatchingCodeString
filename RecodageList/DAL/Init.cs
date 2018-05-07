using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecodageList.DAL
{
    public class Init
    {
        public static List<Referentiel> TableReferentiel { get; set; }
        public static List<Referentiel> TableReferentielFiltre { get; set; }
        public static List<Correspondance> TableCorrespondanceSansModification { get; set; }
        public static List<Correspondance> TableCorrespondance { get; set; }
        public static List<Correspondance> TableCorrespondanceFiltre { get; set; }
        public static List<ComboBoxFiltre> ComboBoxFiltre { get; set; }
        public static string SQLServerConnectionString { get; set; }
        public static bool ThreadStop { get; set; }
    }
}
