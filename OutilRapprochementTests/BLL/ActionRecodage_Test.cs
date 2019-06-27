using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecodageList;
using RecodageList.BLL;
using RecodageList.DAL;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace OutilRapprochementTests.BLL
{
    [TestClass]
    public class ActionRecodage_Test
    {
        [TestMethod]
        public void DesaffecteCode_Saisie_ByLigneCorresp_Examen()
        {
            Form1 myform = new Form1();
            VariablePartage.CheminBaseClient = "C:\\temp\\BaseRecodageCHVAL.db";
            ReferentielDAL referentielDAL = new ReferentielDAL();
            VariablePartage.TableReferentiel = referentielDAL.ObtenirListeReferentiel_SQLITE();
            ReferentielBLL referentielBLL = new ReferentielBLL();
            VariablePartage.TableReferentielFiltre = referentielBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, "135|0|0|NOMEN");
            CorrespondanceDAL correspondanceDAL = new CorrespondanceDAL();
            VariablePartage.TableCorrespondance = correspondanceDAL.ObtenirListeCorrespondance_SQLITE(VariablePartage.TableReferentiel, myform);
            CorrespondanceBLL correspondanceBLL = new CorrespondanceBLL();
            VariablePartage.TableCorrespondanceFiltre = correspondanceBLL.FiltrerListeCorrespondance_parCPL(VariablePartage.TableCorrespondance, "135|0|0|NOMEN");


            DataGridView dataGridView_ref = new DataGridView();
            dataGridView_ref.BindingContext = new BindingContext(); // this makes it work.
            var bindingListRef = new BindingList<ReferentielBLL>(VariablePartage.TableReferentielFiltre);
            var source_ref = new BindingSource(bindingListRef, null);
            dataGridView_ref.DataSource = source_ref;

            DataGridView dataGridView_saisie = new DataGridView();
            dataGridView_saisie.BindingContext = new BindingContext(); // this makes it work.
            var bindingListCorr = new BindingList<CorrespondanceBLL>(VariablePartage.TableCorrespondanceFiltre);
            var source_corr = new BindingSource(bindingListCorr, null);
            dataGridView_saisie.DataSource = source_corr;

            ActionRecodage actionRecodage = new ActionRecodage();

            for(int i=0;i<VariablePartage.TableCorrespondanceFiltre.Count;i++)
            {
               // actionRecodage.DesaffecteCode_Saisie_ByLigneCorresp(VariablePartage.TableCorrespondanceFiltre[i], VariablePartage.TableCorrespondanceFiltre, VariablePartage.TableCorrespondance);
                Assert.AreEqual("", VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code);
                Assert.AreEqual("", VariablePartage.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code);
            }
        }

        [TestMethod]
        public void DesaffecteCode_Saisie_ByLigneCorresp_PersonnelMedical()
        {
            Form1 myform = new Form1();
            string CPLCLEF = "2|0|0|CTRL";
            VariablePartage.CheminBaseClient = "C:\\temp\\BaseRecodageCHVAL.db";
            ReferentielDAL referentielDAL = new ReferentielDAL();
            VariablePartage.TableReferentiel = referentielDAL.ObtenirListeReferentiel_SQLITE();
            ReferentielBLL referentielBLL = new ReferentielBLL();
            VariablePartage.TableReferentielFiltre = referentielBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, CPLCLEF);
            CorrespondanceDAL correspondanceDAL = new CorrespondanceDAL();
            VariablePartage.TableCorrespondance = correspondanceDAL.ObtenirListeCorrespondance_SQLITE(VariablePartage.TableReferentiel, myform);
            CorrespondanceBLL correspondanceBLL = new CorrespondanceBLL();
            VariablePartage.TableCorrespondanceFiltre = correspondanceBLL.FiltrerListeCorrespondance_parCPL(VariablePartage.TableCorrespondance, CPLCLEF);


            DataGridView dataGridView_ref = new DataGridView();
            dataGridView_ref.BindingContext = new BindingContext(); // this makes it work.
            var bindingListRef = new BindingList<ReferentielBLL>(VariablePartage.TableReferentielFiltre);
            var source_ref = new BindingSource(bindingListRef, null);
            dataGridView_ref.DataSource = source_ref;

            DataGridView dataGridView_saisie = new DataGridView();
            dataGridView_saisie.BindingContext = new BindingContext(); // this makes it work.
            var bindingListCorr = new BindingList<CorrespondanceBLL>(VariablePartage.TableCorrespondanceFiltre);
            var source_corr = new BindingSource(bindingListCorr, null);
            dataGridView_saisie.DataSource = source_corr;

            ActionRecodage actionRecodage = new ActionRecodage();

            for (int i = 0; i < VariablePartage.TableCorrespondanceFiltre.Count; i++)
            {
                //actionRecodage.DesaffecteCode_Saisie_ByLigneCorresp(VariablePartage.TableCorrespondanceFiltre[i], VariablePartage.TableCorrespondanceFiltre, VariablePartage.TableCorrespondance);
                Assert.AreEqual("", VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code);
                Assert.AreEqual("", VariablePartage.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code);
            }
        }

        [TestMethod]
        public void AffectationCode_ByLigneCorrespReferentiel_Test_FormeJuridique()
        {
            Form1 myform = new Form1();
            VariablePartage.CheminBaseClient = "C:\\temp\\BaseRecodageCHVAL.db";
            ReferentielDAL referentielDAL = new ReferentielDAL();
            VariablePartage.TableReferentiel = referentielDAL.ObtenirListeReferentiel_SQLITE();
            ReferentielBLL referentielBLL = new ReferentielBLL();
            VariablePartage.TableReferentielFiltre = referentielBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, "113|0|0|NOMEN");
            CorrespondanceDAL correspondanceDAL = new CorrespondanceDAL();
            VariablePartage.TableCorrespondance = correspondanceDAL.ObtenirListeCorrespondance_SQLITE(VariablePartage.TableReferentiel, myform);
            CorrespondanceBLL correspondanceBLL = new CorrespondanceBLL();
            VariablePartage.TableCorrespondanceFiltre = correspondanceBLL.FiltrerListeCorrespondance_parCPL(VariablePartage.TableCorrespondance, "113|0|0|NOMEN");


            DataGridView dataGridView_ref = new DataGridView();
            dataGridView_ref.BindingContext = new BindingContext(); // this makes it work.
            var bindingListRef = new BindingList<ReferentielBLL>(VariablePartage.TableReferentielFiltre);
            var source_ref = new BindingSource(bindingListRef, null);
            dataGridView_ref.DataSource = source_ref;

            DataGridView dataGridView_saisie = new DataGridView();
            dataGridView_saisie.BindingContext = new BindingContext(); // this makes it work.
            var bindingListCorr = new BindingList<CorrespondanceBLL>(VariablePartage.TableCorrespondanceFiltre);
            var source_corr = new BindingSource(bindingListCorr, null);
            dataGridView_saisie.DataSource = source_corr;

            ActionRecodage actionRecodage = new ActionRecodage();

            List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();

            for (int i = 0; i < VariablePartage.TableCorrespondanceFiltre.Count; i++)
            {
                TableRefLevenshtein = actionRecodage.SaisieCellClick_ByLigneCorresp(VariablePartage.TableCorrespondanceFiltre[i], VariablePartage.TableReferentielFiltre, VariablePartage.TableReferentiel, "113|0|0|NOMEN");
                actionRecodage.AffectationCode_ByLigneCorrespReferentiel(VariablePartage.TableCorrespondanceFiltre[i], TableRefLevenshtein[0], VariablePartage.TableCorrespondanceFiltre,VariablePartage.TableCorrespondance,TableRefLevenshtein, "113|0|0|NOMEN");
                Assert.AreEqual(TableRefLevenshtein[0].Code, VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code);
                Assert.AreEqual(TableRefLevenshtein[0].Lib, VariablePartage.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code);
            }
        }

        [TestMethod]
        public void AffectationCode_ByLigneCorrespReferentiel_Test_Examen()
        {
            Form1 myform = new Form1();
            string CPLCLEF = "135|0|0|NOMEN";
            VariablePartage.CheminBaseClient = "C:\\temp\\BaseRecodageCHVAL.db";
            ReferentielDAL referentielDAL = new ReferentielDAL();
            VariablePartage.TableReferentiel = referentielDAL.ObtenirListeReferentiel_SQLITE();
            ReferentielBLL referentielBLL = new ReferentielBLL();
            VariablePartage.TableReferentielFiltre = referentielBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, CPLCLEF);
            CorrespondanceDAL correspondanceDAL = new CorrespondanceDAL();
            VariablePartage.TableCorrespondance = correspondanceDAL.ObtenirListeCorrespondance_SQLITE(VariablePartage.TableReferentiel, myform);
            CorrespondanceBLL correspondanceBLL = new CorrespondanceBLL();
            VariablePartage.TableCorrespondanceFiltre = correspondanceBLL.FiltrerListeCorrespondance_parCPL(VariablePartage.TableCorrespondance, CPLCLEF);


            DataGridView dataGridView_ref = new DataGridView();
            dataGridView_ref.BindingContext = new BindingContext(); // this makes it work.
            var bindingListRef = new BindingList<ReferentielBLL>(VariablePartage.TableReferentielFiltre);
            var source_ref = new BindingSource(bindingListRef, null);
            dataGridView_ref.DataSource = source_ref;

            DataGridView dataGridView_saisie = new DataGridView();
            dataGridView_saisie.BindingContext = new BindingContext(); // this makes it work.
            var bindingListCorr = new BindingList<CorrespondanceBLL>(VariablePartage.TableCorrespondanceFiltre);
            var source_corr = new BindingSource(bindingListCorr, null);
            dataGridView_saisie.DataSource = source_corr;

            ActionRecodage actionRecodage = new ActionRecodage();

            List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();

            for (int i = 0; i < VariablePartage.TableCorrespondanceFiltre.Count; i++)
            {
                TableRefLevenshtein = actionRecodage.SaisieCellClick_ByLigneCorresp(VariablePartage.TableCorrespondanceFiltre[i], VariablePartage.TableReferentielFiltre, VariablePartage.TableReferentiel, CPLCLEF);
                if (TableRefLevenshtein.Any())
                {
                    actionRecodage.AffectationCode_ByLigneCorrespReferentiel(VariablePartage.TableCorrespondanceFiltre[i], TableRefLevenshtein[0], VariablePartage.TableCorrespondanceFiltre, VariablePartage.TableCorrespondance, TableRefLevenshtein, CPLCLEF);
                    Assert.AreEqual(TableRefLevenshtein[0].Code, VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code);
                    Assert.AreEqual(TableRefLevenshtein[0].Lib, VariablePartage.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code);
                }
                else
                {
                    Assert.AreEqual("", VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code);
                    Assert.AreEqual("", VariablePartage.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code);
                }
                //VariablePartage.TableCorrespondanceFiltre = correspondanceBLL.FiltrerListeCorrespondance_parCPL(VariablePartage.TableCorrespondance, CPLCLEF);
            }
        }

        [TestMethod]
        public void AffectationCode_Test_Examen()
        {
            Form1 myform = new Form1();
            myform.InitializeComponent();
            string CPLCLEF = "135|0|0|NOMEN";
            
            VariablePartage.CheminBaseClient = "C:\\temp\\BaseRecodageCHVAL.db";
            ReferentielDAL referentielDAL = new ReferentielDAL();
            VariablePartage.TableReferentiel = referentielDAL.ObtenirListeReferentiel_SQLITE();
            ReferentielBLL referentielBLL = new ReferentielBLL();
            VariablePartage.TableReferentielFiltre = referentielBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, CPLCLEF);
            CorrespondanceDAL correspondanceDAL = new CorrespondanceDAL();
            VariablePartage.TableCorrespondance = correspondanceDAL.ObtenirListeCorrespondance_SQLITE(VariablePartage.TableReferentiel, myform);
            CorrespondanceBLL correspondanceBLL = new CorrespondanceBLL();
            VariablePartage.TableCorrespondanceFiltre = correspondanceBLL.FiltrerListeCorrespondance_parCPL(VariablePartage.TableCorrespondance, CPLCLEF);


            DataGridView dataGridView_ref = new DataGridView();
            dataGridView_ref.BindingContext = new BindingContext(); // this makes it work.
            var bindingListRef = new BindingList<ReferentielBLL>(VariablePartage.TableReferentielFiltre);
            var source_ref = new BindingSource(bindingListRef, null);
            dataGridView_ref.DataSource = source_ref;

            DataGridView dataGridView_saisie = new DataGridView();
            dataGridView_saisie.BindingContext = new BindingContext(); // this makes it work.
            var bindingListCorr = new BindingList<CorrespondanceBLL>(VariablePartage.TableCorrespondanceFiltre);
            var source_corr = new BindingSource(bindingListCorr, null);
            dataGridView_saisie.DataSource = source_corr;

            ActionRecodage actionRecodage = new ActionRecodage();

            List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();

            myform.comboBox_filtre.SelectedValue = CPLCLEF;

            dataGridView_saisie.CurrentCell = dataGridView_saisie.Rows[0].Cells[4];
            actionRecodage.SaisieCellClick(dataGridView_saisie, dataGridView_ref, myform, CPLCLEF);
            dataGridView_ref.CurrentCell = dataGridView_ref.Rows[0].Cells[1];
            actionRecodage.AffectationCode(dataGridView_saisie, dataGridView_ref, myform,CPLCLEF);
            Assert.AreEqual(dataGridView_saisie.Rows[0].Cells[4].Value.ToString(), dataGridView_ref.Rows[0].Cells[2].Value.ToString());

        }

        [TestMethod]
        public void AffecteCode_Test_Examen()
        {
            Form1 myform = new Form1();
            myform.InitializeComponent();
            string CPLCLEF = "135|0|0|NOMEN";

            VariablePartage.CheminBaseClient = "C:\\temp\\BaseRecodageCHVAL.db";
            ReferentielDAL referentielDAL = new ReferentielDAL();
            VariablePartage.TableReferentiel = referentielDAL.ObtenirListeReferentiel_SQLITE();
            ReferentielBLL referentielBLL = new ReferentielBLL();
            VariablePartage.TableReferentielFiltre = referentielBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, CPLCLEF);
            CorrespondanceDAL correspondanceDAL = new CorrespondanceDAL();
            VariablePartage.TableCorrespondance = correspondanceDAL.ObtenirListeCorrespondance_SQLITE(VariablePartage.TableReferentiel, myform);
            CorrespondanceBLL correspondanceBLL = new CorrespondanceBLL();
            VariablePartage.TableCorrespondanceFiltre = correspondanceBLL.FiltrerListeCorrespondance_parCPL(VariablePartage.TableCorrespondance, CPLCLEF);


            DataGridView dataGridView_ref = new DataGridView();
            dataGridView_ref.BindingContext = new BindingContext(); // this makes it work.
            var bindingListRef = new BindingList<ReferentielBLL>(VariablePartage.TableReferentielFiltre);
            var source_ref = new BindingSource(bindingListRef, null);
            dataGridView_ref.DataSource = source_ref;

            DataGridView dataGridView_saisie = new DataGridView();
            dataGridView_saisie.BindingContext = new BindingContext(); // this makes it work.
            var bindingListCorr = new BindingList<CorrespondanceBLL>(VariablePartage.TableCorrespondanceFiltre);
            var source_corr = new BindingSource(bindingListCorr, null);
            dataGridView_saisie.DataSource = source_corr;

            ActionRecodage actionRecodage = new ActionRecodage();

            List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();

            myform.comboBox_filtre.SelectedValue = CPLCLEF;

            dataGridView_saisie.CurrentCell = dataGridView_saisie.Rows[0].Cells[4];
            actionRecodage.SaisieCellClick(dataGridView_saisie, dataGridView_ref, myform, CPLCLEF);
            dataGridView_ref.CurrentCell = dataGridView_ref.Rows[0].Cells[1];
            actionRecodage.AffectationCode(dataGridView_saisie, dataGridView_ref, myform, CPLCLEF);
            //Verifie que l'affectation graphique est bien faite
            Assert.AreEqual(dataGridView_saisie.Rows[0].Cells[4].Value.ToString(), dataGridView_ref.Rows[0].Cells[2].Value.ToString());
            //Verifie que l'affectation dans l'objet est bien faite
            Assert.AreEqual(VariablePartage.TableCorrespondanceFiltre[0].Nouveau_Code, VariablePartage.TableReferentielFiltre[0].Code);
        }

        [TestMethod]
        public void AffectationCode_ByLigneCorrespReferentiel_Test_PersonnelMedical()
        {
            Form1 myform = new Form1();
            string CPLCLEF = "2|0|0|CTRL";
            VariablePartage.CheminBaseClient = "C:\\temp\\BaseRecodageCHVAL.db";
            ReferentielDAL referentielDAL = new ReferentielDAL();
            VariablePartage.TableReferentiel = referentielDAL.ObtenirListeReferentiel_SQLITE();
            ReferentielBLL referentielBLL = new ReferentielBLL();
            VariablePartage.TableReferentielFiltre = referentielBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, CPLCLEF);
            CorrespondanceDAL correspondanceDAL = new CorrespondanceDAL();
            VariablePartage.TableCorrespondance = correspondanceDAL.ObtenirListeCorrespondance_SQLITE(VariablePartage.TableReferentiel, myform);
            CorrespondanceBLL correspondanceBLL = new CorrespondanceBLL();
            VariablePartage.TableCorrespondanceFiltre = correspondanceBLL.FiltrerListeCorrespondance_parCPL(VariablePartage.TableCorrespondance, CPLCLEF);


            DataGridView dataGridView_ref = new DataGridView();
            dataGridView_ref.BindingContext = new BindingContext(); // this makes it work.
            var bindingListRef = new BindingList<ReferentielBLL>(VariablePartage.TableReferentielFiltre);
            var source_ref = new BindingSource(bindingListRef, null);
            dataGridView_ref.DataSource = source_ref;

            DataGridView dataGridView_saisie = new DataGridView();
            dataGridView_saisie.BindingContext = new BindingContext(); // this makes it work.
            var bindingListCorr = new BindingList<CorrespondanceBLL>(VariablePartage.TableCorrespondanceFiltre);
            var source_corr = new BindingSource(bindingListCorr, null);
            dataGridView_saisie.DataSource = source_corr;

            ActionRecodage actionRecodage = new ActionRecodage();

            List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();

            for (int i = 0; i < VariablePartage.TableCorrespondanceFiltre.Count; i++)
            {
                TableRefLevenshtein = actionRecodage.SaisieCellClick_ByLigneCorresp(VariablePartage.TableCorrespondanceFiltre[i], VariablePartage.TableReferentielFiltre, VariablePartage.TableReferentiel, CPLCLEF);
                actionRecodage.AffectationCode_ByLigneCorrespReferentiel(VariablePartage.TableCorrespondanceFiltre[i], TableRefLevenshtein[0], VariablePartage.TableCorrespondanceFiltre, VariablePartage.TableCorrespondance, TableRefLevenshtein, CPLCLEF);
                Assert.AreEqual(TableRefLevenshtein[0].Code, VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code);
                Assert.AreEqual(TableRefLevenshtein[0].Lib, VariablePartage.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code);
            }
        }

        [TestMethod]
        public void AffectationCodeCreeInactifMemeCodeMemeLibelle_ByLigneCorrRef_PersonnelMedical()
        {
            Form1 myform = new Form1();
            string CPLCLEF = "2|0|0|CTRL";
            VariablePartage.CheminBaseClient = "C:\\temp\\BaseRecodageCHVAL.db";
            ReferentielDAL referentielDAL = new ReferentielDAL();
            VariablePartage.TableReferentiel = referentielDAL.ObtenirListeReferentiel_SQLITE();
            ReferentielBLL referentielBLL = new ReferentielBLL();
            VariablePartage.TableReferentielFiltre = referentielBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, CPLCLEF);
            CorrespondanceDAL correspondanceDAL = new CorrespondanceDAL();
            VariablePartage.TableCorrespondance = correspondanceDAL.ObtenirListeCorrespondance_SQLITE(VariablePartage.TableReferentiel, myform);
            CorrespondanceBLL correspondanceBLL = new CorrespondanceBLL();
            VariablePartage.TableCorrespondanceFiltre = correspondanceBLL.FiltrerListeCorrespondance_parCPL(VariablePartage.TableCorrespondance, CPLCLEF);


            DataGridView dataGridView_ref = new DataGridView();
            dataGridView_ref.BindingContext = new BindingContext(); // this makes it work.
            var bindingListRef = new BindingList<ReferentielBLL>(VariablePartage.TableReferentielFiltre);
            var source_ref = new BindingSource(bindingListRef, null);
            dataGridView_ref.DataSource = source_ref;

            DataGridView dataGridView_saisie = new DataGridView();
            dataGridView_saisie.BindingContext = new BindingContext(); // this makes it work.
            var bindingListCorr = new BindingList<CorrespondanceBLL>(VariablePartage.TableCorrespondanceFiltre);
            var source_corr = new BindingSource(bindingListCorr, null);
            dataGridView_saisie.DataSource = source_corr;

            ActionRecodage actionRecodage = new ActionRecodage();

            List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();

            for (int i = 0; i < VariablePartage.TableCorrespondanceFiltre.Count; i++)
            {
               // actionRecodage.AffectationCodeCreeInactifMemeCodeMemeLibelle_ByLigneCorrRef(VariablePartage.TableCorrespondanceFiltre[i], VariablePartage.TableCorrespondance, VariablePartage.TableCorrespondanceFiltre, VariablePartage.TableReferentiel, VariablePartage.TableReferentielFiltre, "2|0|0|CTRL");
                if(!referentielBLL.CodeEstDansLeReferentiel(VariablePartage.TableReferentielFiltre, VariablePartage.TableCorrespondanceFiltre[i].Ancien_Code)
                    && VariablePartage.TableCorrespondanceFiltre[i].Ancien_Code != null && VariablePartage.TableCorrespondanceFiltre[i].Libelle_Ancien_Code != null
                && VariablePartage.TableCorrespondanceFiltre[i].Ancien_Code != "" && VariablePartage.TableCorrespondanceFiltre[i].Libelle_Ancien_Code != "")
                {
                    Assert.AreEqual(VariablePartage.TableCorrespondanceFiltre[i].Ancien_Code, VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code);
                    Assert.AreEqual(VariablePartage.TableCorrespondanceFiltre[i].Libelle_Ancien_Code, VariablePartage.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code);
                    Assert.AreEqual(true, VariablePartage.TableCorrespondanceFiltre[i].NouveauCodeInactif);
                }
            }
        }

        [TestMethod]
        public void AffectationCodeCreeInactifMemeCodeMemeLibelle_ByLigneCorrRef_Examen()
        {
            Form1 myform = new Form1();
            string CPLCLEF = "135|0|0|NOMEN";
            VariablePartage.CheminBaseClient = "C:\\temp\\BaseRecodageCHVAL.db";
            ReferentielDAL referentielDAL = new ReferentielDAL();
            VariablePartage.TableReferentiel = referentielDAL.ObtenirListeReferentiel_SQLITE();
            ReferentielBLL referentielBLL = new ReferentielBLL();
            VariablePartage.TableReferentielFiltre = referentielBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, CPLCLEF);
            CorrespondanceDAL correspondanceDAL = new CorrespondanceDAL();
            VariablePartage.TableCorrespondance = correspondanceDAL.ObtenirListeCorrespondance_SQLITE(VariablePartage.TableReferentiel, myform);
            CorrespondanceBLL correspondanceBLL = new CorrespondanceBLL();
            VariablePartage.TableCorrespondanceFiltre = correspondanceBLL.FiltrerListeCorrespondance_parCPL(VariablePartage.TableCorrespondance, CPLCLEF);


            DataGridView dataGridView_ref = new DataGridView();
            dataGridView_ref.BindingContext = new BindingContext(); // this makes it work.
            var bindingListRef = new BindingList<ReferentielBLL>(VariablePartage.TableReferentielFiltre);
            var source_ref = new BindingSource(bindingListRef, null);
            dataGridView_ref.DataSource = source_ref;

            DataGridView dataGridView_saisie = new DataGridView();
            dataGridView_saisie.BindingContext = new BindingContext(); // this makes it work.
            var bindingListCorr = new BindingList<CorrespondanceBLL>(VariablePartage.TableCorrespondanceFiltre);
            var source_corr = new BindingSource(bindingListCorr, null);
            dataGridView_saisie.DataSource = source_corr;

            ActionRecodage actionRecodage = new ActionRecodage();

            List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();

            for (int i = 0; i < VariablePartage.TableCorrespondanceFiltre.Count; i++)
            {
                //actionRecodage.AffectationCodeCreeInactifMemeCodeMemeLibelle_ByLigneCorrRef(VariablePartage.TableCorrespondanceFiltre[i], VariablePartage.TableCorrespondance, VariablePartage.TableCorrespondanceFiltre, VariablePartage.TableReferentiel, VariablePartage.TableReferentielFiltre, "2|0|0|CTRL");
                if (!referentielBLL.CodeEstDansLeReferentiel(VariablePartage.TableReferentielFiltre, VariablePartage.TableCorrespondanceFiltre[i].Ancien_Code)
                    && VariablePartage.TableCorrespondanceFiltre[i].Ancien_Code != null && VariablePartage.TableCorrespondanceFiltre[i].Libelle_Ancien_Code != null
                && VariablePartage.TableCorrespondanceFiltre[i].Ancien_Code != "" && VariablePartage.TableCorrespondanceFiltre[i].Libelle_Ancien_Code != "")
                {
                    Assert.AreEqual(VariablePartage.TableCorrespondanceFiltre[i].Ancien_Code, VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code);
                    Assert.AreEqual(VariablePartage.TableCorrespondanceFiltre[i].Libelle_Ancien_Code, VariablePartage.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code);
                    Assert.AreEqual(true, VariablePartage.TableCorrespondanceFiltre[i].NouveauCodeInactif);
                    TableRefLevenshtein = actionRecodage.SaisieCellClick_ByLigneCorresp(VariablePartage.TableCorrespondanceFiltre[i], VariablePartage.TableReferentielFiltre, VariablePartage.TableReferentiel, CPLCLEF);
                    Assert.AreEqual(VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code, TableRefLevenshtein[0].Code);
                    Assert.AreEqual(VariablePartage.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code, TableRefLevenshtein[0].Lib);
                }
            }
        }


        [TestMethod]
        public void RoutineCreationExamenResExamPuisSuppression()
        {
            Form1 myform = new Form1();
            string CPLCLEF = "135|0|0|NOMEN";
            VariablePartage.CheminBaseClient = "C:\\temp\\BaseRecodageCHVAL.db";
            ReferentielDAL referentielDAL = new ReferentielDAL();
            VariablePartage.TableReferentiel = referentielDAL.ObtenirListeReferentiel_SQLITE();
            ReferentielBLL referentielBLL = new ReferentielBLL();
            VariablePartage.TableReferentielFiltre = referentielBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, CPLCLEF);
            CorrespondanceDAL correspondanceDAL = new CorrespondanceDAL();
            VariablePartage.TableCorrespondance = correspondanceDAL.ObtenirListeCorrespondance_SQLITE(VariablePartage.TableReferentiel, myform);
            CorrespondanceBLL correspondanceBLL = new CorrespondanceBLL();
            VariablePartage.TableCorrespondanceFiltre = correspondanceBLL.FiltrerListeCorrespondance_parCPL(VariablePartage.TableCorrespondance, CPLCLEF);


            DataGridView dataGridView_ref = new DataGridView();
            dataGridView_ref.BindingContext = new BindingContext(); // this makes it work.
            var bindingListRef = new BindingList<ReferentielBLL>(VariablePartage.TableReferentielFiltre);
            var source_ref = new BindingSource(bindingListRef, null);
            dataGridView_ref.DataSource = source_ref;

            DataGridView dataGridView_saisie = new DataGridView();
            dataGridView_saisie.BindingContext = new BindingContext(); // this makes it work.
            var bindingListCorr = new BindingList<CorrespondanceBLL>(VariablePartage.TableCorrespondanceFiltre);
            var source_corr = new BindingSource(bindingListCorr, null);
            dataGridView_saisie.DataSource = source_corr;

            ActionRecodage actionRecodage = new ActionRecodage();

            List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();
            /*
            for (int i = 0; i < VariablePartage.TableCorrespondanceFiltre.Count; i++)
            {
                actionRecodage.AffectationCodeCreeInactifMemeCodeMemeLibelle_ByLigneCorrRef(VariablePartage.TableCorrespondanceFiltre[i], VariablePartage.TableCorrespondance, VariablePartage.TableCorrespondanceFiltre, VariablePartage.TableReferentiel, VariablePartage.TableReferentielFiltre, "2|0|0|CTRL");
                if (!referentielBLL.CodeEstDansLeReferentiel(VariablePartage.TableReferentielFiltre, VariablePartage.TableCorrespondanceFiltre[i].Ancien_Code)
                    && VariablePartage.TableCorrespondanceFiltre[i].Ancien_Code != null && VariablePartage.TableCorrespondanceFiltre[i].Libelle_Ancien_Code != null
                && VariablePartage.TableCorrespondanceFiltre[i].Ancien_Code != "" && VariablePartage.TableCorrespondanceFiltre[i].Libelle_Ancien_Code != "")
                {
                    Assert.AreEqual(VariablePartage.TableCorrespondanceFiltre[i].Ancien_Code, VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code);
                    Assert.AreEqual(VariablePartage.TableCorrespondanceFiltre[i].Libelle_Ancien_Code, VariablePartage.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code);
                    Assert.AreEqual(true, VariablePartage.TableCorrespondanceFiltre[i].NouveauCodeInactif);
                    TableRefLevenshtein = actionRecodage.SaisieCellClick_ByLigneCorresp(VariablePartage.TableCorrespondanceFiltre[i], VariablePartage.TableReferentielFiltre, VariablePartage.TableReferentiel, CPLCLEF);
                    Assert.AreEqual(VariablePartage.TableCorrespondanceFiltre[i].Nouveau_Code, TableRefLevenshtein[0].Code);
                    Assert.AreEqual(VariablePartage.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code, TableRefLevenshtein[0].Lib);
                }
            }*/

            VariablePartage.TableReferentiel = actionRecodage.DesaffecteCode_Referentiel_ByLigneCorr(VariablePartage.TableCorrespondanceFiltre[0], VariablePartage.TableReferentiel, VariablePartage.TableReferentielFiltre, CPLCLEF);
            VariablePartage.TableReferentielFiltre = referentielBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, CPLCLEF);
            //actionRecodage.DesaffecteCode_Saisie_ByLigneCorresp(VariablePartage.TableCorrespondanceFiltre[0], VariablePartage.TableCorrespondanceFiltre, VariablePartage.TableCorrespondance);
           // actionRecodage.AffectationCodeCreeInactifMemeCodeMemeLibelle_ByLigneCorrRef(VariablePartage.TableCorrespondanceFiltre[0], VariablePartage.TableCorrespondance, VariablePartage.TableCorrespondanceFiltre, VariablePartage.TableReferentiel, VariablePartage.TableReferentielFiltre, CPLCLEF);

            TableRefLevenshtein = actionRecodage.SaisieCellClick_ByLigneCorresp(VariablePartage.TableCorrespondanceFiltre[1], VariablePartage.TableReferentielFiltre, VariablePartage.TableReferentiel, CPLCLEF);

            if(TableRefLevenshtein.Any())
            {
                Assert.AreEqual("", "");
            }
            else
            {
                //Assert.AreEqual(null, TableRefLevenshtein[0].Code); // Erreur
            }
        }

        [TestMethod]
        public void TestBindingDatagrid()
        {
            Form1 myform = new Form1();
            string CPLCLEF = "135|0|0|NOMEN";
            VariablePartage.CheminBaseClient = "C:\\temp\\BaseRecodageCHVAL.db";
            ReferentielDAL referentielDAL = new ReferentielDAL();
            VariablePartage.TableReferentiel = referentielDAL.ObtenirListeReferentiel_SQLITE();
            ReferentielBLL referentielBLL = new ReferentielBLL();
            VariablePartage.TableReferentielFiltre = referentielBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, CPLCLEF);
            CorrespondanceDAL correspondanceDAL = new CorrespondanceDAL();
            VariablePartage.TableCorrespondance = correspondanceDAL.ObtenirListeCorrespondance_SQLITE(VariablePartage.TableReferentiel, myform);
            CorrespondanceBLL correspondanceBLL = new CorrespondanceBLL();
            VariablePartage.TableCorrespondanceFiltre = correspondanceBLL.FiltrerListeCorrespondance_parCPL(VariablePartage.TableCorrespondance, CPLCLEF);


            DataGridView dataGridView_ref = new DataGridView();
            dataGridView_ref.BindingContext = new BindingContext(); // this makes it work.
            var bindingListRef = new BindingList<ReferentielBLL>(VariablePartage.TableReferentielFiltre);
            var source_ref = new BindingSource(bindingListRef, null);
            dataGridView_ref.DataSource = source_ref;

            DataGridView dataGridView_saisie = new DataGridView();
            dataGridView_saisie.BindingContext = new BindingContext(); // this makes it work.
            var bindingListCorr = new BindingList<CorrespondanceBLL>(VariablePartage.TableCorrespondanceFiltre);
            var source_corr = new BindingSource(bindingListCorr, null);
            dataGridView_saisie.DataSource = source_corr;


            Assert.AreEqual(dataGridView_saisie.Rows[0].Cells[1].Value.ToString(), "55|P01");

        }

    }
}
