using Emzedder.Datafile;
using Emzedder.Tests.Datafile.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThermoFisher.CommonCore.Data.Business;
using ThermoFisher.CommonCore.RawFileReader;

namespace Emzedder.Tests.Datafile
{
    public class IsotopeDetectionServiceTests : BaseTest
    {

        [Fact]
        public void FindPeakNeighbours_GivenListOfDatapoints_ReturnsPeaksWithinMzDistance()
        {
            var point1 = new MSDatapoint()
            {
                Mz = 1.0,
                Intensity = 1234
            };
            var point2 = new MSDatapoint()
            {
                Mz = 2.0,
                Intensity = 1234
            };
            var point3 = new MSDatapoint()
            {
                Mz = 2.11,
                Intensity = 1234
            };
            MSDatapoint[] expected = [point1, point2];

            var neighbours = new IsotopeDetectionService([point1, point2, point3]).FindPeakNeighbours(point1, false);

            Assert.Equal(expected, neighbours);
        }
        [Fact]
        public void FindPeakNeighbours_GivenListOfDatapointsAndCheckDownTrue_ReturnsNeighboursInRangeBelowTargetPeakMz()
        {
            var point1 = new MSDatapoint()
            {
                Mz = 7.0,
                Intensity = 1234
            };
            var point2 = new MSDatapoint()
            {
                Mz = 6.0,
                Intensity = 1234
            };
            var point3 = new MSDatapoint()
            {
                Mz = 5.89,
                Intensity = 1234
            };
            MSDatapoint[] expected = [point1, point2];

            var neighbours = new IsotopeDetectionService([point1, point2, point3]).FindPeakNeighbours(point1, true);

            Assert.Equal(expected, neighbours);
        }
        [Fact]
        public void FindPeakNeighbours_GivenPeakWithNoCloseNeighbours_ReturnsOnlyTargetPeak()
        {
            var point1 = new MSDatapoint()
            {
                Mz = 1.0,
                Intensity = 1234
            };
            var point2 = new MSDatapoint()
            {
                Mz = 7.0,
                Intensity = 1234
            };
            var point3 = new MSDatapoint()
            {
                Mz = 7.11,
                Intensity = 1234
            };
            MSDatapoint[] expected = [point1];

            var neighbours = new IsotopeDetectionService([point1, point2, point3]).FindPeakNeighbours(point1, false);

            Assert.Equal(expected, neighbours);
        }
        [Fact]
        public void FindIsotope_GivenTwoIsotopesOfCharge1_ReturnsCorrectIsotopeGroup()
        {
            var point1 = new MSDatapoint()
            {
                Mz = 1.0,
                Intensity = 12345678
            };
            var point2 = new MSDatapoint()
            {
                Mz = 2.0,
                Intensity = 12345678
            };
            var expected = new IsotopeGroup(1);
            expected.AddIsotope(point1);
            expected.AddIsotope(point2);

            var isotopeService = new IsotopeDetectionService([point1, point2]);
            var isotopeGroup = isotopeService.FindIsotopes(point1, 0, false);

            Assert.Equal(expected.Charge, isotopeGroup.Charge);
            Assert.Contains(point1, isotopeGroup.GetIsotopeGroup());
            Assert.Contains(point2, isotopeGroup.GetIsotopeGroup());


        }
        [Fact]
        public void FindIsotope_GivenMultipleIsotopesOfCharge1_ReturnsCorrectIsotopeGroup()
        {
            var point1 = new MSDatapoint()
            {
                Mz = 1.0,
                Intensity = 12345678
            };
            var point2 = new MSDatapoint()
            {
                Mz = 2.0,
                Intensity = 12345678
            };
            var point3 = new MSDatapoint()
            {
                Mz = 3.0,
                Intensity = 12345678
            };

            var isotopeService = new IsotopeDetectionService([point1, point2, point3]);
            var isotopeGroup = isotopeService.FindIsotopes(point1, 0, false);

            Assert.Equal(1, isotopeGroup.Charge);
            Assert.Contains(point1, isotopeGroup.GetIsotopeGroup());
            Assert.Contains(point2, isotopeGroup.GetIsotopeGroup());
            Assert.Contains(point3, isotopeGroup.GetIsotopeGroup());

        }
        [Fact]
        public void FindIsotope_GivenMultipleIsotopesOfCharge1AtLowerMz_ReturnsCorrectIsotopeGroup()
        {
            var point1 = new MSDatapoint()
            {
                Mz = 5.0,
                Intensity = 12345678
            };
            var point2 = new MSDatapoint()
            {
                Mz = 4.0,
                Intensity = 12345678
            };
            var point3 = new MSDatapoint()
            {
                Mz = 3.0,
                Intensity = 12345678
            };

            var isotopeService = new IsotopeDetectionService([point1, point2, point3]);
            var isotopeGroup = isotopeService.FindIsotopes(point1, 0, true);

            Assert.Equal(1, isotopeGroup.Charge);
            Assert.Contains(point1, isotopeGroup.GetIsotopeGroup());
            Assert.Contains(point2, isotopeGroup.GetIsotopeGroup());
            Assert.Contains(point3, isotopeGroup.GetIsotopeGroup());

        }
        [Fact]
        public void FindIsotope_GivenSpecificCharge1_ReturnsCorrectIsotopeGroup()
        {
            var point1 = new MSDatapoint()
            {
                Mz = 1.0,
                Intensity = 12345678
            };
            var point2 = new MSDatapoint()
            {
                Mz = 1.5,
                Intensity = 12345678
            };
            var point3 = new MSDatapoint()
            {
                Mz = 2,
                Intensity = 12345678
            };
            var point4 = new MSDatapoint()
            {
                Mz = 3,
                Intensity = 12345678
            };

            var isotopeService = new IsotopeDetectionService([point1, point2, point3, point4]);
            var isotopeGroup = isotopeService.FindIsotopes(point1, 1, false);

            Assert.Equal(1, isotopeGroup.Charge);
            Assert.Contains(point1, isotopeGroup.GetIsotopeGroup());
            Assert.Contains(point3, isotopeGroup.GetIsotopeGroup());
            Assert.Contains(point4, isotopeGroup.GetIsotopeGroup());
            Assert.DoesNotContain(point2, isotopeGroup.GetIsotopeGroup());

        }
        [Fact]
        public void FindIsotope_GivenSpecificCharge2_ReturnsCorrectIsotopeGroup()
        {
            var point1 = new MSDatapoint()
            {
                Mz = 1.0,
                Intensity = 12345678
            };
            var point2 = new MSDatapoint()
            {
                Mz = 1.5,
                Intensity = 12345678
            };
            var point3 = new MSDatapoint()
            {
                Mz = 2,
                Intensity = 12345678
            };
            var point4 = new MSDatapoint()
            {
                Mz = 3,
                Intensity = 12345678
            };

            var isotopeService = new IsotopeDetectionService([point1, point2, point3]);
            var isotopeGroup = isotopeService.FindIsotopes(point1, 2, false);

            Assert.Equal(2, isotopeGroup.Charge);
            Assert.Contains(point1, isotopeGroup.GetIsotopeGroup());
            Assert.Contains(point2, isotopeGroup.GetIsotopeGroup());
            Assert.Contains(point3, isotopeGroup.GetIsotopeGroup());
            Assert.DoesNotContain(point4, isotopeGroup.GetIsotopeGroup());


        }
        [Fact]
        public void FindIsotope_GivenStartPeakBelowIntensityThreshold_ReturnsEmptyGroup()
        {
            var point1 = new MSDatapoint()
            {
                Mz = 1.0,
                Intensity = 123
            };
            var point2 = new MSDatapoint()
            {
                Mz = 2,
                Intensity = 12345678
            };
            var point3 = new MSDatapoint()
            {
                Mz = 3,
                Intensity = 12345678
            };


            var isotopeService = new IsotopeDetectionService([point1, point2, point3,]);
            var isotopeGroup = isotopeService.FindIsotopes(point1, 0, false);

            Assert.Equal(0, isotopeGroup.Charge);
            Assert.DoesNotContain(point1, isotopeGroup.GetIsotopeGroup());
            Assert.DoesNotContain(point2, isotopeGroup.GetIsotopeGroup());
            Assert.DoesNotContain(point3, isotopeGroup.GetIsotopeGroup());




        }
        [Fact]
        public void FindIsotope_GivenIsotopePeakBelowIntensityThreshold_ExcludesSubsequentPeaks()
        {
            var point1 = new MSDatapoint()
            {
                Mz = 1.0,
                Intensity = 12345678
            };
            var point2 = new MSDatapoint()
            {
                Mz = 2,
                Intensity = 12345678
            };
            var point3 = new MSDatapoint()
            {
                Mz = 3,
                Intensity = 12345678
            };
            var point4 = new MSDatapoint()
            {
                Mz = 4,
                Intensity = 1234
            };
            var point5 = new MSDatapoint()
            {
                Mz = 5,
                Intensity = 500
            };

            var isotopeService = new IsotopeDetectionService([point1, point2, point3, point4, point5]);
            var isotopeGroup = isotopeService.FindIsotopes(point1, 0, false);

            Assert.Equal(1, isotopeGroup.Charge);
            Assert.Contains(point1, isotopeGroup.GetIsotopeGroup());
            Assert.Contains(point2, isotopeGroup.GetIsotopeGroup());
            Assert.Contains(point3, isotopeGroup.GetIsotopeGroup());
            Assert.Contains(point4, isotopeGroup.GetIsotopeGroup());
            Assert.DoesNotContain(point5, isotopeGroup.GetIsotopeGroup());

        }
        [Fact]
        public void FindIsotopes_GivenOverlappingIsotopePeaks_ReturnsCorrectPeaksAboveCurrent()
        {
            var rawData = RawFileReaderAdapter.FileFactory(_validFilePath);
            rawData.SelectInstrument(Device.MS, 1);
            var scanStatistics = rawData.GetScanStatsForScanNumber(_profileScanNumber);
            var centroidStream = rawData.GetCentroidStream(_profileScanNumber, false);
            var thermoSpectrum = new ThermoSpectrum(centroidStream);
            var centroidPeaks = thermoSpectrum.CentroidData!.Where(p => p.Mz > 1056 && p.Mz < 1057.7)
                                           .OrderBy(p => p.Mz)
                                           .ToArray();
            var currentPeak = centroidPeaks.OrderByDescending(p => p.Intensity).First();
            int expectedChargeState = 6;
            int expectedIsotopePeakCount = 4;

            var isotopeService = new IsotopeDetectionService(centroidPeaks);
            var isotopePeaks = isotopeService.FindIsotopes(currentPeak, 0);

            Assert.Equal(expectedChargeState, isotopePeaks.Charge);
            Assert.Equal(expectedIsotopePeakCount, isotopePeaks.GetIsotopeGroup().Length);
        }
        [Fact]
        public void FindIsotope_GivenPeaksAboveAndBelowCurrent_FindsAllRealtedIsotopes()
        {
            var point1 = new MSDatapoint()
            {
                Mz = 1.0,
                Intensity = 1000000
            };
            var point2 = new MSDatapoint()
            {
                Mz = 2,
                Intensity = 1200000
            };
            var point3 = new MSDatapoint()
            {
                Mz = 3,
                Intensity = 20000000
            };
            var point4 = new MSDatapoint()
            {
                Mz = 4,
                Intensity = 1200000
            };
            var point5 = new MSDatapoint()
            {
                Mz = 5,
                Intensity = 1000000
            };

            var isotopeService = new IsotopeDetectionService([point1, point2, point3, point4, point5]);
            var isotopeGroup = isotopeService.GroupIntoIsotopeEnvelopes();

            Assert.Single(isotopeGroup);
            Assert.Equal(5, isotopeGroup[0].GetIsotopeGroup().Length);
            Assert.Equal(1, isotopeGroup[0].Charge);

        }
        [Fact]
        public void FindIsotope_OverlappingDistributions_FindsAllIsotopeGroups()
        {
            var rawData = RawFileReaderAdapter.FileFactory(_validFilePath);
            rawData.SelectInstrument(Device.MS, 1);
            var scanStatistics = rawData.GetScanStatsForScanNumber(_profileScanNumber);
            var centroidStream = rawData.GetCentroidStream(_profileScanNumber, false);
            var thermoSpectrum = new ThermoSpectrum(centroidStream);
            var centroidPeaks = thermoSpectrum.CentroidData!.Where(p => p.Mz > 1056 && p.Mz < 1060.0)
                                           .OrderBy(p => p.Mz)
                                           .ToArray();
            var currentPeak = centroidPeaks.OrderByDescending(p => p.Intensity).First();
            int expectedChargeState1 = 6;
            int expectedIsotopePeakCount1 = 8;

            int expectedChargeState2 = 5;
            int expectedIsotopePeakCount2 = 12;

            int expectedIsotopeGroups = 2;

            var isotopeService = new IsotopeDetectionService(centroidPeaks);
            var isotopeGroups = isotopeService.GroupIntoIsotopeEnvelopes();

            Assert.Equal(expectedIsotopeGroups, isotopeGroups.Length);

            Assert.Equal(expectedChargeState2, isotopeGroups[0].Charge);
            Assert.Equal(expectedChargeState1, isotopeGroups[1].Charge);

            Assert.Equal(expectedIsotopePeakCount2, isotopeGroups[0].GetIsotopeGroup().Length);
            Assert.Equal(expectedIsotopePeakCount1, isotopeGroups[1].GetIsotopeGroup().Length);

        }



    }
}
