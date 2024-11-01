using Emzedder.Datafile;
using System.Threading.Tasks;

namespace Emzedder.Tests.Datafile.Helpers
{
    public class BaseTest
    {
        protected readonly string _invalidFilePath = @"C:\Users\runra\source\repos\Emzedder\Emzedder\TestRawDatafiles\invalid.raw";
        protected readonly string _validFilePath = @"C:\Users\runra\source\repos\Emzedder\Emzedder\TestRawDatafiles\1.raw";
        protected readonly string _invalidRawFile = @"C:\Users\runra\source\repos\Emzedder\Emzedder\TestRawDatafiles\NotAValidRawFile.raw";
        protected readonly string _msSpectrumDataCsv = @"C:\Users\runra\source\repos\Emzedder\Emzedder.Tests\Datafile\Data\MSTestData.csv";
        protected readonly string _msMsSpectrumDataCsv = @"C:\Users\runra\source\repos\Emzedder\Emzedder.Tests\Datafile\Data\CentroidTestData.csv";
        protected readonly int _profileScanNumber = 30749;
        protected readonly int _centroidScanNumber = 30750;

        protected MSDatapoint[] ExpectedMsSpectrumDatapoints { get; private set; }
        protected MSDatapoint[] ExpectedMsMsSpectrumDatapoints { get; private set; }

        public BaseTest()
        {
            BuildTestMsSpectrum();
            BuildTestMsMsSpectrum();

        }
        private void BuildTestMsSpectrum()
        {
            ExpectedMsSpectrumDatapoints = ReadDataPointsFromCSV(_msSpectrumDataCsv);
        }
        private void BuildTestMsMsSpectrum()
        {
            ExpectedMsMsSpectrumDatapoints = ReadDataPointsFromCSV(_msMsSpectrumDataCsv);
        }
        private MSDatapoint[] ReadDataPointsFromCSV(string path)
        {
            var datapoints = new List<MSDatapoint>();

            foreach (var line in File.ReadLines(path))
            {
                if (string.IsNullOrWhiteSpace(line) || !line.Contains(","))
                    continue;

                var parts = line.Split(',');
                if (parts.Length == 2 &&
                    double.TryParse(parts[0], out double mz) &&
                    double.TryParse(parts[1], out double intensity))
                {
                    datapoints.Add(new MSDatapoint { Mz = Math.Round(mz, 4), Intensity = Math.Round(intensity, 4) });
                }
            }
            return [.. datapoints];
        }

    }
}
