using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikiDB.Utils;

namespace MikiDB.SQL.Stmt {
    public class SQLInsert : BaseSQLStmt {
        public string TbName { get; }
        public Dictionary<string, string> KVpairs { get; }

        public SQLInsert(List<string> stmt) : base(StmtType.INSERT, stmt) {
            TbName = stmt[2];
            var values = stmt.Skip(3)
                .Where(v => v != "(" && v != ")")
                .Select(s => s.Replace("'", ""))
                .Select(s => s.Replace("\"", ""))
                .Split(s => s == "values")
                .Select(li => li.Split(s => s == ","))
                .ToList();
            var dict = values[0].Zip(values[1], (k, v) => new { k, v })
                .ToDictionary(x => x.k[0], x => x.v[0]);
            KVpairs = dict;
        }
    }
}
