using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KOAR_Lib.Format
{
    public class Bundle:FormatBase{
        protected List<BundleRecord> _records;

        public bool Add(BundleRecord bundlerecord) {
            foreach (var r in _records) {
                if(r.fileID == bundlerecord.fileID) return false;
            }
            _records.Insert(0, bundlerecord);
            return true;
        }

        public bool Add(UInt32 fileID, byte type, bool bundle) {
            return Add(new BundleRecord(fileID, type, bundle));
        }

        public override void Load(Stream stream) {
            KOARBinaryReader br = new KOARBinaryReader(stream);

            br.ReadInt(); // Always 0
            br.ReadInt(); // Always 0
            Int32 count1 = br.ReadInt();
            Int32 count2 = br.ReadInt();

            _records = new List<BundleRecord>();

            for(int i = 0; i < count1; i++) {
                UInt32 fileID = br.ReadUInt();
                _records.Add(new BundleRecord(fileID));
            }
            for(int i = 0; i < count1; i++) {
                _records[i].type = (byte)br.ReadInt(1);
            }
            for(int i = 0; i < count1; i++) {
                _records[i].unknown = (byte)br.ReadInt(1);
            }
            for(int i = 0; i < count1; i++) {
                _records[i].bundle = br.ReadInt(1) == 1;
            }

            // TODO: add second list 

            br.Close();
        }

        public override void Save(Stream stream) {
            KOARBinaryWriter bw = new KOARBinaryWriter(stream);

            bw.WriteUInt(0);
            bw.WriteUInt(0);
            bw.WriteInt(_records.Count);
            bw.WriteUInt(0); // TODO: write second list size

            for(int i = 0; i < _records.Count; i++) {
                bw.WriteUInt(_records[i].fileID);
            }
            for(int i = 0; i < _records.Count; i++) {
                bw.WriteUInt(_records[i].type, 1);
            }
            for(int i = 0; i < _records.Count; i++) {
                bw.WriteUInt(_records[i].unknown, 1);
            }
            for(int i = 0; i < _records.Count; i++) {
                bw.WriteInt(_records[i].bundle ? 1 : 0, 1);
            }

            // TODO: add second list

            bw.Close();
        }
    }

    public class BundleRecord {
        public UInt32 fileID;
        public byte type;
        public byte unknown = 0;
        public bool bundle; // 1 if file is a bundle containing multiple files, 0 otherwise

        public BundleRecord() : this(0, 0) { }

        public BundleRecord(UInt32 fileID, byte type = 0, bool bundle = false) {
            this.fileID = fileID;
            this.type = type;
            this.bundle = bundle;
        }
    }
}
