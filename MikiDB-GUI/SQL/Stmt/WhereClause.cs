using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikiDB.Exceptions;

namespace MikiDB.SQL.Stmt {
    public class WhereClause {
        public string ColName { get; }
        public WhereConditionType Type { get; }
        public string Value { get; }

        public WhereClause(IList<string> stmt) {
            if(stmt.Count != 3) {
                throw new SyntaxErrorExc();
            }
            ColName = stmt[0];
            Value = stmt[2];
            switch (stmt[1]) {
                case "=":
                    Type = WhereConditionType.EQ;
                    break;
                case ">":
                    Type = WhereConditionType.LAGER;
                    break;
                case "<":
                    Type = WhereConditionType.LESS;
                    break;
                case ">=":
                    Type = WhereConditionType.LAGER_EQ;
                    break;
                case "<=":
                    Type = WhereConditionType.LESS_EQ; 
                    break;
                case "<>":
                    Type = WhereConditionType.NOT_EQ;
                    break;
            }
        }
    }
}
