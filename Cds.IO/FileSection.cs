using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cds.IO
{
    class FileSection
    {
        public static IEnumerable<FileSection> Of(object file) =>
            from property in file.GetType().GetProperties()
            let attribute = property.GetCustomAttribute<SectionAttribute>()
            where attribute != null
            orderby attribute.Order
            select new FileSection(file, property, attribute);

        FileSection(Object file, PropertyInfo property, SectionAttribute attribute)
        {
            File = file;
            Property = property;
            Attribute = attribute;
        }

        object File { get; }
        PropertyInfo Property { get; }
        SectionAttribute Attribute { get; }

        public string Name => Attribute.Name;

        public int Version => Attribute.Version;

        public object CreateObject() => Activator.CreateInstance(Type);
        public IList CreateTable() => (IList)Activator.CreateInstance(
            typeof(List<>).MakeGenericType(Type));

        public Type Type => IsTable 
            ? Property.PropertyType.GetGenericArguments()[0]
            : Property.PropertyType;

        public bool IsTable => Property.PropertyType.IsGenericType &&
            Property.PropertyType.GetGenericTypeDefinition() == typeof(IList<>);
        
        public object Object
        {
            get => Property.GetValue(File);
            set => Property.SetValue(File, value);
        }

        public string Text => $"{Attribute.Name} (v{Version})";
    }
}
