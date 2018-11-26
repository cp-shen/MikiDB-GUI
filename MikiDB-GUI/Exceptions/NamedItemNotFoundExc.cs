using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MikiDB.Exceptions {
    public class NamedItemNotFoundExc : BaseDbExc {
        public string Name { get; }
        public NamedItemNotFoundExc(string dbName)
            : base("\"" + dbName + "\" not found") {
            Name = dbName;
        }
    } 
}
