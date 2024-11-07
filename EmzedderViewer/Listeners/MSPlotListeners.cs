using ScottPlot.Plottables;
using ScottPlot;
using System.Windows.Input;
using ScottPlot.WPF;
using EmzedderViewer.ModelViews;
using Emzedder.Datafile;

using System.ComponentModel;
using Point = System.Windows.Point;
using EmzedderViewer.Views;
using EmzedderViewer.Services;
using Accord;


namespace EmzedderViewer.Listeners
{
    /// <summary>
    /// Collection of Listeners concerned with updating behaviour and appearence of WpfPlot in the ScottPlot package
    /// </summary>
    public class MSPlotListeners
    {
        private readonly WpfPlot _plot;
        private Scatter? _spectrumSeries;
        private VerticalLine? _verticalHair;
        private ChromatogramViewModel _chromVM;
        private MSDatapoint[] _spectrum;


        public MSPlotListeners(WpfPlot plot, ChromatogramViewModel chromVM)
        {
            _plot = plot;
            _chromVM = chromVM;
            _plot.Plot.RenderManager.AxisLimitsChanged += ChromPlot_ZoomChanged;
        }
        public void RegisterPlot(int scanNumber)
        {
            _spectrum = _chromVM.GetMsSpectrum(scanNumber);
            double[] xData = _spectrum.Select(d => d.Mz).ToArray();
            double[] yData = _spectrum.Select(d => d.Intensity).ToArray();
            PlotSpectrum(xData, yData);

        }
        /// <summary>
        /// Detects right mouse clicks on plot and shows nearest spectrum in X direction.
        /// </summary>
        private void ChromPlot_RightClick(Object? sender, MouseEventArgs e)
        {
            Point mousePoint = e.GetPosition(_plot);
            Pixel mousePixel = new(mousePoint.X * _plot.DisplayScale, mousePoint.Y * _plot.DisplayScale);
            Coordinates mouseLocation = _plot.Plot.GetCoordinates(mousePixel);
            int nearestScan = _chromVM.GetNearestScanNumber(mouseLocation.X);
            MassSpectrumWindow msWindow = new MassSpectrumWindow(_chromVM, nearestScan);
            msWindow.Show();
        }
        /// <summary>
        /// ZoomChanged listener ensures that zooming out won't go beyond the Limits of the current data
        /// </summary>
        private void ChromPlot_ZoomChanged(Object? sender, RenderDetails e)
        {
            if (_spectrumSeries != null)
            {
                var dataLimits = _spectrumSeries.Data.GetLimits();
                AxisLimits limits = _plot.Plot.Axes.GetLimits();
                double bottom = limits.Bottom < 0 ? 0 : limits.Bottom;
                double top = limits.Top > dataLimits.Top ? dataLimits.Top : limits.Top;
                double left = limits.Left < dataLimits.Left ? dataLimits.Left : limits.Left;
                double right = limits.Right > dataLimits.Right ? dataLimits.Right : limits.Right;
                _plot.Plot.Axes.SetLimits(left, right, bottom, top);
            }
        }
        /// <summary>
        /// Listener to update the position of the vertical line based on mouse location
        /// </summary>
        private void ChromPlot_UpdateVerticalLineTracker(Object? sender, MouseEventArgs e)

        {
            if (_verticalHair != null)
            {
                Point mousePoint = e.GetPosition(_plot);
                Pixel mousePixel = new(mousePoint.X * _plot.DisplayScale, mousePoint.Y * _plot.DisplayScale);
                Coordinates mouseLocation = _plot.Plot.GetCoordinates(mousePixel);
                _verticalHair.Position = mouseLocation.X;
                _plot.Refresh();
            }
        }
        /// <summary>
        /// Updates the plot to display a new chromatogram when it is detected
        /// </summary>
        private void ChromVM_ChromatogramChanged(Object? sender, PropertyChangedEventArgs e)
        {
            var vm = sender as ChromatogramViewModel;
            if (vm == null) return;
            if (e.PropertyName == nameof(ChromatogramViewModel.CurrentChrom))
            {
                double[] xData = vm.GetCurrentChromXData();
                double[] yData = vm.GetCurrentChromYData();
                PlotSpectrum(xData, yData);


            }
        }
        private void PlotSpectrum(double[] xData, double[] yData)
        {
            _plot.Plot.Clear();

            _spectrumSeries = _plot.Plot.Add.Scatter(xData, yData);

            _spectrumSeries.LineColor = Colors.Blue;
            _spectrumSeries.MarkerSize = 0;
            _spectrumSeries.LineWidth = 2;

            _plot.Plot.Axes.Bottom.Label.Text = "m/z";
            _plot.Plot.Axes.Left.Label.Text = "Intensity";

            //TODO change ticks for when using normalised intensity
            //double[] tickPositions = { 0, 50, 100 };
            //string[] tickLabels = { "0", "%", "100" };
            //_plot.Plot.Axes.Left.SetTicks(tickPositions, tickLabels);

            Func<double, string> formatter = (y) => y.ToString("G2");
            ScottPlot.TickGenerators.NumericAutomatic generator = new()
            {
                LabelFormatter = formatter
            };
            _plot.Plot.Axes.Left.TickGenerator = generator;

            _plot.Plot.HideGrid();

            _plot.Plot.Axes.SetLimits(xData.Min(), xData.Max(), yData.Min(), yData.Max());
            PlotConfigurationFactory.SetZoomBehaviour(_plot);
            _plot.Refresh();
            //_verticalHair = _plot.Plot.Add.VerticalLine(1);
        }

    }
}
