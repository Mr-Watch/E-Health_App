using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;
using System;
using System.Globalization;
using AllThingsHealth.Core.Services;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AllThingsHealth.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class resultpage : Page
    {
        public resultpage()
        {
            this.InitializeComponent();
            medicine_list.Items.Clear();
            Hospital_list.Items.Clear();
            doctor_list.Items.Clear();
            List<Medicine> items1 = new List<Medicine>();
            items1.Add(new Medicine("Medicine_name1"));
            items1.Add(new Medicine("Medicine_name2"));
            items1.Add(new Medicine("Medicine_name3"));
            medicine_list.ItemsSource = items1;
            List<Doctor> items3 = new List<Doctor>();
            items3.Add(new Doctor("Doctor_name1", "Address", 38.048091, 23.719676,"Medic"));
            items3.Add(new Doctor("Doctor_name2", "Address", 38.04800, 23.719600, "Medic"));
            items3.Add(new Doctor("Doctor_name3", "Address", 8.048091, 3.719676, "Medic"));
            doctor_list.ItemsSource = items3;
            Fetch();
        }
        public async void Fetch() {
            HttpDataService url = new HttpDataService("http://127.0.0.1:5000");
            String uri = "/ath/api/v0.1/healthatlas/hospitals/"+ perioxi_id.Text ;
            String uri2 = "/ath/api/v0.1/healthatlas/hospitals/9AEB40EC-A2D2-45E5-B0F5-BABC72591495";
            List<Hospital> items = new List<Hospital>();
            List<Pharmacy> items2 = new List<Pharmacy>();
            try
            {
                JArray json = await url.GetAsync<JArray>(uri);
                JArray json2 = await url.GetAsync<JArray>(uri2);
                foreach (JObject item in json)
                {
                    string name = item.GetValue("Title").ToString();
                    string address = item.GetValue("Address").ToString();
                    string telephone = item.GetValue("Telephone").ToString();
                    string email = item.GetValue("Email").ToString();
                    if (name.Length > 30) {
                        name = name.Substring(0, 30) + "...";
                    }
                    if (address.Length > 30)
                    {
                        address = address.Substring(0, 30)+"...";
                    }
                    Double lon = 0.0;
                    Double lat = 0.0;
                    items.Add(new Hospital(name, address, telephone, email, lat, lon));
                }
                Hospital_list.ItemsSource = items;
                foreach (JObject item2 in json2)
                {
                    string name = item2.GetValue("Title").ToString();
                    string address = item2.GetValue("Address").ToString();
                    string telephone = item2.GetValue("Telephone").ToString();
                    string email = item2.GetValue("Email").ToString();
                    if (name.Length > 30)
                    {
                        name = name.Substring(0, 30) + "...";
                    }
                    if (address.Length > 30)
                    {
                        address = address.Substring(0, 30) + "...";
                    }
                    Double lon = 0.0;
                    Double lat = 0.0;
                    items2.Add(new Pharmacy(name, address, telephone, email, lat, lon));
                }
                pharmacy_list.ItemsSource = items2;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MainPage ma = (MainPage) e.Parameter;
            info.Text = "illness:" + ma.test;
             perioxi_id.Text = ma.test;

        }
    }
    public class Medicine
    {
        public string Name { get; private set; }
        public Medicine(string Name)
        {
            this.Name = Name;
        }
    }
    public class Hospital
    {
        string fmt = "00.000000";
        public string Name { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public GeoLocation Location { get; set; }
        public string Longitude => Location.Longitude.ToString(fmt);
        public string Latitude => Location.Latitude.ToString(fmt);
        public Hospital(string Name,string Address,string Telephone, string Email, double latitude, double longitude)
        {
            this.Email = Email;
            this.Telephone = Telephone;
            this.Name = Name;
            this.Address = Address;
            Location = new GeoLocation(longitude, latitude);
        }
    }
    public class Doctor
    {
        string fmt = "00.000000";
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Spec { get; private set; }
        public GeoLocation Location { get; set; }
        public string Longitude => Location.Longitude.ToString(fmt);
        public string Latitude => Location.Latitude.ToString(fmt);
        public Doctor(string Name, string Address, double latitude, double longitude,string Spec)
        {
            this.Name = Name;
            this.Address = Address;
            Location = new GeoLocation(longitude, latitude);
            this.Spec = Spec;
        }
    }
    public class Pharmacy
    {
        string fmt = "00.000000";
        public string Name { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public GeoLocation Location { get; set; }
        public string Longitude => Location.Longitude.ToString(fmt);
        public string Latitude => Location.Latitude.ToString(fmt);
        public Pharmacy(string Name, string Address, string Telephone, string Email, double latitude, double longitude)
        {
            this.Email = Email;
            this.Telephone = Telephone;
            this.Name = Name;
            this.Address = Address;
            Location = new GeoLocation(longitude, latitude);
        }
    }
    public class GeoLocation
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public GeoLocation(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
        public string toString()
        {
            return "Lo:"+Longitude+" La:"+Latitude;
        }
    }
}
