using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KOAR_Lib;
using KOAR_Lib.Format;

namespace KOAR_Big_Builder.Project 
{
    class BigProject {
        public const String SYMBOLNAME_NONE = "";
        public List<ProjectFile> Files = new List<ProjectFile>();
        public String Filepath { get; set; } = "";
        public String DirPath { get; set; } = "";
        public String TargetPackage { get; set; } = "001";

        public BigProject(){}

        public void AddFile(String filename, UInt32 fileID = 0, String symbolname = BigProject.SYMBOLNAME_NONE, UInt32 type = 0, UInt32 flags = 0x94) {
            string path = Helper.GetRelativePath(DirPath, filename);
            Files.Add(new ProjectFile(fileID, path, flags, type, symbolname));
        }
        public void RemoveFile(ProjectFile file) {
            Files.Remove(file);
        }
        public void Save(String filename) {
            Filepath = filename;
            DirPath = Path.GetDirectoryName(filename);

            List<String> lines = new List<string>();

            lines.Add(String.Format("{0}", TargetPackage));

            foreach(var file in Files) {
                lines.Add(String.Format("{0},{1},{2},{3},{4}", file.FileID.ToString("X8"), Helper.GetRelativePath(DirPath, file.Filename), file.SymbolName.GetString(), file.Type.ToString("X"), file.Flags.ToString("X")));
            }

            File.WriteAllLines(filename, lines);
        }
        public void Load(String filename) {
            Filepath = filename;
            DirPath = Path.GetDirectoryName(filename);

            Files = new List<ProjectFile>();

            String[] lines = File.ReadAllLines(filename);

            TargetPackage = lines[0];

            for(int i = 1; i < lines.Length; i++) {
                string[] args = lines[i].Split(',');
                UInt32 fileID = Convert.ToUInt32(args[0], 16);
                UInt32 type = Convert.ToUInt32(args[3], 16);
                UInt32 flags = 0x94;
                if(args.Length > 4) {
                    flags = Convert.ToUInt32(args[4], 16);
                }
                AddFile(args[1], fileID, args[2], type, flags);
            }
        }
        public void Build(Package package) {
            HashSet<UInt32> types = new HashSet<uint>();
            Dictionary<UInt32, List<ProjectFile>> files = new Dictionary<uint, List<ProjectFile>>();

            Big big = new Big();
            Big bundle_patch = new Big();

            foreach(var file in Files) {
                if(types.Add(file.Type)) {
                    if(!files.ContainsKey(file.Type)) files[file.Type] = new List<ProjectFile>();
                }

                files[file.Type].Add(file);

                String full_path = Path.GetFullPath(Path.Combine(DirPath, file.Filename));

                if(file.Type == 0x100) { // bundle
                    bundle_patch.AddFile(file.FileID, full_path, file.Flags);
                } else {
                    big.AddFile(file.FileID, full_path, file.Flags);
                }
            }

            foreach(var t in types) {
                var init_id = Utils.SH(String.Format(".\\{0}\\{1}_init.bin", package.PackageName, Utils.BundleTypeName(t)));
                var bundle_id = Utils.SH(String.Format(".\\{0}\\bundles\\{1}.bundle", package.PackageName, Utils.BundleMgrName(t)));

                var init_file = package.Main.FindFirst(init_id);
                var bundle_file = package.Main.FindFirst(bundle_id);

                if(init_file != null) {
                    Init init = new Init();
                    init.Load(init_file.GetStream());
                    bool changed = false;

                    foreach(var file in files[t]) {
                        if (init.Add(file.FileID, file.SymbolName.GetHash())) changed = true;
                    }

                    if(changed) {
                        String init_filename = String.Format("tmp\\{0}_init.bin", Utils.BundleTypeName(t));
                        init.Save(new FileStream(init_filename, FileMode.Create));
                        big.AddFile(init_id, init_filename, 0x14);
                    }
                }

                if(bundle_file != null) {
                    Bundle bundle = new Bundle();
                    bundle.Load(bundle_file.GetStream());

                    bool changed = false;

                    foreach(var file in files[t]) {
                        if(bundle.Add(file.FileID, (byte)file.Type, false)) changed = true;
                    }

                    if(changed) {
                        String bundle_filename = String.Format("tmp\\{0}.bundle", Utils.BundleMgrName(t));
                        bundle.Save(new FileStream(bundle_filename, FileMode.Create));
                        bundle_patch.AddFile(bundle_id, bundle_filename, 0x14);
                    }
                }
            }

            Directory.CreateDirectory(Path.Combine(package.BasePath, "Patches"));

            big.Build(Path.Combine(package.BasePath, "Patches", "generated_patch.big"));
            bundle_patch.Build(Path.Combine(package.BasePath, "BundleTarget", "generated_patch.big"));
        }
    }

    class ProjectFile {
        public ProjectFile(UInt32 fileID, String filename, UInt32 flags, UInt32 type, String symbolname) {
            FileID = fileID;
            Filename = filename;
            Flags = flags;
            Type = type;
            SymbolName = new BigHashString(symbolname);
        }
        public UInt32 FileID { get; set; }
        public String Filename { get; set; }
        public UInt32 Flags{ get; set; }
        public UInt32 Type { get; set; }
        public BigHashString SymbolName;
    }

    class BigHashString {
        private UInt32 _hash;
        private String _string;
        public BigHashString(String symbolname) {
            if(UInt32.TryParse(symbolname, out _hash)) {
                _string = null;
            } else {
                _string = symbolname;
                _hash = Utils.SH(symbolname);
            }
        }
        public void SetString(String str) {
            _string = str;
            _hash = Utils.SH(str);
        }
        public void SetHash(UInt32 hash) {
            _string = null;
            _hash = hash;
        }
        public String GetString() {
            return _string != null ? _string : "0x" + _hash.ToString("X");
        }
        public UInt32 GetHash() {
            return _hash;
        }
    }
}
