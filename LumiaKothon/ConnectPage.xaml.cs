using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace LumiaKothon
{
    public partial class ConnectPage : PhoneApplicationPage
    {
        public ConnectPage()
        {
            InitializeComponent();
        }

        private void WebBrowser_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}