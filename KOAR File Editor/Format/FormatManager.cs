using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOAR_File_Editor.Format
{
    static class FormatManager
    {
        public static frmMDIChild CreateWindow(String filepath) {
            String ext = Path.GetExtension(filepath);

            switch(ext) {
                case ".lua_bxml":
                    return new frmLua(filepath);
                case ".ui_bxml":
                case ".bxml":
                    return new frmBXml(filepath, true);
                case ".xml":
                    return new frmBXml(filepath, false);
                default:
                    return new frmHex(filepath);
            }
        }
    }
}
