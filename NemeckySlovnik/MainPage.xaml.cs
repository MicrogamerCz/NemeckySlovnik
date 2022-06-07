using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls;
using NemeckySlovnik.Dialogs;
using NemeckySlovnik.Classes;
using NemeckySlovnik.BaseWordList;
using NemeckySlovnik.Displays;

namespace NemeckySlovnik
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            BackgroundWorker.Initialize(this);
        }

        void SubstantivFilterChecked(object sender, object e)
        {
            bool check = (sender as CheckBox).IsChecked ?? false;
            verbCheckBox.IsChecked = !check ? verbCheckBox.IsChecked : false;
            regVerbCheckBox.Visibility = Visibility.Collapsed;
            regVerbCheckBox.IsChecked = false;
            irregVerbCheckBox.Visibility = Visibility.Collapsed;
            irregVerbCheckBox.IsChecked = false;
            mixVerbCheckBox.Visibility = Visibility.Collapsed;
            mixVerbCheckBox.IsChecked = false;
            modalVerbCheckBox.Visibility = Visibility.Collapsed;
            modalVerbCheckBox.IsChecked = false;

            wordList.Items.Clear();
            if (check)
            {
                foreach (BaseWordInfo bwi in from w in BackgroundWorker.words where w.wordType is Wort.WortType.Substantiv select new BaseWordInfo(w))
                    wordList.Items.Add(bwi);
            }
            else
            {
                foreach (BaseWordInfo bwi in from w in BackgroundWorker.words select new BaseWordInfo(w))
                    wordList.Items.Add(bwi);
            }
        }
        void SwitchVerbCheckBoxes(object sender, object e)
        {
            bool check = (sender as CheckBox).IsChecked ?? false;
            nounCheckBox.IsChecked = !check ? nounCheckBox.IsChecked : false;
            Visibility result = check ? Visibility.Visible : Visibility.Collapsed;
            regVerbCheckBox.Visibility = result;
            regVerbCheckBox.IsChecked = false;
            irregVerbCheckBox.Visibility = result;
            irregVerbCheckBox.IsChecked = false;
            mixVerbCheckBox.Visibility = result;
            mixVerbCheckBox.IsChecked = false;
            modalVerbCheckBox.Visibility = result;
            modalVerbCheckBox.IsChecked = false;
            
            wordList.Items.Clear();
            if (check)
            {
                foreach (BaseWordInfo bwi in from w in BackgroundWorker.words where w.wordType is Wort.WortType.Verb select new BaseWordInfo(w))
                    wordList.Items.Add(bwi);
            }
            else
            {
                foreach (BaseWordInfo bwi in from w in BackgroundWorker.words select new BaseWordInfo(w))
                    wordList.Items.Add(bwi);
            }
        }
        void VerbTypeFilterChecked(object sender, object e)
        {
            wordList.Items.Clear();
            if (!(sender as CheckBox).IsChecked ?? true)
            {
                foreach (BaseWordInfo bwi in from w in BackgroundWorker.words where w.wordType is Wort.WortType.Verb select new BaseWordInfo(w))
                    wordList.Items.Add(bwi);
            }
            else switch ((sender as CheckBox).Name)
                {
                    case "regVerbCheckBox":
                        {
                            irregVerbCheckBox.IsChecked = false;
                            mixVerbCheckBox.IsChecked = false;
                            modalVerbCheckBox.IsChecked = false;
                            foreach (BaseWordInfo bwi in from w in BackgroundWorker.words where w.wordType is Wort.WortType.Verb where (int)w.verb.verbType <= 4 select new BaseWordInfo(w))
                                wordList.Items.Add(bwi);
                            break;
                        }
                    case "irregVerbCheckBox":
                        {
                            mixVerbCheckBox.IsChecked = false;
                            regVerbCheckBox.IsChecked = false;
                            modalVerbCheckBox.IsChecked = false;
                            foreach (BaseWordInfo bwi in from w in BackgroundWorker.words where w.wordType is Wort.WortType.Verb where (int)w.verb.verbType >= 5 && (int)w.verb.verbType <= 7 select new BaseWordInfo(w))
                                wordList.Items.Add(bwi);
                            break;
                        }
                    case "mixVerbCheckBox":
                        {
                            irregVerbCheckBox.IsChecked = false;
                            regVerbCheckBox.IsChecked = false;
                            modalVerbCheckBox.IsChecked = false;
                            foreach (BaseWordInfo bwi in from w in BackgroundWorker.words where w.wordType is Wort.WortType.Verb where (int)w.verb.verbType == 8 select new BaseWordInfo(w))
                                wordList.Items.Add(bwi);
                            break;
                        }
                    case "modalVerbCheckBox":
                        {
                            mixVerbCheckBox.IsChecked = false;
                            regVerbCheckBox.IsChecked = false;
                            irregVerbCheckBox.IsChecked = false;
                            foreach (BaseWordInfo bwi in from w in BackgroundWorker.words where w.wordType is Wort.WortType.Verb where (int)w.verb.verbType == 9 select new BaseWordInfo(w))
                                wordList.Items.Add(bwi);
                            break;
                        }
                }
        }
        async void AddWordToIndex(object sender, object e)
        {
            AddWordDialog addWord = new AddWordDialog();
            if (await addWord.ShowAsync() is ContentDialogResult.Secondary) return;
            BackgroundWorker.words.Add(addWord.newWord);
            wordList.Items.Add(new BaseWordInfo(addWord.newWord));
            searchBox.IsEnabled = true;
            foreach (UIElement child in mainPanel.Children) if (child is InfoBar) mainPanel.Children.Remove(child);
            BackgroundWorker.UpdateDictionary();
        }

        async void OnWordOpened(object sender, ItemClickEventArgs e)
        {
            switch ((e.ClickedItem as BaseWordInfo).currentWord.wordType)
            {
                case Wort.WortType.Verb:
                    {
                        await new VerbDisplayDialog((e.ClickedItem as BaseWordInfo).currentWord.verb).ShowAsync();
                        break;
                    }
                case Wort.WortType.Substantiv:
                    {
                        await new SubstantivDisplayDialog((e.ClickedItem as BaseWordInfo).currentWord.substantiv).ShowAsync();
                        break;
                    }
            }
        }
        void SearchWord(object sender, object e)
        {
            string searchedItem = (sender as TextBox).Text;
            List<Wort> searchResult = (from w in BackgroundWorker.words where w.wordType is Wort.WortType.Verb where w.verb.cz.Contains(searchedItem) || w.verb.infinitiv.Contains(searchedItem) select w) as List<Wort>;
            searchResult.AddRange(from w in BackgroundWorker.words where w.wordType is Wort.WortType.Substantiv where w.substantiv.cz.Contains(searchedItem) || w.substantiv.firstSingular.Contains(searchedItem) select w);
        }
    }
}
