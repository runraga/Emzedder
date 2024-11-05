using Emzedder.Datafile;

namespace Emzedder.Tests.Datafile
{
    public class MSDatapointTests
    {
        [Fact]
        public void Equals_DatapointsHaveSameValues_ReturnsTrue()
        {
            var dp1 = new MSDatapoint()
            {
                Mz = 1234,
                Intensity = 12345
            };
            var dp2 = new MSDatapoint()
            {
                Mz = 1234,
                Intensity = 12345
            };
            Assert.True(dp1.Equals(dp2));
        }
        [Fact]
        public void Equals_OtherDatapointNull_ReturnsFalse()
        {
            var dp1 = new MSDatapoint()
            {
                Mz = 1234,
                Intensity = 12345
            };
            MSDatapoint? dp2 = null;
            Assert.False(dp1.Equals(dp2));
        }
        [Fact]
        public void Equals_OtherDatapointMzValueDifferent_ReturnsFalse()
        {
            var dp1 = new MSDatapoint()
            {
                Mz = 1234,
                Intensity = 12345
            };
            MSDatapoint? dp2 = new MSDatapoint()
            {
                Mz = 5678,
                Intensity = 12345
            };
            Assert.False(dp1.Equals(dp2));
        }
        [Fact]
        public void Equals_OtherDatapointIntensityValueDifferent_ReturnsFalse()
        {
            var dp1 = new MSDatapoint()
            {
                Mz = 1234,
                Intensity = 12345
            };
            MSDatapoint? dp2 = new MSDatapoint()
            {
                Mz = 1234,
                Intensity = 56789
            };
            Assert.False(dp1.Equals(dp2));
        }
        [Fact]
        public void Equals_OtherDatapointBothValuesDifferent_ReturnsFalse()
        {
            var dp1 = new MSDatapoint()
            {
                Mz = 1234,
                Intensity = 12345
            };
            MSDatapoint? dp2 = new MSDatapoint()
            {
                Mz = 789,
                Intensity = 56789
            };
            Assert.False(dp1.Equals(dp2));
        }
        [Fact]
        public void Equals_WhenComparedToItself_ReturnsTrue()
        {
            var dp1 = new MSDatapoint()
            {
                Mz = 1234,
                Intensity = 12345
            };

            Assert.True(dp1.Equals(dp1));
        }
        [Fact]
        public void Equals_WhenComparedBothWays_ReturnsTrue()
        {
            var dp1 = new MSDatapoint()
            {
                Mz = 1234,
                Intensity = 12345
            };
            MSDatapoint? dp2 = new MSDatapoint()
            {
                Mz = 1234,
                Intensity = 12345
            };

            Assert.True(dp1.Equals(dp2) && dp2.Equals(dp1));
        }
        [Fact]
        public void Equals_WhenComparedToThirdPoint_AllComparisonsReturnsTrue()
        {
            var dp1 = new MSDatapoint()
            {
                Mz = 1234,
                Intensity = 12345
            };
            MSDatapoint? dp2 = new MSDatapoint()
            {
                Mz = 1234,
                Intensity = 12345
            };
            MSDatapoint? dp3 = new MSDatapoint()
            {
                Mz = 1234,
                Intensity = 12345
            };

            Assert.True(dp1.Equals(dp2) && dp1.Equals(dp3) && dp2.Equals(dp3));
        }
        [Fact]
        public void GetHashCode_MultipleCallsOnSameObject_ReturnsSameCode()
        {
            var dp1 = new MSDatapoint()
            {
                Mz = 1234,
                Intensity = 12345
            };
            var hash1 = dp1.GetHashCode();
            var hash2 = dp1.GetHashCode();

            Assert.Equal(hash1, hash2);
        }
        [Fact]
        public void GetHashCode_EqualObjects_ReturnsSameCode()
        {
            var dp1 = new MSDatapoint()
            {
                Mz = 1234,
                Intensity = 12345
            };
            var dp2 = new MSDatapoint()
            {
                Mz = 1234,
                Intensity = 12345
            };
            var hash1 = dp1.GetHashCode();
            var hash2 = dp2.GetHashCode();

            Assert.Equal(hash1, hash2);
        }
        [Fact]
        public void GetHashCode_DifferentObjects_UnlikelyToReturnSameCode()
        {
            var dp1 = new MSDatapoint()
            {
                Mz = 1234,
                Intensity = 12345
            };
            var dp2 = new MSDatapoint()
            {
                Mz = 2345,
                Intensity = 12345
            };
            var hash1 = dp1.GetHashCode();
            var hash2 = dp2.GetHashCode();

            Assert.NotEqual(hash1, hash2);
        }
    }
}
