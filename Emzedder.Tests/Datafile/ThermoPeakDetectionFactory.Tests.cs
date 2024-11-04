using Emzedder.Tests.Datafile.Helpers;
using Emzedder.Datafile;

namespace Emzedder.Tests.Datafile
{
    public class ThermoPeakDetectionFactoryTests : BaseTest
    {
        [Fact]
        public void DetectProfilePeak_TakesProfileDatapoints_Returns2DArrayOfDatapoints()
        {
            var testData = ExpectedMsSpectrumDatapoints.Take(35).ToArray();

            MSDatapoint[][] expected = [
                ExpectedMsSpectrumDatapoints[8..13],
                ExpectedMsSpectrumDatapoints[21..29],
                ];

            var actualPeaks = ThermoPeakDetectionFactory.DetectProfilePeaks(testData);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actualPeaks[i]);
            }

        }


        [Fact]
        public void DetectProfilePeaks_TakesEmptyArray_ReturnsEmpty2DArray()
        {
            var testData = ExpectedMsSpectrumDatapoints.Take(8).ToArray();

            var actualPeaks = ThermoPeakDetectionFactory.DetectProfilePeaks(testData);

            Assert.Empty(actualPeaks);

        }
        [Fact]
        public void DetectProfilePeaks_NoZeroIntensityBoundary_ReturnsCorrect2DArrayOfDatapoints()
        {
            var testData = ExpectedMsSpectrumDatapoints[8..30];

            MSDatapoint[][] expected = [
                testData[0..5],
                testData[13..21],
                ];

            var actualPeaks = ThermoPeakDetectionFactory.DetectProfilePeaks(testData);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actualPeaks[i]);
            }

        }
        [Fact]
        public void CalcWeightedAverageCentroid_GivenPeakMSDatapoint_ReturnsCorrectCentroidDatapoint()
        {
            var inputProfilePeak = ExpectedMsSpectrumDatapoints[8..13];
            var expectedCentroid = new MSDatapoint()
            {
                Intensity = Math.Round(408985.78125, 4),
                Mz = Math.Round(350.3694851, 4)
            };

            var calcCentroid = ThermoPeakDetectionFactory.CalcWeightedAverageCentroid(inputProfilePeak);

            Assert.Equal(expectedCentroid, calcCentroid, new MSDatapointComparer());
        }
        [Fact]
        public void CalcWeightedAverageCentroid_GivenSingleDatapoint_ReturnsThatDatapoint()
        {
            var inputProfilePeak = new MSDatapoint[1];
            inputProfilePeak[0] = ExpectedMsSpectrumDatapoints[8];

            var expectedCentroid = new MSDatapoint()
            {
                Intensity = Math.Round(212817.03125, 4),
                Mz = Math.Round(350.3675951, 4)
            };

            var calcCentroid = ThermoPeakDetectionFactory.CalcWeightedAverageCentroid(inputProfilePeak);

            Assert.Equal(expectedCentroid, calcCentroid, new MSDatapointComparer());
        }
        //TODO: need to revisit this once better gaussian fit approach is found
        //current expected values from ChatGPT
        //what is best way to implelemetn gaussian fit checking?
        //[Fact]
        //public void FitGaussian_TakesProfilePeak_ReturnCentroidDatapoint()
        //{
        //    var testPeak = ExpectedMsSpectrumDatapoints[26324..26340];


        //    MSDatapoint expectedCentroid = new MSDatapoint()
        //    {
        //        Mz = 1322.2567,
        //        Intensity = 1281179181.1714
        //    };
        //    var actualCentroid = ThermoPeakDetectionFactory.FitGaussian(testPeak);

        //    Assert.Equal(expectedCentroid, actualCentroid, new MSDatapointComparer());

        //}

    }
}
