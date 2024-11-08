using Emzedder.Datafile;

namespace Emzedder.Tests.Datafile;

public class IsotopeGroupTests

{
    [Fact]
    public void Constructor_SetsChargeProperty()
    {
        int expectedCharge = 2;

        IsotopeGroup isotopeGroup = new(expectedCharge);

        Assert.Equal(expectedCharge, isotopeGroup.Charge);
    }
    [Fact]
    public void AddIsotope_CorrectlyAddsIsotopeToList()
    {
        //test add and get separately?
        int expectedLength = 1;

        MSDatapoint dp = new() { Mz = 1.234, Intensity = 1234 };

        IsotopeGroup group = new(2);

        group.AddIsotope(dp);

        MSDatapoint[] isotopes = group.GetIsotopeGroup();

        Assert.Equal(expectedLength, isotopes.Length);

        Assert.Equal(dp, isotopes[0]);

    }
    [Fact]
    public void AddGroup_GivenIsotopeGroup_KeepsUniqueIsotopes()
    {
        //arrange
        MSDatapoint point1 = new()
        {
            Mz = 1.0,
            Intensity = 10345678
        };
        MSDatapoint point2 = new()
        {
            Mz = 2,
            Intensity = 11345678
        };
        MSDatapoint point3 = new()
        {
            Mz = 3,
            Intensity = 12345678
        };
        MSDatapoint point4 = new()
        {
            Mz = 4,
            Intensity = 11345678
        };
        MSDatapoint point5 = new()
        {
            Mz = 5,
            Intensity = 10345678
        };
        int expectedLength = 5;

        IsotopeGroup group1 = new(1);
        IsotopeGroup group2 = new(1);

        group1.AddIsotope(point2);
        group1.AddIsotope(point3);
        group1.AddIsotope(point4);
        group1.AddIsotope(point5);

        group2.AddIsotope(point1);
        group2.AddIsotope(point2);

        group1.AddGroup(group2);

        MSDatapoint[] actualIsotopes = group1.GetIsotopeGroup();

        Assert.Equal(expectedLength, actualIsotopes.Length);

        Assert.Contains(point1, actualIsotopes);
        Assert.Contains(point2, actualIsotopes);
        Assert.Contains(point3, actualIsotopes);
        Assert.Contains(point4, actualIsotopes);
        Assert.Contains(point5, actualIsotopes);

    }
    [Fact]
    public void AddGroup_GivenIsotopeGroupOfDifferentCharge_ThrowsInvalidArgumentException()
    {
        //arrange
        MSDatapoint point1 = new()
        {
            Mz = 1.0,
            Intensity = 10345678
        };
        MSDatapoint point2 = new()
        {
            Mz = 2,
            Intensity = 11345678
        };

        IsotopeGroup group1 = new(1);
        IsotopeGroup group2 = new(2);

        Type expectedException = typeof(ArgumentException);

        group1.AddIsotope(point1);

        group2.AddIsotope(point2);

        Assert.Throws(expectedException, () => group1.AddGroup(group2));

    }
    [Fact]
    public void IsEmpty_WhenNoIsotopesAdded_ReturnsTrue()
    {
        //arrange
        IsotopeGroup group = new(1);

        Assert.True(group.IsEmpty());

    }
    [Fact]
    public void IsEmpty_WhenIsotopesAdded_ReturnsFalse()
    {
        //arrange
        IsotopeGroup group = new(1);
        group.AddIsotope(new MSDatapoint() { Mz = 1, Intensity = 2 });
        Assert.False(group.IsEmpty());

    }

}
