using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cds.IO
{
    public abstract class CdsFile<T> where T : CdsFile<T>, new()
    {
        public static T Load(string path, bool binary = false) =>
            Load(File.OpenRead(path), binary);

        public static T Load(Stream stream, bool binary = false) => binary 
            ? Load(new BinaryReader(stream)) 
            : Load(new StreamReader(stream));

        public static T Load(BinaryReader reader)
        {
            var file = new T();
            file.Read(reader);
            return file;
        }

        public static T Load(TextReader reader)
        {
            var file = new T();
            file.Read(reader);
            return file;
        }

        public void Save(string path, bool binary = false) =>
            Save(File.OpenWrite(path), binary);

        public void Save(Stream stream, bool binary = false)
        {
            if (binary)
                Save(new BinaryWriter(stream));
            else
                Save(new StreamWriter(stream));
        }

        public void Save(BinaryWriter writer) => 
            this.Write(writer);

        public void Save(TextWriter writer) => 
            this.Write(writer);
    }
}
