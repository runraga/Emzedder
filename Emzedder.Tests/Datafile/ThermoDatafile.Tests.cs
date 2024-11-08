using Emzedder.Datafile;
using Emzedder.Tests.Datafile.Helpers;



namespace Emzedder.Tests.Datafile;

public class ThermoDatafileTests : BaseTest
{
    [Fact]
    public void Constructor_InvalidPath_ThrowsAnException()
    {
        Assert.Throws<FileNotFoundException>(() => new ThermoDatafile(_invalidFilePath));
    }
    [Fact]
    public void Constructor_NotValidThermoFile_ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new ThermoDatafile(_invalidRawFile));
    }
    [Fact]
    public void Constructor_FileInError_SetsInErrorStateCorrectly()
    {

        ThermoDatafile df = new(_validFilePath);
        Assert.False(df.InError);

    }
    [Fact]
    public void GetBasePeakChromatogram_ReturnsChromDatapointArray_HasCorrectData()
    {
        ThermoDatafile df = new(_validFilePath);

        ChromDatapoint[] bpc = df.GetBasePeakChromatogram();
        int actualChromDatapoints = bpc.Length;
        int expectedChromDatapoints = 4275;

        Assert.Equal(actualChromDatapoints, expectedChromDatapoints);

        double actualFirstDatapointRt = Math.Round(bpc[0].RetentionTime, 5);
        double expectedFirstDatapointRt = 0.00282;

        double actualFirstDatapointInt = bpc[0].Intensity;
        int expectedFirstDatapointInt = 19821058;

        Assert.Equal(actualFirstDatapointRt, expectedFirstDatapointRt);
        Assert.Equal(actualFirstDatapointInt, expectedFirstDatapointInt);
    }
    [Fact]
    public void GetUnfilteredChromatogram_ReturnsMSDataPointArray_HasCorrectData()
    {
        ThermoDatafile df = new(_validFilePath);

        ChromDatapoint[] unfilteredChrom = df.GetUnfilteredChromatogram();
        int actualChromDatapoints = unfilteredChrom.Length;
        int expectedChromDatapoints = 47683;

        Assert.Equal(expectedChromDatapoints, actualChromDatapoints);

        double actual34thDatapointRt = Math.Round(unfilteredChrom[33].RetentionTime, 5);
        double expected34thDatapointRt = 0.10464;

        double actual34thDatapointInt = Math.Round(unfilteredChrom[33].Intensity);
        int expected34thDatapointInt = 816214;

        Assert.Equal(expected34thDatapointRt, actual34thDatapointRt);
        Assert.Equal(expected34thDatapointInt, actual34thDatapointInt);
    }

    [Fact]
    public void GetMassSpectrum_GivenScanNumberForMsScan_ReturnsProfileData()
    {
        ThermoDatafile df = new(_validFilePath);
        int scanNumber = 30749;
        MSDatapoint expectedFirstDatapoint = new()
        {
            Mz = Math.Round(346.513885529195, 4),
            Intensity = 0
        };
        MSDatapoint expected16152Datapoint = new()
        {
            Mz = Math.Round(895.353052977902, 4),
            Intensity = Math.Round(264643.8125, 4)
        };
        (MSDatapoint[] spectrumData, bool _) = df.GetMassSpectrum(scanNumber);

        Assert.Equal(expectedFirstDatapoint, spectrumData[0]);
        Assert.Equal(expected16152Datapoint, spectrumData[16151]);
    }
    [Fact]
    public void GetMassSpectrum_GivenInvalidScanNumber_ThrowsArgumentException()
    {
        ThermoDatafile df = new(_validFilePath);
        int scanNumber = 0;
        int scanNumber2 = 1000000000;


        Assert.Throws<ArgumentException>(() => df.GetMassSpectrum(scanNumber));
        Assert.Throws<ArgumentException>(() => df.GetMassSpectrum(scanNumber2));
    }
    [Fact]
    public void GetMassSpectrum_GivenScanNumberForMs2Scan_ReturnsCentroidData()
    {
        ThermoDatafile df = new(_validFilePath);
        int scanNumber = 30750;
        MSDatapoint expectedFirstDatapoint = new()
        {
            Mz = Math.Round(115.050064086914, 4),
            Intensity = Math.Round(197426.0625, 4)
        };
        MSDatapoint expected930Datapoint = new()
        {
            Mz = Math.Round(905.526062011719, 4),
            Intensity = Math.Round(56135660.0, 4)
        };
        (MSDatapoint[] spectrumData, bool _) = df.GetMassSpectrum(scanNumber);

        Assert.Equal(expectedFirstDatapoint, spectrumData[0]);
        Assert.Equal(expected930Datapoint, spectrumData[929]);
    }
    [Fact]
    public void GetProductSpectraForMsScan_GivenValidMsScanNumber_GivesCorrectListOfMsSpectra()
    {
        ThermoDatafile df = new(_validFilePath);
        int scanNumber = 30749;

        int expectedProductCount = 19;
        double expectedFirstPrecursorMass = 1322.5056;
        double expectedLastPrecursorMass = 727.8637;

        double[] productPrecursorMasses = df.GetProductMassesForMsScan(scanNumber);
        int actualCount = productPrecursorMasses.Length;

        Assert.Equal(expectedProductCount, actualCount);
        Assert.Equal(expectedFirstPrecursorMass, productPrecursorMasses[0]);
        Assert.Equal(expectedLastPrecursorMass, productPrecursorMasses[actualCount - 1]);


    }
    [Fact]
    public void GetProductSpectraForMsScan_GivenInvalidScanNumber_ThrowsArgumentException()
    {
        ThermoDatafile df = new(_validFilePath);
        int scanNumberLow = -5;
        int scanNumberHigh = 1000000000;

        Assert.Throws<ArgumentException>(() => df.GetProductMassesForMsScan(scanNumberLow));
        Assert.Throws<ArgumentException>(() => df.GetProductMassesForMsScan(scanNumberHigh));


    }
    [Fact]
    public void GetProductSpectraForMsScan_WhenNoSubsequentMs2_ReturnsEmptyArray()
    {
        ThermoDatafile df = new(_validFilePath);
        int scanNumber = 7702;

        double[] productPrecursorMasses = df.GetProductMassesForMsScan(scanNumber);

        Assert.Empty(productPrecursorMasses);

    }
}
