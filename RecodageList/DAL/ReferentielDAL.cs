using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using RecodageList.BLL;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace RecodageList.DAL
{
    public class ReferentielDAL
    {

        public List<ReferentielBLL> ObtenirListeReferentiel_SQLITE()
        {
            Console.WriteLine("ici");
            using (ConnexionSQLITE con = new ConnexionSQLITE())
            {
                Dictionary<string, string> columnNameToAddColumnSql = new Dictionary<string, string>
            {
                {
                    "FlagPreventiel",
                    "ALTER TABLE TB$S_PrevReferentiel ADD COLUMN FlagPreventiel INTEGER "
                }
            };

                foreach (var pair in columnNameToAddColumnSql)
                {
                    string columnName = pair.Key;
                    string sql = pair.Value;

                    try
                    {
                        con.ExecuteQuery(sql);
                        con.ExecuteQuery("Update TB$S_PrevReferentiel set FlagPreventiel = 0");
                    }
                    catch (System.Data.SQLite.SQLiteException e)
                    {
                        con.sql_con.Close();
                        Console.WriteLine(string.Format("Failed to create column [{0}]. Most likely it already exists, which is fine.", columnName));
                    }
                }


                List<ReferentielBLL> ListRef = new List<ReferentielBLL>();

                con.sql_con.Open();
                string CommandText = "select * from TB$S_PREVReferentiel";
                using (con.sql_cmd = new SQLiteCommand(CommandText, con.sql_con))
                {
                    using (SQLiteDataReader reader = con.sql_cmd.ExecuteReader())
                    {

                        Fonc fonc = new Fonc();
                        while (reader.Read())
                        {
                            ReferentielBLL Ref = new ReferentielBLL();
                            if (reader["Type"] != DBNull.Value)
                            {
                                Ref.Type = (string)reader["Type"];
                            }
                            if (reader["TypeItem"] != DBNull.Value)
                            {
                                Ref.TypeItem = (string)reader["TypeItem"];
                            }
                            if (reader["Code"] != DBNull.Value)
                            {
                                Ref.Code = (string)reader["Code"];
                            }
                            if (reader["Lib"] != DBNull.Value && (string)reader["Lib"] != "")
                            {
                                Ref.Lib = (string)reader["Lib"];
                                Ref.Canonical = fonc.CanonicalString((string)reader["Lib"]);
                            }
                            if (reader["CodeOrigine"] != DBNull.Value)
                            {
                                Ref.CodeOrigine = (string)reader["CodeOrigine"];
                            }
                            if (reader["InActif"] != DBNull.Value)
                            {
                                Ref.InActif = Convert.ToBoolean(reader["InActif"]);
                            }
                            //Console.WriteLine(reader["InActif"]);
                            //Ref.InActif = Convert.ToBoolean(Convert.ToString(reader["InActif"]));
                            if (reader["Cpl"] != DBNull.Value && (string)reader["Cpl"] != "")
                            {
                                Ref.Cpl = (string)reader["Cpl"];
                            }
                            else
                            {
                                Ref.Cpl = "0";
                            }
                            if (reader["Cpl1"] != DBNull.Value && (string)reader["Cpl1"] != "")
                            {
                                Ref.Cpl1 = (string)reader["Cpl1"];
                            }
                            else
                            {
                                Ref.Cpl1 = "0";
                            }
                            if (reader["Cpl2"] != DBNull.Value && (string)reader["Cpl2"] != "")
                            {
                                Ref.Cpl2 = (string)reader["Cpl2"];
                            }
                            else
                            {
                                Ref.Cpl2 = "0";
                            }
                            if (reader["Cpl3"] != DBNull.Value && (string)reader["Cpl3"] != "")
                            {
                                Ref.Cpl3 = (string)reader["Cpl3"];
                            }
                            else
                            {
                                Ref.Cpl3 = "0";
                            }
                            if (reader["DateFinValidite"] != DBNull.Value)
                            {
                                Ref.DateFinValidite = (string)reader["DateFinValidite"];
                            }
                            if (reader["FlagPreventiel"] != DBNull.Value)
                            {
                                Ref.FlagPreventiel = Convert.ToInt32(reader["FlagPreventiel"]);
                            }
                            ListRef.Add(Ref);
                        }
                        con.sql_con.Close();
                        con.sql_con.Dispose();
                    }
                }



                //sql_cmd = sql_con.CreateCommand();
                //string CommandText = "select Type_Item, Ancien_Code, Libelle_Ancien_Code,AncienCodeActif,Nouveau_Code,Libelle_Nouveau_Code,Code_utilise,NomSchema,DateRecensement,DateMAJ,TypeRecodage,NouveauCodeInactif,UtilisateurCreation,TypeRef,NomRef,Cpl,Cpl1,Cpl2,Occurrence from TB$S_CorrespondanceItem";
                // where TypeItem !='PersonnePhysique'";
                //string CommandText = "select Type,TypeItem,Code,Lib,CodeOrigine,InActif,Cpl,Cpl1,Cpl2,Cpl3,DateFinValidite from TB$S_PREVReferentiel where (Type!='CTRL' and Cpl!='0') ";


                //DB = new SQLiteDataAdapter(CommandText, sql_con);

                return (ListRef);
            }
        }
        
        public void InsertIntoSQLITE_TBReferentiel(List<ReferentielBLL> Referentiel)
        {
            ConnexionSQLITE con = new ConnexionSQLITE();
            for (int i = 0; i < Referentiel.Count; i++)
            {
                if(Referentiel[i].Code!=null && Referentiel[i].Code != "" && Referentiel[i].Lib != null && Referentiel[i].Lib != "")
                {
                    if(Referentiel[i].Cpl1 == "55|P03")
                    {
                        Console.WriteLine("ici!");
                    }
                    string txtSQLQuery = "insert into  TB$S_PrevReferentiel (Type,TypeItem,Code,Lib,CodeOrigine,InActif,Cpl,Cpl1,Cpl2,Cpl3,DateFinValidite,FlagPreventiel) " +
              " values ('" + Referentiel[i].Type +
              "','" + Referentiel[i].TypeItem +
              "','" + Referentiel[i].Code +
              "','" + Referentiel[i].Lib.Replace("'", "''") +
              "','" + Referentiel[i].CodeOrigine +
              "','" + Referentiel[i].InActif +
              "','" + Referentiel[i].Cpl +
              "','" + Referentiel[i].Cpl1 +
              "','" + Referentiel[i].Cpl2 +
              "','" + Referentiel[i].Cpl3 +
              "','" + Referentiel[i].DateFinValidite +
              "','" + Referentiel[i].FlagPreventiel + "')";

                    con.ExecuteQuery(txtSQLQuery);

                    //Thread insert_into_sqlite_ref = new Thread(new ParameterizedThreadStart(ThreadQuerySQLite));
                    //insert_into_sqlite_ref.Start(txtSQLQuery);
                }
            }
        }

        public void DeleteFromSQLITE_TBReferentiel(List<ReferentielBLL> Referentiel)
        {
            ConnexionSQLITE con = new ConnexionSQLITE();
            for (int i = 0; i < Referentiel.Count; i++)
            {
                if(Referentiel[i].Cpl1==null || Referentiel[i].Cpl1 == "")
                {
                    Referentiel[i].Cpl1 = "0";
                }
                if (Referentiel[i].Cpl2 == null || Referentiel[i].Cpl2 == "")
                {
                    Referentiel[i].Cpl2 = "0";
                }

                string txtSQLQuery = "delete from TB$S_PrevReferentiel "+
              " where Type like '" + Referentiel[i].Type +
              "' and Code like '" + Referentiel[i].Code +
              "' and Cpl like '" + Referentiel[i].Cpl +
              "' and IFNULL(Cpl1,'') like'" + Referentiel[i].Cpl1 +
              "' and IFNULL(Cpl2,'') like'" + Referentiel[i].Cpl2 +
              "' and FlagPreventiel like 2";

                con.ExecuteQuery(txtSQLQuery);
                //Thread delete_from_sqlite_ref = new Thread(new ParameterizedThreadStart(ThreadQuerySQLite));
                //delete_from_sqlite_ref.Start(txtSQLQuery);
            }
        }

        public void ThreadQuerySQLite(object query)
        {
            ConnexionSQLITE con = new ConnexionSQLITE();
            con.ExecuteQuery((string)query);
        }

        public void SupprimeRecodageModule_referentiel(List<ReferentielBLL> Referentiel)
        {
            ConnexionSQLITE con = new ConnexionSQLITE();
            ReferentielDAL RefObject = new ReferentielDAL();
            string txtSQLQuery = "delete from TB$S_PrevReferentiel where FlagPreventiel = 2 and Cpl = '" + Referentiel[0].Cpl + "' and Cpl1 = '" + Referentiel[0].Cpl1 + "' and Cpl2 = '" + Referentiel[0].Cpl2 + "' and Type = '" + Referentiel[0].Type + "'";
            con.ExecuteQuery(txtSQLQuery);
            con.Dispose();
            if(Referentiel[0].Cpl == "135")
            {
                string txtSQLQuery2 = "delete from TB$S_PrevReferentiel where FlagPreventiel = 2 and Cpl = '50'";
                con.ExecuteQuery(txtSQLQuery2);
                con.Dispose();
            }
            if (Referentiel[0].Cpl == "141")
            {
                string txtSQLQuery2 = "delete from TB$S_PrevReferentiel where FlagPreventiel = 2 and Cpl = '60'";
                con.ExecuteQuery(txtSQLQuery2);
                con.Dispose();
            }
            VariablePartage.TableReferentiel = RefObject.ObtenirListeReferentiel_SQLITE();
        }

        public void SupprimeRecodageFULLTABLE_referentiel()
        {
            ConnexionSQLITE con = new ConnexionSQLITE();
            string txtSQLQuery = "delete from TB$S_PrevReferentiel where FlagPreventiel = 2";
            con.ExecuteQuery(txtSQLQuery);
            con.Dispose();
            ReferentielDAL RefObject = new ReferentielDAL();
            VariablePartage.TableReferentiel = RefObject.ObtenirListeReferentiel_SQLITE();
        }

    }
}
