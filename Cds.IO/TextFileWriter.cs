using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cds.IO
{
    static class TextFileWriter
    {
        public static void Write<T>(this CdsFile<T> file, TextWriter writer) where T : CdsFile<T>, new()
        {

        }
    }
}
