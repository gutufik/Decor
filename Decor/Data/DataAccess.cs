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
        }
    }
}
