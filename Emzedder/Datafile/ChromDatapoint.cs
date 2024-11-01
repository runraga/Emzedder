using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emzedder.Datafile
{
    public class ChromDatapoint
    {
        public double RetentionTime { get; init; }
        public double Intensity { get; init; }
        public double BasePeakMass { get; init; }
    }
}
