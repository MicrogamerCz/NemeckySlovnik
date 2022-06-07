using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;
using System.Runtime.Serialization.Formatters.Binary;
using NemeckySlovnik.Classes;
using System.Linq;

namespace NemeckySlovnik
{
    internal static class BackgroundWorker
    {
        //Reference hlavního okna
        public static MainPage mainPage;

        //List obsahující všechna slova -> slovesa, podstatná jména, atd.
        public static List<Wort> words;

        //Často používané objekty
        const string dictionaryFileName = "czde-index.nsi";
        static readonly StorageFolder localFolder = ApplicationData.Current.LocalFolder;

        //Funkce, která se spustí při spuštění aplikace
        public static async void Initialize(MainPage page)
        {
            words = new List<Wort>();
            mainPage = page; //Hodí se pro referenci na některé objekty (usnadní to bolesti do budoucna)
            
            StorageFile indexfile = (await localFolder.TryGetItemAsync(dictionaryFileName) as StorageFile);
            if (indexfile is null) return;
            System.Diagnostics.Debug.WriteLine(indexfile is null);

            //Převede obsah souboru na použitelné objekty, které se potom převedou do výsledků vyhledávání
            BinaryFormatter bf = new BinaryFormatter();
            using (Stream stream = await indexfile.OpenStreamForReadAsync()) { words = bf.Deserialize(stream) as List<Wort>; }
            foreach (BaseWordInfo word in from word in words select new BaseWordInfo(word)) page.wordList.Items.Add(word);
        }
        public static async void UpdateDictionary()
        {
            StorageFile indexfile = ((await localFolder.TryGetItemAsync(dictionaryFileName)) as StorageFile) ??
                (await localFolder.CreateFileAsync(dictionaryFileName));
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                System.Diagnostics.Debug.WriteLine("Before Serialization");
                bf.Serialize(stream, words);
                System.Diagnostics.Debug.WriteLine("After Serialization");
                await FileIO.WriteBytesAsync(indexfile, stream.ToArray());
            }
        }

        public static void ThrowError(string title, string message, InfoBarSeverity severity) =>
            mainPage.mainPanel.Children.Add(new InfoBar { Title = title, Message = message, Severity = severity, IsOpen = true });
    }
}
