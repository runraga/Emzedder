using EmzedderViewer.ModelViews;
using ScottPlot;
using System.Windows;
using Emzedder.Datafile;

namespace EmzedderViewer.Views
{
    /// <summary>
    /// Interaction logic for MassSpectrumWindow.xaml
    /// </summary>
    public partial class MassSpectrumWindow : Window
    {
        private ChromatogramViewModel _viewModel;

        public MassSpectrumWindow(ChromatogramViewModel chromVM, int spectrumScanNumber)
        {
            _viewModel = chromVM;
            InitializeComponent();
            PlotSpectrum(spectrumScanNumber);

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
            MassSpectrum1.Plot.Axes.SetLimits(0, xData.Max(), 0, yData.Max());
            //SetupPlotMouseActions();
            MassSpectrum1.Refresh();
            //_verticalHair = MassSpectrum1.Plot.Add.VerticalLine(1);
        }
    }
}
