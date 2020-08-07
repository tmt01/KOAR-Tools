using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using KOAR_Lib;
using KOAR_Lib.Format;
using System.Runtime;
using KOAR_Big_Builder.Project;
using System.Security.AccessControl;

namespace KOAR_Big_Builder
{
    public partial class frmMain:Form
    {
        private Big _big = new Big();
        private BigProject _project = new BigProject();
        private Package _package = new Package();

        private String _bigsFolder;

        public frmMain() {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e) {
            if(Properties.Settings.Default.KOARPath != "") {
                txtKOARPath.Text = Properties.Settings.Default.KOARPath;
                _bigsFolder = Path.Combine(Properties.Settings.Default.KOARPath, "bigs");
            }
        }

        private void mnuOpenProject_Click(object sender, EventArgs e) {
            if(ofdProject.ShowDialog() == DialogResult.OK) {
                _project.Load(ofdProject.FileName);
                txtPackageDir.Text = _project.TargetPackage;

                if(txtPackageDir.Text == "") txtPackageDir.Text = "001";
            }
            UpdateListView();
        }

        private void UpdateListView() {
            lsvProject.Items.Clear();

            foreach (var file in _project.Files) {
                string[] rows = { file.FileID.ToString("X8"), file.Filename, file.Flags.ToString("X"), Utils.BundleTypeName(file.Type), file.SymbolName.GetString() };
                var lvi = new ListViewItem(rows);
                lvi.Tag = file;
                lsvProject.Items.Add(lvi);
            }
        }

        private void mnuSaveProject_Click(object sender, EventArgs e) {
            if(sfdProject.ShowDialog() == DialogResult.OK) {
                _project.TargetPackage = txtPackageDir.Text;
                _project.Save(sfdProject.FileName);
            }
        }

        private void mnuProjectBuild_Click(object sender, EventArgs e) {
            if (_package.PackageName != _project.TargetPackage) LoadPackage(_project.TargetPackage);

            _project.Build(_package);
        }

        private void lsvProject_SelectedIndexChanged(object sender, EventArgs e) {
            if(lsvProject.SelectedItems.Count > 0) {
                ProjectFile file = (ProjectFile)lsvProject.SelectedItems[0].Tag;
                txtFilename.Text = file.Filename;
                txtFileID.Text = file.FileID.ToString("X");
                txtFlags.Text = file.Flags.ToString("X");
                txtType.Text = file.Type.ToString("X");
                txtSymbolName.Text = file.SymbolName.GetString();
            } else {
                ResetFileInfo();
            }
        }

        private void ResetFileInfo() {
            txtFilename.Text = "";
            txtFileID.Text = "";
            txtFlags.Text = "";
            txtType.Text = "";
            txtSymbolName.Text = "";
        }

        private void mnuAddFiles_Click(object sender, EventArgs e) {
            if(ofdAddFiles.ShowDialog() == DialogResult.OK) {
                foreach(String filename in ofdAddFiles.FileNames) {
                    // TODO: add unique fileID generation
                    _project.AddFile(filename);
                }
                UpdateListView();
            }
        }

        private void btnFileApply_Click(object sender, EventArgs e) {
            if(lsvProject.SelectedItems.Count > 0) {
                ProjectFile file = (ProjectFile)lsvProject.SelectedItems[0].Tag;
                file.Filename = txtFilename.Text;
                file.FileID = Convert.ToUInt32(txtFileID.Text, 16);
                file.Flags = Convert.ToUInt32(txtFlags.Text, 16);
                file.Type = Convert.ToUInt32(txtType.Text, 16);
                file.SymbolName = new BigHashString(txtSymbolName.Text);
                UpdateListView();
                ResetFileInfo();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if(lsvProject.SelectedItems.Count > 0) {
                _project.RemoveFile((ProjectFile)lsvProject.SelectedItems[0].Tag);
                lsvProject.Items.Remove(lsvProject.SelectedItems[0]);
            }
        }

        private void mnuOpenBig_Click(object sender, EventArgs e) {
            if(ofdBig.ShowDialog() == DialogResult.OK) {
                _big.Load(ofdBig.FileName);
                UpdateListView();
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e) {
            if(lsvProject.SelectedItems.Count > 0) {
                BigFileEntryBase file = (BigFileEntryBase)lsvProject.SelectedItems[0].Tag;

                sfdSaveFile.FileName = file.FileID.ToString("X") + ".bin";
                if(sfdSaveFile.ShowDialog() == DialogResult.OK) {
                    file.SaveToFile(sfdSaveFile.FileName);
                }
            }
        }

        private void txtKOARPath_TextChanged(object sender, EventArgs e) {
            Properties.Settings.Default.KOARPath = txtKOARPath.Text;
        }

        private void UpdatePackageList() {
            lsvPackage.Items.Clear();

            foreach(var file in _package.Main.Bigs) {
                var lvi = new ListViewItem(file.Filename.Replace(_bigsFolder, ""));
                lvi.Tag = file;
                lsvPackage.Items.Add(lvi);
            }
        }

        private void LoadPackage(String package) {
            _bigsFolder = Path.Combine(Properties.Settings.Default.KOARPath, "bigs");
            _package = new Package();
            _package.Load(_bigsFolder, package, chkPackageIncludeMods.Checked);

            txtPackageDir.Text = package;
            UpdatePackageList();
        }

        private void mnuPackageLoadTarget_Click(object sender, EventArgs e) {
            LoadPackage(txtPackageDir.Text);
        }

        private void mnuPackageLoadAll_Click(object sender, EventArgs e) {
            _bigsFolder = Path.Combine(Properties.Settings.Default.KOARPath, "bigs");
            _package = new Package();
            _package.Load(_bigsFolder, "", chkPackageIncludeMods.Checked);

            UpdatePackageList();
        }

        private void btnFind_Click(object sender, EventArgs e) {
            if (_package.Main != null) {
                if(txtFindFileID.Text == "") return;

                var results = _package.Main.Find(Convert.ToUInt32(txtFindFileID.Text, 16), chkFindNormalize.Checked);
                if(results.Count == 0) {
                    MessageBox.Show(String.Format("No results found for {0}", txtFindFileID.Text));
                } else {
                    String s = String.Format("Results for {0}:\n", txtFindFileID.Text);
                    foreach(var r in results) {
                        s += String.Format(r.Filename.Replace(_bigsFolder, "") + " (" + r.FileID.ToString("X8") + ", " + r.Flags.ToString("X") + ")\n");
                    }
                    MessageBox.Show(s);
                }
            }
        }

        private void btnHashGenerate_Click(object sender, EventArgs e) {
            txtHash.Text = Utils.SH(txtHashSource.Text).ToString("X");
        }

        private void btnCreate_Click(object sender, EventArgs e) {
            if(txtFilename.Text == "") return;

            String filename = txtFilename.Text;
            UInt32 fileID = txtFileID.Text != "" ? Convert.ToUInt32(txtFileID.Text, 16) : 0;
            UInt32 flags = Convert.ToUInt32(txtFlags.Text, 16);
            UInt32 type = Convert.ToUInt32(txtType.Text, 16);
            String symbolName = txtSymbolName.Text;
            _project.AddFile(filename, fileID, symbolName, type, flags);
            UpdateListView();
        }
    }
}
