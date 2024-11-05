using Accord.Math;
using Emzedder.Datafile;
using Emzedder.Tests.Datafile.Helpers;



namespace Emzedder.Tests.Datafile
{
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

            var df = new ThermoDatafile(_validFilePath);
            Assert.False(df.InError);

        }
        [Fact]
        public void GetBasePeakChromatogram_ReturnsChromDatapointArray_HasCorrectData()
        {
            var df = new ThermoDatafile(_validFilePath);

            var bpc = df.GetBasePeakChromatogram();
            var actualChromDatapoints = bpc.Length;
            var expectedChromDatapoints = 4275;

            Assert.Equal(actualChromDatapoints, expectedChromDatapoints);

            var actualFirstDatapointRt = Math.Round(bpc[0].RetentionTime, 5);
            var expectedFirstDatapointRt = 0.00282;

            var actualFirstDatapointInt = bpc[0].Intensity;
            var expectedFirstDatapointInt = 19821058;

            Assert.Equal(actualFirstDatapointRt, expectedFirstDatapointRt);
            Assert.Equal(actualFirstDatapointInt, expectedFirstDatapointInt);
        }
        [Fact]
        public void GetUnfilteredChromatogram_ReturnsMSDataPointArray_HasCorrectData()
        {
            var df = new ThermoDatafile(_validFilePath);

            var unfilteredChrom = df.GetUnfilteredChromatogram();
            var actualChromDatapoints = unfilteredChrom.Length;
            var expectedChromDatapoints = 47683;

            Assert.Equal(expectedChromDatapoints, actualChromDatapoints);

            var actual34thDatapointRt = Math.Round(unfilteredChrom[33].RetentionTime, 5);
            var expected34thDatapointRt = 0.10464;

            var actual34thDatapointInt = Math.Round(unfilteredChrom[33].Intensity);
            var expected34thDatapointInt = 816214;

            Assert.Equal(expected34thDatapointRt, actual34thDatapointRt);
            Assert.Equal(expected34thDatapointInt, actual34thDatapointInt);
        }

        [Fact]
        public void GetMassSpectrum_GivenScanNumberForMsScan_ReturnsProfileData()
        {
            var df = new ThermoDatafile(_validFilePath);
            var scanNumber = 30749;
            var expectedFirstDatapoint = new MSDatapoint()
            {
                Mz = Math.Round(346.513885529195, 4),
                Intensity = 0
            };
            var expected16152Datapoint = new MSDatapoint()
            {
                Mz = Math.Round(895.353052977902, 4),
                Intensity = Math.Round(264643.8125, 4)
            };
            var spectrumData = df.GetMassSpectrum(scanNumber);

            Assert.Equal(expectedFirstDatapoint, spectrumData[0]);
            Assert.Equal(expected16152Datapoint, spectrumData[16151]);
        }
        [Fact]
        public void GetMassSpectrum_GivenInvalidScanNumber_ThrowsArgumentException()
        {
            var df = new ThermoDatafile(_validFilePath);
            var scanNumber = 0;
            var scanNumber2 = 1000000000;


            Assert.Throws<ArgumentException>(() => df.GetMassSpectrum(scanNumber));
            Assert.Throws<ArgumentException>(() => df.GetMassSpectrum(scanNumber2));
        }
        [Fact]
        public void GetMassSpectrum_GivenScanNumberForMs2Scan_ReturnsCentroidData()
        {
            var df = new ThermoDatafile(_validFilePath);
            var scanNumber = 30750;
            var expectedFirstDatapoint = new MSDatapoint()
            {
                Mz = Math.Round(115.050064086914, 4),
                Intensity = Math.Round(197426.0625, 4)
            };
            var expected930Datapoint = new MSDatapoint()
            {
                Mz = Math.Round(905.526062011719, 4),
                Intensity = Math.Round(56135660.0, 4)
            };
            var spectrumData = df.GetMassSpectrum(scanNumber);

            Assert.Equal(expectedFirstDatapoint, spectrumData[0]);
            Assert.Equal(expected930Datapoint, spectrumData[929]);
        }
        [Fact]
        public void GetProductSpectraForMsScan_GivenValidMsScanNumber_GivesCorrectListOfMsSpectra()
        {
            var df = new ThermoDatafile(_validFilePath);
            var scanNumber = 30749;

            var expectedProductCount = 19;
            var expectedFirstPrecursorMass = 1322.5056;
            var expectedLastPrecursorMass = 727.8637;

            var productPrecursorMasses = df.GetProductSpectraForMsScan(scanNumber);
            var actualCount = productPrecursorMasses.Length;

            Assert.Equal(expectedProductCount, actualCount);
            Assert.Equal(expectedFirstPrecursorMass, productPrecursorMasses[0]);
            Assert.Equal(expectedLastPrecursorMass, productPrecursorMasses[actualCount - 1]);


        }
        [Fact]
        public void GetProductSpectraForMsScan_GivenInvalidScanNumber_ThrowsArgumentException()
        {
            var df = new ThermoDatafile(_validFilePath);
            var scanNumberLow = -5;
            var scanNumberHigh = 1000000000;

            Assert.Throws<ArgumentException>(() => df.GetProductSpectraForMsScan(scanNumberLow));
            Assert.Throws<ArgumentException>(() => df.GetProductSpectraForMsScan(scanNumberHigh));


        }
        [Fact]
        public void GetProductSpectraForMsScan_WhenNoSubsequentMs2_ReturnsEmptyArray()
        {
            var df = new ThermoDatafile(_validFilePath);
            var scanNumber = 7702;

            var productPrecursorMasses = df.GetProductSpectraForMsScan(scanNumber);

            Assert.Empty(productPrecursorMasses);

        }
    }

}
