using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decor.Data
{
    public partial class Product
    {
        public string Background => CurrentSale > 15 ? "#7fff00" : "";
    }
}
