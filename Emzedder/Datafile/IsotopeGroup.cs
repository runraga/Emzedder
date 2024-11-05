

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
        public void AddGroup(IsotopeGroup g)
        {
            if (Charge != g.Charge)
                throw new ArgumentException("Charges must match to combine isotope groups");
            IsotopePeaks = IsotopePeaks.Union(g.IsotopePeaks).ToList();

        }
        public bool IsEmpty()
        {
            return IsotopePeaks.Count == 0 ? true : false;
        }

    }
}
