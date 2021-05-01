using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace healthtest2.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class login : Page
    {
        public login()
        {
            this.InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Person person = new Person();
            person.Name = name.Text;
            person.age = age.Text;
            person.gender = gender.Text;
            string json = JsonConvert.SerializeObject(person, Formatting.Indented);
            try
            {
                System.IO.File.WriteAllText(@"D:\json\person.json", json);
            }
            catch (UnauthorizedAccessException)
            {
                FileAttributes attributes = File.GetAttributes(@"D:\json\person.json");
                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    attributes &= ~FileAttributes.ReadOnly;
                    File.SetAttributes(@"D:\json\person.json", attributes);
                    System.IO.File.WriteAllText(@"D:\json\person.json", json);
                }
                else
                {
                    throw;
                }
            }

        }

        public class Person
        {
            public string Name { get; set; }
            public string age { get; set; }
            public string gender { get; set; }
        }
    }
}
