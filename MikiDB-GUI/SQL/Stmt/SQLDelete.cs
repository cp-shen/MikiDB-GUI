using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikiDB.SQL.Stmt {

    public class SQLDelete : BaseSQLStmt {
        public string TableName { get; }
        public WhereClause Where { get; }

        public SQLDelete(List<string> stmt) : base(StmtType.DELETE, stmt) {
            TableName = stmt[2];
            if(stmt.Count > 4) {
                Where = new WhereClause(stmt.Skip(4).ToList());
            }
            else {
                Where = null;
            }
        }
    }
}
