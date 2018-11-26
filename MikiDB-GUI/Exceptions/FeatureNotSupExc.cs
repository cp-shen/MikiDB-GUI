using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikiDB.Exceptions {
    public class FeatureNotSupExc : BaseDbExc {
        public FeatureNotSupExc(string fName)
            : base("feature \""  + fName + "\" not supported") {

        }

        public FeatureNotSupExc()
            : base("feature not supported") {

        }
    }
}
