using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikiDB.Core;
using MikiDB.Exceptions;

namespace MikiDB.Utils {
    public static class DbDataTypeExtension {
        public static string GetTypeNameString(this DbDataType type) {
            switch (type) {
                case DbDataType.INT:
                    return "INT";
                case DbDataType.DOUBLE:
                    return "DOUBLE";
                case DbDataType.TEXT:
                    return "TEXT";
                case DbDataType.DATETIME:
                    return "DATETIME";
                default:
                    throw new FeatureNotSupExc();
            }
        }
        public static object ParseValueFromString(this DbDataType type, string str) {
            try {
                switch (type) {
                    case DbDataType.INT:
                        return int.Parse(str);
                    case DbDataType.DOUBLE:
                        return double.Parse(str);
                    case DbDataType.TEXT:
                        return str;
                    case DbDataType.DATETIME:
                        return DateTime.Parse(str);
                    default:
                        throw new SyntaxErrorExc();
                }
            }
            catch(Exception) {
                throw new SyntaxErrorExc();
            }
        }
        public static Type GetCsharpType(this DbDataType type) {
            switch (type) {
                case DbDataType.INT:
                    return typeof(int);
                case DbDataType.DOUBLE:
                    return typeof(double);
                case DbDataType.TEXT:
                    return typeof(string);
                case DbDataType.DATETIME:
                    return typeof(DateTime);
                default:
                    throw new FeatureNotSupExc();
            }
        }
    }
}
