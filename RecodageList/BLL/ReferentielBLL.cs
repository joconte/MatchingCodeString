using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecodageList.BLL;
using RecodageList.DAL;
using System.Windows.Forms;

namespace RecodageList.BLL
{
    public class ReferentielBLL
    {
        public string Type { get; set; }                //0
        public string TypeItem { get; set; }            //1
        public string Code { get; set; }                //2
        public string Lib { get; set; }                 //3
        public string CodeOrigine { get; set; }         //4
        public bool InActif { get; set; }               //5
        public string Cpl { get; set; }                 //6
        public string Cpl1 { get; set; }                //7
        public string Cpl2 { get; set; }                //8
        public string Cpl3 { get; set; }                //9
        public string DateFinValidite { get; set; }     //10
        public int IndiceLevenshtein { get; set; }      //11
        public string Canonical { get; set; }           //12
        public int FlagPreventiel { get; set; }         //13

        public override string ToString()
        {
            return "Ligne referentiel : \r\n" +
                "Type : " + Type + "\r\n" +
                "TypeItem : " + TypeItem + "\r\n" +
                "Code : " + Code + "\r\n" +
                "Lib : " + Lib + "\r\n" +
                "CodeOrigine : " + CodeOrigine + "\r\n" +
                "InActif : " + InActif + "\r\n" +
                "Cpl : " + Cpl + "\r\n" +
                "Cpl1 : " + Cpl1 + "\r\n" +
                "Cpl2 : " + Cpl2 + "\r\n" +
                "Cpl3 : " + Cpl3 + "\r\n" +
                "DateFinValidite : " + DateFinValidite + "\r\n" +
                "IndiceLevenshtein : " + IndiceLevenshtein + "\r\n" +
                "Canonical : " + Canonical + "\r\n" +
                "FlagPreventiel : " + FlagPreventiel;
        }

        public List<ReferentielBLL> FiltrerListeReferentiel_parCPL(List<ReferentielBLL> ReferentielFULL, string CPL)
        {
            string[] CPL123 = CPL.Split('|');
            List<ReferentielBLL> ReferentielFiltre = new List<ReferentielBLL>();

            if (CPL != "135|0|0|NOMEN" && CPL != "141|0|0|NOMEN" && CPL != "1000|0|0|CTRL")
            {
                for (int i = 0; i < ReferentielFULL.Count; i++)
                {
                    if (ReferentielFULL[i].Cpl1 == null || ReferentielFULL[i].Cpl1 == "")
                    {
                        ReferentielFULL[i].Cpl1 = "0";
                    }
                    if (ReferentielFULL[i].Cpl2 == null || ReferentielFULL[i].Cpl2 == "")
                    {
                        ReferentielFULL[i].Cpl2 = "0";
                    }
                    //Console.WriteLine("ReferentielFULL[i].Cpl : " + ReferentielFULL[i].Cpl + ". CPL123[0] : " + CPL123[0]);
                    //Console.WriteLine("ReferentielFULL[i].Cpl1 : " + ReferentielFULL[i].Cpl1 + ". CPL123[1] : " + CPL123[1]);
                    //Console.WriteLine("ReferentielFULL[i].Cpl2 : " + ReferentielFULL[i].Cpl2 + ". CPL123[2] : " + CPL123[2]);
                    //Console.WriteLine("ReferentielFULL[i].Type : " + ReferentielFULL[i].Type + ". CPL123[3] : " + CPL123[3]);
                    if (ReferentielFULL[i].Cpl == CPL123[0] && ReferentielFULL[i].Cpl1 == CPL123[1] && ReferentielFULL[i].Cpl2 == CPL123[2] && ReferentielFULL[i].Type == CPL123[3])
                    {
                        //Console.WriteLine("ReferentielFULL[i].Cpl : " + ReferentielFULL[i].Cpl + ". CPL123[0] : " + CPL123[0]);
                        //Console.WriteLine("ReferentielFULL[i].Cpl1 : " + ReferentielFULL[i].Cpl1 + ". CPL123[1] : " + CPL123[1]);
                        //Console.WriteLine("ReferentielFULL[i].Cpl2 : " + ReferentielFULL[i].Cpl2 + ". CPL123[2] : " + CPL123[2]);
                        ReferentielFiltre.Add(ReferentielFULL[i]);
                        //Console.WriteLine("J'ajoute la ligne " + i + " de la liste");
                    }
                    if (ReferentielFULL[i].Cpl == CPL123[0] && ReferentielFULL[i].Type == CPL123[3]
                        && CPL123[0] == "2" && CPL123[3] == "CTRL")
                    {
                        ReferentielFiltre.Add(ReferentielFULL[i]);
                    }
                }
            }
            else if (CPL == "135|0|0|NOMEN")
            {
                for (int i = 0; i < ReferentielFULL.Count; i++)
                {
                    //Console.WriteLine("CorrespFULL[i].Cpl : " + CorrespFULL[i].Cpl + ". CPL : " + CPL);
                    if ((ReferentielFULL[i].Cpl == CPL123[0] && ReferentielFULL[i].Type == CPL123[3])
                        || (ReferentielFULL[i].Cpl == "50" && ReferentielFULL[i].Type == "CTRL"))
                    {
                        ReferentielFiltre.Add(ReferentielFULL[i]);
                        //if (ReferentielFULL[i].Cpl == "50")
                            //Console.WriteLine(".");
                        //Console.WriteLine("J'ajoute la ligne " + i + " de la liste");
                    }
                }
                ReferentielFiltre = ReferentielFiltre.OrderBy(q => q.Code).ToList();
                //ListaServizi = ListaServizi.OrderBy(q => q).ToList();
            }
            else if (CPL == "141|0|0|NOMEN")
            {
                for (int i = 0; i < ReferentielFULL.Count; i++)
                {
                    //Console.WriteLine("CorrespFULL[i].Cpl : " + CorrespFULL[i].Cpl + ". CPL : " + CPL);
                    if ((ReferentielFULL[i].Cpl == CPL123[0] && ReferentielFULL[i].Type == CPL123[3])
                        || (ReferentielFULL[i].Cpl == "60" && ReferentielFULL[i].Type == "CTRL"))
                    {
                        ReferentielFiltre.Add(ReferentielFULL[i]);
                        //if (ReferentielFULL[i].Cpl == "60")
                            //Console.WriteLine(".");
                        //Console.WriteLine("J'ajoute la ligne " + i + " de la liste");
                    }
                }
                ReferentielFiltre = ReferentielFiltre.OrderBy(q => q.Code).ToList();
                //ListaServizi = ListaServizi.OrderBy(q => q).ToList();
            }
            else if (CPL == "1000|0|0|CTRL")
            {
                for(int i=0; i<ReferentielFULL.Count;i++)
                {
                    if(ReferentielFULL[i].Cpl == "3" && ReferentielFULL[i].Type == "CTRL")
                    {
                        ReferentielFiltre.Add(ReferentielFULL[i]);
                    }
                }
            }
            return (ReferentielFiltre);
        }

        public List<ReferentielBLL> TrierListeParLevenshtein(List<ReferentielBLL> ReferentielFiltrerCPL, string LibelleARapprocher, int MaxLevenshtein = 999)
        {
            List<ReferentielBLL> ReferentielOrderByLevenshtein = ReferentielFiltrerCPL;

            Fonc fonc = new Fonc();
            for (int i = 0; i < ReferentielFiltrerCPL.Count; i++)
            {
                //ReferentielOrderByLevenshtein[i].IndiceLevenshtein = fonc.DamerauLevenshteinDistance(fonc.CanonicalString(ReferentielOrderByLevenshtein[i].Lib).Trim(), fonc.CanonicalString(LibelleARapprocher).Trim(),MaxLevenshtein);
                ReferentielOrderByLevenshtein[i].IndiceLevenshtein = fonc.iLD(fonc.CanonicalString(ReferentielOrderByLevenshtein[i].Lib).Trim(), fonc.CanonicalString(LibelleARapprocher).Trim());

            }
            ReferentielOrderByLevenshtein.Sort(delegate (ReferentielBLL a1, ReferentielBLL a2) { return a1.IndiceLevenshtein - a2.IndiceLevenshtein; }); // tri par le champ i
            return (ReferentielOrderByLevenshtein);
        }

        public List<ReferentielBLL> FiltrerListeResultatExamenReferentielParExamen(List<ReferentielBLL> ReferentielExamenEtResultat, string Examen)
        {
            List<ReferentielBLL> ReferentielResultatFiltre = new List<ReferentielBLL>();
            for (int i = 0; i < ReferentielExamenEtResultat.Count; i++)
            {
                string test = ReferentielExamenEtResultat[i].Cpl1;

                if (Examen != null && ReferentielExamenEtResultat[i].Cpl1.Trim() == Examen.Trim())
                {
                    ReferentielResultatFiltre.Add(ReferentielExamenEtResultat[i]);
                }
            }
            return (ReferentielResultatFiltre);
        }

        public List<ReferentielBLL> SupprimeResultatExamenReferentiel(List<ReferentielBLL> ReferentielExamenEtResultat)
        {
            List<ReferentielBLL> ReferentielSansExamen = new List<ReferentielBLL>();
            for (int i = 0; i < ReferentielExamenEtResultat.Count; i++)
            {
                if (ReferentielExamenEtResultat[i].Cpl != "50")
                {
                    ReferentielSansExamen.Add(ReferentielExamenEtResultat[i]);
                }
            }
            return ReferentielSansExamen;
        }

        public ReferentielBLL TrouveLigneReferentiel(string Type, string Code, string Lib, string Cpl, string Cpl1, string Cpl2 , List<ReferentielBLL> Listereferentiel)
        {
            ReferentielBLL ligne_a_retourner = new ReferentielBLL();
            for (int i=0; i<Listereferentiel.Count; i++)
            {
                if(Listereferentiel[i].Type == Type 
                    && Listereferentiel[i].Code == Code
                    && Listereferentiel[i].Lib == Lib
                    && Listereferentiel[i].Cpl == Cpl
                    && Listereferentiel[i].Cpl1 == Cpl1
                    && Listereferentiel[i].Cpl2 == Cpl2)
                {
                    ligne_a_retourner = Listereferentiel[i];
                    return ligne_a_retourner;
                }
            }
            return ligne_a_retourner;
        }

        public ReferentielBLL TrouveLigneReferentiel_ByLigneRef(ReferentielBLL ligne_referentiel_a_trouver, List<ReferentielBLL> Listereferentiel)
        {
            ReferentielBLL ligne_a_retourner = new ReferentielBLL();
            for (int i = 0; i < Listereferentiel.Count; i++)
            {
                if (Listereferentiel[i].Type == ligne_referentiel_a_trouver.Type
                    && Listereferentiel[i].Code == ligne_referentiel_a_trouver.Code
                    && Listereferentiel[i].Lib == ligne_referentiel_a_trouver.Lib
                    && Listereferentiel[i].Cpl == ligne_referentiel_a_trouver.Cpl
                    && Listereferentiel[i].Cpl1 == ligne_referentiel_a_trouver.Cpl1
                    && Listereferentiel[i].Cpl2 == ligne_referentiel_a_trouver.Cpl2)
                {
                    ligne_a_retourner = Listereferentiel[i];
                    return ligne_a_retourner;
                }
            }
            return ligne_a_retourner;
        }

        public ReferentielBLL TrouveLigneReferentiel_ByLigneCorresp(CorrespondanceBLL ligneCorrespondance, List<ReferentielBLL> Listereferentiel)
        {
            ReferentielBLL ligne_a_retourner = new ReferentielBLL();
            for (int i = 0; i < Listereferentiel.Count; i++)
            {
                if(ligneCorrespondance.Cpl=="50")
                {
                    string[] examen_ancien_code = ligneCorrespondance.Ancien_Code.Split('#');

                    //Retourne le nouveau code de l'examen parent du resultat examen, à partir de l'ancien code examen présent dans l'ancien code du résultat examen.
                    CorrespondanceBLL ligne_exam_nouveau_code = new CorrespondanceBLL();
                    ligne_exam_nouveau_code = ligne_exam_nouveau_code.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, examen_ancien_code[0], "135|0|0|NOMEN");
                    ligneCorrespondance.Cpl1 = ligne_exam_nouveau_code.Nouveau_Code;
                }
                else if (ligneCorrespondance.Cpl == "60")
                {
                    string[] vaccin_ancien_code = ligneCorrespondance.Ancien_Code.Split('#');

                    //Retourne le nouveau code de l'examen parent du resultat examen, à partir de l'ancien code examen présent dans l'ancien code du résultat examen.
                    CorrespondanceBLL ligne_vaccin_nouveau_code = new CorrespondanceBLL();
                    ligne_vaccin_nouveau_code = ligne_vaccin_nouveau_code.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, vaccin_ancien_code[0], "141|0|0|NOMEN");
                    ligneCorrespondance.Cpl1 = ligne_vaccin_nouveau_code.Nouveau_Code;
                }
                else if(ligneCorrespondance.TypeRef == "CTRL" && ligneCorrespondance.Cpl == "2")
                {
                    ligneCorrespondance.Cpl1 = "2";
                }
                if (Listereferentiel[i].Type == ligneCorrespondance.TypeRef
                    && Listereferentiel[i].Code == ligneCorrespondance.Nouveau_Code
                    && Listereferentiel[i].Lib == ligneCorrespondance.Libelle_Nouveau_Code
                    && Listereferentiel[i].Cpl == ligneCorrespondance.Cpl
                    && Listereferentiel[i].Cpl1 == ligneCorrespondance.Cpl1
                    && Listereferentiel[i].Cpl2 == ligneCorrespondance.Cpl2)
                {
                    ligne_a_retourner = Listereferentiel[i];
                    return ligne_a_retourner;
                }
            }
            return ligne_a_retourner;
        }

        public ReferentielBLL CreerLigneReferentiel_ByLigneCorresp(CorrespondanceBLL ligneCorrespondance)
        {
            ReferentielBLL ligne_referentiel_by_corresp = new ReferentielBLL();
            ligne_referentiel_by_corresp.Type = ligneCorrespondance.TypeRef;
            ligne_referentiel_by_corresp.Cpl = ligneCorrespondance.Cpl;
            if (ligneCorrespondance.Cpl == "50")
            {
                string[] examen_ancien_code = ligneCorrespondance.Ancien_Code.Split('#');

                //Retourne le nouveau code de l'examen parent du resultat examen, à partir de l'ancien code examen présent dans l'ancien code du résultat examen.
                CorrespondanceBLL ligne_exam_nouveau_code = new CorrespondanceBLL();
                ligne_exam_nouveau_code = ligne_exam_nouveau_code.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, examen_ancien_code[0], "135|0|0|NOMEN");
                ligne_referentiel_by_corresp.Cpl1 = ligne_exam_nouveau_code.Nouveau_Code;
            }
            else if (ligneCorrespondance.Cpl == "60")
            {
                string[] vaccin_ancien_code = ligneCorrespondance.Ancien_Code.Split('#');

                //Retourne le nouveau code de l'examen parent du resultat examen, à partir de l'ancien code examen présent dans l'ancien code du résultat examen.
                CorrespondanceBLL ligne_vaccin_nouveau_code = new CorrespondanceBLL();
                ligne_vaccin_nouveau_code = ligne_vaccin_nouveau_code.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, vaccin_ancien_code[0], "141|0|0|NOMEN");
                ligne_referentiel_by_corresp.Cpl1 = ligne_vaccin_nouveau_code.Nouveau_Code;
            }
            else if (ligneCorrespondance.TypeRef == "CTRL" && ligneCorrespondance.Cpl == "2")
            {
                ligne_referentiel_by_corresp.Cpl1 = "2";
            }
            else
            {
                ligne_referentiel_by_corresp.Cpl1 = ligneCorrespondance.Cpl1;
            }
            ligne_referentiel_by_corresp.Cpl2 = ligneCorrespondance.Cpl2;
            ligne_referentiel_by_corresp.Code = ligneCorrespondance.Nouveau_Code;
            ligne_referentiel_by_corresp.Lib = ligneCorrespondance.Libelle_Nouveau_Code;
            ligne_referentiel_by_corresp.TypeItem = ligneCorrespondance.NomRef;
            ligne_referentiel_by_corresp.FlagPreventiel = ligneCorrespondance.FlagReferentiel;

            return ligne_referentiel_by_corresp;
        }

        public List<ReferentielBLL> SupprimeProtocoleVaccinal(List<ReferentielBLL> ReferentielVaccinEtProtocole)
        {
            List<ReferentielBLL> ReferentielSansProtocole = new List<ReferentielBLL>();
            for (int i = 0; i < ReferentielVaccinEtProtocole.Count; i++)
            {
                if (ReferentielVaccinEtProtocole[i].Cpl != "60")
                {
                    ReferentielSansProtocole.Add(ReferentielVaccinEtProtocole[i]);
                }
            }
            return ReferentielSansProtocole;
        }

        public List<ReferentielBLL> SupprimeVaccin(List<ReferentielBLL> ReferentielVaccinEtProtocole)
        {
            List<ReferentielBLL> ReferentielSansVaccin = new List<ReferentielBLL>();
            for (int i = 0; i < ReferentielVaccinEtProtocole.Count; i++)
            {
                if (ReferentielVaccinEtProtocole[i].Cpl != "141")
                {
                    ReferentielSansVaccin.Add(ReferentielVaccinEtProtocole[i]);
                }
            }
            return ReferentielSansVaccin;
        }


        public List<ReferentielBLL> FiltreReferentielParChaineRecherche(List<ReferentielBLL> ReferentielNonFiltre, string chainerecherche)
        {
            Fonc fonc = new Fonc();
            List<ReferentielBLL> ReferentielFiltre = new List<ReferentielBLL>();
            for (int i = 0; i < ReferentielNonFiltre.Count; i++)
            {
                string canonicalref = fonc.CanonicalString(ReferentielNonFiltre[i].Lib);
                string canonicalrecherche = fonc.CanonicalString(chainerecherche);
                if (canonicalref.Contains(canonicalrecherche))
                {
                    ReferentielFiltre.Add(ReferentielNonFiltre[i]);
                }
            }
            return ReferentielFiltre;
        }

        public List<ReferentielBLL> FiltreReferentielParCodeRecherche(List<ReferentielBLL> ReferentielNonFiltre, string chainerecherche)
        {
            Fonc fonc = new Fonc();
            List<ReferentielBLL> ReferentielFiltre = new List<ReferentielBLL>();
            for (int i = 0; i < ReferentielNonFiltre.Count; i++)
            {
                string canonicalref = fonc.CanonicalString(ReferentielNonFiltre[i].Code);
                string canonicalrecherche = fonc.CanonicalString(chainerecherche);
                if (canonicalref.Contains(canonicalrecherche))
                {
                    ReferentielFiltre.Add(ReferentielNonFiltre[i]);
                }
            }
            return ReferentielFiltre;
        }

        public List<ReferentielBLL> RetourneListeReferentielUpdater(List<ReferentielBLL> RefNonUpdater,
            ReferentielBLL ligneAUpdater, string Type, string Cpl, string Cpl1, string Cpl2)
        {
            List<ReferentielBLL> RefUpdater = new List<ReferentielBLL>();
            for (int i = 0; i < RefNonUpdater.Count; i++)
            {
                if (RefNonUpdater[i].Code == ligneAUpdater.Code &&
                    RefNonUpdater[i].Lib == ligneAUpdater.Lib
                    && Type == ligneAUpdater.Type
                    && Cpl == ligneAUpdater.Cpl
                    && Cpl1 == ligneAUpdater.Cpl1
                    && Cpl2 == ligneAUpdater.Cpl2)
                {
                    RefNonUpdater[i].Code = ligneAUpdater.Code;
                    RefNonUpdater[i].Lib = ligneAUpdater.Lib;
                    RefNonUpdater[i].FlagPreventiel = ligneAUpdater.FlagPreventiel;
                }
            }
            RefUpdater = RefNonUpdater;
            return RefUpdater;
        }

        public List<ReferentielBLL> SuppressionItem_RetourneListeReferentiel(List<ReferentielBLL> RefNonUpdater, ReferentielBLL ligne_a_supprimer)
        {
            List<ReferentielBLL> RefUpdater = new List<ReferentielBLL>();
            for (int i = 0; i < RefNonUpdater.Count; i++)
            {
                if (RefNonUpdater[i].Cpl1 == null || RefNonUpdater[i].Cpl1 == "")
                {
                    RefNonUpdater[i].Cpl1 = "0";
                }
                if (ligne_a_supprimer.Cpl1 == null || ligne_a_supprimer.Cpl1 == "")
                {
                    ligne_a_supprimer.Cpl1 = "0";
                }

                if (RefNonUpdater[i].Cpl2 == null || RefNonUpdater[i].Cpl2 == "")
                {
                    RefNonUpdater[i].Cpl2 = "0";
                }
                if (ligne_a_supprimer.Cpl2 == null || ligne_a_supprimer.Cpl2 == "")
                {
                    ligne_a_supprimer.Cpl2 = "0";
                }

                if (RefNonUpdater[i].Code != ligne_a_supprimer.Code)
                {
                    RefUpdater.Add(RefNonUpdater[i]);
                }
                else if (RefNonUpdater[i].Code == ligne_a_supprimer.Code
                    && RefNonUpdater[i].Cpl == ligne_a_supprimer.Cpl
                     && RefNonUpdater[i].Cpl1 == ligne_a_supprimer.Cpl1
                     && RefNonUpdater[i].Cpl2 == ligne_a_supprimer.Cpl2)
                {
                    Console.WriteLine("Je ne garde pas la ligne");
                }
                else
                {
                    RefUpdater.Add(RefNonUpdater[i]);
                }
            }
            return RefUpdater;
        }

        public bool CodeEstDansLeReferentiel(List<ReferentielBLL> Referentiel, string Code)
        {
            bool estDansLeReferentiel = false;
            int i = 0;
            while (i < Referentiel.Count && estDansLeReferentiel == false)
            {
                if (Referentiel[i].Code == Code)
                {
                    estDansLeReferentiel = true;
                }
                i++;
            }
            return estDansLeReferentiel;
        }

        public List<ReferentielBLL> RetourneListeResExamByExam(List<ReferentielBLL> Referentiel, string NouveauCodeExam)
        {
            List<ReferentielBLL> ReferentielResExamByExam = new List<ReferentielBLL>();
            try
            {
                for (int i = 0; i < Referentiel.Count; i++)
                {
                    if (NouveauCodeExam != null && Referentiel[i].Cpl1.Trim() == NouveauCodeExam.Trim() && Referentiel[i].FlagPreventiel == 2)
                    {
                        ReferentielResExamByExam.Add(Referentiel[i]);
                    }
                }
                return (ReferentielResExamByExam);
            }
            catch (Exception e)
            {
                MessageBox.Show("Debug : Erreur dans Referentiels.RetourneListeResExamByExam() : \r\n" +
                    "Message d'erreur : " + e.Message);
                throw;
            }
        }

        public List<ReferentielBLL> SupprimeResExamByExam_RetourneTableRef(List<ReferentielBLL> Referentiel, List<ReferentielBLL> LignesResExamASupprimer)
        {
            ReferentielDAL RefObjectDAL = new ReferentielDAL();
            RefObjectDAL.DeleteFromSQLITE_TBReferentiel(LignesResExamASupprimer);
            for (int i = 0; i < LignesResExamASupprimer.Count; i++)
            {
                Referentiel = SuppressionItem_RetourneListeReferentiel(Referentiel, LignesResExamASupprimer[i]);
            }
            return (Referentiel);
        }

        public ReferentielBLL RetourneLigneReferentiel_ByDatagrid(DataGridView dataGridView_ref, DataGridViewCell Cell)
        {
            ReferentielBLL ligne_referentiel_selectionne_datagrid = new ReferentielBLL();
            try
            { ligne_referentiel_selectionne_datagrid.Type = dataGridView_ref.CurrentRow.Cells[0].Value.ToString(); }
            catch (Exception e) { Console.WriteLine(e.Message); }
            try
            { ligne_referentiel_selectionne_datagrid.Code = dataGridView_ref.CurrentRow.Cells[2].Value.ToString(); }
            catch (Exception e) { Console.WriteLine(e.Message); }
            try
            { ligne_referentiel_selectionne_datagrid.Lib = dataGridView_ref.CurrentRow.Cells[3].Value.ToString(); }
            catch(Exception e) { Console.WriteLine(e.Message); }
            ligne_referentiel_selectionne_datagrid.Cpl = dataGridView_ref.CurrentRow.Cells[6].Value.ToString();
            if (dataGridView_ref.CurrentRow.Cells[7].Value == null)
            {
                ligne_referentiel_selectionne_datagrid.Cpl1 = "0";
            }
            else
            {
                ligne_referentiel_selectionne_datagrid.Cpl1 = dataGridView_ref.CurrentRow.Cells[7].Value.ToString();
            }
            if (dataGridView_ref.CurrentRow.Cells[8].Value == null)
            {
                ligne_referentiel_selectionne_datagrid.Cpl2 = "0";
            }
            else
            {
                ligne_referentiel_selectionne_datagrid.Cpl2 = dataGridView_ref.CurrentRow.Cells[8].Value.ToString();
            }
            try
            {
                ligne_referentiel_selectionne_datagrid.InActif = (bool)dataGridView_ref.CurrentRow.Cells[5].Value;
            }
            catch(Exception e) { Console.WriteLine(e.Message); }
            return ligne_referentiel_selectionne_datagrid;
        }

        
    }
}
