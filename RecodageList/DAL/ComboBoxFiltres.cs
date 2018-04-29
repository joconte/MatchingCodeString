using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace RecodageList.DAL
{
    class ComboBoxFiltres
    {
        ConnexionSQLITE con = new ConnexionSQLITE();
        public List<ComboBoxFiltre> ObtenirComboBoxFiltre()
        {
            con.SetConnection();
            con.sql_con.Open();
            //sql_cmd = sql_con.CreateCommand();
            //string CommandText = "select Type_Item, Ancien_Code, Libelle_Ancien_Code,AncienCodeActif,Nouveau_Code,Libelle_Nouveau_Code,Code_utilise,NomSchema,DateRecensement,DateMAJ,TypeRecodage,NouveauCodeInactif,UtilisateurCreation,TypeRef,NomRef,Cpl,Cpl1,Cpl2,Occurrence from TB$S_CorrespondanceItem";
            string CommandText = "select distinct NomRef, Cpl, Cpl1, Cpl2,TypeRef from TB$S_CorrespondanceItem where NomRef is not null and Cpl not in('50','60') and NomRef!= 'PersonnePhysique' order by Nomref";
            con.sql_cmd = new SQLiteCommand(CommandText, con.sql_con);
            SQLiteDataReader reader = con.sql_cmd.ExecuteReader();
            //DB = new SQLiteDataAdapter(CommandText, sql_con);
            List<ComboBoxFiltre> ListComboBox = new List<ComboBoxFiltre>();
            while (reader.Read())
            {
                ComboBoxFiltre comboboxfiltre = new ComboBoxFiltre();
                if (reader["NomRef"] != DBNull.Value)
                {
                    comboboxfiltre.NomRef = (string)reader["NomRef"];
                }
                if (reader["Cpl"] != DBNull.Value)
                {
                    comboboxfiltre.Cpl = (string)reader["Cpl"];
                }
                if (reader["Cpl1"] != DBNull.Value && (string)reader["Cpl1"]!= "")
                {
                    comboboxfiltre.Cpl += "|"+(string)reader["Cpl1"];
                }
                else
                {
                    comboboxfiltre.Cpl += "|" + "0";
                }
                if (reader["Cpl2"] != DBNull.Value && (string)reader["Cpl2"] != "")
                {
                    comboboxfiltre.Cpl += "|"+(string)reader["Cpl2"];
                }
                else
                {
                    comboboxfiltre.Cpl += "|" + "0";
                }
                if(reader["TypeRef"] != DBNull.Value)
                {
                    comboboxfiltre.Cpl += "|" + (string)reader["TypeRef"];
                }
                ListComboBox.Add(comboboxfiltre);
            }
            return (ListComboBox);
        }

        
    }
}
