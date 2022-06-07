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
    public sealed partial class SubstantivDisplayDialog : ContentDialog
    {
        public SubstantivDisplayDialog(Substantiv substantiv)
        {
            this.InitializeComponent();
            Title = $"{substantiv.cz}/{BWL.prefixes1[(int)substantiv.substantivType]} {substantiv.firstSingular}";
            czword.Text = "Česky: " + substantiv.cz;
            switch (substantiv.substantivType)
            {
                case Substantiv.SubstantivType.Maskulinum:
                    {
                        substantivType.Text = "Rod mužský";
                        break;
                    }
                case Substantiv.SubstantivType.Feminimum:
                    {
                        substantivType.Text = "Rod ženský";
                        break;
                    }
                case Substantiv.SubstantivType.Neutrum:
                    {
                        substantivType.Text = "Rod střední";
                        break;
                    }
            }
            hasStrongPronounciationBox.Text = substantiv.isStrong ? "Má silné skloňování" : "Nemá silné skloňování";
            firstSingular.Text = "1. pád (jednotné č.): " + BWL.prefixes1[(int)substantiv.substantivType] + " " + substantiv.firstSingular;
            secondSingular.Text = "2. pád (jednotné č.): " + BWL.prefixes2[(int)substantiv.substantivType] + " " + substantiv.secondSingular;
            thirdSingular.Text = "3. pád (jednotné č.): " + BWL.prefixes3[(int)substantiv.substantivType] + " " + substantiv.thirdSingular;
            fourthSingular.Text = "4. pád (jednotné č.): " + BWL.prefixes4[(int)substantiv.substantivType] + " " + substantiv.fourthSingular;
            firstPlural.Text = "1. pád (množné č.): " + BWL.prefixes1[3] + " " + substantiv.firstPlural;
            secondPlural.Text = "2. pád (množné č.): " + BWL.prefixes2[3] + " " + substantiv.secondPlural;
            thirdPlural.Text = "3. pád (množné č.): " + BWL.prefixes3[3] + " " + substantiv.thirdPlural;
            fourthPlural.Text = "4. pád (množné č.): " + BWL.prefixes4[3] + " " + substantiv.fourthPlural;
            if (substantiv.lection != null) lection.Text = "Lekce " + substantiv.lection;
            foreach (UIElement item in wordProperties.Children) item.Visibility = Visibility.Visible;
        }
    }
}
