using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;
using RecodageList.Fonction;
using System.Linq;
using System.Windows.Forms;

namespace RecodageList.DAL
{
    class Correspondances
    {
        //ConnexionSQLITE con = new ConnexionSQLITE();
        
        public List<Correspondance> ObtenirListeCorrespondance_SQLITE(List<Referentiel> Referentiel)
        {
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
                string CommandText = "select Ancien_Code as [C.Src], Libelle_Ancien_Code as [Intitule source],Nouveau_Code as [Code référentiel],Libelle_Nouveau_Code as [Intitulé du référentiel], Occurrence, Code_utilise,DateRecensement,NomRef,NouveauCodeInactif,TypeRef,Cpl,Cpl1,Cpl2,FlagPreventiel from TB$S_CorrespondanceItem where NomRef!='PersonnePhysique' order by Occurrence desc";

                using (con.sql_cmd = new SQLiteCommand(CommandText, con.sql_con))
                {
                    con.sql_con.Open();

                    using (SQLiteDataReader reader = con.sql_cmd.ExecuteReader())
                    {
                        //DB = new SQLiteDataAdapter(CommandText, sql_con);
                        List<Correspondance> ListCorrespFlagPreventiel = new List<Correspondance>();
                        List<Correspondance> ListCorresp = new List<Correspondance>();
                        Fonc fonc = new Fonc();
                        while (reader.Read())
                        {
                            Correspondance Corresp = new Correspondance();
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
                                if (NouveauCodeEstDansLeReferentiel((string)reader["Code référentiel"], Referentiel, Corresp.Cpl, Corresp.Cpl1, Corresp.Cpl2, (string)reader["TypeRef"]))
                                {
                                    Corresp.FlagReferentiel = 1;
                                    ListCorrespFlagPreventiel.Add(Corresp);
                                    //UpdateSQLITE_TBCorrespondance_FlagPreventiel(Corresp);
                                }
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
                        for (int i = 0; i < ListCorrespFlagPreventiel.Count; i++)
                        {
                            try
                            {
                                UpdateSQLITE_TBCorrespondance_FlagPreventiel(ListCorrespFlagPreventiel[i]);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("ListCorrespFlagPreventiel[" + i + "] : " + ListCorrespFlagPreventiel[i]);
                                Console.WriteLine("Erreur : " + e.Message);
                                throw;
                            }

                        }
                        Init.TableCorrespondanceSansModification = ListCorresp;
                        con.sql_con.Close();
                        return (ListCorresp);
                    }
                       
                }
                   
            }

               
        }

        public bool NouveauCodeEstDansLeReferentiel(string Nouveau_code, List<Referentiel> Referentiel, string Cpl, string Cpl1, string Cpl2, string TypeRef)
        {
            for (int i=0; i<Referentiel.Count; i++)
            {
                if(Referentiel[i].Code.Trim() == Nouveau_code.Trim()
                    && Referentiel[i].Cpl == Cpl 
                    && Referentiel[i].Cpl1 == Cpl1 
                    && Referentiel[i].Cpl2 == Cpl2 
                    && Referentiel[i].Type == TypeRef)
                {
                    return true;
                }
                if(Referentiel[i].Code.Trim() == Nouveau_code.Trim()
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
                if (Referentiel[i].Code.Trim() == Nouveau_code.Trim()
                    && Referentiel[i].Cpl == Cpl
                    && Referentiel[i].Type == TypeRef
                    && Cpl == "125"
                    && TypeRef == "NOMEN"
                    )
                {
                    Console.WriteLine("MotifAptitude");
                }
            }
            return false;

        }

        public void InsertIntoSQLITE_TBCorrespondance(List<Correspondance> Corresp)
        {
            ConnexionSQLITE con = new ConnexionSQLITE();
            for (int i=0;i<Corresp.Count;i++)
            {
                string txtSQLQuery = "insert into  TB$S_CorrespondanceItem (Type_Item,Ancien_Code,Libelle_Ancien_Code,AncienCodeActif,Nouveau_Code,Libelle_Nouveau_Code,Occurrence,NomSchema,DateRecensement,DateMAJ,TypeRecodage,NouveauCodeInactif,UtilisateurCreation,TypeRef,NomRef,Cpl,Cpl1,Cpl2) " +
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
              "','" + Corresp[i].Cpl2 + "')";
                con.ExecuteQuery(txtSQLQuery);
            }
        }

        public void UpdateSQLITE_TBCorrespondance(Correspondance ligneAUpdater)
        {
            ConnexionSQLITE con = new ConnexionSQLITE();
            string txtSQLQuery = " update TB$S_CorrespondanceItem " +
                " set Nouveau_code = '" + ligneAUpdater.Nouveau_Code.Replace("'","''") + "'," +
                " Libelle_nouveau_code = '" + ligneAUpdater.Libelle_Nouveau_Code.Replace("'", "''") + "',"+
                " NouveauCodeInactif = '" + ligneAUpdater.NouveauCodeInactif + "'"+
                " where Ancien_Code = '" + ligneAUpdater.Ancien_Code.Replace("'", "''") + "' and " +
                //" Libelle_ancien_code = '" + ligneAUpdater.Libelle_Ancien_Code.Replace("'", "''") + "' and " +
                " NomRef = '" + ligneAUpdater.NomRef + "'";
            con.ExecuteQuery(txtSQLQuery);
            con.Dispose();
        }

        public void UpdateSQLITE_TBCorrespondance_FlagPreventiel(Correspondance ligneAUpdater)
        {
            ConnexionSQLITE con = new ConnexionSQLITE();
            try
            {
                string txtSQLQuery = " update TB$S_CorrespondanceItem " +
                " set FlagPreventiel = " + ligneAUpdater.FlagReferentiel +
                " where Ancien_Code = '" + ligneAUpdater.Ancien_Code.Replace("'", "''") + "' and " +
                //" Libelle_ancien_code = '" + ligneAUpdater.Libelle_Ancien_Code.Replace("'", "''") + "' and " +
                " NomRef = '" + ligneAUpdater.NomRef + "'";
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

        public List<Correspondance> FiltrerListeCorrespondance_parCPL(List<Correspondance> CorrespFULL,string CPL)
        {
            string[] CPL123 = CPL.Split('|');
            List<Correspondance> CorrespFiltre = new List<Correspondance>();

            if(CPL!="135|0|0|NOMEN" && CPL!="141|0|0|NOMEN")
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
            else if(CPL == "135|0|0|NOMEN")
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

        public List<Correspondance> RetourneListeCorrespondanceUpdater (List<Correspondance> CorrespNonUpdater,
            Correspondance ligneAUpdater, string NomRef)
        {
            List<Correspondance> CorrespUpdater = new List<Correspondance>();
            for (int i = 0; i < CorrespNonUpdater.Count; i++)
            {
                if (CorrespNonUpdater[i].Ancien_Code == ligneAUpdater.Ancien_Code &&
                    CorrespNonUpdater[i].Libelle_Ancien_Code == ligneAUpdater.Libelle_Ancien_Code &&
                    NomRef == ligneAUpdater.NomRef)
                {
                    CorrespNonUpdater[i].Nouveau_Code = ligneAUpdater.Nouveau_Code;
                    CorrespNonUpdater[i].Libelle_Nouveau_Code = ligneAUpdater.Libelle_Nouveau_Code;
                    CorrespNonUpdater[i].FlagReferentiel = ligneAUpdater.FlagReferentiel;
                }
            }
            CorrespUpdater = CorrespNonUpdater;
            return CorrespUpdater;
        }

        public Correspondance RetourneCorrespondanceNouveauCode(List<Correspondance> Corresp, string Ancien_Code, string ClefREF)
        {
            string[] CPL123 = ClefREF.Split('|');
            Correspondance LigneNouveauCode = new Correspondance();
            for(int i = 0;i<Corresp.Count;i++)
            {
                if(Corresp[i].Ancien_Code==Ancien_Code && Corresp[i].Cpl==CPL123[0] && Corresp[i].TypeRef == CPL123[3])
                {
                    LigneNouveauCode = Corresp[i];
                    return LigneNouveauCode;
                }
            }
            return LigneNouveauCode;
        }

        public void ClearTable()
        {
            ConnexionSQLITE con = new ConnexionSQLITE();
            string txtSQLQuery = "delete from TB$S_CorrespondanceItem";
            con.ExecuteQuery(txtSQLQuery);
            con.Dispose();
        }

        public List<Correspondance> ObtenirListeCorrespondance_lambda()
        {
            return new List<Correspondance>
            {
                new Correspondance { TypeItem = null, Ancien_Code = "Test", Libelle_Ancien_Code = "LibelleTest", AncienCodeActif = true, Nouveau_Code = "TestRecodé", Libelle_Nouveau_Code = "LibelléRecodé",Code_utilise = true,NomSchema = "Test_Client", DateRecensement = DateTime.Now, DateMAJ = DateTime.Now , TypeRecodage = null, NouveauCodeInactif = false, UtilisateurCreation = "JCO", TypeRef = "NOMEN", NomRef = "Danger", Cpl = "124", Cpl1 = null, Cpl2 = null, Occurrence = 999 },
                new Correspondance { TypeItem = null, Ancien_Code = "Test123", Libelle_Ancien_Code = "LibelleTest123", AncienCodeActif = true, Nouveau_Code = "TestRecodé123", Libelle_Nouveau_Code = "LibelléRecodé234",Code_utilise = true,NomSchema = "Test_Client456", DateRecensement = DateTime.Now, DateMAJ = DateTime.Now , TypeRecodage = null, NouveauCodeInactif = false, UtilisateurCreation = "TESTJCO", TypeRef = "NOMEN", NomRef = "Pathologie", Cpl = "123", Cpl1 = null, Cpl2 = null, Occurrence = 1999 },
                new Correspondance { TypeItem = null, Ancien_Code = "Test123", Libelle_Ancien_Code = "LibelleTest123", AncienCodeActif = true, Nouveau_Code = "TestRecodé123", Libelle_Nouveau_Code = "LibelléRecodé234",Code_utilise = true,NomSchema = "Test_Client456", DateRecensement = DateTime.Now, DateMAJ = DateTime.Now , TypeRecodage = null, NouveauCodeInactif = false, UtilisateurCreation = "TESTJCO", TypeRef = "NOMEN", NomRef = "MotifVisite", Cpl = "142", Cpl1 = null, Cpl2 = null, Occurrence = 1999 }
            };
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



      

            internal delegate void UpdateProgressDelegate(int ProgressPercentage);

        internal event UpdateProgressDelegate UpdateProgress_admin;

        internal delegate void UpdateProgressTeminateDelegate(bool terminate);

        internal event UpdateProgressTeminateDelegate UpdateProgress_admin_Terminate;

        public void ExporteCorrespondance()
        {
            ConnexionSQLServer con = new ConnexionSQLServer();

            string updateCPL1 = "Update TB$S_CorrespondanceItem set Cpl1 = 0 where isnull(Cpl1,'')='' ";
            con.ExecuteQuery(updateCPL1);

            string updateCPL2 = "Update TB$S_CorrespondanceItem set Cpl2 = 0 where isnull(Cpl2,'')='' ";
            con.ExecuteQuery(updateCPL2);


            for (int i=0;i<Init.TableCorrespondance.Count;i++)
            {
                if (Init.TableCorrespondance[i].Nouveau_Code != null && Init.TableCorrespondance[i].NomRef == "AMT")
                    Console.WriteLine(".");

                    if (Init.TableCorrespondance[i].Nouveau_Code != null)
                {
                    if (Init.TableCorrespondance[i].Cpl != "50" && Init.TableCorrespondance[i].Cpl != "60")
                    {
                        string sqlupdate = "Update TB$S_CorrespondanceItem " +
                        "set nouveau_code = '" + Init.TableCorrespondance[i].Nouveau_Code.Replace("'", "''") + "', " +
                        "libelle_nouveau_code = '" + Init.TableCorrespondance[i].Libelle_Nouveau_Code.Replace("'","''") + "', " +
                        "NouveauCodeInactif = '" + Init.TableCorrespondance[i].NouveauCodeInactif.ToString() + "' "+
                        "where ancien_code ='" + Init.TableCorrespondance[i].Ancien_Code.Replace("'", "''") + "' and " +
                        "ISNULL(Cpl,'') = '" + Init.TableCorrespondance[i].Cpl + "' and " +
                        "ISNULL(Cpl1,'') = '" + Init.TableCorrespondance[i].Cpl1 + "' and " +
                        "ISNULL(Cpl2,'') = '" + Init.TableCorrespondance[i].Cpl2 + "' and " +
                        "TypeRef = '" + Init.TableCorrespondance[i].TypeRef + "' and " +
                        "rtrim(ltrim(isnull(nouveau_code,'')))!= '" + Init.TableCorrespondance[i].Nouveau_Code.Trim().Replace("'", "''") + "' and " +
                        "rtrim(ltrim(isnull(Libelle_nouveau_code,'')))!= '" + Init.TableCorrespondance[i].Libelle_Nouveau_Code.Trim().Replace("'", "''") + "'";
                        con.ExecuteQuery(sqlupdate);
                    }
                    else if (Init.TableCorrespondance[i].Cpl == "50")
                    {
                        string sqlupdate = "Update TB$S_CorrespondanceItem " +
                        "set nouveau_code = '" + Init.TableCorrespondance[i].Nouveau_Code.Replace("'", "''") + "', " +
                        "libelle_nouveau_code = '" + Init.TableCorrespondance[i].Libelle_Nouveau_Code.Replace("'", "''") + "', " +
                        "NouveauCodeInactif = '" + Init.TableCorrespondance[i].NouveauCodeInactif.ToString() + "' " +
                        "where ancien_code ='" + Init.TableCorrespondance[i].Ancien_Code.Replace("'", "''") + "' and " +
                        "ISNULL(Cpl,'') = '" + Init.TableCorrespondance[i].Cpl + "' and " +
                        //"ISNULL(Cpl1,0) = '" + Init.TableCorrespondance[i].Cpl1 + "' and " +
                        //"ISNULL(Cpl2,0) = '" + Init.TableCorrespondance[i].Cpl2 + "' and " +
                        "TypeRef = '" + Init.TableCorrespondance[i].TypeRef + "' and " +
                        "rtrim(ltrim(isnull(nouveau_code,'')))!= '" + Init.TableCorrespondance[i].Nouveau_Code.Trim().Replace("'", "''") + "' and " +
                        "rtrim(ltrim(isnull(Libelle_nouveau_code,'')))!= '" + Init.TableCorrespondance[i].Libelle_Nouveau_Code.Trim().Replace("'", "''") + "'";
                        con.ExecuteQuery(sqlupdate);
                    }
                    else if (Init.TableCorrespondance[i].Cpl == "60")
                    {
                        string sqlupdate = "Update TB$S_CorrespondanceItem " +
                        "set nouveau_code = '" + Init.TableCorrespondance[i].Nouveau_Code.Replace("'", "''") + "', " +
                        "libelle_nouveau_code = '" + Init.TableCorrespondance[i].Libelle_Nouveau_Code.Replace("'", "''") + "', " +
                        "NouveauCodeInactif = '" + Init.TableCorrespondance[i].NouveauCodeInactif.ToString() + "' " +
                        "where ancien_code ='" + Init.TableCorrespondance[i].Ancien_Code.Replace("'", "''") + "' and " +
                        "ISNULL(Cpl,'') = '" + Init.TableCorrespondance[i].Cpl + "' and " +
                        //"ISNULL(Cpl1,0) = '" + Init.TableCorrespondance[i].Cpl1 + "' and " +
                        //"ISNULL(Cpl2,0) = '" + Init.TableCorrespondance[i].Cpl2 + "' and " +
                        "TypeRef = '" + Init.TableCorrespondance[i].TypeRef + "'and " +
                        "rtrim(ltrim(isnull(nouveau_code,'')))!= '" + Init.TableCorrespondance[i].Nouveau_Code.Trim().Replace("'", "''") + "' and " +
                        "rtrim(ltrim(isnull(Libelle_nouveau_code,'')))!= '" + Init.TableCorrespondance[i].Libelle_Nouveau_Code.Trim().Replace("'", "''") + "'";
                        con.ExecuteQuery(sqlupdate);
                    }
                }
                UpdateProgress_admin(i);
                Application.DoEvents();
            }
            UpdateProgress_admin_Terminate(true);
            Application.DoEvents();
        }

    }
}
