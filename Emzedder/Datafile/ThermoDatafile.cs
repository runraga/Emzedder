using ThermoFisher.CommonCore.Data.Business;
using ThermoFisher.CommonCore.Data.FilterEnums;
using ThermoFisher.CommonCore.Data.Interfaces;
using ThermoFisher.CommonCore.RawFileReader;

namespace Emzedder.Datafile;

public class ThermoDatafile
{
    public string? FilePath { get; private set; }
    private readonly IRawDataExtended RawData;
    public bool InError { get; private set; }

    public ThermoDatafile(string filePath)
    {
        if (filePath == null || !File.Exists(filePath))
        {
            throw new FileNotFoundException("Path supplied was not a valid file");
        }
        RawData = OpenFile(filePath);
        RawData.SelectInstrument(Device.MS, 1);

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


        IScanFilter scanFilter = RawData.GetFilterForScanNumber(scanNumber);
        Scan scan = Scan.FromFile(RawData, scanNumber);
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
        IScanFilter initialCheckFilter = RawData.GetFilterForScanNumber(scanNumber);
        if (initialCheckFilter.MSOrder != MSOrderType.Ms)
            throw new ArgumentException("Scan number must be a valid MS spectrum");

        List<double> productMasses = [];
        int scanCursor = scanNumber + 1;
        bool isMsMs = true;
        while (isMsMs)
        {
            IScanFilter scanFilter = RawData.GetFilterForScanNumber(scanCursor);
            if (scanFilter.MSOrder == MSOrderType.Ms)
            {
                isMsMs = false;
                continue;

            }
            IScanEvent scanEvent = RawData.GetScanEventForScanNumber(scanCursor);
            double precursor = scanEvent.GetReaction(0).PrecursorMass;
            productMasses.Add(Math.Round(precursor, 4));
            scanCursor++;

        }
        return [.. productMasses];
    }

    private ChromDatapoint[] GetChromatogram(ChromatogramSettings chromSettings)
    {
        int FirstScan = RawData.RunHeader.FirstSpectrum;
        int FinalScan = RawData.RunHeader.LastSpectrum;


        IChromatogramDataPlus chromData = RawData.GetChromatogramDataEx([chromSettings], FirstScan, FinalScan);
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
        if (scanNumber < RawData.RunHeader.FirstSpectrum || scanNumber > RawData.RunHeader.LastSpectrum)
        {
            throw new ArgumentException("That is not a valid scan number for this file");
        }

    }

}
