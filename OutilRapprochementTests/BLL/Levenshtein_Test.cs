using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecodageList;
using RecodageList.BLL;
using RecodageList.DAL;

namespace OutilRapprochementTests.BLL
{
    [TestClass]
    public class Levenshtein_Test
    {
        [TestMethod]
        public void TestCalcLevenshtein()
        {
            VariablePartage.CheminBaseClient = "E:\\temp\\CHVAL.db";
            Form1 myform = new Form1();
            LevenshteinBLL objLevenshtein = new LevenshteinBLL();
            ReferentielDAL RefObject = new ReferentielDAL();
            CorrespondanceDAL CorrObject = new CorrespondanceDAL();
            ComboBoxFiltreDAL ComboboxObject = new ComboBoxFiltreDAL();

            VariablePartage.TableReferentiel = RefObject.ObtenirListeReferentiel_SQLITE();
            VariablePartage.TableCorrespondance = CorrObject.ObtenirListeCorrespondance_SQLITE(VariablePartage.TableReferentiel, myform);
            GUIFonction GUI = new GUIFonction();
            GUI.InitListeNomRef_admin();
            VariablePartage.ComboBoxFiltre = ComboboxObject.ObtenirComboBoxFiltre();

            objLevenshtein.InitALLLevenshtein(VariablePartage.TableCorrespondance, VariablePartage.TableReferentiel, VariablePartage.ComboBoxFiltre);
        }
    }
}
