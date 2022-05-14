using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NemeckySlovnik.Items;
using NemeckySlovnik.BaseWordList;

namespace NemeckySlovnik
{
    //Třída pro zobrazení v hlavním okně
    public struct BaseWordInfo
    {
        public string wordcz;
        public string wordde;
        public string wordtype;

        public BaseWordInfo(dynamic word)
        {
            switch (word)
            {
                case Verb verb:
                    {
                        wordcz = verb.czech;
                        wordde = verb.infinitiv;
                        wordtype = BWL.verbtypes[(int)verb.verbType];
                        break;
                    }
                default: throw new Exception("Unknown type");
            }
        }
    }
    namespace BaseWordList
    {
        public static class BWL
        {
            public static readonly string[] verbtypes = { "1. Typ (ge-kmen-t)", "2. Typ (ge-kmen-d/t-ed)",
                "3. Typ (končící na -ieren)", "4. Typ (neodlučitelné předpony)", "5. (odlučitelné předpony)",
                "Nepravidelné (ABA)", "Nepravidelné (ABB)", "Nepravidelné (ABC)", "Smíšené" };
        }
    }
}
