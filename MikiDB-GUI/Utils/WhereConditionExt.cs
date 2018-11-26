using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikiDB.Core;
using MikiDB.SQL.Stmt;
using MikiDB.Exceptions;

namespace MikiDB.Utils {
    public static class WhereConditionExt {
        public static bool Compare(this WhereConditionType whT, object v1, object v2) { 
            // check type
            if(v1.GetType() != v2.GetType()) {
                    throw new TypeErrorExc();
            }
            // cast to dynamic
            dynamic v1d = v1 as dynamic;
            dynamic v2d = v2 as dynamic;
            switch (whT) {
                case WhereConditionType.EQ:
                    return v1d == v2d;
                case WhereConditionType.NOT_EQ:
                    return v1d != v2d;
                case WhereConditionType.LAGER:
                    return v1d > v2d;
                case WhereConditionType.LAGER_EQ:
                    return v1d >= v2d;
                case WhereConditionType.LESS:
                    return v1d < v2d;
                case WhereConditionType.LESS_EQ:
                    return v1d <= v2d;
                default:
                    throw new TypeErrorExc();
            }
        }
    }
}
