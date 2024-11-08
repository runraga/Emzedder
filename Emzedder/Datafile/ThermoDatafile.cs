using ThermoFisher.CommonCore.Data.Business;
using ThermoFisher.CommonCore.Data.FilterEnums;
using ThermoFisher.CommonCore.Data.Interfaces;
using ThermoFisher.CommonCore.RawFileReader;

namespace Emzedder.Datafile;

public class ThermoDatafile
{
    public bool InError { get; private set; }
    public string? FilePath { get; private set; }

    private readonly IRawDataExtended _rawData;

    public ThermoDatafile(string filePath)
    {
        if (filePath == null || !File.Exists(filePath))
        {
            throw new FileNotFoundException("Path supplied was not a valid file");
        }
        _rawData = OpenFile(filePath);
        _rawData.SelectInstrument(Device.MS, 1);

    }

    public ChromDatapoint[] GetBasePeakChromatogram()
    {
        ChromatogramSettings chromatogramSettings = new()
        {
            Filter = "MS"
        };
        return GetChromatogram(chromatogramSettings);
    }
    public ChromDatapoint[] GetUnfilteredChromatogram()
    {
        ChromatogramSettings chromSettings = new()
        {
            Filter = "",
            Trace = TraceType.TIC
        };
        return GetChromatogram(chromSettings);
    }
    public (MSDatapoint[], bool) GetMassSpectrum(int scanNumber)
    {
        CheckIsValidScanNumber(scanNumber);


        IScanFilter scanFilter = _rawData.GetFilterForScanNumber(scanNumber);
        Scan scan = Scan.FromFile(_rawData, scanNumber);
        ThermoSpectrum spectrum = new(scan.SegmentedScan, scan.CentroidScan)
        {
            MSOrder = scanFilter.MSOrder
        };
        if (spectrum.MSOrder == MSOrderType.Ms)
        {
            return (spectrum.ProfileData!, false);
        }
        else
        {
            return (spectrum.CentroidData!, true);
        }

    }
    //get peak labels for Spectrum
    //get charge states for Spectrum
    public double[] GetProductMassesForMsScan(int scanNumber)
    {
        CheckIsValidScanNumber(scanNumber);
        IScanFilter initialCheckFilter = _rawData.GetFilterForScanNumber(scanNumber);
        if (initialCheckFilter.MSOrder != MSOrderType.Ms)
            throw new ArgumentException("Scan number must be a valid MS spectrum");

        List<double> productMasses = [];
        int scanCursor = scanNumber + 1;
        bool isMsMs = true;
        while (isMsMs)
        {
            IScanFilter scanFilter = _rawData.GetFilterForScanNumber(scanCursor);
            if (scanFilter.MSOrder == MSOrderType.Ms)
            {
                isMsMs = false;
                continue;

            }
            IScanEvent scanEvent = _rawData.GetScanEventForScanNumber(scanCursor);
            double precursor = scanEvent.GetReaction(0).PrecursorMass;
            productMasses.Add(Math.Round(precursor, 4));
            scanCursor++;

        }
        return [.. productMasses];
    }

    private ChromDatapoint[] GetChromatogram(ChromatogramSettings chromSettings)
    {
        int FirstScan = _rawData.RunHeader.FirstSpectrum;
        int FinalScan = _rawData.RunHeader.LastSpectrum;


        IChromatogramDataPlus chromData = _rawData.GetChromatogramDataEx([chromSettings], FirstScan, FinalScan);
        ChromatogramSignal chromTrace = ChromatogramSignal.FromChromatogramData(chromData)[0];
        List<ChromDatapoint> datapoints = [];
        for (int i = 0; i < chromTrace.Length; i++)
        {
            datapoints.Add(new ChromDatapoint()
            {
                Intensity = chromTrace.Intensities[i],
                RetentionTime = chromTrace.Times[i],
                BasePeakMass = chromTrace.SignalBasePeakMasses[i],
                Scan = chromTrace.Scans[i]

            });
        }
        return datapoints.ToArray();
    }
    private IRawDataExtended OpenFile(string filePath)
    {
        IRawDataExtended rawData = RawFileReaderAdapter.FileFactory(filePath);
        try
        {
            FilePath = filePath;
        }
        catch
        {
            throw new ArgumentOutOfRangeException();
        }
        if (rawData.IsError || rawData.InAcquisition)
        {
            InError = false;
        }

        return rawData;
    }
    private void CheckIsValidScanNumber(int scanNumber)
    {
        if (scanNumber < _rawData.RunHeader.FirstSpectrum || scanNumber > _rawData.RunHeader.LastSpectrum)
        {
            throw new ArgumentException("That is not a valid scan number for this file");
        }

    }

}
