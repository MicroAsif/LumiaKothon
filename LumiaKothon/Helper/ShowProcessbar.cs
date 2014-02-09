using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LumiaKothon.Helper
{
    static class ProgressIndicatorHelper
    {
        private static ProgressIndicator _ProgressIndicator;
        private static PhoneApplicationPage _CurrentPage;

        public static void ShowProgressIndicator(string msg)
        {
            _ProgressIndicator = new ProgressIndicator() { IsIndeterminate = true };
            _ProgressIndicator.Text = msg;
            _ProgressIndicator.IsVisible = true;

            _CurrentPage = (Application.Current.RootVisual as PhoneApplicationFrame).Content as PhoneApplicationPage;

            SystemTray.SetProgressIndicator(_CurrentPage, _ProgressIndicator);
        }

        public static void HideProgressIndicator()
        {
            if (_ProgressIndicator != null && _CurrentPage != null) //just a sanity check;
            {
                _ProgressIndicator.IsVisible = false;
                SystemTray.SetProgressIndicator(_CurrentPage, _ProgressIndicator);
            }
        }
    }
}
