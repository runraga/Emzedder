using Emzedder.Datafile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThermoFisher.CommonCore.Data.Business;
using ThermoFisher.CommonCore.Data.Interfaces;
using ThermoFisher.CommonCore.RawFileReader;

namespace Emzedder.Tests.Datafile
{
    public class MSDatapointComparer : IEqualityComparer<MSDatapoint>
    {
        public bool Equals(MSDatapoint? x, MSDatapoint? y)
        {
            if (x == null || y == null) return false;
            //return Math.Round(x.Mz, 4) == Math.Round(y.Mz, 4) && Math.Round(x.Intensity, 4) == Math.Round(y.Intensity, 4);
            return x.Mz == y.Mz && x.Intensity == y.Intensity;

        }

        public int GetHashCode(MSDatapoint obj)
        {
            int mzHash = Math.Round((double)obj.Mz, 4).GetHashCode();
            int intensityHash = Math.Round((double)obj.Intensity, 4).GetHashCode();

            return obj.Mz.GetHashCode() ^ obj.Intensity.GetHashCode();
        }
    }
    public class ThermoSpectrumTests : BaseTest
    {
        [Fact]
        public void Constructor_TakesSegmentedScan_GivesCorrectProfileData()
        {
            //Arrange
            var rawData = RawFileReaderAdapter.FileFactory(_validFilePath);
            rawData.SelectInstrument(Device.MS, 1);
            var scanStatistics = rawData.GetScanStatsForScanNumber(_scanNumber);
            SegmentedScan segmentedScan = rawData.GetSegmentedScanFromScanNumber(_scanNumber, scanStatistics);



            //Act
            var thermoSpectrum = new ThermoSpectrum(segmentedScan);

            //Assert
            Assert.Equal(ExpectedMsSpectrumDatapoints, thermoSpectrum.ProfileData, new MSDatapointComparer());

        }
    }
}
