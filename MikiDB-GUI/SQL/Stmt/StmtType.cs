using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikiDB.SQL.Stmt{
    public enum StmtType {
        USE,
        INSERT,
        SELECT,
        DELETE,
        UPDATE,

        CREATE_DATABASE,
        DROP_DATABASE,
        SHOW_DATABASES,

        CREATE_TABLE,
        DROP_TABLE,
        ALTER_TABLE,
        SHOW_TABLES,
        DESCRIBE,

        CREATE_INDEX,
        DROP_INDEX,
    }
}
