using Dav_Connector.ViewModel;
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

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace Dav_Connector
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            (DataContext as MainPageViewModel).AccountEditRequested += MainPage_AccountEditRequested;
        }

        private void MainPage_AccountEditRequested(object sender, Guid? e = null)
        {
            if(e == null)
                Frame.Navigate(typeof(EditAccountPage));
            else
                Frame.Navigate(typeof(EditAccountPage), e.Value);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            (DataContext as MainPageViewModel).AccountEditRequested -= MainPage_AccountEditRequested;
            base.OnNavigatedFrom(e);
        }

    }
}
