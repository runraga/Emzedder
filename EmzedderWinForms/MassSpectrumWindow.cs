using EmzedderWinForms.Controller;
using EmzedderWinForms.Listeners;

namespace EmzedderWinForms;

public partial class MassSpectrumWindow : Form
{
    private readonly DatafileController _controller;
    private readonly MsPlotListeners _plotListeners;
    public MassSpectrumWindow(DatafileController dfController)
    {
        InitializeComponent();
        _controller = dfController;
        _controller.AddSpectrumWindow(this);
        _plotListeners = new MsPlotListeners(massSpecPlot, dfController);

    }
    public void PlotSpectrum(double[] xData, double[] yData)
    {
        _plotListeners.PlotSpectrum(xData, yData);
    }
}
