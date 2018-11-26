using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using MikiDB.SQL.Stmt;
using MikiDB.Exceptions;
using MikiDB.Core;

namespace MikiDB.SQL {
    public static class SQLParser {
        public static BaseSQLStmt Parse(string raw) {
            try {
                List<string> formatted = FormatRaw(raw);
                return BuildSql(formatted);
            }
            catch(Exception) {
                throw new SyntaxErrorExc();
            }
        }

        private static List<string> FormatRaw(string raw) {
            string formatted = raw.ToLower();
            // replace newline, tab, enter with one space
            formatted = Regex.Replace(formatted, @"[\r\n\t]", " ");
            // remove ; and chars after ;
            formatted = Regex.Replace(formatted, @";.*$", "");
            // remove leading spaces and trailing spaces
            formatted = Regex.Replace(formatted, @"(^ +)|( +$)", "");
            // remove duplicate spaces
            formatted = Regex.Replace(formatted, @" +", " ");
            // insert space before or after ( ) , = <> < >
            formatted = Regex.Replace(formatted, @" ?(\(|\)|,|=|(<>)|<|>) ?", " $1 ");
            // remove space between comparison operators
            formatted = Regex.Replace(formatted, @"< +>", "<>");
            formatted = Regex.Replace(formatted, @"< +=", "<=");
            formatted = Regex.Replace(formatted, @"> +=", ">=");

            return formatted.Split(' ').ToList();
        }

        private static BaseSQLStmt BuildSql(List<string> stmt) {
            if (stmt.Count < 2) {
                // todo
                throw new SyntaxErrorExc();
            }
            else {
                string flag1 = stmt[0].ToLower();
                string flag2 = stmt[1].ToLower();
                switch (flag1) {
                    case "create":
                        switch (flag2) {
                            case "database":
                                return new SQLCreateDatabase(stmt);
                            case "table":
                                return new SQLCreateTable(stmt);
                            case "index":
                                throw new FeatureNotSupExc("create index");
                            default:
                                // todo
                                throw new SyntaxErrorExc();
                        }
                    case "show":
                        switch (flag2) {
                            case "databases":
                                return new SQLShowDatabases(stmt);
                            case "tables":
                                return new SQLShowTables(stmt);
                            default:
                                // todo
                                throw new SyntaxErrorExc();
                        }
                    case "drop":
                        switch (flag2) {
                            case "database":
                                return new SQLDropDatabase(stmt);
                            case "table":
                                return new SQLDropTable(stmt);
                            case "index":
                                throw new FeatureNotSupExc("drop index");
                            default:
                                // todo
                                throw new SyntaxErrorExc();
                        }
                    case "use":
                        return new SQLUse(stmt);
                    case "insert":
                        if (flag2 == "into") {
                            return new SQLInsert(stmt);
                        }
                        else {
                            throw new SyntaxErrorExc();
                        }
                    case "select":
                        return new SQLSelect(stmt);
                    case "delete":
                        if (flag2 == "from") {
                            return new SQLDelete(stmt);
                        }
                        else {
                            throw new SyntaxErrorExc();
                        }
                    case "update":
                        return new SQLUpdate(stmt);
                    case "describe":
                    case "desc":
                        return new SQLDescribe(stmt);
                    default:
                        // todo
                        throw new SyntaxErrorExc();
                }
            }
        }

        public static DbDataType ToDbDataType(string str) {
            str = str.ToLower();
            switch (str) {
                case "int":
                    return DbDataType.INT;
                case "float":
                case "double":
                    return DbDataType.DOUBLE;
                case "date":
                case "datetime":
                    return DbDataType.DATETIME;
                case "string":
                case "text":
                case "char":
                case "varchar":
                case "varchar2":
                    return DbDataType.TEXT;
                default:
                    throw new SyntaxErrorExc();
            }
        }
    }
}
