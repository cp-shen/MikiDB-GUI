using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikiDB.SQL.Stmt{
    public class SQLShowDatabases : BaseSQLStmt {
        public SQLShowDatabases(List<string> stmt) : base(StmtType.SHOW_DATABASES, stmt) { }
    }
}