using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cds.IO
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SectionAttribute : Attribute
    {
        public SectionAttribute([CallerMemberName] string name = null, int version=1, [CallerLineNumber]int order = 0)
        {
            Name = name;
            Version = version;
            Order = order;
        }

        public string Name { get; }
        public int Version { get; }
        public int Order { get; }
    }
}
