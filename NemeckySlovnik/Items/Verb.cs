using System;
using System.IO;

namespace NemeckySlovnik.Items
{
    /// <summary>
    /// Třída pro slovesa
    /// </summary>
    [Serializable]
    public class Verb
    {
        //Česky
        public string czech;

        //Infinitiv, minulý čas
        public string infinitiv;
        public string präteritum;
        public string perfekt;

        //Osoby a čísla
        public string ich; public string du; public string erSieEs; //Jednotná
        public string wir; public string ihr; public string sieSie; //Množná

        public VerbType verbType; //Typ slovesa (pravidelné 1-5, nepravidelné (aba, abb, abc), smíšené

        public int? lection; //Lekce podle učebnice
    }

    public enum VerbType
    {
        Type1,          //ge- + kmen + -t
        Type2,          //ge- kmen-t/d + -et
        Type3,          //haben/sein + kmen-t      bez ge-     infinitiv končí na -ieren
        Type4,          //haben/sein + kmen-(e)t   neodlučitelné předpony be-, emp-, ent-, er-, ge-, ver-, über-, ...
        Type5,          //haben/sein + předpona-ge-kmen-t   odlučitelné předpony auf-, au-, ein-, zu-

        IrregularABA, IrregularABB, IrregularABC,

        Mixed
    }
}
