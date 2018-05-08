using System;
using System.Collections;
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
            using (writer)
                foreach (var section in FileSection.Of(file))
                    writer.WriteSection(section);
        }

        static void WriteSection(this TextWriter writer, FileSection section)
        {
            writer.WriteHeader(section);
            if (section.IsTable)
                writer.WriteTable(section);
            else
                writer.WriteProperties(section);

            writer.WriteLine();
        }

        static void WriteTable(this TextWriter writer, FileSection section)
        {
            writer.WriteColumns(section);
            foreach (var row in (IList)section.Object)
                writer.WriteRow(section, row);
        }

        static void WriteProperties(this TextWriter writer, FileSection section)
        {
            foreach (var f in section.Fields)
                writer.WriteLine($"{f.Name}: {f.Format(section.Object)}");
        }

        static void WriteHeader(this TextWriter writer, FileSection section) =>
            writer.WriteLine($"# {section.Text}");

        static void WriteColumns(this TextWriter writer, FileSection section) =>        
            writer.WriteLine($"# {string.Join(" | ", from f in section.Fields select f.Name)}");

        static void WriteRow(this TextWriter writer, FileSection section, object row) =>
            writer.WriteLine(string.Join(" | ", from f in section.Fields select f.Format(row)));
    }
}
