using Emzedder.Datafile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emzedder.Tests.Datafile
{
    public class ChromDatapointTests
    {
        [Fact]
        public void Equals_DatapointsHaveSameValues_ReturnsTrue()
        {
            var dp1 = new ChromDatapoint()
            {
                RetentionTime = 1234,
                Intensity = 12345,
                BasePeakMass = 500
            };
            var dp2 = new ChromDatapoint()
            {
                RetentionTime = 1234,
                Intensity = 12345,
                BasePeakMass = 500
            };
            Assert.True(dp1.Equals(dp2));
        }
        [Fact]
        public void Equals_OtherDatapointNull_ReturnsFalse()
        {
            var dp1 = new ChromDatapoint()
            {
                RetentionTime = 1234,
                Intensity = 12345,
                BasePeakMass = 500
            };

            MSDatapoint? dp2 = null;
            Assert.False(dp1.Equals(dp2));
        }
        [Fact]
        public void Equals_OtherDatapointRtValueDifferent_ReturnsFalse()
        {
            var dp1 = new ChromDatapoint()
            {
                RetentionTime = 1234,
                Intensity = 12345,
                BasePeakMass = 500
            };
            var dp2 = new ChromDatapoint()
            {
                RetentionTime = 1235,
                Intensity = 12345,
                BasePeakMass = 500
            };
            Assert.False(dp1.Equals(dp2));
        }
        [Fact]
        public void Equals_OtherDatapointIntensityValueDifferent_ReturnsFalse()
        {
            var dp1 = new ChromDatapoint()
            {
                RetentionTime = 1234,
                Intensity = 12345,
                BasePeakMass = 500
            };
            var dp2 = new ChromDatapoint()
            {
                RetentionTime = 1234,
                Intensity = 12346,
                BasePeakMass = 500
            };
            Assert.False(dp1.Equals(dp2));
        }
        [Fact]
        public void Equals_OtherDatapointAllValuesDifferent_ReturnsFalse()
        {
            var dp1 = new ChromDatapoint()
            {
                RetentionTime = 1234,
                Intensity = 12345,
                BasePeakMass = 500
            };
            var dp2 = new ChromDatapoint()
            {
                RetentionTime = 1235,
                Intensity = 12346,
                BasePeakMass = 501
            };
            Assert.False(dp1.Equals(dp2));
        }
        [Fact]
        public void Equals_WhenComparedToItself_ReturnsTrue()
        {
            var dp1 = new ChromDatapoint()
            {
                RetentionTime = 1234,
                Intensity = 12345,
                BasePeakMass = 500
            };

            Assert.True(dp1.Equals(dp1));
        }
        [Fact]
        public void Equals_WhenComparedBothWays_ReturnsTrue()
        {
            var dp1 = new ChromDatapoint()
            {
                RetentionTime = 1234,
                Intensity = 12345,
                BasePeakMass = 500
            };
            var dp2 = new ChromDatapoint()
            {
                RetentionTime = 1234,
                Intensity = 12345,
                BasePeakMass = 500
            };

            Assert.True(dp1.Equals(dp2) && dp2.Equals(dp1));
        }
        [Fact]
        public void Equals_WhenComparedToThirdPoint_AllComparisonsReturnsTrue()
        {
            var dp1 = new ChromDatapoint()
            {
                RetentionTime = 1234,
                Intensity = 12345,
                BasePeakMass = 500
            };
            var dp2 = new ChromDatapoint()
            {
                RetentionTime = 1234,
                Intensity = 12345,
                BasePeakMass = 500
            };
            var dp3 = new ChromDatapoint()
            {
                RetentionTime = 1234,
                Intensity = 12345,
                BasePeakMass = 500
            };

            Assert.True(dp1.Equals(dp2) && dp1.Equals(dp3) && dp2.Equals(dp3));
        }
        [Fact]
        public void GetHashCode_MultipleCallsOnSameObject_ReturnsSameCode()
        {
            var dp1 = new ChromDatapoint()
            {
                RetentionTime = 1234,
                Intensity = 12345,
                BasePeakMass = 500
            };
            var hash1 = dp1.GetHashCode();
            var hash2 = dp1.GetHashCode();

            Assert.Equal(hash1, hash2);
        }
        [Fact]
        public void GetHashCode_EqualObjects_ReturnsSameCode()
        {
            var dp1 = new ChromDatapoint()
            {
                RetentionTime = 1234,
                Intensity = 12345,
                BasePeakMass = 500
            };
            var dp2 = new ChromDatapoint()
            {
                RetentionTime = 1234,
                Intensity = 12345,
                BasePeakMass = 500
            };
            var hash1 = dp1.GetHashCode();
            var hash2 = dp2.GetHashCode();

            Assert.Equal(hash1, hash2);
        }
        [Fact]
        public void GetHashCode_DifferentObjects_UnlikelyToReturnSameCode()
        {
            var dp1 = new ChromDatapoint()
            {
                RetentionTime = 1234,
                Intensity = 12346,
                BasePeakMass = 500
            };
            var dp2 = new ChromDatapoint()
            {
                RetentionTime = 1234,
                Intensity = 12345,
                BasePeakMass = 500
            };
            var hash1 = dp1.GetHashCode();
            var hash2 = dp2.GetHashCode();

            Assert.NotEqual(hash1, hash2);
        }
    }
}
