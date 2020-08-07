using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KOAR_Big_Builder
{
    static class Program
    {
        [STAThread]
        static void Main() {
            Directory.CreateDirectory("tmp");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
            Properties.Settings.Default.Save();
        }
    }
}
