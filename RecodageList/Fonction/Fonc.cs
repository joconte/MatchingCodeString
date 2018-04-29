using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecodageList.Fonction
{
    class Fonc
    {
        public string CanonicalString(string Str)
        {
            byte[] tab_canonique = Encoding.UTF8.GetBytes ("                " +
                "                " +
                " !     '()*+ -./" +
                "0123456789:  = ?" +
                "@ABCDEFGHIJKLMNO" +
                "PQRSTUVWXYZ(/)  " +
                "'ABCDEFGHIJKLMNO" +
                "PQRSTUVWXYZ(/)- " +
                "E         S(O Z " +
                " ''   --  S)O ZY" +
                " !    /        -" +
                "O 23'u    O    ?" +
                "AAAAAACEEEEEIIII" +
                "DNOOOOOxOUUUUY  " +
                "AAAAAAACEEEEIIII" +
                "ONOOOOO/OUUUUY Y");
                
            /*
            byte[] tab_canonique =
            {
                (byte)'a',(byte)'b',(byte)'c'
            };
            */
            byte charactuel, pchar;
            pchar = (byte)' ';    //previous char

            string NewStr = "";
            try
            {
                for (int i = 0; i < Str.Length; i++)
                {
                    if (Str[i].Equals("è"))
                        Console.WriteLine("J'ai trouvé le è!");
                    charactuel = (byte)Str[i];

                    charactuel = tab_canonique[charactuel];

                    if (charactuel != ' ' || pchar != ' ')
                    {
                        NewStr = NewStr + (char)charactuel;
                        //NewStr.Concat((char)charactuel); //= (char)charactuel;
                    }
                    pchar = charactuel;
                }
            }
            catch (Exception e)
            {
               Console.WriteLine(e.ToString());
            }
            return (NewStr);
        }

        public int LevenshteinDistance(string source, string target)
        {
            if (String.IsNullOrEmpty(source))
            {
                if (String.IsNullOrEmpty(target)) return 0;
                return target.Length;
            }
            if (String.IsNullOrEmpty(target)) return source.Length;

            if (source.Length > target.Length)
            {
                var temp = target;
                target = source;
                source = temp;
            }

            var m = target.Length;
            var n = source.Length;
            var distance = new int[2, m + 1];
            // Initialize the distance 'matrix'
            for (var j = 1; j <= m; j++) distance[0, j] = j;

            var currentRow = 0;
            for (var i = 1; i <= n; ++i)
            {
                currentRow = i & 1;
                distance[currentRow, 0] = i;
                var previousRow = currentRow ^ 1;
                for (var j = 1; j <= m; j++)
                {
                    var cost = (target[j - 1] == source[i - 1] ? 0 : 1);
                    distance[currentRow, j] = Math.Min(Math.Min(
                                distance[previousRow, j] + 1,
                                distance[currentRow, j - 1] + 1),
                                distance[previousRow, j - 1] + cost);
                }
            }
            return distance[currentRow, m];
        }

    }
}
