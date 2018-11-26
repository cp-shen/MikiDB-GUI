using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MikiDB.Core;
using MikiDB.Exceptions;
using MikiDB.SQL.Stmt;

namespace MikiDB.Core {
    /// <summary>
    /// singleton class to manage all the databases
    /// </summary>
    public sealed class DbManager {
        private static readonly DbManager instance = new DbManager();
        public static DbManager Instance { get { return instance; } }
        static DbManager() { }
        private DbManager() {

        }

        private List<Database> _databases = new List<Database>();
        private Database _currentDb = null;

        public Database CurrentDb {
            get {
                if(_currentDb != null) {
                    return _currentDb;
                }
                else {
                    throw new DbNotSelectedExc();
                }
            }
        }

        public void SetCurrentDb(string name) {
            _currentDb = GetDbByName(name);
        }

        public Database GetDbByName(string name) {
            foreach(Database db in _databases) {
                if(db.DbName == name) {
                    return db;
                }
            }
            throw new NamedItemNotFoundExc(name);
        }

        public List<Database> CopyAllDbs() {
            return new List<Database>(_databases);
        }

        public List<string> GetDbNames() {
            List<string> names = new List<string>();
            foreach(Database db in _databases) {
                names.Add(db.DbName);
            }
            return names;
        }

        public Database CreateDb(string name) {
            if (GetDbNames().Contains(name)) {
                throw new NameConflictExc(name);
            }
            else {
                Database db = new Database(this, name);
                _databases.Add(db);
                return db;
            }
        }

        public void DropDb(string name) {
            _databases.Remove(GetDbByName(name));
        }
    }
}