using Emzedder.Datafile;

namespace Emzedder.Tests.Datafile.Helpers
{
    public class MSDatapointComparer : IEqualityComparer<MSDatapoint>
    {
        public bool Equals(MSDatapoint? x, MSDatapoint? y)
        {
            if (x == null || y == null) return false;
            return x.Mz == y.Mz && x.Intensity == y.Intensity;
        }

        public int GetHashCode(MSDatapoint obj)
        {
            return obj.Mz.GetHashCode() ^ obj.Intensity.GetHashCode();
        }
    }
}
