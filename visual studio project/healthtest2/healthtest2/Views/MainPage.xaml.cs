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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace healthtest2.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private ObservableCollection<Symptom> DataSource = new ObservableCollection<Symptom>();
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = this;
            Dictionary<String, string> colors = new Dictionary<string, string>();

            DataSource = GetSymptomData();
            // Put some key value pairs into the dictionary
            colors.Add("SILVER", "#C0C0C0");
            colors.Add("RED", "#FF0000");
            colors.Add("GREEN", "#008000");
            colors.Add("AQUA", "#00FFFF");
            colors.Add("PURPLE", "#800080");
            colors.Add("LIME", "#00FF00");

            // Finally, Specify the ComboBox items source
            ComboBox1.ItemsSource = colors;

            // Specify the ComboBox items text and value
            ComboBox1.SelectedValuePath = "Value";
            ComboBox1.DisplayMemberPath = "Key";

            Items = new List<ItemVM>
                {
                    new ItemVM {IsSelected = false, Name = "Firefox"},
                    new ItemVM {IsSelected = false, Name = "Chrome"},
                    new ItemVM {IsSelected = false, Name = "IE"}
                };
        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the ComboBox instance
            ComboBox comboBox = sender as ComboBox;

            // Get the ComboBox selected item value and display on TextBlock
            testblock.Text += "Value : " + comboBox.SelectedValue.ToString();
            string help1 = comboBox.SelectedValue.ToString();
            foreach (var help2 in help1) {
                illnesslist.Items.Add(help2);
            }
        }


        public IEnumerable<ItemVM> Items { get; set; }
        private IEnumerable<ItemVM> _selectedItems;
        public class ItemVM
        {
            public string Name { get; set; }
            public bool IsSelected { get; set; }

        }
        public IEnumerable<ItemVM> SelectedItems
        {
            get { return _selectedItems; }
            set
            {
                _selectedItems = value;
                if (PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs("SelectedItems"));
            }
        }

        private void EvaluateSelectedItems(object sender, RoutedEventArgs e)
        {

            SelectedItems = Items.Where(item => item.IsSelected);
            
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

        private void Addbutton_click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                
                {


                }
            }
            catch
            {
            }

        }
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
        
        private ObservableCollection<Symptom> GetSymptomData() {
            var list = new ObservableCollection<Symptom>();
            Symptom ShoulderCategory = new Symptom() {
                Name = "Shoulder Symptoms",
                Children = {
                    new Symptom() { Name = "lump in shoulder" },
                    new Symptom() { Name = "shoulder girdle muscle weakness" },
                    new Symptom() { Name = "shoulder muscle pain" },
                    new Symptom() { Name = "shoulder muscle twitching" },
                    new Symptom() { Name = "shoulder tender to touch" },
                    new Symptom() { Name = "subacromial bursal tenderness" }
            }
            };
            Symptom ArmpitCategory = new Symptom()
            {
                Name = "Armpit Symptoms",
                Children = {
                    new Symptom() { Name = "axillary lymph node enlargement" },
                    new Symptom() { Name = "axillary lymph node tenderness" },
                    new Symptom() { Name = "darkening skin on armpit" },
                    new Symptom() { Name = "firm lump in arm pit" },
                    new Symptom() { Name = "losing armpit hair" },
                    new Symptom() { Name = "lump in armpit that doesn't move" },
                    new Symptom() { Name = "painful nodules in armpits" },
                    new Symptom() { Name = "rash limited to armpit"},
                    new Symptom() { Name = "very little armpit hair" }
            }
            };
            Symptom UpperArmCategory = new Symptom()
            {
                Name = "Upper Arm Symptoms",
                Children = {
                    new Symptom() { Name = "bicep shaking" },
                    new Symptom() { Name = "biceps and triceps hyperreflexia" },
                    new Symptom() { Name = "biceps hyporeflexia" },
                    new Symptom() { Name = "humeral swelling, lower" },
                    new Symptom() { Name = "triceps hyporeflexia" },
                    new Symptom() { Name = "upper arm pain" }
            }
            };
            Symptom ElbowCategory = new Symptom()
            {
                Name = "Elbow Symptoms",
                Children = {
                    new Symptom() { Name = "darkened skin on elbow" },
                    new Symptom() { Name = "elbow bones out of place" },
                    new Symptom() { Name = "elbow pain" },
                    new Symptom() { Name = "flaky bump(s) limited to elbows or knees" },
                    new Symptom() { Name = "red bump(s) on elbow" },
                    new Symptom() { Name = "single flaky raised skin patch on elbows or knees" },
                    new Symptom() { Name = "stiff elbow" },
                    new Symptom() { Name = "tenderness lateral epicondyle" }
            }
            };
            Symptom ForearmCategory = new Symptom()
            {
                Name = "Forearm Symptoms",
                Children = {
                    new Symptom() { Name = "forearm feels more sensitive" },
                    new Symptom() { Name = "forearm hurts" },
                    new Symptom() { Name = "forearm itches" },
                    new Symptom() { Name = "lump on forearm" },
                    new Symptom() { Name = "tingling or prickling in forearm" }
            }
            };
            Symptom WristCategory = new Symptom()
            {
                Name = "Wrist Symptoms",
                Children = {
                    new Symptom() { Name = "crackling sound when moving wrist" },
                    new Symptom() { Name = "wrist hurts when moved" },
                    new Symptom() { Name = "wrist pain" },
                    new Symptom() { Name = "wrist stiffness" },
                    new Symptom() { Name = "wrist swelling" }
            }
            };
            list.Add(ShoulderCategory);
            list.Add(ArmpitCategory);
            list.Add(UpperArmCategory);
            list.Add(ElbowCategory);
            list.Add(ForearmCategory);
            list.Add(WristCategory);
            return list;
        }

        private void ArmsTree_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {
                illnesslist.Items.Add(args.InvokedItem);
                Debug.WriteLine(args.InvokedItem);
        }
    }
    public class Symptom
    {
        public string Name { get; set; }
        public ObservableCollection<Symptom> Children { get; set; } = new ObservableCollection<Symptom>();

        public override string ToString()
        {
            return Name;
        }
    }
}

