using KOAR_File_Editor.Format;
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

namespace KOAR_File_Editor
{
    public partial class frmMain:Form
    {
        public frmMain() {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e) {

        }

        private void mnuFileOpen_Click(object sender, EventArgs e) {
            if(ofdOpenFile.ShowDialog() == DialogResult.OK) {
                foreach(String filepath in ofdOpenFile.FileNames) {
                    frmMDIChild form = FormatManager.CreateWindow(filepath);

                    String filename = Path.GetFileName(filepath);

                    form.MdiParent = this;
                    form.Text = filename;

                    form.tbcMDIControl = tbcMDIChildren;
                    TabPage tab = new TabPage(filename);
                    tab.Parent = tbcMDIChildren;
                    tab.Tag = form;
                    tab.MouseClick += new MouseEventHandler(tab_MouseClick);
                    tab.Show();
                    form.tabMDITab = tab;

                    form.Show();
                }
            }
        }

        private void tab_MouseClick(object sender, MouseEventArgs e) {
            if(e.Button == MouseButtons.Middle) {
                var form = (frmMDIChild)sender;
                form.Close();
            }
        }

        private void tbcMDIChildren_SelectedIndexChanged(object sender, EventArgs e) {
            if(tbcMDIChildren.SelectedTab != null) {
                var form = (frmMDIChild)tbcMDIChildren.SelectedTab.Tag;
                form.Select();
            }
        }

        private void mnuFileSave_Click(object sender, EventArgs e) {
            if(tbcMDIChildren.TabCount > 0) {
                var form = (frmMDIChild)tbcMDIChildren.SelectedTab.Tag;
                form.SaveFile();
            }
        }
    }
}
