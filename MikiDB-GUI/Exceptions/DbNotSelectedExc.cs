using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikiDB.Exceptions {
    public class DbNotSelectedExc : BaseDbExc {
        public DbNotSelectedExc() 
            : base("database not selected") {

        }
    }
}
