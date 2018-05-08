using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cds.IO
{
    static class BinaryFileReader
    {
        public static void Read<T>(this CdsFile<T> file, BinaryReader reader) where T : CdsFile<T>, new()
        {
        }
    }
}
