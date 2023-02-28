using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Decor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DecorWindow: Window
    {
        public DecorWindow()
        {
            InitializeComponent();
            frmMain.Navigated += FrmMain_Navigated;
            frmMain.NavigationService.Navigate(new Pages.AuthorizationPage());
        }

        private void FrmMain_Navigated(object sender, NavigationEventArgs e)
        {
            tbTitle.Text = (frmMain.Content as Page).Title;
            btnGoBack.Visibility = frmMain.NavigationService.CanGoBack? Visibility.Visible: Visibility.Collapsed;
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            if (frmMain.NavigationService.CanGoBack)
                frmMain.NavigationService.GoBack();
        }
    }
}
