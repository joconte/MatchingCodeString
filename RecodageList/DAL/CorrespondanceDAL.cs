using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;
using RecodageList.BLL;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using RecodageList.GUI;

namespace RecodageList.DAL
{
    public class CorrespondanceDAL
    {
        //ConnexionSQLITE con = new ConnexionSQLITE();
        public static List<CorrespondanceBLL> ListCorrespFlagPreventiel { get; set; }
        
        public static List<CorrespondanceBLL> ListCorresp { get; set; }
        public static List<ReferentielBLL> ListRefFlagPreventiel { get; set; }
        public static int NbLigneListeCorresp { get; set; }


        public void AjoutColonneFlagPreventielCorresp_Thread(object args)
        {
            while(VariablePartage.ThreadNumber!=0)
            {
                //Staying here;
            }
            //VariablePartage.ThreadStop = true;
            Chargement myformchargement = (Chargement)args;
            UpdateProgressAdmin_Maximun(2, myformchargement);
            UpdateLabel_trt_en_cours("TB$S_CorrespondanceItem : \r\nAjout de la colonne FlagPreventiel", myformchargement);
            using (ConnexionSQLITE con = new ConnexionSQLITE())
            {
                Dictionary<string, string> columnNameToAddColumnSql = new Dictionary<string, string>
            {
                {
                    "FlagPreventiel",
                    "ALTER TABLE TB$S_CorrespondanceItem ADD COLUMN FlagPreventiel INTEGER "
                }
            };
                UpdateProgress_rapprochement(1, myformchargement);
                foreach (var pair in columnNameToAddColumnSql)
                {
                    string columnName = pair.Key;
                    string sql = pair.Value;

                    try
                    {
                        con.ExecuteQuery(sql);
                        con.ExecuteQuery("Update TB$S_CorrespondanceItem set FlagPreventiel = 0");
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        con.sql_con.Close();
                        Console.WriteLine(string.Format("Failed to create column [{0}]. Most likely it already exists, which is fine.", columnName));
                    }
                }
                UpdateProgress_rapprochement(2, myformchargement);
                con.sql_con.Close();
            }
            UpdateProgress_rapprochement(0, myformchargement);

            VariablePartage.ThreadNumber = 1;
        }

        public void RetourneLeNombreDeLigneCorrespondance()
        {
            while (VariablePartage.ThreadNumber!=1)
            {
                //Staying here;
            }
            int nbligne = 0;
            
            using (ConnexionSQLITE con = new ConnexionSQLITE())
            {
                con.SetConnection();
                string CommandText = "select count(*) as NbLigne from TB$S_CorrespondanceItem";
                using (con.sql_cmd = new SQLiteCommand(CommandText, con.sql_con))
                {
                    con.sql_con.Open();

                    using (SQLiteDataReader reader = con.sql_cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            nbligne = Convert.ToInt32(reader["NbLigne"]);
                        }
                    }
                }
                con.sql_con.Close();
            }
            NbLigneListeCorresp = nbligne;
            //NbLigneListeCorresp = 50000;
            VariablePartage.ThreadNumber = 2;
        }


        public void ObtenirListeCorrespondance_SQLITE_Thread(object args)
        {
            while(VariablePartage.ThreadNumber!=2)
            {
                //Staying here;
            }
            //VariablePartage.ThreadStop = true;
            Array argArray = new object[3];
            argArray = (Array)args;
            List<ReferentielBLL> Referentiel = (List<ReferentielBLL>)argArray.GetValue(0);
            Form1 myform = (Form1)argArray.GetValue(1);
            Chargement myformchargement = (Chargement)argArray.GetValue(2);

            ListCorrespFlagPreventiel = new List<CorrespondanceBLL>();
            ListRefFlagPreventiel = new List<ReferentielBLL>();
            ListCorresp = new List<CorrespondanceBLL>();

            int maxligne = NbLigneListeCorresp;
            UpdateProgressAdmin_Maximun(maxligne, myformchargement);
            UpdateProgress_rapprochement(0, myformchargement);
            UpdateLabel_trt_en_cours("Chargement de la table SQLite en mémoire et stockage \r\ndes lignes avec un 'nouveau_code' renseignées à recalculer", myformchargement);
            int i = 0;
            using (ConnexionSQLITE con = new ConnexionSQLITE())
            {
                con.SetConnection();
                string CommandText = "select NomSchema, Ancien_Code as [C.Src], Libelle_Ancien_Code as [Intitule source],Nouveau_Code as [Code référentiel],Libelle_Nouveau_Code as [Intitulé du référentiel], Occurrence, Code_utilise,DateRecensement,NomRef,NouveauCodeInactif,TypeRef,Cpl,Cpl1,Cpl2,FlagPreventiel from TB$S_CorrespondanceItem order by Occurrence desc";

                using (con.sql_cmd = new SQLiteCommand(CommandText, con.sql_con))
                {
                    con.sql_con.Open();

                    using (SQLiteDataReader reader = con.sql_cmd.ExecuteReader())
                    {
                        //DB = new SQLiteDataAdapter(CommandText, sql_con);
                        //List<CorrespondanceBLL> ListCorrespFlagPreventiel = new List<CorrespondanceBLL>();
                        //List<CorrespondanceBLL> ListCorresp = new List<CorrespondanceBLL>();
                        //List<ReferentielBLL> ListRefFlagPreventiel = new List<ReferentielBLL>();
                        Fonc fonc = new Fonc();
                        while (reader.Read())
                        {
                            CorrespondanceBLL Corresp = new CorrespondanceBLL();
                            if (reader["NomSchema"] != DBNull.Value)
                            {
                                Corresp.NomSchema = (string)reader["NomSchema"];
                            }
                            if (reader["C.Src"] != DBNull.Value)
                            {
                                Corresp.Ancien_Code = (string)reader["C.Src"];
                            }
                            if (reader["Intitule source"] != DBNull.Value)
                            {
                                Corresp.Libelle_Ancien_Code = (string)reader["Intitule source"];
                                Corresp.Canonical = fonc.CanonicalString(Corresp.Libelle_Ancien_Code);
                            }
                            if (reader["Code référentiel"] != DBNull.Value)
                            {
                                Corresp.Nouveau_Code = (string)reader["Code référentiel"];
                            }
                            if (reader["Intitulé du référentiel"] != DBNull.Value)
                            {
                                Corresp.Libelle_Nouveau_Code = (string)reader["Intitulé du référentiel"];
                                //Console.WriteLine("Je suis avant le canonical");
                                //Console.WriteLine("Canonical : " + fonc.CanonicalString(Corresp.Libelle_Nouveau_Code));
                            }
                            if (reader["NomRef"] != DBNull.Value)
                            {
                                Corresp.NomRef = (string)reader["NomRef"];
                            }
                            if (reader["Occurrence"] != DBNull.Value)
                            {
                                Corresp.Occurrence = Convert.ToInt32(reader["Occurrence"]);
                            }
                            if (reader["TypeRef"] != DBNull.Value)
                            {
                                Corresp.TypeRef = (string)reader["TypeRef"];
                            }
                            if (reader["Cpl"] != DBNull.Value && (string)reader["Cpl"] != "")
                            {
                                Corresp.Cpl = (string)reader["Cpl"];
                            }
                            else
                            {
                                Corresp.Cpl = "0";
                            }
                            if (reader["Cpl1"] != DBNull.Value && (string)reader["Cpl1"] != "")
                            {
                                Corresp.Cpl1 = (string)reader["Cpl1"];
                            }
                            else
                            {
                                Corresp.Cpl1 = "0";
                            }
                            if (reader["Cpl2"] != DBNull.Value && (string)reader["Cpl2"] != "")
                            {
                                Corresp.Cpl2 = (string)reader["Cpl2"];
                            }
                            else
                            {
                                Corresp.Cpl2 = "0";
                            }
                            if (reader["FlagPreventiel"] != DBNull.Value)
                            {
                                Corresp.FlagReferentiel = Convert.ToInt32(reader["FlagPreventiel"]);
                            }
                            /*if (reader["Code référentiel"].ToString().Trim() == "APTITU_06" && reader["FlagPreventiel"] != DBNull.Value)
                            {
                                Console.WriteLine("Motif Aptitude");
                                Console.WriteLine(Convert.ToInt32(reader["FlagPreventiel"]));
                            }*/
                            if (reader["Code référentiel"] != DBNull.Value && (string)reader["Code référentiel"] != "" && reader["FlagPreventiel"] != DBNull.Value && Convert.ToInt32(reader["FlagPreventiel"]) == 0)
                            {
                                /*if(reader["Code référentiel"] != DBNull.Value && (string)reader["Code référentiel"] != "" && reader["FlagPreventiel"] != DBNull.Value && Convert.ToInt32(reader["FlagPreventiel"]) == 0 
                                    && reader["Code référentiel"].ToString().Trim() == "APTITU_06")
                                {
                                    Console.WriteLine("Motif Aptitude");
                                }*/
                                CorrespondanceBLL correspondanceBLL = new CorrespondanceBLL();
                                if (correspondanceBLL.NouveauCodeEstDansLeReferentiel((string)reader["Code référentiel"], Referentiel, Corresp.Cpl, Corresp.Cpl1, Corresp.Cpl2, (string)reader["TypeRef"]))
                                {
                                    Corresp.FlagReferentiel = 1;
                                    ListCorrespFlagPreventiel.Add(Corresp);
                                    //UpdateSQLITE_TBCorrespondance_FlagPreventiel(Corresp);
                                }
                                else
                                {
                                    Corresp.FlagReferentiel = 2;
                                    ListCorrespFlagPreventiel.Add(Corresp);
                                    //ReferentielDAL ObjRefDAL = new ReferentielDAL();
                                    ReferentielBLL ligneref = new ReferentielBLL();
                                    ligneref.Type = Corresp.TypeRef;
                                    ligneref.Cpl = Corresp.Cpl;
                                    ligneref.Cpl1 = Corresp.Cpl1;
                                    if (Corresp.NomRef == "PersonnelMedical")
                                    {
                                        ligneref.Cpl1 = "2";
                                    }
                                    else if (Corresp.Cpl == "50")
                                    {
                                        string[] examen_ancien_code = Corresp.Ancien_Code.ToString().Split('#');
                                        CorrespondanceBLL ligne_exam_nouveau_code = new CorrespondanceBLL();
                                        ligne_exam_nouveau_code = ligne_exam_nouveau_code.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondance, examen_ancien_code[0], "135|0|0|NOMEN");
                                        if(ligne_exam_nouveau_code.Nouveau_Code!=null && ligne_exam_nouveau_code.Nouveau_Code != "")
                                        {
                                            ligneref.Cpl1 = ligne_exam_nouveau_code.Nouveau_Code;
                                            if (ligneref.Cpl1 == "55|P03")
                                                Console.WriteLine("Ici!");
                                        }
                                        else
                                        {
                                            ligneref.Cpl1 = ligne_exam_nouveau_code.Ancien_Code;
                                        }
                                    }
                                    else if (Corresp.Cpl == "60")
                                    {
                                        string[] examen_ancien_code = Corresp.Ancien_Code.ToString().Split('#');
                                        CorrespondanceBLL ligne_exam_nouveau_code = new CorrespondanceBLL();
                                        ligne_exam_nouveau_code = ligne_exam_nouveau_code.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondance, examen_ancien_code[0], "141|0|0|NOMEN");
                                        if (ligne_exam_nouveau_code.Nouveau_Code != null && ligne_exam_nouveau_code.Nouveau_Code != "")
                                        {
                                            ligneref.Cpl1 = ligne_exam_nouveau_code.Nouveau_Code;
                                        }
                                        else
                                        {
                                            ligneref.Cpl1 = ligne_exam_nouveau_code.Ancien_Code;
                                        }
                                    }
                                    ligneref.Cpl2 = Corresp.Cpl2;
                                    ligneref.Code = Corresp.Nouveau_Code;
                                    ligneref.Lib = Corresp.Libelle_Nouveau_Code;
                                    ligneref.FlagPreventiel = 2;
                                    ReferentielBLL RefObjBLL = new ReferentielBLL();
                                    if (!RefObjBLL.CodeEstDansLeReferentiel(ListRefFlagPreventiel, ligneref.Code))
                                    {
                                        if(ligneref.Cpl1=="55|P03")
                                        {
                                            Console.WriteLine("ici!");
                                        }
                                        ListRefFlagPreventiel.Add(ligneref);
                                    }
                                }
                            }
                            if (reader["Code référentiel"] != DBNull.Value && (string)reader["Code référentiel"] == "" && reader["FlagPreventiel"] != DBNull.Value && Convert.ToInt32(reader["FlagPreventiel"]) == 0
                                && reader["Intitulé du référentiel"] != DBNull.Value && (string)reader["Intitulé du référentiel"] == "Non repris")
                            {
                                Corresp.FlagReferentiel = 3;
                            }
                            if (reader["NouveauCodeInactif"] != DBNull.Value)
                            {
                                Corresp.NouveauCodeInactif = (bool)reader["NouveauCodeInactif"];
                            }
                            //Console.WriteLine("Je suis avant le ADD");
                            ListCorresp.Add(Corresp);

                            i++;
                            UpdateProgress_rapprochement(i, myformchargement);
                            //Console.WriteLine("Je suis après le ADD");
                        }
                        //con.sql_con.Close();
                        //myform.progressBar_admin.Maximum = ListCorrespFlagPreventiel.Count;
                        //Thread ajoutFlagReferentielCorrespSQLite_Thread = new Thread(new ParameterizedThreadStart(AjoutFlagReferentielCorrespSQLite_Thread));
                        //List<CorrespondanceBLL> p1 = ListCorrespFlagPreventiel;
                        //Form1 p2 = myform;
                        //object args = new object[2] { p1, p2 };
                        //ajoutFlagReferentielCorrespSQLite_Thread.Start(args);

                        //AjoutFlagReferentielCorrespSQLite_Thread(ListCorrespFlagPreventiel,myform);

                        //Thread ajoutFlagReferentielRefSQLite_Thread = new Thread(new ParameterizedThreadStart(AjoutFlagReferentielRefSQLite_Thread));
                        //List<ReferentielBLL> a1 = ListRefFlagPreventiel;
                        //Form1 a2 = myform;
                        //object args2 = new object[2] { a1, a2 };
                        //ajoutFlagReferentielRefSQLite_Thread.Start(args2);

                        //AjoutFlagReferentielRefSQLite_Thread(ListRefFlagPreventiel);




                        VariablePartage.TableCorrespondanceSansModification = ListCorresp;
                        con.sql_con.Close();
                        //return (ListCorresp);
                    }

                }
            }
            UpdateProgress_rapprochement(0, myformchargement);
            try
            {
                VariablePartage.ClientEnCours = ListCorresp[0].NomSchema;
            }
            catch(Exception e)
            {
                Console.WriteLine("Erreur lors de l'affectation du client en cours : " + e.Message);
            }
            VariablePartage.ThreadNumber = 3;
        }

        public List<CorrespondanceBLL> ObtenirListeCorrespondance_SQLITE(List<ReferentielBLL> Referentiel, Form1 myform)
        {
            ListCorrespFlagPreventiel = new List<CorrespondanceBLL>();
            ListRefFlagPreventiel = new List<ReferentielBLL>();
            ListCorresp = new List<CorrespondanceBLL>();
            //con.SetConnection();
            using (ConnexionSQLITE con = new ConnexionSQLITE())
            {
                Dictionary<string, string> columnNameToAddColumnSql = new Dictionary<string, string>
            {
                {
                    "FlagPreventiel",
                    "ALTER TABLE TB$S_CorrespondanceItem ADD COLUMN FlagPreventiel INTEGER "
                }
            };

                foreach (var pair in columnNameToAddColumnSql)
                {
                    string columnName = pair.Key;
                    string sql = pair.Value;

                    try
                    {
                        con.ExecuteQuery(sql);
                        con.ExecuteQuery("Update TB$S_CorrespondanceItem set FlagPreventiel = 0");
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        con.sql_con.Close();
                        Console.WriteLine(string.Format("Failed to create column [{0}]. Most likely it already exists, which is fine.", columnName));
                    }
                }


                //con.sql_con.Open();
                //sql_cmd = sql_con.CreateCommand();
                //string CommandText = "select Type_Item, Ancien_Code, Libelle_Ancien_Code,AncienCodeActif,Nouveau_Code,Libelle_Nouveau_Code,Code_utilise,NomSchema,DateRecensement,DateMAJ,TypeRecodage,NouveauCodeInactif,UtilisateurCreation,TypeRef,NomRef,Cpl,Cpl1,Cpl2,Occurrence from TB$S_CorrespondanceItem";
                string CommandText = "select NomSchema, Ancien_Code as [C.Src], Libelle_Ancien_Code as [Intitule source],Nouveau_Code as [Code référentiel],Libelle_Nouveau_Code as [Intitulé du référentiel], Occurrence, Code_utilise,DateRecensement,NomRef,NouveauCodeInactif,TypeRef,Cpl,Cpl1,Cpl2,FlagPreventiel from TB$S_CorrespondanceItem where NomRef not like 'PersonnePhysique' order by Occurrence desc";

                using (con.sql_cmd = new SQLiteCommand(CommandText, con.sql_con))
                {
                    con.sql_con.Open();

                    using (SQLiteDataReader reader = con.sql_cmd.ExecuteReader())
                    {
                        //DB = new SQLiteDataAdapter(CommandText, sql_con);
                        //List<CorrespondanceBLL> ListCorrespFlagPreventiel = new List<CorrespondanceBLL>();
                        //List<CorrespondanceBLL> ListCorresp = new List<CorrespondanceBLL>();
                        //List<ReferentielBLL> ListRefFlagPreventiel = new List<ReferentielBLL>();
                        Fonc fonc = new Fonc();
                        while (reader.Read())
                        {
                            CorrespondanceBLL Corresp = new CorrespondanceBLL();
                            if (reader["NomSchema"] != DBNull.Value)
                            {
                                Corresp.NomSchema = (string)reader["NomSchema"];
                            }
                            if (reader["C.Src"] != DBNull.Value)
                            {
                                Corresp.Ancien_Code = (string)reader["C.Src"];
                            }
                            if (reader["Intitule source"] != DBNull.Value)
                            {
                                Corresp.Libelle_Ancien_Code = (string)reader["Intitule source"];
                                Corresp.Canonical = fonc.CanonicalString(Corresp.Libelle_Ancien_Code);
                            }
                            if (reader["Code référentiel"] != DBNull.Value)
                            {
                                Corresp.Nouveau_Code = (string)reader["Code référentiel"];
                            }
                            if (reader["Intitulé du référentiel"] != DBNull.Value)
                            {
                                Corresp.Libelle_Nouveau_Code = (string)reader["Intitulé du référentiel"];
                                //Console.WriteLine("Je suis avant le canonical");
                                //Console.WriteLine("Canonical : " + fonc.CanonicalString(Corresp.Libelle_Nouveau_Code));
                            }
                            if (reader["NomRef"] != DBNull.Value)
                            {
                                Corresp.NomRef = (string)reader["NomRef"];
                            }
                            if (reader["Occurrence"] != DBNull.Value)
                            {
                                Corresp.Occurrence = Convert.ToInt32(reader["Occurrence"]);
                            }
                            if (reader["TypeRef"] != DBNull.Value)
                            {
                                Corresp.TypeRef = (string)reader["TypeRef"];
                            }
                            if (reader["Cpl"] != DBNull.Value && (string)reader["Cpl"] != "")
                            {
                                Corresp.Cpl = (string)reader["Cpl"];
                            }
                            else
                            {
                                Corresp.Cpl = "0";
                            }
                            if (reader["Cpl1"] != DBNull.Value && (string)reader["Cpl1"] != "")
                            {
                                Corresp.Cpl1 = (string)reader["Cpl1"];
                            }
                            else
                            {
                                Corresp.Cpl1 = "0";
                            }
                            if (reader["Cpl2"] != DBNull.Value && (string)reader["Cpl2"] != "")
                            {
                                Corresp.Cpl2 = (string)reader["Cpl2"];
                            }
                            else
                            {
                                Corresp.Cpl2 = "0";
                            }
                            if (reader["FlagPreventiel"] != DBNull.Value)
                            {
                                Corresp.FlagReferentiel = Convert.ToInt32(reader["FlagPreventiel"]);
                            }
                            /*if (reader["Code référentiel"].ToString().Trim() == "APTITU_06" && reader["FlagPreventiel"] != DBNull.Value)
                            {
                                Console.WriteLine("Motif Aptitude");
                                Console.WriteLine(Convert.ToInt32(reader["FlagPreventiel"]));
                            }*/
                            if (reader["Code référentiel"] != DBNull.Value && (string)reader["Code référentiel"] != "" && reader["FlagPreventiel"] != DBNull.Value && Convert.ToInt32(reader["FlagPreventiel"]) == 0)
                            {
                                /*if(reader["Code référentiel"] != DBNull.Value && (string)reader["Code référentiel"] != "" && reader["FlagPreventiel"] != DBNull.Value && Convert.ToInt32(reader["FlagPreventiel"]) == 0 
                                    && reader["Code référentiel"].ToString().Trim() == "APTITU_06")
                                {
                                    Console.WriteLine("Motif Aptitude");
                                }*/
                                CorrespondanceBLL correspondanceBLL = new CorrespondanceBLL();
                                if (correspondanceBLL.NouveauCodeEstDansLeReferentiel((string)reader["Code référentiel"], Referentiel, Corresp.Cpl, Corresp.Cpl1, Corresp.Cpl2, (string)reader["TypeRef"]))
                                {
                                    Corresp.FlagReferentiel = 1;
                                    ListCorrespFlagPreventiel.Add(Corresp);
                                    //UpdateSQLITE_TBCorrespondance_FlagPreventiel(Corresp);
                                }
                                else
                                {
                                    Corresp.FlagReferentiel = 2;
                                    ListCorrespFlagPreventiel.Add(Corresp);
                                    //ReferentielDAL ObjRefDAL = new ReferentielDAL();
                                    ReferentielBLL ligneref = new ReferentielBLL();
                                    ligneref.Type = Corresp.TypeRef;
                                    ligneref.Cpl = Corresp.Cpl;
                                    ligneref.Cpl1 = Corresp.Cpl1;
                                    if(Corresp.NomRef=="PersonnelMedical")
                                    {
                                        ligneref.Cpl1 = "2";
                                    }
                                    else if(Corresp.Cpl == "50")
                                    {
                                        string[] examen_ancien_code = Corresp.Ancien_Code.ToString().Split('#');
                                        CorrespondanceBLL ligne_exam_nouveau_code = new CorrespondanceBLL();
                                        ligne_exam_nouveau_code = ligne_exam_nouveau_code.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondance, examen_ancien_code[0], "135|0|0|NOMEN");
                                        ligneref.Cpl1 = ligne_exam_nouveau_code.Nouveau_Code;
                                    }
                                    else if (Corresp.Cpl == "60")
                                    {
                                        string[] examen_ancien_code = Corresp.Ancien_Code.ToString().Split('#');
                                        CorrespondanceBLL ligne_exam_nouveau_code = new CorrespondanceBLL();
                                        ligne_exam_nouveau_code = ligne_exam_nouveau_code.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondance, examen_ancien_code[0], "141|0|0|NOMEN");
                                        ligneref.Cpl1 = ligne_exam_nouveau_code.Nouveau_Code;
                                    }
                                    ligneref.Cpl2 = Corresp.Cpl2;
                                    ligneref.Code = Corresp.Nouveau_Code;
                                    ligneref.Lib = Corresp.Libelle_Nouveau_Code;
                                    ligneref.FlagPreventiel = 2;
                                    ReferentielBLL RefObjBLL = new ReferentielBLL();
                                    if(!RefObjBLL.CodeEstDansLeReferentiel(ListRefFlagPreventiel, ligneref.Code))
                                    {
                                        ListRefFlagPreventiel.Add(ligneref);
                                    }
                                }
                            }
                            if(reader["Code référentiel"] != DBNull.Value && (string)reader["Code référentiel"] == "" && reader["FlagPreventiel"] != DBNull.Value && Convert.ToInt32(reader["FlagPreventiel"]) == 0
                                && reader["Intitulé du référentiel"] != DBNull.Value && (string)reader["Intitulé du référentiel"] == "Non repris")
                            {
                                Corresp.FlagReferentiel = 3;
                            }
                            if (reader["NouveauCodeInactif"] != DBNull.Value)
                            {
                                Corresp.NouveauCodeInactif = (bool)reader["NouveauCodeInactif"];
                            }
                            //Console.WriteLine("Je suis avant le ADD");
                            ListCorresp.Add(Corresp);
                            //Console.WriteLine("Je suis après le ADD");
                        }
                        //con.sql_con.Close();
                        //myform.progressBar_admin.Maximum = ListCorrespFlagPreventiel.Count;
                        Thread ajoutFlagReferentielCorrespSQLite_Thread = new Thread(new ParameterizedThreadStart(AjoutFlagReferentielCorrespSQLite_Thread));
                        List<CorrespondanceBLL> p1 = ListCorrespFlagPreventiel;
                        Form1 p2 = myform;
                        object args = new object[2] { p1, p2 };
                        ajoutFlagReferentielCorrespSQLite_Thread.Start(args);

                        //AjoutFlagReferentielCorrespSQLite_Thread(ListCorrespFlagPreventiel,myform);

                        Thread ajoutFlagReferentielRefSQLite_Thread = new Thread(new ParameterizedThreadStart(AjoutFlagReferentielRefSQLite_Thread));
                        List<ReferentielBLL> a1 = ListRefFlagPreventiel;
                        Form1 a2 = myform;
                        object args2 = new object[2] { a1, a2 };
                        ajoutFlagReferentielRefSQLite_Thread.Start(args2);

                        //AjoutFlagReferentielRefSQLite_Thread(ListRefFlagPreventiel);




                        VariablePartage.TableCorrespondanceSansModification = ListCorresp;
                        con.sql_con.Close();
                        return (ListCorresp);
                    }
                       
                }
                   
            }

               
        }

        /// <summary>
        /// Ce delegate me permet de communiquer une valeur qui représente l'avancement du Thread.
        /// Cette valeur sera utilisée plus tard pour faire avancer une ProgressBar.
        /// </summary>
        /// <param name="valeur"></param>
        public delegate void MontrerProgres(int valeur, Chargement myformchargement);

        public delegate void MontrerProgres_close( Chargement myformchargement);

        /// <summary>
        /// Permet d'appeler la méthode Progres_rapprochement() via le delegate MontrerProgres().
        /// Ce qui revient à mettre à jour la valeur 
        /// d'avancement de la ProgressBar 'progressBar_admin'
        /// </summary>
        /// <param name="value"></param>
        private void UpdateProgress_rapprochement(int value, Chargement myformchargement)
        {
            try
            {
                //On invoque le delegate pour qu'il effectue la tâche sur le temps
                //de l'autre thread.
                myformchargement.Invoke((MontrerProgres)Progres_rapprochement, value, myformchargement);
            }
            catch (Exception ex) { return; }
        }

        private void UpdateProgress_rapprochement_terminate(Chargement myformchargement)
        {
            try
            {
                //On invoque le delegate pour qu'il effectue la tâche sur le temps
                //de l'autre thread.
                myformchargement.Invoke((MontrerProgres_close)Progres_rapprochement_terminate, myformchargement);
            }
            catch (Exception ex) { return; }
        }

        public void Progres_rapprochement_terminate( Chargement myformchargement)
        {
            myformchargement.Close();
        }

        /// <summary>
        /// Met à jour la valeur d'avancement de la ProgressBar 'progressBar_admin'
        /// </summary>
        /// <param name="valeur"></param>
        public void Progres_rapprochement(int valeur, Chargement myformchargement)
        {
            //On met la valeur dans le contrôle Windows Forms.
            myformchargement.progressBar_chargement.Value = valeur;
        }

        /// <summary>
        /// Met à jour le max de la ProgressBar 'progressBar_admin'
        /// </summary>
        /// <param name="valeur"></param>
        public void ProgressAdmin_Maximum(int valeur, Chargement myformchargement)
        {
            //On met la valeur dans le contrôle Windows Forms.
            myformchargement.progressBar_chargement.Maximum = valeur;
        }

        private void UpdateProgressAdmin_Maximun(int value, Chargement myformchargement)
        {
            try
            {
                //On invoque le delegate pour qu'il effectue la tâche sur le temps
                //de l'autre thread.
                myformchargement.Invoke((MontrerProgres)ProgressAdmin_Maximum, value, myformchargement);
            }
            catch (Exception ex) { return; }
        }

        /// <summary>
        /// Ce delegate me permet de communiquer une valeur qui représente le traitement en cour.
        /// </summary>
        /// <param name="valeur"></param>
        public delegate void UpdateLabel_trt_en_cours_delegate(string trtencour, Chargement myformchargement);

        private void UpdateLabel_trt_en_cours(string trtencour, Chargement myformchargement)
        {
            try
            {
                //On invoque le delegate pour qu'il effectue la tâche sur le temps
                //de l'autre thread.
                myformchargement.Invoke((UpdateLabel_trt_en_cours_delegate)Label_trt_en_cours, trtencour, myformchargement);
            }
            catch (Exception ex) { return; }
        }

        /// <summary>
        /// Met à jour le label label_traitement_en_cours 
        /// </summary>
        /// <param name="trtencour"></param>
        /// <param name="myform"></param>
        public void Label_trt_en_cours(string trtencour, Chargement myformchargement)
        {
            myformchargement.label_trt_encours.Text = trtencour;
        }


        public void AjoutFlagReferentielCorrespSQLite_Thread(object args)
        {
            while (VariablePartage.ThreadNumber!=3)
            {
                //Staying here;
            }
            //VariablePartage.ThreadStop = true;
            Array argArray = new object[1];
            argArray = (Array)args;
            //List<CorrespondanceBLL> ListCorrespFlagPreventiel = (List<CorrespondanceBLL>)argArray.GetValue(0);
            Chargement myformchargement = (Chargement)argArray.GetValue(0);
            UpdateProgressAdmin_Maximun(ListCorrespFlagPreventiel.Count, myformchargement);
            UpdateProgress_rapprochement(0, myformchargement);
            UpdateLabel_trt_en_cours("Calcul du rapprochement déjà effectué sur la base PREVTGVX7: \r\nCode couleur correspondance", myformchargement);
            for (int i = 0; i < ListCorrespFlagPreventiel.Count; i++)
            {
                try
                {
                    UpdateSQLITE_TBCorrespondance_FlagPreventiel(ListCorrespFlagPreventiel[i]);
                    UpdateProgress_rapprochement(i, myformchargement);
                }
                catch (Exception e)
                {
                    Console.WriteLine("ListCorrespFlagPreventiel[" + i + "] : " + ListCorrespFlagPreventiel[i]);
                    Console.WriteLine("Erreur : " + e.Message);
                    throw;
                }

            }
            UpdateLabel_trt_en_cours("...", myformchargement);
            UpdateProgress_rapprochement(0, myformchargement);
            VariablePartage.ThreadNumber = 4;
        }

        public void AjoutFlagReferentielRefSQLite_Thread(object args)
        {
            while(VariablePartage.ThreadNumber!=4)
            {
                //Staying here;
            }
            //VariablePartage.ThreadStop = true;
            Array argArray = new object[1];
            argArray = (Array)args;
            //List<ReferentielBLL> ListRefFlagPreventiel = (List<ReferentielBLL>)argArray.GetValue(0);
            Chargement myformchargement = (Chargement)argArray.GetValue(0);
            UpdateProgressAdmin_Maximun(2, myformchargement);
            UpdateProgress_rapprochement(0, myformchargement);
            UpdateLabel_trt_en_cours("Calcul du rapprochement déjà effectué sur la base PREVTGVX7:\r\nAjout des 'nouveau_code' non présent dans le référentiel au statut 'à créer' ", myformchargement);
            ReferentielDAL ObjRefDAL = new ReferentielDAL();
            try
            {
                ObjRefDAL.InsertIntoSQLITE_TBReferentiel(ListRefFlagPreventiel);
                UpdateProgress_rapprochement(1, myformchargement);
                ReferentielDAL RefObject = new ReferentielDAL();
                VariablePartage.TableReferentiel = RefObject.ObtenirListeReferentiel_SQLITE();
                UpdateProgress_rapprochement(2, myformchargement);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur : " + e.Message);
            }
            UpdateProgress_rapprochement(0, myformchargement);
            UpdateLabel_trt_en_cours("...", myformchargement);
            VariablePartage.ThreadNumber = 5;
        }

        public void AffecteListeCorresp(object args)
        {
            Array argArray = new object[1];
            argArray = (Array)args;
            Chargement myformchargement = (Chargement)argArray.GetValue(0);
            while (VariablePartage.ThreadNumber != 5)
            {
                //Staying here;
            }
            //VariablePartage.ThreadStop = true;
            UpdateProgressAdmin_Maximun(1, myformchargement);
            UpdateProgress_rapprochement(0, myformchargement);
            VariablePartage.TableCorrespondance = ListCorresp;
            UpdateProgress_rapprochement(1, myformchargement);
            UpdateLabel_trt_en_cours("Table Correspondance entièrement chargée !", myformchargement);
            UpdateProgress_rapprochement(0, myformchargement);
            VariablePartage.ThreadNumber = 6;
            UpdateProgress_rapprochement_terminate(myformchargement);
            MessageBox.Show("Chargement des tables terminé.");
        }

        public void InsertIntoSQLITE_TBCorrespondance(List<CorrespondanceBLL> Corresp)
        {
            ConnexionSQLITE con = new ConnexionSQLITE();
            for (int i=0;i<Corresp.Count;i++)
            {
                string txtSQLQuery = "insert into  TB$S_CorrespondanceItem (Type_Item,Ancien_Code,Libelle_Ancien_Code,AncienCodeActif,Nouveau_Code,Libelle_Nouveau_Code,Occurrence,NomSchema,DateRecensement,DateMAJ,TypeRecodage,NouveauCodeInactif,UtilisateurCreation,TypeRef,NomRef,Cpl,Cpl1,Cpl2,FlagPreventiel) " +
              " values ('" + Corresp[i].TypeItem+
              "','"+Corresp[i].Ancien_Code+ 
              "','" + Corresp[i].Libelle_Ancien_Code + 
              "','" + Corresp[i].AncienCodeActif + 
              "','" + Corresp[i].Nouveau_Code + 
              "','" + Corresp[i].Libelle_Nouveau_Code + 
              "','" + Corresp[i].Occurrence + 
              "','" + Corresp[i].NomSchema + 
              "','" + Corresp[i].DateRecensement + 
              "','" + Corresp[i].DateMAJ + 
              "','" + Corresp[i].TypeRecodage + 
              "','" + Corresp[i].NouveauCodeInactif + 
              "','" + Corresp[i].UtilisateurCreation + 
              "','" + Corresp[i].TypeRef + 
              "','" + Corresp[i].NomRef + 
              "','" + Corresp[i].Cpl + 
              "','" + Corresp[i].Cpl1 + 
              "','" + Corresp[i].Cpl2 +
              "','" + Corresp[i].FlagReferentiel +
              "')";
                con.ExecuteQuery(txtSQLQuery);
            }
        }

        public void UpdateSQLITE_TBCorrespondance(CorrespondanceBLL ligneAUpdater)
        {
            if (ligneAUpdater.Nouveau_Code == null)
            {
                ligneAUpdater.Nouveau_Code = "";
            }
            if (ligneAUpdater.Libelle_Nouveau_Code== null)
            {
                ligneAUpdater.Libelle_Nouveau_Code = "";
            }
            ConnexionSQLITE con = new ConnexionSQLITE();
            string txtSQLQuery = " update TB$S_CorrespondanceItem " +
                " set Nouveau_code = '" + ligneAUpdater.Nouveau_Code.Replace("'", "''") + "'," +
                " Libelle_nouveau_code = '" + ligneAUpdater.Libelle_Nouveau_Code.Replace("'", "''") + "'," +
                //" Cpl1 = '" + ligneAUpdater.Cpl1 + "'," +
                " NouveauCodeInactif = '" + ligneAUpdater.NouveauCodeInactif + "'" +
                " where Ancien_Code like '" + ligneAUpdater.Ancien_Code.Replace("'", "''") + "' and " +
                //" Libelle_ancien_code = '" + ligneAUpdater.Libelle_Ancien_Code.Replace("'", "''") + "' and " +
                " NomRef like '" + ligneAUpdater.NomRef + "'";
                //" Cpl = '" + ligneAUpdater.Cpl + "' and " +
                //" TypeRef = '" + ligneAUpdater.TypeRef + "' and " +
                //" Cpl1 = '" + ligneAUpdater.Cpl1 + "' and " +
                //" Cpl2 = '" + ligneAUpdater.Cpl2 + "'";
            con.ExecuteQuery(txtSQLQuery);
            con.Dispose();
        }

        public void DeleteSQLITE_TBCorrespondance(CorrespondanceBLL ligne_a_supprimer)
        {
            ConnexionSQLITE con = new ConnexionSQLITE();
            string txtSQLQuery =
                "delete " +
                "from TB$S_CorrespondanceItem " +
                "where Ancien_Code like '" + ligne_a_supprimer.Ancien_Code.Replace("'", "''") + "' and " +
                "Cpl like '" + ligne_a_supprimer.Cpl.Replace("'", "''") + "' and " +
                "TypeRef like '" + ligne_a_supprimer.TypeRef + "'";
                con.ExecuteQuery(txtSQLQuery);
                con.Dispose();
        }

        public void UpdateSQLITE_TBCorrespondance_WithCpl1(CorrespondanceBLL ligneAUpdater)
        {
            if (ligneAUpdater.Nouveau_Code == null)
            {
                ligneAUpdater.Nouveau_Code = "";
            }
            if (ligneAUpdater.Libelle_Nouveau_Code == null)
            {
                ligneAUpdater.Libelle_Nouveau_Code = "";
            }
            ConnexionSQLITE con = new ConnexionSQLITE();
            string txtSQLQuery = " update TB$S_CorrespondanceItem " +
                " set Nouveau_code = '" + ligneAUpdater.Nouveau_Code.Replace("'", "''") + "'," +
                " Libelle_nouveau_code = '" + ligneAUpdater.Libelle_Nouveau_Code.Replace("'", "''") + "'," +
                " Cpl1 = '" + ligneAUpdater.Cpl1 + "'," +
                " NouveauCodeInactif = '" + ligneAUpdater.NouveauCodeInactif + "'" +
                " where Ancien_Code like '" + ligneAUpdater.Ancien_Code.Replace("'", "''") + "' and " +
                //" Libelle_ancien_code = '" + ligneAUpdater.Libelle_Ancien_Code.Replace("'", "''") + "' and " +
                " NomRef like '" + ligneAUpdater.NomRef + "'";
            con.ExecuteQuery(txtSQLQuery);
            con.Dispose();
        }

        public void UpdateSQLITE_TBCorrespondance_FlagPreventiel(CorrespondanceBLL ligneAUpdater)
        {
            ConnexionSQLITE con = new ConnexionSQLITE();
            try
            {
                string txtSQLQuery = " update TB$S_CorrespondanceItem " +
                " set FlagPreventiel = " + ligneAUpdater.FlagReferentiel +
                " where Ancien_Code like '" + ligneAUpdater.Ancien_Code.Replace("'", "''") + "' and " +
                //" Libelle_ancien_code = '" + ligneAUpdater.Libelle_Ancien_Code.Replace("'", "''") + "' and " +
                " NomRef like '" + ligneAUpdater.NomRef + "'";
                con.ExecuteQuery(txtSQLQuery);
                //con.Dispose();
                //con.sql_con.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Erreur : " + e.Message);
                throw;
            }
        }

        public void SupprimeRecodageFULLTABLE()
        {
            ConnexionSQLITE con = new ConnexionSQLITE();
            string txtSQLQuery = "Update TB$S_CorrespondanceItem set Nouveau_code = null, libelle_nouveau_code = null, NouveauCodeInactif = 0, FlagPreventiel = 0";
            con.ExecuteQuery(txtSQLQuery);
            con.Dispose();
            for(int i=0;i<VariablePartage.TableCorrespondance.Count;i++)
            {
                VariablePartage.TableCorrespondance[i].Nouveau_Code = null;
                VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code = null;
                VariablePartage.TableCorrespondance[i].NouveauCodeInactif = false;
                VariablePartage.TableCorrespondance[i].FlagReferentiel = 0;
            }
        }

        public void SupprimeRecodageModule(string NomRef)
        {
            ConnexionSQLITE con = new ConnexionSQLITE();
            string txtSQLQuery = "Update TB$S_CorrespondanceItem set Nouveau_code = null, libelle_nouveau_code = null, NouveauCodeInactif = 0, FlagPreventiel = 0 where rtrim(ltrim(NomRef)) like '"+ NomRef.Trim()+"'";
            con.ExecuteQuery(txtSQLQuery);
            con.Dispose();
            for (int i = 0; i < VariablePartage.TableCorrespondance.Count; i++)
            {
                if(VariablePartage.TableCorrespondance[i].NomRef.Trim() == NomRef.Trim())
                {
                    VariablePartage.TableCorrespondance[i].Nouveau_Code = null;
                    VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code = null;
                    VariablePartage.TableCorrespondance[i].NouveauCodeInactif = false;
                    VariablePartage.TableCorrespondance[i].FlagReferentiel = 0;
                }
            }
            if(NomRef == "Examen")
            {
                NomRef = "ParametragesResultatExamen";
                txtSQLQuery = "Update TB$S_CorrespondanceItem set Nouveau_code = null, libelle_nouveau_code = null, NouveauCodeInactif = 0, FlagPreventiel = 0 where rtrim(ltrim(NomRef)) like '" + NomRef.Trim() + "'";
                con.ExecuteQuery(txtSQLQuery);
                con.Dispose();
                for (int i = 0; i < VariablePartage.TableCorrespondance.Count; i++)
                {
                    if (VariablePartage.TableCorrespondance[i].NomRef.Trim() == NomRef.Trim())
                    {
                        VariablePartage.TableCorrespondance[i].Nouveau_Code = null;
                        VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code = null;
                        VariablePartage.TableCorrespondance[i].NouveauCodeInactif = false;
                        VariablePartage.TableCorrespondance[i].FlagReferentiel = 0;
                    }
                }
            }
            if (NomRef == "Vaccin")
            {
                NomRef = "ProtocoleVaccinal";
                txtSQLQuery = "Update TB$S_CorrespondanceItem set Nouveau_code = null, libelle_nouveau_code = null, NouveauCodeInactif = 0, FlagPreventiel = 0 where rtrim(ltrim(NomRef)) like '" + NomRef.Trim() + "'";
                con.ExecuteQuery(txtSQLQuery);
                con.Dispose();
                for (int i = 0; i < VariablePartage.TableCorrespondance.Count; i++)
                {
                    if (VariablePartage.TableCorrespondance[i].NomRef.Trim() == NomRef.Trim())
                    {
                        VariablePartage.TableCorrespondance[i].Nouveau_Code = null;
                        VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code = null;
                        VariablePartage.TableCorrespondance[i].NouveauCodeInactif = false;
                        VariablePartage.TableCorrespondance[i].FlagReferentiel = 0;
                    }
                }
            }
        }

        public void AddFlagPreventielToTbCorrespondance()
        {
            ConnexionSQLITE connexion = new ConnexionSQLITE();
            Dictionary<string, string> columnNameToAddColumnSql = new Dictionary<string, string>
            {
                {
                    "Column1",
                    "ALTER TABLE TB$S_CorrespondanceItem ADD COLUMN FlagPreventiel INTEGER"
                }
            };

            foreach (var pair in columnNameToAddColumnSql)
            {
                string columnName = pair.Key;
                string sql = pair.Value;

                try
                {
                    connexion.ExecuteQuery(sql);
                    connexion.Dispose();
                }
                catch (System.Data.SQLite.SQLiteException e)
                {
                    Console.WriteLine(string.Format("Failed to create column [{0}]. Most likely it already exists, which is fine.", columnName));
                }
            }
        }

        //internal delegate void UpdateProgressDelegate(int ProgressPercentage);

        //internal event UpdateProgressDelegate UpdateProgress_admin;



        //internal delegate void UpdateProgressTeminateDelegate(bool terminate);

        //internal event UpdateProgressTeminateDelegate UpdateProgress_admin_Terminate;

        

        public void ExporteCorrespondance(object args)
        {
            Chargement myformchargement = (Chargement)args;
            
            ConnexionSQLServer con = new ConnexionSQLServer();

            string updateCPL1 = "Update TB$S_CorrespondanceItem set Cpl1 = '' where isnull(Cpl1,'')='' or isnull(Cpl1,'0')='0' ";
            con.ExecuteQuery(updateCPL1);

            string updateCPL2 = "Update TB$S_CorrespondanceItem set Cpl2 = '' where isnull(Cpl2,'')='' or isnull(Cpl2,'0')='0' ";
            con.ExecuteQuery(updateCPL2);


            for (int i=0;i<VariablePartage.TableCorrespondance.Count;i++)
            {
                if (VariablePartage.TableCorrespondance[i].Cpl1 == null)
                {
                    VariablePartage.TableCorrespondance[i].Cpl1 = "";
                }
                if (VariablePartage.TableCorrespondance[i].Cpl1 == "0")
                {
                    VariablePartage.TableCorrespondance[i].Cpl1 = "";
                }
                if (VariablePartage.TableCorrespondance[i].Cpl2 == null)
                {
                    VariablePartage.TableCorrespondance[i].Cpl2 = "";
                }
                if (VariablePartage.TableCorrespondance[i].Cpl2 == "0")
                {
                    VariablePartage.TableCorrespondance[i].Cpl2 = "";
                }
                if (VariablePartage.TableCorrespondance[i].Libelle_Ancien_Code == null)
                {
                    VariablePartage.TableCorrespondance[i].Libelle_Ancien_Code = "";
                }
                if (VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code == null)
                {
                    VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code = "";
                }
                if (VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code == "Non repris" && VariablePartage.TableCorrespondance[i].Nouveau_Code == null)
                {
                    VariablePartage.TableCorrespondance[i].Nouveau_Code = "";
                }
                if (VariablePartage.TableCorrespondance[i].Nouveau_Code != null && VariablePartage.TableCorrespondance[i].NomRef == "AMT")
                    Console.WriteLine(".");

                    if (VariablePartage.TableCorrespondance[i].Nouveau_Code != null)
                {
                    if(VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code == "Non repris")
                    {
                        string sqlupdate = "Update TB$S_CorrespondanceItem " +
                        "set nouveau_code = '" + VariablePartage.TableCorrespondance[i].Nouveau_Code.Replace("'", "''") + "', " +
                        "libelle_nouveau_code = '" + VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code.Replace("'", "''") + "', " +
                        "NouveauCodeInactif = '" + VariablePartage.TableCorrespondance[i].NouveauCodeInactif.ToString() + "' " +
                        "where ancien_code ='" + VariablePartage.TableCorrespondance[i].Ancien_Code.Replace("'", "''") + "' and " +
                        "ISNULL(Cpl,'') = '" + VariablePartage.TableCorrespondance[i].Cpl + "' and " +
                        "ISNULL(Cpl1,'') = '" + VariablePartage.TableCorrespondance[i].Cpl1 + "' and " +
                        "ISNULL(Cpl2,'') = '" + VariablePartage.TableCorrespondance[i].Cpl2 + "' and " +
                        "TypeRef = '" + VariablePartage.TableCorrespondance[i].TypeRef + "' and " +
                        "rtrim(ltrim(isnull(Libelle_nouveau_code,'')))!= '" + VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code.Trim().Replace("'", "''") + "'";
                        con.ExecuteQuery(sqlupdate);
                    }
                    if (VariablePartage.TableCorrespondance[i].Cpl != "50" && VariablePartage.TableCorrespondance[i].Cpl != "60")
                    {
                        string sqlupdate = "Update TB$S_CorrespondanceItem " +
                        "set nouveau_code = '" + VariablePartage.TableCorrespondance[i].Nouveau_Code.Replace("'", "''") + "', " +
                        "libelle_nouveau_code = '" + VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code.Replace("'","''") + "', " +
                        "NouveauCodeInactif = '" + VariablePartage.TableCorrespondance[i].NouveauCodeInactif.ToString() + "' "+
                        "where ancien_code ='" + VariablePartage.TableCorrespondance[i].Ancien_Code.Replace("'", "''") + "' and " +
                        "ISNULL(Cpl,'') = '" + VariablePartage.TableCorrespondance[i].Cpl + "' and " +
                        "ISNULL(Cpl1,'') = '" + VariablePartage.TableCorrespondance[i].Cpl1 + "' and " +
                        "ISNULL(Cpl2,'') = '" + VariablePartage.TableCorrespondance[i].Cpl2 + "' and " +
                        "TypeRef = '" + VariablePartage.TableCorrespondance[i].TypeRef + "' and " +
                        "rtrim(ltrim(isnull(nouveau_code,'')))!= '" + VariablePartage.TableCorrespondance[i].Nouveau_Code.Trim().Replace("'", "''") + "' and " +
                        "rtrim(ltrim(isnull(Libelle_nouveau_code,'')))!= '" + VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code.Trim().Replace("'", "''") + "'";
                        con.ExecuteQuery(sqlupdate);
                    }
                    else if (VariablePartage.TableCorrespondance[i].Cpl == "50")
                    {
                        string sqlupdate = "Update TB$S_CorrespondanceItem " +
                        "set nouveau_code = '" + VariablePartage.TableCorrespondance[i].Nouveau_Code.Replace("'", "''") + "', " +
                        "libelle_nouveau_code = '" + VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code.Replace("'", "''") + "', " +
                        "NouveauCodeInactif = '" + VariablePartage.TableCorrespondance[i].NouveauCodeInactif.ToString() + "' " +
                        "where ancien_code ='" + VariablePartage.TableCorrespondance[i].Ancien_Code.Replace("'", "''") + "' and " +
                        "ISNULL(Cpl,'') = '" + VariablePartage.TableCorrespondance[i].Cpl + "' and " +
                        //"ISNULL(Cpl1,0) = '" + Init.TableCorrespondance[i].Cpl1 + "' and " +
                        //"ISNULL(Cpl2,0) = '" + Init.TableCorrespondance[i].Cpl2 + "' and " +
                        "TypeRef = '" + VariablePartage.TableCorrespondance[i].TypeRef + "' and " +
                        "rtrim(ltrim(isnull(nouveau_code,'')))!= '" + VariablePartage.TableCorrespondance[i].Nouveau_Code.Trim().Replace("'", "''") + "' and " +
                        "rtrim(ltrim(isnull(Libelle_nouveau_code,'')))!= '" + VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code.Trim().Replace("'", "''") + "'";
                        con.ExecuteQuery(sqlupdate);
                    }
                    else if (VariablePartage.TableCorrespondance[i].Cpl == "60")
                    {
                        string sqlupdate = "Update TB$S_CorrespondanceItem " +
                        "set nouveau_code = '" + VariablePartage.TableCorrespondance[i].Nouveau_Code.Replace("'", "''") + "', " +
                        "libelle_nouveau_code = '" + VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code.Replace("'", "''") + "', " +
                        "NouveauCodeInactif = '" + VariablePartage.TableCorrespondance[i].NouveauCodeInactif.ToString() + "' " +
                        "where ancien_code ='" + VariablePartage.TableCorrespondance[i].Ancien_Code.Replace("'", "''") + "' and " +
                        "ISNULL(Cpl,'') = '" + VariablePartage.TableCorrespondance[i].Cpl + "' and " +
                        //"ISNULL(Cpl1,0) = '" + Init.TableCorrespondance[i].Cpl1 + "' and " +
                        //"ISNULL(Cpl2,0) = '" + Init.TableCorrespondance[i].Cpl2 + "' and " +
                        "TypeRef = '" + VariablePartage.TableCorrespondance[i].TypeRef + "'and " +
                        "rtrim(ltrim(isnull(nouveau_code,'')))!= '" + VariablePartage.TableCorrespondance[i].Nouveau_Code.Trim().Replace("'", "''") + "' and " +
                        "rtrim(ltrim(isnull(Libelle_nouveau_code,'')))!= '" + VariablePartage.TableCorrespondance[i].Libelle_Nouveau_Code.Trim().Replace("'", "''") + "'";
                        con.ExecuteQuery(sqlupdate);
                    }
                }
                UpdateProgress_rapprochement(i, myformchargement);
                Application.DoEvents();
                if (VariablePartage.TableCorrespondance[i].Cpl1 == "")
                {
                    VariablePartage.TableCorrespondance[i].Cpl1 = "0";
                }
                if (VariablePartage.TableCorrespondance[i].Cpl2 == "")
                {
                    VariablePartage.TableCorrespondance[i].Cpl2 = "0";
                }
            }
            UpdateProgress_rapprochement_terminate(myformchargement);
            Application.DoEvents();
            
        }

    }
}
