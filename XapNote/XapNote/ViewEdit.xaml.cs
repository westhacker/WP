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
    public partial class ViewEdit : PhoneApplicationPage
    {
        private IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        public ViewEdit()
        {
            InitializeComponent();
        }

        private void backApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            if (edit.Visibility == System.Windows.Visibility.Visible)
            {
                edit.Visibility = System.Windows.Visibility.Collapsed;
                view.Visibility = System.Windows.Visibility.Visible;
                settings["vore"] = "v";
            }
            else
            {
                NavigationService.Navigate(new Uri("/XapNote;component/MainPage.xaml", UriKind.Relative));
                settings["state"] = "";
            }
        }

        private void saveApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            if (edit.Visibility == System.Windows.Visibility.Visible)
            {
                var appStorage = IsolatedStorageFile.GetUserStoreForApplication();
                string fileName = NavigationContext.QueryString["id"];
                using (var fileStream = appStorage.OpenFile(fileName, System.IO.FileMode.Open))
                {
                    using (StreamWriter sw = new StreamWriter(fileStream))
                    {
                        sw.Write(edit.Text);
                    }
                }
                edit.Visibility = System.Windows.Visibility.Collapsed;
                view.Visibility = System.Windows.Visibility.Visible;
                view.Text = edit.Text;
                settings["vore"] = "v";
            }else
            {
                return;
                }
        }

        private void editApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {
            view.Visibility = System.Windows.Visibility.Collapsed;
            edit.Visibility = System.Windows.Visibility.Visible;
            edit.Text = view.Text;
            edit.Focus();
            settings["vore"] = "e";
        }

        private void deleteApplicationBarIconButton_Click_3(object sender, EventArgs e)
        {
            delete.Visibility = System.Windows.Visibility.Visible;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            string state = "";
            if (settings.TryGetValue<string>("state", out state))
            {
                if (state == "edit")
                {
                    string vore = "";
                    if (settings.Contains("vore"))
                    {
                        if (settings.TryGetValue<string>("vore", out vore))
                        {
                            if (vore == "v") {
                                settings["vore"] = "v";
                                string id = "";
                                if (settings.Contains("id"))
                                {
                                    if (settings.TryGetValue<string>("id", out id))
                                    {
                                        NavigationContext.QueryString["id"] = id;
                                    }
                                }
                                
                            }
                            if (vore == "e") {
                                view.Visibility = System.Windows.Visibility.Collapsed;
                                edit.Visibility = System.Windows.Visibility.Visible;
                               
                                edit.Focus();
                                string value = "";
                                if (settings.Contains("value"))
                                {
                                    if (settings.TryGetValue<string>("value", out value))
                                    {
                                        edit.Text = value;
                                    }
                                }
                                string id = "";
                                if (settings.Contains("id"))
                                {
                                    if (settings.TryGetValue<string>("id", out id))
                                    {
                                        NavigationContext.QueryString["id"] = id;
                                    }
                                }
                               
                                settings["vore"] = "e";
                            }
                           
                        }
                    }
                   
                }
            }
            settings["state"] = "edit";
            settings["value"] = edit.Text;
            settings["id"] = NavigationContext.QueryString["id"];

            string fileName = NavigationContext.QueryString["id"];
            var appStorage = IsolatedStorageFile.GetUserStoreForApplication();
            using (var file = appStorage.OpenFile(fileName, System.IO.FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    view.Text = sr.ReadToEnd();
                }
            }
        }

        private void sureButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = NavigationContext.QueryString["id"];
            var appStorage = IsolatedStorageFile.GetUserStoreForApplication();
            appStorage.DeleteFile(fileName);
            NavigationService.Navigate(new Uri("/XapNote;component/MainPage.xaml", UriKind.Relative));
            delete.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            delete.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void edit_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            settings["value"] = edit.Text;
        }
    }
}