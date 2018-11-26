using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikiDB.SQL.Stmt;
using MikiDB.SQL;
using MikiDB.Core;
using MikiDB.Exceptions;
using MikiDB.API;

namespace MikiDB.CLI{
    class Program {
        static void Main(string[] args) {
            Executor executor = new Executor(DbManager.Instance);

            while (true) {
                Console.Write("MikiDB > ");
                string input = "";
                do {
                    input += Console.ReadLine();
                }
                while (input.IndexOf(";") == -1);

                Result result = executor.ParseAndRun(input);
                Console.WriteLine(result.ToString());
            }
        }
    }
}
