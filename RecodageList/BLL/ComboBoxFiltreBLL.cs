using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecodageList.BLL
{
    public class ComboBoxFiltreBLL
    {
        public string NomRef { get; set; }
        public string Cpl { get; set; }
        public string Cpl1 { get; set; }
        public string Cpl2 { get; set; }
        public string TypeRef { get; set; }
        public int TypeRecodage { get; set; }

        public override string ToString()
        {
            return NomRef;
        }

        /*public string ToStringClef()
        {
            return TypeRef + "|" + (Cpl ?? "0") + "|" + (Cpl1 ?? "0") + "|" + (Cpl2 ?? "0");
        }*/

        public bool ContientNomRefAdmin(List<string> ListeNomrefAdmin, string Nomref)
        {
            bool contient = false;
            for (int i = 0; i < ListeNomrefAdmin.Count; i++)
            {
                if (ListeNomrefAdmin[i] == Nomref)
                {
                    contient = true;
                    return contient;
                }
            }
            return contient;
        }
    }
}
