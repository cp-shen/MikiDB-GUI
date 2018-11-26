using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikiDB.SQL.Stmt{ 
    public class SQLDropDatabase : BaseSQLStmt {
        public string DbName { get; }
        public SQLDropDatabase(List<string> stmt) : base(StmtType.DROP_DATABASE, stmt) {
            DbName = stmt[2];
        }
    }
}
