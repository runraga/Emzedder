using Emzedder.Datafile;
using Emzedder.Tests.Datafile.Helpers;
using ThermoFisher.CommonCore.Data.Business;
using ThermoFisher.CommonCore.RawFileReader;

namespace Emzedder.Tests.Datafile;

public class IsotopeDetectionServiceTests : BaseTest
{

    [Fact]
    public void FindPeakNeighbours_GivenListOfDatapoints_ReturnsPeaksWithinMzDistance()
    {
        MSDatapoint point1 = new()
        {
            Mz = 1.0,
            Intensity = 1234
        };
        MSDatapoint point2 = new()
        {
            Mz = 2.0,
            Intensity = 1234
        };
        MSDatapoint point3 = new()
        {
            Mz = 2.11,
            Intensity = 1234
        };
        MSDatapoint[] expected = [point1, point2];

        MSDatapoint[] neighbours = new IsotopeDetectionService([point1, point2, point3]).FindPeakNeighbours(point1, false);

        Assert.Equal(expected, neighbours);
    }
    [Fact]
    public void FindPeakNeighbours_GivenListOfDatapointsAndCheckDownTrue_ReturnsNeighboursInRangeBelowTargetPeakMz()
    {
        MSDatapoint point1 = new()
        {
            Mz = 7.0,
            Intensity = 1234
        };
        MSDatapoint point2 = new()
        {
            Mz = 6.0,
            Intensity = 1234
        };
        MSDatapoint point3 = new()
        {
            Mz = 5.89,
            Intensity = 1234
        };
        MSDatapoint[] expected = [point1, point2];

        MSDatapoint[] neighbours = new IsotopeDetectionService([point1, point2, point3]).FindPeakNeighbours(point1, true);

        Assert.Equal(expected, neighbours);
    }
    [Fact]
    public void FindPeakNeighbours_GivenPeakWithNoCloseNeighbours_ReturnsOnlyTargetPeak()
    {
        MSDatapoint point1 = new()
        {
            Mz = 1.0,
            Intensity = 1234
        };
        MSDatapoint point2 = new()
        {
            Mz = 7.0,
            Intensity = 1234
        };
        MSDatapoint point3 = new()
        {
            Mz = 7.11,
            Intensity = 1234
        };
        MSDatapoint[] expected = [point1];

        MSDatapoint[] neighbours = new IsotopeDetectionService([point1, point2, point3]).FindPeakNeighbours(point1, false);

        Assert.Equal(expected, neighbours);
    }
    [Fact]
    public void FindIsotope_GivenTwoIsotopesOfCharge1_ReturnsCorrectIsotopeGroup()
    {
        MSDatapoint point1 = new()
        {
            Mz = 1.0,
            Intensity = 12345678
        };
        MSDatapoint point2 = new()
        {
            Mz = 2.0,
            Intensity = 12345678
        };
        IsotopeGroup expected = new(1);
        expected.AddIsotope(point1);
        expected.AddIsotope(point2);

        IsotopeDetectionService isotopeService = new([point1, point2]);
        IsotopeGroup isotopeGroup = isotopeService.FindIsotopes(point1, 0, false);

        Assert.Equal(expected.Charge, isotopeGroup.Charge);
        Assert.Contains(point1, isotopeGroup.GetIsotopeGroup());
        Assert.Contains(point2, isotopeGroup.GetIsotopeGroup());


    }
    [Fact]
    public void FindIsotope_GivenMultipleIsotopesOfCharge1_ReturnsCorrectIsotopeGroup()
    {
        MSDatapoint point1 = new()
        {
            Mz = 1.0,
            Intensity = 12345678
        };
        MSDatapoint point2 = new()
        {
            Mz = 2.0,
            Intensity = 12345678
        };
        MSDatapoint point3 = new()
        {
            Mz = 3.0,
            Intensity = 12345678
        };

        IsotopeDetectionService isotopeService = new([point1, point2, point3]);
        IsotopeGroup isotopeGroup = isotopeService.FindIsotopes(point1, 0, false);

        Assert.Equal(1, isotopeGroup.Charge);
        Assert.Contains(point1, isotopeGroup.GetIsotopeGroup());
        Assert.Contains(point2, isotopeGroup.GetIsotopeGroup());
        Assert.Contains(point3, isotopeGroup.GetIsotopeGroup());

    }
    [Fact]
    public void FindIsotope_GivenMultipleIsotopesOfCharge1AtLowerMz_ReturnsCorrectIsotopeGroup()
    {
        MSDatapoint point1 = new()
        {
            Mz = 5.0,
            Intensity = 12345678
        };
        MSDatapoint point2 = new()
        {
            Mz = 4.0,
            Intensity = 12345678
        };
        MSDatapoint point3 = new()
        {
            Mz = 3.0,
            Intensity = 12345678
        };

        IsotopeDetectionService isotopeService = new([point1, point2, point3]);
        IsotopeGroup isotopeGroup = isotopeService.FindIsotopes(point1, 0, true);

        Assert.Equal(1, isotopeGroup.Charge);
        Assert.Contains(point1, isotopeGroup.GetIsotopeGroup());
        Assert.Contains(point2, isotopeGroup.GetIsotopeGroup());
        Assert.Contains(point3, isotopeGroup.GetIsotopeGroup());

    }
    [Fact]
    public void FindIsotope_GivenSpecificCharge1_ReturnsCorrectIsotopeGroup()
    {
        MSDatapoint point1 = new()
        {
            Mz = 1.0,
            Intensity = 12345678
        };
        MSDatapoint point2 = new()
        {
            Mz = 1.5,
            Intensity = 12345678
        };
        MSDatapoint point3 = new()
        {
            Mz = 2,
            Intensity = 12345678
        };
        MSDatapoint point4 = new()
        {
            Mz = 3,
            Intensity = 12345678
        };

        IsotopeDetectionService isotopeService = new([point1, point2, point3, point4]);
        IsotopeGroup isotopeGroup = isotopeService.FindIsotopes(point1, 1, false);

        Assert.Equal(1, isotopeGroup.Charge);
        Assert.Contains(point1, isotopeGroup.GetIsotopeGroup());
        Assert.Contains(point3, isotopeGroup.GetIsotopeGroup());
        Assert.Contains(point4, isotopeGroup.GetIsotopeGroup());
        Assert.DoesNotContain(point2, isotopeGroup.GetIsotopeGroup());

    }
    [Fact]
    public void FindIsotope_GivenSpecificCharge2_ReturnsCorrectIsotopeGroup()
    {
        MSDatapoint point1 = new()
        {
            Mz = 1.0,
            Intensity = 12345678
        };
        MSDatapoint point2 = new()
        {
            Mz = 1.5,
            Intensity = 12345678
        };
        MSDatapoint point3 = new()
        {
            Mz = 2,
            Intensity = 12345678
        };
        MSDatapoint point4 = new()
        {
            Mz = 3,
            Intensity = 12345678
        };

        IsotopeDetectionService isotopeService = new([point1, point2, point3]);
        IsotopeGroup isotopeGroup = isotopeService.FindIsotopes(point1, 2, false);

        Assert.Equal(2, isotopeGroup.Charge);
        Assert.Contains(point1, isotopeGroup.GetIsotopeGroup());
        Assert.Contains(point2, isotopeGroup.GetIsotopeGroup());
        Assert.Contains(point3, isotopeGroup.GetIsotopeGroup());
        Assert.DoesNotContain(point4, isotopeGroup.GetIsotopeGroup());


    }
    [Fact]
    public void FindIsotope_GivenStartPeakBelowIntensityThreshold_ReturnsEmptyGroup()
    {
        MSDatapoint point1 = new()
        {
            Mz = 1.0,
            Intensity = 123
        };
        MSDatapoint point2 = new()
        {
            Mz = 2,
            Intensity = 12345678
        };
        MSDatapoint point3 = new()
        {
            Mz = 3,
            Intensity = 12345678
        };


        IsotopeDetectionService isotopeService = new([point1, point2, point3,]);
        IsotopeGroup isotopeGroup = isotopeService.FindIsotopes(point1, 0, false);

        Assert.Equal(0, isotopeGroup.Charge);
        Assert.DoesNotContain(point1, isotopeGroup.GetIsotopeGroup());
        Assert.DoesNotContain(point2, isotopeGroup.GetIsotopeGroup());
        Assert.DoesNotContain(point3, isotopeGroup.GetIsotopeGroup());




    }
    [Fact]
    public void FindIsotope_GivenIsotopePeakBelowIntensityThreshold_ExcludesSubsequentPeaks()
    {
        MSDatapoint point1 = new()
        {
            Mz = 1.0,
            Intensity = 12345678
        };
        MSDatapoint point2 = new()
        {
            Mz = 2,
            Intensity = 12345678
        };
        MSDatapoint point3 = new()
        {
            Mz = 3,
            Intensity = 12345678
        };
        MSDatapoint point4 = new()
        {
            Mz = 4,
            Intensity = 1234
        };
        MSDatapoint point5 = new()
        {
            Mz = 5,
            Intensity = 500
        };

        IsotopeDetectionService isotopeService = new([point1, point2, point3, point4, point5]);
        IsotopeGroup isotopeGroup = isotopeService.FindIsotopes(point1, 0, false);

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
        ThermoFisher.CommonCore.Data.Interfaces.IRawDataExtended rawData = RawFileReaderAdapter.FileFactory(_validFilePath);
        rawData.SelectInstrument(Device.MS, 1);
        ScanStatistics scanStatistics = rawData.GetScanStatsForScanNumber(_profileScanNumber);
        CentroidStream centroidStream = rawData.GetCentroidStream(_profileScanNumber, false);
        ThermoSpectrum thermoSpectrum = new(centroidStream);
        MSDatapoint[] centroidPeaks = thermoSpectrum.CentroidData!.Where(p => p.Mz > 1056 && p.Mz < 1057.7)
                                       .OrderBy(p => p.Mz)
                                       .ToArray();
        MSDatapoint currentPeak = centroidPeaks.OrderByDescending(p => p.Intensity).First();
        int expectedChargeState = 6;
        int expectedIsotopePeakCount = 4;

        IsotopeDetectionService isotopeService = new(centroidPeaks);
        IsotopeGroup isotopePeaks = isotopeService.FindIsotopes(currentPeak, 0);

        Assert.Equal(expectedChargeState, isotopePeaks.Charge);
        Assert.Equal(expectedIsotopePeakCount, isotopePeaks.GetIsotopeGroup().Length);
    }
    [Fact]
    public void FindIsotope_GivenPeaksAboveAndBelowCurrent_FindsAllRealtedIsotopes()
    {
        MSDatapoint point1 = new()
        {
            Mz = 1.0,
            Intensity = 1000000
        };
        MSDatapoint point2 = new()
        {
            Mz = 2,
            Intensity = 1200000
        };
        MSDatapoint point3 = new()
        {
            Mz = 3,
            Intensity = 20000000
        };
        MSDatapoint point4 = new()
        {
            Mz = 4,
            Intensity = 1200000
        };
        MSDatapoint point5 = new()
        {
            Mz = 5,
            Intensity = 1000000
        };

        IsotopeDetectionService isotopeService = new([point1, point2, point3, point4, point5]);
        IsotopeGroup[] isotopeGroup = isotopeService.GroupIntoIsotopeEnvelopes();

        Assert.Single(isotopeGroup);
        Assert.Equal(5, isotopeGroup[0].GetIsotopeGroup().Length);
        Assert.Equal(1, isotopeGroup[0].Charge);

    }
    [Fact]
    public void FindIsotope_OverlappingDistributions_FindsAllIsotopeGroups()
    {
        ThermoFisher.CommonCore.Data.Interfaces.IRawDataExtended rawData = RawFileReaderAdapter.FileFactory(_validFilePath);
        rawData.SelectInstrument(Device.MS, 1);
        ScanStatistics scanStatistics = rawData.GetScanStatsForScanNumber(_profileScanNumber);
        CentroidStream centroidStream = rawData.GetCentroidStream(_profileScanNumber, false);
        ThermoSpectrum thermoSpectrum = new(centroidStream);
        MSDatapoint[] centroidPeaks = thermoSpectrum.CentroidData!.Where(p => p.Mz > 1056 && p.Mz < 1060.0)
                                       .OrderBy(p => p.Mz)
                                       .ToArray();
        MSDatapoint currentPeak = centroidPeaks.OrderByDescending(p => p.Intensity).First();
        int expectedChargeState1 = 6;
        int expectedIsotopePeakCount1 = 8;

        int expectedChargeState2 = 5;
        int expectedIsotopePeakCount2 = 12;

        int expectedIsotopeGroups = 2;

        IsotopeDetectionService isotopeService = new(centroidPeaks);
        IsotopeGroup[] isotopeGroups = isotopeService.GroupIntoIsotopeEnvelopes();

        Assert.Equal(expectedIsotopeGroups, isotopeGroups.Length);

        Assert.Equal(expectedChargeState2, isotopeGroups[0].Charge);
        Assert.Equal(expectedChargeState1, isotopeGroups[1].Charge);

        Assert.Equal(expectedIsotopePeakCount2, isotopeGroups[0].GetIsotopeGroup().Length);
        Assert.Equal(expectedIsotopePeakCount1, isotopeGroups[1].GetIsotopeGroup().Length);

    }



}
