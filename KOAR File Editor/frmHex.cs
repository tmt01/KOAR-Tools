using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KOAR_File_Editor
{
    public partial class frmHex:frmMDIChild
    {
        public frmHex(String filename):base(filename) {
            InitializeComponent();
            ByteViewer bv = new ByteViewer();
            bv.Dock = DockStyle.Fill;
            bv.SetFile(filename); // or SetBytes
            Controls.Add(bv);
        }

        private void frmHex_Load(object sender, EventArgs e) {
            
        }
    }
}
