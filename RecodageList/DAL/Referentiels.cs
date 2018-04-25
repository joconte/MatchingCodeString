using System;
using System.Collections.Generic;
using System.Text;
using Finisar.SQLite;

namespace RecodageList.DAL
{
    class Referentiels
    {
        ConnexionSQLITE con = new ConnexionSQLITE();
        public List<Referentiel> ObtenirListeReferentiel_SQLITE()
        {
            con.SetConnection();
            con.sql_con.Open();
            //sql_cmd = sql_con.CreateCommand();
            //string CommandText = "select Type_Item, Ancien_Code, Libelle_Ancien_Code,AncienCodeActif,Nouveau_Code,Libelle_Nouveau_Code,Code_utilise,NomSchema,DateRecensement,DateMAJ,TypeRecodage,NouveauCodeInactif,UtilisateurCreation,TypeRef,NomRef,Cpl,Cpl1,Cpl2,Occurrence from TB$S_CorrespondanceItem";
            string CommandText = "select * from TB$S_PREVReferentiel";
            con.sql_cmd = new SQLiteCommand(CommandText, con.sql_con);
            SQLiteDataReader reader = con.sql_cmd.ExecuteReader();
            //DB = new SQLiteDataAdapter(CommandText, sql_con);
            List<Referentiel> ListRef = new List<Referentiel>();
            while (reader.Read())
            {
                Referentiel Ref = new Referentiel();
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
                if (reader["Lib"] != DBNull.Value)
                {
                    Ref.Lib = (string)reader["Lib"];
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
                if (reader["Cpl"] != DBNull.Value)
                {
                    Ref.Cpl = (string)reader["Cpl"];
                }
                if (reader["Cpl1"] != DBNull.Value)
                {
                    Ref.Cpl1 = (string)reader["Cpl1"];
                }
                if (reader["Cpl2"] != DBNull.Value)
                {
                    Ref.Cpl2 = (string)reader["Cpl2"];
                }
                if (reader["Cpl3"] != DBNull.Value)
                {
                    Ref.Cpl3 = (string)reader["Cpl3"];
                }
                if (reader["DateFinValidite"] != DBNull.Value)
                {
                    Ref.DateFinValidite = (string)reader["DateFinValidite"];
                }
                ListRef.Add(Ref);
            }
            return (ListRef);
        }

        public List<Referentiel> FiltrerListeReferentiel_parCPL(List<Referentiel> ReferentielFULL, string CPL)
        {
            List<Referentiel> ReferentielFiltre = new List<Referentiel>();
            for (int i = 0; i < ReferentielFULL.Count; i++)
            {
                //Console.WriteLine("ReferentielFULL[i].Cpl : " + ReferentielFULL[i].Cpl + ". CPL : " + CPL);
                if (ReferentielFULL[i].Cpl == CPL)
                {
                    ReferentielFiltre.Add(ReferentielFULL[i]);
                    //Console.WriteLine("J'ajoute la ligne " + i + " de la liste");
                }
            }
            return (ReferentielFiltre);
        }

    }
}
