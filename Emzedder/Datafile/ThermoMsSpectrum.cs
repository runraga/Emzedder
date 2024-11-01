using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emzedder.Datafile
{
    internal class ThermoMsSpectrum
    {
        public MSDatapoint[] MsDatapoints { get; init; }
        public int ScanNumber { get; init; }

        public bool IsCentroidScan { get; init; }

        //constructor for SegmentedScan


        //constructor for CentroidStream

    }
}
