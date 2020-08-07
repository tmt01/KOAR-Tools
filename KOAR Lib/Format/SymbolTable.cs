using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KOAR_Lib.Format
{
    public class SymbolTable:FormatBase {
        private List<UInt32> _fileIDs = new List<uint>();
        private List<String> _symbol_names = new List<string>();
        private List<UInt32> _name_hashes = new List<uint>(); // currently not used

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

        public override void Load(Stream stream) {
            KOARBinaryReader br = new KOARBinaryReader(stream);

            _fileIDs = new List<uint>();
            _symbol_names = new List<string>();
            _name_hashes = new List<uint>();

            var count = br.ReadInt();

            Int32 char_array_offset = 8 + count * 12;

            for(int i = 0; i < count; i++) {
                _fileIDs.Add(br.ReadUInt());

                Int32 str_start = br.ReadInt();
                Int32 str_end = br.ReadInt();

                br.SavePosition();
                br.SetOffset(char_array_offset + str_start);

                string s = br.ReadString(str_end - str_start);

                _symbol_names.Add(s);
                _name_hashes.Add(Utils.SH(s));

                br.LoadPosition();
            }

            br.Close();
        }

        public override void Save(Stream stream) {
            throw new NotImplementedException();
        }
    }
}
