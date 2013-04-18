using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using System.IO;

namespace XapNote
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/XapNote;component/Add.xaml", UriKind.Relative));
            /*
             * I created this for a single file test
             * 
            string fileName = "2011_10_07_19_44_01_SJTU_Shanghai-China.txt";
            string fileContent = "This is just a test.";

            var appStorage = IsolatedStorageFile.GetUserStoreForApplication();

            if (!appStorage.FileExists(fileName))
            {
                using (var file = appStorage.CreateFile(fileName))
                {
                    using (var writer = new StreamWriter(file))
                    {
                        writer.WriteLine(fileContent);
                    }
                }
            }
             */
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            help.Visibility = System.Windows.Visibility.Visible;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            var appStorage = IsolatedStorageFile.GetUserStoreForApplication();
           
           

            string state = "";
            
            if (settings.Contains("state"))
            {
                if (settings.TryGetValue<string>("state", out state))
                {
                    if (state == "add")
                    {
                        NavigationService.Navigate(new Uri("/XapNote;component/Add.xaml", UriKind.Relative));
                    }
                    else if (state == "edit")
                    {
                        string id = "";
                        if (settings.TryGetValue<string>("id", out id))
                        {
                            if (appStorage.FileExists(id))
                            {
                                NavigationService.Navigate(new Uri("/XapNote;component/ViewEdit.xaml", UriKind.Relative));
                            }
                           
                        }
                    }
                   
                }
            }
            
            bindList();
        }

        private void bindList()
        {
            var appStorage = IsolatedStorageFile.GetUserStoreForApplication();
            string[] fileList = appStorage.GetFileNames();

            List<note> notes = new List<note>();

            foreach (string file in fileList)
            {
                if (file != "__ApplicationSettings")
                {
                    string fileName = file;

                    string year = file.Substring(0, 4);
                    string month = file.Substring(5, 2);
                    string day = file.Substring(8, 2);
                    string hour = file.Substring(11, 2);
                    string minute = file.Substring(14, 2);
                    string second = file.Substring(17, 2);

                    DateTime dateCreated = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day), int.Parse(hour), int.Parse(minute), int.Parse(second));

                    string location = file.Substring(20);
                    location = location.Replace("_", ",");
                    location = location.Replace("-", " ");
                    location = location.Substring(0, location.Length - 4);

                    notes.Add(new note { Location = location, DateCreated = dateCreated.ToLongDateString(), FileName = fileName });
                }

            }

            noteListBox.ItemsSource = notes;
        }

        private void listclick(object sender, RoutedEventArgs e)
        {
            HyperlinkButton clickedLink = (HyperlinkButton)sender;
            string uri = String.Format("/XapNote;component/ViewEdit.xaml?id={0}",clickedLink.Tag);
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }

        private void helpButton_Click(object sender, RoutedEventArgs e)
        {
            help.Visibility = System.Windows.Visibility.Collapsed;
        }

    
    }
}