using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emzedder.Datafile
{
    internal class IsotopeGroup
    {
        private List<MSDatapoint> IsotopePeaks = [];
        public int Charge { get; init; }

        public IsotopeGroup(int charge)
        {
            Charge = charge;
        }
        public void AddIsotope(MSDatapoint isotope)
        {
            IsotopePeaks.Add(isotope);
        }
        public MSDatapoint[] GetIsotopeGroup()
        {
            return IsotopePeaks.ToArray();
        }

    }
}
