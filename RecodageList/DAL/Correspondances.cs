using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Finisar.SQLite;


namespace RecodageList.DAL
{
    class Correspondances
    {
        ConnexionSQLITE con = new ConnexionSQLITE();
        public List<Correspondance> ObtenirListeCorrespondance_SQLITE()
        {
            con.SetConnection();
            con.sql_con.Open();
            //sql_cmd = sql_con.CreateCommand();
            //string CommandText = "select Type_Item, Ancien_Code, Libelle_Ancien_Code,AncienCodeActif,Nouveau_Code,Libelle_Nouveau_Code,Code_utilise,NomSchema,DateRecensement,DateMAJ,TypeRecodage,NouveauCodeInactif,UtilisateurCreation,TypeRef,NomRef,Cpl,Cpl1,Cpl2,Occurrence from TB$S_CorrespondanceItem";
            string CommandText = "select Ancien_Code as [C.Src], Libelle_Ancien_Code as [Intitule source],Nouveau_Code as [Code référentiel],Libelle_Nouveau_Code as [Intitulé du référentiel], Occurrence, Code_utilise,DateRecensement,NomRef,Cpl,Cpl1,Cpl2 from TB$S_CorrespondanceItem";
            con.sql_cmd = new SQLiteCommand(CommandText, con.sql_con);
            SQLiteDataReader reader = con.sql_cmd.ExecuteReader();
            //DB = new SQLiteDataAdapter(CommandText, sql_con);
            List<Correspondance> ListCorresp = new List<Correspondance>();
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
                }
                if (reader["Code référentiel"] != DBNull.Value)
                {
                    Corresp.Nouveau_Code = (string)reader["Code référentiel"];
                }
                if (reader["Intitulé du référentiel"] != DBNull.Value)
                {
                    Corresp.Libelle_Nouveau_Code = (string)reader["Intitulé du référentiel"];
                }
                if (reader["NomRef"] != DBNull.Value)
                {
                    Corresp.NomRef = (string)reader["NomRef"];
                }
                if (reader["Cpl"] != DBNull.Value)
                {
                    Corresp.Cpl = (string)reader["Cpl"];
                }
                if (reader["Cpl1"] != DBNull.Value)
                {
                    Corresp.Cpl1 = (string)reader["Cpl1"];
                }
                if (reader["Cpl2"] != DBNull.Value)
                {
                    Corresp.Cpl2 = (string)reader["Cpl2"];
                }
                ListCorresp.Add(Corresp);
            }
            return (ListCorresp);
        }

        public void InsertIntoSQLITE_TBCorrespondance(List<Correspondance> Corresp)
        {
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

        public List<Correspondance> FiltrerListeCorrespondance_parCPL(List<Correspondance> CorrespFULL,string CPL)
        {
            List<Correspondance> CorrespFiltre = new List<Correspondance>();
            for(int i=0;i<CorrespFULL.Count;i++)
            {
                //Console.WriteLine("CorrespFULL[i].Cpl : " + CorrespFULL[i].Cpl + ". CPL : " + CPL);
                if(CorrespFULL[i].Cpl == CPL)
                {
                    CorrespFiltre.Add(CorrespFULL[i]);
                    //Console.WriteLine("J'ajoute la ligne " + i + " de la liste");
                }
            }
            return (CorrespFiltre);
        }

        public void ClearTable()
        {
            string txtSQLQuery = "delete from TB$S_CorrespondanceItem";
            con.ExecuteQuery(txtSQLQuery);
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
    }
}
