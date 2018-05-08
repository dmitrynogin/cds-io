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

            //var file = new TestFile()
            //{
            //    Header = new HeaderSection { Type = "Test", Version = "3.4" },
            //    Data = new []
            //    {
            //        new DataSection { Depth = 1, Tip = 2 },
            //        new DataSection { Depth = 3, Tip = 4 }
            //    }
            //};
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
