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
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = this;
            Dictionary<String, string> colors = new Dictionary<string, string>();

            DataSource = GetSymptomData();
            DataSourceChest = GetSymptomDataChest();
            DataSourceLeg = GetSymptomDataLeg();
            DataSourcePelvis = GetSymptomDataPelvis();
            DataSourceNeck = GetSymptomDataNeck();
            DataSourceHead = GetSymptomDataHead();
            DataSourceAbdomen = GetSymptomDataAbdomen();

            // Put some key value pairs into the dictionary
            colors.Add("SILVER", "#C0C0C0");
            colors.Add("RED", "#FF0000");
            colors.Add("GREEN", "#008000");
            colors.Add("AQUA", "#00FFFF");
            colors.Add("PURPLE", "#800080");
            colors.Add("LIME", "#00FF00");

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
            if (handsSplit.IsPaneOpen == false)
            {
                handsSplit.IsPaneOpen = true;
            }
            else
            {
                handsSplit.IsPaneOpen = false;
            }
        }
        private void Button_Clickchest(object sender, RoutedEventArgs e)
        {
            if (chestSplit.IsPaneOpen == false)
            {
                chestSplit.IsPaneOpen = true;
            }
            else
            {
                chestSplit.IsPaneOpen = false;
            }
        }
        private void Button_Clicklegs(object sender, RoutedEventArgs e)
        {
            if (legsSplit.IsPaneOpen == false)
            {
                legsSplit.IsPaneOpen = true;
            }
            else
            {
                legsSplit.IsPaneOpen = false;
            }
        }
        private void Button_Clickneck(object sender, RoutedEventArgs e)
        {
            if (neckSplit.IsPaneOpen == false)
            {
                neckSplit.IsPaneOpen = true;
            }
            else
            {
                neckSplit.IsPaneOpen = false;
            }
        }
        private void Button_Clickhead(object sender, RoutedEventArgs e)
        {
            if (headSplit.IsPaneOpen == false)
            {
                headSplit.IsPaneOpen = true;
            }
            else
            {
                headSplit.IsPaneOpen = false;
            }
        }
        private void Button_Clickpelvis(object sender, RoutedEventArgs e)
        {
            if (pelvisSplit.IsPaneOpen == false)
            {
                pelvisSplit.IsPaneOpen = true;
            }
            else
            {
                pelvisSplit.IsPaneOpen = false;
            }
        }
        private void Button_Clickabdomen(object sender, RoutedEventArgs e)
        {
            if (abdomenSplit.IsPaneOpen == false)
            {
                abdomenSplit.IsPaneOpen = true;
            }
            else
            {
                abdomenSplit.IsPaneOpen = false;
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
        private ObservableCollection<Symptom> GetSymptomDataChest()
        {
            var list = new ObservableCollection<Symptom>();
            Symptom UpperChestCategory = new Symptom()
            {
                Name = "Upper Chest Symptoms",
                Children = {
                    new Symptom() { Name = "fatty area above collar bone" },
                    new Symptom() { Name = "left supraclavicular lymph node enlargement" },
                    new Symptom() { Name = "supraclavicular fossa bruit" },
                    new Symptom() { Name = "supraclavicular lymph node enlargement" },
                    new Symptom() { Name = "supraclavicular pulsation" }
            }
            };
            Symptom SternumCategory = new Symptom()
            {
                Name = "Sternum Symptoms",
                Children = {
                    new Symptom { Name = "aortic dilation,ascending"},
                    new Symptom { Name = "aortic dissection"},
                    new Symptom { Name = "aortic infection"},
                    new Symptom { Name = "behind the breastbone hurts"},
                    new Symptom { Name = "breast bone hurts"},
                    new Symptom { Name = "breastbone is abnormal"},
                    new Symptom { Name = "breastbone tender to touch"},
                    new Symptom { Name = "breath sound decrease, basilar, unilateral"},
                    new Symptom { Name = "cardiomegaly"},
                    new Symptom { Name = "chest bones cave in"},
                    new Symptom { Name = "chest bones stick out"},
                    new Symptom { Name = "chest pain that spreads to arm, shoulder, neck or jaw"},
                    new Symptom { Name = "congestive heart failure"},
                    new Symptom { Name = "ejection fraction reduced"},
                    new Symptom { Name = "feeling of pressure in food pipe"},
                    new Symptom { Name = "food gets stuck"},
                    new Symptom { Name = "gibson's murmur"},
                    new Symptom { Name = "hard for food to go down"},
                    new Symptom { Name = "heart beats faster when exercising"},
                    new Symptom { Name = "heart displacement"},
                    new Symptom { Name = "heart displacement, left"},
                    new Symptom { Name = "heart displacement, right"},
                    new Symptom { Name = "heart murmur"},
                    new Symptom { Name = "heart murmur increased with inspiration"},
                    new Symptom { Name = "heart murmur, changing"},
                    new Symptom { Name = "heart murmur, diastolic"},
                    new Symptom { Name = "heart murmur, diastolic, pulmonic"},
                    new Symptom { Name = "heart murmur, holosystolic"},
                    new Symptom { Name = "heart murmur, machinery"},
                    new Symptom { Name = "heart murmur, presystolic"},
                    new Symptom { Name = "heart murmur, systolic"},
                    new Symptom { Name = "heart murmur, systolic, apical"},
                    new Symptom { Name = "heart murmur, systolic, crescendo-decrescendo"},
                    new Symptom { Name = "heart murmur, systolic, pulmonic"},
                    new Symptom { Name = "heart size decrease"},
                    new Symptom { Name = "heart sound absence, second"},
                    new Symptom { Name = "heart sound decrease, first"},
                    new Symptom { Name = "heart sound decrease, second"},
                    new Symptom { Name = "heart sound increase"},
                    new Symptom { Name = "heart sound increase, first"},
                    new Symptom { Name = "heart sound increase, second"},
                    new Symptom { Name = "heart sound irregularity"},
                    new Symptom { Name = "heart sound split, first"},
                    new Symptom { Name = "heart sound split, second"},
                    new Symptom { Name = "heart sound variation, first"},
                    new Symptom { Name = "heart sound, fourth"},
                    new Symptom { Name = "heart sound, third"},
                    new Symptom { Name = "heart sounds muffled"},
                    new Symptom { Name = "heart thrill"},
                    new Symptom { Name = "heart thrill, apical"},
                    new Symptom { Name = "heart thrill, diastolic"},
                    new Symptom { Name = "heart thrill, pulmonic"},
                    new Symptom { Name = "heartburn"},
                    new Symptom { Name = "hiccups"},
                    new Symptom { Name = "inflammation of esophagus"},
                    new Symptom { Name = "mitral valve prolapse"},
                    new Symptom { Name = "nipple hurts"},
                    new Symptom { Name = "palpitations"},
                    new Symptom { Name = "pericardial friction rub"},
                    new Symptom { Name = "pressure on heart due to fluid buildup"},
                    new Symptom { Name = "pulmonary ejection click"},
                    new Symptom { Name = "pulmonic sound absence"},
                    new Symptom { Name = "pulmonic sound decrease"},
                    new Symptom { Name = "pulmonic sound increased"},
                    new Symptom { Name = "severe chest pain/pressure"},
                    new Symptom { Name = "sternal lift"},
                    new Symptom { Name = "sternal pulsation visible"},
                    new Symptom { Name = "sternoclavicular joint pulsation"},
                    new Symptom { Name = "systolic heart murmur, increased with valsalva"},
                    new Symptom { Name = "systolic thrill"},
                    new Symptom { Name = "tightening of esophagus"}
            }
            };
            Symptom BreastCategory = new Symptom()
            {
                Name = "Breast Symptoms",
                Children = {
                    new Symptom { Name = "abnormal growth of male breasts"},
                    new Symptom { Name = "bloody nipple discharge"},
                    new Symptom { Name = "breast cancer"},
                    new Symptom { Name = "breast feels harder"},
                    new Symptom { Name = "breast feels heavy"},
                    new Symptom { Name = "breast getting bigger"},
                    new Symptom { Name = "breast getting smaller"},
                    new Symptom { Name = "breast hurts"},
                    new Symptom { Name = "breast mass roundness"},
                    new Symptom { Name = "breast mass smoothness"},
                    new Symptom { Name = "breast mass, unilateral"},
                    new Symptom { Name = "breast redness"},
                    new Symptom { Name = "breast skin feels like an orange peel"},
                    new Symptom { Name = "breastfeeding mom"},
                    new Symptom { Name = "breasts not developing"},
                    new Symptom { Name = "darkened skin on nipple"},
                    new Symptom { Name = "enlarged vein on breast"},
                    new Symptom { Name = "fluid leaking from nipple"},
                    new Symptom { Name = "growth on nipple"},
                    new Symptom { Name = "hard lump in breast"},
                    new Symptom { Name = "infected lump or sore on breast"},
                    new Symptom { Name = "loss of skin color on nipple"},
                    new Symptom { Name = "lump in breast"},
                    new Symptom { Name = "lump in breast that can be moved"},
                    new Symptom { Name = "lump in breast that doesn't move"},
                    new Symptom { Name = "nipple doesn't move"},
                    new Symptom { Name = "nipple pulling to one side"},
                    new Symptom { Name = "nipple redness"},
                    new Symptom { Name = "nipple stays hard all the time"},
                    new Symptom { Name = "nipple tender to touch"},
                    new Symptom { Name = "painful tube like lump in breast"},
                    new Symptom { Name = "part of breast skin appears pulled inward"},
                    new Symptom { Name = "rash limited to under the breast"},
                    new Symptom { Name = "red, irritated nipple"},
                    new Symptom { Name = "squishy lump in breast"},
                    new Symptom { Name = "swollen breast"},
                    new Symptom { Name = "swollen nipples"},
                    new Symptom { Name = "wide set nipples "}
                }
            };
            list.Add(UpperChestCategory);
            list.Add(SternumCategory);
            list.Add(BreastCategory);
            return list;
        }
        private ObservableCollection<Symptom> GetSymptomDataLeg()
        {
            var list = new ObservableCollection<Symptom>();
            Symptom ThighCategory = new Symptom()
            {
                Name = "Thigh Symptoms",
                Children = {
                    new Symptom { Name = "burning feeling on thigh"},
                    new Symptom { Name = "can't feel hot or cold on thigh"},
                    new Symptom { Name = "cramp in thigh muscle"},
                    new Symptom { Name = "dahl's sign positive"},
                    new Symptom { Name = "fat thigh"},
                    new Symptom { Name = "itching thigh"},
                    new Symptom { Name = "large thigh muscle"},
                    new Symptom { Name = "movement of upper leg outward"},
                    new Symptom { Name = "numb thigh muscle"},
                    new Symptom { Name = "pain in thigh"},
                    new Symptom { Name = "popping sound when turn thigh outward"},
                    new Symptom { Name = "red thigh"},
                    new Symptom { Name = "thigh muscle feels firm"},
                    new Symptom { Name = "thigh muscle mass"},
                    new Symptom { Name = "thigh twitching"},
                    new Symptom { Name = "weak thigh muscle"}
                }
            };
            Symptom HamstringCategory = new Symptom()
            {
                Name = "Hamstring Symptoms",
                Children = {
                    new Symptom { Name = "back of upper leg is weak"}
                }
            };
            Symptom KneeCategory = new Symptom()
            {
                Name = "Knee Symptoms",
                Children = {
                    new Symptom { Name = " back of knee hurts"},
                    new Symptom { Name = "can feel small lump in knee"},
                    new Symptom { Name = "clutton joints"},
                    new Symptom { Name = "darkened skin on knee"},
                    new Symptom { Name = "dislocated knee"},
                    new Symptom { Name = "flaky bump(s) limited to elbows or knees"},
                    new Symptom { Name = "front of knee hurts"},
                    new Symptom { Name = "front of knee is swollen"},
                    new Symptom { Name = "genu valgum"},
                    new Symptom { Name = "genu varum"},
                    new Symptom { Name = "hurts to kneel"},
                    new Symptom { Name = "hurts to walk"},
                    new Symptom { Name = "inflamed fluid sac in knee"},
                    new Symptom { Name = "inside edge of knee is swollen"},
                    new Symptom { Name = "knee cracking when moving"},
                    new Symptom { Name = "knee feels like it is slipping"},
                    new Symptom { Name = "knee gets stuck when moving"},
                    new Symptom { Name = "knee hurts"},
                    new Symptom { Name = "knee instability"},
                    new Symptom { Name = "knee is able to bend"},
                    new Symptom { Name = "knee joint inflammation"},
                    new Symptom { Name = "knee joint makes popping sounds"},
                    new Symptom { Name = "knee tender to touch"},
                    new Symptom { Name = "lump on knee"},
                    new Symptom { Name = "mcmurray test positive"},
                    new Symptom { Name = "outer side of knee hurts"},
                    new Symptom { Name = "pain on inside edge of knee"},
                    new Symptom { Name = "patellar tendon reflex absent"},
                    new Symptom { Name = "patellar tendon reflex decreased"},
                    new Symptom { Name = "patellar tendon reflex increased"},
                    new Symptom { Name = "pulsating lump around knee"},
                    new Symptom { Name = "single flaky raised skin patch on elbows or knees"},
                    new Symptom { Name = "stiff knee"},
                    new Symptom { Name = "swollen knee"},
                    new Symptom { Name = "tibial tuberosity tenderness"},
                    new Symptom { Name = "trouble moving knee"},
                    new Symptom { Name = "weak knee muscle"}
                }
            };
            Symptom PoplitealCategory = new Symptom()
            {
                Name = "Popliteal Symptoms",
                Children = {
                    new Symptom() { Name = "back of knee is swollen" },
                    new Symptom() { Name = "joint fluid swelling of back off knee joint" },
                    new Symptom() { Name = "lachman test positive" }
                }
            };
            Symptom ShinCategory = new Symptom()
            {
                Name = "Shin Symptoms",
                Children = {
                    new Symptom() { Name = "ridges on shin bone" },
                    new Symptom() { Name = "sharp forward bowing of shin" },
                    new Symptom() { Name = "tibial bone mass" },
                    new Symptom() { Name = "tibial deformity" },
                    new Symptom() { Name = "tibial pulse absence" }
                }
            };
            Symptom CalfCategory = new Symptom()
            {
                Name = "Calf Symptoms",
                Children = {
                    new Symptom { Name = "calf muscle cramp"},
                    new Symptom { Name = "calf muscle feels hard"},
                    new Symptom { Name = "calf muscle is larger than normal"},
                    new Symptom { Name = "calf pain"},
                    new Symptom { Name = "calf swelling"},
                    new Symptom { Name = "hurts to walk"},
                    new Symptom { Name = "peroneal sign positive"},
                    new Symptom { Name = "tender calf muscle"},
                    new Symptom { Name = "weakness in lower legs"}
                }
            };
            Symptom AnkleCategory = new Symptom()
            {
                Name = "Ankle Symptoms",
                Children = {
                    new Symptom { Name = "achilles areflexia"},
                    new Symptom { Name = "ankle pain"},
                    new Symptom { Name = "ankle redness"},
                    new Symptom { Name = "ankle reflex decreased"},
                    new Symptom { Name = "ankle swollen"},
                    new Symptom { Name = "arthritis in ankle"},
                    new Symptom { Name = "bruise on ankle"},
                    new Symptom { Name = "lump on ankle"}
                }
            };
            Symptom FootCategory = new Symptom()
            {
                Name = "Foot Symptoms",
                Children = {
                    new Symptom { Name = "ankle is overly flexible"},
                    new Symptom { Name = "arthritis in big toe joint"},
                    new Symptom { Name = "arthritis in the arch of foot"},
                    new Symptom { Name = "ball of foot joint hurts when move"},
                    new Symptom { Name = "big toe joint is stiff"},
                    new Symptom { Name = "big toe joint is swollen"},
                    new Symptom { Name = "big toe joint is tender to touch"},
                    new Symptom { Name = "bottom of foot pain"},
                    new Symptom { Name = "bottom of foot peeling"},
                    new Symptom { Name = "bottom of foot red"},
                    new Symptom { Name = "bottom of foot sweats more"},
                    new Symptom { Name = "bottom of foot swelling"},
                    new Symptom { Name = "bottom of foot yellow"},
                    new Symptom { Name = "can't hold foot up"},
                    new Symptom { Name = "charcot joint"},
                    new Symptom { Name = "clubfoot"},
                    new Symptom { Name = "enlarged rounded toe"},
                    new Symptom { Name = "extreme arch in foot"},
                    new Symptom { Name = "feels like toe is burning"},
                    new Symptom { Name = "flat feet"},
                    new Symptom { Name = "foot changing color"},
                    new Symptom { Name = "foot deformity"},
                    new Symptom { Name = "foot feels cold"},
                    new Symptom { Name = "foot feels hot or warm"},
                    new Symptom { Name = "foot feels stiff"},
                    new Symptom { Name = "foot feels weak"},
                    new Symptom { Name = "foot hurts"},
                    new Symptom { Name = "foot is numb"},
                    new Symptom { Name = "foot is turned out"},
                    new Symptom { Name = "foot is turned up"},
                    new Symptom { Name = "foot muscle is thinning"},
                    new Symptom { Name = "foot peeling"},
                    new Symptom { Name = "foot pulse absence"},
                    new Symptom { Name = "foot smallness"},
                    new Symptom { Name = "foot turning blue"},
                    new Symptom { Name = "foot turning red"},
                    new Symptom { Name = "heel hurts"},
                    new Symptom { Name = "heel is swollen"},
                    new Symptom { Name = "heel is turning in"},
                    new Symptom { Name = "heel is turning out"},
                    new Symptom { Name = "heel spur"},
                    new Symptom { Name = "heel tenderness"},
                    new Symptom { Name = "hives on foot"},
                    new Symptom { Name = "infected lump or sore on foot"},
                    new Symptom { Name = "itchy foot"},
                    new Symptom { Name = "large feet"},
                    new Symptom { Name = "matles test positive"},
                    new Symptom { Name = "metacarpal shortness"},
                    new Symptom { Name = "open sore(s) on foot"},
                    new Symptom { Name = "open sore(s) on soles of feet"},
                    new Symptom { Name = "pain in the arch of foot"},
                    new Symptom { Name = "pale toe"},
                    new Symptom { Name = "pes cavus"},
                    new Symptom { Name = "pigeon toed"},
                    new Symptom { Name = "plantar reflex, absent"},
                    new Symptom { Name = "rash limited to feet"},
                    new Symptom { Name = "rash limited to soles of feet"},
                    new Symptom { Name = "rocker bottom feet"},
                    new Symptom { Name = "stiff big toe"},
                    new Symptom { Name = "swelling in the arch of foot"},
                    new Symptom { Name = "swollen foot"},
                    new Symptom { Name = "tingling and prickling in toe"},
                    new Symptom { Name = "tingling or prickling in foot"},
                    new Symptom { Name = "toe angle cleft"},
                    new Symptom { Name = "toe deformity"},
                    new Symptom { Name = "toe pulse absence"},
                    new Symptom { Name = "toe pulse weakness"},
                    new Symptom { Name = "toe shortness"},
                    new Symptom { Name = "tripping"},
                    new Symptom { Name = "trouble moving foot"}
                }
            };
            Symptom ToesCategory = new Symptom()
            {
                Name = "Toes Symptoms",
                Children = {
                    new Symptom { Name = "arthritis in big toe"},
                    new Symptom { Name = "big toe bends too far up"},
                    new Symptom { Name = "big toe hurts"},
                    new Symptom { Name = "big toe hurts when moving"},
                    new Symptom { Name = "big toe is under the second toe"},
                    new Symptom { Name = "big toe joint is swollen"},
                    new Symptom { Name = "big toe joint is tender to touch"},
                    new Symptom { Name = "great toe metatarsophalangeal prominence"},
                    new Symptom { Name = "great toe microdactyly"},
                    new Symptom { Name = "great toe synostosis"},
                    new Symptom { Name = "nail loss"},
                    new Symptom { Name = "nail not growing the way it should"},
                    new Symptom { Name = "nail pulling away from cuticle"},
                    new Symptom { Name = "rash limited to between toes"},
                    new Symptom { Name = "stiff knuckles in hands or toes"},
                    new Symptom { Name = "toe pain"},
                    new Symptom { Name = "up-going toe"}
                }
            };
            list.Add(ThighCategory);
            list.Add(HamstringCategory);
            list.Add(KneeCategory);
            list.Add(PoplitealCategory);
            list.Add(ShinCategory);
            list.Add(CalfCategory);
            list.Add(AnkleCategory);
            list.Add(FootCategory);
            list.Add(ToesCategory);
            return list;
        }
        private ObservableCollection<Symptom> GetSymptomDataPelvis()
        {
            var list = new ObservableCollection<Symptom>();
            Symptom HipCategory = new Symptom()
            {
                Name = "Hip Symptoms",
                Children = {
                    new Symptom { Name = "acetabular dysplasia"},
                    new Symptom { Name = "bend at hip"},
                    new Symptom { Name = "coxa valga"},
                    new Symptom { Name = "coxa vara"},
                    new Symptom { Name = "difficulty getting up from a chair"},
                    new Symptom { Name = "greater tuberosity tenderness"},
                    new Symptom { Name = "hip deformity"},
                    new Symptom { Name = "hip feels like it pops out of socket"},
                    new Symptom { Name = "hip feels stiff"},
                    new Symptom { Name = "hip hurts"},
                    new Symptom { Name = "hip is swollen"},
                    new Symptom { Name = "hip muscle is weak"},
                    new Symptom { Name = "hip tenderness"},
                    new Symptom { Name = "hurts to walk"},
                    new Symptom { Name = "ischial tuberosity tenderness"},
                    new Symptom { Name = "pelvic muscles are tight"},
                    new Symptom { Name = "pelvic muscles feel weak"},
                    new Symptom { Name = "pelvic smallness"},
                    new Symptom { Name = "pelvis tilted"},
                    new Symptom { Name = "pelvis wide"}
                }
            };
            Symptom GroinCategory = new Symptom()
            {
                Name = "Groin Symptoms",
                Children = {
                    new Symptom { Name = "darkened skin on groin"},
                    new Symptom { Name = "duroziez sign"},
                    new Symptom { Name = "feeling of heaviness in groin"},
                    new Symptom { Name = "femoral bruit"},
                    new Symptom { Name = "femoral lymph node enlargement"},
                    new Symptom { Name = "femoral pulse absence"},
                    new Symptom { Name = "femoral pulse decrease"},
                    new Symptom { Name = "groin pain"},
                    new Symptom { Name = "groin tenderness"},
                    new Symptom { Name = "hernia, femoral"},
                    new Symptom { Name = "inguinal hernia"},
                    new Symptom { Name = "inguinal lymph node abscess"},
                    new Symptom { Name = "inguinal lymph node enlargement"},
                    new Symptom { Name = "inguinal lymph node firmness"},
                    new Symptom { Name = "inguinal lymph node matting"},
                    new Symptom { Name = "inguinal lymph node tenderness"},
                    new Symptom { Name = "lump comes and goes on groin"},
                    new Symptom { Name = "lump in groin"},
                    new Symptom { Name = "painful gland in groin"},
                    new Symptom { Name = "pea-sized lump(s) on groin"},
                    new Symptom { Name = "rash limited to groin"},
                    new Symptom { Name = "redness of groin"}
                }
            };
            Symptom SuprapubicCategory = new Symptom()
            {
                Name = "Suprapubic Symptoms",
                Children = {
                    new Symptom() { Name = "pubic area swollen" },
                    new Symptom() { Name = "pubic hair early onset" },
                    new Symptom() { Name = "pubic hair lice" },
                    new Symptom() { Name = "thinning pubic hair" }
                }
            };
            Symptom GenitalsCategory = new Symptom()
            {
                Name = "Genitals Symptoms",
                Children = {
                    new Symptom { Name = "a lot of blood in urine"},
                    new Symptom { Name = "able to feel vein in scrotum"},
                    new Symptom { Name = "abnormal swelling of penis"},
                    new Symptom { Name = "balls turning blue"},
                    new Symptom { Name = "bladder distention"},
                    new Symptom { Name = "bladder edema"},
                    new Symptom { Name = "bladder erythema"},
                    new Symptom { Name = "bladder feels full"},
                    new Symptom { Name = "bladder infection"},
                    new Symptom { Name = "bladder mass"},
                    new Symptom { Name = "blood in urine"},
                    new Symptom { Name = "bloody pee"},
                    new Symptom { Name = "bloody sperm"},
                    new Symptom { Name = "bubbles in my urine"},
                    new Symptom { Name = "bulging veins in scrotum"},
                    new Symptom { Name = "can't have orgasm"},
                    new Symptom { Name = "can't pee"},
                    new Symptom { Name = "can't tell when bladder is full"},
                    new Symptom { Name = "change in bladder habits"},
                    new Symptom { Name = "chlamydial infection"},
                    new Symptom { Name = "cloudy pee"},
                    new Symptom { Name = "cremasteric reflex absent, unilateral"},
                    new Symptom { Name = "cyst on genitals"},
                    new Symptom { Name = "cyst on testicle"},
                    new Symptom { Name = "dark pee"},
                    new Symptom { Name = "decreased sex drive"},
                    new Symptom { Name = "deformed scrotum"},
                    new Symptom { Name = "delayed or late period"},
                    new Symptom { Name = "difficult to pee"},
                    new Symptom { Name = "discharge from penis"},
                    new Symptom { Name = "double ureter"},
                    new Symptom { Name = "enlarged prostate"},
                    new Symptom { Name = "epididymal mass"},
                    new Symptom { Name = "epididymal tenderness"},
                    new Symptom { Name = "epididymitis"},
                    new Symptom { Name = "erection that won't go down or soften"},
                    new Symptom { Name = "feels like need to pee all the time"},
                    new Symptom { Name = "firm pus filled rash around head of penis"},
                    new Symptom { Name = "foreskin stuck over head of penis"},
                    new Symptom { Name = "foreskin stuck to penis"},
                    new Symptom { Name = "genital abnormality"},
                    new Symptom { Name = "genital necrosis"},
                    new Symptom { Name = "genital numbness"},
                    new Symptom { Name = "genital pain"},
                    new Symptom { Name = "genital underdevelopment"},
                    new Symptom { Name = "genitalia, ambiguous"},
                    new Symptom { Name = "genitals getting larger"},
                    new Symptom { Name = "genitals itching"},
                    new Symptom { Name = "genitals swollen"},
                    new Symptom { Name = "glans penis calculus"},
                    new Symptom { Name = "glans penis scar tissue formation"},
                    new Symptom { Name = "gonad disorder"},
                    new Symptom { Name = "gonadal hypoplasia"},
                    new Symptom { Name = "gray skin peeling off penis"},
                    new Symptom { Name = "green urine"},
                    new Symptom { Name = "hard bump(s) around head of penis"},
                    new Symptom { Name = "hard bump(s) on head of penis"},
                    new Symptom { Name = "hard pus-filled rash on head of penis"},
                    new Symptom { Name = "head of penis curves downward"},
                    new Symptom { Name = "head of penis hurts"},
                    new Symptom { Name = "head of penis is irritated"},
                    new Symptom { Name = "head of penis is red and swollen"},
                    new Symptom { Name = "head of penis is swollen"},
                    new Symptom { Name = "hematuria, microscopic"},
                    new Symptom { Name = "hemoglobinuria"},
                    new Symptom { Name = "hives on penis"},
                    new Symptom { Name = "hurts to ejaculate or cum"},
                    new Symptom { Name = "immediate urge to pee"},
                    new Symptom { Name = "impotence"},
                    new Symptom { Name = "incontinence"},
                    new Symptom { Name = "infected testicles"},
                    new Symptom { Name = "infertility"},
                    new Symptom { Name = "inflamed scrotum"},
                    new Symptom { Name = "inflammation of urinary tract"},
                    new Symptom { Name = "irritation between butt and genitals"},
                    new Symptom { Name = "itching on urethra"},
                    new Symptom { Name = "iud in place"},
                    new Symptom { Name = "large blister(s) on penis"},
                    new Symptom { Name = "large non-emptying bladder"},
                    new Symptom { Name = "large penis"},
                    new Symptom { Name = "light colored pee"},
                    new Symptom { Name = "lump between butt and genitals"},
                    new Symptom { Name = "lump in genital area"},
                    new Symptom { Name = "lump in urinary tract"},
                    new Symptom { Name = "lump on penis"},
                    new Symptom { Name = "lump on scrotum"},
                    new Symptom { Name = "lump on testicle"},
                    new Symptom { Name = "lymphogranuloma venereum"},
                    new Symptom { Name = "man ejaculates sooner during sexual intercourse than he or his partner would like"},
                    new Symptom { Name = "massive scrotal swelling"},
                    new Symptom { Name = "muscle twitching in genital area"},
                    new Symptom { Name = "need to pee often"},
                    new Symptom { Name = "open sore(s) around head of penis"},
                    new Symptom { Name = "open sore(s) between butt and genitals"},
                    new Symptom { Name = "open sore(s) on genitals"},
                    new Symptom { Name = "open sore(s) on head of penis"},
                    new Symptom { Name = "open sore(s) on penis"},
                    new Symptom { Name = "open sore(s) on urethra"},
                    new Symptom { Name = "opening of urinary tract is blocked"},
                    new Symptom { Name = "orange pee"},
                    new Symptom { Name = "pain at the opening of urinary tract"},
                    new Symptom { Name = "pain between butt and genitals"},
                    new Symptom { Name = "pain in cord running vertically behind testicle"},
                    new Symptom { Name = "pain in testicle"},
                    new Symptom { Name = "pain in testicle or ovary"},
                    new Symptom { Name = "pain in tube behind testicle"},
                    new Symptom { Name = "pain while peeing"},
                    new Symptom { Name = "painful erection"},
                    new Symptom { Name = "painless ulcer on the genitals"},
                    new Symptom { Name = "passing small kidney stones"},
                    new Symptom { Name = "pea-sized lump(s) on prostate"},
                    new Symptom { Name = "pee comes out of top of penis"},
                    new Symptom { Name = "pee hole is on bottom side of penis"},
                    new Symptom { Name = "pee more than usual"},
                    new Symptom { Name = "pee too much at night"},
                    new Symptom { Name = "pelvic calculus palpable, rectum"},
                    new Symptom { Name = "pelvic mass"},
                    new Symptom { Name = "penis hurts"},
                    new Symptom { Name = "penis is red"},
                    new Symptom { Name = "penis is red and irritated"},
                    new Symptom { Name = "penis pulled in"},
                    new Symptom { Name = "penis tenderness"},
                    new Symptom { Name = "prostate fluctuance"},
                    new Symptom { Name = "prostate hardening"},
                    new Symptom { Name = "prostate infection"},
                    new Symptom { Name = "prostate pain"},
                    new Symptom { Name = "prostate tenderness"},
                    new Symptom { Name = "prostatitis"},
                    new Symptom { Name = "rash limited to genitals"},
                    new Symptom { Name = "rectovaginal fistula"},
                    new Symptom { Name = "red bump(s) around head of penis"},
                    new Symptom { Name = "red bump(s) on head of penis"},
                    new Symptom { Name = "redness around urinary tract"},
                    new Symptom { Name = "redness of private parts"},
                    new Symptom { Name = "redness of testicle sac"},
                    new Symptom { Name = "scrotal mass"},
                    new Symptom { Name = "scrotal mass, firm"},
                    new Symptom { Name = "scrotal pulling sensation"},
                    new Symptom { Name = "scrotal ulceration"},
                    new Symptom { Name = "scrotum cyst"},
                    new Symptom { Name = "scrotum hurts"},
                    new Symptom { Name = "scrotum pain goes away when lift testicle"},
                    new Symptom { Name = "seminal vesicular induration"},
                    new Symptom { Name = "seminal vesicular swelling"},
                    new Symptom { Name = "sexual desire increased"},
                    new Symptom { Name = "shrunken testicles"},
                    new Symptom { Name = "small penis"},
                    new Symptom { Name = "spermatic cord cyst"},
                    new Symptom { Name = "spermatic cord enlargement"},
                    new Symptom { Name = "spermatic cord hydrocele"},
                    new Symptom { Name = "spermatic cord inflammation"},
                    new Symptom { Name = "spermatic cord mass"},
                    new Symptom { Name = "spermatic cord tenderness"},
                    new Symptom { Name = "spermatic cord torsion"},
                    new Symptom { Name = "std transmission"},
                    new Symptom { Name = "sterile pyuria"},
                    new Symptom { Name = "stopping the flow of urine"},
                    new Symptom { Name = "swelling at opening of urinary tract"},
                    new Symptom { Name = "swelling between butt and genitals"},
                    new Symptom { Name = "swollen scrotum"},
                    new Symptom { Name = "swollen testicle"},
                    new Symptom { Name = "tenderness of private parts"},
                    new Symptom { Name = "testicle feels squishy"},
                    new Symptom { Name = "testicle riding too high"},
                    new Symptom { Name = "testicles feel tight"},
                    new Symptom { Name = "testicles hurt to touch"},
                    new Symptom { Name = "testicles never fully developed"},
                    new Symptom { Name = "tight scrotum"},
                    new Symptom { Name = "trouble starting to pee"},
                    new Symptom { Name = "unable to pee"},
                    new Symptom { Name = "uncircumcised penis"},
                    new Symptom { Name = "undescended testicles"},
                    new Symptom { Name = "ureteral mass"},
                    new Symptom { Name = "urethral fistula"},
                    new Symptom { Name = "urethral meatus protrusion"},
                    new Symptom { Name = "urethral obstruction"},
                    new Symptom { Name = "urethral pain"},
                    new Symptom { Name = "urinary incontinence"},
                    new Symptom { Name = "urinary tract abnormality"},
                    new Symptom { Name = "urinary tract infection"},
                    new Symptom { Name = "urinary tract obstruction"},
                    new Symptom { Name = "urinating less"},
                    new Symptom { Name = "urinating stool"},
                    new Symptom { Name = "vas deferens swelling"},
                    new Symptom { Name = "vas deferens tenderness"},
                    new Symptom { Name = "weak pee stream"},
                    new Symptom { Name = "wet dream"}
                }
            };
            list.Add(HipCategory);
            list.Add(GroinCategory);
            list.Add(SuprapubicCategory);
            list.Add(GenitalsCategory);
            return list;
        }
        private ObservableCollection<Symptom> GetSymptomDataNeck()
        {
            var list = new ObservableCollection<Symptom>();
            Symptom NeckCategory = new Symptom()
            {
                Name = "Neck Symptoms",
                Children = {
                    new Symptom { Name = "blister(s) on back of throat"},
                    new Symptom { Name = "brown mucous in throat"},
                    new Symptom { Name = "burn back of throat"},
                    new Symptom { Name = "can't bend head forward"},
                    new Symptom { Name = "can't turn head"},
                    new Symptom { Name = "carotid artery bruit"},
                    new Symptom { Name = "carotid artery distention"},
                    new Symptom { Name = "carotid artery mass"},
                    new Symptom { Name = "carotid pulse absence"},
                    new Symptom { Name = "carotid pulse increase"},
                    new Symptom { Name = "carotodynia"},
                    new Symptom { Name = "cervical erosion"},
                    new Symptom { Name = "cervical lymph node bleeding"},
                    new Symptom { Name = "cervical stenosis"},
                    new Symptom { Name = "choking"},
                    new Symptom { Name = "choking sensation"},
                    new Symptom { Name = "clearly outlined pea-sized lump on neck"},
                    new Symptom { Name = "cough"},
                    new Symptom { Name = "cracking sound in neck"},
                    new Symptom { Name = "cricothyroid paralysis"},
                    new Symptom { Name = "enlarged jugular vein"},
                    new Symptom { Name = "epiglottic enlargement"},
                    new Symptom { Name = "epiglottic erythema"},
                    new Symptom { Name = "epiglottis swelling"},
                    new Symptom { Name = "epiglottitis"},
                    new Symptom { Name = "episodes of not breathing during sleep"},
                    new Symptom { Name = "feel pressure on neck"},
                    new Symptom { Name = "feels like something is stuck in my throat"},
                    new Symptom { Name = "food comes back up"},
                    new Symptom { Name = "food or liquid goes down wrong pipe"},
                    new Symptom { Name = "globus major nodule"},
                    new Symptom { Name = "hair on neck feels tender"},
                    new Symptom { Name = "hair roots on neck are red"},
                    new Symptom { Name = "hashimoto disease"},
                    new Symptom { Name = "head turned to one side"},
                    new Symptom { Name = "hepatojugular reflux"},
                    new Symptom { Name = "high pitched breathing"},
                    new Symptom { Name = "infected lump or sore on neck"},
                    new Symptom { Name = "itchy neck"},
                    new Symptom { Name = "itchy throat"},
                    new Symptom { Name = "jugular vein a wave increased"},
                    new Symptom { Name = "jugular venous distention with inspiration"},
                    new Symptom { Name = "laryngeal anesthesia"},
                    new Symptom { Name = "laryngeal crepitation"},
                    new Symptom { Name = "laryngeal dryness"},
                    new Symptom { Name = "laryngeal edema"},
                    new Symptom { Name = "laryngeal erythema"},
                    new Symptom { Name = "laryngeal hematoma"},
                    new Symptom { Name = "laryngeal mass"},
                    new Symptom { Name = "laryngeal mobility increase"},
                    new Symptom { Name = "laryngeal obstruction"},
                    new Symptom { Name = "laryngeal pain"},
                    new Symptom { Name = "laryngeal papilloma"},
                    new Symptom { Name = "laryngeal pressure sensation"},
                    new Symptom { Name = "laryngeal stenosis"},
                    new Symptom { Name = "laryngeal tenderness"},
                    new Symptom { Name = "laryngeal ulceration"},
                    new Symptom { Name = "laryngitis"},
                    new Symptom { Name = "lump on neck"},
                    new Symptom { Name = "lump on one side of neck"},
                    new Symptom { Name = "lump on one side of throat"},
                    new Symptom { Name = "lump on the front of neck"},
                    new Symptom { Name = "nasopharyngeal induration"},
                    new Symptom { Name = "neck bones fused together"},
                    new Symptom { Name = "neck bones sticking out"},
                    new Symptom { Name = "neck does not sweat"},
                    new Symptom { Name = "neck fascia thickening"},
                    new Symptom { Name = "neck has changed colors"},
                    new Symptom { Name = "neck hurts"},
                    new Symptom { Name = "neck is blue"},
                    new Symptom { Name = "neck is red"},
                    new Symptom { Name = "neck is swollen"},
                    new Symptom { Name = "neck lymph node too big"},
                    new Symptom { Name = "neck mass, anterior cervical"},
                    new Symptom { Name = "neck mass, posterior cervical"},
                    new Symptom { Name = "neck muscles are weak"},
                    new Symptom { Name = "neck subcutaneous emphysema"},
                    new Symptom { Name = "neck tender to touch"},
                    new Symptom { Name = "neck vasodilatation"},
                    new Symptom { Name = "neck vessel bruit"},
                    new Symptom { Name = "no fat in neck"},
                    new Symptom { Name = "open sore(s) on back of throat"},
                    new Symptom { Name = "orange mucous in throat"},
                    new Symptom { Name = "pain on one side of throat"},
                    new Symptom { Name = "pain when i swallow"},
                    new Symptom { Name = "painful swollen gland in front part of neck"},
                    new Symptom { Name = "pea-sized lump(s) in neck"},
                    new Symptom { Name = "pharyngeal mucous membrane edema"},
                    new Symptom { Name = "pharyngeal paralysis"},
                    new Symptom { Name = "prickling or tingling in neck"},
                    new Symptom { Name = "pus-filled bump(s) in neck hair follicle(s)"},
                    new Symptom { Name = "rash limited to neck"},
                    new Symptom { Name = "red bumps around hair follicles on neck"},
                    new Symptom { Name = "red open sore(s) on neck"},
                    new Symptom { Name = "red pea-sized lump(s) in lining of throat"},
                    new Symptom { Name = "removal of thyroid gland"},
                    new Symptom { Name = "short neck"},
                    new Symptom { Name = "small red or purple spots on back of throat"},
                    new Symptom { Name = "sore throat"},
                    new Symptom { Name = "spider vein(s) on neck"},
                    new Symptom { Name = "sternocleidomastoid muscle paralysis"},
                    new Symptom { Name = "stiff neck"},
                    new Symptom { Name = "swelling at back of throat"},
                    new Symptom { Name = "tender neck lymph node"},
                    new Symptom { Name = "throat bleeding"},
                    new Symptom { Name = "throat burning sensation"},
                    new Symptom { Name = "throat clearing"},
                    new Symptom { Name = "throat dryness"},
                    new Symptom { Name = "throat feels numb"},
                    new Symptom { Name = "throat feels tender"},
                    new Symptom { Name = "throat feels weak"},
                    new Symptom { Name = "throat is red"},
                    new Symptom { Name = "throat spasm"},
                    new Symptom { Name = "thyroid bruit"},
                    new Symptom { Name = "thyroid enlargement"},
                    new Symptom { Name = "thyroid nodule"},
                    new Symptom { Name = "tightness in throat"},
                    new Symptom { Name = "tingling and prickling in throat"},
                    new Symptom { Name = "tracheal compression"},
                    new Symptom { Name = "trouble swallowing"},
                    new Symptom { Name = "tumor on back of throat"},
                    new Symptom { Name = "voice deepening"},
                    new Symptom { Name = "voice is hoarse"},
                    new Symptom { Name = "webbing on side of neck"},
                    new Symptom { Name = "whispered pectoriloquy"},
                    new Symptom { Name = "white mucous in throat"},
                    new Symptom { Name = "white stuff on throat"},
                    new Symptom { Name = "windpipe is shifted"}
                }
            };
            list.Add(NeckCategory);
            return list;
        }
        private ObservableCollection<Symptom> GetSymptomDataHead()
        {
            var list = new ObservableCollection<Symptom>();
            Symptom ScalpCategory = new Symptom()
            {
                Name = "Scalp Symptoms",
                Children = {
                    new Symptom { Name = "baby's soft spot is bulging"},
                    new Symptom { Name = "baby's soft spot is still open"},
                    new Symptom { Name = "baby's soft spot is sunken"},
                    new Symptom { Name = "baby's soft spot is tight"},
                    new Symptom { Name = "bald spots (hair)"},
                    new Symptom { Name = "blond hair"},
                    new Symptom { Name = "bulging out of back of skull"},
                    new Symptom { Name = "clear ringing sound note when tapping the skull"},
                    new Symptom { Name = "clogged pores in bald spots"},
                    new Symptom { Name = "complete loss of hair over entire body"},
                    new Symptom { Name = "completely bald"},
                    new Symptom { Name = "cranial osteoma"},
                    new Symptom { Name = "craniosynostosis"},
                    new Symptom { Name = "cut on scalp"},
                    new Symptom { Name = "dandruff"},
                    new Symptom { Name = "deformed forehead"},
                    new Symptom { Name = "dry scalp"},
                    new Symptom { Name = "dull sound when tapping the skull"},
                    new Symptom { Name = "early grey hair"},
                    new Symptom { Name = "enlarged vein on scalp"},
                    new Symptom { Name = "flaky or greasy skin on scalp"},
                    new Symptom { Name = "follicular scarring"},
                    new Symptom { Name = "forehead bones breaking down"},
                    new Symptom { Name = "forehead sticks out"},
                    new Symptom { Name = "hair dryness"},
                    new Symptom { Name = "hair getting straighter"},
                    new Symptom { Name = "hair loss with crusty rash"},
                    new Symptom { Name = "hair loss with scarring"},
                    new Symptom { Name = "hair sparse"},
                    new Symptom { Name = "head lice"},
                    new Symptom { Name = "headache"},
                    new Symptom { Name = "increased pressure in skull"},
                    new Symptom { Name = "intracranial bruit"},
                    new Symptom { Name = "itchy scalp"},
                    new Symptom { Name = "long hair"},
                    new Symptom { Name = "long nails"},
                    new Symptom { Name = "losing hair"},
                    new Symptom { Name = "losing hair in patch(es)"},
                    new Symptom { Name = "low hairline"},
                    new Symptom { Name = "lump on scalp"},
                    new Symptom { Name = "male pattern baldness"},
                    new Symptom { Name = "matted hair"},
                    new Symptom { Name = "open pores in bald spots"},
                    new Symptom { Name = "open sore(s) on scalp"},
                    new Symptom { Name = "pointed head"},
                    new Symptom { Name = "pulling out hair"},
                    new Symptom { Name = "pus-filled bump(s) in bald spot"},
                    new Symptom { Name = "pus-filled bump(s) in scalp hair follicle(s)"},
                    new Symptom { Name = "random hairs in bald patch(es)"},
                    new Symptom { Name = "random white hairs in bald spots"},
                    new Symptom { Name = "rash limited to scalp"},
                    new Symptom { Name = "red bumps around hair follicles on scalp"},
                    new Symptom { Name = "red hair"},
                    new Symptom { Name = "red skin in bald areas"},
                    new Symptom { Name = "redness and dry scaly skin with hair loss"},
                    new Symptom { Name = "scalp feels overly sensitive"},
                    new Symptom { Name = "scalp feels warm"},
                    new Symptom { Name = "scalp hurts"},
                    new Symptom { Name = "scalp tender to touch"},
                    new Symptom { Name = "scalp vessel pulse increase"},
                    new Symptom { Name = "seams of skull separate"},
                    new Symptom { Name = "shiny bald head"},
                    new Symptom { Name = "shiny scalp"},
                    new Symptom { Name = "skin on scalp feels thinner"},
                    new Symptom { Name = "soft skull"},
                    new Symptom { Name = "swollen scalp"},
                    new Symptom { Name = "tough or thick skin on scalp"},
                    new Symptom { Name = "white hair"},
                    new Symptom { Name = "widespread loss of hair"}
            }
            };
            Symptom ForeheadCategory = new Symptom()
            {
                Name = "Forehead Symptoms",
                Children = {
                    new Symptom { Name = "can't pay attention"},
                    new Symptom { Name = "confused thinking and reduced awareness of your environment"},
                    new Symptom { Name = "enlarged vein on forehead"},
                    new Symptom { Name = "fatigue"},
                    new Symptom { Name = "fever"},
                    new Symptom { Name = "forehead is tender"},
                    new Symptom { Name = "forehead sticks out"},
                    new Symptom { Name = "hairy forehead"},
                    new Symptom { Name = "hallucination"},
                    new Symptom { Name = "headache"},
                    new Symptom { Name = "headache in front of head"},
                    new Symptom { Name = "high forehead"},
                    new Symptom { Name = "inappropriate behavior"},
                    new Symptom { Name = "lightheadedness"},
                    new Symptom { Name = "paranoia"},
                    new Symptom { Name = "wide forehead"},
                    new Symptom { Name = "wrinkled forehead"}
            }
            };
            Symptom EyesCategory = new Symptom()
            {
                Name = "Eyes Symptoms",
                Children = {
                    new Symptom { Name = "argyll-robertson pupil"},
                    new Symptom { Name = "astigmatism"},
                    new Symptom { Name = "black eye"},
                    new Symptom { Name = "black spots floating in my eye"},
                    new Symptom { Name = "bleeding around the eye"},
                    new Symptom { Name = "bleeding eyelid"},
                    new Symptom { Name = "bleeding in eye"},
                    new Symptom { Name = "bleeding in front part of eye"},
                    new Symptom { Name = "blind spot"},
                    new Symptom { Name = "blind spot that appears blank"},
                    new Symptom { Name = "blind spot that appears dark"},
                    new Symptom { Name = "blood vessels in colored part of eye"},
                    new Symptom { Name = "blurry vision"},
                    new Symptom { Name = "blurry vision in one eye"},
                    new Symptom { Name = "brownish-yellowish ring around the color of eye"},
                    new Symptom { Name = "bruising around eyes"},
                    new Symptom { Name = "bugs in my eye"},
                    new Symptom { Name = "bulging eyes"},
                    new Symptom { Name = "burn to part of eye"},
                    new Symptom { Name = "bushy eyebrows"},
                    new Symptom { Name = "can't close eye all the way"},
                    new Symptom { Name = "can't focus eyes"},
                    new Symptom { Name = "can't look up"},
                    new Symptom { Name = "can't move eyes to the side"},
                    new Symptom { Name = "can't recognize things i see"},
                    new Symptom { Name = "can't turn my eyes"},
                    new Symptom { Name = "can? see far away"},
                    new Symptom { Name = "can? see up close"},
                    new Symptom { Name = "cataract"},
                    new Symptom { Name = "central scotoma"},
                    new Symptom { Name = "central vision loss"},
                    new Symptom { Name = "chorioretinitis"},
                    new Symptom { Name = "choroid coloboma"},
                    new Symptom { Name = "choroiditis"},
                    new Symptom { Name = "cloudy area on cornea"},
                    new Symptom { Name = "color blindness"},
                    new Symptom { Name = "conjunctival contracture"},
                    new Symptom { Name = "conjunctival fold necrosis, yellow white"},
                    new Symptom { Name = "conjunctival papillary flatness"},
                    new Symptom { Name = "conjunctival papillary hardening"},
                    new Symptom { Name = "conjunctival pseudomembrane"},
                    new Symptom { Name = "conjunctival smoothness"},
                    new Symptom { Name = "conjunctivitis, follicular"},
                    new Symptom { Name = "constant red dry eyes"},
                    new Symptom { Name = "corneal areflexia"},
                    new Symptom { Name = "corneal contracture"},
                    new Symptom { Name = "corneal dystrophy"},
                    new Symptom { Name = "corneal protrusion"},
                    new Symptom { Name = "cross eyed"},
                    new Symptom { Name = "decreased tears"},
                    new Symptom { Name = "decreased vision"},
                    new Symptom { Name = "dilated pupil"},
                    new Symptom { Name = "distorted vision"},
                    new Symptom { Name = "double vision"},
                    new Symptom { Name = "double vision in one eye"},
                    new Symptom { Name = "downslanting palpebral fissues"},
                    new Symptom { Name = "drooping eyelid"},
                    new Symptom { Name = "drusen"},
                    new Symptom { Name = "dry eyes"},
                    new Symptom { Name = "enlarged brow ridge"},
                    new Symptom { Name = "enlarged vein on clear part of eye"},
                    new Symptom { Name = "extraocular muscle imbalance"},
                    new Symptom { Name = "extraocular muscle weakness"},
                    new Symptom { Name = "eye bleeds"},
                    new Symptom { Name = "eye blinking"},
                    new Symptom { Name = "eye color is changing to blue"},
                    new Symptom { Name = "eye contact impairment"},
                    new Symptom { Name = "eye discharge"},
                    new Symptom { Name = "eye hurts"},
                    new Symptom { Name = "eye hurts when i move it"},
                    new Symptom { Name = "eye is burning"},
                    new Symptom { Name = "eye is red and irritated"},
                    new Symptom { Name = "eye not in normal position in socket"},
                    new Symptom { Name = "eye opening is narrow"},
                    new Symptom { Name = "eye pain"},
                    new Symptom { Name = "eye socket hurts"},
                    new Symptom { Name = "eye socket is sinking inward"},
                    new Symptom { Name = "eye strain"},
                    new Symptom { Name = "eye too large"},
                    new Symptom { Name = "eye twitching"},
                    new Symptom { Name = "eyeball is swollen"},
                    new Symptom { Name = "eyebrow hair loss"},
                    new Symptom { Name = "eyebrow lice"},
                    new Symptom { Name = "eyebrow loss of color"},
                    new Symptom { Name = "eyelid closes too slowly"},
                    new Symptom { Name = "eyelid crusting and sticking together"},
                    new Symptom { Name = "eyelid feels like it is burning"},
                    new Symptom { Name = "eyelid feels scratchy"},
                    new Symptom { Name = "eyelid feels thick"},
                    new Symptom { Name = "eyelid flipped up"},
                    new Symptom { Name = "eyelid folds inward"},
                    new Symptom { Name = "eyelid granulation"},
                    new Symptom { Name = "eyelid hurts"},
                    new Symptom { Name = "eyelid is red and irritated"},
                    new Symptom { Name = "eyelid pink gray"},
                    new Symptom { Name = "eyelid pus"},
                    new Symptom { Name = "eyelid tender to touch"},
                    new Symptom { Name = "eyelid twitching"},
                    new Symptom { Name = "eyelids feel hard"},
                    new Symptom { Name = "eyelids feel heavy"},
                    new Symptom { Name = "eyes are irritated"},
                    new Symptom { Name = "eyes bulge out"},
                    new Symptom { Name = "eyes don't move together"},
                    new Symptom { Name = "eyes fixed on a single location"},
                    new Symptom { Name = "eyes rolling back"},
                    new Symptom { Name = "eyes tearing more"},
                    new Symptom { Name = "eyes wide open"},
                    new Symptom { Name = "eyesight getting worse"},
                    new Symptom { Name = "eyesight worse in one eye"},
                    new Symptom { Name = "feeling pressure below the eye"},
                    new Symptom { Name = "flashing lights in vision"},
                    new Symptom { Name = "flickering uncolored zig-zag lines in vision"},
                    new Symptom { Name = "frequent squinting"},
                    new Symptom { Name = "front part of eye is swollen"},
                    new Symptom { Name = "front part of eye looks cloudy"},
                    new Symptom { Name = "glaucoma"},
                    new Symptom { Name = "grayish/brown spots on the outside of colored part of eye"},
                    new Symptom { Name = "growth that looks like a yellow spot or bump on the eyeball"},
                    new Symptom { Name = "hemianopia"},
                    new Symptom { Name = "hole in colored part of eye"},
                    new Symptom { Name = "hypopyon"},
                    new Symptom { Name = "infected lump or sore on eyelid"},
                    new Symptom { Name = "inner corner of eye is swollen"},
                    new Symptom { Name = "intraocular pressure decrease"},
                    new Symptom { Name = "intraocular pressure increase"},
                    new Symptom { Name = "iridocyclitis"},
                    new Symptom { Name = "iritis"},
                    new Symptom { Name = "irritated eye"},
                    new Symptom { Name = "itchy eye"},
                    new Symptom { Name = "itchy eyelid"},
                    new Symptom { Name = "keratitis"},
                    new Symptom { Name = "lacrimal gland lobulation"},
                    new Symptom { Name = "lacrimal sac inflammation"},
                    new Symptom { Name = "large blister on the eye"},
                    new Symptom { Name = "lateral vision loss"},
                    new Symptom { Name = "lazy eye"},
                    new Symptom { Name = "lens dislocation"},
                    new Symptom { Name = "light hurts eyes"},
                    new Symptom { Name = "little eyes"},
                    new Symptom { Name = "losing eye color"},
                    new Symptom { Name = "losing eyelashes"},
                    new Symptom { Name = "loss of an area of vision"},
                    new Symptom { Name = "loss of an area of vision in both eyes"},
                    new Symptom { Name = "loss of an area of vision in one eye"},
                    new Symptom { Name = "loss of color vision in one spot"},
                    new Symptom { Name = "loss of eyelashes"},
                    new Symptom { Name = "loss of vision in both eyes"},
                    new Symptom { Name = "loss of vision in one eye"},
                    new Symptom { Name = "lump in eye socket"},
                    new Symptom { Name = "maculae ceruleae"},
                    new Symptom { Name = "macular degeneration"},
                    new Symptom { Name = "mucus coming from the eye"},
                    new Symptom { Name = "no color in eye"},
                    new Symptom { Name = "no peripheral vision"},
                    new Symptom { Name = "not able to make tears"},
                    new Symptom { Name = "nystagmus"},
                    new Symptom { Name = "nystagmus latency"},
                    new Symptom { Name = "nystagmus reversal"},
                    new Symptom { Name = "nystagmus, fatigue"},
                    new Symptom { Name = "nystagmus, rotary"},
                    new Symptom { Name = "ocular cherry red spot"},
                    new Symptom { Name = "one eye bulges"},
                    new Symptom { Name = "one eye not turning"},
                    new Symptom { Name = "one eye sees better than the other"},
                    new Symptom { Name = "one eyelid swollen"},
                    new Symptom { Name = "one or both eyes look downward"},
                    new Symptom { Name = "one or both eyes look to the side"},
                    new Symptom { Name = "open sore(s) on colored part of eye"},
                    new Symptom { Name = "open sore(s) on eye"},
                    new Symptom { Name = "open sore(s) on eyelid"},
                    new Symptom { Name = "opening snap"},
                    new Symptom { Name = "optic nerve atrophy"},
                    new Symptom { Name = "optic neuritis"},
                    new Symptom { Name = "pain around the eye"},
                    new Symptom { Name = "pain behind the eye"},
                    new Symptom { Name = "painful and weak eye movement"},
                    new Symptom { Name = "papilledema"},
                    new Symptom { Name = "part of outer layer of eye sticking to another part"},
                    new Symptom { Name = "peripheral vision loss"},
                    new Symptom { Name = "pink eye"},
                    new Symptom { Name = "pinpoint pupils"},
                    new Symptom { Name = "poor night vision"},
                    new Symptom { Name = "prominent brow ridge"},
                    new Symptom { Name = "pupil fixed"},
                    new Symptom { Name = "pupil irregularity"},
                    new Symptom { Name = "pupillary deformity"},
                    new Symptom { Name = "pupillary inequality"},
                    new Symptom { Name = "pupillary whiteness"},
                    new Symptom { Name = "pus coming from the eye"},
                    new Symptom { Name = "rash limited to eyelid"},
                    new Symptom { Name = "red color blindness"},
                    new Symptom { Name = "red eye"},
                    new Symptom { Name = "red eyelid"},
                    new Symptom { Name = "retinal angioid streaks"},
                    new Symptom { Name = "retinal bleeding"},
                    new Symptom { Name = "retinal coloboma"},
                    new Symptom { Name = "retinal detachment"},
                    new Symptom { Name = "retinal exudate"},
                    new Symptom { Name = "retinal granuloma"},
                    new Symptom { Name = "retinal opacity"},
                    new Symptom { Name = "retinal pallor"},
                    new Symptom { Name = "retinal pigmentation"},
                    new Symptom { Name = "retinitis"},
                    new Symptom { Name = "roth spots"},
                    new Symptom { Name = "scar on clear part eye"},
                    new Symptom { Name = "scar on the eye"},
                    new Symptom { Name = "seeing halos of light around things"},
                    new Symptom { Name = "severe eye pain"},
                    new Symptom { Name = "single red eye"},
                    new Symptom { Name = "skin and eyes more sensitive to sunlight"},
                    new Symptom { Name = "skin folded over upper eyelid"},
                    new Symptom { Name = "slower blinking"},
                    new Symptom { Name = "small blister on eye"},
                    new Symptom { Name = "small dot of light or zigzag shape in your vision"},
                    new Symptom { Name = "small flat spots of loss of skin color"},
                    new Symptom { Name = "something stuck inside the eye"},
                    new Symptom { Name = "sore eye"},
                    new Symptom { Name = "specks or spots in colored part of eye"},
                    new Symptom { Name = "spider vein(s) in eye"},
                    new Symptom { Name = "stye"},
                    new Symptom { Name = "sunken eyes"},
                    new Symptom { Name = "swelling around the eyes"},
                    new Symptom { Name = "swollen eyelid"},
                    new Symptom { Name = "swollen tear duct"},
                    new Symptom { Name = "tear duct hurts"},
                    new Symptom { Name = "tear duct is red"},
                    new Symptom { Name = "tearing in one eye"},
                    new Symptom { Name = "temporary vision loss"},
                    new Symptom { Name = "things appear smaller than they are"},
                    new Symptom { Name = "things in vision appear yellowish"},
                    new Symptom { Name = "thinning eyebrows"},
                    new Symptom { Name = "thinning eyelashes"},
                    new Symptom { Name = "third nerve paralysis"},
                    new Symptom { Name = "tiny red or purple spots limited to inside eye"},
                    new Symptom { Name = "torn eyelid"},
                    new Symptom { Name = "trouble looking up"},
                    new Symptom { Name = "trouble moving eyes"},
                    new Symptom { Name = "trouble opening eye"},
                    new Symptom { Name = "tumor on one eye"},
                    new Symptom { Name = "twitching of colored part of eye"},
                    new Symptom { Name = "unable to see clearly"},
                    new Symptom { Name = "uveitis"},
                    new Symptom { Name = "uveitis, bilateral"},
                    new Symptom { Name = "violet color to eyelid"},
                    new Symptom { Name = "vision loss"},
                    new Symptom { Name = "visual aura"},
                    new Symptom { Name = "visual hallucination"},
                    new Symptom { Name = "vitreous hemorrhage"},
                    new Symptom { Name = "watery eyes"},
                    new Symptom { Name = "white part of eye is black"},
                    new Symptom { Name = "white part of eye is blue"},
                    new Symptom { Name = "white part of eye is white"},
                    new Symptom { Name = "white patch(es) around eye"},
                    new Symptom { Name = "white, grey or blue ring seen around color part of eye"},
                    new Symptom { Name = "whitish material on eyelid"},
                    new Symptom { Name = "wideset eyes"},
                    new Symptom { Name = "wrinkle between eyebrows"},
                    new Symptom { Name = "yellow eyes"},
                    new Symptom { Name = "yellow open sore(s) on eye"},
                    new Symptom { Name = "yellow pea-sized lump(s) on eyelid"}
            }
            };
            Symptom NoseCategory = new Symptom()
            {
                Name = "Nose Symptoms",
                Children = {
                    new Symptom { Name = "along smile or laugh lines are red"},
                    new Symptom { Name = "blockage in nose"},
                    new Symptom { Name = "bloody nose"},
                    new Symptom { Name = "boil on nose"},
                    new Symptom { Name = "bridge of nose looks flat"},
                    new Symptom { Name = "clear runny nose"},
                    new Symptom { Name = "cleft nose"},
                    new Symptom { Name = "dented nostril"},
                    new Symptom { Name = "deviated septum"},
                    new Symptom { Name = "dry nasal passages"},
                    new Symptom { Name = "growth in nose"},
                    new Symptom { Name = "hay fever"},
                    new Symptom { Name = "head congestion"},
                    new Symptom { Name = "high nose bridge"},
                    new Symptom { Name = "hooked nose"},
                    new Symptom { Name = "inside of nose is black"},
                    new Symptom { Name = "inside of nose is red"},
                    new Symptom { Name = "inside of nose is swollen"},
                    new Symptom { Name = "irritated nose"},
                    new Symptom { Name = "itchy nose"},
                    new Symptom { Name = "low nose bridge"},
                    new Symptom { Name = "nasal sinus draining"},
                    new Symptom { Name = "nasal sinus feels full"},
                    new Symptom { Name = "nasal sinus is blocked"},
                    new Symptom { Name = "nasal sinus pain"},
                    new Symptom { Name = "nasal sinus sore"},
                    new Symptom { Name = "nose and throat are inflamed"},
                    new Symptom { Name = "nose destruction"},
                    new Symptom { Name = "nose discharge, foul smelling, unilateral"},
                    new Symptom { Name = "nose discharge, purulent, unilateral"},
                    new Symptom { Name = "nose feels like it is burning"},
                    new Symptom { Name = "nose flares open"},
                    new Symptom { Name = "nose getting bigger"},
                    new Symptom { Name = "nose hair burned"},
                    new Symptom { Name = "nose hurts"},
                    new Symptom { Name = "nose is turned upward"},
                    new Symptom { Name = "nose misshapen"},
                    new Symptom { Name = "nose mucous membrane atrophy"},
                    new Symptom { Name = "nose oral communication"},
                    new Symptom { Name = "nose septum destruction"},
                    new Symptom { Name = "nose septum necrosis"},
                    new Symptom { Name = "nose septum perforation"},
                    new Symptom { Name = "nose septum ulceration"},
                    new Symptom { Name = "nose skin infected"},
                    new Symptom { Name = "nose tender to touch"},
                    new Symptom { Name = "nosebleed"},
                    new Symptom { Name = "nostrils are very small"},
                    new Symptom { Name = "nostrils tilt down"},
                    new Symptom { Name = "open sore(s) on nose"},
                    new Symptom { Name = "open sore(s) on the nostril"},
                    new Symptom { Name = "postnasal drip"},
                    new Symptom { Name = "pus coming out of nose"},
                    new Symptom { Name = "red nose"},
                    new Symptom { Name = "rounded nose"},
                    new Symptom { Name = "runny nose"},
                    new Symptom { Name = "shingles on tip of nose"},
                    new Symptom { Name = "sinus pain or infection bridge of nose"},
                    new Symptom { Name = "sinus ulceration"},
                    new Symptom { Name = "sinusitis"},
                    new Symptom { Name = "small nose"},
                    new Symptom { Name = "smelling things that aren't there"},
                    new Symptom { Name = "smelly, runny nose"},
                    new Symptom { Name = "sneezing"},
                    new Symptom { Name = "snotty, runny nose"},
                    new Symptom { Name = "snout reflex"},
                    new Symptom { Name = "stuffy nose"},
                    new Symptom { Name = "swollen nose"},
                    new Symptom { Name = "thin nose"},
                    new Symptom { Name = "tingling or pricking of nose"},
                    new Symptom { Name = "trouble smelling"},
                    new Symptom { Name = "using decongestant nose drops"},
                    new Symptom { Name = "wide nose"}
            }
            };
            Symptom EarsCategory = new Symptom()
            {
                Name = "Ears Symptoms",
                Children = {
                    new Symptom { Name = "big ears"},
                    new Symptom { Name = "blocked ear"},
                    new Symptom { Name = "bony area behind ear is infected with pus"},
                    new Symptom { Name = "bony area behind ear is swollen"},
                    new Symptom { Name = "bony area behind ear is tender"},
                    new Symptom { Name = "bony growths in the ear"},
                    new Symptom { Name = "bruising on skull behind the ear"},
                    new Symptom { Name = "can't hear on one side"},
                    new Symptom { Name = "conductive hearing loss"},
                    new Symptom { Name = "constant ear ringing"},
                    new Symptom { Name = "diagonal crease in ear lobe"},
                    new Symptom { Name = "dry skin in ear"},
                    new Symptom { Name = "ear bleeding"},
                    new Symptom { Name = "ear cartilage is blue or black"},
                    new Symptom { Name = "ear doesn't look right"},
                    new Symptom { Name = "ear infection"},
                    new Symptom { Name = "ear infection middle ear"},
                    new Symptom { Name = "ear is red"},
                    new Symptom { Name = "ear lesion, mucoid"},
                    new Symptom { Name = "ear tender to touch"},
                    new Symptom { Name = "ear wax blocking ear"},
                    new Symptom { Name = "earache"},
                    new Symptom { Name = "earlobe crease"},
                    new Symptom { Name = "ears feel full"},
                    new Symptom { Name = "ears set low"},
                    new Symptom { Name = "fluid leaking from my ear"},
                    new Symptom { Name = "hard lumps around joints"},
                    new Symptom { Name = "headache behind ears"},
                    new Symptom { Name = "hear crackling noises in my ears"},
                    new Symptom { Name = "hearing is getting worse"},
                    new Symptom { Name = "hearing things that aren't there"},
                    new Symptom { Name = "hole in eardrum"},
                    new Symptom { Name = "inner ear infection"},
                    new Symptom { Name = "itchy ear"},
                    new Symptom { Name = "large blister on eardrum"},
                    new Symptom { Name = "large earlobes"},
                    new Symptom { Name = "lump in front of ear"},
                    new Symptom { Name = "lump on ear"},
                    new Symptom { Name = "mastoid bruit"},
                    new Symptom { Name = "mastoiditis"},
                    new Symptom { Name = "middle ear infection"},
                    new Symptom { Name = "otitis externa"},
                    new Symptom { Name = "otitis interna"},
                    new Symptom { Name = "outside of ear hurts"},
                    new Symptom { Name = "pain in bony area behind ear"},
                    new Symptom { Name = "pus coming from my ear"},
                    new Symptom { Name = "rash limited to ear"},
                    new Symptom { Name = "red and irritated swollen ear"},
                    new Symptom { Name = "redness of skin on skull behind ear"},
                    new Symptom { Name = "scaly or greasy skin on or behind ear"},
                    new Symptom { Name = "single skin growth on ear lobe"},
                    new Symptom { Name = "small ear"},
                    new Symptom { Name = "small ear canal"},
                    new Symptom { Name = "something is stuck in my ear"},
                    new Symptom { Name = "swelling in front of ears"},
                    new Symptom { Name = "swollen ear cartilage"},
                    new Symptom { Name = "tough or thick skin around joints"},
                    new Symptom { Name = "trouble hearing"},
                    new Symptom { Name = "tympanic membrane bulging"},
                    new Symptom { Name = "tympanic membrane hypomobile"},
                    new Symptom { Name = "tympanic membrane inflammation"},
                    new Symptom { Name = "tympanic membrane opaque"},
                    new Symptom { Name = "tympanic membrane retraction"},
                    new Symptom { Name = "tympanic membrane scarring"},
                    new Symptom { Name = "very sensitive to noise"},
                    new Symptom { Name = "very sensitive to sounds"},
                    new Symptom { Name = "vestibular impairment"},
                    new Symptom { Name = "whole ear swollen ear"}
            }
            };
            Symptom FaceCategory = new Symptom()
            {
                Name = "Face Symptoms",
                Children = {
                    new Symptom { Name = "can't feel temperature on face"},
                    new Symptom { Name = "can't move my face"},
                    new Symptom { Name = "can't move my face well"},
                    new Symptom { Name = "can't move one side of my face"},
                    new Symptom { Name = "cheek bone pain"},
                    new Symptom { Name = "cheek pain"},
                    new Symptom { Name = "enlarged vein on face"},
                    new Symptom { Name = "expressionless face"},
                    new Symptom { Name = "face extremely thin and bony"},
                    new Symptom { Name = "face feels full"},
                    new Symptom { Name = "face feels numb"},
                    new Symptom { Name = "face feels weak"},
                    new Symptom { Name = "face hair turning white"},
                    new Symptom { Name = "face hurts"},
                    new Symptom { Name = "face is blotchy"},
                    new Symptom { Name = "face is swollen"},
                    new Symptom { Name = "face is turning blue"},
                    new Symptom { Name = "face is yellow"},
                    new Symptom { Name = "face misshapen"},
                    new Symptom { Name = "face round"},
                    new Symptom { Name = "face spasms when stimulated"},
                    new Symptom { Name = "face sweats a lot"},
                    new Symptom { Name = "face tender to touch"},
                    new Symptom { Name = "face turns red when eating, drinking or exercising"},
                    new Symptom { Name = "face turns reddish color"},
                    new Symptom { Name = "face twitches"},
                    new Symptom { Name = "face twitching"},
                    new Symptom { Name = "facies coarse"},
                    new Symptom { Name = "facies grotesque"},
                    new Symptom { Name = "facies mongoloid"},
                    new Symptom { Name = "facies triangular"},
                    new Symptom { Name = "feels like air is under my face"},
                    new Symptom { Name = "fragile or thin skin on face"},
                    new Symptom { Name = "hair on face"},
                    new Symptom { Name = "half of face is flushed"},
                    new Symptom { Name = "horner syndrome"},
                    new Symptom { Name = "huge cheek bones"},
                    new Symptom { Name = "infected lump or sore on face"},
                    new Symptom { Name = "itchy face"},
                    new Symptom { Name = "limp muscle in face"},
                    new Symptom { Name = "long face"},
                    new Symptom { Name = "losing fat in face"},
                    new Symptom { Name = "loss of facial hair"},
                    new Symptom { Name = "lump on face"},
                    new Symptom { Name = "nasal sinus pain"},
                    new Symptom { Name = "no facial sweating"},
                    new Symptom { Name = "numbness of face"},
                    new Symptom { Name = "one side of face feels weak"},
                    new Symptom { Name = "one side of face not the same as the other"},
                    new Symptom { Name = "one side of my face hurts"},
                    new Symptom { Name = "open sore(s) on face"},
                    new Symptom { Name = "pale face"},
                    new Symptom { Name = "pea-sized lump under skin on face"},
                    new Symptom { Name = "pinched expression"},
                    new Symptom { Name = "rash limited to face"},
                    new Symptom { Name = "red face"},
                    new Symptom { Name = "red flaky rash limited to smile or laugh lines"},
                    new Symptom { Name = "red, swollen, runny nose"},
                    new Symptom { Name = "rough hair on face"},
                    new Symptom { Name = "sagging skin on face"},
                    new Symptom { Name = "skin on face feels hard"},
                    new Symptom { Name = "sore facial hair"},
                    new Symptom { Name = "spider vein(s) on face"},
                    new Symptom { Name = "stiff muscle in face"},
                    new Symptom { Name = "thinning facial hair"},
                    new Symptom { Name = "tingling or pricking face skin"},
                    new Symptom { Name = "tingling or pricking on one side of face"},
                    new Symptom { Name = "tingling or pricking skin of face"},
                    new Symptom { Name = "trigeminal neuralgia"},
                    new Symptom { Name = "trigeminal paralysis"},
                    new Symptom { Name = "unibrow"},
                    new Symptom { Name = "veins on face dilated"},
                    new Symptom { Name = "weak muscles in face"},
                    new Symptom { Name = "winking caused by jaw movement"}
            }
            };
            Symptom MouthCategory = new Symptom()
            {
                Name = "Mouth Symptoms",
                Children = {
                    new Symptom { Name = "area of mucus on tongue"},
                    new Symptom { Name = "area under tongue is swollen"},
                    new Symptom { Name = "back of mouth is red"},
                    new Symptom { Name = "bad breath"},
                    new Symptom { Name = "black stuff coating tongue"},
                    new Symptom { Name = "bleeding gums"},
                    new Symptom { Name = "blisters on tongue"},
                    new Symptom { Name = "breath has a fruity smell"},
                    new Symptom { Name = "breath has a sweet and tarry smell"},
                    new Symptom { Name = "breath smells like almonds"},
                    new Symptom { Name = "breath smells like garlic"},
                    new Symptom { Name = "breath smells like urine"},
                    new Symptom { Name = "breath smells metallic"},
                    new Symptom { Name = "broken speech pattern"},
                    new Symptom { Name = "brown flat discolored spot(s) limited to lips"},
                    new Symptom { Name = "buccal patch(es) mucus"},
                    new Symptom { Name = "bulimia"},
                    new Symptom { Name = "can't pucker lips"},
                    new Symptom { Name = "can't speak"},
                    new Symptom { Name = "canker sore"},
                    new Symptom { Name = "chin recession"},
                    new Symptom { Name = "cleft lip"},
                    new Symptom { Name = "cleft palate"},
                    new Symptom { Name = "cold sore"},
                    new Symptom { Name = "corner of mouth hurts"},
                    new Symptom { Name = "corner of mouth is sagging"},
                    new Symptom { Name = "cough"},
                    new Symptom { Name = "crack at the corner of mouth"},
                    new Symptom { Name = "crack on tongue"},
                    new Symptom { Name = "cracked lips"},
                    new Symptom { Name = "crave salt"},
                    new Symptom { Name = "damaged teeth enamel"},
                    new Symptom { Name = "dehydration"},
                    new Symptom { Name = "dental alveolar suppuration"},
                    new Symptom { Name = "dental arch narrowness"},
                    new Symptom { Name = "dental caries"},
                    new Symptom { Name = "dentition delay"},
                    new Symptom { Name = "denture pain"},
                    new Symptom { Name = "diminished gag reflex"},
                    new Symptom { Name = "drooling"},
                    new Symptom { Name = "dry lips"},
                    new Symptom { Name = "dry mouth"},
                    new Symptom { Name = "dry tongue"},
                    new Symptom { Name = "edentulous"},
                    new Symptom { Name = "food doesn't taste good"},
                    new Symptom { Name = "furry green coating on tongue"},
                    new Symptom { Name = "geographic tongue"},
                    new Symptom { Name = "gingival erythema"},
                    new Symptom { Name = "gingival fistula"},
                    new Symptom { Name = "gingival lead line, purple"},
                    new Symptom { Name = "gingival leukoplakia"},
                    new Symptom { Name = "gingival tenderness"},
                    new Symptom { Name = "gingival ulceration"},
                    new Symptom { Name = "gingival vesicle"},
                    new Symptom { Name = "gingivitis"},
                    new Symptom { Name = "gums hurt"},
                    new Symptom { Name = "hives inside of mouth"},
                    new Symptom { Name = "hives on lips"},
                    new Symptom { Name = "hot food or liquids hurt tooth"},
                    new Symptom { Name = "infected lump or sore on lip"},
                    new Symptom { Name = "inflamed tongue"},
                    new Symptom { Name = "inside of mouth is black"},
                    new Symptom { Name = "inside of mouth is brown"},
                    new Symptom { Name = "inside of mouth is red"},
                    new Symptom { Name = "inside of mouth is white"},
                    new Symptom { Name = "inside of mouth is yellow"},
                    new Symptom { Name = "inside of mouth swollen"},
                    new Symptom { Name = "interdental papillary ulceration"},
                    new Symptom { Name = "involuntary jerky or fitful movement of tongue"},
                    new Symptom { Name = "koplik spot"},
                    new Symptom { Name = "large blister(s) in mouth"},
                    new Symptom { Name = "large tongue"},
                    new Symptom { Name = "lip chewing"},
                    new Symptom { Name = "lip hurts"},
                    new Symptom { Name = "lip is tingling or prickling"},
                    new Symptom { Name = "lip pulled back"},
                    new Symptom { Name = "lip tender to touch"},
                    new Symptom { Name = "lip trembling"},
                    new Symptom { Name = "lipoatrophy"},
                    new Symptom { Name = "lips are thicker"},
                    new Symptom { Name = "lips turning blue"},
                    new Symptom { Name = "long groove between nose and lip"},
                    new Symptom { Name = "lower lip droops"},
                    new Symptom { Name = "lump on tongue"},
                    new Symptom { Name = "mallampati grade i"},
                    new Symptom { Name = "mallampati grade iii-iv"},
                    new Symptom { Name = "malocclusion"},
                    new Symptom { Name = "metal taste in mouth"},
                    new Symptom { Name = "microdontia"},
                    new Symptom { Name = "micrognathia"},
                    new Symptom { Name = "molar loosening, deciduous"},
                    new Symptom { Name = "more thirsty than usual"},
                    new Symptom { Name = "mouth bleeding"},
                    new Symptom { Name = "mouth breathing"},
                    new Symptom { Name = "mouth burn"},
                    new Symptom { Name = "mouth hurts"},
                    new Symptom { Name = "mouth is sore"},
                    new Symptom { Name = "mouth is swollen"},
                    new Symptom { Name = "mouth itches"},
                    new Symptom { Name = "mouth looks crooked"},
                    new Symptom { Name = "mouth mucous membrane bleeding"},
                    new Symptom { Name = "mouth mucous membrane ulceration"},
                    new Symptom { Name = "mouth opened"},
                    new Symptom { Name = "mouth tender to touch"},
                    new Symptom { Name = "mouth wideness"},
                    new Symptom { Name = "mucous membrane petechia"},
                    new Symptom { Name = "mucous membrane scarring"},
                    new Symptom { Name = "mute"},
                    new Symptom { Name = "open sore(s) in mouth"},
                    new Symptom { Name = "open sore(s) inside of cheek"},
                    new Symptom { Name = "open sore(s) on back of mouth"},
                    new Symptom { Name = "open sore(s) on inside of cheek"},
                    new Symptom { Name = "open sore(s) on lip"},
                    new Symptom { Name = "open sore(s) on roof of mouth"},
                    new Symptom { Name = "open sore(s) on tongue"},
                    new Symptom { Name = "orange tonsils"},
                    new Symptom { Name = "pain in tooth socket"},
                    new Symptom { Name = "palatal muscle weakness"},
                    new Symptom { Name = "palatal paralysis"},
                    new Symptom { Name = "palatal tremor"},
                    new Symptom { Name = "pale around mouth"},
                    new Symptom { Name = "pea-sized lump on tongue"},
                    new Symptom { Name = "producing too much saliva"},
                    new Symptom { Name = "pseudomembrane"},
                    new Symptom { Name = "puckered lip"},
                    new Symptom { Name = "raised skin patch(es) on tongue"},
                    new Symptom { Name = "red bump(s) inside of cheek"},
                    new Symptom { Name = "red irritated throat"},
                    new Symptom { Name = "red lips"},
                    new Symptom { Name = "red or purple flat spots on inside of cheeks"},
                    new Symptom { Name = "red tonsil"},
                    new Symptom { Name = "roof of mouth has high arch"},
                    new Symptom { Name = "roof of mouth is inflamed"},
                    new Symptom { Name = "roof of mouth is misshapen"},
                    new Symptom { Name = "roof of mouth is numb"},
                    new Symptom { Name = "roof of mouth is red"},
                    new Symptom { Name = "roof of mouth narrow"},
                    new Symptom { Name = "roof of mouth red"},
                    new Symptom { Name = "roof of mouth swollen"},
                    new Symptom { Name = "round ball in back of throat is out of place"},
                    new Symptom { Name = "round ball in back of throat is swollen"},
                    new Symptom { Name = "self induced vomiting"},
                    new Symptom { Name = "severely bad breath"},
                    new Symptom { Name = "short groove between nose and lip"},
                    new Symptom { Name = "shrinking tongue"},
                    new Symptom { Name = "skin sore(s) inside of mouth"},
                    new Symptom { Name = "skin sore(s) on tonsil"},
                    new Symptom { Name = "small blister on roof of mouth"},
                    new Symptom { Name = "small bump on inside of cheek"},
                    new Symptom { Name = "small bump(s) on back of mouth"},
                    new Symptom { Name = "small flat red or purple spots on back of mouth"},
                    new Symptom { Name = "small flat red or purple spots on round ball in back of throat"},
                    new Symptom { Name = "small flat red or purple spots on tonsil"},
                    new Symptom { Name = "small red spots on roof of mouth"},
                    new Symptom { Name = "small white bump(s) on inside of cheek"},
                    new Symptom { Name = "smooth groove between nose and lip"},
                    new Symptom { Name = "snoring"},
                    new Symptom { Name = "soft palate atrophy"},
                    new Symptom { Name = "soft palate numbness"},
                    new Symptom { Name = "soft palate paralysis"},
                    new Symptom { Name = "soft palate swelling"},
                    new Symptom { Name = "sores in or on side of mouth"},
                    new Symptom { Name = "speech is slow"},
                    new Symptom { Name = "spider vein(s) on roof of mouth"},
                    new Symptom { Name = "stuff coats top of tongue"},
                    new Symptom { Name = "stuttering"},
                    new Symptom { Name = "swelling around the mouth"},
                    new Symptom { Name = "swollen gums"},
                    new Symptom { Name = "swollen lips"},
                    new Symptom { Name = "swollen throat"},
                    new Symptom { Name = "swollen tongue"},
                    new Symptom { Name = "swollen tonsil on one side"},
                    new Symptom { Name = "swollen tonsils"},
                    new Symptom { Name = "tasting things that aren't there"},
                    new Symptom { Name = "teeth do not fit well"},
                    new Symptom { Name = "teeth grinding"},
                    new Symptom { Name = "thin lips"},
                    new Symptom { Name = "throat is dry"},
                    new Symptom { Name = "thrush"},
                    new Symptom { Name = "tingling or numbness around mouth"},
                    new Symptom { Name = "tingling or pricking inside mouth"},
                    new Symptom { Name = "tingling or pricking tongue"},
                    new Symptom { Name = "tiny mouth"},
                    new Symptom { Name = "tongue biting"},
                    new Symptom { Name = "tongue blanching"},
                    new Symptom { Name = "tongue feels like it is burning"},
                    new Symptom { Name = "tongue glazing"},
                    new Symptom { Name = "tongue has no grooves"},
                    new Symptom { Name = "tongue hurts"},
                    new Symptom { Name = "tongue infection"},
                    new Symptom { Name = "tongue is more red than usual"},
                    new Symptom { Name = "tongue is out of place"},
                    new Symptom { Name = "tongue is weak"},
                    new Symptom { Name = "tongue not normal size and shape"},
                    new Symptom { Name = "tongue pushed out too far"},
                    new Symptom { Name = "tongue quivers"},
                    new Symptom { Name = "tongue trembling"},
                    new Symptom { Name = "tonsil inflammation"},
                    new Symptom { Name = "tonsil is out of place"},
                    new Symptom { Name = "tonsillar leukoplakia"},
                    new Symptom { Name = "tooth deformity"},
                    new Symptom { Name = "tooth discoloration"},
                    new Symptom { Name = "tooth enamel hypoplasia"},
                    new Symptom { Name = "tooth enamel pitting"},
                    new Symptom { Name = "tooth erosion"},
                    new Symptom { Name = "tooth extraction"},
                    new Symptom { Name = "tooth hurts with cold liquids or food"},
                    new Symptom { Name = "tooth impaction"},
                    new Symptom { Name = "tooth loose"},
                    new Symptom { Name = "tooth loss"},
                    new Symptom { Name = "tooth pegged"},
                    new Symptom { Name = "tooth root defect"},
                    new Symptom { Name = "tooth spacing irregularity"},
                    new Symptom { Name = "toothache"},
                    new Symptom { Name = "top lip hangs over"},
                    new Symptom { Name = "trouble chewing"},
                    new Symptom { Name = "trouble communicating"},
                    new Symptom { Name = "trouble producing saliva"},
                    new Symptom { Name = "trouble speaking"},
                    new Symptom { Name = "trouble tasting"},
                    new Symptom { Name = "tumor in mouth"},
                    new Symptom { Name = "upper lip is swollen"},
                    new Symptom { Name = "voice doesn't sound right"},
                    new Symptom { Name = "vomiting blood"},
                    new Symptom { Name = "white coating on tongue"},
                    new Symptom { Name = "white rash on inside of mouth"},
                    new Symptom { Name = "white rash on roof of mouth"},
                    new Symptom { Name = "white skin sore(s) on back of mouth"},
                    new Symptom { Name = "white skin sore(s) on round ball in back of throat"},
                    new Symptom { Name = "whitish coating on tonsil"},
                    new Symptom { Name = "yawning"},
                    new Symptom { Name = "yellow skin sore(s) on back of mouth"},
                    new Symptom { Name = "yellow skin sore(s) on uvula"}
            }
            };
            Symptom JawCategory = new Symptom()
            {
                Name = "Jaw Symptoms",
                Children = {
                    new Symptom { Name = "area under chin is hard"},
                    new Symptom { Name = "beard lice"},
                    new Symptom { Name = "cheek and jaw swollen"},
                    new Symptom { Name = "cheek fullness"},
                    new Symptom { Name = "cheek swelling"},
                    new Symptom { Name = "chin thrusting"},
                    new Symptom { Name = "clicking or popping sound from jaw"},
                    new Symptom { Name = "enlarged jaw vein"},
                    new Symptom { Name = "indentations going down cheek"},
                    new Symptom { Name = "inflammation inside mouth"},
                    new Symptom { Name = "jaw angle tenderness"},
                    new Symptom { Name = "jaw deformity"},
                    new Symptom { Name = "jaw hurts"},
                    new Symptom { Name = "jaw induration"},
                    new Symptom { Name = "jaw mass firmness"},
                    new Symptom { Name = "jaw muscle spasm"},
                    new Symptom { Name = "jaw muscle weakness"},
                    new Symptom { Name = "jaw tremor"},
                    new Symptom { Name = "lower jaw hurts"},
                    new Symptom { Name = "lump on cheek"},
                    new Symptom { Name = "lymph node under jaw enlarged"},
                    new Symptom { Name = "mandibular degeneration"},
                    new Symptom { Name = "mandibular osteoma"},
                    new Symptom { Name = "mandibular swelling"},
                    new Symptom { Name = "mandibular tenderness"},
                    new Symptom { Name = "mandibular tumor"},
                    new Symptom { Name = "muscle spasm in jaw"},
                    new Symptom { Name = "pain in jaw when chewing"},
                    new Symptom { Name = "parotid gland ulceration"},
                    new Symptom { Name = "parotitis"},
                    new Symptom { Name = "prominent jaw"},
                    new Symptom { Name = "pus-filled bump(s) in beard hair follicle(s)"},
                    new Symptom { Name = "red bumps around hair follicles in beard"},
                    new Symptom { Name = "retrognathia"},
                    new Symptom { Name = "small jaw"},
                    new Symptom { Name = "submaxillary gland swelling"},
                    new Symptom { Name = "swelling beneath lower jaw"},
                    new Symptom { Name = "swelling inside mouth"},
                    new Symptom { Name = "tumor on upper jaw"},
                    new Symptom { Name = "upper jaw hurts"},
                    new Symptom { Name = "upper jaw is large"},
                    new Symptom { Name = "upper jaw is small"}
            }
            };
            list.Add(ScalpCategory);
            list.Add(ForeheadCategory);
            list.Add(EyesCategory);
            list.Add(NoseCategory);
            list.Add(EarsCategory);
            list.Add(FaceCategory);
            list.Add(MouthCategory);
            list.Add(JawCategory);
            return list;
        }
        private ObservableCollection<Symptom> GetSymptomDataAbdomen()
        {
            var list = new ObservableCollection<Symptom>();
            Symptom UpperAbdomenCategory = new Symptom()
            {
                Name = "Upper Abdomen Symptoms",
                Children = {
                    new Symptom { Name = "abdominal mass, movable, upper"},
                    new Symptom { Name = "abdominal mass, right upper quadrant"},
                    new Symptom { Name = "abdominal mass, upper"},
                    new Symptom { Name = "abdominal tenderness, left upper quadrant"},
                    new Symptom { Name = "burping"},
                    new Symptom { Name = "can't digest fatty foods"},
                    new Symptom { Name = "courvoisier sign"},
                    new Symptom { Name = "diarrhea after meals"},
                    new Symptom { Name = "fatty liver"},
                    new Symptom { Name = "gall bladder distention"},
                    new Symptom { Name = "gallbladder inflammation"},
                    new Symptom { Name = "gallstones"},
                    new Symptom { Name = "hepatic friction rub"},
                    new Symptom { Name = "hepatosplenomegaly"},
                    new Symptom { Name = "inflammation of stomach and intestines"},
                    new Symptom { Name = "liver border irregularity"},
                    new Symptom { Name = "liver bruit"},
                    new Symptom { Name = "liver disease"},
                    new Symptom { Name = "liver displacement"},
                    new Symptom { Name = "liver enlargement"},
                    new Symptom { Name = "liver hard"},
                    new Symptom { Name = "liver mass"},
                    new Symptom { Name = "liver pulsation"},
                    new Symptom { Name = "liver tenderness"},
                    new Symptom { Name = "murphy sign positive"},
                    new Symptom { Name = "nausea"},
                    new Symptom { Name = "open sore in stomach or esophagus"},
                    new Symptom { Name = "pain in diaphragm"},
                    new Symptom { Name = "pancreas inflammation"},
                    new Symptom { Name = "past gallbladder removal"},
                    new Symptom { Name = "reflux"},
                    new Symptom { Name = "scarring of the liver"},
                    new Symptom { Name = "spleen enlargement"},
                    new Symptom { Name = "spleen friction rub"},
                    new Symptom { Name = "spleen palpable"},
                    new Symptom { Name = "spleen tenderness"},
                    new Symptom { Name = "stomach pain upper left side"},
                    new Symptom { Name = "stomach pain upper right side"},
                    new Symptom { Name = "ulcer in muscle connecting stomach to duodenum"},
                    new Symptom { Name = "upper abdominal wound"},
                    new Symptom { Name = "upper belly bloating"},
                    new Symptom { Name = "upper stomach pain"}
            }
            };
            Symptom EpigastricCategory = new Symptom()
            {
                Name = "Epigastric Symptoms",
                Children = {
                    new Symptom { Name = "burping"},
                    new Symptom { Name = "epigastric abdominal tenderness"},
                    new Symptom { Name = "heartburn"},
                    new Symptom { Name = "hernia in belly button"},
                    new Symptom { Name = "indigestion"},
                    new Symptom { Name = "nausea"},
                    new Symptom { Name = "pain around belly button"},
                    new Symptom { Name = "pain in middle of belly"},
                    new Symptom { Name = "pain near belly button spreading to lower right side of stomach"},
                    new Symptom { Name = "reflux"},
                    new Symptom { Name = "stomach inflammation"},
                    new Symptom { Name = "stomach pushes through diaphragm"},
                    new Symptom { Name = "urine leaking from belly button"},
                    new Symptom { Name = "vomiting blood"}
            }
            };
            Symptom LowerAbdomenCategory = new Symptom()
            {
                Name = "Lower Abdomen Symptoms",
                Children = {
                    new Symptom { Name = "abdominal mass, left lower quadrant"},
                    new Symptom { Name = "abdominal mass, lower"},
                    new Symptom { Name = "abdominal mass, right lower quadrant"},
                    new Symptom { Name = "abdominal tenderness, left lower quadrant"},
                    new Symptom { Name = "abdominal tenderness, lower"},
                    new Symptom { Name = "bladder distention"},
                    new Symptom { Name = "bladder feels full"},
                    new Symptom { Name = "c-section"},
                    new Symptom { Name = "change in bowel habits"},
                    new Symptom { Name = "diarrhea"},
                    new Symptom { Name = "feels like need to pee all the time"},
                    new Symptom { Name = "frequent bowel movements"},
                    new Symptom { Name = "gassy"},
                    new Symptom { Name = "hurts when ovulating"},
                    new Symptom { Name = "indirect tenderness right lower quadrant"},
                    new Symptom { Name = "inflammation of colon"},
                    new Symptom { Name = "inflammation of stomach and intestines"},
                    new Symptom { Name = "lower belly bloating"},
                    new Symptom { Name = "lower stomach pain"},
                    new Symptom { Name = "ovarian mass"},
                    new Symptom { Name = "ovarian mass, irregular"},
                    new Symptom { Name = "ovarian swelling"},
                    new Symptom { Name = "ovary palpable"},
                    new Symptom { Name = "past appendix removal"},
                    new Symptom { Name = "stomach pain lower left side"},
                    new Symptom { Name = "stomach pain lower right side"}
                }
            };
            list.Add(UpperAbdomenCategory);
            list.Add(EpigastricCategory);
            list.Add(LowerAbdomenCategory);
            return list;
        }

        private void Tree_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
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

