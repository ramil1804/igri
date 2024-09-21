using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.db
{
    public class ConnectionDB
    {
        public static TestDBWpfEntities db = new TestDBWpfEntities();
        public static TestDBWpfEntities1 db1 = new TestDBWpfEntities1();
        public static users user;
        public static HistoryMatches history;
    }
}
