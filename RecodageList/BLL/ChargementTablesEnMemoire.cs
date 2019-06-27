using RecodageList.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RecodageList.GUI;

namespace RecodageList.BLL
{
    class ChargementTablesEnMemoire
    {
        
        ReferentielDAL RefObject = new ReferentielDAL();
        CorrespondanceDAL CorrObject = new CorrespondanceDAL();

        public void ChargementTableEnMemoire(string cheminBaseClient,Form1 myform , Chargement myformchargement)
        {
            if (File.Exists(cheminBaseClient))
            {
                InitTable(myform, myformchargement);
                GUIFonction GUI = new GUIFonction();
                //GUI.InitComboBoxFiltre(myform);
                //myform.progressBar_admin.Value = 0;
            }
        }

        /// <summary>
        /// Procédure qui permet de charger la base de données SQLite en mémoire 
        /// </summary>
        public void InitTable(Form1 myform, Chargement myformchargement)
        {
            if (File.Exists(VariablePartage.CheminBaseClient))
            {
                //myform.progressBar_admin.Maximum = 6;
                //myform.progressBar_admin.Value = 0;


                VariablePartage.TableReferentiel = RefObject.ObtenirListeReferentiel_SQLITE();
                //myform.progressBar_admin.Value++;
                VariablePartage.TableReferentielFiltre = new List<ReferentielBLL>();
                //myform.progressBar_admin.Value++;
                //myform.progressBar_admin.Maximum = 2;
                VariablePartage.ThreadStop = false;
                VariablePartage.ThreadNumber = 0;

                
                Thread ajoutColonneFlagPreventielCorresp_Thread = new Thread(new ParameterizedThreadStart(CorrObject.AjoutColonneFlagPreventielCorresp_Thread));
                ajoutColonneFlagPreventielCorresp_Thread.Start(myformchargement);

                Thread retourneLeNombreDeLigneCorrespondance = new Thread(CorrObject.RetourneLeNombreDeLigneCorrespondance);
                retourneLeNombreDeLigneCorrespondance.Start();

                Thread obtenirListeCorrespondance_SQLITE_Thread = new Thread(new ParameterizedThreadStart(CorrObject.ObtenirListeCorrespondance_SQLITE_Thread));

                List<ReferentielBLL> p1 = (List<ReferentielBLL>)VariablePartage.TableReferentiel;
                Form1 p2 = myform;
                Chargement p3 = myformchargement;
                object args = new object[3] { p1, p2, p3 };
                obtenirListeCorrespondance_SQLITE_Thread.Start(args);

                //CorrespondanceDAL correspondanceDAL = new CorrespondanceDAL();
                //List<CorrespondanceBLL> a1 = CorrespondanceDAL.ListCorrespFlagPreventiel;
                Chargement a1 = myformchargement;

                object args2 = new object[1] { a1 };
                Thread ajoutFlagReferentielCorrespSQLite_Thread = new Thread(new ParameterizedThreadStart(CorrObject.AjoutFlagReferentielCorrespSQLite_Thread));
                ajoutFlagReferentielCorrespSQLite_Thread.Start(args2);

                //List<ReferentielBLL> b1 = CorrespondanceDAL.ListRefFlagPreventiel;
                Chargement b1 = myformchargement;
                object args3 = new object[1] { b1 };
                Thread ajoutFlagReferentielRefSQLite_Thread = new Thread(new ParameterizedThreadStart(CorrObject.AjoutFlagReferentielRefSQLite_Thread));
                ajoutFlagReferentielRefSQLite_Thread.Start(args3);

                Chargement c1 = myformchargement;
                object args4 = new object[1] { c1 };
                Thread affecteListeCorresp = new Thread(new ParameterizedThreadStart(CorrObject.AffecteListeCorresp));
                affecteListeCorresp.Start(args4);


                //CorrObject.AffecteListeCorresp
                
                //CorrObject.AjoutFlagReferentielRefSQLite_Thread
                //CorrObject.AjoutFlagReferentielCorrespSQLite_Thread

                //CorrObject.ObtenirListeCorrespondance_SQLITE_Thread

                //VariablePartage.TableCorrespondance = CorrObject.ObtenirListeCorrespondance_SQLITE(VariablePartage.TableReferentiel, myform);
                //myform.progressBar_admin.Value++;
                VariablePartage.TableCorrespondanceFiltre = new List<CorrespondanceBLL>();
                //myform.progressBar_admin.Value++;
                GUIFonction GUI = new GUIFonction();
                GUI.InitListeNomRef_admin();
                ComboBoxFiltreDAL ComboObject = new ComboBoxFiltreDAL();

                Form1 d1 = myform;
                object args5 = new object[1] { d1 };
                Thread obtenirComboBoxFiltre_Thread = new Thread(new ParameterizedThreadStart(ComboObject.ObtenirComboBoxFiltre_Thread));
                obtenirComboBoxFiltre_Thread.Start(args5);
                //VariablePartage.ComboBoxFiltre = ComboObject.ObtenirComboBoxFiltre();
                //myform.progressBar_admin.Value++;
                //VariablePartage.ClientEnCours = VariablePartage.TableCorrespondance[0].NomSchema;
            }
        }

        
    }
}
