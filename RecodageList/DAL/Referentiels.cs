using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using RecodageList.Fonction;
using System.Linq;

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
            string CommandText = "select * from TB$S_PREVReferentiel";// where TypeItem !='PersonnePhysique'";
            //string CommandText = "select Type,TypeItem,Code,Lib,CodeOrigine,InActif,Cpl,Cpl1,Cpl2,Cpl3,DateFinValidite from TB$S_PREVReferentiel where (Type!='CTRL' and Cpl!='0') ";
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
                if (reader["Cpl"] != DBNull.Value && (string)reader["Cpl"]!="")
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
                ListRef.Add(Ref);
            }
            return (ListRef);
        }

        public List<Referentiel> FiltrerListeReferentiel_parCPL(List<Referentiel> ReferentielFULL, string CPL)
        {
            string [] CPL123 = CPL.Split('|');
            List<Referentiel> ReferentielFiltre = new List<Referentiel>();
            
            if (CPL != "135|0|0|NOMEN" /*&& CPL != "141|0|0|NOMEN"*/)
            {
                for (int i = 0; i < ReferentielFULL.Count; i++)
                {
                    //Console.WriteLine("ReferentielFULL[i].Cpl : " + ReferentielFULL[i].Cpl + ". CPL123[0] : " + CPL123[0]);
                    //Console.WriteLine("ReferentielFULL[i].Cpl1 : " + ReferentielFULL[i].Cpl1 + ". CPL123[1] : " + CPL123[1]);
                    //Console.WriteLine("ReferentielFULL[i].Cpl2 : " + ReferentielFULL[i].Cpl2 + ". CPL123[2] : " + CPL123[2]);
                    if (ReferentielFULL[i].Cpl == CPL123[0] && ReferentielFULL[i].Cpl1 == CPL123[1] && ReferentielFULL[i].Cpl2 == CPL123[2] && ReferentielFULL[i].Type == CPL123[3])
                    {
                        //Console.WriteLine("ReferentielFULL[i].Cpl : " + ReferentielFULL[i].Cpl + ". CPL123[0] : " + CPL123[0]);
                        //Console.WriteLine("ReferentielFULL[i].Cpl1 : " + ReferentielFULL[i].Cpl1 + ". CPL123[1] : " + CPL123[1]);
                        //Console.WriteLine("ReferentielFULL[i].Cpl2 : " + ReferentielFULL[i].Cpl2 + ". CPL123[2] : " + CPL123[2]);
                        ReferentielFiltre.Add(ReferentielFULL[i]);
                        //Console.WriteLine("J'ajoute la ligne " + i + " de la liste");
                    }
                    if (ReferentielFULL[i].Cpl == CPL123[0] && ReferentielFULL[i].Type == CPL123[3]
                        && CPL123[0] == "2" && CPL123[3] == "CTRL")
                    {
                        ReferentielFiltre.Add(ReferentielFULL[i]);
                    }
                }
            }
            else if (CPL == "135|0|0|NOMEN")
            {
                for (int i = 0; i < ReferentielFULL.Count; i++)
                {
                    //Console.WriteLine("CorrespFULL[i].Cpl : " + CorrespFULL[i].Cpl + ". CPL : " + CPL);
                    if ((ReferentielFULL[i].Cpl == CPL123[0] && ReferentielFULL[i].Type == CPL123[3]) 
                        || (ReferentielFULL[i].Cpl == "50" && ReferentielFULL[i].Type == "CTRL"))
                    {
                        ReferentielFiltre.Add(ReferentielFULL[i]);
                        if (ReferentielFULL[i].Cpl == "50")
                            Console.WriteLine(".");
                        //Console.WriteLine("J'ajoute la ligne " + i + " de la liste");
                    }
                }
                ReferentielFiltre = ReferentielFiltre.OrderBy(q => q.Code).ToList();
                //ListaServizi = ListaServizi.OrderBy(q => q).ToList();
            }
            return (ReferentielFiltre);
        }

        public List<Referentiel> TrierListeParLevenshtein(List <Referentiel> ReferentielFiltrerCPL,string LibelleARapprocher)
        {
            List<Referentiel> ReferentielOrderByLevenshtein = ReferentielFiltrerCPL;
            Fonc fonc = new Fonc();
            for(int i=0;i<ReferentielFiltrerCPL.Count;i++)
            {
                ReferentielOrderByLevenshtein[i].IndiceLevenshtein = fonc.LevenshteinDistance(fonc.CanonicalString(ReferentielOrderByLevenshtein[i].Lib), fonc.CanonicalString(LibelleARapprocher));
            }
            ReferentielOrderByLevenshtein.Sort(delegate (Referentiel a1, Referentiel a2) { return a1.IndiceLevenshtein - a2.IndiceLevenshtein; }); // tri par le champ i
            return (ReferentielOrderByLevenshtein);
        }

        public List<Referentiel> FiltrerListeResultatExamenReferentielParExamen(List<Referentiel> ReferentielExamenEtResultat, string Examen)
        {
            List<Referentiel> ReferentielResultatFiltre = new List<Referentiel>();
            for (int i=0;i<ReferentielExamenEtResultat.Count;i++)
            {
                string test = ReferentielExamenEtResultat[i].Cpl1;

                if (Examen!=null && ReferentielExamenEtResultat[i].Cpl1.Trim()==Examen.Trim())
                {
                    ReferentielResultatFiltre.Add(ReferentielExamenEtResultat[i]);
                }
            }
            return (ReferentielResultatFiltre);
        }

        public List<Referentiel> SupprimeResultatExamenReferentiel(List<Referentiel> ReferentielExamenEtResultat)
        {
            List<Referentiel> ReferentielSansExamen = new List<Referentiel>();
            for(int i=0; i<ReferentielExamenEtResultat.Count;i++)
            {
                if(ReferentielExamenEtResultat[i].Cpl != "50")
                {
                    ReferentielSansExamen.Add(ReferentielExamenEtResultat[i]);
                }
            }
            return ReferentielSansExamen;
        }

    }
}
