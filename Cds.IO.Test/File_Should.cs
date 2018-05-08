using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cds.IO.Test
{
    [TestClass]
    public class File_Should
    {
        [TestMethod]
        public void Persist()
        {
            var file = TestFile.Load("Test.txt");
            file.Save("Copy.txt");

            var copy = TestFile.Load("Copy.txt");

            Assert.AreEqual(file.Header.Type, copy.Header.Type);
            Assert.AreEqual(file.Header.Version, copy.Header.Version);

            Assert.AreEqual(file.Data[0].Depth, copy.Data[0].Depth);
            Assert.AreEqual(file.Data[0].Tip, copy.Data[0].Tip);
            Assert.AreEqual(file.Data[1].Depth, copy.Data[1].Depth);
            Assert.AreEqual(file.Data[1].Tip, copy.Data[1].Tip);
        }
    }

    public class TestFile : CdsFile<TestFile>
    {
        [Section] public HeaderSection Header { get; set; }
        [Section] public IList<DataSection> Data { get; set; }
    }

    public class HeaderSection
    {
        [Field] public string Type { get; set; }
        [Field] public string Version { get; set; }
    }

    public class DataSection
    {
        [Field] public double Depth { get; set; }
        [Field] public double Tip { get; set; }
    }
}
