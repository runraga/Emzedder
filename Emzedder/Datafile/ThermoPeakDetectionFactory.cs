using Accord.Math;
using Accord.Statistics.Models.Regression.Fitting;

namespace Emzedder.Datafile
{
    internal class ThermoPeakDetectionFactory
    {
        internal static MSDatapoint[][] DetectProfilePeaks(MSDatapoint[] spectrum)
        {
            MSDatapoint[] msOrderedDatapoints = spectrum.OrderBy(d => d.Mz).ToArray();
            List<MSDatapoint[]> peaks = [];
            List<MSDatapoint> peak = [];


            bool isAPeak = spectrum[0].Intensity == 0 ? false : true;
            for (int i = 0; i < msOrderedDatapoints.Length; i++)
            {
                double intensity = msOrderedDatapoints[i].Intensity;
                if (intensity != 0 && !isAPeak)
                {
                    peak.Add(msOrderedDatapoints[i]);
                    isAPeak = true;
                }
                else if (intensity != 0 && isAPeak)
                {
                    peak.Add(msOrderedDatapoints[i]);
                }
                else if (intensity == 0 && isAPeak)
                {
                    isAPeak = false;
                    peaks.Add(peak.ToArray());
                    peak = [];
                }
            }
            if (peak.Count != 0)
            {
                peaks.Add(peak.ToArray());
            }
            return [.. peaks];
        }
        //this calculates the weighted centroid for m/z and takes the intensity from the most intense datapoint
        internal static MSDatapoint CalcWeightedAverageCentroid(MSDatapoint[] profilePeak)
        {
            var numerator = profilePeak.Sum(p => p.Intensity * p.Mz);
            var denominator = profilePeak.Sum(p => p.Intensity);

            var centroidMz = Math.Round(numerator / denominator, 4);

            var maxIntensity = Math.Round(profilePeak.Max(p => p.Intensity), 4);
            return new MSDatapoint() { Intensity = maxIntensity, Mz = centroidMz };
        }
        //TODO: native gaussian fitting approach takes too long, need to explore options with python?
        //internal static async Task<MSDatapoint> FitGaussianAsync(MSDatapoint[] profilePeak)
        //{
        //    //TODO this is the only gaussian fit approach that has worked
        //    //need to find and implement way of doing this by passing the data to python.
        //    return await Task.Run(() =>
        //    {


        //        double[] masses = profilePeak.Select(d => d.Mz).ToArray();
        //        double[] intensities = profilePeak.Select(d => d.Intensity).ToArray();

        //        double maxIntensity = intensities.Max();
        //        double[] initialGuess = { maxIntensity, masses.Average(), .01 };

        //        var fitter = new NonlinearLeastSquares()
        //        {
        //            Function = (x, p) => x[0] * Math.Exp(-Math.Pow((p[0] - x[1]), 2) / (2 * Math.Pow(x[2], 2))),
        //            Gradient = (coefficients, input, result) =>
        //            {
        //                double amplitude = coefficients[0];
        //                double mean = coefficients[1];
        //                double stddev = coefficients[2];

        //                double x = input[0];
        //                double expPart = Math.Exp(-Math.Pow((x - mean), 2) / (2 * Math.Pow(stddev, 2)));

        //                result[0] = expPart;
        //                result[1] = amplitude * (x - mean) / Math.Pow(stddev, 2) * expPart;
        //                result[2] = amplitude * Math.Pow((x - mean), 2) / Math.Pow(stddev, 3) * expPart;
        //            },
        //            NumberOfParameters = 3,
        //            StartValues = initialGuess,

        //        };

        //        double[][] mass2d = masses.Select(m => new double[] { m }).ToArray();
        //        double[] output = intensities;

        //        double[] fittedParameters = fitter.Learn(mass2d, output).Coefficients;

        //        var centroidMz = Math.Round(fittedParameters[1], 4); // Fitted mean (centroid)
        //        var centroidIntensity = Math.Round(fittedParameters[0], 4);

        //        return new MSDatapoint() { Mz = centroidMz, Intensity = centroidIntensity };
        //    });
        //}
    }
}
