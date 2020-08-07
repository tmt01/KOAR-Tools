using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace KOAR_Lib{
    public abstract class KOARBinaryIO {
        protected bool _isLittleEndian = true;
        protected Encoding _enc = Encoding.UTF8;
        protected Stream _stream;
        private long _saved_pos = 0;

        ~KOARBinaryIO() {
            Close();
        }

        public abstract void Close();

        public void SetLittleEndian(bool value) {
            _isLittleEndian = value;
        }

        public void SetEncoding(Encoding encoding) {
            _enc = encoding;
        }

        public long GetOffset() {
            return _stream.Position;
        }

        public void SetOffset(long offset) {
            _stream.Position = offset;
        }

        public void SavePosition() {
            _saved_pos = GetOffset();
        }

        public void LoadPosition() {
            SetOffset(_saved_pos);
        }
    }

    public class KOARBinaryReader:KOARBinaryIO {
        private BinaryReader _br;

        public KOARBinaryReader(String filename) {
            _br = new BinaryReader(File.Open(filename, FileMode.Open));
            this._stream = _br.BaseStream;
        }

        public KOARBinaryReader(Stream stream) {
            _br = new BinaryReader(stream);
            this._stream = _br.BaseStream;
        }

        public override void Close() {
            _br.Close();
        }

        public byte[] Read(int length = 1) {
            return _br.ReadBytes(length);
        }

        public UInt32 ReadUInt(int size = 4) {
            byte[] bytes = _br.ReadBytes(size);

            // Check if current and target endian are the same
            if (BitConverter.IsLittleEndian != this._isLittleEndian){
                Array.Reverse(bytes);
            }

            switch(size) {
                case 1: return bytes[0];
                case 2: return BitConverter.ToUInt16(bytes, 0);
                case 4:
                default: return BitConverter.ToUInt32(bytes, 0);
            }
        }

        public UInt64 ReadUInt64() {
            byte[] bytes = _br.ReadBytes(8);

            // Check if current and target endian are the same
            if(BitConverter.IsLittleEndian != this._isLittleEndian) {
                Array.Reverse(bytes);
            }

            return BitConverter.ToUInt64(bytes, 0);
        }

        public Int32 ReadInt(int size = 4) {
            return (Int32)ReadUInt(size);
        }

        public String ReadString(int length = -1) {
            if (length == -1) length = ReadInt();

            if (length > 0) {
                return _enc.GetString(Read(length));
            } else {
                return "";
            }
        }

        public byte[] ReadAll() {
            using(var ms = new MemoryStream()) {
                _stream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public bool IsEOF() {
            return _stream.Length == _stream.Position;
        }
    }

    public class KOARBinaryWriter:KOARBinaryIO{
        private BinaryWriter _bw;

        public KOARBinaryWriter(String filename) {
            _bw = new BinaryWriter(File.Open(filename, FileMode.Create));
            this._stream = _bw.BaseStream;
        }
        
        public KOARBinaryWriter(Stream stream) {
            _bw = new BinaryWriter(stream);
            this._stream = _bw.BaseStream;
        }

        public override void Close() {
            _bw.Close();
        }

        public void Write(byte[] data) {
            _bw.Write(data);
        }

        public void WriteByte(byte data) {
            _bw.Write(data);
        }

        public void WriteUInt(UInt32 value, int size = 4) {
            byte[] bytes = BitConverter.GetBytes(value);

            // Cut bytes if needed
            if (size < 4) {
                // Convert to little endian
                if(!BitConverter.IsLittleEndian) Array.Reverse(bytes);

                byte[] temp = new byte[size];
                Array.Copy(bytes, temp, size);
                bytes = temp;

                // Convert back to big endian if needed
                if(!BitConverter.IsLittleEndian) Array.Reverse(bytes);
            }

            // Check if current and target endian are the same
            if(BitConverter.IsLittleEndian != this._isLittleEndian) {
                Array.Reverse(bytes);
            }

            Write(bytes);
        }

        public void WriteUInt64(UInt64 value) {
            byte[] bytes = BitConverter.GetBytes(value);

            if(BitConverter.IsLittleEndian != this._isLittleEndian) {
                Array.Reverse(bytes);
            }

            Write(bytes);
        }

        public void WriteInt(Int32 value, int size = 4) {
            WriteUInt((UInt32)value, size);
        }

        public void WriteString(String str, bool append_null = false) {
            int length = _enc.GetByteCount(str);
            byte[] bytes = _enc.GetBytes(str);

            if(!append_null) {
                WriteInt(length);
                Write(bytes);
            } else {
                WriteInt(length + 1);
                Write(bytes);
                WriteByte(0);
            }
        }
    }
}
