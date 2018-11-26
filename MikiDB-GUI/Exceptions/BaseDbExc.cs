using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikiDB.Exceptions {
    public class BaseDbExc : Exception{
        public BaseDbExc(string msg) 
            : base(msg) {

        }
    }
}
