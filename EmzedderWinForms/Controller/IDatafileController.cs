using System.ComponentModel;
using Emzedder.Datafile;

namespace EmzedderWinForms.Controller;
public interface IDatafileController : INotifyPropertyChanged
{
    ChromatogramType ChromType { get; set; }
    string FilePath { get; }
    (double[], double[])? ChromatogramDatapoints { get; }
    (double[], double[])? SpectrumDatapoints { get; }

    MSDatapoint[] BufferPeaksWithZeros(MSDatapoint[] datapoints);
    double? GetBasePeakMass(int scanNumber);
    void GetMsSpectrum(int scanNumber);
    int GetNearestScanNumber(double retentionTime);
    void OpenDatafile(string filePath);
}