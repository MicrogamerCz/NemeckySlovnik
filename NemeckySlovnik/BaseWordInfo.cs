using System;
using NemeckySlovnik.BaseWordList;
using NemeckySlovnik.Classes;

namespace NemeckySlovnik
{
    //Třída pro zobrazení v hlavním okně
    public class BaseWordInfo
    {
        public string wordcz;
        public string wordde;
        public string wordtype;
        public Wort currentWord;
        public BaseWordInfo(Wort word)
        {
            currentWord = word;
            switch (word.wordType)
            {
                case Wort.WortType.Verb:
                    {
                        wordcz = "Česky: " + word.verb.cz;
                        wordde = "Německy: " + word.verb.infinitiv;
                        wordtype = "Typ slovesa: " + BWL.verbtypes[(int)word.verb.verbType];
                        break;
                    }
                case Wort.WortType.Substantiv:
                    {
                        wordcz = "Česky: " + word.substantiv.cz;
                        wordde = "Německy: " + BWL.prefixes1[(int)word.substantiv.substantivType] + " " + word.substantiv.firstSingular;
                        wordtype = word.substantiv.isStrong ? "Má silné skloňování" : "Má slabé skloňování";
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

            public static readonly string[] prefixes1 = { "der", "die", "das", "die" };
            public static readonly string[] prefixes2 = { "des", "der", "des", "der" };
            public static readonly string[] prefixes3 = { "dem", "der", "dem", "den" };
            public static readonly string[] prefixes4 = { "den", "die", "das", "die" };

            
        }
    }
}
