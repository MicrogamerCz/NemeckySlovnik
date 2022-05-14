using System;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using Windows.Storage.Streams;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using NemeckySlovnik.Items;
using System.Runtime.Serialization.Formatters.Binary;

namespace NemeckySlovnik
{
    internal static class BackgroundWorker
    {
        //Reference hlavního okna
        public static MainPage mainPage;

        //List obsahující všechna slova -> slovesa, podstatná jména, atd.
        public static List<object> words;

        //Funkce, která se spustí při spuštění aplikace
        public static async void Initialize(MainPage page)
        {
            words = new List<object>();
            mainPage = page; //Hodí se pro referenci na některé objekty (usnadní to bolesti do budoucna)
            
            //Tento celý blok otevře tzv. indexový soubor, kde budou všechna slova pro tuto aplikaci
            //Jestli soubor nebude existovat, tak to vyhodí chybu a vypne vyhledávání, jinak to bude normálně fungovat
            var indexfile = await ApplicationData.Current.LocalFolder.TryGetItemAsync("czde-index.nsi") as StorageFile;
            if (indexfile is null)
            {

                ThrowError("Chybí indexový soubor", "Bez indexového souboru nemůže fungovat tato aplikace. Dokud nebude " +
                    "otevřen index anebo vytvořen nový přidáním slova, vyhledávání bude vypnuto", InfoBarSeverity.Warning);
                page.searchBox.IsEnabled = false;
                return;
            }

            //Převede obsah souboru na použitelné objekty, které se potom převedou do výsledků vyhledávání
            BinaryFormatter bf = new BinaryFormatter();
            using (Stream stream = await indexfile.OpenStreamForReadAsync())
            { words = bf.Deserialize(stream) as List<object>; }
            foreach (object word in words) page.wordList.Items.Add(new BaseWordInfo(word));
        }
        public static async void UpdateIndexFile()
        {
            StorageFile indexfile =
                ((await ApplicationData.Current.LocalFolder.TryGetItemAsync("czde-index.nsi")) as StorageFile) ??
                (await ApplicationData.Current.LocalFolder.CreateFileAsync("czde-index.nsi"));
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                bf.Serialize(stream, words);
                await FileIO.WriteBytesAsync(indexfile, stream.ToArray());
            }
        }

        public static void ThrowError(string title, string message, InfoBarSeverity severity) =>
            mainPage.mainPanel.Children.Add(new InfoBar { Title = title, Message = message, Severity = severity, IsOpen = true });
    }
}
