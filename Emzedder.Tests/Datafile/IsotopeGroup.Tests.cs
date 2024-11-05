using Emzedder.Datafile;

namespace Emzedder.Tests.Datafile
{
    public class IsotopeGroupTests

    {
        [Fact]
        public void Constructor_SetsChargeProperty()
        {
            int expectedCharge = 2;

            var isotopeGroup = new IsotopeGroup(expectedCharge);

            Assert.Equal(expectedCharge, isotopeGroup.Charge);
        }
        [Fact]
        public void AddIsotope_CorrectlyAddsIsotopeToList()
        {
            //test add and get separately?
            var expectedLength = 1;

            var dp = new MSDatapoint() { Mz = 1.234, Intensity = 1234 };

            var group = new IsotopeGroup(2);

            group.AddIsotope(dp);

            var isotopes = group.GetIsotopeGroup();

            Assert.Equal(expectedLength, isotopes.Length);

            Assert.Equal(dp, isotopes[0]);

        }
        [Fact]
        public void AddGroup_GivenIsotopeGroup_KeepsUniqueIsotopes()
        {
            //arrange
            var point1 = new MSDatapoint()
            {
                Mz = 1.0,
                Intensity = 10345678
            };
            var point2 = new MSDatapoint()
            {
                Mz = 2,
                Intensity = 11345678
            };
            var point3 = new MSDatapoint()
            {
                Mz = 3,
                Intensity = 12345678
            };
            var point4 = new MSDatapoint()
            {
                Mz = 4,
                Intensity = 11345678
            };
            var point5 = new MSDatapoint()
            {
                Mz = 5,
                Intensity = 10345678
            };
            int expectedLength = 5;

            var group1 = new IsotopeGroup(1);
            var group2 = new IsotopeGroup(1);

            group1.AddIsotope(point2);
            group1.AddIsotope(point3);
            group1.AddIsotope(point4);
            group1.AddIsotope(point5);

            group2.AddIsotope(point1);
            group2.AddIsotope(point2);

            group1.AddGroup(group2);

            var actualIsotopes = group1.GetIsotopeGroup();

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
            var point1 = new MSDatapoint()
            {
                Mz = 1.0,
                Intensity = 10345678
            };
            var point2 = new MSDatapoint()
            {
                Mz = 2,
                Intensity = 11345678
            };

            var group1 = new IsotopeGroup(1);
            var group2 = new IsotopeGroup(2);

            var expectedException = typeof(ArgumentException);

            group1.AddIsotope(point1);

            group2.AddIsotope(point2);

            Assert.Throws(expectedException, () => group1.AddGroup(group2));

        }
        [Fact]
        public void IsEmpty_WhenNoIsotopesAdded_ReturnsTrue()
        {
            //arrange
            var group = new IsotopeGroup(1);

            Assert.True(group.IsEmpty());

        }
        [Fact]
        public void IsEmpty_WhenIsotopesAdded_ReturnsFalse()
        {
            //arrange
            var group = new IsotopeGroup(1);
            group.AddIsotope(new MSDatapoint() { Mz = 1, Intensity = 2 });
            Assert.False(group.IsEmpty());

        }

    }
}
