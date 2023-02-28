using System;
using System.IO;
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
using Decor.Data;

namespace Decor.Pages
{
    /// <summary>
    /// Interaction logic for ProductListPage.xaml
    /// </summary>
    public partial class ProductListPage : Page
    {
        public List<Product> Products { get; set; }
        public ProductListPage()
        {
            InitializeComponent();
            Products = DataAccess.GetProducts();

            DataContext = this;

        }

        private void lvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
