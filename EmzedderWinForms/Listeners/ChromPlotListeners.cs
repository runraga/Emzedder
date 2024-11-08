using EmzedderWinForms.Controller;
using EmzedderWinForms.Services;
using ScottPlot;
using ScottPlot.Plottables;
using ScottPlot.WinForms;

namespace EmzedderWinForms.Listeners;

public class ChromPlotListeners
{
    private readonly FormsPlot FormsPlot;
    private readonly DatafileController DfController;

    private Scatter _chromScatterPlot;
    private VerticalLine? _verticalHair;

    public ChromPlotListeners(FormsPlot formsPlot, DatafileController dfController)
    {
        FormsPlot = formsPlot;
        DfController = dfController;
        FormsPlot.Plot.RenderManager.AxisLimitsChanged += ChromPlot_ZoomChanged;
        FormsPlot.MouseDown += ChromPlot_RightClick;
        FormsPlot.MouseMove += MouseMove_ShowTooltip;
        FormsPlot.MouseMove += ChromPlot_UpdateVerticalLineTracker;
        PlotConfigurationFactory.SetZoomBehaviour(FormsPlot);
    }
    public void PlotChromatogram(double[] xData, double[] yData)
    {
        FormsPlot.Plot.Clear();
        _chromScatterPlot = FormsPlot.Plot.Add.Scatter(xData, yData);
        _chromScatterPlot.LineColor = ScottPlot.Colors.Blue;
        _chromScatterPlot.MarkerSize = 0;
        _chromScatterPlot.LineWidth = 2;

        FormsPlot.Plot.Axes.Bottom.Label.Text = "Time";
        FormsPlot.Plot.Axes.Left.Label.Text = "Intensity";

        //TODO change ticks for when using normalised intensity
        //double[] tickPositions = { 0, 50, 100 };
        //string[] tickLabels = { "0", "%", "100" };
        //_plot.Plot.Axes.Left.SetTicks(tickPositions, tickLabels);

        Func<double, string> formatter = (y) => y.ToString("G2");
        ScottPlot.TickGenerators.NumericAutomatic generator = new()
        {
            LabelFormatter = formatter
        };
        FormsPlot.Plot.Axes.Left.TickGenerator = generator;

        FormsPlot.Plot.HideGrid();
        FormsPlot.Plot.Axes.SetLimits(0, xData.Max(), 0, yData.Max());
        //PlotConfigurationFactory.SetZoomBehaviour(FormsPlot);
        FormsPlot.Refresh();
        _verticalHair = FormsPlot.Plot.Add.VerticalLine(1);
    }

    private void ChromPlot_ZoomChanged(Object? sender, RenderDetails e)
    {
        if (_chromScatterPlot != null)
        {
            AxisLimits dataLimits = _chromScatterPlot.Data.GetLimits();
            AxisLimits limits = FormsPlot.Plot.Axes.GetLimits();
            double bottom = limits.Bottom < 0 ? 0 : limits.Bottom;
            double top = limits.Top > dataLimits.Top ? dataLimits.Top : limits.Top;
            double left = limits.Left < 0 ? 0 : limits.Left;
            double right = limits.Right > dataLimits.Right ? dataLimits.Right : limits.Right;
            FormsPlot.Plot.Axes.SetLimits(left, right, bottom, top);
        }
    }
    private void ChromPlot_RightClick(Object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {

            (double X, double _) = GetMousePosition(e);
            int nearestScan = DfController.GetNearestScanNumber(X);
            DfController.GetMsSpectrum(nearestScan);
        }
    }
    private (double, double) GetMousePosition(MouseEventArgs e)
    {
        Pixel mousePixel = new(e.X, e.Y);
        Coordinates mouseLocation = FormsPlot.Plot.GetCoordinates(mousePixel);
        return (mouseLocation.X, mouseLocation.Y);
    }
    private void MouseMove_ShowTooltip(Object? sender, MouseEventArgs e)
    {
        if (_chromScatterPlot == null) return;
        (double mz, double _) = GetMousePosition(e);
        int nearestScan = DfController.GetNearestScanNumber(mz);
        double basePeak = DfController.GetBasePeakMass(nearestScan).GetValueOrDefault();
        FormsPlot.Plot.Clear<Annotation>();

        Annotation anno = FormsPlot.Plot.Add.Annotation($"BP: {Math.Round(basePeak, 4)}\nScan: {nearestScan}");

        FormsPlot.Refresh();


    }
    private void ChromPlot_UpdateVerticalLineTracker(Object? sender, MouseEventArgs e)

    {
        if (_verticalHair != null)
        {

            Pixel mousePixel = new(e.X, e.Y);
            Coordinates mouseLocation = FormsPlot.Plot.GetCoordinates(mousePixel);
            _verticalHair.Position = mouseLocation.X;
            FormsPlot.Refresh();
        }
    }
}
