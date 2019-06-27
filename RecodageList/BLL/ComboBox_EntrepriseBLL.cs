using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RecodageList.GUI;
using RecodageList.DAL;
using System.Threading;

namespace RecodageList.BLL
{
    public class ComboBox_EntrepriseBLL
    {
        
        public string Code { get; set; }
        public string Lib { get; set; }

        public override string ToString()
        {
            return Lib;
        }

        public RattachementEtablissementEntreprise myformrattachement { get; set; }

        public void InitComboBoxRattachementEntEtab (List<ReferentielBLL> TableReferentiel)
        {
            ReferentielBLL RefObject = new ReferentielBLL();
            List<ReferentielBLL> TableReferentielEntreprise = new List<ReferentielBLL>();
            TableReferentielEntreprise = RefObject.FiltrerListeReferentiel_parCPL(TableReferentiel, "3|0|0|CTRL");
            //myformrattachement.comboBox_entreprise_existante.DataSource = new BindingSource(TableReferentielEntreprise, null);
            myformrattachement.comboBox_entreprise_existante.DisplayMember = "Lib";
            myformrattachement.comboBox_entreprise_existante.ValueMember = "Code";
            //myform.progressBar_admin.Value++;
            myformrattachement.comboBox_entreprise_existante.DropDownStyle = ComboBoxStyle.DropDownList;

            List<ComboBox_EntrepriseBLL> comboBox_EntrepriseBLLs = new List<ComboBox_EntrepriseBLL>();
            

            for (int i=0;i<TableReferentielEntreprise.Count;i++)
            {
                ComboBox_EntrepriseBLL comboBox_EntrepriseBLL = new ComboBox_EntrepriseBLL();
                comboBox_EntrepriseBLL.Code = "";
                comboBox_EntrepriseBLL.Lib = "";
                comboBox_EntrepriseBLL.Code = TableReferentielEntreprise[i].Code;
                comboBox_EntrepriseBLL.Lib = TableReferentielEntreprise[i].Lib;
                comboBox_EntrepriseBLLs.Add(comboBox_EntrepriseBLL);
                myformrattachement.comboBox_entreprise_existante.Items.Add(comboBox_EntrepriseBLL);
            }

           // myformrattachement.comboBox_entreprise_existante.SelectedIndex = 0;
        }

        public void AjouteLigneEntEtab(Form1 myform, RattachementEtablissementEntreprise myformrattachement, CorrespondanceBLL ligne_selectionné, string Code, string Libelle, bool ent_existante)
        {
            List<CorrespondanceBLL> lignes_a_ajouter = new List<CorrespondanceBLL>();
            CorrespondanceBLL ligne_a_ajouter = new CorrespondanceBLL();
            ligne_a_ajouter = ligne_a_ajouter.CopieLigneCorrespondanceSansReference(ligne_selectionné);
            // ligne_a_ajouter = ligne_selectionné;
            ligne_a_ajouter.Ancien_Code = ligne_a_ajouter.Nouveau_Code;
            ligne_a_ajouter.Libelle_Ancien_Code = ligne_a_ajouter.Libelle_Nouveau_Code;
            //ligne_a_ajouter.Nouveau_Code = (myformrattachement.comboBox_entreprise_existante.SelectedItem as ComboBox_EntrepriseBLL).Code.ToString();
            ligne_a_ajouter.Nouveau_Code = Code;
            ligne_a_ajouter.Libelle_Nouveau_Code = Libelle;
            ligne_a_ajouter.Cpl = "1000";
            ligne_a_ajouter.NomRef = "Rapprochement-Entreprise-Etablissement";
            if(ent_existante)
            {
                ligne_a_ajouter.FlagReferentiel = 1;
            }
            else
            {
                ligne_a_ajouter.FlagReferentiel = 2;
            }
            
            lignes_a_ajouter.Add(ligne_a_ajouter);

            VariablePartage.TableCorrespondance.Add(ligne_a_ajouter);

            CorrespondanceDAL CorObjDAL = new CorrespondanceDAL();
            CorObjDAL.InsertIntoSQLITE_TBCorrespondance(lignes_a_ajouter);

            if(!ent_existante)
            {
                ReferentielBLL ligne_referentiel = new ReferentielBLL();

                ligne_referentiel = ligne_referentiel.CreerLigneReferentiel_ByLigneCorresp(ligne_a_ajouter);
                ligne_referentiel.Cpl = "3";
                ligne_referentiel.TypeItem = "Entreprise";
                ligne_referentiel.Code = Code;
                ligne_referentiel.Lib = Libelle;
                ligne_referentiel.InActif = true;

                List < ReferentielBLL > lignes_referentiel_a_ajouter = new List<ReferentielBLL>();
                lignes_referentiel_a_ajouter.Add(ligne_referentiel);

                VariablePartage.TableReferentiel.Add(ligne_referentiel);

                ReferentielDAL RefObjDAL = new ReferentielDAL();
                RefObjDAL.InsertIntoSQLITE_TBReferentiel(lignes_referentiel_a_ajouter);
            }
            

            ComboBoxFiltreDAL ComboObject = new ComboBoxFiltreDAL();
            VariablePartage.ComboBoxFiltre = ComboObject.ObtenirComboBoxFiltre();
            GUIFonction GUI = new GUIFonction();
            GUI.InitComboBoxFiltre(myform);

        }

        public void AjouteLigneEntEtab_ByComboBoxEntEtab(Form1 myform, RattachementEtablissementEntreprise myformrattachement, CorrespondanceBLL ligne_selectionné)
        {
            string Code = (myformrattachement.comboBox_entreprise_existante.SelectedItem as ComboBox_EntrepriseBLL).Code.ToString();
            string Libelle = (myformrattachement.comboBox_entreprise_existante.SelectedItem as ComboBox_EntrepriseBLL).Lib.ToString();
            AjouteLigneEntEtab(myform, myformrattachement, ligne_selectionné, Code, Libelle, true);
        }

        public void AjoutLigneEntEtab_ByCreerEnt(Form1 myform, RattachementEtablissementEntreprise myformrattachement, CorrespondanceBLL ligne_selectionné)
        {
            string Code = myformrattachement.textBox_code_entreprise_a_creer.Text;
            string Libelle = myformrattachement.textBox_libelle_ent_a_creer.Text;
            if(Code != null && Code != "" && Libelle != null && Libelle != "")
            {
                AjouteLigneEntEtab(myform, myformrattachement, ligne_selectionné, Code, Libelle, false);
            }
            else
            {
                MessageBox.Show("Code et Libellé de l'entreprise obligatoire !");
            }
        }
    }
}

