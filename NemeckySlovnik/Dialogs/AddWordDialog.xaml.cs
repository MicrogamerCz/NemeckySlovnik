using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using NemeckySlovnik;
using NemeckySlovnik.Classes;

namespace NemeckySlovnik.Dialogs
{
    public sealed partial class AddWordDialog : ContentDialog
    {
        public Wort newWord { get; private set; }
        bool doRepeatFunc = true;

        const string sloveso = "Sloveso";
        const string podstatnéJméno = "Podstatné jméno";

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
                    case sloveso:
                        {
                            IsPrimaryButtonEnabled = czword.Text != string.Empty && infword.Text != string.Empty &&
                                prätword.Text != string.Empty && perfword.Text != string.Empty &&
                                firstsingular.Text != string.Empty && secondsingular.Text != string.Empty &&
                                thirdsingular.Text != string.Empty && firstplural.Text != string.Empty &&
                                secondplural.Text != string.Empty && thirdplural.Text != string.Empty &&
                                (typePicker.SelectedItem as string) != null;
                            break;
                        }
                    case podstatnéJméno:
                        {
                            IsPrimaryButtonEnabled = czword.Text != string.Empty && (substantivTypePicker.SelectedItem as string) != null
                                && substantivBox.Text != string.Empty;
                            break;
                        }
                    default: break; //IsPrimaryButtonEnabled = false; return;
                }
            } while (doRepeatFunc);
        }

        void ChangePanelVisibility(object sender, object e)
        {
            czword.IsEnabled = true;
            lectionNumberBox.IsEnabled = true;
            switch ((sender as ComboBox).SelectedItem as string)
            {
                case sloveso:
                    {
                        verbInfo.Visibility = Visibility.Visible;
                        substantivInfo.Visibility = Visibility.Collapsed;
                        czword.PlaceholderText = "př. hrát";
                        break;
                    }
                case podstatnéJméno:
                    {
                        substantivInfo.Visibility = Visibility.Visible;
                        verbInfo.Visibility = Visibility.Collapsed;
                        czword.PlaceholderText = "student";
                        break;
                    }
            }
        }

        void StopRepeater(object sender, object args)
        {
            doRepeatFunc = false;
            int? result = null;
            if (!double.IsNaN(lectionNumberBox.Value)) result = (int)lectionNumberBox.Value;
            switch (wordTypePicker.SelectedItem as string)
            {
                case sloveso:
                    {
                        newWord = new Wort(new Verb(czword.Text, infword.Text, prätword.Text, perfword.Text, firstsingular.Text,
                            secondsingular.Text, thirdsingular.Text, firstplural.Text, secondplural.Text, thirdplural.Text,
                            (Verb.VerbType)typePicker.SelectedIndex, result));
                        break;
                    }
                case podstatnéJméno:
                    {
                        newWord = new Wort(new Substantiv(czword.Text,
                            (Substantiv.SubstantivType)substantivTypePicker.SelectedIndex, strongWordCheck.IsChecked ?? false,
                            result, substantivBox.Text));
                        break;
                    }
            }
        }
    }
}
