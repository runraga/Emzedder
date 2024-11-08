using Emzedder.Datafile;

namespace EmzedderWinForms.Controller;

public class DatafileController
{
    private readonly Form1 View;

    private ThermoDatafile? _datafile;
    private MassSpectrumWindow? _spectrumWindow;

    private ChromDatapoint[]? _currentChrom;
    public ChromatogramType ChromType { get; set; } = ChromatogramType.BPC;
    public string FilePath { get; private set; } = "Choose a datafile...";

    public DatafileController(Form1 view)
    {
        View = view;
    }
    public void OpenDatafile(string filePath)
    {
        FilePath = filePath;
        _datafile = new ThermoDatafile(filePath);

        SetChromatogram();
        PlotChromatogram();

        View.UpdateFileNameLabel(filePath);
    }
    public void AddSpectrumWindow(MassSpectrumWindow spectrumWindow)
    {
        _spectrumWindow = spectrumWindow;
    }
    private void PlotChromatogram()
    {
        if (_currentChrom == null) return;
        double[] xData = _currentChrom.Select(d => d.RetentionTime).ToArray();
        double[] yData = _currentChrom.Select(d => d.Intensity).ToArray();
        View.PlotChromatogram(xData, yData);
    }
    private void SetChromatogram()
    {
        if (_datafile != null)
        {
            switch (ChromType)
            {
                case ChromatogramType.BPC:
                    _currentChrom = _datafile.GetBasePeakChromatogram();
                    break;
                case ChromatogramType.TIC:
                    _currentChrom = _datafile.GetUnfilteredChromatogram();
                    break;
                case ChromatogramType.Unfiltered:
                    _currentChrom = _datafile.GetUnfilteredChromatogram();
                    break;
                default:
                    break;
            }
        }
    }
    public int GetNearestScanNumber(double retentionTime)
    {
        if (_currentChrom == null) return -1;
        return _currentChrom.OrderBy(d => Math.Abs(d.RetentionTime - retentionTime))
                            .First()
                            .Scan;
    }
    public double? GetBasePeakMass(int scanNumber)
    {
        return Array.Find(_currentChrom!, d => d.Scan == scanNumber)?.BasePeakMass;
    }
    public void GetMsSpectrum(int scanNumber)
    {
        (MSDatapoint[] datapoints, bool isCentroidSpectrum) = _datafile!.GetMassSpectrum(scanNumber);
        if (isCentroidSpectrum)
        {
            datapoints = BufferPeaksWithZeros(datapoints);
        }
        _spectrumWindow = new MassSpectrumWindow(this);
        _spectrumWindow.Show();
        double[] xData = datapoints.Select(d => d.Mz).ToArray();
        double[] yData = datapoints.Select(d => d.Intensity).ToArray();
        _spectrumWindow.PlotSpectrum(xData, yData);

    }
    public MSDatapoint[] BufferPeaksWithZeros(MSDatapoint[] datapoints)
    {
        List<MSDatapoint> zeroBufferedList = [];
        foreach (MSDatapoint d in datapoints)
        {
            zeroBufferedList.Add(new MSDatapoint() { Mz = d.Mz, Intensity = 0 });
            zeroBufferedList.Add(d);
            zeroBufferedList.Add(new MSDatapoint() { Mz = d.Mz, Intensity = 0 });

        }
        return zeroBufferedList.ToArray();
    }
}
public enum ChromatogramType
{
    BPC,
    TIC,
    Unfiltered
}
