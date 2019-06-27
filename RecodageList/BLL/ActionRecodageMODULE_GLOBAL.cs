using RecodageList.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RecodageList.BLL;
using System.Windows.Forms;
using RecodageList.GUI;

namespace RecodageList.BLL
{
    class ActionRecodageMODULE_GLOBAL
    {
        GUIFonction GUI = new GUIFonction();
        Fonc fonc = new Fonc();
        CorrespondanceBLL CorrObjectBLL = new CorrespondanceBLL();
        CorrespondanceDAL CorrObjectDAL = new CorrespondanceDAL();
        ReferentielBLL RefObjectBLL = new ReferentielBLL();
        ReferentielDAL RefObjectDAL = new ReferentielDAL();

        public void RapprochementModule(Form1 myform, Chargement myformchargement)
        {
            if (VariablePartage.BaseCharge)
            {
                myformchargement.progressBar_chargement.Maximum = VariablePartage.TableCorrespondanceFiltre.Count - 1;
                //myform.progressBar_rapprochement.Maximum = VariablePartage.TableCorrespondanceFiltre.Count - 1;
                //GUI.InactiveControlExceptParamButton(myform.button_acces_admin, myform);
                Thread rapprochement = new Thread(new ParameterizedThreadStart(RapprochementModuleThread));

                string p1 = (string)myform.comboBox_filtre.SelectedValue;
                Form1 p2 = myform;
                Chargement p3 = myformchargement;
                object args = new object[3] { p1, p2, p3};
                VariablePartage.ThreadStop = false;
                rapprochement.Start(args);
            }
        }


        public void RapprochementModuleThread(object args)
        {
            Array argArray = new object[3];
            argArray = (Array)args;
            string clefValeur = (string)argArray.GetValue(0);
            Form1 myform = (Form1)argArray.GetValue(1);
            Chargement myformchargement = (Chargement)argArray.GetValue(2);

            int nbItemRapprochés = 0;

            CorrespondanceDAL ObjCorr = new CorrespondanceDAL();

            if ((string)clefValeur == "135|0|0|NOMEN")
            {
                int i = 0;
                while (i < VariablePartage.TableCorrespondanceFiltre.Count && VariablePartage.ThreadStop == false)
                {
                    if ((VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code == "" || VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code == null) && VariablePartage.TableCorrespondanceFiltre[i].FlagReferentiel == 0)
                    {
                        int j = 0;
                        while (j < VariablePartage.TableReferentielFiltre.Count && VariablePartage.ThreadStop == false)
                        {
                            if (VariablePartage.TableCorrespondanceFiltre[i].Canonical.Trim() == VariablePartage.TableReferentielFiltre[j].Canonical.Trim()
                                && VariablePartage.TableCorrespondanceFiltre[i].Cpl == VariablePartage.TableReferentielFiltre[j].Cpl
                                && VariablePartage.TableCorrespondanceFiltre[i].Cpl1 == VariablePartage.TableReferentielFiltre[j].Cpl1
                                && VariablePartage.TableCorrespondanceFiltre[i].Cpl2 == VariablePartage.TableReferentielFiltre[j].Cpl2
                                && VariablePartage.TableCorrespondanceFiltre[i].TypeRef == VariablePartage.TableReferentielFiltre[j].Type
                                && VariablePartage.TableCorrespondanceFiltre[i].Cpl == "135"
                                && VariablePartage.TableReferentielFiltre[j].Cpl == "135"
                                && VariablePartage.TableCorrespondanceFiltre[i].TypeRef == "NOMEN"
                                && VariablePartage.TableReferentielFiltre[j].Type == "NOMEN")
                            {
                                VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code = VariablePartage.TableReferentielFiltre[j].Code;
                                VariablePartage.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code = VariablePartage.TableReferentielFiltre[j].Lib;
                                if (VariablePartage.TableReferentielFiltre[j].InActif == true)
                                {
                                    VariablePartage.TableCorrespondanceFiltre[i].NouveauCodeInactif = true;
                                }
                                else
                                {
                                    VariablePartage.TableCorrespondanceFiltre[i].NouveauCodeInactif = false;
                                }
                                VariablePartage.TableCorrespondanceFiltre[i].FlagReferentiel = 1;
                                CorrObjectDAL.UpdateSQLITE_TBCorrespondance(VariablePartage.TableCorrespondanceFiltre[i]);
                                CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(VariablePartage.TableCorrespondanceFiltre[i]);
                                nbItemRapprochés++;
                            }
                            else if (VariablePartage.TableCorrespondanceFiltre[i].Canonical.Trim() == VariablePartage.TableReferentielFiltre[j].Canonical.Trim()
                                && VariablePartage.TableCorrespondanceFiltre[i].Cpl == VariablePartage.TableReferentielFiltre[j].Cpl
                                && VariablePartage.TableCorrespondanceFiltre[i].TypeRef == VariablePartage.TableReferentielFiltre[j].Type
                                && VariablePartage.TableCorrespondanceFiltre[i].Cpl == "50"
                                && VariablePartage.TableReferentielFiltre[j].Cpl == "50"
                                && VariablePartage.TableCorrespondanceFiltre[i].TypeRef == "CTRL"
                                && VariablePartage.TableReferentielFiltre[j].Type == "CTRL"
                                && VariablePartage.TableCorrespondanceFiltre[i].Ancien_Code.ToString().Contains("#"))
                            {
                                string[] examen_ancien_code = VariablePartage.TableCorrespondanceFiltre[i].Ancien_Code.ToString().Split('#');
                                CorrespondanceBLL ligne_exam_nouveau_code = CorrObjectBLL.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, examen_ancien_code[0], "135|0|0|NOMEN");
                                List<ReferentielBLL> TableRefResultatExamen = new List<ReferentielBLL>();
                                TableRefResultatExamen = RefObjectBLL.FiltrerListeResultatExamenReferentielParExamen(VariablePartage.TableReferentiel, ligne_exam_nouveau_code.Nouveau_Code);
                                int k = 0;
                                while ( k < TableRefResultatExamen.Count && VariablePartage.ThreadStop == false)
                                {
                                    if (TableRefResultatExamen[k].Canonical.Trim() == VariablePartage.TableCorrespondanceFiltre[i].Canonical.Trim())
                                    {
                                        VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code = TableRefResultatExamen[k].Code;
                                        VariablePartage.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code = TableRefResultatExamen[k].Lib;
                                        if (TableRefResultatExamen[k].InActif == true)
                                        {
                                            VariablePartage.TableCorrespondanceFiltre[i].NouveauCodeInactif = true;
                                        }
                                        else
                                        {
                                            VariablePartage.TableCorrespondanceFiltre[i].NouveauCodeInactif = false;
                                        }
                                        VariablePartage.TableCorrespondanceFiltre[i].FlagReferentiel = 1;
                                        CorrObjectDAL.UpdateSQLITE_TBCorrespondance(VariablePartage.TableCorrespondanceFiltre[i]);
                                        CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(VariablePartage.TableCorrespondanceFiltre[i]);
                                        nbItemRapprochés++;
                                    }
                                    k++;
                                }
                            }
                            j++;
                        }
                    }
                    //UpdateProgress_rapprochement(i,myform);
                    UpdateProgress_rapprochement_chargement(i, myformchargement);
                    //Console.WriteLine("Avancement : " + i + "/" + Init.TableCorrespondanceFiltre.Count);
                    i++;
                }
            }
            else if ((string)clefValeur == "141|0|0|NOMEN")
            {
                int i = 0;
                while ( i < VariablePartage.TableCorrespondanceFiltre.Count && VariablePartage.ThreadStop == false)
                {
                    if ((VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code == "" || VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code == null) && VariablePartage.TableCorrespondanceFiltre[i].FlagReferentiel == 0)
                    {
                        int j = 0;
                        while ( j < VariablePartage.TableReferentielFiltre.Count && VariablePartage.ThreadStop == false)
                        {
                            if (VariablePartage.TableCorrespondanceFiltre[i].Canonical.Trim() == VariablePartage.TableReferentielFiltre[j].Canonical.Trim()
                                && VariablePartage.TableCorrespondanceFiltre[i].Cpl == VariablePartage.TableReferentielFiltre[j].Cpl
                                && VariablePartage.TableCorrespondanceFiltre[i].Cpl1 == VariablePartage.TableReferentielFiltre[j].Cpl1
                                && VariablePartage.TableCorrespondanceFiltre[i].Cpl2 == VariablePartage.TableReferentielFiltre[j].Cpl2
                                && VariablePartage.TableCorrespondanceFiltre[i].TypeRef == VariablePartage.TableReferentielFiltre[j].Type
                                && VariablePartage.TableCorrespondanceFiltre[i].Cpl == "141"
                                && VariablePartage.TableReferentielFiltre[j].Cpl == "141"
                                && VariablePartage.TableCorrespondanceFiltre[i].TypeRef == "NOMEN"
                                && VariablePartage.TableReferentielFiltre[j].Type == "NOMEN")
                            {
                                VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code = VariablePartage.TableReferentielFiltre[j].Code;
                                VariablePartage.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code = VariablePartage.TableReferentielFiltre[j].Lib;
                                if (VariablePartage.TableReferentielFiltre[j].InActif == true)
                                {
                                    VariablePartage.TableCorrespondanceFiltre[i].NouveauCodeInactif = true;
                                }
                                else
                                {
                                    VariablePartage.TableCorrespondanceFiltre[i].NouveauCodeInactif = false;
                                }
                                VariablePartage.TableCorrespondanceFiltre[i].FlagReferentiel = 1;
                                CorrObjectDAL.UpdateSQLITE_TBCorrespondance(VariablePartage.TableCorrespondanceFiltre[i]);
                                CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(VariablePartage.TableCorrespondanceFiltre[i]);
                                nbItemRapprochés++;
                            }
                            else if (VariablePartage.TableCorrespondanceFiltre[i].Canonical.Trim() == VariablePartage.TableReferentielFiltre[j].Canonical.Trim()
                                && VariablePartage.TableCorrespondanceFiltre[i].Cpl == VariablePartage.TableReferentielFiltre[j].Cpl
                                && VariablePartage.TableCorrespondanceFiltre[i].TypeRef == VariablePartage.TableReferentielFiltre[j].Type
                                && VariablePartage.TableCorrespondanceFiltre[i].Cpl == "60"
                                && VariablePartage.TableReferentielFiltre[j].Cpl == "60"
                                && VariablePartage.TableCorrespondanceFiltre[i].TypeRef == "CTRL"
                                && VariablePartage.TableReferentielFiltre[j].Type == "CTRL"
                                && VariablePartage.TableCorrespondanceFiltre[i].Ancien_Code.ToString().Contains("#"))
                            {
                                string[] vaccin_ancien_code = VariablePartage.TableCorrespondanceFiltre[i].Ancien_Code.ToString().Split('#');
                                CorrespondanceBLL ligne_vaccin_nouveau_code = CorrObjectBLL.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, vaccin_ancien_code[0], "141|0|0|NOMEN");
                                List<ReferentielBLL> TableProtocoleVaccinal = new List<ReferentielBLL>();
                                TableProtocoleVaccinal = RefObjectBLL.FiltrerListeResultatExamenReferentielParExamen(VariablePartage.TableReferentiel, ligne_vaccin_nouveau_code.Nouveau_Code);
                                int k = 0;
                                while ( k < TableProtocoleVaccinal.Count && VariablePartage.ThreadStop == false)
                                {
                                    if (TableProtocoleVaccinal[k].Canonical.Trim() == VariablePartage.TableCorrespondanceFiltre[i].Canonical.Trim())
                                    {
                                        VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code = TableProtocoleVaccinal[k].Code;
                                        VariablePartage.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code = TableProtocoleVaccinal[k].Lib;
                                        if (TableProtocoleVaccinal[k].InActif == true)
                                        {
                                            VariablePartage.TableCorrespondanceFiltre[i].NouveauCodeInactif = true;
                                        }
                                        else
                                        {
                                            VariablePartage.TableCorrespondanceFiltre[i].NouveauCodeInactif = false;
                                        }
                                        VariablePartage.TableCorrespondanceFiltre[i].FlagReferentiel = 1;
                                        CorrObjectDAL.UpdateSQLITE_TBCorrespondance(VariablePartage.TableCorrespondanceFiltre[i]);
                                        CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(VariablePartage.TableCorrespondanceFiltre[i]);
                                        nbItemRapprochés++;
                                    }
                                    k++;
                                }
                            }
                            j++;
                        }
                    }
                    //UpdateProgress_rapprochement(i, myform);
                    UpdateProgress_rapprochement_chargement(i, myformchargement);
                    //Console.WriteLine("Avancement : " + i + "/" + Init.TableCorrespondanceFiltre.Count);
                    i++;
                }
            }
            else
            {
                int i = 0;
                while ( i < VariablePartage.TableCorrespondanceFiltre.Count && VariablePartage.ThreadStop == false)
                {
                    if ((VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code == "" || VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code == null) && VariablePartage.TableCorrespondanceFiltre[i].FlagReferentiel == 0)
                    {
                        int j = 0;
                        while ( j < VariablePartage.TableReferentielFiltre.Count && VariablePartage.ThreadStop == false)
                        {
                            if (VariablePartage.TableCorrespondanceFiltre[i].Canonical != null && VariablePartage.TableReferentielFiltre[j].Canonical != null
                                && VariablePartage.TableCorrespondanceFiltre[i].Canonical.Trim() == VariablePartage.TableReferentielFiltre[j].Canonical.Trim()
                                && VariablePartage.TableCorrespondanceFiltre[i].Cpl == VariablePartage.TableReferentielFiltre[j].Cpl
                                && VariablePartage.TableCorrespondanceFiltre[i].Cpl1 == VariablePartage.TableReferentielFiltre[j].Cpl1
                                && VariablePartage.TableCorrespondanceFiltre[i].Cpl2 == VariablePartage.TableReferentielFiltre[j].Cpl2
                                && VariablePartage.TableCorrespondanceFiltre[i].TypeRef == VariablePartage.TableReferentielFiltre[j].Type)
                            {
                                VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code = VariablePartage.TableReferentielFiltre[j].Code;
                                VariablePartage.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code = VariablePartage.TableReferentielFiltre[j].Lib;
                                if (VariablePartage.TableReferentielFiltre[j].InActif == true)
                                {
                                    VariablePartage.TableCorrespondanceFiltre[i].NouveauCodeInactif = true;
                                }
                                else
                                {
                                    VariablePartage.TableCorrespondanceFiltre[i].NouveauCodeInactif = false;
                                }
                                VariablePartage.TableCorrespondanceFiltre[i].FlagReferentiel = 1;
                                CorrObjectDAL.UpdateSQLITE_TBCorrespondance(VariablePartage.TableCorrespondanceFiltre[i]);
                                CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(VariablePartage.TableCorrespondanceFiltre[i]);
                                nbItemRapprochés++;
                            }
                            j++;
                        }
                    }
                    //UpdateProgress_rapprochement(i,myform);
                    UpdateProgress_rapprochement_chargement(i, myformchargement);
                    //Console.WriteLine("Avancement : " + i + "/" + Init.TableCorrespondanceFiltre.Count);
                    i++;
                }
            }
            UpdateProgress_rapprochement_module_terminate_chargement(nbItemRapprochés,myform,myformchargement);
        }


        /// <summary>
        /// Ce delegate me permet de communiquer une valeur qui représente l'avancement du Thread.
        /// Cette valeur sera utilisée plus tard pour faire avancer une ProgressBar.
        /// </summary>
        /// <param name="valeur"></param>
        public delegate void MontrerProgres(int valeur,Form1 myform);

        /// <summary>
        /// Ce delegate me permet de communiquer une valeur qui représente l'avancement du Thread.
        /// Cette valeur sera utilisée plus tard pour faire avancer une ProgressBar.
        /// </summary>
        /// <param name="valeur"></param>
        public delegate void MontrerProgres_Chargement(int valeur, Chargement myformchargement);


        /// <summary>
        /// Permet d'appeler la méthode Progres_rapprochement() via le delegate MontrerProgres().
        /// Ce qui revient à mettre à jour la valeur 
        /// d'avancement de la ProgressBar 'progressBar_rapprochement'
        /// </summary>
        /// <param name="value"></param>
        private void UpdateProgress_rapprochement_chargement(int value,Chargement myformchargement)
        {
            try
            {
                //On invoque le delegate pour qu'il effectue la tâche sur le temps
                //de l'autre thread.
                myformchargement.Invoke((MontrerProgres_Chargement)Progres_rapprochement_chargement, value, myformchargement);
            }
            catch (Exception ex) { return; }
        }

        /// <summary>
        /// Permet d'appeler la méthode Progres_rapprochement() via le delegate MontrerProgres().
        /// Ce qui revient à mettre à jour la valeur 
        /// d'avancement de la ProgressBar 'progressBar_rapprochement'
        /// </summary>
        /// <param name="value"></param>
        private void UpdateProgress_rapprochement(int value, Chargement myformchargement)
        {
            try
            {
                //On invoque le delegate pour qu'il effectue la tâche sur le temps
                //de l'autre thread.
                myformchargement.Invoke((MontrerProgres_Chargement)Progres_rapprochement, value, myformchargement);
            }
            catch (Exception ex) { return; }
        }

        /// <summary>
        /// Met à jour la valeur d'avancement de la ProgressBar 'progressBar_rapprochement'
        /// </summary>
        /// <param name="valeur"></param>
        public void Progres_rapprochement(int valeur, Chargement myformchargement)
        {
            //On met la valeur dans le contrôle Windows Forms.
            myformchargement.progressBar_chargement.Value = valeur;
        }
        public void Progres_rapprochement_chargement(int valeur, Chargement myformchargement)
        {
            //On met la valeur dans le contrôle Windows Forms.
            myformchargement.progressBar_chargement.Value = valeur;
        }


        /// <summary>
        /// Ce delegate permet au thread d'indiquer qu'il a fini son job.
        /// </summary>
        /// <param name="terminate"></param>
        public delegate void MontrerProgresTerminate(bool terminate);

        /// <summary>
        /// Ce delegate me permet de communiquer le nombre d'item traité par le thread.
        /// </summary>
        /// <param name="nbItemRapprochés"></param>
        public delegate void MontrerProgresTerminate_rapprochement(int nbItemRapprochés, Form1 myform, Chargement myformchargement);

        public delegate void MontrerProgresTerminate_rapprochement_chargement(int nbItemRapprochés, Form1 myform, Chargement myformchargement);

        /// <summary>
        /// Permet d'appeler la méthode Progres_rapprochement_terminate() 
        /// via le delegate MontrerProgresTerminate_rapprochement()
        /// </summary>
        /// <param name="nbItemRapprochés"></param>
        private void UpdateProgress_rapprochement_terminate(int nbItemRapprochés, Form1 myform, Chargement myformchargement)
        {
            try
            {
                //On invoque le delegate pour qu'il effectue la tâche sur le temps
                //de l'autre thread.
                myform.Invoke((MontrerProgresTerminate_rapprochement)Progres_rapprochement_terminate, nbItemRapprochés,myform, myformchargement);
            }
            catch (Exception ex) { return; }
        }

        private void UpdateProgress_rapprochement_terminate_chargement(int nbItemRapprochés, Form1 myform, Chargement myformchargement)
        {
            try
            {
                //On invoque le delegate pour qu'il effectue la tâche sur le temps
                //de l'autre thread.
                myform.Invoke((MontrerProgresTerminate_rapprochement_chargement)Progres_rapprochement_terminate_chargement, nbItemRapprochés, myform, myformchargement);
            }
            catch (Exception ex) { return; }
        }

        /// <summary>
        /// Permet d'indiquer : 
        /// - Le nombre d'item traités via un MessageBox
        /// - Recharge l'interface graphique pour afficher les nouveaux codes traités
        /// - Reactive les boutons qui étaient inactifs pendant le chargement du thread
        /// - Réinitialise la progressbar 'progressBar_rapprochement' à 0.
        /// </summary>
        /// <param name="nbItemRapprochés"></param>
        public void Progres_rapprochement_terminate(int nbItemRapprochés, Form1 myform, Chargement myformchargement)
        {
            //On met la valeur dans le contrôle Windows Forms.
            if (myform.checkBox_nontraite.Checked)
            {
                List<CorrespondanceBLL> CorrespNonTraite = new List<CorrespondanceBLL>();
                CorrespNonTraite = CorrObjectBLL.RetourneSeulementNonTraite(VariablePartage.TableCorrespondanceFiltre);
                var source_ref = new BindingSource();
                source_ref.DataSource = CorrespNonTraite;
                myform.dataGridView_saisie.DataSource = source_ref;
            }
            else
            {
                var source = new BindingSource();
                source.DataSource = VariablePartage.TableCorrespondanceFiltre;
                myform.dataGridView_saisie.DataSource = source;
            }
            GUI.InitStructSaisie(myform.dataGridView_saisie,myform);
            GUI.InitColorSaisie(myform.dataGridView_saisie);
            myformchargement.Close();
            MessageBox.Show("Traitement automatique terminé. \r\n" + nbItemRapprochés.ToString() + " code(s) traité(s) automatiquement.");
            GUI.ReactiveButton(myform);
            myformchargement.progressBar_chargement.Value = 0;
        }

        public void Progres_rapprochement_terminate_chargement(int nbItemRapprochés, Form1 myform, Chargement myformchargement)
        {
            //On met la valeur dans le contrôle Windows Forms.
            if (myform.checkBox_nontraite.Checked)
            {
                List<CorrespondanceBLL> CorrespNonTraite = new List<CorrespondanceBLL>();
                CorrespNonTraite = CorrObjectBLL.RetourneSeulementNonTraite(VariablePartage.TableCorrespondanceFiltre);
                var source_ref = new BindingSource();
                source_ref.DataSource = CorrespNonTraite;
                myform.dataGridView_saisie.DataSource = source_ref;
            }
            else
            {
                var source = new BindingSource();
                source.DataSource = VariablePartage.TableCorrespondanceFiltre;
                myform.dataGridView_saisie.DataSource = source;
            }
            GUI.InitStructSaisie(myform.dataGridView_saisie, myform);
            GUI.InitColorSaisie(myform.dataGridView_saisie);
            myformchargement.Close();
            MessageBox.Show("Traitement automatique terminé. \r\n" + nbItemRapprochés.ToString() + " code(s) traité(s) automatiquement.");
            GUI.ReactiveButton(myform);
            myformchargement.progressBar_chargement.Value = 0;
        }

        /// <summary>
        /// Permet d'appeler la méthode Progres_rapprochement_module_terminate()
        /// via le delegate MontrerProgresTerminate_rapprochement()
        /// </summary>
        /// <param name="nbItemRapprochés"></param>
        private void UpdateProgress_rapprochement_module_terminate(int nbItemRapprochés,Form1 myform, Chargement myformchargement)
        {
            try
            {
                //On invoque le delegate pour qu'il effectue la tâche sur le temps
                //de l'autre thread.
                myform.Invoke((MontrerProgresTerminate_rapprochement)Progres_rapprochement_module_terminate, nbItemRapprochés, myform, myformchargement);
            }
            catch (Exception ex) { return; }
        }

        private void UpdateProgress_rapprochement_module_terminate_chargement(int nbItemRapprochés, Form1 myform, Chargement myformchargement)
        {
            try
            {
                //On invoque le delegate pour qu'il effectue la tâche sur le temps
                //de l'autre thread.
                myformchargement.Invoke((MontrerProgresTerminate_rapprochement_chargement)Progres_rapprochement_module_terminate_chargement, nbItemRapprochés, myform, myformchargement);
            }
            catch (Exception ex) { return; }
        }

        /// <summary>
        /// Permet d'indiquer : 
        /// - Le nombre d'item traités via un MessageBox
        /// - Recharge l'interface graphique pour afficher les nouveaux codes traités
        /// - Reactive les boutons qui étaient inactifs pendant le chargement du thread
        /// - Réinitialise la progressbar 'progressBar_rapprochement' à 0.
        /// </summary>
        /// <param name="nbItemRapprochés"></param>
        public void Progres_rapprochement_module_terminate(int nbItemRapprochés, Form1 myform, Chargement myformchargement)
        {
            //On met la valeur dans le contrôle Windows Forms.
            if (myform.checkBox_nontraite.Checked)
            {
                List<CorrespondanceBLL> CorrespNonTraite = new List<CorrespondanceBLL>();
                CorrespNonTraite = CorrObjectBLL.RetourneSeulementNonTraite(VariablePartage.TableCorrespondanceFiltre);
                var source_ref = new BindingSource();
                source_ref.DataSource = CorrespNonTraite;
                myform.dataGridView_saisie.DataSource = source_ref;
            }
            else
            {
                var source = new BindingSource();
                source.DataSource = VariablePartage.TableCorrespondanceFiltre;
                myform.dataGridView_saisie.DataSource = source;
            }
            VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);
            var source_referentiel = new BindingSource();
            source_referentiel.DataSource = VariablePartage.TableReferentielFiltre;
            myform.dataGridView_ref.DataSource = source_referentiel;
            GUI.InitStructSaisie(myform.dataGridView_saisie, myform);
            GUI.InitColorSaisie(myform.dataGridView_saisie);
            myformchargement.Close();
            MessageBox.Show("Traitement automatique terminé. \r\n" + nbItemRapprochés.ToString() + " code(s) traité(s) automatiquement.");
            myformchargement.progressBar_chargement.Value = 0;
            GUI.ReactiveButton(myform);
            //myform.button_deleteRecodage.Enabled = true;
            //myform.button_afficherCreationCode.Enabled = true;
            //myform.button_affecter.Enabled = true;
        }

        public void Progres_rapprochement_module_terminate_chargement(int nbItemRapprochés, Form1 myform, Chargement myformchargement)
        {
            //On met la valeur dans le contrôle Windows Forms.
            if (myform.checkBox_nontraite.Checked)
            {
                List<CorrespondanceBLL> CorrespNonTraite = new List<CorrespondanceBLL>();
                CorrespNonTraite = CorrObjectBLL.RetourneSeulementNonTraite(VariablePartage.TableCorrespondanceFiltre);
                var source_ref = new BindingSource();
                source_ref.DataSource = CorrespNonTraite;
                myform.dataGridView_saisie.DataSource = source_ref;
            }
            else
            {
                var source = new BindingSource();
                source.DataSource = VariablePartage.TableCorrespondanceFiltre;
                myform.dataGridView_saisie.DataSource = source;
            }
            VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);
            var source_referentiel = new BindingSource();
            source_referentiel.DataSource = VariablePartage.TableReferentielFiltre;
            myform.dataGridView_ref.DataSource = source_referentiel;
            GUI.InitStructSaisie(myform.dataGridView_saisie, myform);
            GUI.InitColorSaisie(myform.dataGridView_saisie);
            myformchargement.Close();
            MessageBox.Show("Traitement automatique terminé. \r\n" + nbItemRapprochés.ToString() + " code(s) traité(s) automatiquement.");
            myformchargement.progressBar_chargement.Value = 0;
            GUI.ReactiveButton(myform);
            //myform.button_deleteRecodage.Enabled = true;
            //myform.button_afficherCreationCode.Enabled = true;
            //myform.button_affecter.Enabled = true;
        }

        /// <summary>
        /// Ce delegate me permet de mettre à jour le texte d'un bouton.
        /// </summary>
        /// <param name="button"></param>
        /// <param name="text"></param>
        public delegate void UpdateButtonText(Button button, string text);

        /// <summary>
        /// Il s'agit de la méthode associée au delegate précedent : UpdateButtonText()
        /// Cette méthode permet simplement d'utiliser le delegate.
        /// Vu que le delegate possède les mêmes arguments que la méthode UpdateButtonText_fonc()
        /// nous pouvons "invoke" cette méthode et donc finalement mettre à jour le texte d'un bouton
        /// depuis un thread.
        /// </summary>
        /// <param name="button"></param>
        /// <param name="text"></param>
        private void UpdateButtonText_foncdelegate(Button button, string text,Form1 myform)
        {
            try
            {
                myform.Invoke((UpdateButtonText)UpdateButtonText_fonc, button, text);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }

        /// <summary>
        /// Met simplement à jour le Text d'un bouton.
        /// </summary>
        /// <param name="button"></param>
        /// <param name="text"></param>
        public void UpdateButtonText_fonc(Button button, string text)
        {
            button.Text = text;
        }



        public void RapprochementGlobal(Form1 myform, Chargement myformchargement)
        {
            if (VariablePartage.BaseCharge)
            {
                myformchargement.progressBar_chargement.Maximum = VariablePartage.TableCorrespondance.Count - 1;
                //GUI.InactiveControlExceptParamButton(myform.button_acces_admin, myform);
                //GUI.InactiveAllTextbox(myform);
                Thread rapprochement = new Thread(new ParameterizedThreadStart(RapprochementGlobalThread));

                string p1 = (string)myform.comboBox_filtre.SelectedValue;
                Form1 p2 = myform;
                Chargement p3 = myformchargement;
                object args = new object[3] { p1, p2, p3 };
                VariablePartage.ThreadStop = false;
                rapprochement.Start(args);
            }
        }

        public void RapprochementGlobalThread(object args)
        {
            Array argArray = new object[3];
            argArray = (Array)args;
            string clefValeur = (string)argArray.GetValue(0);
            Form1 myform = (Form1)argArray.GetValue(1);
            Chargement myformchargement = (Chargement)argArray.GetValue(2);

            VariablePartage.ThreadStop = false;
            //UpdateButtonText_foncdelegate(myform.button_rapprochement_global, "Annuler", myform);
            int nbItemRapprochés = 0;

            CorrespondanceDAL ObjCorr = new CorrespondanceDAL();

            if ((string)clefValeur == "135|0|0|NOMEN" && VariablePartage.ThreadStop == false)
            {
                int i = 0;
                while (i < VariablePartage.TableCorrespondance.Count && VariablePartage.ThreadStop == false)
                {
                    if ((VariablePartage.TableCorrespondance[i].Nouveau_Code == "" || VariablePartage.TableCorrespondance[i].Nouveau_Code == null) && VariablePartage.TableCorrespondance[i].FlagReferentiel == 0 && VariablePartage.ThreadStop == false)
                    {
                        int j = 0;
                        while (j < VariablePartage.TableReferentiel.Count && VariablePartage.ThreadStop == false)
                        {
                            if (VariablePartage.ThreadStop == false && VariablePartage.TableCorrespondance[i].Canonical != null && VariablePartage.TableCorrespondance[i].Canonical.Trim() != "" && VariablePartage.TableReferentiel[j].Canonical.Trim() != ""
                                && VariablePartage.TableCorrespondance[i].Canonical.Trim() == VariablePartage.TableReferentiel[j].Canonical.Trim()
                                && VariablePartage.TableCorrespondance[i].Cpl == VariablePartage.TableReferentiel[j].Cpl
                                && VariablePartage.TableCorrespondance[i].Cpl1 == VariablePartage.TableReferentiel[j].Cpl1
                                && VariablePartage.TableCorrespondance[i].Cpl2 == VariablePartage.TableReferentiel[j].Cpl2
                                && VariablePartage.TableCorrespondance[i].TypeRef == VariablePartage.TableReferentiel[j].Type
                                && VariablePartage.TableCorrespondance[i].Cpl == "135"
                                && VariablePartage.TableReferentiel[j].Cpl == "135"
                                && VariablePartage.TableCorrespondance[i].TypeRef == "NOMEN"
                                && VariablePartage.TableReferentiel[j].Type == "NOMEN")
                            {
                                VariablePartage.TableCorrespondance[i].Nouveau_Code = VariablePartage.TableReferentiel[j].Code;
                                VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code = VariablePartage.TableReferentiel[j].Lib;
                                if (VariablePartage.ThreadStop == false && VariablePartage.TableReferentiel[j].InActif == true)
                                {
                                    VariablePartage.TableCorrespondance[i].NouveauCodeInactif = true;
                                }
                                else
                                {
                                    VariablePartage.TableCorrespondance[i].NouveauCodeInactif = false;
                                }
                                VariablePartage.TableCorrespondance[i].FlagReferentiel = 1;
                                CorrObjectDAL.UpdateSQLITE_TBCorrespondance(VariablePartage.TableCorrespondance[i]);
                                CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(VariablePartage.TableCorrespondance[i]);
                                nbItemRapprochés++;
                            }
                            else if (VariablePartage.ThreadStop == false && VariablePartage.TableCorrespondance[i].Canonical != null && VariablePartage.TableCorrespondance[i].Canonical.Trim() != "" && VariablePartage.TableReferentiel[j].Canonical.Trim() != ""
                                && VariablePartage.TableCorrespondance[i].Canonical.Trim() == VariablePartage.TableReferentiel[j].Canonical.Trim()
                                && VariablePartage.TableCorrespondance[i].Cpl == VariablePartage.TableReferentiel[j].Cpl
                                && VariablePartage.TableCorrespondance[i].TypeRef == VariablePartage.TableReferentiel[j].Type
                                && VariablePartage.TableCorrespondance[i].Cpl == "50"
                                && VariablePartage.TableReferentiel[j].Cpl == "50"
                                && VariablePartage.TableCorrespondance[i].TypeRef == "CTRL"
                                && VariablePartage.TableReferentiel[j].Type == "CTRL"
                                && VariablePartage.TableCorrespondance[i].Ancien_Code.ToString().Contains("#"))
                            {
                                string[] examen_ancien_code = VariablePartage.TableCorrespondance[i].Ancien_Code.ToString().Split('#');
                                CorrespondanceBLL ligne_exam_nouveau_code = CorrObjectBLL.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondance, examen_ancien_code[0], "135|0|0|NOMEN");
                                List<ReferentielBLL> TableRefResultatExamen = new List<ReferentielBLL>();
                                TableRefResultatExamen = RefObjectBLL.FiltrerListeResultatExamenReferentielParExamen(VariablePartage.TableReferentiel, ligne_exam_nouveau_code.Nouveau_Code);
                                int k = 0;
                                while (k < TableRefResultatExamen.Count && VariablePartage.ThreadStop == false)
                                {
                                    if (VariablePartage.ThreadStop == false && VariablePartage.TableCorrespondance[i].Canonical.Trim() != "" && TableRefResultatExamen[k].Canonical != ""
                                && TableRefResultatExamen[k].Canonical == VariablePartage.TableCorrespondance[i].Canonical.Trim())
                                    {
                                        VariablePartage.TableCorrespondance[i].Nouveau_Code = TableRefResultatExamen[k].Code;
                                        VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code = TableRefResultatExamen[k].Lib;
                                        if (VariablePartage.ThreadStop == false && TableRefResultatExamen[k].InActif == true)
                                        {
                                            VariablePartage.TableCorrespondance[i].NouveauCodeInactif = true;
                                        }
                                        else
                                        {
                                            VariablePartage.TableCorrespondance[i].NouveauCodeInactif = false;
                                        }
                                        VariablePartage.TableCorrespondance[i].FlagReferentiel = 1;
                                        CorrObjectDAL.UpdateSQLITE_TBCorrespondance(VariablePartage.TableCorrespondance[i]);
                                        CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(VariablePartage.TableCorrespondance[i]);
                                        nbItemRapprochés++;
                                    }
                                    k++;
                                }
                            }
                            j++;
                        }
                    }
                    UpdateProgress_rapprochement_chargement(i, myformchargement);
                    //Console.WriteLine("Avancement : " + i + "/" + Init.TableCorrespondance.Count);
                    i++;
                }
            }
            else if (VariablePartage.ThreadStop == false && (string)clefValeur == "141|0|0|NOMEN")
            {
                int i = 0;
                while (i < VariablePartage.TableCorrespondance.Count && VariablePartage.ThreadStop == false)
                {
                    if (VariablePartage.ThreadStop == false && (VariablePartage.TableCorrespondance[i].Nouveau_Code == "" || VariablePartage.TableCorrespondance[i].Nouveau_Code == null) && VariablePartage.TableCorrespondance[i].FlagReferentiel == 0)
                    {
                        int j = 0;
                        while (j < VariablePartage.TableReferentiel.Count && VariablePartage.ThreadStop == false)
                        {
                            if (VariablePartage.ThreadStop == false && VariablePartage.TableCorrespondance[i].Canonical.Trim() != "" && VariablePartage.TableReferentiel[j].Canonical.Trim() != ""
                                && VariablePartage.TableCorrespondance[i].Canonical.Trim() == VariablePartage.TableReferentiel[j].Canonical.Trim()
                                && VariablePartage.TableCorrespondance[i].Cpl == VariablePartage.TableReferentiel[j].Cpl
                                && VariablePartage.TableCorrespondance[i].Cpl1 == VariablePartage.TableReferentiel[j].Cpl1
                                && VariablePartage.TableCorrespondance[i].Cpl2 == VariablePartage.TableReferentiel[j].Cpl2
                                && VariablePartage.TableCorrespondance[i].TypeRef == VariablePartage.TableReferentiel[j].Type
                                && VariablePartage.TableCorrespondance[i].Cpl == "141"
                                && VariablePartage.TableReferentiel[j].Cpl == "141"
                                && VariablePartage.TableCorrespondance[i].TypeRef == "NOMEN"
                                && VariablePartage.TableReferentiel[j].Type == "NOMEN")
                            {
                                VariablePartage.TableCorrespondance[i].Nouveau_Code = VariablePartage.TableReferentiel[j].Code;
                                VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code = VariablePartage.TableReferentiel[j].Lib;
                                if (VariablePartage.ThreadStop == false && VariablePartage.TableReferentiel[j].InActif == true)
                                {
                                    VariablePartage.TableCorrespondance[i].NouveauCodeInactif = true;
                                }
                                else
                                {
                                    VariablePartage.TableCorrespondance[i].NouveauCodeInactif = false;
                                }
                                VariablePartage.TableCorrespondance[i].FlagReferentiel = 1;
                                CorrObjectDAL.UpdateSQLITE_TBCorrespondance(VariablePartage.TableCorrespondance[i]);
                                CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(VariablePartage.TableCorrespondance[i]);
                                nbItemRapprochés++;
                            }
                            else if (VariablePartage.ThreadStop == false && VariablePartage.TableCorrespondance[i].Canonical.Trim() != "" && VariablePartage.TableReferentiel[j].Canonical.Trim() != ""
                                && VariablePartage.TableCorrespondance[i].Canonical.Trim() == VariablePartage.TableReferentiel[j].Canonical.Trim()
                                && VariablePartage.TableCorrespondance[i].Cpl == VariablePartage.TableReferentiel[j].Cpl
                                && VariablePartage.TableCorrespondance[i].TypeRef == VariablePartage.TableReferentiel[j].Type
                                && VariablePartage.TableCorrespondance[i].Cpl == "60"
                                && VariablePartage.TableReferentiel[j].Cpl == "60"
                                && VariablePartage.TableCorrespondance[i].TypeRef == "CTRL"
                                && VariablePartage.TableReferentiel[j].Type == "CTRL"
                                && VariablePartage.TableCorrespondance[i].Ancien_Code.ToString().Contains("#"))
                            {
                                string[] vaccin_ancien_code = VariablePartage.TableCorrespondance[i].Ancien_Code.ToString().Split('#');
                                CorrespondanceBLL ligne_vaccin_nouveau_code = CorrObjectBLL.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondance, vaccin_ancien_code[0], "141|0|0|NOMEN");
                                List<ReferentielBLL> TableProtocoleVaccinal = new List<ReferentielBLL>();
                                TableProtocoleVaccinal = RefObjectBLL.FiltrerListeResultatExamenReferentielParExamen(VariablePartage.TableReferentiel, ligne_vaccin_nouveau_code.Nouveau_Code);
                                int k = 0;
                                while (k < TableProtocoleVaccinal.Count && VariablePartage.ThreadStop == false)
                                {
                                    if (VariablePartage.ThreadStop == false && VariablePartage.TableCorrespondance[i].Canonical.Trim() != "" && TableProtocoleVaccinal[k].Canonical != ""
                                && TableProtocoleVaccinal[k].Canonical == VariablePartage.TableCorrespondance[i].Canonical.Trim())
                                    {
                                        VariablePartage.TableCorrespondance[i].Nouveau_Code = TableProtocoleVaccinal[k].Code;
                                        VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code = TableProtocoleVaccinal[k].Lib;
                                        if (VariablePartage.ThreadStop == false && TableProtocoleVaccinal[k].InActif == true)
                                        {
                                            VariablePartage.TableCorrespondance[i].NouveauCodeInactif = true;
                                        }
                                        else
                                        {
                                            VariablePartage.TableCorrespondance[i].NouveauCodeInactif = false;
                                        }
                                        VariablePartage.TableCorrespondance[i].FlagReferentiel = 1;
                                        CorrObjectDAL.UpdateSQLITE_TBCorrespondance(VariablePartage.TableCorrespondance[i]);
                                        CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(VariablePartage.TableCorrespondance[i]);
                                        nbItemRapprochés++;
                                    }
                                    k++;
                                }
                            }
                            j++;
                        }
                    }
                    UpdateProgress_rapprochement_chargement(i, myformchargement);
                    //Console.WriteLine("Avancement : " + i + "/" + Init.TableCorrespondance.Count);
                    i++;
                }
            }
            else
            {
                int i = 0;
                while (i < VariablePartage.TableCorrespondance.Count && VariablePartage.ThreadStop == false)
                {
                    if (VariablePartage.ThreadStop == false && (VariablePartage.TableCorrespondance[i].Nouveau_Code == "" || VariablePartage.TableCorrespondance[i].Nouveau_Code == null) && VariablePartage.TableCorrespondance[i].FlagReferentiel == 0)
                    {
                        int j = 0;
                        while (j < VariablePartage.TableReferentiel.Count && VariablePartage.ThreadStop == false)
                        {
                            if (VariablePartage.ThreadStop == false && VariablePartage.TableCorrespondance[i].Canonical != null && VariablePartage.TableCorrespondance[i].Canonical.Trim() != "" && VariablePartage.TableReferentiel[j].Canonical != null && VariablePartage.TableReferentiel[j].Canonical.Trim() != ""
                            && VariablePartage.TableCorrespondance[i].Canonical.Trim() == VariablePartage.TableReferentiel[j].Canonical.Trim()
                            && VariablePartage.TableCorrespondance[i].Cpl == VariablePartage.TableReferentiel[j].Cpl
                            && VariablePartage.TableCorrespondance[i].Cpl1 == VariablePartage.TableReferentiel[j].Cpl1
                            && VariablePartage.TableCorrespondance[i].Cpl2 == VariablePartage.TableReferentiel[j].Cpl2
                            && VariablePartage.TableCorrespondance[i].TypeRef == VariablePartage.TableReferentiel[j].Type)
                            {
                                VariablePartage.TableCorrespondance[i].Nouveau_Code = VariablePartage.TableReferentiel[j].Code;
                                VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code = VariablePartage.TableReferentiel[j].Lib;
                                if (VariablePartage.ThreadStop == false && VariablePartage.TableReferentiel[j].InActif == true)
                                {
                                    VariablePartage.TableCorrespondance[i].NouveauCodeInactif = true;
                                }
                                else
                                {
                                    VariablePartage.TableCorrespondance[i].NouveauCodeInactif = false;
                                }
                                VariablePartage.TableCorrespondance[i].FlagReferentiel = 1;
                                CorrObjectDAL.UpdateSQLITE_TBCorrespondance(VariablePartage.TableCorrespondance[i]);
                                CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(VariablePartage.TableCorrespondance[i]);
                                nbItemRapprochés++;
                            }
                            j++;
                        }
                    }
                    UpdateProgress_rapprochement_chargement(i, myformchargement);
                    //Console.WriteLine("Avancement : " + i + "/" + Init.TableCorrespondance.Count);
                    i++;
                }
            }

            UpdateProgress_rapprochement_terminate_chargement(nbItemRapprochés, myform, myformchargement);
            //UpdateButtonText_foncdelegate(myform.button_rapprochement_global, "Rapprochement automatique global", myform);


        }

        public void CreerCodeModule(Form1 myform, Chargement myformchargement, bool CodeActif)
        {
            if (VariablePartage.BaseCharge)
            {
                myformchargement.progressBar_chargement.Maximum = VariablePartage.TableCorrespondanceFiltre.Count;
                //GUI.InactiveControlExceptParamButton(myform.button_acces_admin, myform);
                Thread creercodeinactif_module = new Thread(new ParameterizedThreadStart(CreerCodeInactifModuleThread));
                string p1 = (string)myform.comboBox_filtre.SelectedValue;
                Form1 p2 = myform;
                Chargement p3 = myformchargement;
                bool p4 = CodeActif;
                object args = new object[4] { p1, p2, p3, p4 };
                VariablePartage.ThreadStop = false;
                creercodeinactif_module.Start(args);
            }
        }

        
        public void CreerCodeInactifModuleThread(object args)
        {
            Array argArray = new object[3];
            argArray = (Array)args;
            string clefValeur = (string)argArray.GetValue(0);
            Form1 myform = (Form1)argArray.GetValue(1);
            Chargement myformchargement = (Chargement)argArray.GetValue(2);
            bool CodeActif = (bool)argArray.GetValue(3);
            int nbItemRapprochés = 0;
            int i = 0;
            VariablePartage.ThreadStop = false;
            CorrespondanceDAL ObjCorr = new CorrespondanceDAL();
            while (i < VariablePartage.TableCorrespondanceFiltre.Count && VariablePartage.ThreadStop == false)
            {
                if ((VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code == null || VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code == "")
                    && VariablePartage.TableCorrespondanceFiltre[i].Libelle_Ancien_Code != null && VariablePartage.TableCorrespondanceFiltre[i].Libelle_Ancien_Code != ""
                    && VariablePartage.TableCorrespondanceFiltre[i].FlagReferentiel == 0
                    && CorrObjectBLL.NouveauCodeEstDansLeReferentiel(
                        VariablePartage.TableCorrespondanceFiltre[i].Ancien_Code,
                        VariablePartage.TableReferentiel,
                        VariablePartage.TableCorrespondanceFiltre[i].Cpl,
                        VariablePartage.TableCorrespondanceFiltre[i].Cpl1,
                        VariablePartage.TableCorrespondanceFiltre[i].Cpl2,
                        VariablePartage.TableCorrespondanceFiltre[i].TypeRef)==false)
                {
                    VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code = VariablePartage.TableCorrespondanceFiltre[i].Ancien_Code;
                    VariablePartage.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code = VariablePartage.TableCorrespondanceFiltre[i].Libelle_Ancien_Code;
                    VariablePartage.TableCorrespondanceFiltre[i].NouveauCodeInactif = !CodeActif;
                    VariablePartage.TableCorrespondanceFiltre[i].FlagReferentiel = 2;
                    CorrObjectDAL.UpdateSQLITE_TBCorrespondance(VariablePartage.TableCorrespondanceFiltre[i]);
                    CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(VariablePartage.TableCorrespondanceFiltre[i]);

                    List<ReferentielBLL> ligneref = new List<ReferentielBLL>();
                    ReferentielBLL ligneref1 = new ReferentielBLL();
                    ligneref1.Code = VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code;
                    ligneref1.Lib = VariablePartage.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code;
                    ligneref1.InActif = !CodeActif;
                    ligneref1.Type = VariablePartage.TableCorrespondanceFiltre[i].TypeRef;
                    ligneref1.Cpl = VariablePartage.TableCorrespondanceFiltre[i].Cpl;

                    if (VariablePartage.TableCorrespondanceFiltre[i].Cpl1 == null)
                    {
                        VariablePartage.TableCorrespondanceFiltre[i].Cpl1 = "";
                    }
                    ligneref1.Cpl1 = VariablePartage.TableCorrespondanceFiltre[i].Cpl1;

                    if (VariablePartage.TableCorrespondanceFiltre[i].Cpl2 == null)
                    {
                        VariablePartage.TableCorrespondanceFiltre[i].Cpl2 = "";
                    }
                    ligneref1.Cpl2 = VariablePartage.TableCorrespondanceFiltre[i].Cpl2;
                    ligneref1.FlagPreventiel = 2;
                    ligneref.Add(ligneref1);
                    RefObjectDAL.InsertIntoSQLITE_TBReferentiel(ligneref);
                    VariablePartage.TableReferentiel.Add(ligneref1);
                    //Init.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(Init.TableReferentiel, (string)comboBox_filtre.SelectedValue);
                    nbItemRapprochés++;
                }
                UpdateProgress_rapprochement(i, myformchargement);
                i++;
            }
            UpdateProgress_rapprochement_module_terminate(nbItemRapprochés, myform, myformchargement);
        }


        public void NePasReprendreModule(Form1 myform, Chargement myformchargement)
        {
            if (VariablePartage.BaseCharge)
            {
                myformchargement.progressBar_chargement.Maximum = VariablePartage.TableCorrespondanceFiltre.Count;
                //GUI.InactiveControlExceptParamButton(myform.button_acces_admin, myform);
                Thread nepasreprendrecode_module = new Thread(new ParameterizedThreadStart(NePasReprendreCodeModuleThread));
                string p1 = (string)myform.comboBox_filtre.SelectedValue;
                Form1 p2 = myform;
                Chargement p3 = myformchargement;
                object args = new object[3] { p1, p2, p3 };
                VariablePartage.ThreadStop = false;
                nepasreprendrecode_module.Start(args);
            }
        }


        public void NePasReprendreCodeModuleThread(object args)
        {
            Array argArray = new object[2];
            argArray = (Array)args;
            string clefValeur = (string)argArray.GetValue(0);
            Form1 myform = (Form1)argArray.GetValue(1);
            Chargement myformchargement = (Chargement)argArray.GetValue(2);
            int nbItemRapprochés = 0;

            
            int i = 0;
            while (i < VariablePartage.TableCorrespondanceFiltre.Count && VariablePartage.ThreadStop == false)
            {
                if (VariablePartage.TableCorrespondanceFiltre[i].FlagReferentiel == 0)
                {
                    VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code = "";
                    VariablePartage.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code = "Non repris";
                    VariablePartage.TableCorrespondanceFiltre[i].FlagReferentiel = 3;
                    CorrObjectDAL.UpdateSQLITE_TBCorrespondance(VariablePartage.TableCorrespondanceFiltre[i]);
                    CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(VariablePartage.TableCorrespondanceFiltre[i]);
                    nbItemRapprochés++;
                }
                UpdateProgress_rapprochement(i, myformchargement);
                i++;
            }
            UpdateProgress_rapprochement_module_terminate(nbItemRapprochés, myform,myformchargement);


        }

        public void CreerGlobal(Form1 myform, Chargement myformchargement, bool CodeActif)
        {
            if (VariablePartage.BaseCharge)
            {
                myformchargement.progressBar_chargement.Maximum = VariablePartage.TableCorrespondance.Count;
                //GUI.InactiveControlExceptParamButton(myform.button_acces_admin, myform);
                Thread creer_inactif_global = new Thread(new ParameterizedThreadStart(CreerCodeGlobalThread));
                Form1 p1 = myform;
                Chargement p2 = myformchargement;
                bool p3 = CodeActif;
                object args = new object[3] { p1, p2, p3 };
                VariablePartage.ThreadStop = false;
                creer_inactif_global.Start(args);
            }
        }

        public void CreerCodeGlobalThread(object args)
        {
            Array argArray = new object[3];
            argArray = (Array)args;
            Form1 myform = (Form1)argArray.GetValue(0);
            Chargement myformchargement = (Chargement)argArray.GetValue(1);
            bool CodeActif = (bool)argArray.GetValue(2);
            CorrespondanceDAL ObjCorr = new CorrespondanceDAL();
            int nbItemRapprochés = 0;

            int i = 0;

            VariablePartage.ThreadStop = false;
            while (i < VariablePartage.TableCorrespondance.Count && VariablePartage.ThreadStop == false)
            {
                if ((VariablePartage.TableCorrespondance[i].Nouveau_Code == null || VariablePartage.TableCorrespondance[i].Nouveau_Code == "")
                    && VariablePartage.TableCorrespondance[i].Libelle_Ancien_Code != null && VariablePartage.TableCorrespondance[i].Libelle_Ancien_Code != ""
                    && VariablePartage.TableCorrespondance[i].FlagReferentiel == 0
                    && CorrObjectBLL.NouveauCodeEstDansLeReferentiel(
                        VariablePartage.TableCorrespondance[i].Ancien_Code,
                        VariablePartage.TableReferentiel,
                        VariablePartage.TableCorrespondance[i].Cpl,
                        VariablePartage.TableCorrespondance[i].Cpl1,
                        VariablePartage.TableCorrespondance[i].Cpl2,
                        VariablePartage.TableCorrespondance[i].TypeRef) == false)
                {
                    VariablePartage.TableCorrespondance[i].Nouveau_Code = VariablePartage.TableCorrespondance[i].Ancien_Code;
                    VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code = VariablePartage.TableCorrespondance[i].Libelle_Ancien_Code;
                    VariablePartage.TableCorrespondance[i].NouveauCodeInactif = !CodeActif;
                    VariablePartage.TableCorrespondance[i].FlagReferentiel = 2;
                    CorrObjectDAL.UpdateSQLITE_TBCorrespondance(VariablePartage.TableCorrespondance[i]);
                    CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(VariablePartage.TableCorrespondance[i]);

                    List<ReferentielBLL> ligneref = new List<ReferentielBLL>();
                    ReferentielBLL ligneref1 = new ReferentielBLL();
                    ligneref1.Code = VariablePartage.TableCorrespondance[i].Nouveau_Code;
                    ligneref1.Lib = VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code;
                    ligneref1.InActif = !CodeActif;
                    ligneref1.Type = VariablePartage.TableCorrespondance[i].TypeRef;
                    ligneref1.Cpl = VariablePartage.TableCorrespondance[i].Cpl;

                    if (VariablePartage.TableCorrespondance[i].Cpl1 == null)
                    {
                        VariablePartage.TableCorrespondance[i].Cpl1 = "";
                    }
                    ligneref1.Cpl1 = VariablePartage.TableCorrespondance[i].Cpl1;

                    if (VariablePartage.TableCorrespondance[i].Cpl2 == null)
                    {
                        VariablePartage.TableCorrespondance[i].Cpl2 = "";
                    }
                    ligneref1.Cpl2 = VariablePartage.TableCorrespondance[i].Cpl2;
                    ligneref1.FlagPreventiel = 2;
                    ligneref.Add(ligneref1);
                    RefObjectDAL.InsertIntoSQLITE_TBReferentiel(ligneref);
                    VariablePartage.TableReferentiel.Add(ligneref1);

                    nbItemRapprochés++;
                }
                UpdateProgress_rapprochement(i, myformchargement);
                i++;
            }
            UpdateProgress_rapprochement_module_terminate(nbItemRapprochés, myform, myformchargement);

        }

        public void NePasReprendreGlobal(Form1 myform, Chargement myformchargement)
        {
            if (VariablePartage.BaseCharge)
            {
                myformchargement.progressBar_chargement.Maximum = VariablePartage.TableCorrespondance.Count;
                //GUI.InactiveControlExceptParamButton(myform.button_acces_admin, myform);
                Thread nepasreprendrecode_global = new Thread(new ParameterizedThreadStart(NePasReprendreCodeGlobalThread));
                Form1 p1 = myform;
                Chargement p2 = myformchargement;
                object args = new object[2] { p1, p2 };
                VariablePartage.ThreadStop = false;
                nepasreprendrecode_global.Start(args);
            }
        }

        public void NePasReprendreCodeGlobalThread(object args)
        {
            Array argArray = new object[2];
            argArray = (Array)args;
            Form1 myform = (Form1)argArray.GetValue(0);
            Chargement myformchargement = (Chargement)argArray.GetValue(1);
            
            VariablePartage.ThreadStop = false;
            

            CorrespondanceDAL ObjCorr = new CorrespondanceDAL();
            int nbItemRapprochés = 0;
            int i = 0;
            while (i < VariablePartage.TableCorrespondance.Count && VariablePartage.ThreadStop == false)
            {
                if (VariablePartage.TableCorrespondance[i].FlagReferentiel == 0)
                {
                    VariablePartage.TableCorrespondance[i].Nouveau_Code = "";
                    VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code = "Non repris";
                    VariablePartage.TableCorrespondance[i].FlagReferentiel = 3;
                    CorrObjectDAL.UpdateSQLITE_TBCorrespondance(VariablePartage.TableCorrespondance[i]);
                    CorrObjectDAL.UpdateSQLITE_TBCorrespondance_FlagPreventiel(VariablePartage.TableCorrespondance[i]);
                    nbItemRapprochés++;
                }
                UpdateProgress_rapprochement(i, myformchargement);
                i++;
            }

            UpdateProgress_rapprochement_module_terminate(nbItemRapprochés, myform, myformchargement);

        }


        public void SuppressionRecodageModule(Form1 myform)
        {
            if (VariablePartage.BaseCharge)
            {
                DialogResult dialogResult = MessageBox.Show("Vous allez supprimer le recodage du module, êtes-vous bien sûr ?", "Suppression du recodage module", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    CorrObjectDAL.SupprimeRecodageModule(VariablePartage.TableCorrespondanceFiltre[0].NomRef);
                    RefObjectDAL.SupprimeRecodageModule_referentiel(VariablePartage.TableReferentielFiltre);
                    VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);
                    var source_referentiel = new BindingSource();
                    source_referentiel.DataSource = VariablePartage.TableReferentielFiltre;
                    myform.dataGridView_ref.DataSource = source_referentiel;
                    GUI.InitStructSaisie(myform.dataGridView_saisie, myform);
                    GUI.InitColorSaisie(myform.dataGridView_saisie);
                    GUI.InitStructReferentiel(myform.dataGridView_ref, myform.dataGridView_saisie);
                    GUI.InitColorReferentiel(myform.dataGridView_ref);
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
        }

        public void SuppressionRecodageGlobal(Form1 myform)
        {
            if (VariablePartage.BaseCharge)
            {
                DialogResult dialogResult = MessageBox.Show("Vous allez supprimer l'intégralité du recodage, êtes-vous bien sûr ?", "Suppression du recodage global", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    CorrObjectDAL.SupprimeRecodageFULLTABLE();
                    RefObjectDAL.SupprimeRecodageFULLTABLE_referentiel();
                    VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);
                    var source_referentiel = new BindingSource();
                    source_referentiel.DataSource = VariablePartage.TableReferentielFiltre;
                    myform.dataGridView_ref.DataSource = source_referentiel;
                    GUI.InitStructSaisie(myform.dataGridView_saisie, myform);
                    GUI.InitColorSaisie(myform.dataGridView_saisie);
                    GUI.InitStructReferentiel(myform.dataGridView_ref, myform.dataGridView_saisie);
                    GUI.InitColorReferentiel(myform.dataGridView_ref);
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
        }
    }
}
