using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cds.IO
{
    class FileField
    {
        public static IEnumerable<FileField> Of(Type sectionType) =>
            from property in sectionType.GetProperties()
            let attribute = property.GetCustomAttribute<FieldAttribute>()
            where attribute != null
            orderby attribute.Order
            select new FileField(property, attribute);

        FileField(PropertyInfo property, FieldAttribute attribute)
        {
            Property = property;
            Attribute = attribute;
        }

        PropertyInfo Property { get; }
        FieldAttribute Attribute { get; }

        public string Name => Attribute.Name;
        public Type Type => Property.PropertyType;

        public object this[object section]
        {
            get => Property.GetValue(section);
            set => Property.SetValue(section, value);
        }

        public object Format(object section) =>
            string.Format("{0:" + Attribute.Format + "}", this[section]);        
    }
}
