using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikiDB.SQL.Stmt {

    public abstract class BaseSQLStmt {
        public StmtType StmtType { get; }
        public List<string> RawContent { get; }

        public BaseSQLStmt(StmtType type, List<string> stmt) {
            StmtType = type;
            RawContent = stmt;
        }


        public override string ToString() {
            string content = "";
            // concatenate
            foreach (string str in RawContent) {
                content += (str + " ");
            }
            // remove the last space
            content = content.Substring(0, content.LastIndexOf(" "));
            // add a semicolon
            content += ";";
            return content;
        }
    }
}
