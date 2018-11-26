using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikiDB.SQL.Stmt{

    public class SQLDropTable : BaseSQLStmt {
        public string TableName { get; }
        public SQLDropTable(List<string> stmt) : base(StmtType.DROP_TABLE, stmt) {
            TableName = stmt[2];
        }
    }
}
