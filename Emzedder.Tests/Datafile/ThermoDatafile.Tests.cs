using Emzedder.Datafile;



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
        public void GetBasePeakChromatogram_ReturnsMSDataPointArray_HasCorrectData()
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


    }

}
