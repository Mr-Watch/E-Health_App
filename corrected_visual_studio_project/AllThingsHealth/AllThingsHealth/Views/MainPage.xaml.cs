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
using AllThingsHealth.Core.Services;
using System.Threading.Tasks;

namespace AllThingsHealth.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private ObservableCollection<Symptom> DataSource = new ObservableCollection<Symptom>();
        private ObservableCollection<Symptom> DataSourceChest = new ObservableCollection<Symptom>();
        private ObservableCollection<Symptom> DataSourceLeg = new ObservableCollection<Symptom>();
        private ObservableCollection<Symptom> DataSourcePelvis = new ObservableCollection<Symptom>();
        private ObservableCollection<Symptom> DataSourceNeck = new ObservableCollection<Symptom>();
        private ObservableCollection<Symptom> DataSourceHead = new ObservableCollection<Symptom>();
        private ObservableCollection<Symptom> DataSourceAbdomen = new ObservableCollection<Symptom>();
        private ObservableCollection<Symptom> DataSourceHand = new ObservableCollection<Symptom>();
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = this;
            Dictionary<String, string> colors = new Dictionary<string, string>();

            DataSource = GetSymptomData();

            // Put some key value pairs into the dictionary
            colors.Add("THESSALONIKI", "9AEB40EC-A2D2-45E5-B0F5-BABC72591495");
            colors.Add("ATTIKI", "E641ED9D-2A70-409F-948A-AEB07682F977");
            ComboBox1.ItemsSource = colors;


            // Specify the ComboBox items text and value
            ComboBox1.SelectedValuePath = "Value";
            ComboBox1.DisplayMemberPath = "Key";
            ComboBox1.SelectedIndex = 1;
        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the ComboBox instance
            ComboBox comboBox = sender as ComboBox;

            // Get the ComboBox selected item value and display on TextBlock
            // testblock.Text = "Value : " + comboBox.SelectedValue.ToString();
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
            String area = (string)ComboBox1.SelectedValue;
            Frame.Navigate(typeof(resultpage), area);

        }


        public class testcontentpass
        {
            public object data { set; get; }
        }
        public class illname
        {
            public string translation { get; set; }
        }

        private ObservableCollection<Symptom> GetSymptomData()
        {
            var list = new ObservableCollection<Symptom>();

            return list;
        }

        private void Tree_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {
            illnesslist.Items.Add(args.InvokedItem);
            Symptom a = new Symptom();
            foreach (Symptom s in DataSourceAbdomen)
            {
                foreach (Symptom s1 in s.Children)
                {
                    if (s1.Name.Equals(args.InvokedItem.ToString()))
                    {
                        a = s1;
                    }
                }
            }
            if (a != null)
            {
                Debug.WriteLine("Name:" + a.Name + "\nID:" + a.ID + "\nWID:" + a.Webmdid + "\nbid:" + a.Bodyid);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.OriginalSource;
            string selected = btn.Name.ToString();
            if (mainsplit.IsPaneOpen == false)
            {
                mainsplit.IsPaneOpen = true;
            }
            else
            {
                mainsplit.IsPaneOpen = false;
            }
            if (selected.Equals("abdomen"))
            {
                maintree.ItemsSource = DataSourceAbdomen;
                Symptom UpperAbdomenCategory = new Symptom()
                {
                    Name = "Upper Abdomen Symptoms",
                    Children = await FetchSymptoms("abdomen", "upper-abdomen")
                };
                DataSourceAbdomen.Add(UpperAbdomenCategory);
                Symptom EpigastricCategory = new Symptom()
                {
                    Name = "Epigastric Symptoms",
                    Children = await FetchSymptoms("abdomen", "epigastric")
                };
                DataSourceAbdomen.Add(EpigastricCategory);
                Symptom LowerAbdomenCategory = new Symptom()
                {
                    Name = "Lower Abdomen Symptoms",
                    Children = await FetchSymptoms("abdomen", "lower-abdomen")
                };
                DataSourceAbdomen.Add(LowerAbdomenCategory);
            }
            else if (selected.Equals("arm_r") || selected.Equals("arm_l"))
            {
                maintree.ItemsSource = DataSource;
                Symptom ShoulderCategory = new Symptom()
                {
                    Name = "Shoulder Symptoms",
                    Children = await FetchSymptoms("arms", "shoulder")
                };
                DataSource.Add(ShoulderCategory);
                Symptom ArmpitCategory = new Symptom()
                {
                    Name = "Armpit Symptoms",
                    Children = await FetchSymptoms("arms", "armpit")
                };
                DataSource.Add(ArmpitCategory);
                Symptom UpperArmCategory = new Symptom()
                {
                    Name = "Upper Arm Symptoms",
                    Children = await FetchSymptoms("arms", "upper-arm")
                };
                DataSource.Add(UpperArmCategory);
                Symptom ElbowCategory = new Symptom()
                {
                    Name = "Elbow Symptoms",
                    Children = await FetchSymptoms("arms", "elbow")
                };
                DataSource.Add(ElbowCategory);
                Symptom ForearmCategory = new Symptom()
                {
                    Name = "Forearm Symptoms",
                    Children = await FetchSymptoms("arms", "forearm")
                };
                DataSource.Add(ForearmCategory);
                Symptom WristCategory = new Symptom()
                {
                    Name = "Wrist Symptoms",
                    Children = await FetchSymptoms("arms", "wrist")
                };
                DataSource.Add(WristCategory);
            }
            else if (selected.Equals("hand_l") || selected.Equals("hand_r"))
            {
                maintree.ItemsSource = DataSourceHand;
                Symptom HandCategory = new Symptom()
                {
                    Name = "Hand Symptoms",
                    Children = await FetchSymptoms("arms", "hand")
                };
                DataSourceHand.Add(HandCategory);
                Symptom FingerCategory = new Symptom()
                {
                    Name = "Finger Symptoms",
                    Children = await FetchSymptoms("arms", "finger")
                };
                DataSourceHand.Add(FingerCategory);
            }
            else if (selected.Equals("leg_l") || selected.Equals("leg_r"))
            {
                maintree.ItemsSource = DataSourceLeg;
                Symptom ThighCategory = new Symptom()
                {
                    Name = "Thigh Symptoms",
                    Children = await FetchSymptoms("legs", "thigh")
                };
                DataSourceLeg.Add(ThighCategory);
                Symptom HamstringCategory = new Symptom()
                {
                    Name = "Hamstring Symptoms",
                    Children = await FetchSymptoms("legs", "hamstring")
                };
                DataSourceLeg.Add(HamstringCategory);
                Symptom KneeCategory = new Symptom()
                {
                    Name = "Knee Symptoms",
                    Children = await FetchSymptoms("legs", "knee")
                };
                DataSourceLeg.Add(KneeCategory);
                Symptom PoplitealCategory = new Symptom()
                {
                    Name = "Popliteal Symptoms",
                    Children = await FetchSymptoms("legs", "popliteal")
                };
                DataSourceLeg.Add(PoplitealCategory);
                Symptom ShinCategory = new Symptom()
                {
                    Name = "Shin Symptoms",
                    Children = await FetchSymptoms("legs", "shin")
                };
                DataSourceLeg.Add(ShinCategory);
                Symptom CalfCategory = new Symptom()
                {
                    Name = "Calf Symptoms",
                    Children = await FetchSymptoms("legs", "calf")
                };
                DataSourceLeg.Add(CalfCategory);
                Symptom AnkleCategory = new Symptom()
                {
                    Name = "Ankle Symptoms",
                    Children = await FetchSymptoms("legs", "ankle")
                };
                DataSourceLeg.Add(AnkleCategory);
                Symptom FootCategory = new Symptom()
                {
                    Name = "Foot Symptoms",
                    Children = await FetchSymptoms("legs", "foot")
                };
                DataSourceLeg.Add(FootCategory);
                Symptom ToesCategory = new Symptom()
                {
                    Name = "Toes Symptoms",
                    Children = await FetchSymptoms("legs", "toes")
                };
                DataSourceLeg.Add(ToesCategory);
            }
            else if (selected.Equals("pelvis"))
            {
                maintree.ItemsSource = DataSourcePelvis;
                Symptom HipCategory = new Symptom()
                {
                    Name = "Hip Symptoms",
                    Children = await FetchSymptoms("pelvis", "hip")
                };
                DataSourcePelvis.Add(HipCategory);
                Symptom GroinCategory = new Symptom()
                {
                    Name = "Groin Symptoms",
                    Children = await FetchSymptoms("pelvis", "groin")
                };
                DataSourcePelvis.Add(GroinCategory);
                Symptom SuprapubicCategory = new Symptom()
                {
                    Name = "Suprapubic Symptoms",
                    Children = await FetchSymptoms("pelvis", "suprapubic")
                };
                DataSourcePelvis.Add(SuprapubicCategory);
                Symptom GenitalsCategory = new Symptom()
                {
                    Name = "Genitals Symptoms",
                    Children = await FetchSymptoms("pelvis", "genital")
                };
                DataSourcePelvis.Add(GenitalsCategory);
            }
            else if (selected.Equals("chest"))
            {
                maintree.ItemsSource = DataSourceChest;
                Symptom UpperChestCategory = new Symptom()
                {
                    Name = "Upper Chest Symptoms",
                    Children = await FetchSymptoms("chest", "upper-chest")
                };
                DataSourceChest.Add(UpperChestCategory);
                Symptom SternumCategory = new Symptom()
                {
                    Name = "Sternum Symptoms",
                    Children = await FetchSymptoms("chest", "sternum")
                };
                DataSourceChest.Add(SternumCategory);
                Symptom BreastCategory = new Symptom()
                {
                    Name = "Breast Symptoms",
                    Children = await FetchSymptoms("chest", "breast")
                };
                DataSourceChest.Add(BreastCategory);
            }
            else if (selected.Equals("neck"))
            {
                maintree.ItemsSource = DataSourceNeck;
                Symptom NeckCategory = new Symptom()
                {
                    Name = "Neck Symptoms",
                    Children = await FetchSymptoms("neck", "")
                };
                DataSourceNeck.Add(NeckCategory);
            }
            else if (selected.Equals("head"))
            {
                maintree.ItemsSource = DataSourceHead;
                Symptom ScalpCategory = new Symptom()
                {
                    Name = "Scalp Symptoms",
                    Children = await FetchSymptoms("head", "scalp")
                };
                DataSourceHead.Add(ScalpCategory);
                Symptom ForeheadCategory = new Symptom()
                {
                    Name = "Forehead Symptoms",
                    Children = await FetchSymptoms("head", "forehead")
                };
                DataSourceHead.Add(ForeheadCategory);
                Symptom EyesCategory = new Symptom()
                {
                    Name = "Eyes Symptoms",
                    Children = await FetchSymptoms("head", "eye")
                };
                DataSourceHead.Add(EyesCategory);
                Symptom NoseCategory = new Symptom()
                {
                    Name = "Nose Symptoms",
                    Children = await FetchSymptoms("head", "nose")
                };
                DataSourceHead.Add(NoseCategory);
                Symptom EarsCategory = new Symptom()
                {
                    Name = "Ears Symptoms",
                    Children = await FetchSymptoms("head", "ear")
                };
                DataSourceHead.Add(EarsCategory);
                Symptom FaceCategory = new Symptom()
                {
                    Name = "Face Symptoms",
                    Children = await FetchSymptoms("head", "face")
                };
                DataSourceHead.Add(FaceCategory);
                Symptom MouthCategory = new Symptom()
                {
                    Name = "Mouth Symptoms",
                    Children = await FetchSymptoms("head", "mouth")
                };
                DataSourceHead.Add(MouthCategory);
                Symptom JawCategory = new Symptom()
                {
                    Name = "Jaw Symptoms",
                    Children = await FetchSymptoms("head", "jaw")
                };
                DataSourceHead.Add(JawCategory);
                Symptom HeadCategory = new Symptom()
                {
                    Name = "Head Symptoms",
                    Children = await FetchSymptoms("head", "head")
                };
                DataSourceHead.Add(HeadCategory);
            }
        }
        public async Task<ObservableCollection<Symptom>> FetchSymptoms(string type, string subtype)
        {
            HttpDataService url = new HttpDataService("http://127.0.0.1:5000");
            String uri = "/ath/api/v0.1/webmd/symptoms/" + type + "/" + subtype;
             String uri2 = "/ath/api/v0.1/webmd/symptoms/id/" + type + "/" + subtype;
                
            ObservableCollection<Symptom> sympt = new ObservableCollection<Symptom>();
            try
            {
                JObject json = await url.GetAsync<JObject>(uri);
                JArray jar = (JArray)json.GetValue("data");
                JObject json2 = await url.GetAsync<JObject>(uri2);
                JValue jar2 = (JValue)json2.GetValue("bodyid");
               
                
                foreach (JObject item in jar)
                {
                    string name = item.GetValue("Name").ToString();
                    string id = item.GetValue("id").ToString();
                    int id1 = int.Parse(id.Substring(3, id.Length - 3));
                    int wid = int.Parse(item.GetValue("webmdid").ToString());
                    
                    int bid = int.Parse(jar2.Value.ToString());
                    sympt.Add(new Symptom { Name = name, ID = id1, Webmdid = wid, Bodyid = bid }); 
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return sympt;
        }
    }
        public class Symptom
        {
            public string Name { get; set; }
            public int ID { get; set; }
            public int Webmdid { get; set; }
            public int Bodyid { get; set; }
        public ObservableCollection<Symptom> Children { get; set; } = new ObservableCollection<Symptom>();

            public override string ToString()
            {
                return Name;
            }
        }
    }


