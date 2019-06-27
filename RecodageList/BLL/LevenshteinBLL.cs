using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RecodageList.BLL
{
    public class LevenshteinBLL
    {
        public string Ancien_code { get; set; }
        public string Libelle_ancien_code_canonique { get; set; }
        public string Cpl { get; set; }
        public string Cpl1 { get; set; }
        public string Cpl2 { get; set; }
        public string Nomref { get; set; }
        public string CodeReferentiel { get; set; }
        public string Libellereferentiel_canonique { get; set; }
        public int ScoreLevenshtein { get; set; }

        public void InitALLLevenshtein(List<CorrespondanceBLL> TableCorrespondance, List<ReferentielBLL> TableReferentiel, List<ComboBoxFiltreBLL> ListeModule)
        {
            CorrespondanceBLL CorrObject = new CorrespondanceBLL();
            ReferentielBLL RefObject = new ReferentielBLL();
            List<LevenshteinBLL> ListeCompleteDesLevenshtein = new List<LevenshteinBLL>();
            for (int i=0;i<ListeModule.Count;i++)
            {
                //Console.WriteLine("Module : " + ListeModule[i].ToString());
                List<CorrespondanceBLL> TableCorrespondanceFiltre = new List<CorrespondanceBLL>();
                List<ReferentielBLL> TableReferentielFiltre = new List<ReferentielBLL>();
                List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();
                TableCorrespondanceFiltre  = CorrObject.FiltrerListeCorrespondance_parCPL(TableCorrespondance,ListeModule[i].Cpl);
                TableReferentielFiltre = RefObject.FiltrerListeReferentiel_parCPL(TableReferentiel, ListeModule[i].Cpl);
                for(int j=0;j<TableCorrespondance.Count;j++)
                {
                    TableRefLevenshtein = RefObject.TrierListeParLevenshtein(TableReferentielFiltre, TableCorrespondance[j].Libelle_Ancien_Code);
                    InsertLevenshteinByTableRef_Corresp(ref ListeCompleteDesLevenshtein, TableReferentielFiltre, TableCorrespondance[i]);
                }
            }
        }

        public void InsertLevenshteinByTableRef_Corresp(ref List<LevenshteinBLL> ListeCompleteDesLevenshtein , List<ReferentielBLL> TableReferentielLevenshtein, CorrespondanceBLL LigneCorresp)
        {
            for(int i=0;i<TableReferentielLevenshtein.Count;i++)
            {
                LevenshteinBLL ligne_a_ajouter = new LevenshteinBLL();
                ligne_a_ajouter.Ancien_code = LigneCorresp.Ancien_Code;
                ligne_a_ajouter.Libelle_ancien_code_canonique = LigneCorresp.Canonical;
                ligne_a_ajouter.Cpl = LigneCorresp.Cpl;
                ligne_a_ajouter.Cpl1 = LigneCorresp.Cpl1;
                ligne_a_ajouter.Cpl2 = LigneCorresp.Cpl2;
                ligne_a_ajouter.Nomref = LigneCorresp.NomRef;
                ligne_a_ajouter.CodeReferentiel = TableReferentielLevenshtein[i].Code;
                ligne_a_ajouter.Libellereferentiel_canonique = TableReferentielLevenshtein[i].Canonical;
                ligne_a_ajouter.ScoreLevenshtein = TableReferentielLevenshtein[i].IndiceLevenshtein;
                ListeCompleteDesLevenshtein.Add(ligne_a_ajouter);
            }
        }
    }
}
