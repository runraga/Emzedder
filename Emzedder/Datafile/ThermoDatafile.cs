using ThermoFisher.CommonCore.RawFileReader;
using ThermoFisher.CommonCore.Data.Business;
using ThermoFisher.CommonCore.Data.Interfaces;
using ThermoFisher.CommonCore.Data.FilterEnums;

namespace Emzedder.Datafile
{
    public class ThermoDatafile
    {
        public string? FilePath { get; private set; }
        private IRawDataExtended RawData;
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
            var chromatogramSettings = new ChromatogramSettings()
            {
                Filter = "MS"
            };
            return GetChromatogram(chromatogramSettings);
        }
        public ChromDatapoint[] GetUnfilteredChromatogram()
        {
            var chromSettings = new ChromatogramSettings()
            {
                Filter = "",
                Trace = TraceType.TIC
            };
            return GetChromatogram(chromSettings);
        }
        public MSDatapoint[] GetMassSpectrum(int scanNumber)
        {
            CheckIsValidScanNumber(scanNumber);


            var scanFilter = RawData.GetFilterForScanNumber(scanNumber);
            var scan = Scan.FromFile(RawData, scanNumber);
            var spectrum = new ThermoSpectrum(scan.SegmentedScan, scan.CentroidScan);

            if (scanFilter.MSOrder == MSOrderType.Ms)
            {
                return spectrum.ProfileData!;
            }
            else
            {
                return spectrum.CentroidData!;
            }

        }
        //get peak labels for Spectrum
        //get charge states for Spectrum
        public double[] GetProductMassesForMsScan(int scanNumber)
        {
            CheckIsValidScanNumber(scanNumber);
            var initialCheckFilter = RawData.GetFilterForScanNumber(scanNumber);
            if (initialCheckFilter.MSOrder != MSOrderType.Ms)
                throw new ArgumentException("Scan number must be a valid MS spectrum");

            List<double> productMasses = [];
            int scanCursor = scanNumber + 1;
            bool isMsMs = true;
            while (isMsMs)
            {
                var scanFilter = RawData.GetFilterForScanNumber(scanCursor);
                if (scanFilter.MSOrder == MSOrderType.Ms)
                {
                    isMsMs = false;
                    continue;

                }
                var scanEvent = RawData.GetScanEventForScanNumber(scanCursor);
                var precursor = scanEvent.GetReaction(0).PrecursorMass;
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
            var rawData = RawFileReaderAdapter.FileFactory(filePath);
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
}
