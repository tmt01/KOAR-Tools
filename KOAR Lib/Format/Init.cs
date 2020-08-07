using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace KOAR_Lib.Format
{
    public class Init:FormatBase {
        protected List<UInt32> _fileIDs;
        protected List<UInt32> _hashes;

        public bool Add(UInt32 fileID, UInt32 hash = 0) {
            if(_fileIDs.Contains(fileID)) return false;

            _fileIDs.Add(fileID);
            if(_hashes != null) {
                _hashes.Add(hash);
            }
            return true;
        }

        public override void Load(Stream stream) {
            KOARBinaryReader br = new KOARBinaryReader(stream);

            br.ReadUInt(); // Always 1
            Int32 count = br.ReadInt();

            _fileIDs = new List<uint>();

            // First list - fileIDs
            for(int i = 0; i < count; i++) {
                _fileIDs.Add(br.ReadUInt());
            }

            // Second list - hashes
            if(br.IsEOF()) {
                _hashes = null;
            } else {
                _hashes = new List<uint>();

                for(int i = 0; i < count; i++) {
                    _hashes.Add(br.ReadUInt());
                }
            }

            br.Close();
        }

        public override void Save(Stream stream) {
            KOARBinaryWriter bw = new KOARBinaryWriter(stream);

            bw.WriteUInt(1);
            bw.WriteInt(_fileIDs.Count);

            for(int i = 0; i < _fileIDs.Count; i++) {
                bw.WriteUInt(_fileIDs[i]);
            }

            if(_hashes != null) {
                for(int i = 0; i < _fileIDs.Count; i++) {
                    bw.WriteUInt(_hashes[i]);
                }
            }

            bw.Close();
        }
    }
}
