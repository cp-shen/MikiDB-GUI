using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikiDB.SQL.Stmt{
    public class SQLCreateDatabase : BaseSQLStmt {
        public string DbName { get; }

        public SQLCreateDatabase(List<string> stmt) : base(StmtType.CREATE_DATABASE, stmt) {
            DbName = stmt[2];
        }
    }
}
