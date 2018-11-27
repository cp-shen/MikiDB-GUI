using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikiDB.Exceptions;
using MikiDB.Utils;
using MikiDB.SQL.Stmt;

namespace MikiDB.Core {
    [Serializable]
    public class Table {
        public Database Db { get; }
        private string _tbName;
        public string TbName {
            get { return _tbName; }
            set {
                foreach(string name in Db.GetTableNames()) {
                    if(name == value) {
                        throw new NameConflictExc(name);
                    }
                }
                _tbName = value;
            }
        }
        private readonly Dictionary<string, DbDataType> _columnDefs;
        private readonly Dictionary<string, Tuple<ConsType, List<string>>> _constraints;
        private readonly List<TbRow> _rows;

        public Table(Database db, string name) {
            Db = db;
            TbName = name;
            _columnDefs = new Dictionary<string, DbDataType>();
            _constraints = new Dictionary<string, Tuple<ConsType, List<string>>>();
            _rows = new List<TbRow>();
        }

        public void CreateColumn(string name, DbDataType dataType) {
            if (_columnDefs.ContainsKey(name)) {
                throw new NameConflictExc(name);
            }
            if(_rows.Count > 0) {
                //todo
                throw new FeatureNotSupExc();
            }
            else {
                _columnDefs.Add(name, dataType);
            }
        }

        public void AddConstraint(string name, ConsType type, List<string> cols) {
            //todo
            throw new FeatureNotSupExc();
        }

        public void AddConstraint(ConsType type, List<string> cols) {
            //todo
            throw new FeatureNotSupExc();
        }

        public List<TbRow> CopyRows() {
            return new List<TbRow>(_rows);
        }

        public Dictionary<string, Tuple<ConsType, List<string>>> CopyConstraints() {
            return new Dictionary<string, Tuple<ConsType, List<string>>>(_constraints);
        }

        public Dictionary<string, DbDataType> CopyColDefs() {
            return new Dictionary<string, DbDataType>(_columnDefs);
        }

        public void Insert(Dictionary<string, string> kvs) {
            var r = new TbRow(this);
            foreach(string k in kvs.Keys) {
                r.SetData(k, kvs[k]);
            }
            _rows.Add(r);
        }

        private Predicate<TbRow> GetPredicate(WhereClause wh) {
            // check col name in wh and get predicate
            Predicate<TbRow> p;
            if(wh == null) {
                p = row => true;
            }
            else {
                // check col name
                if (!_columnDefs.ContainsKey(wh.ColName)) {
                    throw new NamedItemNotFoundExc(wh.ColName);
                }
                object value = _columnDefs[wh.ColName].ParseValueFromString(wh.Value);
                p = delegate(TbRow r) {
                    try {
                        return wh.Type.Compare(r.GetData(wh.ColName), value);
                    }
                    catch (BaseDbExc) {
                        // not comparable
                        return true;
                    }
                };
            }
            return p;
        }

        public int Delete(WhereClause wh) {
            return _rows.RemoveAll(GetPredicate(wh));
        }

        public List<Dictionary<string, object>> Select(IList<string> cols, WhereClause wh) {
            var result = new List<Dictionary<string, object>>();
            // get rows
            List<TbRow> rows = new List<TbRow>(_rows.FindAll(GetPredicate(wh)));
            // filter columns
            if(cols[0] == "*") {
                cols = new List<string>(_columnDefs.Keys);
            }
            rows.ForEach(row => {
                var filledRow = new Dictionary<string, object>();
                foreach(string col in cols) {
                    try {
                        filledRow.Add(col, row.GetData(col));
                    }
                    catch (NamedItemNotFoundExc) {
                        filledRow.Add(col, null);
                    }
                }
                result.Add(filledRow);
            });
            return result;
        }

        public int Update(Dictionary<string, string> kvs, WhereClause wh) {
            var rows = _rows.FindAll(GetPredicate(wh));
            foreach(var row in rows) {
                foreach(string k in kvs.Keys) {
                    row.SetData(k, kvs[k]); 
                }
            }
            return rows.Count;
        }
    }
}
