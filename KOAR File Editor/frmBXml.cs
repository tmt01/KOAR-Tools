using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using KOAR_Lib.Format;
namespace KOAR_File_Editor
{
    public partial class frmBXml:frmMDIChild
    {
        BXml _bxml;
        public frmBXml(String filename, bool convert = true):base(filename) {
            InitializeComponent();

            if(convert) {
                _bxml = new BXml();
                _bxml.Load(new FileStream(filename, FileMode.Open));
                rtbXml.Text = _bxml.Xml.ToString();
            } else {
                rtbXml.Text = File.ReadAllText(filename);
            }
        }

        public override void SaveFile(string filepath = null) {
            if(filepath == null) filepath = Filename;

            _bxml.Xml = XDocument.Parse(rtbXml.Text);
            _bxml.Save(new FileStream(filepath, FileMode.Create));
        }
    }
}
