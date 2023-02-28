using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decor.Data
{
    public class DataAccess
    {
        public delegate void RefreshListDelegate();
        public static event RefreshListDelegate RefreshList;

        public static List<Product> GetProducts() => DecorMishaEntities.GetContext().Products.ToList();
        public static List<User> GetUsers() => DecorMishaEntities.GetContext().Users.ToList();

        internal static User GetUser(string login, string password)
        {
            return GetUsers().FirstOrDefault(x => x.Login == login && x.Password == password);
        }

        internal static void SaveProduct(Product product)
        {
            if (product.Id == 0)
                DecorMishaEntities.GetContext().Products.Add(product);

            DecorMishaEntities.GetContext().SaveChanges();
            RefreshList?.Invoke();
        }

        internal static List<Supplier> GetSuppliers()
        {
            return DecorMishaEntities.GetContext().Suppliers.ToList();
        }

        internal static List<Unit> GetUnits()
        {
            return DecorMishaEntities.GetContext().Units.ToList();
        }

        internal static List<ProductCategory> GetProductCategories()
        {
            return DecorMishaEntities.GetContext().ProductCategories.ToList();
        }

        internal static void DeleteProduct(Product product)
        {
            DecorMishaEntities.GetContext().Products.Remove(product);
            DecorMishaEntities.GetContext().SaveChanges();
            RefreshList?.Invoke();
        }
    }
}
