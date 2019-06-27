using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using RecodageList.BLL;
using System.Windows.Forms;

namespace RecodageList.DAL
{
    public class ComboBoxFiltreDAL
    {
        public void ObtenirComboBoxFiltre_Thread(object args)
        {
            Array argArray = new object[1];
            argArray = (Array)args;
            Form1 myform = (Form1)argArray.GetValue(0);
            while (VariablePartage.ThreadNumber!=6)
            {
                //Staying here;
            }
            using (ConnexionSQLITE con = new ConnexionSQLITE())
            {
                con.SetConnection();
                con.sql_con.Open();
                //sql_cmd = sql_con.CreateCommand();
                //string CommandText = "select Type_Item, Ancien_Code, Libelle_Ancien_Code,AncienCodeActif,Nouveau_Code,Libelle_Nouveau_Code,Code_utilise,NomSchema,DateRecensement,DateMAJ,TypeRecodage,NouveauCodeInactif,UtilisateurCreation,TypeRef,NomRef,Cpl,Cpl1,Cpl2,Occurrence from TB$S_CorrespondanceItem";
                string CommandText = "select distinct NomRef, Cpl, Cpl1, Cpl2,TypeRef from TB$S_CorrespondanceItem where NomRef is not null and Cpl not in('50','60') and NomRef!= 'PersonnePhysique' order by Nomref";
                using (con.sql_cmd = new SQLiteCommand(CommandText, con.sql_con))
                {
                    using (SQLiteDataReader reader = con.sql_cmd.ExecuteReader())
                    {
                        //DB = new SQLiteDataAdapter(CommandText, sql_con);
                        List<ComboBoxFiltreBLL> ListComboBox = new List<ComboBoxFiltreBLL>();
                        while (reader.Read())
                        {
                            ComboBoxFiltreBLL comboboxfiltre = new ComboBoxFiltreBLL();
                            if (reader["NomRef"] != DBNull.Value)
                            {
                                comboboxfiltre.NomRef = (string)reader["NomRef"];
                            }
                            if (reader["Cpl"] != DBNull.Value)
                            {
                                comboboxfiltre.Cpl = (string)reader["Cpl"];
                            }
                            if (reader["Cpl1"] != DBNull.Value && (string)reader["Cpl1"] != "")
                            {
                                comboboxfiltre.Cpl += "|" + (string)reader["Cpl1"];
                            }
                            else
                            {
                                comboboxfiltre.Cpl += "|" + "0";
                            }
                            if (reader["Cpl2"] != DBNull.Value && (string)reader["Cpl2"] != "")
                            {
                                comboboxfiltre.Cpl += "|" + (string)reader["Cpl2"];
                            }
                            else
                            {
                                comboboxfiltre.Cpl += "|" + "0";
                            }
                            if (reader["TypeRef"] != DBNull.Value)
                            {
                                comboboxfiltre.Cpl += "|" + (string)reader["TypeRef"];
                            }

                            ComboBoxFiltreBLL comboBoxFiltreBLL = new ComboBoxFiltreBLL();
                            if (comboBoxFiltreBLL.ContientNomRefAdmin(VariablePartage.ListeNomRef_admin, comboboxfiltre.NomRef))
                            {
                                //Init.ListeNomRef_Adm_Med[i][0] = Init.ListeNomRef_all[i];
                                //Init.ListeNomRef_Adm_Med[i][1] = "Adm";
                                comboboxfiltre.TypeRecodage = 1;
                            }
                            else
                            {
                                //Init.ListeNomRef_Adm_Med[i][0] = Init.ListeNomRef_all[i];
                                //Init.ListeNomRef_Adm_Med[i][1] = "Med";
                                comboboxfiltre.TypeRecodage = 3;
                            }


                            ListComboBox.Add(comboboxfiltre);
                        }
                        con.sql_con.Close();
                        ComboBoxFiltreBLL comboboxfiltre_administratif = new ComboBoxFiltreBLL();
                        comboboxfiltre_administratif.NomRef = "-----Administratif-----";
                        comboboxfiltre_administratif.Cpl = "adm1|adm1|adm1|adm1";
                        comboboxfiltre_administratif.TypeRecodage = 0;
                        ListComboBox.Add(comboboxfiltre_administratif);
                        ComboBoxFiltreBLL comboboxfiltre_medical = new ComboBoxFiltreBLL();
                        comboboxfiltre_medical.NomRef = "-------Medical---------";
                        comboboxfiltre_medical.Cpl = "med1|med1|med1|med1";
                        comboboxfiltre_medical.TypeRecodage = 2;
                        ListComboBox.Add(comboboxfiltre_medical);

                        ListComboBox.Sort(delegate (ComboBoxFiltreBLL a1, ComboBoxFiltreBLL a2) { return a1.TypeRecodage - a2.TypeRecodage; });

                        VariablePartage.ComboBoxFiltre = ListComboBox;
                        //return (ListComboBox);
                    }

                }

            }
            GUIFonction GUI = new GUIFonction();
            InitComboBoxFiltre_invoke(myform);
            VariablePartage.BaseCharge = true;
            ReactiveButton_invoke(myform);
            //myform.comboBox_filtre.DropDownStyle = ComboBoxStyle.DropDownList;
            InitClientEncour_Invoke(myform);
            //GUI.InitStructSaisie(dataGridView_saisie);
            //GUI.InitStructReferentiel(dataGridView_ref,dataGridView_saisie);
            //ActionRecodage actionRecodage = new ActionRecodage();
            //actionRecodage.FilterModule(myform.dataGridView_saisie, myform.dataGridView_ref, myform);
            FilterModule_invoke(myform);
            VariablePartage.ThreadNumber = 7;
        }

        public void AffecteComboBoxFiltre_thread()
        {

        }

        public void FilterModule_invoke(Form1 myform)
        {
            try
            {
                ActionRecodage actionRecodage = new ActionRecodage();
                myform.Invoke((FilterModuleDelegate)actionRecodage.FilterModule, myform.dataGridView_saisie, myform.dataGridView_ref, myform);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void InitClientEncour_Invoke(Form1 myform)
        {
            try
            {
                myform.Invoke((InitComboBoxFiltre_delegate)InitClientEnCour, myform);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void InitClientEnCour(Form1 myform)
        {
            myform.label_client_en_cour.Text = VariablePartage.ClientEnCours;
        }

        public delegate void InitComboBoxFiltre_delegate(Form1 myform);

        public delegate void FilterModuleDelegate(DataGridView datagrid_saisie, DataGridView datagrid_ref, Form1 myform);


        public void InitComboBoxFiltre_invoke(Form1 myform)
        {
            try
            {
                GUIFonction GUI = new GUIFonction();
                myform.Invoke((InitComboBoxFiltre_delegate)GUI.InitComboBoxFiltre, myform);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ReactiveButton_invoke(Form1 myform)
        {
            try
            {
                GUIFonction GUI = new GUIFonction();
                myform.Invoke((InitComboBoxFiltre_delegate)GUI.ReactiveButton, myform);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public List<ComboBoxFiltreBLL> ObtenirComboBoxFiltre()
        {
            using (ConnexionSQLITE con = new ConnexionSQLITE())
            {
                con.SetConnection();
                con.sql_con.Open();
                //sql_cmd = sql_con.CreateCommand();
                //string CommandText = "select Type_Item, Ancien_Code, Libelle_Ancien_Code,AncienCodeActif,Nouveau_Code,Libelle_Nouveau_Code,Code_utilise,NomSchema,DateRecensement,DateMAJ,TypeRecodage,NouveauCodeInactif,UtilisateurCreation,TypeRef,NomRef,Cpl,Cpl1,Cpl2,Occurrence from TB$S_CorrespondanceItem";
                string CommandText = "select distinct NomRef, Cpl, Cpl1, Cpl2,TypeRef from TB$S_CorrespondanceItem where NomRef is not null and Cpl not in('50','60') and NomRef!= 'PersonnePhysique' order by Nomref";
                using (con.sql_cmd = new SQLiteCommand(CommandText, con.sql_con))
                {
                    using (SQLiteDataReader reader = con.sql_cmd.ExecuteReader())
                    {
                        //DB = new SQLiteDataAdapter(CommandText, sql_con);
                        List<ComboBoxFiltreBLL> ListComboBox = new List<ComboBoxFiltreBLL>();
                        while (reader.Read())
                        {
                            ComboBoxFiltreBLL comboboxfiltre = new ComboBoxFiltreBLL();
                            if (reader["NomRef"] != DBNull.Value)
                            {
                                comboboxfiltre.NomRef = (string)reader["NomRef"];
                            }
                            if (reader["Cpl"] != DBNull.Value)
                            {
                                comboboxfiltre.Cpl = (string)reader["Cpl"];
                            }
                            if (reader["Cpl1"] != DBNull.Value && (string)reader["Cpl1"] != "")
                            {
                                comboboxfiltre.Cpl += "|" + (string)reader["Cpl1"];
                            }
                            else
                            {
                                comboboxfiltre.Cpl += "|" + "0";
                            }
                            if (reader["Cpl2"] != DBNull.Value && (string)reader["Cpl2"] != "")
                            {
                                comboboxfiltre.Cpl += "|" + (string)reader["Cpl2"];
                            }
                            else
                            {
                                comboboxfiltre.Cpl += "|" + "0";
                            }
                            if (reader["TypeRef"] != DBNull.Value)
                            {
                                comboboxfiltre.Cpl += "|" + (string)reader["TypeRef"];
                            }

                            ComboBoxFiltreBLL comboBoxFiltreBLL = new ComboBoxFiltreBLL();
                                if (comboBoxFiltreBLL.ContientNomRefAdmin(VariablePartage.ListeNomRef_admin, comboboxfiltre.NomRef))
                                {
                                    //Init.ListeNomRef_Adm_Med[i][0] = Init.ListeNomRef_all[i];
                                    //Init.ListeNomRef_Adm_Med[i][1] = "Adm";
                                    comboboxfiltre.TypeRecodage = 1;
                                }
                                else
                                {
                                    //Init.ListeNomRef_Adm_Med[i][0] = Init.ListeNomRef_all[i];
                                    //Init.ListeNomRef_Adm_Med[i][1] = "Med";
                                    comboboxfiltre.TypeRecodage = 3;
                                }
                            

                            ListComboBox.Add(comboboxfiltre);
                        }
                        con.sql_con.Close();
                        ComboBoxFiltreBLL comboboxfiltre_administratif = new ComboBoxFiltreBLL();
                        comboboxfiltre_administratif.NomRef = "-----Administratif-----";
                        comboboxfiltre_administratif.Cpl = "adm1|adm1|adm1|adm1";
                        comboboxfiltre_administratif.TypeRecodage = 0;
                        ListComboBox.Add(comboboxfiltre_administratif);
                        ComboBoxFiltreBLL comboboxfiltre_medical = new ComboBoxFiltreBLL();
                        comboboxfiltre_medical.NomRef = "-------Medical---------";
                        comboboxfiltre_medical.Cpl = "med1|med1|med1|med1";
                        comboboxfiltre_medical.TypeRecodage = 2;
                        ListComboBox.Add(comboboxfiltre_medical);

                        ListComboBox.Sort(delegate (ComboBoxFiltreBLL a1, ComboBoxFiltreBLL a2) { return a1.TypeRecodage - a2.TypeRecodage; });
                        
                        return (ListComboBox);
                    }
                       
                }
                   
            }
                
        }

    }
}
