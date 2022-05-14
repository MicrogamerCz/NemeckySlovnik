using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using NemeckySlovnik.Items;

namespace NemeckySlovnik.Dialogs
{
    public sealed partial class AddWordDialog : ContentDialog
    {
        public object newWord { get; private set; }
        bool doRepeatFunc = true;
        public AddWordDialog()
        {
            this.InitializeComponent();
            Repeater();
        }

        async void Repeater()
        {
            do
            {
                await Task.Delay(100);
                switch (wordTypePicker.SelectedValue as string)
                {
                    case "Sloveso":
                        {
                            IsPrimaryButtonEnabled = czword.Text != string.Empty && infword.Text != string.Empty &&
                                prätword.Text != string.Empty && perfword.Text != string.Empty &&
                                firstsingular.Text != string.Empty && secondsingular.Text != string.Empty &&
                                thirdsingular.Text != string.Empty && firstplural.Text != string.Empty &&
                                secondplural.Text != string.Empty && thirdplural.Text != string.Empty &&
                                typePicker.SelectedItem as string != string.Empty;
                            break;
                        }
                    default: break; //IsPrimaryButtonEnabled = false; return;
                }
            } while (doRepeatFunc);
        }

        void ChangePanelVisibility(object sender, object e)
        {
            switch ((sender as ComboBox).SelectedItem as string)
            {
                case "Sloveso":
                    {
                        verbInfo.Visibility = Visibility.Visible;
                        break;
                    }
            }
        }

        void StopRepeater(object sender, object args)
        {
            doRepeatFunc = false;
            switch ((wordTypePicker as ComboBox).SelectedItem as string)
            {
                case "Sloveso":
                    {
                        int? result = null;
                        if (!double.IsNaN(lectionNumberBox.Value)) result = ((int)lectionNumberBox.Value);

                        newWord = new Verb
                        {
                            czech = czword.Text,
                            infinitiv = infword.Text,
                            präteritum = prätword.Text,
                            perfekt = perfword.Text,
                            ich = firstsingular.Text,
                            du = secondsingular.Text,
                            erSieEs = thirdsingular.Text,
                            wir = firstplural.Text,
                            ihr = secondplural.Text,
                            sieSie = thirdplural.Text,
                            lection = result
                        };
                        break;
                    }
            }
        }
    }
}
