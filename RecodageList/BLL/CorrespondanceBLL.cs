using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecodageList.DAL;
using RecodageList.BLL;
using System.Windows.Forms;

namespace RecodageList.BLL
{
    /// <summary>
    /// Objet Correspondances global, couche métier.
    /// Contient toutes les méthodes associées à l'objet Correspondance.
    /// </summary>
    public class CorrespondanceBLL
    {
        public string TypeItem { get; set; }                        //0
        public string Ancien_Code { get; set; }                     //1
        public string Libelle_Ancien_Code { get; set; }             //2
        public bool AncienCodeActif { get; set; }                   //3
        public string Nouveau_Code { get; set; }                    //4
        public string Libelle_Nouveau_Code { get; set; }            //5
        public bool Code_utilise { get; set; }                      //6
        public int Occurrence { get; set; }                         //7
        public string NomSchema { get; set; }                       //8
        public DateTime DateRecensement { get; set; }               //9
        public DateTime DateMAJ { get; set; }                       //10
        public string TypeRecodage { get; set; }                    //11
        public bool NouveauCodeInactif { get; set; }                //12
        public string UtilisateurCreation { get; set; }             //13
        public string TypeRef { get; set; }                         //14
        public string NomRef { get; set; }                          //15
        public string Cpl { get; set; }                             //16
        public string Cpl1 { get; set; }                            //17
        public string Cpl2 { get; set; }                            //18
        public string Canonical { get; set; }                       //19
        public int FlagReferentiel { get; set; }                    //20
        public bool CreerInactif { get; set; }                      //21
        public bool CreerActif { get; set; }                        //22
        public bool NePasReprendre { get; set; }                    //23

        public override string ToString()
        {
            return "Ligne correspondance : \r\n" +
                "TypeItem : " + TypeItem + "\r\n" +
                "Ancien_Code : " + Ancien_Code + "\r\n" +
                "Libelle_Ancien_Code : " + Libelle_Ancien_Code + "\r\n" +
                "AncienCodeActif : " + AncienCodeActif + "\r\n" +
                "Nouveau_Code : " + Nouveau_Code + "\r\n" +
                "Libelle_Nouveau_Code : " + Libelle_Nouveau_Code + "\r\n" +
                "Code_utilise : " + Code_utilise + "\r\n" +
                "Occurrence : " + Occurrence + "\r\n" +
                "NomSchema : " + NomSchema + "\r\n" +
                "DateRecensement : " + DateRecensement + "\r\n" +
                "DateMAJ : " + DateMAJ + "\r\n" +
                "TypeRecodage : " + TypeRecodage + "\r\n" +
                "NouveauCodeInactif : " + NouveauCodeInactif + "\r\n" +
                "UtilisateurCreation : " + UtilisateurCreation + "\r\n" +
                "TypeRef : " + TypeRef + "\r\n" +
                "NomRef : " + NomRef + "\r\n" +
                "Cpl : " + Cpl + "\r\n" +
                "Cpl1 : " + Cpl1 + "\r\n" +
                "Cpl2 : " + Cpl2 + "\r\n" +
                "Canonical : " + Canonical + "\r\n" +
                "FlagReferentiel : " + FlagReferentiel;
        }

        /**
         * Sommaire de la classe : 
         * <seealso cref = "TrouveLigneCorrespondance" >
         * <seealso cref = "TrouveLigneCorrespondance_ByLigne" >
         * <seealso cref = "RetourneLigneCorrespondanceDatagrid" >
         * <seealso cref = "NouveauCodeEstDansLeReferentiel" >
         * <seealso cref = "FiltrerListeCorrespondance_parCPL" >
         * <seealso cref = "FiltrerListeCorrespondance_parNOMREF" >
         * <seealso cref = "RetourneListeCorrespondanceUpdater" >
         * <seealso cref = "RetourneNombreRecodageMemeCode" >
         * <seealso cref = "RetourneCorrespondanceNouveauCode" >
         * <seealso cref = "DeleteRecodageResultatExamenByExamen" >
         * <seealso cref = "IsExamenParentRecode_ByLigneCorr" >
         * <seealso cref = "TrouveLigneCorrespondance_ByLigne" >
         * <seealso cref = "TrouveLigneCorrespondance_ByLigne" >
         * <seealso cref = "TrouveLigneCorrespondance_ByLigne" >
             */

        CorrespondanceDAL ObjCorrDAL = new CorrespondanceDAL();

        /// <summary>
        /// Permet de retrouver une ligne de correspondance dans une liste d'objet CorrespondanceBLL
        /// à partir des informations suivantes : 
        /// TypeRef, 
        /// Ancien_code, 
        /// Libelle_Ancien_Code, 
        /// Cpl,
        /// Cpl1,
        /// Cpl2,
        /// NomRef
        /// </summary>
        /// <param name="TypeRef"></param>
        /// <param name="Ancien_code"></param>
        /// <param name="Libelle_Ancien_Code"></param>
        /// <param name="Cpl"></param>
        /// <param name="Cpl1"></param>
        /// <param name="Cpl2"></param>
        /// <param name="NomRef"></param>
        /// <param name="ListeCorrespondance"></param>
        /// <returns></returns>
        public CorrespondanceBLL TrouveLigneCorrespondance(string TypeRef, string Ancien_code, string Libelle_Ancien_Code, string Cpl, string Cpl1, string Cpl2, string NomRef, List<CorrespondanceBLL> ListeCorrespondance)
        {
            CorrespondanceBLL ligne_a_retourner = new CorrespondanceBLL();
            for (int i=0;i<ListeCorrespondance.Count;i++)
            {
                if (ListeCorrespondance[i].TypeRef == TypeRef
                    && ListeCorrespondance[i].Ancien_Code == Ancien_code
                    && ListeCorrespondance[i].Libelle_Ancien_Code == Libelle_Ancien_Code
                    && ListeCorrespondance[i].Cpl == Cpl
                    && ListeCorrespondance[i].Cpl1 == Cpl1
                    && ListeCorrespondance[i].Cpl2 == Cpl2
                    && ListeCorrespondance[i].NomRef == NomRef)
                {
                    ligne_a_retourner = ListeCorrespondance[i];
                    return ligne_a_retourner;
                }
                    
            }
            return ligne_a_retourner;
        }

        /// <summary>
        /// Permet de retrouver un objet CorrespondanceBLL similaire dans une liste d'objet CorrespondanceBLL
        /// Condition de similitude : 
        /// TypeRef, Ancien_code, Cpl, Cpl1, Cpl2, NomRef Identiques
        /// </summary>
        /// <param name="ligne_correspondance"></param>
        /// <param name="ListeCorrespondance"></param>
        /// <returns></returns>
        public CorrespondanceBLL TrouveLigneCorrespondance_ByLigne(CorrespondanceBLL ligne_correspondance, List<CorrespondanceBLL> ListeCorrespondance)
        {
            CorrespondanceBLL ligne_a_retourner = new CorrespondanceBLL();
            for (int i = 0; i < ListeCorrespondance.Count; i++)
            {
                if (ListeCorrespondance[i].TypeRef == ligne_correspondance.TypeRef
                    && ListeCorrespondance[i].Ancien_Code == ligne_correspondance.Ancien_Code
                    //&& ListeCorrespondance[i].Libelle_Ancien_Code == ligne_correspondance.Libelle_Ancien_Code
                    && ListeCorrespondance[i].Cpl == ligne_correspondance.Cpl
                    && ListeCorrespondance[i].Cpl1 == ligne_correspondance.Cpl1
                    && ListeCorrespondance[i].Cpl2 == ligne_correspondance.Cpl2
                    && ListeCorrespondance[i].NomRef == ligne_correspondance.NomRef)
                {
                    ligne_a_retourner = ListeCorrespondance[i];
                    return ligne_a_retourner;
                }

            }
            return ligne_a_retourner;
        }

        /// <summary>
        /// Permet de retrouver un objet CorrespondanceBLL à partir d'un DataGridView et d'une cellule.
        /// Dans notre cas cela sert à retrouver notre ligne de correspondance à partir de l'interface graphique.
        /// </summary>
        /// <param name="dataGridView_saisie"></param>
        /// <param name="Cell"></param>
        /// <returns></returns>
        public CorrespondanceBLL RetourneLigneCorrespondanceDatagrid(DataGridView dataGridView_saisie, DataGridViewCell Cell)
        {
            CorrespondanceBLL ligne_correspondance_selectionne_datagrid = new CorrespondanceBLL();
            ligne_correspondance_selectionne_datagrid.TypeRef = dataGridView_saisie.Rows[Cell.RowIndex].Cells[14].Value.ToString();
            ligne_correspondance_selectionne_datagrid.Ancien_Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString();
            if(dataGridView_saisie.Rows[Cell.RowIndex].Cells[2].Value==null)
            {
                ligne_correspondance_selectionne_datagrid.Libelle_Ancien_Code = "";
            }
            else
            {
                ligne_correspondance_selectionne_datagrid.Libelle_Ancien_Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[2].Value.ToString();
            }
            ligne_correspondance_selectionne_datagrid.Cpl = dataGridView_saisie.Rows[Cell.RowIndex].Cells[16].Value.ToString();
            if (dataGridView_saisie.Rows[Cell.RowIndex].Cells[17].Value == null)
            {
                ligne_correspondance_selectionne_datagrid.Cpl1 = "0";
            }
            else
            {
                ligne_correspondance_selectionne_datagrid.Cpl1 = dataGridView_saisie.Rows[Cell.RowIndex].Cells[17].Value.ToString();
            }
            if (dataGridView_saisie.Rows[Cell.RowIndex].Cells[18].Value == null)
            {
                ligne_correspondance_selectionne_datagrid.Cpl2 = "0";
            }
            else
            {
                ligne_correspondance_selectionne_datagrid.Cpl2 = dataGridView_saisie.Rows[Cell.RowIndex].Cells[18].Value.ToString();
            }
            ligne_correspondance_selectionne_datagrid.NomRef = dataGridView_saisie.Rows[Cell.RowIndex].Cells[15].Value.ToString();
            return (ligne_correspondance_selectionne_datagrid);
        }


        /// <summary>
        /// Retourne un booléen qui indique si le Nouveau_code est présent dans le référentiel Preventiel (filtré par Cpl, Cpl1, Cpl2, Typeref)
        /// </summary>
        /// <param name="Nouveau_code"></param>
        /// <param name="Referentiel"></param>
        /// <param name="Cpl"></param>
        /// <param name="Cpl1"></param>
        /// <param name="Cpl2"></param>
        /// <param name="TypeRef"></param>
        /// <returns></returns>
        public bool NouveauCodeEstDansLeReferentiel(string Nouveau_code, List<ReferentielBLL> Referentiel, string Cpl, string Cpl1, string Cpl2, string TypeRef)
        {
            for (int i = 0; i < Referentiel.Count; i++)
            {
                if (Referentiel[i].Code.Trim() == Nouveau_code.Trim()
                    && Referentiel[i].Cpl == Cpl
                    && Referentiel[i].Cpl1 == Cpl1
                    && Referentiel[i].Cpl2 == Cpl2
                    && Referentiel[i].Type == TypeRef)
                {
                    return true;
                }
                if (Referentiel[i].Code.Trim() == Nouveau_code.Trim()
                    && Referentiel[i].Cpl == Cpl
                    && Referentiel[i].Type == TypeRef
                    && Cpl == "0"
                    && TypeRef == "CTRL"
                    )
                {
                    return true;
                }
                if (Referentiel[i].Code.Trim() == Nouveau_code.Trim()
                    && Referentiel[i].Cpl == Cpl
                    && Referentiel[i].Type == TypeRef
                    && Cpl == "2"
                    && TypeRef == "CTRL"
                    )
                {
                    return true;
                }
                if (Referentiel[i].Code.Trim() == Nouveau_code.Trim()
                    && Referentiel[i].Cpl == Cpl
                    && Referentiel[i].Type == TypeRef
                    && Cpl == "50"
                    && TypeRef == "CTRL"
                    )
                {
                    return true;
                }
                if (Referentiel[i].Code.Trim() == Nouveau_code.Trim()
                    && Referentiel[i].Cpl == Cpl
                    && Referentiel[i].Type == TypeRef
                    && Cpl == "60"
                    && TypeRef == "CTRL"
                    )
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Retourne une liste d'objet CorrespondanceBLL qui correspond à notre table de correspondance filtrée. 
        /// La clef de filtrage attendue est de la forme 'CPL|CPL1|CPL2|TYPEREF'
        /// </summary>
        /// <param name="CorrespFULL"></param>
        /// <param name="CPL"></param>
        /// <returns></returns>
        public List<CorrespondanceBLL> FiltrerListeCorrespondance_parCPL(List<CorrespondanceBLL> CorrespFULL, string CPL)
        {
            string[] CPL123 = CPL.Split('|');
            List<CorrespondanceBLL> CorrespFiltre = new List<CorrespondanceBLL>();

            if (CPL != "135|0|0|NOMEN" && CPL != "141|0|0|NOMEN")
            {
                for (int i = 0; i < CorrespFULL.Count; i++)
                {
                    //Console.WriteLine("CorrespFULL[i].Cpl : " + CorrespFULL[i].Cpl + ". CPL : " + CPL);
                    if (CorrespFULL[i].Cpl == CPL123[0] && CorrespFULL[i].Cpl1 == CPL123[1] && CorrespFULL[i].Cpl2 == CPL123[2] && CorrespFULL[i].TypeRef == CPL123[3])
                    {
                        CorrespFiltre.Add(CorrespFULL[i]);
                        //Console.WriteLine("J'ajoute la ligne " + i + " de la liste");
                    }
                }
            }
            else if (CPL == "135|0|0|NOMEN")
            {
                for (int i = 0; i < CorrespFULL.Count; i++)
                {
                    //Console.WriteLine("CorrespFULL[i].Cpl : " + CorrespFULL[i].Cpl + ". CPL : " + CPL);
                    if ((CorrespFULL[i].Cpl == CPL123[0] && CorrespFULL[i].TypeRef == CPL123[3]) || (CorrespFULL[i].Cpl == "50" && CorrespFULL[i].TypeRef == "CTRL"))
                    {
                        CorrespFiltre.Add(CorrespFULL[i]);
                        if (CorrespFULL[i].Cpl == "50")
                            Console.WriteLine(".");
                        //Console.WriteLine("J'ajoute la ligne " + i + " de la liste");
                    }
                }
                CorrespFiltre = CorrespFiltre.OrderBy(q => q.Ancien_Code).ToList();
                //ListaServizi = ListaServizi.OrderBy(q => q).ToList();
            }
            else if (CPL == "141|0|0|NOMEN")
            {
                for (int i = 0; i < CorrespFULL.Count; i++)
                {
                    //Console.WriteLine("CorrespFULL[i].Cpl : " + CorrespFULL[i].Cpl + ". CPL : " + CPL);
                    if ((CorrespFULL[i].Cpl == CPL123[0] && CorrespFULL[i].TypeRef == CPL123[3]) || (CorrespFULL[i].Cpl == "60" && CorrespFULL[i].TypeRef == "CTRL"))
                    {
                        CorrespFiltre.Add(CorrespFULL[i]);
                        if (CorrespFULL[i].Cpl == "60")
                            Console.WriteLine(".");
                        //Console.WriteLine("J'ajoute la ligne " + i + " de la liste");
                    }
                }
                CorrespFiltre = CorrespFiltre.OrderBy(q => q.Ancien_Code).ToList();
                //ListaServizi = ListaServizi.OrderBy(q => q).ToList();
            }

            return (CorrespFiltre);
        }

        /// <summary>
        /// Retourne une liste d'objet CorrespondanceBLL qui correspond à notre table de correspondance filtrée par NomRef. 
        /// </summary>
        /// <param name="CorrespFULL"></param>
        /// <param name="NOMREF"></param>
        /// <returns></returns>
        public List<CorrespondanceBLL> FiltrerListeCorrespondance_parNOMREF(List<CorrespondanceBLL> CorrespFULL, string NOMREF)
        {
            List<CorrespondanceBLL> CorrespFiltre = new List<CorrespondanceBLL>();

            if (NOMREF != "Examen" && NOMREF != "Vaccin")
            {
                for (int i = 0; i < CorrespFULL.Count; i++)
                {
                    //Console.WriteLine("CorrespFULL[i].Cpl : " + CorrespFULL[i].Cpl + ". CPL : " + CPL);
                    if (CorrespFULL[i].NomRef == NOMREF)
                    {
                        CorrespFiltre.Add(CorrespFULL[i]);
                        //Console.WriteLine("J'ajoute la ligne " + i + " de la liste");
                    }
                }
            }
            else if (NOMREF == "Examen")
            {
                for (int i = 0; i < CorrespFULL.Count; i++)
                {
                    //Console.WriteLine("CorrespFULL[i].Cpl : " + CorrespFULL[i].Cpl + ". CPL : " + CPL);
                    if ((CorrespFULL[i].NomRef == "Examen") || (CorrespFULL[i].NomRef == "ParametragesResultatExamen"))
                    {
                        CorrespFiltre.Add(CorrespFULL[i]);
                        //Console.WriteLine("J'ajoute la ligne " + i + " de la liste");
                    }
                }
                CorrespFiltre = CorrespFiltre.OrderBy(q => q.Ancien_Code).ToList();
                //ListaServizi = ListaServizi.OrderBy(q => q).ToList();
            }
            else if (NOMREF == "Vaccin")
            {
                for (int i = 0; i < CorrespFULL.Count; i++)
                {
                    //Console.WriteLine("CorrespFULL[i].Cpl : " + CorrespFULL[i].Cpl + ". CPL : " + CPL);
                    if ((CorrespFULL[i].NomRef == "Vaccin") || (CorrespFULL[i].NomRef == "ProtocoleVaccinal"))
                    {
                        CorrespFiltre.Add(CorrespFULL[i]);
                        //Console.WriteLine("J'ajoute la ligne " + i + " de la liste");
                    }
                }
                CorrespFiltre = CorrespFiltre.OrderBy(q => q.Ancien_Code).ToList();
                //ListaServizi = ListaServizi.OrderBy(q => q).ToList();
            }

            return (CorrespFiltre);
        }

        /// <summary>
        /// Retourne une liste d'objet CorrespondanceBLL qui correspond à notre table de correspondance mise à jour. 
        /// On passe en paramètre la ligne que l'on veut mettre à jour ainsi que son NomRef et celle-ci est mise à jour tout simplement.
        /// </summary>
        /// <param name="CorrespNonUpdater"></param>
        /// <param name="ligneAUpdater"></param>
        /// <param name="NomRef"></param>
        /// <returns></returns>
        public List<CorrespondanceBLL> RetourneListeCorrespondanceUpdater(List<CorrespondanceBLL> CorrespNonUpdater,
            CorrespondanceBLL ligneAUpdater, string NomRef)
        {
            List<CorrespondanceBLL> CorrespUpdater = new List<CorrespondanceBLL>();
            for (int i = 0; i < CorrespNonUpdater.Count; i++)
            {
                if (CorrespNonUpdater[i].Ancien_Code == ligneAUpdater.Ancien_Code &&
                    CorrespNonUpdater[i].Libelle_Ancien_Code == ligneAUpdater.Libelle_Ancien_Code &&
                    NomRef == ligneAUpdater.NomRef)
                {
                    CorrespNonUpdater[i].Nouveau_Code = ligneAUpdater.Nouveau_Code;
                    CorrespNonUpdater[i].Libelle_Nouveau_Code = ligneAUpdater.Libelle_Nouveau_Code;
                    CorrespNonUpdater[i].FlagReferentiel = ligneAUpdater.FlagReferentiel;
                    CorrespNonUpdater[i].CreerInactif = ligneAUpdater.CreerInactif;
                    CorrespNonUpdater[i].CreerActif = ligneAUpdater.CreerActif;
                    CorrespNonUpdater[i].NePasReprendre = ligneAUpdater.NePasReprendre;
                }
            }
            CorrespUpdater = CorrespNonUpdater;
            return CorrespUpdater;
        }

        /// <summary>
        /// Retourne un entier indiquant le nombre de recodages identiques au code passé en paramètre.
        /// Les recodages examinés sont ceux de la liste d'objet CorrespondanceBLL passée en paramètre.
        /// </summary>
        /// <param name="Corresp"></param>
        /// <param name="Nouveau_code"></param>
        /// <returns></returns>
        public int RetourneNombreRecodageMemeCode(List<CorrespondanceBLL> Corresp, string Nouveau_code)
        {
            int nbrecodage = 0;
            for (int i = 0; i < Corresp.Count; i++)
            {
                if (Corresp[i].Nouveau_Code == Nouveau_code)
                {
                    nbrecodage++;
                }
            }
            return nbrecodage;
        }

        /// <summary>
        /// Retourne un objet CorrespondanceBLL qui représente une ligne de notre table de correspondance.
        /// La ligne retournée est trouvé à partir de l'ancien_code 
        /// et des critères de filtrage du paramètre ClefRef, sous la forme 'Cpl|Cpl1|Cpl2|TypeRef'.
        /// </summary>
        /// <param name="Corresp"></param>
        /// <param name="Ancien_Code"></param>
        /// <param name="ClefREF"></param>
        /// <returns></returns>
        public CorrespondanceBLL RetourneCorrespondanceNouveauCode(List<CorrespondanceBLL> Corresp, string Ancien_Code, string ClefREF)
        {
            string[] CPL123 = ClefREF.Split('|');
            CorrespondanceBLL LigneNouveauCode = new CorrespondanceBLL();
            if(Corresp!=null)
            {
                for (int i = 0; i < Corresp.Count; i++)
                {
                    if (Corresp[i].Ancien_Code == Ancien_Code && Corresp[i].Cpl == CPL123[0] && Corresp[i].TypeRef == CPL123[3])
                    {
                        LigneNouveauCode = Corresp[i];
                        return LigneNouveauCode;
                    }
                }
            }
            return LigneNouveauCode;
        }

        /// <summary>
        /// Retourne une liste d'objet CorrespondanceBLL qui représente notre table de correspondance. 
        /// Avant d'etre retournée, cette table est vidée de tous les recodages résultat examen rattachés au code examen parent passé en paramètre.
        /// </summary>
        /// <param name="CorrespFULL"></param>
        /// <param name="CodeExamen"></param>
        /// <returns></returns>
        public List<CorrespondanceBLL> DeleteRecodageResultatExamenByExamen(List<CorrespondanceBLL> CorrespFULL, string CodeExamen)
        {
            List<CorrespondanceBLL> CorrespFiltre = new List<CorrespondanceBLL>();
            for (int i = 0; i < CorrespFULL.Count; i++)
            {
                if (!CorrespFULL[i].Ancien_Code.Contains("#") || !CorrespFULL[i].Ancien_Code.Contains(CodeExamen) || CorrespFULL[i].FlagReferentiel == 3)
                {
                    CorrespFiltre.Add(CorrespFULL[i]);
                }
                else
                {
                    CorrespFULL[i].Nouveau_Code = "";
                    CorrespFULL[i].Libelle_Nouveau_Code = "";
                    CorrespFULL[i].NouveauCodeInactif = true;
                    CorrespFULL[i].FlagReferentiel = 0;
                    CorrespFiltre.Add(CorrespFULL[i]);
                    ObjCorrDAL.UpdateSQLITE_TBCorrespondance(CorrespFULL[i]);
                    ObjCorrDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(CorrespFULL[i]);
                }
            }
            return CorrespFiltre;
        }


        /// <summary>
        /// Retourne un booléen 
        /// qui indique si l'ancien code de la ligne de correspondance (qui doit donc être un résultat examen ou un protocole vaccinal) 
        /// passée en paramètre possède un examen parent recodé.
        /// </summary>
        /// <param name="ligneCorrespondance"></param>
        /// <param name="listeCorrespondance"></param>
        /// <returns></returns>
        public bool IsExamenParentRecode_ByLigneCorr(CorrespondanceBLL ligneCorrespondance, List<CorrespondanceBLL> listeCorrespondance)
        {
            bool isexamenparentrecode = false;
            string[] examen_ancien_code = ligneCorrespondance.Ancien_Code.Split('#');
            CorrespondanceBLL ligne_exam_nouveau_code = RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, examen_ancien_code[0], "135|0|0|NOMEN");
            if (ligne_exam_nouveau_code.Nouveau_Code == null || ligne_exam_nouveau_code.Nouveau_Code == "")
            {
                isexamenparentrecode = false;
            }
            else
            {
                isexamenparentrecode = true;
            }
            return isexamenparentrecode;
        }

        public bool IsVaccinParentRecode_ByLigneCorr(CorrespondanceBLL ligneCorrespondance, List<CorrespondanceBLL> listeCorrespondance)
        {
            bool isexamenparentrecode = false;
            string[] examen_ancien_code = ligneCorrespondance.Ancien_Code.Split('#');
            CorrespondanceBLL ligne_exam_nouveau_code = RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, examen_ancien_code[0], "141|0|0|NOMEN");
            if (ligne_exam_nouveau_code.Nouveau_Code == null || ligne_exam_nouveau_code.Nouveau_Code == "")
            {
                isexamenparentrecode = false;
            }
            else
            {
                isexamenparentrecode = true;
            }
            return isexamenparentrecode;
        }

        public List<CorrespondanceBLL> DeleteRecodageProtocoleVaccByVaccin(List<CorrespondanceBLL> CorrespFULL, string CodeExamen)
        {
            List<CorrespondanceBLL> CorrespFiltre = new List<CorrespondanceBLL>();
            for (int i = 0; i < CorrespFULL.Count; i++)
            {
                if (!CorrespFULL[i].Ancien_Code.Contains("#") || !CorrespFULL[i].Ancien_Code.Contains(CodeExamen) || CorrespFULL[i].FlagReferentiel == 3)
                {
                    CorrespFiltre.Add(CorrespFULL[i]);
                }
                else
                {
                    CorrespFULL[i].Nouveau_Code = "";
                    CorrespFULL[i].Libelle_Nouveau_Code = "";
                    CorrespFULL[i].NouveauCodeInactif = true;
                    CorrespFULL[i].FlagReferentiel = 0;
                    CorrespFiltre.Add(CorrespFULL[i]);
                    ObjCorrDAL.UpdateSQLITE_TBCorrespondance(CorrespFULL[i]);
                    ObjCorrDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(CorrespFULL[i]);
                }
            }
            return CorrespFiltre;
        }

        public List<CorrespondanceBLL> DeleteRattachementEntEtab(List<CorrespondanceBLL> CorrespFULL, string CodeEtablissement)
        {
            List<CorrespondanceBLL> CorrespRattachementSupprime = new List<CorrespondanceBLL>();
            for(int i=0;i<CorrespFULL.Count;i++)
            {
                if(CorrespFULL[i].Cpl != "1000" || CorrespFULL[i].Ancien_Code != CodeEtablissement)
                {
                    CorrespRattachementSupprime.Add(CorrespFULL[i]);
                }
                else
                {
                    Console.WriteLine("Je supprime la ligne suivante : " + CorrespFULL[i]);
                    CorrespondanceDAL correspondanceDAL = new CorrespondanceDAL();
                    correspondanceDAL.DeleteSQLITE_TBCorrespondance(CorrespFULL[i]);
                }
            }
            return CorrespRattachementSupprime;
        }

        public float RetourneEtatAvancementRecodage(List<CorrespondanceBLL> CorrespondanceFiltre)
        {
            float pourcentage = 0;
            if (CorrespondanceFiltre != null)
            {
                float nbItemFlag = 0;
                for (int i = 0; i < CorrespondanceFiltre.Count; i++)
                {
                    if (CorrespondanceFiltre[i].FlagReferentiel != 0)
                    {
                        nbItemFlag++;
                    }
                }
                pourcentage = (nbItemFlag / CorrespondanceFiltre.Count) * 100;
            }
            return pourcentage;
        }

        public List<CorrespondanceBLL> RetourneSeulementNonTraite(List<CorrespondanceBLL> CorrespFULL)
        {
            List<CorrespondanceBLL> CorrespNonTraite = new List<CorrespondanceBLL>();
            if (CorrespFULL != null)
            {
                for (int i = 0; i < CorrespFULL.Count; i++)
                {
                    if (CorrespFULL[i].FlagReferentiel == 0)
                    {
                        CorrespNonTraite.Add(CorrespFULL[i]);
                    }
                }
            }
            return CorrespNonTraite;
        }

        public List<CorrespondanceBLL> FiltreCorrespondanceParChaineRecherche(List<CorrespondanceBLL> CorrespNonFiltre, string chainerecherche)
        {
            Fonc fonc = new Fonc();
            List<CorrespondanceBLL> CorrespFiltre = new List<CorrespondanceBLL>();
            if (CorrespNonFiltre != null)
            {
                for (int i = 0; i < CorrespNonFiltre.Count; i++)
                {
                    string canonicalref = fonc.CanonicalString(CorrespNonFiltre[i].Libelle_Ancien_Code).Trim();
                    string canonicalrecherche = fonc.CanonicalString(chainerecherche).Trim();
                    if (canonicalref.Contains(canonicalrecherche))
                    {
                        CorrespFiltre.Add(CorrespNonFiltre[i]);
                    }
                }
            }
            return CorrespFiltre;
        }

        public List<CorrespondanceBLL> FiltreCorrespondanceParCodeRecherche(List<CorrespondanceBLL> CorrespNonFiltre, string chainerecherche)
        {
            Fonc fonc = new Fonc();
            List<CorrespondanceBLL> CorrespFiltre = new List<CorrespondanceBLL>();
            if (CorrespNonFiltre != null)
            {
                for (int i = 0; i < CorrespNonFiltre.Count; i++)
                {
                    string canonicalref = fonc.CanonicalString(CorrespNonFiltre[i].Ancien_Code).Trim();
                    string canonicalrecherche = fonc.CanonicalString(chainerecherche).Trim();
                    if (canonicalref.Contains(canonicalrecherche))
                    {
                        CorrespFiltre.Add(CorrespNonFiltre[i]);
                    }
                }
            }
            return CorrespFiltre;
        }

        public bool IsAffecteASoitMeme_ByDatagrid(DataGridView dataGridView_saisie, DataGridView dataGridView_ref)
        {
            bool isAffecteSoitMeme = false;
            
            CorrespondanceBLL ligne_correspondance_selectionne_datagrid = new CorrespondanceBLL();
            ligne_correspondance_selectionne_datagrid = ligne_correspondance_selectionne_datagrid.RetourneLigneCorrespondanceDatagrid(dataGridView_saisie, dataGridView_saisie.CurrentCell);

            CorrespondanceBLL ligne_correspondance_selectionne_objet =
                ligne_correspondance_selectionne_datagrid.TrouveLigneCorrespondance_ByLigne(ligne_correspondance_selectionne_datagrid, VariablePartage.TableCorrespondanceFiltre);

            
            ReferentielBLL ligne_referentiel_datagrid = new ReferentielBLL();
            //ligne_corresp.RetourneLigneCorrespondanceDatagrid(dataGridView_saisie, dataGridView_saisie.CurrentCell);
            ligne_referentiel_datagrid = ligne_referentiel_datagrid.RetourneLigneReferentiel_ByDatagrid(dataGridView_ref, dataGridView_ref.CurrentCell);

            ReferentielBLL ligne_referentiel_objet = 
            ligne_referentiel_datagrid.TrouveLigneReferentiel_ByLigneRef(ligne_referentiel_datagrid, VariablePartage.TableReferentielFiltre);


            isAffecteSoitMeme = IsAffecteASoitMeme_ByLigne(ligne_correspondance_selectionne_objet, ligne_referentiel_objet);
            return isAffecteSoitMeme;
        }

        public bool IsAffecteASoitMeme_ByLigne(CorrespondanceBLL ligne_corresp, ReferentielBLL ligne_referentiel)
        {
            bool isAffecteSoitMeme = false;
            if(ligne_corresp.Nouveau_Code == ligne_referentiel.Code)
            {
                isAffecteSoitMeme = true; 
            }
            return isAffecteSoitMeme;
        }

        public CorrespondanceBLL CopieLigneCorrespondanceSansReference(CorrespondanceBLL ligne_a_copier)
        {
            CorrespondanceBLL ligne_copie = new CorrespondanceBLL();

            ligne_copie.TypeItem = ligne_a_copier.TypeItem;
            ligne_copie.Ancien_Code = ligne_a_copier.Ancien_Code;
            ligne_copie.Libelle_Ancien_Code = ligne_a_copier.Libelle_Ancien_Code;
            ligne_copie.AncienCodeActif = ligne_a_copier.AncienCodeActif;
            ligne_copie.Nouveau_Code = ligne_a_copier.Nouveau_Code;
            ligne_copie.Libelle_Nouveau_Code = ligne_a_copier.Libelle_Nouveau_Code;
            ligne_copie.Code_utilise = ligne_a_copier.Code_utilise;
            ligne_copie.Occurrence = ligne_a_copier.Occurrence;
            ligne_copie.NomSchema = ligne_a_copier.NomSchema;
            ligne_copie.DateRecensement = ligne_a_copier.DateRecensement;
            ligne_copie.DateMAJ = ligne_a_copier.DateMAJ;
            ligne_copie.TypeRecodage = ligne_a_copier.TypeRecodage;
            ligne_copie.NouveauCodeInactif = ligne_a_copier.NouveauCodeInactif;
            ligne_copie.UtilisateurCreation = ligne_a_copier.UtilisateurCreation;
            ligne_copie.TypeRef = ligne_a_copier.TypeRef;
            ligne_copie.NomRef = ligne_a_copier.NomRef;
            ligne_copie.Cpl = ligne_a_copier.Cpl;
            ligne_copie.Cpl1 = ligne_a_copier.Cpl1;
            ligne_copie.Cpl2 = ligne_a_copier.Cpl2;
            ligne_copie.Canonical = ligne_a_copier.Canonical;
            ligne_copie.FlagReferentiel = ligne_a_copier.FlagReferentiel;
            ligne_copie.CreerInactif = ligne_a_copier.CreerInactif;
            ligne_copie.CreerActif = ligne_a_copier.CreerActif;
            ligne_copie.NePasReprendre = ligne_a_copier.NePasReprendre;

            return ligne_copie;
        }
    }
}
