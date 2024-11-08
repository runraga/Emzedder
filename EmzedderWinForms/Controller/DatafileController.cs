using Emzedder.Datafile;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EmzedderWinForms.Controller;

public class DatafileController : IDatafileController
{

    private ThermoDatafile? _datafile;
    private ChromDatapoint[]? _currentChrom;

    public ChromatogramType ChromType { get; set; } = ChromatogramType.BPC;
    public event PropertyChangedEventHandler? PropertyChanged;


    private string _filePath = "Choose a datafile...";
    public string FilePath
    {
        get => _filePath;
        private set
        {
            if (_filePath != value)
            {
                _filePath = value;
                OnPropertyChanged();
            }
        }
    }
    private (double[], double[])? _chromatogramDatapoints;
    public (double[], double[])? ChromatogramDatapoints
    {
        get => _chromatogramDatapoints;
        private set
        {
            if (_chromatogramDatapoints != value)
            {
                _chromatogramDatapoints = value;
                OnPropertyChanged();
            }
        }
    }
    private (double[], double[])? _spectrumDatapoints;
    public (double[], double[])? SpectrumDatapoints
    {
        get => _spectrumDatapoints;
        private set
        {
            if (_spectrumDatapoints != value)
            {
                _spectrumDatapoints = value;
                OnPropertyChanged();
            }
        }
    }


    public void OpenDatafile(string filePath)
    {
        FilePath = filePath;
        _datafile = new ThermoDatafile(filePath);

        SetChromatogram();
        PlotChromatogram();


    }
    private void PlotChromatogram()
    {
        if (_currentChrom == null) return;
        double[] xData = _currentChrom.Select(d => d.RetentionTime).ToArray();
        double[] yData = _currentChrom.Select(d => d.Intensity).ToArray();
        ChromatogramDatapoints = (xData, yData);
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
        MassSpectrumWindow spectrumWindow = new MassSpectrumWindow(this);
        spectrumWindow.ShowWindow();
        double[] xData = datapoints.Select(d => d.Mz).ToArray();
        double[] yData = datapoints.Select(d => d.Intensity).ToArray();
        SpectrumDatapoints = (xData, yData);

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
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
public enum ChromatogramType
{
    BPC,
    TIC,
    Unfiltered
}
