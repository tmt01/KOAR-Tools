using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace KOAR_Lib
{
    public class Package {
        public String BasePath { get; private set; }
        public String PackageName { get; private set; }
        public BigGroup Main { get; private set; }
        //public BigGroup BundleTarget { get; private set; }

        public void Load(String BigsPath, String package, bool includeMods = false) {
            BasePath = Path.Combine(BigsPath, package);
            PackageName = package;

            Main = new BigGroup();
            Main.Load(BasePath, includeMods);

            /*BundleTarget = new BigGroup();
            BundleTarget.Load(Path.Combine(_path, "BundleTarget"));*/
        }
    }

    public class BigGroup {
        String _path;
        public List<Big> Bigs { get; set; } = new List<Big>();
        
        public List<BigFileEntryBase> Find(UInt32 fileID, bool normalize = false) {
            List<BigFileEntryBase> result = new List<BigFileEntryBase>();
            foreach (Big big in Bigs) {
                var files = big.FindAll(fileID, normalize);
                foreach (var r in files) {
                    result.Add(r);
                }
            }
            return result;
        }
        public BigFileEntryBase FindFirst(UInt32 fileID) {
            foreach(Big big in Bigs) {
                var file = big.Find(fileID);
                if(file != null) return file;
            }
            return null;
        }
        public void Load(String directory, bool includeMods = false) {
            _path = directory;
            LoadBigs(directory, includeMods);
            //LoadBigs(Path.Combine(directory, "Patches"));
        }
        private void LoadBigs(String directory, bool includeMods) {
            if(!Directory.Exists(directory)) return;

            string[] bigs;

            if(includeMods) bigs = Directory.GetFiles(directory, "*.big", SearchOption.AllDirectories);
            else bigs = Directory.GetFiles(directory, "*_000?.big", SearchOption.AllDirectories);

            foreach(var file in bigs) {
                Big big = new Big();
                big.Load(file);
                Bigs.Add(big);
            }
        }
    }
}
