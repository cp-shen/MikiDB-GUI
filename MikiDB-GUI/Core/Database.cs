using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikiDB.Exceptions;
using MikiDB.API;

namespace MikiDB.Core {
    [Serializable]
    public class Database {
        [NonSerialized]
        private DbManager _manager;
        public DbManager Manager {
            get { return _manager; }
            set {
                foreach (string name in value.GetDbNames()) {
                    if(name == _dbName) {
                        throw new NameConflictExc(name);
                    }
                }
                _manager = value;
            }
        }
        private readonly List<Table> _tables;
        private string _dbName;
        public string DbName {
            get {
                return _dbName; 
            }
            set {
                foreach(string name in Manager.GetDbNames()) {
                    if(name == value) {
                        throw new NameConflictExc(name);
                    }
                }
                _dbName = value;
            }
        }


        public Database(DbManager manager, string name) {
            Manager = manager;
            DbName = name;
            _tables = new List<Table>();
        }

        public Table GetTableByName(string name) {
            foreach(Table tb in _tables) {
                if(tb.TbName == name) {
                    return tb;
                }
            }
            throw new NamedItemNotFoundExc(name);
        }

        public List<Table> CopyTables() {
            return new List<Table>(_tables);
        }

        public List<string> GetTableNames() {
            var tbNames = new List<string>();
            foreach(Table tb in _tables) {
                tbNames.Add(tb.TbName);
            }
            return tbNames;
        }

        public Table CreateTable(string name, Dictionary<string, DbDataType> columns) {
            if (GetTableNames().Contains(name)) {
                throw new NameConflictExc(name);
            }
            else {
                Table tb = new Table(this, name);
                foreach(string colName in columns.Keys) {
                    tb.CreateColumn(colName, columns[colName]);
                }
                _tables.Add(tb);
                return tb;
            }
        }

        public void DropTable(string name) {
            _tables.Remove(GetTableByName(name));
        }
    }
}
