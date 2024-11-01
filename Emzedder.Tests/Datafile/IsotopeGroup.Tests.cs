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

    }
}
