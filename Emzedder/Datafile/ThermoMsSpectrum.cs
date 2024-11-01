﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThermoFisher.CommonCore.Data.Business;

namespace Emzedder.Datafile
{
    internal class ThermoSpectrum
    {
        public MSDatapoint[] ProfileData { get; private set; }
        public MSDatapoint[] CentroidData { get; private set; }

        public ThermoSpectrum(SegmentedScan scan)
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
        public ThermoSpectrum(CentroidStream stream)
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

    }




}
