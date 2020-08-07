using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KOAR_File_Editor
{
    public partial class frmMDIChild:Form
    {
        public String Filename { get; protected set; }
        public TabControl tbcMDIControl { get; set; }
        public TabPage tabMDITab { get; set; }

        public frmMDIChild() {
            InitializeComponent();
        }

        public frmMDIChild(String filename):this() {
            Filename = filename;
        }

        public virtual void SaveFile(String filepath = null){ }
        public virtual void SaveSource(String filepath = null) { }

        private void frmMDIChild_FormClosing(object sender, FormClosingEventArgs e) {
            tabMDITab.Dispose();

            if(!tbcMDIControl.HasChildren) {
                tbcMDIControl.Visible = false;
            }
        }

        private void frmMDIChild_Activated(object sender, EventArgs e) {
            tbcMDIControl.SelectedTab = tabMDITab;

            if(!tbcMDIControl.Visible) {
                tbcMDIControl.Visible = true;
            }
        }
    }
}
