using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LumiaKothon.Resources;
using LumiaKothon.Helper;
using LumiaKothon.FacebookApi;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using LumiaKothon.FacebookTools;

namespace LumiaKothon
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += MainPage_Loaded;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }


        public string TextToPost { get; set; }
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            ProgressIndicatorHelper.ShowProgressIndicator("Loading Data ......");
        }

        private void facebook_connect(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/FBMain.xaml", UriKind.Relative));
        }

         //Sample code for building a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new button and set the text value to the localized string from AppResources.
            ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
            appBarButton.Text = AppResources.AppBarButtonText;
            ApplicationBar.Buttons.Add(appBarButton);

            // Create a new menu item with the localized string from AppResources.
            ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
            ApplicationBar.MenuItems.Add(appBarMenuItem);
        }

        private void ButtonPost_Click(object sender, RoutedEventArgs e)
        {
            TextToPost = txtpost.Text;
            if (TextToPost == "")
            {
                MessageBox.Show("Kindly put a word!");
                return;
            }
            if (FacebookClient.Instance.AccessToken != null)
            {
                FacebookClient.Instance.PostMessageOnWall(TextToPost, new UploadStringCompletedEventHandler(PostMessageOnWallCompleted));
                
            }
            else
            {
                MessageBox.Show("please Connect to facebook!");
            }
        }


        private void PostMessageOnWallCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            if (e.Cancelled)
                return;
            if (e.Error != null)
            {
                //MessageBox.Show("Error Occurred: " + e.Error.Message);
                MessageBox.Show("please Connect to facebook!");
                return;
            }

            System.Diagnostics.Debug.WriteLine(e.Result);

            string result = e.Result;
            byte[] data = Encoding.UTF8.GetBytes(result);
            MemoryStream memStream = new MemoryStream(data);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ResponseData));
            ResponseData responseData = (ResponseData)serializer.ReadObject(memStream);

            if (responseData.id != null && !responseData.id.Equals(""))
            {
                // Success
                MessageBox.Show("Message Posted!");
                txtpost.Text = "";
            }
            else if (responseData.error != null && responseData.error.code == 190)
            {
                if (responseData.error.error_subcode == 463)
                {
                    // Access Token Expired, need to get new token
                    FacebookClient.Instance.ExchangeAccessToken(new UploadStringCompletedEventHandler(ExchangeAccessTokenCompleted));
                }
                else
                {
                    // Another Error with Access Token, need to clear the Access Token
                    FacebookClient.Instance.AccessToken = "";
                }
            }
            else
            {
                //Error
                MessageBox.Show("You should join Lumia Kothon group before trying to Post something!");
            }
        }


        void ExchangeAccessTokenCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            // Acquire access_token and expires timestamp
            IEnumerable<KeyValuePair<string, string>> pairs = UriToolKits.ParseQueryString(e.Result);
            string accessToken = KeyValuePairUtils.GetValue(pairs, "access_token");

            if (accessToken != null && !accessToken.Equals(""))
            {
                MessageBox.Show("Access Token Exchange Failed");
                return;
            }

            // Save access_token
            FacebookClient.Instance.AccessToken = accessToken;
            FacebookClient.Instance.PostMessageOnWall(TextToPost, new UploadStringCompletedEventHandler(PostMessageOnWallCompleted));
        }

        private void ButtonBangla_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Chandrabindu.xaml", UriKind.Relative));
        }

    }
}