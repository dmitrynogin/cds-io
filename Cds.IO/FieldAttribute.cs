using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cds.IO
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldAttribute : Attribute
    {
        public FieldAttribute([CallerMemberName] string name=null, string format="", [CallerLineNumber]int order = 0)
        {
            Name = name;
            Format = format;
            Order = order;
        }

        public string Name { get; }
        public string Format { get; }
        public int Order { get; }
    }
}
