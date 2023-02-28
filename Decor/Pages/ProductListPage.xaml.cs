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
        public List<Product> ProductsForFilters { get; set; }
        public Dictionary<string, Func<Product, object>> Sortings { get; set; }
        public Dictionary<string, Func<Product, bool>> Filters { get; set; }

        public ProductListPage()
        {
            InitializeComponent();
            Products = DataAccess.GetProducts();
            Sortings = new Dictionary<string, Func<Product, object>>
            {
                { "Стоимость по возрастанию", x => x.Price },
                { "Стоимость по убыванию", x => x.Price },
            };

            Filters = new Dictionary<string, Func<Product, bool>>
            {
                { "Все диапазоны", x => true },
                { "0 - 10", x => x.CurrentSale <= 10 },
                { "11 - 14", x => x.CurrentSale <= 14 && x.CurrentSale >= 11 },
                { "15+", x => x.CurrentSale >= 15 },
            };
            DataAccess.RefreshList += DataAccess_RefreshList;
            if (App.User == null)
            {
                btnAddProduct.Visibility = Visibility.Collapsed;
            }

            DataContext = this;
        }

        private void DataAccess_RefreshList()
        {
            Products = DataAccess.GetProducts();
            lvProducts.ItemsSource = Products;
            lvProducts.Items.Refresh();
        }

        private void Filter()
        {
            var searchText = tbSearch.Text.ToLower();
            var filter = cbFilter.SelectedItem as string;
            var sort = cbSort.SelectedItem as string;

            if (filter != null && sort != null)
            {
                ProductsForFilters = Products.Where(Filters[filter])
                    .Where(x => x.Name.ToLower().Contains(searchText)).ToList();
                ProductsForFilters = ProductsForFilters.OrderBy(Sortings[sort]).ToList();
                if (sort.Contains("убыванию"))
                {
                    ProductsForFilters.Reverse();
                }

                lvProducts.ItemsSource = ProductsForFilters;
                lvProducts.Items.Refresh();
                tbProductCount.Text = $"{ProductsForFilters.Count} из {Products.Count}";
            }
        }

        private void lvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var product = lvProducts.SelectedItem as Product;
            if (product != null)
                NavigationService.Navigate(new Pages.ProductPage(product));
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.ProductPage(new Product()));
        }
    }
}
