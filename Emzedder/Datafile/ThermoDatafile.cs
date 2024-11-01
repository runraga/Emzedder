using Emzedder.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ThermoFisher.CommonCore.RawFileReader;
using ThermoFisher.CommonCore.Data.Business;
using ThermoFisher.CommonCore.Data.Interfaces;

namespace Emzedder.Datafile
{
    public class ThermoDatafile : IDatafile
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
                    BasePeakMass = chromTrace.SignalBasePeakMasses[i]
                });
            }
            return datapoints.ToArray();
        }
        private IRawDataExtended OpenFile(string filePath)
        {
            var rawData = RawFileReaderAdapter.FileFactory(filePath);
            try
            {
                FilePath = rawData.Path;
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
        public MSDatapoint[] GetBPC()
        {
            throw new NotImplementedException();
        }

        public MSDatapoint[] GetTIC()
        {
            throw new NotImplementedException();
        }

        public MSDatapoint[] GetXIC(double mass, double tolerance, MSUnits toleranceUnit)
        {
            throw new NotImplementedException();
        }
    }
}
