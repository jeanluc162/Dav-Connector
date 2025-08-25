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
    public sealed partial class EditAccountPage : Page
    {
        public EditAccountPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            (DataContext as EditAccountPageViewModel).AccountEditCompleted += EditAccountPage_AccountEditCompleted;
            if (e.Parameter is Guid)
            {
                ((EditAccountPageViewModel)DataContext).Load((Guid)e.Parameter);
            }
            else
            {
                ((EditAccountPageViewModel)DataContext).Load(null);
            }
        }

        private void EditAccountPage_AccountEditCompleted(object sender, EventArgs e)
        {
            Frame.GoBack();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            (DataContext as EditAccountPageViewModel).AccountEditCompleted -= EditAccountPage_AccountEditCompleted;
            base.OnNavigatingFrom(e);
        }
    }
}
