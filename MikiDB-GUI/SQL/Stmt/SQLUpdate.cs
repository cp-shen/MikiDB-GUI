using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikiDB.Utils;
using MikiDB.Exceptions;

namespace MikiDB.SQL.Stmt {
    public class SQLUpdate : BaseSQLStmt {
        public string TableName { get; }
        public Dictionary<string, string> KVs { get; }
        public WhereClause Wh { get; }

        public SQLUpdate(List<string> stmt) : base(StmtType.UPDATE, stmt) {
            stmt.ForEach(s => s.ToLower());
            TableName = stmt[1];
            var subStmts = stmt.Split(str => str == "update" || str == "set" || str == "where").ToList();
            if(subStmts.Count < 2) {
                throw new SyntaxErrorExc();
            }
            TableName = subStmts[0][0];
            KVs = subStmts[1].Split(s => s == ",").ToList().ToDictionary(list => list[0], list => list[2]);
            if(subStmts.Count > 2) {
                Wh = new WhereClause(subStmts[2]);
            }
            else {
                Wh = null;
            }
        }
    }
}
