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
using Microsoft.Win32;

namespace Decor.Pages
{
    /// <summary>
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public Product Product { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<Unit> Units { get; set; }
        public ProductPage(Product product)
        {
            InitializeComponent();
            Product = product;
            Suppliers = DataAccess.GetSuppliers();
            ProductCategories = DataAccess.GetProductCategories();
            Units = DataAccess.GetUnits();
            if (Product.Id != 0)
            {
                tbArticle.IsEnabled = false;
            }
            if(App.User == null || App.User.Role.Name != "Администратор")
            {
                btnDelete.Visibility = Visibility.Collapsed;
            }

            if (App.User == null)
            {
                this.IsEnabled = false;
            }

            DataContext = this;
        }

        private void btnChangePhoto_Click(object sender, RoutedEventArgs e)
        {
            var openFileDilog = new OpenFileDialog();
            if (openFileDilog.ShowDialog() != null)
            {
                if (openFileDilog.FileName != "")
                {
                    Product.Image = File.ReadAllBytes(openFileDilog.FileName);
                    imgProduct.Source = new BitmapImage(new Uri(openFileDilog.FileName));
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить?", "", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                DataAccess.DeleteProduct(Product);
                NavigationService.GoBack();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var stringBuilder = new StringBuilder();
            try
            {
                if (Product.CurrentSale < 0)
                    stringBuilder.AppendLine("Скидка не может быть < 0");
                if (Product.MaxSaleAmount < 0)
                    stringBuilder.AppendLine("максимальная скидка не может быть < 0");
                if (Product.CurrentSale > 100)
                    stringBuilder.AppendLine("Скидка не может быть < 100");
                if (Product.MaxSaleAmount > 100)
                    stringBuilder.AppendLine("максимальная скидка не может быть > 100");
                if (string.IsNullOrEmpty(Product.Name))
                    stringBuilder.AppendLine("Заполните имя");

                if (stringBuilder.Length > 0)
                {
                    MessageBox.Show(stringBuilder.ToString());
                    return;
                }


                DataAccess.SaveProduct(Product);
                NavigationService.GoBack();
            }
            catch
            {
                MessageBox.Show("Ошибка \n"+ stringBuilder.ToString());
            }
        }
    }
}
