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
            return x.Mz == y.Mz && x.Intensity == y.Intensity;
        }

        public int GetHashCode(MSDatapoint obj)
        {
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
            var scanStatistics = rawData.GetScanStatsForScanNumber(_profileScanNumber);
            SegmentedScan segmentedScan = rawData.GetSegmentedScanFromScanNumber(_profileScanNumber, scanStatistics);



            //Act
            var thermoSpectrum = new ThermoSpectrum(segmentedScan);

            //Assert
            Assert.Equal(ExpectedMsSpectrumDatapoints, thermoSpectrum.ProfileData, new MSDatapointComparer());

        }
        [Fact]
        public void Constructor_TakesCentroidStream_GivesCorrectCentroidData()
        {
            //Arrange
            var rawData = RawFileReaderAdapter.FileFactory(_validFilePath);
            rawData.SelectInstrument(Device.MS, 1);
            var scanStatistics = rawData.GetScanStatsForScanNumber(_centroidScanNumber);
            var centroidStream = rawData.GetCentroidStream(_centroidScanNumber, false);



            //Act
            var thermoSpectrum = new ThermoSpectrum(centroidStream);

            //Assert
            Assert.Equal(ExpectedMsMsSpectrumDatapoints, thermoSpectrum.CentroidData, new MSDatapointComparer());

        }
    }
}
