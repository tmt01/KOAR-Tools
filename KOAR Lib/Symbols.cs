using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KOAR_Lib
{
    public class Symbols{
        private List<UInt32> _fileIDs = new List<uint>();
        private List<String> _symbol_names = new List<string>();

        public Dictionary<UInt32, String> GetIdToNameDict() {
            var result = new Dictionary<UInt32, String>();

            for(int i = 0; i < _fileIDs.Count; i++) {
                result[_fileIDs[i]] = _symbol_names[i];
            }

            return result;
        }
        public Dictionary<String, UInt32> GetNameToIDDict() {
            var result = new Dictionary<String, UInt32>();

            for(int i = 0; i < _fileIDs.Count; i++) {
                result[_symbol_names[i]] = _fileIDs[i];
            }

            return result;
        }
        public void Load(String name) {
            KOARBinaryReader br = new KOARBinaryReader(Path.Combine("symbols", name));

            _fileIDs = new List<uint>();
            _symbol_names = new List<string>();

            var count = br.ReadInt();

            for(int i = 0; i < count; i++) {
                _fileIDs.Add(br.ReadUInt());
                _symbol_names.Add(br.ReadString());
            }

            br.Close();
        }
    }
}
