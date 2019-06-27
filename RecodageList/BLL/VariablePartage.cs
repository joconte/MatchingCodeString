using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecodageList.BLL
{
    public class VariablePartage
    {
        public static List<ReferentielBLL> TableReferentiel { get; set; }
        public static List<ReferentielBLL> TableReferentielFiltre { get; set; }
        public static List<CorrespondanceBLL> TableCorrespondanceSansModification { get; set; }
        public static List<CorrespondanceBLL> TableCorrespondance { get; set; }
        public static List<CorrespondanceBLL> TableCorrespondanceFiltre { get; set; }
        public static List<ComboBoxFiltreBLL> ComboBoxFiltre { get; set; }
        public static string SQLServerConnectionString { get; set; }
        public static bool ThreadStop { get; set; }
        public static string CheminBaseClient { get; set; }
        public static bool BaseCharge { get; set; }
        public static string ClientEnCours { get; set; }
        public static List<String> ListeNomRef_all { get; set; }
        public static List<String> ListeNomRef_admin { get; set; }
        public static List<List<string>> ListeNomRef_Adm_Med { get; set; }
        public static int ThreadNumber { get; set; }
    }
}
