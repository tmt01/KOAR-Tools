using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KOAR_Lib;
using KOAR_Lib.Format;

namespace ConsoleTesting {
    class Program {
        private const String KOAR_PATH = "C:\\Programs\\Steam\\steamapps\\common\\KOAReckoning";
        private const String SYMTYPE_FORMAT = "{0} ({1})";
        static void Main(string[] args) {
            Package package = new Package();
            package.Load(Path.Combine(KOAR_PATH, "bigs"), "001");

            var file = package.Main.FindFirst(Utils.SH(".\\symbol_table_simtype.bin"));

            SymbolTable st = new SymbolTable();
            st.Load(file.GetStream());

            var dict_id = st.GetIdToNameDict();
            var dict_name = st.GetNameToIDDict();

            var id = dict_name["PARENT_Daggers"];
            Console.WriteLine(id);
            Console.WriteLine(dict_id[id]);
        }
    }
}
