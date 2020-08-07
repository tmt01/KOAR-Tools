using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KOAR_Big_Builder
{
    static class Helper
    {
        public static string GetRelativePath(string fromPath, string toPath) {
            int fromAttr = GetPathAttribute(fromPath);
            int toAttr = GetPathAttribute(toPath);

            StringBuilder path = new StringBuilder(260); // MAX_PATH
            if(PathRelativePathTo(
                path,
                fromPath,
                fromAttr,
                toPath,
                toAttr) == 1) {
                return path.ToString();
            }
            return toPath;
        }

        private static int GetPathAttribute(string path) {
            try{
                DirectoryInfo di = new DirectoryInfo(path);
                if(di.Exists) {
                    return FILE_ATTRIBUTE_DIRECTORY;
                }
                return FILE_ATTRIBUTE_NORMAL;
            }catch (ArgumentException ex) {
                return FILE_ATTRIBUTE_NORMAL;
            }
        }

        private const int FILE_ATTRIBUTE_DIRECTORY = 0x10;
        private const int FILE_ATTRIBUTE_NORMAL = 0x80;

        [DllImport("shlwapi.dll", SetLastError = true)]
        private static extern int PathRelativePathTo(StringBuilder pszPath,
            string pszFrom, int dwAttrFrom, string pszTo, int dwAttrTo);
    }
}
