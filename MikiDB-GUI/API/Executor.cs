using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikiDB.Core;
using MikiDB.Utils;
using MikiDB.SQL;
using MikiDB.SQL.Stmt;
using MikiDB.Exceptions;

namespace MikiDB.API {
    public class Executor {
        public DbManager DbManager { get; }

        public Executor(DbManager manager) {
            DbManager = manager;
        }

        public Result ParseAndRun(string sql) {
            try {
                return RunSQL(SQLParser.Parse(sql));
            }
            catch(BaseDbExc e) {
                return new Result(false, e.Message);
            }
        }

        public Result RunSQL(BaseSQLStmt sql) {
            try {
                switch (sql.StmtType) {
                    //case StmtType.ALTER_TABLE:
                    //    break;
                    case StmtType.CREATE_DATABASE:
                        return CreateDb(sql as SQLCreateDatabase);
                    //case StmtType.CREATE_INDEX:
                    //    break;
                    case StmtType.CREATE_TABLE:
                        return CreateTable(sql as SQLCreateTable);
                    case StmtType.DELETE:
                        return Delete(sql as SQLDelete);
                    case StmtType.DROP_DATABASE:
                        return DropDb(sql as SQLDropDatabase);
                    //case StmtType.DROP_INDEX:
                    //    break;
                    case StmtType.DROP_TABLE:
                        return DropTable(sql as SQLDropTable);
                    case StmtType.INSERT:
                        return Insert(sql as SQLInsert);
                    case StmtType.SELECT:
                        return Select(sql as SQLSelect);
                    case StmtType.SHOW_DATABASES:
                        return ShowDbs(sql as SQLShowDatabases);
                    case StmtType.SHOW_TABLES:
                        return ShowTables(sql as SQLShowTables);
                    case StmtType.UPDATE:
                        return Update(sql as SQLUpdate);
                    case StmtType.USE:
                        return UseDb(sql as SQLUse);
                    case StmtType.DESCRIBE:
                        return DescTable(sql as SQLDescribe);
                    default:
                        throw new FeatureNotSupExc();
                }
            }
            catch (BaseDbExc e) {
                return new Result(false, e.Message);
            }
        }

        public Result UseDb(string name) {

            DbManager.SetCurrentDb(name);
            return new Result(true);
        }

        public Result UseDb(SQLUse sql) {
            return UseDb(sql.DbName);
        }

        public Result CreateDb(string name) {
            DbManager.CreateDb(name);
            return new Result(true);
        }

        public Result CreateDb(SQLCreateDatabase sql) {
            return CreateDb(sql.DbName);
        }

        public Result DropDb(string name) {
            DbManager.DropDb(name);
            return new Result(true);
        }

        public Result DropDb(SQLDropDatabase sql) {
            return DropDb(sql.DbName);
        }

        public Result ShowDbs(SQLShowDatabases sql) {
            string result = "";
            List<string> names = DbManager.GetDbNames();
            foreach (string name in names) {
                result += (name + Environment.NewLine);
            }
            return new Result(true, names.Count, result);
        }

        public Result ShowTables(SQLShowTables sql) {
            string result = "";
            List<string> names = DbManager.CurrentDb.GetTableNames();
            foreach (string name in names) {
                result += (name + Environment.NewLine);
            }
            return new Result(true, names.Count, result);
        }

        public Result CreateTable(string name, Dictionary<string, DbDataType> columns) {
            Table tb = DbManager.CurrentDb.CreateTable(name, columns);
            return new Result(true);
        }

        public Result CreateTable(SQLCreateTable sql) {
            return CreateTable(sql.TbName, sql.ColumnDefs);
        }

        public Result DropTable(string name) {
            DbManager.CurrentDb.DropTable(name);
            return new Result(true);
        }

        public Result DropTable(SQLDropTable sql) {
            return DropTable(sql.TableName);
        }

        public Result DescTable(string name) {
            Dictionary<string, DbDataType> columns = DbManager.CurrentDb.GetTableByName(name).CopyColDefs();
            string msg = "";
            foreach(string colName in columns.Keys) {
                msg += (colName + " " + columns[colName].GetTypeNameString() + Environment.NewLine);
            }
            return new Result(true, columns.Count, msg);
        }

        public Result DescTable(SQLDescribe sql) {
            return DescTable(sql.TbName);
        }

        public Result Insert(string tbName, Dictionary<string, string> values) {
            DbManager.CurrentDb.GetTableByName(tbName).Insert(values);
            return new Result(true);
        }

        public Result Insert(SQLInsert sql) {
            return Insert(sql.TbName, sql.KVpairs);
        }

        public Result Delete(string tbName, WhereClause wh) {
            int num = DbManager.CurrentDb.GetTableByName(tbName).Delete(wh);
            return new Result(true, num);
        }

        public Result Delete(SQLDelete sql) {
            return Delete(sql.TableName, sql.Where);
        }

        public Result Select(string tb, IList<string> cols, WhereClause wh) {
            var selected = DbManager.CurrentDb.GetTableByName(tb).Select(cols, wh);
            string content = "";
            string lb = Environment.NewLine;
            foreach(Dictionary<string, object> row in selected) {
                foreach(var kv in row) {
                    content += (kv.Key + " : " + (kv.Value?.ToString() ?? "null") + ", ");
                }
                content += lb;
            }
            return new Result(true, selected.Count, content);
        }

        public Result Select(SQLSelect sql) {
            return Select(sql.TableName, sql.ColNames, sql.Wh);
        }

        public Result Update(string tbn, Dictionary<string, string> kvs, WhereClause wh) {
            int updated = DbManager.CurrentDb.GetTableByName(tbn).Update(kvs, wh);
            return new Result(true, updated);
        }

        public Result Update(SQLUpdate sql) {
            return Update(sql.TableName, sql.KVs, sql.Wh);
        }
    }
}
