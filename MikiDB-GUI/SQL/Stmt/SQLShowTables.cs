using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikiDB.SQL.Stmt {
    public class SQLShowTables : BaseSQLStmt {
        public SQLShowTables(List<string> stmt) : base(StmtType.SHOW_TABLES, stmt) { }
    }
}
