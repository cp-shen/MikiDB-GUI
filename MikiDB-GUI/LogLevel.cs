using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikiDB_GUI {
    public enum LogLevel {
        ACCEPTED,
        INFO,
        WARN,
        ERROR,
    }

    public static class LogLevelExt {
        public static string GetStr(this LogLevel lv) {
            switch (lv) {
                case LogLevel.ACCEPTED:
                    return "[Get Input] ";
                case LogLevel.WARN:
                    return "[WARN] ";
                case LogLevel.ERROR:
                    return "[ERROR] ";
                case LogLevel.INFO:
                    return "";
                default:
                    return "";
            }
        }
    }
}
