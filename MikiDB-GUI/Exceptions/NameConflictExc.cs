using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikiDB.Exceptions {
    public class NameConflictExc : BaseDbExc{
        public string Name { get; }
        public NameConflictExc(string name)
            : base ("an entity of the same type named \"" + name + "\" already exits") {
            Name = name;
        }
    }
}
