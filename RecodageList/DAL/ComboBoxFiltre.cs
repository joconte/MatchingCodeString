using System;
using System.Collections.Generic;
using System.Text;

namespace RecodageList.DAL
{
    class ComboBoxFiltre
    {
        public string NomRef { get; set; }
        public string Cpl { get; set; }
        public string Cpl1 { get; set; }
        public string Cpl2 { get; set; }
        public string TypeRef { get; set; }

        public override string ToString()
        {
            return NomRef;
        }
    }
}
