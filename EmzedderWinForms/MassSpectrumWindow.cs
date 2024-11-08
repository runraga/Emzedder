using EmzedderWinForms.Controller;
using EmzedderWinForms.Services;
using ScottPlot;
using ScottPlot.Plottables;
using System.ComponentModel;

namespace EmzedderWinForms;

public partial class MassSpectrumWindow : Form
{
    private readonly IDatafileController _control;

    private Scatter? _massSpecScatter;
    public MassSpectrumWindow(IDatafileController dfController)
    {
        InitializeComponent();
        _control = dfController;

        massSpecPlot.Plot.RenderManager.AxisLimitsChanged += SpectrumPlot_ZoomChanged;
        PlotConfigurationFactory.SetZoomBehaviour(massSpecPlot);
        _control.PropertyChanged += SpectrumUpdated_PlotChromatogram;

    }

    public void ShowWindow()
    {
        this.Show();
    }

    private void SpectrumPlot_ZoomChanged(Object? sender, RenderDetails e)
    {
        if (_massSpecScatter != null)
        {
            AxisLimits dataLimits = _massSpecScatter.Data.GetLimits();
            AxisLimits limits = massSpecPlot.Plot.Axes.GetLimits();
            double bottom = limits.Bottom < 0 ? 0 : limits.Bottom;
            double top = limits.Top > dataLimits.Top ? dataLimits.Top : limits.Top;
            double left = limits.Left < dataLimits.Left ? dataLimits.Left : limits.Left;
            double right = limits.Right > dataLimits.Right ? dataLimits.Right : limits.Right;
            massSpecPlot.Plot.Axes.SetLimits(left, right, bottom, top);
        }
    }
    private void SpectrumUpdated_PlotChromatogram(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_control.SpectrumDatapoints))
        {
            if (_control.SpectrumDatapoints.HasValue)
            {
                (double[] xData, double[] yData) = _control.SpectrumDatapoints.Value;
                PlotSpectrum(xData, yData);

            }
        }
    }
    public void PlotSpectrum(double[] xData, double[] yData)
    {
        massSpecPlot.Plot.Clear();
        _massSpecScatter = massSpecPlot.Plot.Add.Scatter(xData, yData);
        _massSpecScatter.LineColor = ScottPlot.Colors.Blue;
        _massSpecScatter.MarkerSize = 0;
        _massSpecScatter.LineWidth = 2;

        massSpecPlot.Plot.Axes.Bottom.Label.Text = "m/z";
        massSpecPlot.Plot.Axes.Left.Label.Text = "Intensity";

        //TODO change ticks for when using normalised intensity
        //double[] tickPositions = { 0, 50, 100 };
        //string[] tickLabels = { "0", "%", "100" };
        //_plot.Plot.Axes.Left.SetTicks(tickPositions, tickLabels);

        Func<double, string> formatter = (y) => y.ToString("G2");
        ScottPlot.TickGenerators.NumericAutomatic generator = new()
        {
            LabelFormatter = formatter
        };
        massSpecPlot.Plot.Axes.Left.TickGenerator = generator;

        massSpecPlot.Plot.HideGrid();
        massSpecPlot.Plot.Axes.SetLimits(0, xData.Max(), 0, yData.Max());
        //PlotConfigurationFactory.SetZoomBehaviour(FormsPlot);
        massSpecPlot.Refresh();
        //VerticalHair = MsWindow.Plot.Add.VerticalLine(1);
    }
}
