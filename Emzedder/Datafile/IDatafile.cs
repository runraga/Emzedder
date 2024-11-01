using Emzedder.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emzedder.Datafile
{

    public interface IDatafile
    {
        bool InError { get; }
        public MSDatapoint[] GetTIC();
        public MSDatapoint[] GetBPC();
        public MSDatapoint[] GetXIC(double mass, double tolerance, MSUnits toleranceUnit);
    }
}
