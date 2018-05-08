using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cds.IO
{
    static class TextFileReader
    {
        public static void Read<T>(this CdsFile<T> file, TextReader reader) where T : CdsFile<T>, new()
        {
            using (reader)
                foreach (var section in FileSection.Of(file))
                    section.Object = reader.Read(section);
        }

        static object Read(this TextReader reader, FileSection section) =>
            section.IsTable 
            ? reader.ReadTable(section) 
            : reader.ReadProperties(section);

        static object ReadProperties(this TextReader reader, FileSection section)
        {
            var properties = section.CreateObject();
            if (!reader.TryReadHeader(section))
                throw new FormatException($"Section {section.Text} not found or out of order.");

            var fields = section.Fields
                .ToDictionary(f => f.Name);

            while (reader.TryReadProperty(out var name, out var value))
                fields[name][properties] = Convert.ChangeType(value, fields[name].Type);

            return properties;
        }

        static bool TryReadProperty(this TextReader reader, out string name, out string value)
        {
            name = value = null;
            var line = reader.ReadLine()?.Trim();
            if (line == null || !line.Contains(':'))
                return false;

            var split = line.IndexOf(':');
            name = line.Substring(0, split).Trim();
            value = line.Substring(split + 1).Trim();
            return true;
        }

        static object ReadTable(this TextReader reader, FileSection section)
        {
            var table = section.CreateTable();
            if (!reader.TryReadHeader(section))
                throw new FormatException($"Section {section.Text} not found or out of order.");
            if (!reader.TryReadColumns(section))
                throw new FormatException($"Section {section.Text} is missing some columns.");
            while (reader.TryReadRow(section, out var row))
                table.Add(row);

            return table;
        }

        static bool TryReadHeader(this TextReader reader, FileSection section)
        {
            var line = reader.ReadLine()?.Trim();
            return line != null && 
                line.StartsWith("#") && 
                line.Contains(section.Text);
        }

        static bool TryReadColumns(this TextReader reader, FileSection section)
        {
            var line = reader.ReadLine()?.Trim();
            return line != null 
                && line.StartsWith("#") && 
                section.Fields.All(f => line.Contains(f.Name));
        }

        static bool TryReadRow(this TextReader reader, FileSection section, out object row)
        {
            row = section.CreateObject();
            var line = reader.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(line))
                return false;

            var values = line.Split('|')
                .Select((v, i) => new { Value = v.Trim(), Index = i })
                .ToDictionary(x => x.Index, x => x.Value);
            
            foreach (var x in section.Fields.Select((f, i) => new { Field = f, Index = i }))
                x.Field[row] = Convert.ChangeType(values[x.Index], x.Field.Type);

            return true;
        }
    }
}
