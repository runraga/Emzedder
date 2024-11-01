using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emzedder.Tests.Datafile
{
    public class BaseTest
    {
        protected readonly string _invalidFilePath;
        protected readonly string _validFilePath;
        protected readonly string _invalidRawFile;


        protected BaseTest()
        {
            _validFilePath = @"C:\Users\runra\source\repos\Emzedder\Emzedder\TestRawDatafiles\invalid.raw";

            _validFilePath = @"C:\Users\runra\source\repos\Emzedder\Emzedder\TestRawDatafiles\1.raw";

            _invalidRawFile = @"C:\Users\runra\source\repos\Emzedder\Emzedder\TestRawDatafiles\NotAValidRawFile.raw";
        }
    }
}
