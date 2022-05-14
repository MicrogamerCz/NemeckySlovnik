using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using NemeckySlovnik.Dialogs;

namespace NemeckySlovnik
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            BackgroundWorker.Initialize(this);
        }

        //Při vybrání filtru slovesa se zviditelní/zneviditelní další možnosti pro slovesa
        void SwitchVerbCheckBoxes(object sender, object e)
        {
            BackgroundWorker.ThrowError("smthing", "nothing", InfoBarSeverity.Informational);
            bool result = (sender as CheckBox).IsChecked ?? false;
            regVerbCheckBox.Visibility = result ? Visibility.Visible : Visibility.Collapsed;
            irregVerbCheckBox.Visibility = result ? Visibility.Visible : Visibility.Collapsed;
            mixVerbCheckBox.Visibility = result ? Visibility.Visible : Visibility.Collapsed;
        }


        async void AddWordToIndex(object sender, object e)
        {
            AddWordDialog addWord = new AddWordDialog();
            if (await addWord.ShowAsync() is ContentDialogResult.Secondary) return;
            BackgroundWorker.words.Add(addWord.newWord);
            wordList.Items.Add(new BaseWordInfo(addWord.newWord));
            searchBox.IsEnabled = true;
            foreach (UIElement child in mainPanel.Children) if (child is InfoBar) mainPanel.Children.Remove(child);
            BackgroundWorker.UpdateIndexFile();
        }
    }
}
