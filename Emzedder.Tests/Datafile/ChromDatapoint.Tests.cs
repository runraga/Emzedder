using Emzedder.Datafile;

namespace Emzedder.Tests.Datafile;

public class ChromDatapointTests
{
    [Fact]
    public void Equals_DatapointsHaveSameValues_ReturnsTrue()
    {
        ChromDatapoint dp1 = new()
        {
            RetentionTime = 1234,
            Intensity = 12345,
            BasePeakMass = 500
        };
        ChromDatapoint dp2 = new()
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
        ChromDatapoint dp1 = new()
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
        ChromDatapoint dp1 = new()
        {
            RetentionTime = 1234,
            Intensity = 12345,
            BasePeakMass = 500
        };
        ChromDatapoint dp2 = new()
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
        ChromDatapoint dp1 = new()
        {
            RetentionTime = 1234,
            Intensity = 12345,
            BasePeakMass = 500
        };
        ChromDatapoint dp2 = new()
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
        ChromDatapoint dp1 = new()
        {
            RetentionTime = 1234,
            Intensity = 12345,
            BasePeakMass = 500
        };
        ChromDatapoint dp2 = new()
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
        ChromDatapoint dp1 = new()
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
        ChromDatapoint dp1 = new()
        {
            RetentionTime = 1234,
            Intensity = 12345,
            BasePeakMass = 500
        };
        ChromDatapoint dp2 = new()
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
        ChromDatapoint dp1 = new()
        {
            RetentionTime = 1234,
            Intensity = 12345,
            BasePeakMass = 500
        };
        ChromDatapoint dp2 = new()
        {
            RetentionTime = 1234,
            Intensity = 12345,
            BasePeakMass = 500
        };
        ChromDatapoint dp3 = new()
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
        ChromDatapoint dp1 = new()
        {
            RetentionTime = 1234,
            Intensity = 12345,
            BasePeakMass = 500
        };
        int hash1 = dp1.GetHashCode();
        int hash2 = dp1.GetHashCode();

        Assert.Equal(hash1, hash2);
    }
    [Fact]
    public void GetHashCode_EqualObjects_ReturnsSameCode()
    {
        ChromDatapoint dp1 = new()
        {
            RetentionTime = 1234,
            Intensity = 12345,
            BasePeakMass = 500
        };
        ChromDatapoint dp2 = new()
        {
            RetentionTime = 1234,
            Intensity = 12345,
            BasePeakMass = 500
        };
        int hash1 = dp1.GetHashCode();
        int hash2 = dp2.GetHashCode();

        Assert.Equal(hash1, hash2);
    }
    [Fact]
    public void GetHashCode_DifferentObjects_UnlikelyToReturnSameCode()
    {
        ChromDatapoint dp1 = new()
        {
            RetentionTime = 1234,
            Intensity = 12346,
            BasePeakMass = 500
        };
        ChromDatapoint dp2 = new()
        {
            RetentionTime = 1234,
            Intensity = 12345,
            BasePeakMass = 500
        };
        int hash1 = dp1.GetHashCode();
        int hash2 = dp2.GetHashCode();

        Assert.NotEqual(hash1, hash2);
    }
}
