using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace KOAR_Lib.Format
{
    public class BXml:FormatBase
    {
        public XDocument Xml { get; set; }
        bool _unmodified;

        public BXml(bool unmodified = false) {
            _unmodified = unmodified;
        }

        public override void Load(Stream stream) {
            KOARBinaryReader br = new KOARBinaryReader(stream);
            Load(br);
            br.Close();
        }

        private void Load(KOARBinaryReader br) {
            List<BxmlRecord> records = new List<BxmlRecord>();

            var count = br.ReadInt();
            for(int i = 0; i < count; i++) {
                BxmlRecord record = new BxmlRecord();
                record.Parent = br.ReadInt();
                record.FirstChild = br.ReadInt();
                record.Next = br.ReadInt();
                record.PropertiesCount = br.ReadInt();
                record.PropertiesStartIndex = br.ReadInt();
                records.Add(record);
            }

            List<UInt32> properties = new List<UInt32>();

            count = br.ReadInt();
            for(int i = 0; i < count; i++) {
                properties.Add(br.ReadUInt());
            }

            List<UInt32> tags = new List<UInt32>();

            count = br.ReadInt();
            for(int i = 0; i < count; i++) {
                tags.Add(br.ReadUInt());
            }

            List<String> strings = new List<string>();

            count = br.ReadInt();
            for(int i = 0; i < count; i++) {
                String s = br.ReadString();
                //s = s.Replace("\x0d\x0a", "\\n");
                strings.Add(s);
            }

            List<UInt32> loc_keys = new List<uint>();

            count = br.ReadInt();
            for(int i = 0; i < count; i++) {
                loc_keys.Add(br.ReadUInt());
            }

            // Rebuilding XML

            Xml = new XDocument();

            List<XElement> XmlRecords = new List<XElement>();

            for(int i = 0; i < records.Count; i++) {
                XElement elem = new XElement("ELEMENT");
                XmlRecords.Add(elem);

                if(records[i].Parent != -1) XmlRecords[records[i].Parent].Add(elem);

                for(int j = records[i].PropertiesStartIndex; j < records[i].PropertiesStartIndex + records[i].PropertiesCount; j++) {
                    var tag_index = (int)properties[j] & 0x00000fff;
                    var data_index = (int)(properties[j] & 0x00fff000) >> 12;
                    var data_type = (properties[j] & 0xff000000) >> 24;

                    var tag_name = GetTagName(tags[tag_index]);
                    String data = "";

                    if(data_type == 0x60) {
                        // string
                        data = strings[data_index];
                    }else if(data_type == 0x80) {
                        // loc_key
                        data = String.Format("loc_key:{0}", loc_keys[data_index].ToString("X"));
                    }

                    if(j == records[i].PropertiesStartIndex) {
                        elem.Name = tag_name;
                        if(_unmodified || records[i].FirstChild == -1) {
                            elem.Value = data;
                        }
                    } else {
                        elem.Add(new XAttribute(tag_name, data));
                    }
                }
            }

            Xml.Add(XmlRecords[0]);
        }

        public override void Save(Stream stream) {
            var XmlRecords = Xml.Descendants().ToList();
            List<BxmlRecord> records = new List<BxmlRecord>();

            for(int i = 0; i < XmlRecords.Count; i++) {
                var record = new BxmlRecord();
                record.Parent = -1;
                record.FirstChild = -1;
                record.Next = -1;
                record.PropertiesCount = 1;
                record.PropertiesStartIndex = 0;
                records.Add(record);
            }

            List<UInt32> properties = new List<UInt32>();
            List<UInt32> tags = new List<UInt32>();
            List<String> strings = new List<string>();
            List<UInt32> loc_keys = new List<uint>();

            for(int i = 0; i < XmlRecords.Count; i++) {
                var children = XmlRecords[i].Elements().ToList();

                if(children.Count > 0){ 
                    records[i].FirstChild = XmlRecords.IndexOf(children[0]);

                    for(int j = 0; j < children.Count; j++) {
                        int index = XmlRecords.IndexOf(children[j]);
                        records[index].Parent = i;

                        if(j > 0) records[XmlRecords.IndexOf(children[j - 1])].Next = index;
                    }
                }

                if(i > 0) {
                    records[i].PropertiesStartIndex = records[i - 1].PropertiesStartIndex + records[i - 1].PropertiesCount;
                }

                // only save inner text for tags without children
                if(records[i].FirstChild == -1) {
                    properties.Add(ConvertProperty(XmlRecords[i].Name.LocalName, XmlRecords[i].Value, tags, strings, loc_keys));
                } else {
                    properties.Add(ConvertProperty(XmlRecords[i].Name.LocalName, null, tags, strings, loc_keys));
                }

                var attributes = XmlRecords[i].Attributes().ToList();

                if(attributes.Count > 0) records[i].PropertiesCount = attributes.Count + 1;

                foreach(var attribute in attributes) {
                    properties.Add(ConvertProperty(attribute.Name.LocalName, attribute.Value, tags, strings, loc_keys));
                }
            }

            KOARBinaryWriter bw = new KOARBinaryWriter(stream);

            bw.WriteInt(records.Count);
            foreach(var record in records) {
                bw.WriteInt(record.Parent);
                bw.WriteInt(record.FirstChild);
                bw.WriteInt(record.Next);
                bw.WriteInt(record.PropertiesCount);
                bw.WriteInt(record.PropertiesStartIndex);
            }

            bw.WriteInt(properties.Count);
            foreach(var v in properties) {
                bw.WriteUInt(v);
            }

            bw.WriteInt(tags.Count);
            foreach(var v in tags) {
                bw.WriteUInt(v);
            }

            bw.WriteInt(strings.Count);
            foreach(var v in strings) {
                bw.WriteString(v);
            }

            bw.WriteInt(loc_keys.Count);
            foreach(var v in loc_keys) {
                bw.WriteUInt(v);
            }

            bw.Close();
        }

        private UInt32 ConvertProperty(String tag, String value, List<UInt32> tags, List<String> strings, List<UInt32> loc_keys) {
            UInt32 hash;
            if(tag.StartsWith("unk_")) {
                hash = Convert.ToUInt32(tag.Substring(4), 16);
            } else {
                hash = Utils.SH(tag);
            }

            UInt32 flag;
            UInt32 data_index;
            if(value == null || value == ""){
                flag = 0;
                data_index = 0;
            }else if(value.StartsWith("loc_key:")) {
                flag = 0x80;
                UInt32 loc_key = Convert.ToUInt32(value.Substring(8), 16);
                data_index = GetOrAddIndex(loc_keys, loc_key);
            } else {
                flag = 0x60;
                data_index = GetOrAddIndex(strings, value);
            }

            UInt32 hash_index = GetOrAddIndex(tags, hash);

            return hash_index | (data_index << 12) | (flag) << 24;
        }

        private UInt32 GetOrAddIndex<T>(List<T> lst, T value) {
            if(lst.Contains(value)) {
                return (UInt32)lst.IndexOf(value);
            } else {
                lst.Add(value);
                return (UInt32)lst.Count - 1;
            }
        }

        private String GetTagName(UInt32 hash) {
            String s;
            if((s = Utils.RestoreHashedString(hash)) != null) return s;
            return "unk_" + hash.ToString("X");
        }
    }

    class BxmlRecord
    {
        public int Parent{ get; set; }
        public int FirstChild{ get; set; }
        public int Next{ get; set; }
        public int PropertiesCount{ get; set; }
        public int PropertiesStartIndex{ get; set; }
        public BxmlRecord() {
        }
    }
}
