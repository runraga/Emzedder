using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThermoFisher.CommonCore.Data.Business;

namespace Emzedder.Datafile
{
    internal class ThermoSpectrum
    {
        public MSDatapoint[]? ProfileData { get; private set; }
        public MSDatapoint[]? CentroidData { get; private set; }

        public ThermoSpectrum(SegmentedScan scan)
        {
            PopulateProfileFromSegmentScan(scan);
            ConvertToCentroid();
        }
        internal void ConvertToCentroid()
        {
            MSDatapoint[][] peaks = ThermoPeakDetectionFactory.DetectProfilePeaks(ProfileData!);
            CentroidData = peaks.Select(profilePeak =>
                ThermoPeakDetectionFactory.CalcWeightedAverageCentroid(profilePeak)
            ).ToArray();


        }
        public ThermoSpectrum(CentroidStream stream)
        {
            PopulateCentroidsFromStream(stream);
        }
        public ThermoSpectrum(SegmentedScan scan, CentroidStream stream)
        {
            PopulateCentroidsFromStream(stream);
            PopulateProfileFromSegmentScan(scan);
        }
        private void PopulateCentroidsFromStream(CentroidStream stream)
        {
            List<MSDatapoint> centroidData = [];

            for (int i = 0; i < stream.Length; i++)
            {
                MSDatapoint current = new MSDatapoint()
                {
                    Intensity = Math.Round(stream.Intensities[i], 4),
                    Mz = Math.Round(stream.Masses[i], 4)
                };
                centroidData.Add(current);
            }
            CentroidData = centroidData.ToArray();
        }
        private void PopulateProfileFromSegmentScan(SegmentedScan scan)
        {
            List<MSDatapoint> profileData = [];
            for (int i = 0; i < scan.PositionCount; i++)
            {

                profileData.Add(new MSDatapoint()
                {
                    Intensity = Math.Round(scan.Intensities[i], 4),
                    Mz = Math.Round(scan.Positions[i], 4)
                });
            }
            ProfileData = profileData.ToArray();
        }

    }




}
