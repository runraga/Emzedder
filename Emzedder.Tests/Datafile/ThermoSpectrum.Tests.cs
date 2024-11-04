using Emzedder.Datafile;
using Emzedder.Tests.Datafile.Helpers;
using ThermoFisher.CommonCore.Data.Business;
using ThermoFisher.CommonCore.RawFileReader;

namespace Emzedder.Tests.Datafile
{

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
        [Fact]
        public void Constructor_TakesSegmentedScan_CalculatedCorrectCentroidData()
        {
            //peaks selected at random from data and centroid calculated manually
            MSDatapoint[] expectedCentroids =
            {
                new MSDatapoint()
                {
                    Mz = Math.Round(525.8762702,4),
                    Intensity =Math.Round(1264315.0,4)
                },
                new MSDatapoint()
                {
                    Mz = Math.Round(596.9870019,4),
                    Intensity =Math.Round(1075331.375,4)
                },
                new MSDatapoint()
                {
                    Mz = Math.Round(772.7318778,4),
                    Intensity = Math.Round(1356147.75,4)
                },
                new MSDatapoint()
                {
                    Mz = Math.Round(1046.767586,4),
                    Intensity = Math.Round(2821312.25,4)
                },
                new MSDatapoint()
                {
                    Mz = Math.Round(1109.825689,4),
                    Intensity = Math.Round(3838149.5,4)
                },
                new MSDatapoint()
                {
                    Mz = Math.Round(1126.104162,4),
                    Intensity = Math.Round(2188434.0,4)
                },
            };

            var rawData = RawFileReaderAdapter.FileFactory(_validFilePath);
            rawData.SelectInstrument(Device.MS, 1);
            var scanStatistics = rawData.GetScanStatsForScanNumber(_profileScanNumber);
            SegmentedScan segmentedScan = rawData.GetSegmentedScanFromScanNumber(_profileScanNumber, scanStatistics);

            //Act
            var thermoSpectrum = new ThermoSpectrum(segmentedScan);

            //Assert
            foreach (var datapoint in expectedCentroids)
            {
                Assert.Contains(datapoint, thermoSpectrum.CentroidData!, new MSDatapointComparer());
            }

        }
        //[Fact]
        //public async Task Constructor_TakesSegmentedScan_PopulatesCentroidDataAsynchronously()
        //{
        //    //Arrange
        //    var testData = ExpectedMsSpectrumDatapoints[19938..20351];



        //var rawData = RawFileReaderAdapter.FileFactory(_validFilePath);
        //rawData.SelectInstrument(Device.MS, 1);
        //var scanStatistics = rawData.GetScanStatsForScanNumber(_profileScanNumber);
        //SegmentedScan segmentedScan = rawData.GetSegmentedScanFromScanNumber(_profileScanNumber, scanStatistics);

        ////Act
        //var thermoSpectrum = new ThermoSpectrum(segmentedScan);

        ////Assert
        //Assert.Equal(ExpectedMsSpectrumDatapoints, thermoSpectrum.ProfileData, new MSDatapointComparer());
    }
}

