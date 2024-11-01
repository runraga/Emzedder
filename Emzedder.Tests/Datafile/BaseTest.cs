using Emzedder.Datafile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emzedder.Tests.Datafile
{
    public class BaseTest
    {
        protected readonly string _invalidFilePath = @"C:\Users\runra\source\repos\Emzedder\Emzedder\TestRawDatafiles\invalid.raw";
        protected readonly string _validFilePath = @"C:\Users\runra\source\repos\Emzedder\Emzedder\TestRawDatafiles\1.raw";
        protected readonly string _invalidRawFile = @"C:\Users\runra\source\repos\Emzedder\Emzedder\TestRawDatafiles\NotAValidRawFile.raw";
        protected readonly string _msSpectrumDataCsv = @"C:\Users\runra\source\repos\Emzedder\Emzedder.Tests\TestData\MSTestData.csv";
        protected readonly int _scanNumber = 30749;
        protected MSDatapoint[] ExpectedMsSpectrumDatapoints { get; private set; }
        public BaseTest()
        {
            BuildTestMsSpectrum();
        }
        private void BuildTestMsSpectrum()
        {
            var datapoints = new List<MSDatapoint>();

            foreach (var line in File.ReadLines(_msSpectrumDataCsv))
            {
                if (string.IsNullOrWhiteSpace(line) || !line.Contains(","))
                    continue;

                var parts = line.Split(',');
                if (parts.Length == 2 &&
                    double.TryParse(parts[0], out double mz) &&
                    double.TryParse(parts[1], out double intensity))
                {
                    datapoints.Add(new MSDatapoint { Mz = Math.Round(mz,4), Intensity = Math.Round(intensity,4) });
                }
            }

            ExpectedMsSpectrumDatapoints = [.. datapoints];
        }
    }
}
