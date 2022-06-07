using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using NemeckySlovnik.Classes;
using NemeckySlovnik.BaseWordList;

namespace NemeckySlovnik.Displays
{
    public sealed partial class VerbDisplayDialog : ContentDialog
    {
        Verb verb;
        public VerbDisplayDialog(Verb verb)
        {
            this.InitializeComponent();
            Title = $"{verb.cz}/{verb.infinitiv}";
            czword.Text = "Česky: " + verb.cz;
            infword.Text = "Infinitiv: " + verb.infinitiv;
            prätword.Text = "Präteritum: " + verb.präteritum;
            perfword.Text = "Perfektum: " + verb.perfekt;
            firstsingular.Text = "ich " + verb.ich;
            secondsingular.Text = "du " + verb.du;
            thirdsingular.Text = "er / sie / es " + verb.erSieEs;
            firstplural.Text = "wir " + verb.wir;
            secondplural.Text = "ihr " + verb.ihr;
            thirdplural.Text = "sie / Sie " + verb.sieSie;
            verbtype.Text = "Typ slovesa: " + BWL.verbtypes[(int)verb.verbType];
            if (verb.lection != null) lection.Text = "Lekce " + verb.lection;
            foreach (UIElement item in wordProperties.Children) item.Visibility = Visibility.Visible;
        }
    }
}
