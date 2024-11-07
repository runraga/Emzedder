using Emzedder.Datafile;

namespace EmzedderWinForms.Controller
{
    public class DatafileController
    {
        private ThermoDatafile? Datafile { get; set; }
        public ChromatogramType ChromType { get; set; } = ChromatogramType.BPC;

        public ChromDatapoint[]? CurrentChrom;
        private readonly Form1 _view;
        public DatafileController(Form1 view)
        {
            _view = view;
        }
        public string FilePath { get; private set; } = "Choose a datafile...";
        public void OpenDatafile(string filePath)
        {
            FilePath = filePath;
            _view.UpdateFileNameLabel(filePath);
            Datafile = new ThermoDatafile(filePath);
            SetChromatogram();
            PlotChromatogram();
        }
        private void PlotChromatogram()
        {
            if (CurrentChrom == null) return;
            var xData = CurrentChrom.Select(d => d.RetentionTime).ToArray();
            var yData = CurrentChrom.Select(d => d.Intensity).ToArray();
            _view.PlotChromatogram(xData, yData);
        }
        private void SetChromatogram()
        {
            if (Datafile != null)
            {
                switch (ChromType)
                {
                    case ChromatogramType.BPC:
                        CurrentChrom = Datafile.GetBasePeakChromatogram();
                        break;
                    case ChromatogramType.TIC:
                        CurrentChrom = Datafile.GetUnfilteredChromatogram();
                        break;
                    case ChromatogramType.Unfiltered:
                        CurrentChrom = Datafile.GetUnfilteredChromatogram();
                        break;
                    default:
                        break;
                }
            }
        }
        public int GetNearestScanNumber(double retentionTime)
        {

            return CurrentChrom.OrderBy(d => Math.Abs(d.RetentionTime - retentionTime))
                                .First()
                                .Scan;
        }
        public double? GetBasePeakMass(int scanNumber)
        {
            return Array.Find(CurrentChrom, d => d.Scan == scanNumber).BasePeakMass;
        }
    }
    public enum ChromatogramType
    {
        BPC,
        TIC,
        Unfiltered
    }
}
