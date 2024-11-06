using EmzedderViewer.ModelViews;
using ScottPlot;
using System.Windows;
using Emzedder.Datafile;
using EmzedderViewer.Services;
using EmzedderViewer.Listeners;

namespace EmzedderViewer.Views
{
    /// <summary>
    /// Interaction logic for MassSpectrumWindow.xaml
    /// </summary>
    public partial class MassSpectrumWindow : Window
    {
        private ChromatogramViewModel _viewModel;
        private MSPlotListeners _msPlotListeners { get; init; }


        public MassSpectrumWindow(ChromatogramViewModel chromVM, int spectrumScanNumber)
        {
            _viewModel = chromVM;
            InitializeComponent();
            _msPlotListeners = new MSPlotListeners(MassSpectrum1, _viewModel);
            _msPlotListeners.RegisterPlot(spectrumScanNumber);

        }
        private void PlotSpectrum(int spectrumNumber)
        {
            MSDatapoint[] spectrum = _viewModel.GetMsSpectrum(spectrumNumber);
            double[] xData = spectrum.Select(s => s.Mz).ToArray();
            double[] yData = spectrum.Select(s => s.Intensity).ToArray();
            MassSpectrum1.Plot.Clear();
            var spectrumPlot = MassSpectrum1.Plot.Add.Scatter(xData, yData);
            spectrumPlot.LineColor = Colors.Blue;
            spectrumPlot.MarkerSize = 0;
            //BpcChromatogram.LineColor = 
            spectrumPlot.LineWidth = 2;
            MassSpectrum1.Plot.HideGrid();
            MassSpectrum1.Plot.Axes.SetLimits(xData.Min(), xData.Max(), yData.Min(), yData.Max());
            PlotConfigurationFactory.SetZoomBehaviour(MassSpectrum1);
            MassSpectrum1.Refresh();
            //_verticalHair = MassSpectrum1.Plot.Add.VerticalLine(1);
        }
    }
}
