using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MikiDB.Core;
using MikiDB.SQL;
using MikiDB.Exceptions;
using MikiDB.Utils;

namespace MikiDB.SQL.Stmt{
    public class SQLCreateTable : BaseSQLStmt {
        public string TbName { get; }

        public Dictionary<string, DbDataType> ColumnDefs { get; }
        public List<Tuple<string, ConsType, List<string>>> Constraints { get; }

        public SQLCreateTable(List<string> stmt) : base(StmtType.CREATE_TABLE, stmt) {
            TbName = stmt[2];

            int lParenPos = stmt.IndexOf("(");
            int rParenPos = stmt.IndexOf(")");
            if (lParenPos == -1 || rParenPos == -1 || (rParenPos - lParenPos) < 3) {
                throw new SyntaxErrorExc();
            }
            stmt = stmt.GetRange(lParenPos + 1, rParenPos - lParenPos - 1);
            ColumnDefs = stmt.Split(s => s == ",").ToDictionary(expr => expr[0], expr => SQLParser.ToDbDataType(expr[1]));
            // todo: handle table constraints
        }
    }
}
