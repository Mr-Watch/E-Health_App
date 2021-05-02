using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Windows.UI.Xaml.Controls;
using System.Net;
using System.IO;
using System.Windows;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace healthtest2.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public MainPage()
        {
            InitializeComponent();

        }
        private void Button_Clickarm(object sender, RoutedEventArgs e)
        {
            if (handsTestSplit.IsPaneOpen == false)
            {
                handsTestSplit.IsPaneOpen = true;
            }
            else
            {
                handsTestSplit.IsPaneOpen = false;
            }
        }
        private void Button_Clickchest(object sender, RoutedEventArgs e)
        {
            if (chestTestSplit.IsPaneOpen == false)
            {
                chestTestSplit.IsPaneOpen = true;
            }
            else
            {
                chestTestSplit.IsPaneOpen = false;
            }
        }
        private void Button_Clicklegs(object sender, RoutedEventArgs e)
        {
            if (legsTestSplit.IsPaneOpen == false)
            {
                legsTestSplit.IsPaneOpen = true;
            }
            else
            {
                legsTestSplit.IsPaneOpen = false;
            }
        }
        private void Button_Clickneck(object sender, RoutedEventArgs e)
        {
            if (neckTestSplit.IsPaneOpen == false)
            {
                neckTestSplit.IsPaneOpen = true;
            }
            else
            {
                neckTestSplit.IsPaneOpen = false;
            }
        }
        private void Button_Clickhead(object sender, RoutedEventArgs e)
        {
            if (headTestSplit.IsPaneOpen == false)
            {
                headTestSplit.IsPaneOpen = true;
            }
            else
            {
                headTestSplit.IsPaneOpen = false;
            }
        }
        private void Button_Clickpelvis(object sender, RoutedEventArgs e)
        {
            if (pelvisTestSplit.IsPaneOpen == false)
            {
                pelvisTestSplit.IsPaneOpen = true;
            }
            else
            {
                pelvisTestSplit.IsPaneOpen = false;
            }
        }
        private void Button_Clickabdomen(object sender, RoutedEventArgs e)
        {
            if (abdomenTestSplit.IsPaneOpen == false)
            {
                abdomenTestSplit.IsPaneOpen = true;
            }
            else
            {
                abdomenTestSplit.IsPaneOpen = false;
            }
        }
        public string test { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void searchbutton_click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                WebRequest request = HttpWebRequest.Create("http://127.0.0.1:5000/ath/api/v0.1/biomedlexicon/" + searchbar1.Text);
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string illnamejason = reader.ReadToEnd();
                JObject json = JObject.Parse(illnamejason);
                foreach (var pair in json)
                {
                    Debug.WriteLine(pair.Value);
                    illnesslist.Items.Add(pair.Value);
                    testtext.Text = (string)pair.Value;


                }
                response.Close();
            }
            catch
            {
            }

        }
        private void navigatebutton(object sender, RoutedEventArgs args)
        {
            MainPage ma = new MainPage() {test = testtext.Text };
           Frame.Navigate(typeof(resultpage), ma);

        }


        public class testcontentpass
        {
            public object data { set; get; }
        }
        public class illname
        {
            public string translation { get; set; }
        }

    }
}

