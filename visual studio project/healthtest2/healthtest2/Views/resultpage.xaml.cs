using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;
using System;
using System.Globalization;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace healthtest2.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class resultpage : Page
    {
        public resultpage()
        {
            this.InitializeComponent();
            List<Medicine> items1 = new List<Medicine>();
            items1.Add(new Medicine("Medicine_name1"));
            items1.Add(new Medicine("Medicine_name2"));
            items1.Add(new Medicine("Medicine_name3"));
            medicine_list.ItemsSource = items1;
            List<Pharmacy> items2 = new List<Pharmacy>();
            items2.Add(new Pharmacy("Pharmacy_name1","Address", 38.048091, 23.719676));
            items2.Add(new Pharmacy("Pharmacy_name2", "Address", 38.04800, 23.719600));
            items2.Add(new Pharmacy("Pharmacy_name3", "Address", 8.048091, 3.719676));
            pharmacy_list.ItemsSource = items2;
            List<Doctor> items3 = new List<Doctor>();
            items3.Add(new Doctor("Doctor_name1", "Address", 38.048091, 23.719676,"Medic"));
            items3.Add(new Doctor("Doctor_name2", "Address", 38.04800, 23.719600, "Medic"));
            items3.Add(new Doctor("Doctor_name3", "Address", 8.048091, 3.719676, "Medic"));
            doctor_list.ItemsSource = items3;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MainPage ma = (MainPage) e.Parameter;
            info.Text = "illness:" + ma.test;
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
    public class Pharmacy
    {
        string fmt = "00.000000";
        public string Name { get; private set; }
        public string Address { get; private set; }
        public GeoLocation Location { get; set; }
        public string Longitude => Location.Longitude.ToString(fmt);
        public string Latitude => Location.Latitude.ToString(fmt);
        public Pharmacy(string Name,string Address,double latitude, double longitude)
        {
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
