using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace KOAR_Lib {
    public class Big {
        public static readonly byte[] SIGNATURE = new byte[] { 0x80, 0xC7, 0xC8, 0xC2 };

        protected UInt32 _version = 0x10;
        protected List<BigFileEntryBase> _fileTable = new List<BigFileEntryBase>();

        public String Filename{ get; set; }

        public String BuildString = "BHG6120 : 38CORP : bhg.builder";
        
        public Big() {}

        public void AddFile(BigFileEntryBase file) {
            _fileTable.Add(file);
        }

        public void AddFile(UInt32 fileID, String filename, UInt32 flags = 0x94) {
            _fileTable.Add(new BigFileEntryExternal(fileID, filename, flags));
        }
        
        public BigFileEntryBase Find(UInt32 fileID) {
            foreach(var file in _fileTable) {
                if(file.FileID == fileID) {
                    return file;
                }
            }
            return null;
        }

        public List<BigFileEntryBase> FindAll (UInt32 fileID, bool normalize = false) {
            var result = new List<BigFileEntryBase>();

            foreach(var file in _fileTable) {
                UInt32 id = file.FileID;
                if(normalize) id = id & 0x0fffffff;

                if(id == fileID) {
                    result.Add(file);
                }
            }
            return result;
        }

        public List<BigFileEntryBase> GetFileTable() {
            return _fileTable;
        }

        public void SaveFileList(String filename) {
            List<String> lines = new List<string>();

            lines.Add(String.Format("{0}", Filename));

            foreach (var file in _fileTable) {
                lines.Add(String.Format("{0},{1},{2}", file.FileID.ToString("X"), file.Filename, file.Flags.ToString("X")));
            }

            File.WriteAllLines(filename, lines);
        }

        public void LoadFileList(String filename) {
            _fileTable = new List<BigFileEntryBase>();

            String[] lines = File.ReadAllLines(filename);

            if (lines[0] != "") {
                Filename = lines[0];
            }

            for(int i = 1; i < lines.Length; i++) {
                string[] args = lines[i].Split(',');
                UInt32 fileID = Convert.ToUInt32(args[0], 16);
                UInt32 flags = 0x94;
                if(args.Length > 2) {
                    flags = Convert.ToUInt32(args[2], 16);
                }
                AddFile(fileID, args[1], flags);
            }
        }

        public void Build(String filename = null) {
            if(filename == null) filename = Filename;

            KOARBinaryWriter bw = new KOARBinaryWriter(filename);

            WriteHeader(bw);
            WriteFileTable(bw);

            bw.Close();
        }

        public bool HasFilename() {
            return Filename != null;
        }

        public void Load(String bigfile) {
            Filename = bigfile;

            KOARBinaryReader br = new KOARBinaryReader(bigfile);

            ReadHeader(br);
            ReadFileTable(br);

            br.Close();

            ReadAdditionalInfo();
        }

        private void ReadHeader(KOARBinaryReader br) {
            byte[] signature = br.Read(4);
            if(!signature.SequenceEqual(Big.SIGNATURE)) {
                throw new IOException("big file signature doesn't match");
            }
            _version = br.ReadUInt();
            br.Read(); // 1
            BuildString = br.ReadString();
            br.ReadUInt(); // 0x20
        }

        private void ReadFileTable(KOARBinaryReader br) {
            _fileTable = new List<BigFileEntryBase>();

            Int32 count = br.ReadInt();

            for(int i = 0; i < count; i++) {
                var entry = new BigFileEntryInternal(this, br.ReadUInt(), br.ReadUInt(), br.ReadUInt(), br.ReadUInt(), br.ReadUInt());
                _fileTable.Add(entry);
            }
        }

        private void ReadAdditionalInfo() {
            // load names
            var lookup_names = this.Find(0xffffffff); // .\lookup_names_as_strings.bin

            if (lookup_names != null) {
                List<String> names = new List<string>();

                //var br = lookup_names.GetBinaryReader();
                var br = new KOARBinaryReader(lookup_names.GetStream());

                int count = br.ReadInt();
                br.ReadInt(); // skip count duplicate
                br.Read(3); // skip ffff00

                for(int i = 0; i < count; i++) {
                    _fileTable[i].Filename = br.ReadString();
                }

                br.Close();
            }
        }

        private void WriteHeader(KOARBinaryWriter bw) {
            bw.Write(Big.SIGNATURE);
            bw.WriteUInt(_version);
            bw.WriteByte(1);
            bw.WriteString(this.BuildString);
            bw.WriteUInt(0x20);
        }

        private void WriteFileTable(KOARBinaryWriter bw) {
            bw.WriteInt(_fileTable.Count);

            UInt32 offset = (UInt32)(bw.GetOffset() + _fileTable.Count * 20);

            foreach (BigFileEntryBase file in _fileTable) {
                byte[] buffer = file.GetBytes();
                Int32 size = buffer.Length;

                bw.WriteInt(size);

                // TODO: add compression
                file.SetRaw(true);
                bw.WriteInt(size);

                bw.WriteUInt(offset);
                bw.WriteUInt(file.FileID);
                bw.WriteUInt(file.Flags);

                bw.SavePosition();
                bw.SetOffset(offset);

                bw.Write(buffer);

                offset = (UInt32)bw.GetOffset();

                bw.LoadPosition();
            }
        }
    }

    public abstract class BigFileEntryBase {
        public BigFileEntryBase(UInt32 fileID, UInt32 flags) {
            FileID = fileID;
            Flags = flags;
        }

        public bool IsRaw(){ return (Flags & 0x0f) == 0x04; }

        public void SetRaw(bool raw) {
            if(raw) {
                Flags = (Flags & 0xf0) | 0x04;
            } else {
                Flags = (Flags & 0xf0) | 0x01;
            }
        }

        public UInt32 FileID{ get; set; }
        public UInt32 Flags{ get; set; }
        public abstract String Filename { get; set; }
        public void SaveToFile(String filename) {
            KOARBinaryWriter bw = new KOARBinaryWriter(filename);
            bw.Write(GetUncompressedData());
            bw.Close();
        }
        public abstract byte[] GetBytes();
        public abstract byte[] GetUncompressedData();
        public abstract Stream GetStream();
    }

    /// <summary>
    /// Part of a big file
    /// </summary>
    public class BigFileEntryInternal:BigFileEntryBase{
        protected Big _parent;
        protected UInt32 _offset;
        protected UInt32 _size;
        protected UInt32 _size_uncompressed;

        private String _filename;

        public BigFileEntryInternal(Big parent, UInt32 size_unc, UInt32 size, UInt32 offset, UInt32 fileID, UInt32 flags) : base(fileID, flags) {
            _parent = parent;
            _offset = offset;
            _size = size;
            _size_uncompressed = size_unc;
        }
        public override String Filename { get { if(_filename != null) return _filename; return _parent.Filename + ":" + _offset.ToString("X"); } set { _filename = value; } }

        public KOARBinaryReader GetBinaryReaderUncompressed() {
            KOARBinaryReader br = new KOARBinaryReader(_parent.Filename);
            br.SetOffset(_offset);
            return br;
        }

        public override byte[] GetBytes() {
            KOARBinaryReader br = GetBinaryReaderUncompressed();
            byte[] result = br.Read((int)_size);
            br.Close();
            return result;
        }

        public override Stream GetStream() {
            return new MemoryStream(GetUncompressedData());
        }

        public override byte[] GetUncompressedData() {
            if(IsRaw()) {
                return GetBytes();
            } else {
                MemoryStream bytes = new MemoryStream();
                var br = GetBinaryReaderUncompressed();

                Int32 bytesRead = 0;

                do {
                    UInt32 block_size = br.ReadUInt(); // 0xCCSSSSSS - SS is size, CC is a compression flag
                    UInt32 block_out_offset = br.ReadUInt();
                    UInt32 block_uncompressed_size = br.ReadUInt();
                    UInt32 block_compressed_size = br.ReadUInt();

                    UInt32 compression_flag = (block_size & 0xff000000) >> 24;
                    block_size &= 0x00ffffff;

                    int data_size = (int)block_size - 16; // 16 - header size

                    byte[] block = br.Read(data_size); 

                    if(compression_flag == 0) {
                        try {
                            MemoryStream ms = new MemoryStream(block);
                            GZipStream ds = new GZipStream(ms, CompressionMode.Decompress);
                            ds.CopyTo(bytes);
                        } catch(InvalidDataException ex) {
                            br.Close();
                            throw;
                        }
                        
                    } else {
                        bytes.Write(block, 0, data_size);
                    }

                    bytesRead += (int)block_size;
                } while(bytesRead < _size);

                br.Close();
                return bytes.ToArray();
            }
        }
    }

    /// <summary>
    /// External file that is supposed to be included into the archive
    /// </summary>
    public class BigFileEntryExternal:BigFileEntryBase {
        protected String _filename;
        public BigFileEntryExternal(UInt32 fileID, String filename, UInt32 flags = 0x94):base(fileID, flags) {
            _filename = filename;
        }

        public override String Filename { get { return _filename; } set { _filename = value; } }

        public override byte[] GetBytes() {
            KOARBinaryReader br = new KOARBinaryReader(_filename);
            byte[] result = br.ReadAll();
            br.Close();
            return result;
        }

        public override Stream GetStream() {
            return new FileStream(_filename, FileMode.Open);
        }

        public override byte[] GetUncompressedData() {
            return GetBytes();
        }

    }
}
