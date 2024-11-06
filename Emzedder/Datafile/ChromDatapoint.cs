using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emzedder.Datafile
{
    public class ChromDatapoint : IEquatable<ChromDatapoint>
    {
        public double RetentionTime { get; init; }
        public double Intensity { get; init; }
        public double BasePeakMass { get; init; }
        public int Scan { get; init; }

        public bool Equals(ChromDatapoint? other)
        {
            if (other == null) return false;
            return RetentionTime == other.RetentionTime && Intensity == other.Intensity && BasePeakMass == other.BasePeakMass;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(RetentionTime, Intensity, BasePeakMass);
        }
    }

}
