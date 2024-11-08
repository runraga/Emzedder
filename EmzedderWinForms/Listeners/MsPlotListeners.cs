using EmzedderWinForms.Controller;
using EmzedderWinForms.Services;
using ScottPlot;
using ScottPlot.Plottables;
using ScottPlot.WinForms;

namespace EmzedderWinForms.Listeners;


public class MsPlotListeners
{
    private readonly FormsPlot _msPlot;
    private readonly DatafileController _dfController;

    private Scatter? _msScatterPlot;
    public MsPlotListeners(FormsPlot msWindow, DatafileController dfController)
    {
        _msPlot = msWindow;
        _dfController = dfController;
        _msPlot.Plot.RenderManager.AxisLimitsChanged += ChromPlot_ZoomChanged;
        PlotConfigurationFactory.SetZoomBehaviour(_msPlot);

    }
    private void ChromPlot_ZoomChanged(Object? sender, RenderDetails e)
    {
        if (_msScatterPlot != null)
        {
            AxisLimits dataLimits = _msScatterPlot.Data.GetLimits();
            AxisLimits limits = _msPlot.Plot.Axes.GetLimits();
            double bottom = limits.Bottom < 0 ? 0 : limits.Bottom;
            double top = limits.Top > dataLimits.Top ? dataLimits.Top : limits.Top;
            double left = limits.Left < dataLimits.Left ? dataLimits.Left : limits.Left;
            double right = limits.Right > dataLimits.Right ? dataLimits.Right : limits.Right;
            _msPlot.Plot.Axes.SetLimits(left, right, bottom, top);
        }
    }
    public void PlotSpectrum(double[] xData, double[] yData)
    {
        _msPlot.Plot.Clear();
        _msScatterPlot = _msPlot.Plot.Add.Scatter(xData, yData);
        _msScatterPlot.LineColor = ScottPlot.Colors.Blue;
        _msScatterPlot.MarkerSize = 0;
        _msScatterPlot.LineWidth = 2;

        _msPlot.Plot.Axes.Bottom.Label.Text = "m/z";
        _msPlot.Plot.Axes.Left.Label.Text = "Intensity";

        //TODO change ticks for when using normalised intensity
        //double[] tickPositions = { 0, 50, 100 };
        //string[] tickLabels = { "0", "%", "100" };
        //_plot.Plot.Axes.Left.SetTicks(tickPositions, tickLabels);

        Func<double, string> formatter = (y) => y.ToString("G2");
        ScottPlot.TickGenerators.NumericAutomatic generator = new()
        {
            LabelFormatter = formatter
        };
        _msPlot.Plot.Axes.Left.TickGenerator = generator;

        _msPlot.Plot.HideGrid();
        _msPlot.Plot.Axes.SetLimits(0, xData.Max(), 0, yData.Max());
        //PlotConfigurationFactory.SetZoomBehaviour(FormsPlot);
        _msPlot.Refresh();
        //VerticalHair = MsWindow.Plot.Add.VerticalLine(1);
    }
}
