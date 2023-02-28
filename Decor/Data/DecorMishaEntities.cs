using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decor.Data
{
    public partial class DecorMishaEntities
    {
        private static DecorMishaEntities _context;

        public static DecorMishaEntities GetContext()
        {
            if(_context == null )
                _context = new DecorMishaEntities();

            return _context;
        }
    }
}
