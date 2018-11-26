using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikiDB.Utils;
using MikiDB.Exceptions;

namespace MikiDB.SQL.Stmt {
    public class SQLSelect : BaseSQLStmt {
        public string TableName { get; }
        public IList<string> ColNames { get; }
        public WhereClause Wh { get; }

        public SQLSelect(List<string> stmt) : base(StmtType.SELECT, stmt) {
            stmt.ForEach(str => str.ToLower());
            var subStmts = stmt.Split(str => str == "select" || str == "from" || str == "where").ToList();
            if(subStmts.Count == 2) {
                ColNames = subStmts[0];
                TableName = subStmts[1][0];
                Wh = null;
            }
            else if(subStmts.Count == 3) {
                ColNames = subStmts[0];
                TableName = subStmts[1][0];
                Wh = new WhereClause(subStmts[2]);
            }
            else {
                throw new SyntaxErrorExc();
            }
        }
    }
}
