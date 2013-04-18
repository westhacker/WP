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
using System.Device.Location;
using System.IO.IsolatedStorage;
using System.Text;
using System.IO;


namespace XapNote
{
    public partial class Add : PhoneApplicationPage
    {
        private IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        private string location = "";
        public Add()
        {
            InitializeComponent();

            GeoCoordinateWatcher myWatcher = new GeoCoordinateWatcher();

            var myPosition = myWatcher.Position;

            double latitude = 47.674;
            double longitude = 120;
           


            if (!myPosition.Location.IsUnknown)
            {
                latitude = myPosition.Location.Latitude;
                longitude = myPosition.Location.Longitude;
            }

            myts.TerraServiceSoapClient client = new myts.TerraServiceSoapClient();
            client.ConvertLonLatPtToNearestPlaceCompleted += new EventHandler<myts.ConvertLonLatPtToNearestPlaceCompletedEventArgs>(client_ConvertLonLatPtToNearestPlaceCompleted);
            client.ConvertLonLatPtToNearestPlaceAsync(new myts.LonLatPt { Lat = latitude, Lon = longitude });
           
        }

        void client_ConvertLonLatPtToNearestPlaceCompleted(object sender, myts.ConvertLonLatPtToNearestPlaceCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            location = e.Result;
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            //save into file
            if (location.Trim().Length == 0)
            {
                location = "Unknown";
            }

            var appStorage = IsolatedStorageFile.GetUserStoreForApplication();

            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.Year);
            sb.Append("_");
            sb.Append(String.Format("{0:00}", DateTime.Now.Month));
            sb.Append("_");
            sb.Append(String.Format("{0:00}", DateTime.Now.Day));
            sb.Append("_");
            sb.Append(String.Format("{0:00}", DateTime.Now.Hour));
            sb.Append("_");
            sb.Append(String.Format("{0:00}", DateTime.Now.Minute));
            sb.Append("_");
            sb.Append(String.Format("{0:00}", DateTime.Now.Second));
            sb.Append("_");

            location = location.Replace(" ", "-");
            location = location.Replace(",", "_");
            sb.Append(location);
            sb.Append(".txt");

            using (var fileStream = appStorage.OpenFile(sb.ToString(), System.IO.FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fileStream))
                {
                    sw.Write(edit.Text);
                }
            }

            navigateBack();
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
           
            navigateBack();
        }

        private void navigateBack()
        {
            settings["state"] = "";
            settings["value"] = "";
            NavigationService.Navigate(new Uri("/XapNote;component/MainPage.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
          

            string state = "";
            if (settings.TryGetValue<string>("state", out state))
            {
                if (state == "add")
                {
                    string value = "";
                    if (settings.Contains("value"))
                    {
                        if (settings.TryGetValue<string>("value", out value))
                        {
                            edit.Text = value;
                        }
                    }
                }
            }
            settings["state"] = "add";
            settings["value"] = edit.Text;

            edit.Focus();
        }

        private void edit_TextChanged(object sender, TextChangedEventArgs e)
        {
            settings["value"] = edit.Text;
        }

    }
}