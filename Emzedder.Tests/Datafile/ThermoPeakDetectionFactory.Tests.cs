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
        public void DetectProfilePeak_TakesEmptyArray_ReturnsEmpty2DArray()
        {
            var testData = ExpectedMsSpectrumDatapoints.Take(8).ToArray();

            var actualPeaks = ThermoPeakDetectionFactory.DetectProfilePeaks(testData);

            Assert.Empty(actualPeaks);

        }
        [Fact]
        public void DetectProfilePeak_NoZeroIntensityBoundary_Returns2DArrayOfDatapoints()
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
