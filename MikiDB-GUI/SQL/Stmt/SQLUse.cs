using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikiDB.SQL.Stmt {

    public class SQLUse : BaseSQLStmt {
        public string DbName { get; }
        public SQLUse(List<string> stmt) : base(StmtType.USE, stmt) {
            DbName = stmt[1];
        }
    }
}
