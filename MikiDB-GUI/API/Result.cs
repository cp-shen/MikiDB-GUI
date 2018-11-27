using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikiDB.API {
    public class Result {
        public bool Succeeded { get; }
        public int CountAffected { get; }
        public string Content { get; }

        public Result (bool suc) {
            Succeeded = suc;
            CountAffected = 0;
            Content = "";
        }

        public Result (bool suc, int count) {
            Succeeded = suc;
            CountAffected = count;
            Content = "";
        }

        public Result (bool suc, string content) {
            Succeeded = suc;
            CountAffected = 0;
            Content = content;
        }

        public Result (bool suc, int count, string content) {
            Succeeded = suc;
            CountAffected = count;
            Content = content;
        }

        public override string ToString() {
            string str = "";
            string lb = Environment.NewLine;
            string divider = "-----------------------------------------";

            if (Succeeded) {
                str += "OK";
            }
            else {
                str += "Failed";
            }
            str += lb;
            if(CountAffected != 0) {
                str += (divider + lb);
                str += (CountAffected + " items queried" + lb);
            }
            if(Content != "") {
                str += (divider + lb);
                str += (Content + lb);
            }
            return str;
        }
    }
}
