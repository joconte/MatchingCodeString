using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RecodageList.DAL;
using RecodageList.BLL;
using RecodageList.GUI;
using System.ComponentModel;
using System.Threading;
using System.Drawing;

namespace RecodageList.BLL
{
    public class ActionRecodage
    {
        
        Fonc fonc = new Fonc();
        GUIFonction GUI = new GUIFonction();
        CorrespondanceBLL CorrObjectBLL = new CorrespondanceBLL();
        CorrespondanceDAL CorrObjectDAL = new CorrespondanceDAL();
        ReferentielBLL RefObjectBLL = new ReferentielBLL();
        ReferentielDAL RefObjectDAL = new ReferentielDAL();


        public void AffecteCode(DataGridView dataGridView_saisie, DataGridView dataGridView_ref, Form1 myform, string CPLCLEF)
        {
            if(CorrObjectBLL.IsAffecteASoitMeme_ByDatagrid(dataGridView_saisie,dataGridView_ref)==false)
            {
                int maxindex;
                maxindex = DesaffecteCode(dataGridView_saisie, dataGridView_ref, myform);
                maxindex = AffectationCode(dataGridView_saisie, dataGridView_ref, myform, CPLCLEF);
                //BindDatagrid(dataGridView_saisie, Init.TableCorrespondanceFiltre);
                GUI.InitStructSaisie(dataGridView_saisie, myform);
                GUI.InitColorSaisie(dataGridView_saisie);
                CalCulCelluleFocus(dataGridView_saisie, maxindex, myform.checkBox_nontraite);
                SaisieCellClick(dataGridView_saisie, dataGridView_ref, myform, CPLCLEF);
            }
            else
            {
                Console.WriteLine("J'essaye d'affecter à lui même, donc je ne fais rien.");
            }
        }

        public int DesaffecteCode(DataGridView dataGridView_saisie, DataGridView dataGridView_ref, Form1 myform)
        {
            int maxindex;
            DesaffecteCode_Referentiel(dataGridView_saisie, dataGridView_ref,myform);
            maxindex = DesaffecteCode_Saisie(dataGridView_saisie, dataGridView_ref, myform);
            //BindDatagrid(dataGridView_saisie, Init.TableCorrespondanceFiltre);
            //InitStructSaisie();
            //InitColorSaisie();
            return maxindex;
        }


        public List<ReferentielBLL> DesaffecteCode_Referentiel_ByLigneCorr(CorrespondanceBLL ligne_correspondance_selectionne_objet, List<ReferentielBLL> TableReferentiel, List<ReferentielBLL> TableReferentielFiltre,  string CPLCLEF)
        {
            //Désaffecte le code avant de l'affecter à un nouveau
            //Cas particuler des examens : purge des resultats examens créés dans le référentiel sur cet examen
            if (ligne_correspondance_selectionne_objet.NomRef == "Examen")
            {
                //Déclare une liste vierge de type référentiel 
                List<ReferentielBLL> LigneResExamASupprimer = new List<ReferentielBLL>();
                //Remplit cette liste de tous les résultats examens à supprimer du référentiel pour l'item examen selectionné
                LigneResExamASupprimer = RefObjectBLL.RetourneListeResExamByExam(VariablePartage.TableReferentiel, ligne_correspondance_selectionne_objet.Nouveau_Code);

                //Supprime les résultats examens précédemment stockés dans la liste LigneResExamASupprimer (ci-dessus) de la table des referentiels.
                TableReferentiel = RefObjectBLL.SupprimeResExamByExam_RetourneTableRef(TableReferentiel, LigneResExamASupprimer);

                //Suite à la suppression dans la table referentiel, la table des referentiels filtrés (sur le module en cours) est réinitialisée 
                //de manière à prendre en compte la purge des résultats examens.
                TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(TableReferentiel, CPLCLEF);


            }
            //Désaffecte le code avant de l'affecter à un nouveau
            //Cas particuler des vaccins : purge des protocoles vaccinaux créés dans le référentiel sur ce vaccin
            else if (ligne_correspondance_selectionne_objet.NomRef == "Vaccin")
            {
                //Déclare une liste vierge de type référentiel 
                List<ReferentielBLL> LigneResExamASupprimer = new List<ReferentielBLL>();
                //Remplit cette liste de tous les protocoles vaccinaux à supprimer du référentiel pour l'item vaccin selectionné 
                //(utilisation de la même méthode que les examens car même mécanisme)
                LigneResExamASupprimer = RefObjectBLL.RetourneListeResExamByExam(TableReferentiel, ligne_correspondance_selectionne_objet.Nouveau_Code);

                //Supprime les protocoles vaccinaux précédemment stockés dans la liste LigneResExamASupprimer (ci-dessus) de la table des referentiels.
                TableReferentiel = RefObjectBLL.SupprimeResExamByExam_RetourneTableRef(TableReferentiel, LigneResExamASupprimer);

                //Suite à la suppression dans la table referentiel, la table des referentiels filtrés (sur le module en cours) est réinitialisée 
                //de manière à prendre en compte la purge des protocoles vaccinaux.
                TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(TableReferentiel, CPLCLEF);

                //Supprime le recodage de tous les protocoles vaccinaux associés au vaccin désaffecté.
                //VariablePartage.TableCorrespondanceFiltre = CorrObjectBLL.DeleteRecodageProtocoleVaccByVaccin(VariablePartage.TableCorrespondanceFiltre, dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString());


            }

            //Cas particulier : rapprochement ent etab
            //S'il s'agit du module rapprochement ent etab, alors on va purger dans le referentiel entreprise 
            if(ligne_correspondance_selectionne_objet.Cpl == "1000")
            {
                ligne_correspondance_selectionne_objet.Cpl = "3";
                ligne_correspondance_selectionne_objet.NomRef = "Entreprise";
            }


            //Si le code est de type 'créé' et que le code est présent une seule fois dans le referentiel
            //alors on le supprime du referentiel
            if (ligne_correspondance_selectionne_objet.FlagReferentiel == 2 && CorrObjectBLL.RetourneNombreRecodageMemeCode(VariablePartage.TableCorrespondanceFiltre, ligne_correspondance_selectionne_objet.Nouveau_Code) < 2)
            {
                ReferentielBLL ligneref_trouve = new ReferentielBLL();

                ligneref_trouve =
                RefObjectBLL.TrouveLigneReferentiel_ByLigneCorresp(ligne_correspondance_selectionne_objet, VariablePartage.TableReferentielFiltre);

                List<ReferentielBLL> ligneref_trouve_list = new List<ReferentielBLL>();
                ligneref_trouve_list.Add(ligneref_trouve);
                RefObjectDAL.DeleteFromSQLITE_TBReferentiel(ligneref_trouve_list);
                TableReferentiel = RefObjectBLL.SuppressionItem_RetourneListeReferentiel(TableReferentiel, ligneref_trouve);
                //TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(TableReferentiel, CPLCLEF);

            }

            return TableReferentiel;
        }

        public void DesaffecteCode_Referentiel(DataGridView dataGridView_saisie, DataGridView dataGridView_ref, Form1 myform)
        {
            //Vérifie qu'une cellule saisie soit selectionnée 
            if (dataGridView_saisie.CurrentCell != null && dataGridView_saisie.CurrentCell.RowIndex != -1)
                //&& dataGridView_ref.CurrentCell != null && dataGridView_ref.CurrentCell.RowIndex != -1)
            {
                foreach (DataGridViewCell Cell in dataGridView_saisie.SelectedCells)
                {
                    CorrespondanceBLL ligne_correspondance_selectionne_datagrid = CorrObjectBLL.RetourneLigneCorrespondanceDatagrid(dataGridView_saisie, Cell);

                    CorrespondanceBLL ligne_correspondance_selectionne_objet =
                        CorrObjectBLL.TrouveLigneCorrespondance_ByLigne(ligne_correspondance_selectionne_datagrid, VariablePartage.TableCorrespondanceFiltre);

                    VariablePartage.TableReferentiel = DesaffecteCode_Referentiel_ByLigneCorr(ligne_correspondance_selectionne_objet, VariablePartage.TableReferentiel, VariablePartage.TableReferentielFiltre, (string)myform.comboBox_filtre.SelectedValue);
                    VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);
                }
                GUI.InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                GUI.InitColorReferentiel(dataGridView_ref);
            }
        }


        public void DesaffecteCode_Referentiel_archive(DataGridView dataGridView_saisie, DataGridView dataGridView_ref, Form1 myform)
        {
            //Vérifie qu'une cellule saisie soit selectionnée ainsi qu'une cellule référentiel soit selectionnée. 
            if (dataGridView_saisie.CurrentCell != null && dataGridView_saisie.CurrentCell.RowIndex != -1
                && dataGridView_ref.CurrentCell != null && dataGridView_ref.CurrentCell.RowIndex != -1)
            {
                foreach (DataGridViewCell Cell in dataGridView_saisie.SelectedCells)
                {
                    CorrespondanceBLL ligne_correspondance_selectionne_datagrid = CorrObjectBLL.RetourneLigneCorrespondanceDatagrid(dataGridView_saisie, Cell);

                    CorrespondanceBLL ligne_correspondance_selectionne_objet = 
                        CorrObjectBLL.TrouveLigneCorrespondance_ByLigne(ligne_correspondance_selectionne_datagrid, VariablePartage.TableCorrespondanceFiltre);

                    //Désaffecte le code avant de l'affecter à un nouveau
                    //Cas particuler des examens : purge des resultats examens créés dans le référentiel sur cet examen
                    if (ligne_correspondance_selectionne_objet.NomRef == "Examen")
                    {
                        //Déclare une liste vierge de type référentiel 
                        List<ReferentielBLL> LigneResExamASupprimer = new List<ReferentielBLL>();
                        //Remplit cette liste de tous les résultats examens à supprimer du référentiel pour l'item examen selectionné
                        LigneResExamASupprimer = RefObjectBLL.RetourneListeResExamByExam(VariablePartage.TableReferentiel, (string)dataGridView_saisie.Rows[Cell.RowIndex].Cells[4].Value);

                        //Supprime les résultats examens précédemment stockés dans la liste LigneResExamASupprimer (ci-dessus) de la table des referentiels.
                        VariablePartage.TableReferentiel = RefObjectBLL.SupprimeResExamByExam_RetourneTableRef(VariablePartage.TableReferentiel, LigneResExamASupprimer);

                        //Suite à la suppression dans la table referentiel, la table des referentiels filtrés (sur le module en cours) est réinitialisée 
                        //de manière à prendre en compte la purge des résultats examens.
                        VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);

                        //Supprime le recodage de tous les résultats examens associés à l'examen désaffecté.
                        VariablePartage.TableCorrespondanceFiltre = CorrObjectBLL.DeleteRecodageResultatExamenByExamen(VariablePartage.TableCorrespondanceFiltre, dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString());

                        //Ré-initialise la structure de la table Referentiel ainsi que ses couleurs.
                        GUI.InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                        GUI.InitColorReferentiel(dataGridView_ref);
                    }


                    //Désaffecte le code avant de l'affecter à un nouveau
                    //Cas particuler des vaccins : purge des protocoles vaccinaux créés dans le référentiel sur ce vaccin
                    if (ligne_correspondance_selectionne_objet.NomRef == "Vaccin")
                    {
                        //Déclare une liste vierge de type référentiel 
                        List<ReferentielBLL> LigneResExamASupprimer = new List<ReferentielBLL>();
                        //Remplit cette liste de tous les protocoles vaccinaux à supprimer du référentiel pour l'item vaccin selectionné 
                        //(utilisation de la même méthode que les examens car même mécanisme)
                        LigneResExamASupprimer = RefObjectBLL.RetourneListeResExamByExam(VariablePartage.TableReferentiel, (string)dataGridView_saisie.Rows[Cell.RowIndex].Cells[4].Value);

                        //Supprime les protocoles vaccinaux précédemment stockés dans la liste LigneResExamASupprimer (ci-dessus) de la table des referentiels.
                        VariablePartage.TableReferentiel = RefObjectBLL.SupprimeResExamByExam_RetourneTableRef(VariablePartage.TableReferentiel, LigneResExamASupprimer);

                        //Suite à la suppression dans la table referentiel, la table des referentiels filtrés (sur le module en cours) est réinitialisée 
                        //de manière à prendre en compte la purge des protocoles vaccinaux.
                        VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);

                        //Supprime le recodage de tous les protocoles vaccinaux associés au vaccin désaffecté.
                        VariablePartage.TableCorrespondanceFiltre = CorrObjectBLL.DeleteRecodageProtocoleVaccByVaccin(VariablePartage.TableCorrespondanceFiltre, dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString());

                        //Ré-initialise la structure de la table Referentiel ainsi que ses couleurs.
                        GUI.InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                        GUI.InitColorReferentiel(dataGridView_ref);
                    }

                    //Si le code selectionné est de type FlagPreventiel == 2 (creer actif ou inactif) et qu'il y à moins de 2 codes restants dans le référentiel pour ce code là 
                    //alors on passe à la condition suivante
                    if (ligne_correspondance_selectionne_objet.FlagReferentiel == 2 && CorrObjectBLL.RetourneNombreRecodageMemeCode(VariablePartage.TableCorrespondanceFiltre, ligne_correspondance_selectionne_objet.Nouveau_Code) < 2)
                    {
                        //Cas particulier des resultats examens
                        //On supprime du référentiel le code (on est toujours dans la partie désaffectation du précédent code)
                        if (ligne_correspondance_selectionne_objet.Cpl == "50" && ligne_correspondance_selectionne_objet.Ancien_Code.Contains("#"))
                        {
                            //Permet de splitter l'ancien_code sur le critère du '#' -> ex : EXA|405#V06 -> examen_ancien_code[0] = EXA|405; examen_ancien_code[1] = V06
                            string[] examen_ancien_code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString().Split('#');

                            //Retourne le nouveau code de l'examen parent du resultat examen, à partir de l'ancien code examen présent dans l'ancien code du résultat examen.
                            CorrespondanceBLL ligne_exam_nouveau_code = CorrObjectBLL.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, examen_ancien_code[0], "135|0|0|NOMEN");

                            //Déclare un liste vierge de type referentiel
                            List<ReferentielBLL> ligneref1 = new List<ReferentielBLL>();

                            //Déclare un objet vierge de type referentiel
                            //et le remplit avec les valeurs du datagridview Saisie sauf pour le Cpl1
                            //Le Cpl1 est affecté à la valeur du Nouveau_code de l'examen parent
                            //En effet c'est la méthode utilisé par le PREVTGXV7 
                            //pour connaitre la dépendance entre un résultat examen et son examen parent
                            ReferentielBLL ligneref = new ReferentielBLL();
                            ligneref.Type = dataGridView_saisie.Rows[Cell.RowIndex].Cells[14].Value.ToString();
                            ligneref.Cpl = dataGridView_saisie.Rows[Cell.RowIndex].Cells[16].Value.ToString();
                            ligneref.Cpl1 = ligne_exam_nouveau_code.Nouveau_Code;// Affectation spécifique du CPL1
                            ligneref.Cpl2 = dataGridView_saisie.Rows[Cell.RowIndex].Cells[18].Value.ToString();
                            ligneref.Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[4].Value.ToString();
                            ligneref.Lib = dataGridView_saisie.Rows[Cell.RowIndex].Cells[5].Value.ToString();

                            //La ligne de type referentiel précédemment remplie est insérée dans la liste de référentiel précédemment créée également.
                            //En effet la méthode DeleteFromSQLITE_TBReferentiel attend une liste de referentiel et non pas un simple objet referentiel.
                            ligneref1.Add(ligneref);

                            //Supprime de la table TB$S_PrevReferentiel SQLite (accès aux données) la ligne référentiel précédemment construite.
                            RefObjectDAL.DeleteFromSQLITE_TBReferentiel(ligneref1);

                            //Supprime dans l'objet Init.TableReferentiel la ligne référentiel précémment construite 
                            //Cette manip' évite de recharger la table SQLite en mémoire en appliquant la même modification à la table qu'a l'objet.
                            VariablePartage.TableReferentiel = RefObjectBLL.SuppressionItem_RetourneListeReferentiel(VariablePartage.TableReferentiel, ligneref);

                            //Recharge l'objet Init.TableReferentielFiltre qui représente la table des référentiel filtrés (module en cours) 
                            //à partir de l'objet Init.TableReferentiel de manière à prendre en compte la suppression.
                            VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);

                            //Recharge la structure ainsi que les couleurs du référentiel (datagridview_ref)
                            GUI.InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                            GUI.InitColorReferentiel(dataGridView_ref);
                        }
                        else if (dataGridView_saisie.Rows[Cell.RowIndex].Cells[16].Value.ToString() == "60" && dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString().Contains("#"))
                        {
                            string[] vaccin_ancien_code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString().Split('#');
                            CorrespondanceBLL ligne_vaccin_nouveau_code = CorrObjectBLL.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, vaccin_ancien_code[0], "141|0|0|NOMEN");
                            List<ReferentielBLL> ligneref1 = new List<ReferentielBLL>();
                            ReferentielBLL ligneref = new ReferentielBLL();
                            ligneref.Type = dataGridView_saisie.Rows[Cell.RowIndex].Cells[14].Value.ToString();
                            ligneref.Cpl = dataGridView_saisie.Rows[Cell.RowIndex].Cells[16].Value.ToString();
                            ligneref.Cpl1 = ligne_vaccin_nouveau_code.Nouveau_Code;
                            ligneref.Cpl2 = dataGridView_saisie.Rows[Cell.RowIndex].Cells[18].Value.ToString();
                            ligneref.Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[4].Value.ToString();
                            ligneref.Lib = dataGridView_saisie.Rows[Cell.RowIndex].Cells[5].Value.ToString();
                            ligneref1.Add(ligneref);
                            RefObjectDAL.DeleteFromSQLITE_TBReferentiel(ligneref1);
                            VariablePartage.TableReferentiel = RefObjectBLL.SuppressionItem_RetourneListeReferentiel(VariablePartage.TableReferentiel, ligneref);
                            VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);
                            GUI.InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                            GUI.InitColorReferentiel(dataGridView_ref);
                        }
                        else
                        {
                            List<ReferentielBLL> ligneref1 = new List<ReferentielBLL>();
                            ReferentielBLL ligneref = new ReferentielBLL();
                            ligneref.Type = dataGridView_saisie.Rows[Cell.RowIndex].Cells[14].Value.ToString();
                            ligneref.Cpl = dataGridView_saisie.Rows[Cell.RowIndex].Cells[16].Value.ToString();
                            ligneref.Cpl1 = dataGridView_saisie.Rows[Cell.RowIndex].Cells[17].Value.ToString();
                            if (ligneref.Type == "CTRL" && ligneref.Cpl == "2")
                            {
                                ligneref.Cpl1 = "2";
                            }
                            else
                            {
                                ligneref.Cpl1 = dataGridView_saisie.Rows[Cell.RowIndex].Cells[17].Value.ToString();
                            }
                            ligneref.Cpl2 = dataGridView_saisie.Rows[Cell.RowIndex].Cells[18].Value.ToString();
                            ligneref.Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[4].Value.ToString();
                            ligneref.Lib = dataGridView_saisie.Rows[Cell.RowIndex].Cells[5].Value.ToString();
                            ligneref1.Add(ligneref);
                            RefObjectDAL.DeleteFromSQLITE_TBReferentiel(ligneref1);
                            VariablePartage.TableReferentiel = RefObjectBLL.SuppressionItem_RetourneListeReferentiel(VariablePartage.TableReferentiel, ligneref);
                            VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);
                            GUI.InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                            GUI.InitColorReferentiel(dataGridView_ref);
                        }
                    }
                }
            }
        }

        public void DesaffecteCode_Saisie_ByLigneCorresp(CorrespondanceBLL ligne_correspondance_selectionne_objet, List<CorrespondanceBLL> TableCorrespondanceFiltre, List<CorrespondanceBLL> TableCorrespondance, Form1 myform)
        {
            if (ligne_correspondance_selectionne_objet.NomRef == "Examen")
            {
                TableCorrespondanceFiltre = CorrObjectBLL.DeleteRecodageResultatExamenByExamen(TableCorrespondanceFiltre, ligne_correspondance_selectionne_objet.Ancien_Code);
            }
            else if (ligne_correspondance_selectionne_objet.NomRef == "Vaccin")
            {
                TableCorrespondanceFiltre = CorrObjectBLL.DeleteRecodageProtocoleVaccByVaccin(TableCorrespondanceFiltre, ligne_correspondance_selectionne_objet.Ancien_Code);
            }
            else if(ligne_correspondance_selectionne_objet.NomRef == "Etablissement" && ligne_correspondance_selectionne_objet.FlagReferentiel == 2)
            {
                TableCorrespondance = CorrObjectBLL.DeleteRattachementEntEtab(TableCorrespondance, ligne_correspondance_selectionne_objet.Nouveau_Code);
                ComboBoxFiltreDAL ComboObject = new ComboBoxFiltreDAL();
                VariablePartage.ComboBoxFiltre = ComboObject.ObtenirComboBoxFiltre();
                GUIFonction GUI = new GUIFonction();
                GUI.InitComboBoxFiltre(myform);
                myform.comboBox_filtre.SelectedIndex = myform.comboBox_filtre.FindStringExact("Etablissement");
            }
            
            ligne_correspondance_selectionne_objet.Nouveau_Code = "";
            ligne_correspondance_selectionne_objet.Libelle_Nouveau_Code = "";
            ligne_correspondance_selectionne_objet.FlagReferentiel = 0;
            ligne_correspondance_selectionne_objet.CreerInactif = false;
            ligne_correspondance_selectionne_objet.CreerActif = false;
            ligne_correspondance_selectionne_objet.NePasReprendre = false;
            ligne_correspondance_selectionne_objet.NouveauCodeInactif = false;

            CorrObjectDAL.UpdateSQLITE_TBCorrespondance(ligne_correspondance_selectionne_objet);
            CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(ligne_correspondance_selectionne_objet);
            
            TableCorrespondance = CorrObjectBLL.RetourneListeCorrespondanceUpdater(TableCorrespondance, ligne_correspondance_selectionne_objet, ligne_correspondance_selectionne_objet.NomRef);
            VariablePartage.TableCorrespondance = TableCorrespondance;
        }

        public int DesaffecteCode_Saisie(DataGridView dataGridView_saisie, DataGridView dataGridView_ref, Form1 myform)
        {
            int maxindex = 0;
            if (dataGridView_saisie.CurrentCell != null && dataGridView_saisie.CurrentCell.RowIndex != -1)
                //&& dataGridView_ref.CurrentCell != null && dataGridView_ref.CurrentCell.RowIndex != -1
            {
                foreach (DataGridViewCell Cell in dataGridView_saisie.SelectedCells)
                {

                    CorrespondanceBLL ligne_correspondance_selectionne_datagrid = new CorrespondanceBLL();
                    ligne_correspondance_selectionne_datagrid = CorrObjectBLL.RetourneLigneCorrespondanceDatagrid(dataGridView_saisie, Cell);

                    CorrespondanceBLL ligne_correspondance_selectionne_objet = new CorrespondanceBLL();
                    ligne_correspondance_selectionne_objet = CorrObjectBLL.TrouveLigneCorrespondance_ByLigne(
                        ligne_correspondance_selectionne_datagrid, 
                        VariablePartage.TableCorrespondanceFiltre);

                    DesaffecteCode_Saisie_ByLigneCorresp(ligne_correspondance_selectionne_objet, VariablePartage.TableCorrespondanceFiltre, VariablePartage.TableCorrespondance, myform);


                    if (maxindex < Cell.RowIndex)
                    {
                        maxindex = Cell.RowIndex;
                    }
                }
            }
            return maxindex;
        }

        public void AffectationCode_ByLigneCorrespReferentiel(CorrespondanceBLL ligne_correspondance_selectionne_objet, ReferentielBLL ligne_referentiel_selectionne_objet, List<CorrespondanceBLL> TableCorrespondanceFiltre, List<CorrespondanceBLL> TableCorrespondance, List<ReferentielBLL> TableReferentielFiltre, string CPLCLEF)
        {
            ligne_correspondance_selectionne_objet.Nouveau_Code = ligne_referentiel_selectionne_objet.Code;
            ligne_correspondance_selectionne_objet.Libelle_Nouveau_Code = ligne_referentiel_selectionne_objet.Lib;
            if (ligne_referentiel_selectionne_objet.FlagPreventiel == 2)
            {
                ligne_correspondance_selectionne_objet.FlagReferentiel = 2;
            }
            else
            {
                ligne_correspondance_selectionne_objet.FlagReferentiel = 1;
            }

            ligne_correspondance_selectionne_objet.NouveauCodeInactif = ligne_referentiel_selectionne_objet.InActif;
            ligne_correspondance_selectionne_objet.CreerInactif = ligne_referentiel_selectionne_objet.InActif;
            ligne_correspondance_selectionne_objet.CreerActif = !ligne_referentiel_selectionne_objet.InActif;
            //ligne_correspondance_selectionne_objet.FlagReferentiel = ligne_referentiel_selectionne_objet.FlagPreventiel;

            CorrObjectDAL.UpdateSQLITE_TBCorrespondance(ligne_correspondance_selectionne_objet);
            CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(ligne_correspondance_selectionne_objet);


            TableCorrespondance = CorrObjectBLL.RetourneListeCorrespondanceUpdater(TableCorrespondance, ligne_correspondance_selectionne_objet, ligne_correspondance_selectionne_objet.NomRef);
            TableCorrespondanceFiltre = CorrObjectBLL.FiltrerListeCorrespondance_parCPL(TableCorrespondance, CPLCLEF);
            VariablePartage.TableCorrespondance = TableCorrespondance;
            VariablePartage.TableCorrespondanceFiltre = TableCorrespondanceFiltre;
        }

        public int AffectationCode(DataGridView dataGridView_saisie, DataGridView dataGridView_ref, Form1 myform, string CPLCLEF)
        {
            int maxindex = 0;
            if (dataGridView_saisie.CurrentCell != null && dataGridView_saisie.CurrentCell.RowIndex != -1
                && dataGridView_ref.CurrentCell != null && dataGridView_ref.CurrentCell.RowIndex != -1)
            {
                foreach (DataGridViewCell Cell in dataGridView_saisie.SelectedCells)
                {
                    CorrespondanceBLL ligne_correspondance_selectionne_datagrid = new CorrespondanceBLL();
                    ligne_correspondance_selectionne_datagrid = CorrObjectBLL.RetourneLigneCorrespondanceDatagrid(dataGridView_saisie, Cell);

                    CorrespondanceBLL ligne_correspondance_selectionne_objet = new CorrespondanceBLL();
                    ligne_correspondance_selectionne_objet =
                        CorrObjectBLL.TrouveLigneCorrespondance_ByLigne(ligne_correspondance_selectionne_datagrid,
                        VariablePartage.TableCorrespondanceFiltre);

                    ReferentielBLL ligne_referentiel_selectionne_datagrid = new ReferentielBLL();

                    ligne_referentiel_selectionne_datagrid = ligne_referentiel_selectionne_datagrid.RetourneLigneReferentiel_ByDatagrid(dataGridView_ref, Cell);

                    ReferentielBLL ligne_referentiel_selectionne_objet =
                        RefObjectBLL.TrouveLigneReferentiel_ByLigneRef(ligne_referentiel_selectionne_datagrid,
                        VariablePartage.TableReferentielFiltre);

                    AffectationCode_ByLigneCorrespReferentiel(ligne_correspondance_selectionne_objet, ligne_referentiel_selectionne_objet, VariablePartage.TableCorrespondanceFiltre, VariablePartage.TableCorrespondance, VariablePartage.TableReferentielFiltre, CPLCLEF);

                    if (maxindex < Cell.RowIndex)
                    {
                        maxindex = Cell.RowIndex;
                    }
                }
            }
            return maxindex;
        }


        public int AffectationCode_archive(DataGridView dataGridView_saisie, DataGridView dataGridView_ref, Form1 myform)
        {
            int maxindex = 0;
            if (dataGridView_saisie.CurrentCell != null && dataGridView_saisie.CurrentCell.RowIndex != -1
                && dataGridView_ref.CurrentCell != null && dataGridView_ref.CurrentCell.RowIndex != -1)
            {
                foreach (DataGridViewCell Cell in dataGridView_saisie.SelectedCells)
                {
                    dataGridView_saisie.Rows[Cell.RowIndex].Cells[4].Value = dataGridView_ref.CurrentRow.Cells[2].Value.ToString();
                    dataGridView_saisie.Rows[Cell.RowIndex].Cells[5].Value = dataGridView_ref.CurrentRow.Cells[3].Value.ToString();
                    dataGridView_saisie.Rows[Cell.RowIndex].Cells[12].Value = dataGridView_ref.CurrentRow.Cells[5].Value;

                    if (Convert.ToInt32(dataGridView_ref.CurrentRow.Cells[13].Value) == 2)
                    {
                        dataGridView_saisie.Rows[Cell.RowIndex].Cells[20].Value = 2;
                    }
                    else
                    {
                        dataGridView_saisie.Rows[Cell.RowIndex].Cells[20].Value = 1;
                    }

                    //Fait l'affectation du nouveau code en base
                    CorrespondanceBLL ligne = new CorrespondanceBLL();
                    ligne.Ancien_Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString();
                    //ligne.Libelle_Ancien_Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[2].Value.ToString();
                    ligne.Nouveau_Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[4].Value.ToString();
                    ligne.Libelle_Nouveau_Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[5].Value.ToString();
                    ligne.NomRef = dataGridView_saisie.Rows[Cell.RowIndex].Cells[15].Value.ToString();
                    ligne.NouveauCodeInactif = (bool)dataGridView_saisie.Rows[Cell.RowIndex].Cells[12].Value;

                    if (Convert.ToInt32(dataGridView_ref.CurrentRow.Cells[13].Value) == 2)
                    {
                        ligne.FlagReferentiel = 2;
                    }
                    else
                    {
                        ligne.FlagReferentiel = 1;
                    }


                    if ((string)dataGridView_saisie.Rows[Cell.RowIndex].Cells[15].Value == "Examen")
                    {
                        VariablePartage.TableCorrespondanceFiltre = CorrObjectBLL.DeleteRecodageResultatExamenByExamen(VariablePartage.TableCorrespondanceFiltre, dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString());
                    }

                    if ((string)dataGridView_saisie.Rows[Cell.RowIndex].Cells[15].Value == "Vaccin")
                    {
                        VariablePartage.TableCorrespondanceFiltre = CorrObjectBLL.DeleteRecodageProtocoleVaccByVaccin(VariablePartage.TableCorrespondanceFiltre, dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString());
                    }

                    ligne.ToString();

                    CorrObjectDAL.UpdateSQLITE_TBCorrespondance(ligne);
                    CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(ligne);

                    VariablePartage.TableCorrespondance = CorrObjectBLL.RetourneListeCorrespondanceUpdater(VariablePartage.TableCorrespondance, ligne, ligne.NomRef);

                    if (maxindex < Cell.RowIndex)
                    {
                        maxindex = Cell.RowIndex;
                    }
                }
                VariablePartage.TableCorrespondanceFiltre = CorrObjectBLL.FiltrerListeCorrespondance_parCPL(VariablePartage.TableCorrespondance, (string)myform.comboBox_filtre.SelectedValue);
            }
            return maxindex;
        }

        public void CalCulCelluleFocus(DataGridView dataGridView_saisie, int maxindex, CheckBox checkBox_nontraite, string typeaction = "")
        {

            if (checkBox_nontraite.Checked == false)
            {
                if (maxindex > dataGridView_saisie.RowCount - 2)
                {
                    try
                    {
                        dataGridView_saisie.CurrentCell = dataGridView_saisie.Rows[dataGridView_saisie.RowCount - 1].Cells[4];
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    
                }
                else
                {
                    if (typeaction == "Suppr")
                    {
                        try
                        {
                            dataGridView_saisie.CurrentCell = dataGridView_saisie.Rows[maxindex].Cells[4];
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    else
                    {
                        try
                        {
                            dataGridView_saisie.CurrentCell = dataGridView_saisie.Rows[maxindex + 1].Cells[4];
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }

            }

            if (checkBox_nontraite.Checked)
            {
                List<CorrespondanceBLL> CorrespNonTraite = new List<CorrespondanceBLL>();
                CorrespNonTraite = CorrObjectBLL.RetourneSeulementNonTraite(VariablePartage.TableCorrespondanceFiltre);
                BindDatagrid(dataGridView_saisie, CorrespNonTraite);
                if (dataGridView_saisie.CurrentCell != null && dataGridView_saisie.CurrentCell.RowIndex != -1)
                {
                    dataGridView_saisie.CurrentCell = dataGridView_saisie.Rows[dataGridView_saisie.CurrentCell.RowIndex].Cells[4];
                    //dataGridView_saisie.BeginEdit(true);
                    //SaisieCellClick();
                }
            }

            if(dataGridView_saisie.CurrentCell !=null)
            {
                dataGridView_saisie.BeginEdit(true);
            }
            //SaisieCellClick();
            //InitStructSaisie();
            //InitColorSaisie();
        }

        public void BindDatagrid(DataGridView dataGridView, List<CorrespondanceBLL> TableCorrespondance)
        {
            var source = new BindingSource();
            source.DataSource = TableCorrespondance;
            dataGridView.DataSource = source;
        }

        

        public void SaisieCellClick_ByLigneCorresp_Thread(object args)
        {
            Array argArray = new object[6];
            argArray = (Array)args;
            //string clefValeur = (string)argArray.GetValue(0);
            //Form1 myform = (Form1)argArray.GetValue(1);
            CorrespondanceBLL ligne_correspondance_selectionnee_objet = (CorrespondanceBLL)argArray.GetValue(0);
            List<ReferentielBLL> TableReferentielFiltre = (List<ReferentielBLL>)argArray.GetValue(1);
            List<ReferentielBLL> TableReferentiel = (List<ReferentielBLL>)argArray.GetValue(2); 
            List<ReferentielBLL> TableRefLevenshtein = (List<ReferentielBLL>)argArray.GetValue(3); 
            string CPLCLEF = (string)argArray.GetValue(4);
            Form1 myform = (Form1)argArray.GetValue(5);

            string labelchargement;
            labelchargement = "Chargement...";
            var source_ref = new BindingSource(labelchargement, null);
            UpdateDatagridviewInvoke(myform, source_ref);


            //List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();
            if (ligne_correspondance_selectionnee_objet.Cpl != "50" && ligne_correspondance_selectionnee_objet.Cpl != "135"
                && ligne_correspondance_selectionnee_objet.Cpl != "60" && ligne_correspondance_selectionnee_objet.Cpl != "141")
            {
                //Init.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(Init.TableReferentiel, (string)comboBox_filtre.SelectedValue);

                if (ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code != null)
                {

                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code);

                }
                else if (ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code == null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, "");
                }

            }
            else if (ligne_correspondance_selectionnee_objet.Cpl == "50" && ligne_correspondance_selectionnee_objet.Ancien_Code.Contains("#"))
            {
                string[] examen_ancien_code = ligne_correspondance_selectionnee_objet.Ancien_Code.Split('#');
                CorrespondanceBLL ligne_exam_nouveau_code = CorrObjectBLL.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, examen_ancien_code[0], "135|0|0|NOMEN");
                List<ReferentielBLL> TableRefResultatExamen = new List<ReferentielBLL>();
                TableRefResultatExamen = RefObjectBLL.FiltrerListeResultatExamenReferentielParExamen(VariablePartage.TableReferentiel, ligne_exam_nouveau_code.Nouveau_Code);
                if (ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code != null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(TableRefResultatExamen, ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code);
                }
                else if (ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code == null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(TableRefResultatExamen, "");
                }
            }
            else if (ligne_correspondance_selectionnee_objet.Cpl == "135")
            {
                //ReferentielDAL Ref = new ReferentielDAL();
                //Console.WriteLine("(string)comboBox_filtre.SelectedValue : " + (string)comboBox_filtre.SelectedValue);
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, CPLCLEF);

                //Supprime les resultat examen du referentiel
                VariablePartage.TableReferentielFiltre = RefObjectBLL.SupprimeResultatExamenReferentiel(VariablePartage.TableReferentielFiltre);
                if (ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code != null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code);
                }
                else if (ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code == null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, "");
                }
                //var source_ref = new BindingSource();
                //source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                //dataGridView_ref.DataSource = source_ref;

                //InitStructReferentiel();
                //InitColorReferentiel();
            }
            else if (ligne_correspondance_selectionnee_objet.Cpl == "60" && ligne_correspondance_selectionnee_objet.Ancien_Code.Contains("#"))
            {
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, CPLCLEF);
                string[] vaccin_ancien_code = ligne_correspondance_selectionnee_objet.Ancien_Code.Split('#');
                CorrespondanceBLL ligne_vacin_nouveau_code = CorrObjectBLL.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, vaccin_ancien_code[0], "141|0|0|NOMEN");
                List<ReferentielBLL> TableProtocoleVaccin = new List<ReferentielBLL>();
                //TableProtocoleVaccin = RefObjectBLL.FiltrerListeResultatExamenReferentielParExamen(VariablePartage.TableReferentiel, ligne_vacin_nouveau_code.Nouveau_Code);
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeResultatExamenReferentielParExamen(VariablePartage.TableReferentiel, ligne_vacin_nouveau_code.Nouveau_Code);
                //VariablePartage.TableReferentielFiltre = RefObjectBLL.SupprimeProtocoleVaccinal(VariablePartage.TableReferentielFiltre);

                if (ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code != null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code);
                }
                else if (ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code == null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, "");
                }
                //var source_ref = new BindingSource();
                //source_ref.DataSource = TableRefLevenshtein;
                //dataGridView_ref.DataSource = source_ref;
            }
            else if (ligne_correspondance_selectionnee_objet.Cpl == "141")
            {
                //ReferentielDAL Ref = new ReferentielDAL();
                //Console.WriteLine("(string)comboBox_filtre.SelectedValue : " + (string)comboBox_filtre.SelectedValue);
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, CPLCLEF);

                VariablePartage.TableReferentielFiltre = RefObjectBLL.SupprimeProtocoleVaccinal(VariablePartage.TableReferentielFiltre);


                if (ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code != null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code);
                }
                else if (ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code == null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, "");
                }
                //var source_ref = new BindingSource();
                //source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                //dataGridView_ref.DataSource = source_ref;
            }
            //return (TableRefLevenshtein);
            //var bindingListRef = new BindingList<ReferentielBLL>(TableRefLevenshtein);
            //var source_ref = new BindingSource(bindingListRef, null);
            //dataGridView_ref.DataSource = source_ref;
            //InitStructReferentiel();
            //GUI.InitColorReferentiel(dataGridView_ref);

            var bindingListRef = new BindingList<ReferentielBLL>(TableRefLevenshtein);
            source_ref = new BindingSource(bindingListRef, null);
            UpdateDataSourceInvoke(myform, source_ref);
        }

        public delegate void UpdateDataSourceDelegate(Form1 myform, BindingSource source_ref);

        private void UpdateDataSourceInvoke(Form1 myform, BindingSource source_ref)
        {
            try
            {
                //On invoque le delegate pour qu'il effectue la tâche sur le temps
                //de l'autre thread.
                myform.Invoke((UpdateDataSourceDelegate)UpdateDataSource, myform,source_ref);
            }
            catch (Exception ex) { return; }
        }

        private void UpdateDatagridviewInvoke(Form1 myform, BindingSource source_ref)
        {
            try
            {
                //On invoque le delegate pour qu'il effectue la tâche sur le temps
                //de l'autre thread.
                myform.Invoke((UpdateDataSourceDelegate)UpdateDatagridview, myform, source_ref);
            }
            catch (Exception ex) { return; }
        }

        public void UpdateDataSource(Form1 myform, BindingSource source_ref)
        {
            //myform.dataGridView_ref.Paint -= myform.dataGridView_ref_Paint;
            myform.dataGridView_ref.DataSource = source_ref;
            GUI.InitStructReferentiel(myform.dataGridView_ref, myform.dataGridView_saisie);
            GUI.InitColorReferentiel(myform.dataGridView_ref);
            myform.Cursor = Cursors.Default;
        }

        public void UpdateDatagridview(Form1 myform, BindingSource source_ref)
        {
            myform.dataGridView_ref.DataSource = source_ref;
            //myform.dataGridView_ref.Paint += myform.dataGridView_ref_Paint;
            myform.Cursor = Cursors.WaitCursor;
        }

        public List<ReferentielBLL> SaisieCellClick_ByLigneCorresp(CorrespondanceBLL ligne_correspondance_selectionnee_objet, List<ReferentielBLL> TableReferentielFiltre, List<ReferentielBLL> TableReferentiel, string CPLCLEF )
        {
            List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();
            if (ligne_correspondance_selectionnee_objet.Cpl != "50" && ligne_correspondance_selectionnee_objet.Cpl != "135"
                && ligne_correspondance_selectionnee_objet.Cpl != "60" && ligne_correspondance_selectionnee_objet.Cpl != "141")
            {
                //Init.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(Init.TableReferentiel, (string)comboBox_filtre.SelectedValue);

                if (ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code != null)
                {

                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code);

                }
                else if (ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code == null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, "");
                }

            }
            else if (ligne_correspondance_selectionnee_objet.Cpl == "50" && ligne_correspondance_selectionnee_objet.Ancien_Code.Contains("#"))
            {
                string[] examen_ancien_code = ligne_correspondance_selectionnee_objet.Ancien_Code.Split('#');
                CorrespondanceBLL ligne_exam_nouveau_code = CorrObjectBLL.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, examen_ancien_code[0], "135|0|0|NOMEN");
                List<ReferentielBLL> TableRefResultatExamen = new List<ReferentielBLL>();
                TableRefResultatExamen = RefObjectBLL.FiltrerListeResultatExamenReferentielParExamen(VariablePartage.TableReferentiel, ligne_exam_nouveau_code.Nouveau_Code);
                if (ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code != null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(TableRefResultatExamen, ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code);
                }
                else if (ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code == null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(TableRefResultatExamen, "");
                }
            }
            else if (ligne_correspondance_selectionnee_objet.Cpl == "135")
            {
                //ReferentielDAL Ref = new ReferentielDAL();
                //Console.WriteLine("(string)comboBox_filtre.SelectedValue : " + (string)comboBox_filtre.SelectedValue);
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, CPLCLEF);

                //Supprime les resultat examen du referentiel
                VariablePartage.TableReferentielFiltre = RefObjectBLL.SupprimeResultatExamenReferentiel(VariablePartage.TableReferentielFiltre);
                if (ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code != null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code);
                }
                else if (ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code == null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, "");
                }
                //var source_ref = new BindingSource();
                //source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                //dataGridView_ref.DataSource = source_ref;

                //InitStructReferentiel();
                //InitColorReferentiel();
            }
            else if (ligne_correspondance_selectionnee_objet.Cpl == "60" && ligne_correspondance_selectionnee_objet.Ancien_Code.Contains("#"))
            {
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, CPLCLEF);
                string[] vaccin_ancien_code = ligne_correspondance_selectionnee_objet.Ancien_Code.Split('#');
                CorrespondanceBLL ligne_vacin_nouveau_code = CorrObjectBLL.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, vaccin_ancien_code[0], "141|0|0|NOMEN");
                List<ReferentielBLL> TableProtocoleVaccin = new List<ReferentielBLL>();
                //TableProtocoleVaccin = RefObjectBLL.FiltrerListeResultatExamenReferentielParExamen(VariablePartage.TableReferentiel, ligne_vacin_nouveau_code.Nouveau_Code);
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeResultatExamenReferentielParExamen(VariablePartage.TableReferentiel, ligne_vacin_nouveau_code.Nouveau_Code);
                //VariablePartage.TableReferentielFiltre = RefObjectBLL.SupprimeProtocoleVaccinal(VariablePartage.TableReferentielFiltre);

                if (ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code != null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code);
                }
                else if (ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code == null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, "");
                }
                //var source_ref = new BindingSource();
                //source_ref.DataSource = TableRefLevenshtein;
                //dataGridView_ref.DataSource = source_ref;
            }
            else if (ligne_correspondance_selectionnee_objet.Cpl == "141")
            {
                //ReferentielDAL Ref = new ReferentielDAL();
                //Console.WriteLine("(string)comboBox_filtre.SelectedValue : " + (string)comboBox_filtre.SelectedValue);
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, CPLCLEF);

                VariablePartage.TableReferentielFiltre = RefObjectBLL.SupprimeProtocoleVaccinal(VariablePartage.TableReferentielFiltre);


                if (ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code != null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code);
                }
                else if (ligne_correspondance_selectionnee_objet.Libelle_Ancien_Code == null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, "");
                }
                //var source_ref = new BindingSource();
                //source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                //dataGridView_ref.DataSource = source_ref;
            }
            return (TableRefLevenshtein);
            //var bindingListRef = new BindingList<ReferentielBLL>(TableRefLevenshtein);
            //var source_ref = new BindingSource(bindingListRef, null);
            //dataGridView_ref.DataSource = source_ref;
            //InitStructReferentiel();
            //GUI.InitColorReferentiel(dataGridView_ref);
        }

        public void SaisieCellClick(DataGridView dataGridView_saisie, DataGridView dataGridView_ref, Form1 myform, string CPLCLEF)
        {
            if (dataGridView_saisie.CurrentCell != null && dataGridView_saisie.CurrentCell.ColumnIndex.Equals(4) && dataGridView_saisie.CurrentCell.RowIndex != -1)
            {
                //myform.button_deleteRecodage.Enabled = true;
                //myform.button_affecter.Enabled = true;
                //myform.button_afficherCreationCode.Enabled = true;
                //myform.button_creercode.Enabled = false;
                //myform.textBox_codeacreer.Text = "";
                //myform.textBox_libellecodeacreer.Text = "";
                //myform.textBox_codeacreer.Enabled = false;
                //myform.textBox_libellecodeacreer.Enabled = false;
                //myform.checkBox_codeacreer_actif_inactif.Enabled = false;
                //myform.button_nepasreprendre.Enabled = true;
                myform.textBox_search.Enabled = true;
                myform.button_del_filter_search.Enabled = true;
                //myform.button_nepasreprendre_module.Enabled = true;
                //myform.button_nepasreprendre_global.Enabled = true;
                myform.textBox_search.Text = "";


                if (dataGridView_saisie.CurrentCell != null)
                {
                    CorrespondanceBLL ligne_correspondance_selectionnee_datagrid = new CorrespondanceBLL();
                    ligne_correspondance_selectionnee_datagrid = ligne_correspondance_selectionnee_datagrid.RetourneLigneCorrespondanceDatagrid(dataGridView_saisie, dataGridView_saisie.CurrentCell);

                    CorrespondanceBLL ligne_correspondance_selectionnee_objet = new CorrespondanceBLL();
                    ligne_correspondance_selectionnee_objet = ligne_correspondance_selectionnee_objet.TrouveLigneCorrespondance_ByLigne(ligne_correspondance_selectionnee_datagrid, VariablePartage.TableCorrespondanceFiltre);

                    List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();
                    Thread saisieCellClick_ByLigneCorresp_Thread = new Thread(new ParameterizedThreadStart(SaisieCellClick_ByLigneCorresp_Thread));

                    CorrespondanceBLL p1 = ligne_correspondance_selectionnee_objet;
                    List<ReferentielBLL> p2 = VariablePartage.TableReferentielFiltre;
                    List<ReferentielBLL> p3 = VariablePartage.TableReferentiel;
                    List<ReferentielBLL> p4 = TableRefLevenshtein;
                    string p5 = CPLCLEF;
                    Form1 p6 = myform;
                    object args = new object[6] { p1, p2, p3, p4, p5, p6 };

                    

                    saisieCellClick_ByLigneCorresp_Thread.Start(args);

                    
                }
            }
            else
            {

                //myform.button_deleteRecodage.Enabled = false;
                //myform.button_affecter.Enabled = false;
                //myform.button_afficherCreationCode.Enabled = false;
                //myform.button_creercode.Enabled = false;
                //myform.textBox_codeacreer.Text = "";
                //myform.textBox_libellecodeacreer.Text = "";
                //myform.textBox_codeacreer.Enabled = false;
                //myform.textBox_libellecodeacreer.Enabled = false;
                //myform.checkBox_codeacreer_actif_inactif.Enabled = false;
                //myform.button_nepasreprendre.Enabled = false;
            }
        }

        

        public void SaisieCellClick_archive(DataGridView dataGridView_saisie, DataGridView dataGridView_ref, Form1 myform, string CPLCLEF)
        {
            if (dataGridView_saisie.CurrentCell != null && dataGridView_saisie.CurrentCell.ColumnIndex.Equals(4) && dataGridView_saisie.CurrentCell.RowIndex != -1)
            {
                //myform.button_deleteRecodage.Enabled = true;
                //myform.button_affecter.Enabled = true;
                //myform.button_afficherCreationCode.Enabled = true;
                //myform.button_creercode.Enabled = false;
                //myform.textBox_codeacreer.Text = "";
                //myform.textBox_libellecodeacreer.Text = "";
                //myform.textBox_codeacreer.Enabled = false;
                //myform.textBox_libellecodeacreer.Enabled = false;
                //myform.checkBox_codeacreer_actif_inactif.Enabled = false;
                //myform.button_nepasreprendre.Enabled = true;
                myform.textBox_search.Enabled = true;
                myform.button_del_filter_search.Enabled = true;
                //myform.button_nepasreprendre_module.Enabled = true;
                //myform.button_nepasreprendre_global.Enabled = true;
                myform.textBox_search.Text = "";


                if (dataGridView_saisie.CurrentCell != null)
                {
                    CorrespondanceBLL ligne_correspondance_selectionnee_datagrid = new CorrespondanceBLL();
                    ligne_correspondance_selectionnee_datagrid = ligne_correspondance_selectionnee_datagrid.RetourneLigneCorrespondanceDatagrid(dataGridView_saisie, dataGridView_saisie.CurrentCell);

                    CorrespondanceBLL ligne_correspondance_selectionnee_objet = new CorrespondanceBLL();
                    ligne_correspondance_selectionnee_objet = ligne_correspondance_selectionnee_objet.TrouveLigneCorrespondance_ByLigne(ligne_correspondance_selectionnee_datagrid, VariablePartage.TableCorrespondanceFiltre);

                    List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();
                    TableRefLevenshtein = SaisieCellClick_ByLigneCorresp(ligne_correspondance_selectionnee_objet, VariablePartage.TableReferentielFiltre, VariablePartage.TableReferentiel, CPLCLEF);

                    var bindingListRef = new BindingList<ReferentielBLL>(TableRefLevenshtein);
                    var source_ref = new BindingSource(bindingListRef, null);
                    dataGridView_ref.DataSource = source_ref;
                    //InitStructReferentiel();
                    GUI.InitColorReferentiel(dataGridView_ref);
                }
            }
            else
            {

                //myform.button_deleteRecodage.Enabled = false;
                //myform.button_affecter.Enabled = false;
                //myform.button_afficherCreationCode.Enabled = false;
                //myform.button_creercode.Enabled = false;
                //myform.textBox_codeacreer.Text = "";
                //myform.textBox_libellecodeacreer.Text = "";
                //myform.textBox_codeacreer.Enabled = false;
                //myform.textBox_libellecodeacreer.Enabled = false;
                //myform.checkBox_codeacreer_actif_inactif.Enabled = false;
                //myform.button_nepasreprendre.Enabled = false;
            }
        }

        public void FilterModule(DataGridView dataGridView_saisie, DataGridView dataGridView_ref, Form1 myform)
        {
            if ((string)myform.comboBox_filtre.SelectedValue != null)
            {
                var source_corr = new BindingSource();
                var source_ref = new BindingSource();
                
                //Console.WriteLine(comboBox_filtre.ValueMember);
                //Console.WriteLine("(string)comboBox_filtre.SelectedValue : " + (string)comboBox_filtre.SelectedValue);
                VariablePartage.TableCorrespondanceFiltre = CorrObjectBLL.FiltrerListeCorrespondance_parCPL(VariablePartage.TableCorrespondance, (string)myform.comboBox_filtre.SelectedValue);
                if (myform.checkBox_nontraite.Checked)
                {
                    List<CorrespondanceBLL> CorrespNonTraite = new List<CorrespondanceBLL>();
                    CorrespNonTraite = CorrObjectBLL.RetourneSeulementNonTraite(VariablePartage.TableCorrespondanceFiltre);

                    var bindingListCorr = new BindingList<CorrespondanceBLL>(CorrespNonTraite);
                    source_corr = new BindingSource(bindingListCorr, null);
                    dataGridView_saisie.DataSource = source_corr;

                    //source_corr.DataSource = CorrespNonTraite;
                    //dataGridView_saisie.DataSource = source_corr;
                    GUI.InitStructSaisie(dataGridView_saisie, myform);
                    GUI.InitColorSaisie(dataGridView_saisie);
                }
                else
                {
                    //Filtre la table de corresp globale par CPL


                    //Créé une nouvelle liste de type affichage qui va etre affectée de la liste ci-dessus (liste globale filtrée).
                    //CorrespondancesAffichages CorrAffichage = new CorrespondancesAffichages();
                    //List<CorrespondanceAffichage> TableCorrespAffichage = CorrAffichage.ObtenirListeCorrespondanceAffichage(Init.TableCorrespondanceFiltre);

                    //Réaffecte le datagrid à la liste filtrée 

                    var bindingListCorr = new BindingList<CorrespondanceBLL>(VariablePartage.TableCorrespondanceFiltre);
                    source_corr = new BindingSource(bindingListCorr, null);
                    dataGridView_saisie.DataSource = source_corr;

                    //source_corr.DataSource = VariablePartage.TableCorrespondanceFiltre;
                    //dataGridView_saisie.DataSource = source_corr;
                    GUI.InitStructSaisie(dataGridView_saisie, myform);
                    GUI.InitColorSaisie(dataGridView_saisie);
                }
                //ReferentielDAL Ref = new ReferentielDAL();
                //List<Referentiel> TableRef = Ref.ObtenirListeReferentiel_SQLITE();
                //Console.WriteLine("(string)comboBox_filtre.SelectedValue : " + (string)comboBox_filtre.SelectedValue);
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);
                //ReferentielsAffichages RefAffichage = new ReferentielsAffichages();
                //List<ReferentielAffichage> TableRefAffichage = RefAffichage.ObtenirListeReferentielAffichage(Init.TableReferentielFiltre);

                var bindingListRef = new BindingList<ReferentielBLL>(VariablePartage.TableReferentielFiltre);
                source_ref = new BindingSource(bindingListRef, null);
                dataGridView_ref.DataSource = source_ref;

                //source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                //dataGridView_ref.DataSource = source_ref;
                GUI.InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                GUI.InitColorReferentiel(dataGridView_ref);

                SaisieCellClick(dataGridView_saisie, dataGridView_ref, myform, (string)myform.comboBox_filtre.SelectedValue);

            }
        }

        public void NePasReprendre(DataGridView dataGridView_saisie, DataGridView dataGridView_ref,Form1 myform)
        {
            int maxindex;
            maxindex = DesaffecteCode(dataGridView_saisie, dataGridView_ref, myform);
            maxindex = AffectationNePasReprendre(dataGridView_saisie, dataGridView_ref);
            //BindDatagrid(dataGridView_saisie, Init.TableCorrespondanceFiltre);
            GUI.InitStructSaisie(dataGridView_saisie, myform);
            GUI.InitColorSaisie(dataGridView_saisie);
            CalCulCelluleFocus(dataGridView_saisie, maxindex, myform.checkBox_nontraite);
            SaisieCellClick(dataGridView_saisie, dataGridView_ref, myform, (string)myform.comboBox_filtre.SelectedValue);
        }

        public int AffectationNePasReprendre(DataGridView dataGridView_saisie, DataGridView dataGridView_ref)
        {
            int maxindex = 0;
            if (dataGridView_saisie.CurrentCell != null && dataGridView_saisie.CurrentCell.RowIndex != -1)
            {
                CorrespondanceBLL ligne = new CorrespondanceBLL();
                foreach (DataGridViewCell Cell in dataGridView_saisie.SelectedCells)
                {
                    ligne.Ancien_Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString();
                    ligne.NomRef = dataGridView_saisie.Rows[Cell.RowIndex].Cells[15].Value.ToString();
                    ligne.FlagReferentiel = 3; // 3 : Ne pas reprendre le code
                    ligne.NePasReprendre = true;
                    dataGridView_saisie.Rows[Cell.RowIndex].Cells[20].Value = 3;
                    dataGridView_saisie.Rows[Cell.RowIndex].Cells[23].Value = true;
                    ligne.Libelle_Nouveau_Code = "Non repris";
                    ligne.Nouveau_Code = "";
                    dataGridView_saisie.Rows[Cell.RowIndex].Cells[4].Value = "";
                    dataGridView_saisie.Rows[Cell.RowIndex].Cells[5].Value = "Non repris";

                    if ((string)dataGridView_saisie.Rows[Cell.RowIndex].Cells[15].Value == "Examen")
                    {
                        VariablePartage.TableCorrespondanceFiltre = CorrObjectBLL.DeleteRecodageResultatExamenByExamen(VariablePartage.TableCorrespondanceFiltre, dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString());
                    }

                    if ((string)dataGridView_saisie.Rows[Cell.RowIndex].Cells[15].Value == "Vaccin")
                    {
                        VariablePartage.TableCorrespondanceFiltre = CorrObjectBLL.DeleteRecodageProtocoleVaccByVaccin(VariablePartage.TableCorrespondanceFiltre, dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString());
                    }

                    CorrObjectDAL.UpdateSQLITE_TBCorrespondance(ligne);
                    CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(ligne);
                    if (maxindex < Cell.RowIndex)
                    {
                        maxindex = Cell.RowIndex;
                    }
                }
            }
            return maxindex;
        }

        public void DeleteButton(DataGridView dataGridView_saisie, DataGridView dataGridView_ref,Form1 myform)
        {
            int maxindex;
            maxindex = DesaffecteCode(dataGridView_saisie, dataGridView_ref, myform);
            //BindDatagrid(dataGridView_saisie, Init.TableCorrespondanceFiltre);
            GUI.InitStructSaisie(dataGridView_saisie, myform);
            GUI.InitColorSaisie(dataGridView_saisie);
            CalCulCelluleFocus(dataGridView_saisie, maxindex, myform.checkBox_nontraite, "Suppr");
            SaisieCellClick(dataGridView_saisie, dataGridView_ref, myform, (string)myform.comboBox_filtre.SelectedValue);
        }

        public void CreerCodeMemeCodeMemeLibelle(DataGridView dataGridView_saisie, DataGridView dataGridView_ref,Form1 myform, bool CodeActif)
        {
            int maxindex;
            maxindex = DesaffecteCode(dataGridView_saisie, dataGridView_ref, myform);
            maxindex = AffectationCodeCreeMemeCodeMemeLibelle(dataGridView_saisie, dataGridView_ref, myform, CodeActif);
            GUI.InitStructSaisie(dataGridView_saisie, myform);
            GUI.InitColorSaisie(dataGridView_saisie);
            CalCulCelluleFocus(dataGridView_saisie, maxindex, myform.checkBox_nontraite);
            SaisieCellClick(dataGridView_saisie, dataGridView_ref, myform, (string)myform.comboBox_filtre.SelectedValue);
        }

        public void AffectationCodeCreeMemeCodeMemeLibelle_ByLigneCorrRef(CorrespondanceBLL ligne_correspondance_objet, List<CorrespondanceBLL> TableCorrespondance , List<CorrespondanceBLL> TableCorrespondanceFiltre, List<ReferentielBLL> TableReferentiel, List<ReferentielBLL> TableReferentielFiltre, string CPLCLEF,bool CodeActif, Form1 myform)
        {

            //Si le code et le libellé sont renseignés alors on avance, sinon rejet
            if (ligne_correspondance_objet.Ancien_Code != null && ligne_correspondance_objet.Libelle_Ancien_Code != null
                && ligne_correspondance_objet.Ancien_Code != "" && ligne_correspondance_objet.Libelle_Ancien_Code != "")
            {

                //recharge le referentiel filtre
                TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(TableReferentiel, CPLCLEF);

                //Si le code n'est pas dans le referentiel alors on avance, sinon rejet
                if (!RefObjectBLL.CodeEstDansLeReferentiel(TableReferentielFiltre, ligne_correspondance_objet.Ancien_Code))
                {
                    
                    //Cas particulier resultat examen, Si l'examen parent est recodé on avance, sinon rejet
                    if (ligne_correspondance_objet.Cpl == "50" && ligne_correspondance_objet.Ancien_Code.Contains("#") && !CorrObjectBLL.IsExamenParentRecode_ByLigneCorr(ligne_correspondance_objet, TableCorrespondanceFiltre))
                    {
                        //Cell.ToolTipText = "Impossible de créer un resultat examen si l'examen parent n'est pas recodé.";
                        goto End;
                    }
                    //Cas particulier protocole vaccinal, Si le vaccin parent est recodé on avance, sinon rejet
                    else if (ligne_correspondance_objet.Cpl == "60" && ligne_correspondance_objet.Ancien_Code.Contains("#") && !CorrObjectBLL.IsVaccinParentRecode_ByLigneCorr(ligne_correspondance_objet, TableCorrespondanceFiltre))
                    {
                        //Cell.ToolTipText = "Impossible de créer un protocole vaccinal si le vaccin parent n'est pas recodé.";
                        goto End;
                    }
                    //Nouveau_code = ancien_code et Libelle_nouveau_code = libelle_ancien_code
                    ligne_correspondance_objet.Nouveau_Code = ligne_correspondance_objet.Ancien_Code;
                    ligne_correspondance_objet.Libelle_Nouveau_Code = ligne_correspondance_objet.Libelle_Ancien_Code;
                    
                    ligne_correspondance_objet.FlagReferentiel = 2;

                    //Ajout du flag actif ou inactif
                    if(CodeActif==true)
                    {
                        ligne_correspondance_objet.CreerActif = true;
                    }
                    else
                    {
                        ligne_correspondance_objet.CreerInactif = true;
                        ligne_correspondance_objet.NouveauCodeInactif = true;
                    }
                    ligne_correspondance_objet.NePasReprendre = false;

                    //Cas particulier du rapprochementEntEtab
                    if(ligne_correspondance_objet.Cpl == "1000")
                    {
                        ligne_correspondance_objet.Cpl = "3";
                    }

                    CorrObjectDAL.UpdateSQLITE_TBCorrespondance(ligne_correspondance_objet);
                    CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(ligne_correspondance_objet);
                    TableCorrespondance = CorrObjectBLL.RetourneListeCorrespondanceUpdater(TableCorrespondance, ligne_correspondance_objet, ligne_correspondance_objet.NomRef);
                    ReferentielBLL ligne_referentiel_cree_by_corresp = new ReferentielBLL();
                    ligne_referentiel_cree_by_corresp = ligne_referentiel_cree_by_corresp.CreerLigneReferentiel_ByLigneCorresp(ligne_correspondance_objet);
                    ligne_referentiel_cree_by_corresp.InActif = !CodeActif;
                    List<ReferentielBLL> ligne_referentiel_cree_by_corresp_list = new List<ReferentielBLL>();
                    ligne_referentiel_cree_by_corresp_list.Add(ligne_referentiel_cree_by_corresp);
                    RefObjectDAL.InsertIntoSQLITE_TBReferentiel(ligne_referentiel_cree_by_corresp_list);
                    TableReferentiel.Add(ligne_referentiel_cree_by_corresp);
                    TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(TableReferentiel, CPLCLEF);

                    //TableCorrespondanceFiltre = CorrObjectBLL.FiltrerListeCorrespondance_parCPL(TableCorrespondance, CPLCLEF);

                    VariablePartage.TableCorrespondance = TableCorrespondance;
                    VariablePartage.TableCorrespondanceFiltre = TableCorrespondanceFiltre;
                    VariablePartage.TableReferentiel = TableReferentiel;
                    VariablePartage.TableReferentielFiltre = TableReferentielFiltre;
                    
                    //Cas particulier des etablissements, on lance la fenetre de rattachement aux entreprises
                    if (ligne_correspondance_objet.Cpl == "1")
                    {
                        RattachementEtablissementEntreprise rattachementEtablissementEntreprise = new RattachementEtablissementEntreprise();
                        rattachementEtablissementEntreprise.form1 = myform;
                        rattachementEtablissementEntreprise.ShowDialog();
                    }

                    
                    

                }
                else
                {
                    MessageBox.Show("Impossible de créer ce code car il existe déja dans le référentiel");
                }
                End:;
            }
            
            else
            {
                //Cell.ToolTipText = "Code et libellé obligatoire";
                MessageBox.Show("Code et libellé obligatoire");
            }
        }

        public int AffectationCodeCreeMemeCodeMemeLibelle(DataGridView dataGridView_saisie, DataGridView dataGridView_ref, Form1 myform, bool CodeActif)
        {
            int maxindex = 0;
            foreach (DataGridViewCell Cell in dataGridView_saisie.SelectedCells)
            {
                //Ligne corresp extrait du datagrid_saisie
                CorrespondanceBLL ligne_correspondance_datagrid = CorrObjectBLL.RetourneLigneCorrespondanceDatagrid(dataGridView_saisie, Cell);
                //Ligne corresp extrait de l'objet tableCorrespondanceFiltre
                CorrespondanceBLL ligne_correspondance_objet = CorrObjectBLL.TrouveLigneCorrespondance_ByLigne(ligne_correspondance_datagrid, VariablePartage.TableCorrespondanceFiltre);
                AffectationCodeCreeMemeCodeMemeLibelle_ByLigneCorrRef(ligne_correspondance_objet,VariablePartage.TableCorrespondance,VariablePartage.TableCorrespondanceFiltre, VariablePartage.TableReferentiel, VariablePartage.TableReferentielFiltre, (string)myform.comboBox_filtre.SelectedValue, CodeActif, myform);
                if (maxindex < Cell.RowIndex)
                {
                    maxindex = Cell.RowIndex;
                }
            }
            return maxindex;
        }

        

        public int AffectationCodeCreeInactifMemeCodeMemeLibelle_archive(DataGridView dataGridView_saisie, DataGridView dataGridView_ref,Form1 myform)
        {
            int maxindex = 0;
            foreach (DataGridViewCell Cell in dataGridView_saisie.SelectedCells)
            {
                if ((string)dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value != "" && (string)dataGridView_saisie.Rows[Cell.RowIndex].Cells[2].Value != ""
                    && (string)dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value != null && (string)dataGridView_saisie.Rows[Cell.RowIndex].Cells[2].Value != null)
                {

                    CorrespondanceBLL ligne_correspondance_datagrid = CorrObjectBLL.RetourneLigneCorrespondanceDatagrid(dataGridView_saisie, Cell);

                    CorrespondanceBLL ligne_correspondance_objet = CorrObjectBLL.TrouveLigneCorrespondance_ByLigne(ligne_correspondance_datagrid, VariablePartage.TableCorrespondanceFiltre);


                    //Créé une ligne correspondance qui représente la ligne à mettre à jour.
                    CorrespondanceBLL ligne = new CorrespondanceBLL();
                    ligne.Ancien_Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString();
                    ligne.Nouveau_Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString();
                    ligne.Libelle_Nouveau_Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[2].Value.ToString();
                    ligne.NomRef = dataGridView_saisie.Rows[Cell.RowIndex].Cells[15].Value.ToString();
                    ligne.FlagReferentiel = 2;
                    ligne.NouveauCodeInactif = true;

                    //Filtre le referentiel
                    VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);

                    if (RefObjectBLL.CodeEstDansLeReferentiel(VariablePartage.TableReferentielFiltre, ligne.Nouveau_Code))
                    {
                        MessageBox.Show("Impossible de créer ce code, il éxiste déjà dans le référentiel.");
                        maxindex = Cell.RowIndex;
                    }
                    else
                    {
                        if (dataGridView_saisie.Rows[Cell.RowIndex].Cells[16].Value.ToString() == "50" && dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString().Contains("#"))
                        {
                            string[] examen_ancien_code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString().Split('#');
                            CorrespondanceBLL ligne_exam_nouveau_code = CorrObjectBLL.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, examen_ancien_code[0], "135|0|0|NOMEN");
                            if (ligne_exam_nouveau_code.Nouveau_Code == null || ligne_exam_nouveau_code.Nouveau_Code == "")
                            {
                                MessageBox.Show("Impossible de récoder un résultat examen sans recoder l'examen parent d'abord.");
                                Fonc fonc = new Fonc();
                                maxindex = fonc.RetourneIndexDatagridViewByAncienCode(dataGridView_saisie, examen_ancien_code[0]);
                            }
                            else
                            {
                                /*
                                if (Convert.ToInt32(dataGridView_saisie.Rows[Cell.RowIndex].Cells[20].Value) == 2 && CorrObjectBLL.RetourneNombreRecodageMemeCode(Init.TableCorrespondanceFiltre, dataGridView_saisie.Rows[Cell.RowIndex].Cells[4].Value.ToString()) < 2)
                                {
                                    List<Referentiel> ligneref12 = new List<Referentiel>();
                                    Referentiel ligneref2 = new Referentiel();
                                    ligneref2.Type = dataGridView_saisie.Rows[Cell.RowIndex].Cells[14].Value.ToString();
                                    ligneref2.Cpl = dataGridView_saisie.Rows[Cell.RowIndex].Cells[16].Value.ToString();
                                    ligneref2.Cpl1 = ligne_exam_nouveau_code.Nouveau_Code;
                                    ligneref2.Cpl2 = dataGridView_saisie.Rows[Cell.RowIndex].Cells[18].Value.ToString();
                                    ligneref2.Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[4].Value.ToString();
                                    ligneref2.Lib = dataGridView_saisie.Rows[Cell.RowIndex].Cells[5].Value.ToString();
                                    ligneref12.Add(ligneref2);
                                    RefObjectBLL.DeleteFromSQLITE_TBReferentiel(ligneref12);
                                    Init.TableReferentiel = RefObjectBLL.SuppressionItem_RetourneListeReferentiel(Init.TableReferentiel, ligneref2);
                                    Init.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(Init.TableReferentiel, (string)comboBox_filtre.SelectedValue);
                                    GUI.InitStructReferentiel(dataGridView_ref,dataGridView_saisie);
                                    GUI.InitColorReferentiel(dataGridView_ref);
                                }
                                */
                                dataGridView_saisie.Rows[Cell.RowIndex].Cells[4].Value = dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value;
                                dataGridView_saisie.Rows[Cell.RowIndex].Cells[5].Value = dataGridView_saisie.Rows[Cell.RowIndex].Cells[2].Value;
                                dataGridView_saisie.Rows[Cell.RowIndex].Cells[12].Value = true;
                                dataGridView_saisie.Rows[Cell.RowIndex].Cells[20].Value = 2;



                                ligne.Ancien_Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString();
                                ligne.Nouveau_Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[4].Value.ToString();
                                ligne.Libelle_Nouveau_Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[5].Value.ToString();
                                ligne.NomRef = dataGridView_saisie.Rows[Cell.RowIndex].Cells[15].Value.ToString();
                                //ligne.Cpl1 = ligne_exam_nouveau_code.Nouveau_Code;
                                ligne.Cpl1 = dataGridView_saisie.Rows[Cell.RowIndex].Cells[17].Value.ToString();
                                ligne.FlagReferentiel = 2;
                                ligne.NouveauCodeInactif = (bool)dataGridView_saisie.Rows[Cell.RowIndex].Cells[12].Value;

                                CorrObjectDAL.UpdateSQLITE_TBCorrespondance(ligne);
                                CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(ligne);

                                List<ReferentielBLL> ligneref = new List<ReferentielBLL>();
                                ReferentielBLL ligneref1 = new ReferentielBLL();
                                ligneref1.Code = ligne.Nouveau_Code;
                                ligneref1.Lib = ligne.Libelle_Nouveau_Code;
                                ligneref1.InActif = true;
                                ligneref1.Type = dataGridView_saisie.CurrentRow.Cells[14].Value.ToString();
                                ligneref1.TypeItem = dataGridView_saisie.CurrentRow.Cells[15].Value.ToString();
                                ligneref1.Cpl = dataGridView_saisie.CurrentRow.Cells[16].Value.ToString();

                                /*
                                if (dataGridView_saisie.CurrentRow.Cells[17].Value == null)
                                {
                                    dataGridView_saisie.CurrentRow.Cells[17].Value = "";
                                }
                                ligneref1.Cpl1 = dataGridView_saisie.CurrentRow.Cells[17].Value.ToString();
                                */

                                ligneref1.Cpl1 = ligne_exam_nouveau_code.Nouveau_Code;

                                if (dataGridView_saisie.CurrentRow.Cells[18].Value == null)
                                {
                                    dataGridView_saisie.CurrentRow.Cells[18].Value = "";
                                }
                                ligneref1.Cpl2 = dataGridView_saisie.CurrentRow.Cells[18].Value.ToString();

                                ligneref1.FlagPreventiel = 2;

                                ligneref.Add(ligneref1);

                                RefObjectDAL.InsertIntoSQLITE_TBReferentiel(ligneref);

                                VariablePartage.TableReferentiel.Add(ligneref1);
                                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);

                                var source_ref = new BindingSource();
                                source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                                dataGridView_ref.DataSource = source_ref;
                                GUI.InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                                GUI.InitColorReferentiel(dataGridView_ref);

                                VariablePartage.TableCorrespondance = CorrObjectBLL.RetourneListeCorrespondanceUpdater(VariablePartage.TableCorrespondance, ligne, ligne.NomRef);

                                if (maxindex < Cell.RowIndex)
                                {
                                    maxindex = Cell.RowIndex;
                                }
                            }
                        }
                        else if (dataGridView_saisie.Rows[Cell.RowIndex].Cells[16].Value.ToString() == "60" && dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString().Contains("#"))
                        {
                            string[] examen_ancien_code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString().Split('#');
                            CorrespondanceBLL ligne_exam_nouveau_code = CorrObjectBLL.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, examen_ancien_code[0], "141|0|0|NOMEN");
                            if (ligne_exam_nouveau_code.Nouveau_Code == null || ligne_exam_nouveau_code.Nouveau_Code == "")
                            {
                                MessageBox.Show("Impossible de récoder protocole vaccinal sans recoder le vaccin parent d'abord.");
                                Fonc fonc = new Fonc();
                                maxindex = fonc.RetourneIndexDatagridViewByAncienCode(dataGridView_saisie, examen_ancien_code[0]);
                            }
                            else
                            {
          

                                dataGridView_saisie.Rows[Cell.RowIndex].Cells[4].Value = dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value;
                                dataGridView_saisie.Rows[Cell.RowIndex].Cells[5].Value = dataGridView_saisie.Rows[Cell.RowIndex].Cells[2].Value;
                                dataGridView_saisie.Rows[Cell.RowIndex].Cells[12].Value = true;
                                dataGridView_saisie.Rows[Cell.RowIndex].Cells[20].Value = 2;


                                ligne.Ancien_Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString();
                                ligne.Nouveau_Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[4].Value.ToString();
                                ligne.Libelle_Nouveau_Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[5].Value.ToString();
                                ligne.NomRef = dataGridView_saisie.Rows[Cell.RowIndex].Cells[15].Value.ToString();
                                ligne.FlagReferentiel = 2;
                                ligne.NouveauCodeInactif = (bool)dataGridView_saisie.Rows[Cell.RowIndex].Cells[12].Value;

                                CorrObjectDAL.UpdateSQLITE_TBCorrespondance(ligne);
                                CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(ligne);

                                List<ReferentielBLL> ligneref = new List<ReferentielBLL>();
                                ReferentielBLL ligneref1 = new ReferentielBLL();
                                ligneref1.Code = ligne.Nouveau_Code;
                                ligneref1.Lib = ligne.Libelle_Nouveau_Code;
                                ligneref1.InActif = true;
                                ligneref1.Type = dataGridView_saisie.CurrentRow.Cells[14].Value.ToString();
                                ligneref1.TypeItem = dataGridView_saisie.CurrentRow.Cells[15].Value.ToString();
                                ligneref1.Cpl = dataGridView_saisie.CurrentRow.Cells[16].Value.ToString();
                                /*
                                if (dataGridView_saisie.CurrentRow.Cells[17].Value == null)
                                {
                                    dataGridView_saisie.CurrentRow.Cells[17].Value = "";
                                }*/
                                ligneref1.Cpl1 = ligne_exam_nouveau_code.Nouveau_Code;

                                if (dataGridView_saisie.CurrentRow.Cells[18].Value == null)
                                {
                                    dataGridView_saisie.CurrentRow.Cells[18].Value = "";
                                }
                                ligneref1.Cpl2 = dataGridView_saisie.CurrentRow.Cells[18].Value.ToString();

                                ligneref1.FlagPreventiel = 2;

                                ligneref.Add(ligneref1);

                                RefObjectDAL.InsertIntoSQLITE_TBReferentiel(ligneref);

                                VariablePartage.TableReferentiel.Add(ligneref1);
                                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);

                                var source_ref = new BindingSource();
                                source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                                dataGridView_ref.DataSource = source_ref;
                                GUI.InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                                GUI.InitColorReferentiel(dataGridView_ref);

                                VariablePartage.TableCorrespondance = CorrObjectBLL.RetourneListeCorrespondanceUpdater(VariablePartage.TableCorrespondance, ligne, ligne.NomRef);

                                if (maxindex < Cell.RowIndex)
                                {
                                    maxindex = Cell.RowIndex;
                                }
                            }
                        }
                        else
                        {
                            

                            dataGridView_saisie.Rows[Cell.RowIndex].Cells[4].Value = dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value;
                            dataGridView_saisie.Rows[Cell.RowIndex].Cells[5].Value = dataGridView_saisie.Rows[Cell.RowIndex].Cells[2].Value;
                            dataGridView_saisie.Rows[Cell.RowIndex].Cells[12].Value = true;
                            dataGridView_saisie.Rows[Cell.RowIndex].Cells[20].Value = 2;


                            ligne.Ancien_Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[1].Value.ToString();
                            ligne.Nouveau_Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[4].Value.ToString();
                            ligne.Libelle_Nouveau_Code = dataGridView_saisie.Rows[Cell.RowIndex].Cells[5].Value.ToString();
                            ligne.NomRef = dataGridView_saisie.Rows[Cell.RowIndex].Cells[15].Value.ToString();
                            ligne.FlagReferentiel = 2;
                            ligne.NouveauCodeInactif = (bool)dataGridView_saisie.Rows[Cell.RowIndex].Cells[12].Value;

                            CorrObjectDAL.UpdateSQLITE_TBCorrespondance(ligne);
                            CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(ligne);

                            List<ReferentielBLL> ligneref = new List<ReferentielBLL>();
                            ReferentielBLL ligneref1 = new ReferentielBLL();
                            ligneref1.Code = ligne.Nouveau_Code;
                            ligneref1.Lib = ligne.Libelle_Nouveau_Code;
                            ligneref1.InActif = true;
                            ligneref1.Type = dataGridView_saisie.CurrentRow.Cells[14].Value.ToString();
                            ligneref1.TypeItem = dataGridView_saisie.CurrentRow.Cells[15].Value.ToString();
                            ligneref1.Cpl = dataGridView_saisie.CurrentRow.Cells[16].Value.ToString();

                            if (dataGridView_saisie.CurrentRow.Cells[17].Value == null)
                            {
                                dataGridView_saisie.CurrentRow.Cells[17].Value = "";
                            }
                            ligneref1.Cpl1 = dataGridView_saisie.CurrentRow.Cells[17].Value.ToString();

                            if (ligne.NomRef == "PersonnelMedical")
                            {
                                ligneref1.Cpl1 = "2";
                            }

                            if (dataGridView_saisie.CurrentRow.Cells[18].Value == null)
                            {
                                dataGridView_saisie.CurrentRow.Cells[18].Value = "";
                            }
                            ligneref1.Cpl2 = dataGridView_saisie.CurrentRow.Cells[18].Value.ToString();

                            ligneref1.FlagPreventiel = 2;

                            ligneref.Add(ligneref1);

                            RefObjectDAL.InsertIntoSQLITE_TBReferentiel(ligneref);

                            //Init.TableReferentiel = RefObjectBLL.RetourneListeReferentielUpdater(Init.TableReferentiel, ligneref1, ligneref1.Type, ligneref1.Cpl, ligneref1.Cpl1, ligneref1.Cpl2);
                            VariablePartage.TableReferentiel.Add(ligneref1);
                            VariablePartage.TableReferentiel = RefObjectBLL.RetourneListeReferentielUpdater(VariablePartage.TableReferentiel, ligneref1, ligneref1.Type, ligneref1.Cpl, ligneref1.Cpl1, ligneref1.Cpl2);
                            VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);

                            var source_ref = new BindingSource();
                            source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                            dataGridView_ref.DataSource = source_ref;
                            GUI.InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                            GUI.InitColorReferentiel(dataGridView_ref);

                            VariablePartage.TableCorrespondance = CorrObjectBLL.RetourneListeCorrespondanceUpdater(VariablePartage.TableCorrespondance, ligne, ligne.NomRef);

                            if (maxindex < Cell.RowIndex)
                            {
                                maxindex = Cell.RowIndex;
                            }
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Code et Libellé obligatoire. Veuillez passer par l'interface avancée.");
                    maxindex = Cell.RowIndex - 1;
                }
            }
            return maxindex;
        }


        public void CreerCodeInterfaceAvancee(DataGridView dataGridView_saisie, DataGridView dataGridView_ref, Form1 myform, string textBox_codeacreer, string textBox_libellecodeacreer,bool CodeActif)
        {
            int maxindex;
            maxindex = DesaffecteCode(dataGridView_saisie, dataGridView_ref, myform);
            AffectationCodeCreeInterfaceAvancee(dataGridView_saisie, dataGridView_ref, myform, textBox_codeacreer, textBox_libellecodeacreer, CodeActif);
            //BindDatagrid(dataGridView_saisie, Init.TableCorrespondanceFiltre);
            GUI.InitStructSaisie(dataGridView_saisie, myform);
            GUI.InitColorSaisie(dataGridView_saisie);
            CalCulCelluleFocus(dataGridView_saisie, maxindex, myform.checkBox_nontraite);
            SaisieCellClick(dataGridView_saisie, dataGridView_ref, myform, (string)myform.comboBox_filtre.SelectedValue);
        }

        public void AffectationCodeCreeInterfaceAvancee(DataGridView dataGridView_saisie, DataGridView dataGridView_ref, Form1 myform, string textBox_codeacreer, string textBox_libellecodeacreer, bool CodeActif)
        {
            if (dataGridView_saisie.CurrentCell != null && dataGridView_saisie.CurrentCell.RowIndex != -1)
            {
                if (textBox_codeacreer != "" && textBox_libellecodeacreer != "")
                {
                    //Fait l'affectation du nouveau code en base
                    CorrespondanceBLL ligne = new CorrespondanceBLL();
                    ligne.Ancien_Code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString();
                    ligne.Nouveau_Code = textBox_codeacreer;
                    ligne.Libelle_Nouveau_Code = textBox_libellecodeacreer;
                    ligne.NomRef = dataGridView_saisie.CurrentRow.Cells[15].Value.ToString();
                    ligne.FlagReferentiel = 2;
                    ligne.NouveauCodeInactif = !CodeActif;
                    if (CodeActif == true)
                    {
                        ligne.CreerActif = true;
                    }
                    else
                    {
                        ligne.CreerInactif = true;
                        ligne.NouveauCodeInactif = true;
                    }
                    ligne.NePasReprendre = false;
                    VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);

                    if (RefObjectBLL.CodeEstDansLeReferentiel(VariablePartage.TableReferentielFiltre, ligne.Nouveau_Code))
                    {
                        MessageBox.Show("Impossible de créer ce code, il éxiste déjà dans le référentiel.");
                    }
                    else
                    {
                        int maxindex = dataGridView_saisie.CurrentRow.Index;
                        if (dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() == "50" && dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Contains("#"))
                        {
                            string[] examen_ancien_code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Split('#');
                            CorrespondanceBLL ligne_exam_nouveau_code = CorrObjectBLL.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, examen_ancien_code[0], "135|0|0|NOMEN");
                            if (ligne_exam_nouveau_code.Nouveau_Code == null || ligne_exam_nouveau_code.Nouveau_Code == "")
                            {
                                MessageBox.Show("Impossible de récoder un résultat examen sans recoder l'examen parent d'abord.");
                                Fonc fonc = new Fonc();
                                maxindex = fonc.RetourneIndexDatagridViewByAncienCode(dataGridView_saisie, examen_ancien_code[0]);
                            }
                            else
                            {
                                if (Convert.ToInt32(dataGridView_saisie.CurrentRow.Cells[20].Value) == 2 && CorrObjectBLL.RetourneNombreRecodageMemeCode(VariablePartage.TableCorrespondanceFiltre, dataGridView_saisie.CurrentRow.Cells[4].Value.ToString()) < 2)
                                {
                                    List<ReferentielBLL> ligneref12 = new List<ReferentielBLL>();
                                    ReferentielBLL ligneref2 = new ReferentielBLL();
                                    ligneref2.Type = dataGridView_saisie.CurrentRow.Cells[14].Value.ToString();
                                    ligneref2.Cpl = dataGridView_saisie.CurrentRow.Cells[16].Value.ToString();
                                    ligneref2.Cpl1 = ligne_exam_nouveau_code.Nouveau_Code;
                                    ligneref2.Cpl2 = dataGridView_saisie.CurrentRow.Cells[18].Value.ToString();
                                    ligneref2.Code = dataGridView_saisie.CurrentRow.Cells[4].Value.ToString();
                                    ligneref2.Lib = dataGridView_saisie.CurrentRow.Cells[5].Value.ToString();
                                    ligneref12.Add(ligneref2);
                                    RefObjectDAL.DeleteFromSQLITE_TBReferentiel(ligneref12);
                                    VariablePartage.TableReferentiel = RefObjectBLL.SuppressionItem_RetourneListeReferentiel(VariablePartage.TableReferentiel, ligneref2);
                                    VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);
                                    GUI.InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                                    GUI.InitColorReferentiel(dataGridView_ref);
                                }

                                dataGridView_saisie.CurrentRow.Cells[4].Value = textBox_codeacreer;
                                dataGridView_saisie.CurrentRow.Cells[5].Value = textBox_libellecodeacreer;
                                dataGridView_saisie.CurrentRow.Cells[12].Value = !CodeActif;
                                dataGridView_saisie.CurrentRow.Cells[20].Value = 2;

                                ligne.Ancien_Code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString();
                                ligne.Nouveau_Code = dataGridView_saisie.CurrentRow.Cells[4].Value.ToString();
                                ligne.Libelle_Nouveau_Code = dataGridView_saisie.CurrentRow.Cells[5].Value.ToString();
                                ligne.NomRef = dataGridView_saisie.CurrentRow.Cells[15].Value.ToString();
                                ligne.FlagReferentiel = 2;
                                ligne.NouveauCodeInactif = !CodeActif;

                                if (CodeActif == true)
                                {
                                    ligne.CreerActif = true;
                                }
                                else
                                {
                                    ligne.CreerInactif = true;
                                    ligne.NouveauCodeInactif = true;
                                }
                                ligne.NePasReprendre = false;

                                List<ReferentielBLL> ligneref = new List<ReferentielBLL>();
                                ReferentielBLL ligneref1 = new ReferentielBLL();
                                ligneref1.Code = ligne.Nouveau_Code;
                                ligneref1.Lib = ligne.Libelle_Nouveau_Code;
                                ligneref1.InActif = !CodeActif;
                                ligneref1.Type = dataGridView_saisie.CurrentRow.Cells[14].Value.ToString();
                                ligneref1.TypeItem = dataGridView_saisie.CurrentRow.Cells[15].Value.ToString();
                                ligneref1.Cpl = dataGridView_saisie.CurrentRow.Cells[16].Value.ToString();

                                /*if (dataGridView_saisie.CurrentRow.Cells[17].Value==null)
                                {
                                    dataGridView_saisie.CurrentRow.Cells[17].Value = "";
                                }
                                ligneref1.Cpl1 = dataGridView_saisie.CurrentRow.Cells[17].Value.ToString();*/
                                ligneref1.Cpl1 = ligne_exam_nouveau_code.Nouveau_Code;
                                if (dataGridView_saisie.CurrentRow.Cells[18].Value == null)
                                {
                                    dataGridView_saisie.CurrentRow.Cells[18].Value = "";
                                }
                                ligneref1.Cpl2 = dataGridView_saisie.CurrentRow.Cells[18].Value.ToString();

                                ligneref1.FlagPreventiel = 2;

                                ligneref.Add(ligneref1);

                                RefObjectDAL.InsertIntoSQLITE_TBReferentiel(ligneref);

                                VariablePartage.TableReferentiel.Add(ligneref1);
                                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);

                                var source_ref = new BindingSource();
                                source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                                dataGridView_ref.DataSource = source_ref;
                                GUI.InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                                GUI.InitColorReferentiel(dataGridView_ref);

                                CorrObjectDAL.UpdateSQLITE_TBCorrespondance(ligne);
                                CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(ligne);

                                VariablePartage.TableCorrespondance = CorrObjectBLL.RetourneListeCorrespondanceUpdater(VariablePartage.TableCorrespondance, ligne, ligne.NomRef);



                            }

                        }
                        else if (dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() == "60" && dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Contains("#"))
                        {
                            string[] examen_ancien_code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Split('#');
                            CorrespondanceBLL ligne_exam_nouveau_code = CorrObjectBLL.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, examen_ancien_code[0], "141|0|0|NOMEN");
                            if (ligne_exam_nouveau_code.Nouveau_Code == null || ligne_exam_nouveau_code.Nouveau_Code == "")
                            {
                                MessageBox.Show("Impossible de récoder protocole vaccinal sans recoder le vaccin parent d'abord.");
                                Fonc fonc = new Fonc();
                                maxindex = fonc.RetourneIndexDatagridViewByAncienCode(dataGridView_saisie, examen_ancien_code[0]);
                            }
                            else
                            {
                                if (Convert.ToInt32(dataGridView_saisie.CurrentRow.Cells[20].Value) == 2 && CorrObjectBLL.RetourneNombreRecodageMemeCode(VariablePartage.TableCorrespondanceFiltre, dataGridView_saisie.CurrentRow.Cells[4].Value.ToString()) < 2)
                                {
                                    List<ReferentielBLL> ligneref12 = new List<ReferentielBLL>();
                                    ReferentielBLL ligneref2 = new ReferentielBLL();
                                    ligneref2.Type = dataGridView_saisie.CurrentRow.Cells[14].Value.ToString();
                                    ligneref2.Cpl = dataGridView_saisie.CurrentRow.Cells[16].Value.ToString();
                                    ligneref2.Cpl1 = ligne_exam_nouveau_code.Nouveau_Code;
                                    ligneref2.Cpl2 = dataGridView_saisie.CurrentRow.Cells[18].Value.ToString();
                                    ligneref2.Code = dataGridView_saisie.CurrentRow.Cells[4].Value.ToString();
                                    ligneref2.Lib = dataGridView_saisie.CurrentRow.Cells[5].Value.ToString();
                                    ligneref12.Add(ligneref2);
                                    RefObjectDAL.DeleteFromSQLITE_TBReferentiel(ligneref12);
                                    VariablePartage.TableReferentiel = RefObjectBLL.SuppressionItem_RetourneListeReferentiel(VariablePartage.TableReferentiel, ligneref2);
                                    VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);
                                    GUI.InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                                    GUI.InitColorReferentiel(dataGridView_ref);
                                }

                                dataGridView_saisie.CurrentRow.Cells[4].Value = textBox_codeacreer;
                                dataGridView_saisie.CurrentRow.Cells[5].Value = textBox_libellecodeacreer;
                                dataGridView_saisie.CurrentRow.Cells[12].Value = !CodeActif;
                                dataGridView_saisie.CurrentRow.Cells[20].Value = 2;

                                ligne.Ancien_Code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString();
                                ligne.Nouveau_Code = dataGridView_saisie.CurrentRow.Cells[4].Value.ToString();
                                ligne.Libelle_Nouveau_Code = dataGridView_saisie.CurrentRow.Cells[5].Value.ToString();
                                ligne.NomRef = dataGridView_saisie.CurrentRow.Cells[15].Value.ToString();
                                ligne.FlagReferentiel = 2;
                                ligne.NouveauCodeInactif = !CodeActif;
                                if (CodeActif == true)
                                {
                                    ligne.CreerActif = true;
                                }
                                else
                                {
                                    ligne.CreerInactif = true;
                                    ligne.NouveauCodeInactif = true;
                                }
                                ligne.NePasReprendre = false;

                                List<ReferentielBLL> ligneref = new List<ReferentielBLL>();
                                ReferentielBLL ligneref1 = new ReferentielBLL();
                                ligneref1.Code = ligne.Nouveau_Code;
                                ligneref1.Lib = ligne.Libelle_Nouveau_Code;
                                ligneref1.InActif = !CodeActif;
                                ligneref1.Type = dataGridView_saisie.CurrentRow.Cells[14].Value.ToString();
                                ligneref1.TypeItem = dataGridView_saisie.CurrentRow.Cells[15].Value.ToString();
                                ligneref1.Cpl = dataGridView_saisie.CurrentRow.Cells[16].Value.ToString();

                                /*if (dataGridView_saisie.CurrentRow.Cells[17].Value == null)
                                {
                                    dataGridView_saisie.CurrentRow.Cells[17].Value = "";
                                }
                                ligneref1.Cpl1 = dataGridView_saisie.CurrentRow.Cells[17].Value.ToString();*/
                                ligneref1.Cpl1 = ligne_exam_nouveau_code.Nouveau_Code;
                                if (dataGridView_saisie.CurrentRow.Cells[18].Value == null)
                                {
                                    dataGridView_saisie.CurrentRow.Cells[18].Value = "";
                                }
                                ligneref1.Cpl2 = dataGridView_saisie.CurrentRow.Cells[18].Value.ToString();
                                ligneref1.FlagPreventiel = 2;
                                ligneref.Add(ligneref1);

                                RefObjectDAL.InsertIntoSQLITE_TBReferentiel(ligneref);

                                VariablePartage.TableReferentiel.Add(ligneref1);
                                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);

                                var source_ref = new BindingSource();
                                source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                                dataGridView_ref.DataSource = source_ref;
                                GUI.InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                                GUI.InitColorReferentiel(dataGridView_ref);

                                CorrObjectDAL.UpdateSQLITE_TBCorrespondance(ligne);
                                CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(ligne);

                                VariablePartage.TableCorrespondance = CorrObjectBLL.RetourneListeCorrespondanceUpdater(VariablePartage.TableCorrespondance, ligne, ligne.NomRef);
                            }

                        }
                        else
                        {




                            dataGridView_saisie.CurrentRow.Cells[4].Value = textBox_codeacreer;
                            dataGridView_saisie.CurrentRow.Cells[5].Value = textBox_libellecodeacreer;
                            dataGridView_saisie.CurrentRow.Cells[12].Value = !CodeActif;
                            dataGridView_saisie.CurrentRow.Cells[20].Value = 2;

                            List<ReferentielBLL> ligneref = new List<ReferentielBLL>();
                            ReferentielBLL ligneref1 = new ReferentielBLL();
                            ligneref1.Code = ligne.Nouveau_Code;
                            ligneref1.Lib = ligne.Libelle_Nouveau_Code;
                            ligneref1.InActif = !CodeActif;
                            ligneref1.Type = dataGridView_saisie.CurrentRow.Cells[14].Value.ToString();
                            ligneref1.TypeItem = dataGridView_saisie.CurrentRow.Cells[15].Value.ToString();
                            ligneref1.Cpl = dataGridView_saisie.CurrentRow.Cells[16].Value.ToString();
                            //Cas particulier du rapprochementEntEtab
                            if (ligneref1.Cpl == "1000")
                            {
                                ligneref1.Cpl = "3";
                            }
                            if (dataGridView_saisie.CurrentRow.Cells[17].Value == null)
                            {
                                dataGridView_saisie.CurrentRow.Cells[17].Value = "";
                            }
                            ligneref1.Cpl1 = dataGridView_saisie.CurrentRow.Cells[17].Value.ToString();

                            if (ligne.NomRef == "PersonnelMedical")
                            {
                                ligneref1.Cpl1 = "2";
                            }

                            if (dataGridView_saisie.CurrentRow.Cells[18].Value == null)
                            {
                                dataGridView_saisie.CurrentRow.Cells[18].Value = "";
                            }
                            ligneref1.Cpl2 = dataGridView_saisie.CurrentRow.Cells[18].Value.ToString();
                            ligneref1.FlagPreventiel = 2;
                            ligneref.Add(ligneref1);

                            RefObjectDAL.InsertIntoSQLITE_TBReferentiel(ligneref);

                            VariablePartage.TableReferentiel.Add(ligneref1);
                            VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);

                            var source_ref = new BindingSource();
                            source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                            dataGridView_ref.DataSource = source_ref;
                            GUI.InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                            GUI.InitColorReferentiel(dataGridView_ref);

                            CorrObjectDAL.UpdateSQLITE_TBCorrespondance(ligne);
                            CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(ligne);

                            VariablePartage.TableCorrespondance = CorrObjectBLL.RetourneListeCorrespondanceUpdater(VariablePartage.TableCorrespondance, ligne, dataGridView_saisie.CurrentRow.Cells[15].Value.ToString());
                            ligne.Cpl = dataGridView_saisie.CurrentRow.Cells[16].Value.ToString();
                            //Cas particulier des etablissements, on lance la fenetre de rattachement aux entreprises
                            if (ligne.Cpl == "1")
                            {
                                RattachementEtablissementEntreprise rattachementEtablissementEntreprise = new RattachementEtablissementEntreprise();
                                rattachementEtablissementEntreprise.form1 = myform;
                                rattachementEtablissementEntreprise.ShowDialog();
                            }


                        }

                        
                        //myform.button_affecter.Enabled = true;
                        //myform.button_afficherCreationCode.Enabled = true;
                        //myform.button_creercode.Enabled = false;
                        //myform.textBox_codeacreer.Text = "";
                        //myform.textBox_libellecodeacreer.Text = "";
                        //myform.textBox_codeacreer.Enabled = false;
                        //myform.textBox_libellecodeacreer.Enabled = false;
                        //myform.checkBox_codeacreer_actif_inactif.Enabled = false;
                        //myform.button_afficherCreationCode.Text = "Création code";
                        GUI.ReactiveButton(myform);
                    }
                }
            }
        }

        

        //public void CreerCodeInactifMemeCodeMemeLibelle_ByMenuItem(Object sender, System.EventArgs e, DataGridView dataGridView_saisie, DataGridView dataGridView_ref, Form1 myform)
        //{
        //    CreerCodeInactifMemeCodeMemeLibelle(dataGridView_saisie, dataGridView_ref, myform);
        //}
    }
}
