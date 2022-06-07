using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NemeckySlovnik.Classes
{
    [Serializable]
    public class Wort
    {
        public WortType wordType;

        public Verb verb;
        public Substantiv substantiv;

        public enum WortType
        {
            Verb,
            Substantiv
        }
        public Wort(Verb verb)
        {
            this.verb = verb;
            wordType = WortType.Verb;
            substantiv = null;
        }
        public Wort(Substantiv substantiv)
        {
            this.substantiv = substantiv;
            wordType = WortType.Substantiv;
            verb = null;
        }
    }

    [Serializable]
    public class Verb
    {
        //Česky
        public string cz;

        //Infinitiv, minulý čas
        public string infinitiv;
        public string präteritum;
        public string perfekt;

        //Osoby a čísla
        public string ich; public string du; public string erSieEs; //Jednotná
        public string wir; public string ihr; public string sieSie; //Množná

        public VerbType verbType; //Typ slovesa (pravidelné 1-5, nepravidelné (aba, abb, abc), smíšené

        public int? lection; //Lekce podle učebnice (nepovinné)

        public enum VerbType
        {
            /// <summary>
            /// Stavba: ge- + kmen + -t
            /// </summary>
            Type1,
            /// <summary>
            /// Stavba: ge- kmen-t/d + -et
            /// </summary>
            Type2,
            /// <summary>
            /// Stavba: haben/sein + kmen-t      bez ge-     infinitiv končí na -ieren
            /// </summary>
            Type3,
            /// <summary>
            /// Stavba: haben/sein + kmen-(e)t   neodlučitelné předpony be-, emp-, ent-, er-, ge-, ver-, über-, ...
            /// </summary>
            Type4,
            /// <summary>
            /// Stavba: haben/sein + předpona-ge-kmen-t   odlučitelné předpony auf-, au-, ein-, zu-
            /// </summary>
            Type5,

            /// <summary>
            /// Nepravidelná slovesa A-B-A
            /// </summary>
            IrregularABA,
            /// <summary>
            /// Nepravidelná slovesa A-B-B
            /// </summary>
            IrregularABB,
            /// <summary>
            /// Nepravidelná slovesa A-B-C
            /// </summary>
            IrregularABC,

            /// <summary>
            /// Smíšená slovesa
            /// </summary>
            Mixed,

            /// <summary>
            /// Modální slovesa
            /// </summary>
            Modal
        }

        public Verb(string cz, string infinitiv, string präteritum, string perfekt, string ich, string du, string erSieEs, 
            string wir, string ihr, string sieSie, VerbType verbType, int? lection)
        {
            this.cz = cz.ToLower();

            this.infinitiv = infinitiv; this.präteritum = präteritum; this.perfekt = perfekt;

            this.ich = ich; this.du = du; this.erSieEs = erSieEs;
            this.wir = wir; this.ihr = ihr; this.sieSie = sieSie;

            this.verbType = verbType;
            this.lection = lection;
        }
    }
    [Serializable]
    public class Substantiv
    {
        //Česky
        public string cz;

        //Rod
        public SubstantivType substantivType; //Der / Die / Das
        public bool isStrong; //Silné skloňování

        //Pády
        public string firstSingular;    //První pád, číslo jednotné
        public string secondSingular;   //Druhý pád, číslo jednotné
        public string thirdSingular;    //Třetí pád, číslo jednotné
        public string fourthSingular;   //Čtvrtý pád, číslo jednotné
        public string firstPlural;      //První pád, číslo množné
        public string secondPlural;     //Druhý pád, číslo množné
        public string thirdPlural;      //Třetí pád, číslo množné
        public string fourthPlural;     //Čtvrtý pád, číslo množné

        public int? lection;

        /// <summary>
        /// Maskulinum (der)
        /// Feminimum (die)
        /// Neutrum (das)
        /// </summary>
        public enum SubstantivType
        {
            Maskulinum,     //Der
            Feminimum,      //Die
            Neutrum         //Das
        }

        public Substantiv(string cz, SubstantivType substantivType, bool isStrong, int? lection, string substantiv)
        {
            this.cz = cz.ToLower();

            this.substantivType = substantivType;
            this.isStrong = isStrong;

            this.lection = lection;

            firstSingular = substantiv;
            thirdSingular = substantiv;
            fourthSingular = substantiv;

            firstPlural = substantiv + 'e';
            secondPlural = substantiv + 'e';
            thirdPlural = substantiv + "en";
            fourthPlural = substantiv + 'e';

            if (isStrong)
            {
                if (substantivType is SubstantivType.Feminimum) secondSingular = substantiv;
                else if (WordSuffixes.strongSuffixes_s.Any(substantiv.EndsWith)) secondSingular = substantiv + 's';
                else if (WordSuffixes.strongSuffixes_es.Any(substantiv.EndsWith)) secondSingular = substantiv + "es";
                else throw new NotImplementedException("Neplatné slovo");
            }
            else secondSingular = substantiv + (substantiv.EndsWith('e') ? "n" : "en");
        }
    }
    public static class WordSuffixes
    {
        public static readonly string[] strongSuffixes_s = { "ß", "z", "s", "ss", "d", "t" };
        public static readonly string[] strongSuffixes_es = { "el", "lein", "chen", "er" };
    }
}
