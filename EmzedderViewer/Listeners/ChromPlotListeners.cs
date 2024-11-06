using ScottPlot.Plottables;
using ScottPlot;
using System.Windows.Input;
using ScottPlot.WPF;
using EmzedderViewer.ModelViews;
using System.ComponentModel;
using Point = System.Windows.Point;
using EmzedderViewer.Views;


namespace EmzedderViewer.Listeners
{
    /// <summary>
    /// Collection of Listeners concerned with updating behaviour and appearence of WpfPlot in the ScottPlot package
    /// </summary>
    public class ChromPlotListeners
    {
        private readonly WpfPlot _plot;
        private Scatter? _chrom;
        private VerticalLine? _verticalHair;
        private ChromatogramViewModel _chromVM;


        public ChromPlotListeners(WpfPlot plot, ChromatogramViewModel chromVM)
        {
            _plot = plot;
            _chromVM = chromVM;
            _chromVM.PropertyChanged += ChromVM_ChromatogramChanged;
            _plot.Plot.RenderManager.AxisLimitsChanged += ChromPlot_ZoomChanged;
            _plot.MouseMove += ChromPlot_UpdateVerticalLineTracker;
            _plot.MouseRightButtonDown += ChromPlot_RightClick;
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
            Console.WriteLine($"Nearest scan: {nearestScan}");
            MassSpectrumWindow msWindow = new MassSpectrumWindow(_chromVM, nearestScan);
            msWindow.Show();
        }
        /// <summary>
        /// ZoomChanged listener ensures that zooming out won't go beyond the Limits of the current data
        /// </summary>
        private void ChromPlot_ZoomChanged(Object? sender, RenderDetails e)
        {
            if (_chrom != null)
            {

                var dataLimits = _chrom.Data.GetLimits();
                AxisLimits limits = _plot.Plot.Axes.GetLimits();
                double bottom = limits.Bottom < 0 ? 0 : limits.Bottom;
                double top = limits.Top > dataLimits.Top ? dataLimits.Top : limits.Top;
                double left = limits.Left < 0 ? 0 : limits.Left;
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
                _plot.Plot.Clear();
                _chrom = _plot.Plot.Add.Scatter(xData, yData);
                _chrom.LineColor = Colors.Blue;
                _chrom.MarkerSize = 0;
                _chrom.LineWidth = 2;
                _plot.Plot.HideGrid();
                _plot.Plot.Axes.SetLimits(0, xData.Max(), 0, yData.Max());
                SetupPlotMouseActions();
                _plot.Refresh();
                _verticalHair = _plot.Plot.Add.VerticalLine(1);
            }

        }
        /// <summary>
        /// Setsup mouse behaviour on new plots.
        /// </summary>
        private void SetupPlotMouseActions()
        {
            var responses = _plot.UserInputProcessor.UserActionResponses;
            var zoomResponse = _plot.UserInputProcessor.UserActionResponses.Find(d => d is ScottPlot.Interactivity.UserActionResponses.MouseWheelZoom);

            _plot.UserInputProcessor.UserActionResponses.Clear();

            _plot.UserInputProcessor.UserActionResponses.Add(zoomResponse!);

            var zoomRectangle = ScottPlot.Interactivity.StandardMouseButtons.Left;
            var zoomRectangleResponse = new ScottPlot.Interactivity.UserActionResponses.MouseDragZoomRectangle(zoomRectangle);
            _plot.UserInputProcessor.UserActionResponses.Add(zoomRectangleResponse);
        }
    }
}
