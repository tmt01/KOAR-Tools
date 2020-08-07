using KOAR_Lib.Format;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KOAR_File_Editor {
    public partial class frmLua:frmMDIChild {
        Lua _lua;
        public frmLua(String filename, bool convert = true):base(filename) {
            InitializeComponent();

            _lua = new Lua();
            _lua.Load(new FileStream(filename, FileMode.Open));

            foreach(LuaInstruction inst in _lua.Root.Instructions) {
                rtbBytecode.Text += inst.ToString() + "\n";
            }
        }
    }
}
