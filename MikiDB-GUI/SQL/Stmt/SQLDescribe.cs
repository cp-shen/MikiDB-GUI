using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikiDB.SQL.Stmt {
    public class SQLDescribe : BaseSQLStmt{
        public string TbName { get; }
        public SQLDescribe(List<string> stmt)
            : base (StmtType.DESCRIBE, stmt) {
            TbName = stmt[1];
        }
    }
}
