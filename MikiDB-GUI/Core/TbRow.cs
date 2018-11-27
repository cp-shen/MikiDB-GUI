using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikiDB.Exceptions;
using MikiDB.Utils;

namespace MikiDB.Core {
    [Serializable]
    public class TbRow {
        public Table Table { get; }
        private readonly Dictionary<string, object> _kvs;

        public TbRow(Table tb) {
            Table = tb;
            _kvs = new Dictionary<string, object>();
        }

        private void SetData(string colName, object data) {
            // check data type
            Type t = Table.CopyColDefs()[colName].GetCsharpType();
            if(data.GetType() != t) {
                throw new TypeErrorExc();
            }
            // todo : check table constraints

            // set value
            _kvs.Remove(colName);
            _kvs.Add(colName, data);
        }

        public void SetData(string colName, string data) {
            // check column name
            if (!Table.CopyColDefs().ContainsKey(colName)) {
                throw new NamedItemNotFoundExc(colName);
            }
            object parsed = Table.CopyColDefs()[colName].ParseValueFromString(data);
            SetData(colName, parsed);
        }

        public object GetData(string colName) {
            // check column name
            if (!_kvs.ContainsKey(colName)) {
                throw new NamedItemNotFoundExc(colName);
            }
            return _kvs[colName];
        }

        public Dictionary<string, object> CopyData() {
            return new Dictionary<string, object>(_kvs);
        }

        public Dictionary<string, object> DataCopy {
            get { return CopyData(); }
        }
    }
}
