using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KOAR_Lib.Format
{
    public abstract class FormatBase {
        public abstract void Load(Stream stream);
        public abstract void Save(Stream stream);
    }
}
