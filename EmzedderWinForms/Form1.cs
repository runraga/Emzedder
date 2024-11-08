using EmzedderWinForms.Controller;
using EmzedderWinForms.Services;
using ScottPlot;
using ScottPlot.Plottables;
using System.ComponentModel;

namespace EmzedderWinForms;

public partial class Form1 : Form
{
    private readonly IDatafileController _control;

    private Scatter? _chromatogramScatter;
    private VerticalLine? _verticalHair;
    public Form1()
    {
        InitializeComponent();
        _control = new DatafileController();
        RegisterListeners();
    }
    private void RegisterListeners()
    {
        formsPlot1.Plot.RenderManager.AxisLimitsChanged += ChromPlot_ZoomChanged;
        formsPlot1.MouseDown += ChromPlot_RightClick;
        formsPlot1.MouseMove += MouseMove_ShowTooltip;
        formsPlot1.MouseMove += ChromPlot_UpdateVerticalLineTracker;
        datafilePathLabel.DataBindings.Add("Text", _control, nameof(_control.FilePath));
        _control.PropertyChanged += ChromatogramUpdated_PlotChromatogram;


        PlotConfigurationFactory.SetZoomBehaviour(formsPlot1);
    }
    private void ChooseDatafileButton_Click(object sender, EventArgs e)
    {
        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            string filePath = openFileDialog1.FileName;
            _control.OpenDatafile(filePath);

        }
    }

    private void ChromatogramUpdated_PlotChromatogram(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_control.ChromatogramDatapoints))
        {
            if (_control.ChromatogramDatapoints.HasValue)
            {
                (double[] xData, double[] yData) = _control.ChromatogramDatapoints.Value;
                PlotChromatogram(xData, yData);

            }
        }
    }
    private void PlotChromatogram(double[] xData, double[] yData)
    {
        formsPlot1.Plot.Clear();
        _chromatogramScatter = formsPlot1.Plot.Add.Scatter(xData, yData);
        _chromatogramScatter.LineColor = ScottPlot.Colors.Blue;
        _chromatogramScatter.MarkerSize = 0;
        _chromatogramScatter.LineWidth = 2;

        formsPlot1.Plot.Axes.Bottom.Label.Text = "Time";
        formsPlot1.Plot.Axes.Left.Label.Text = "Intensity";

        //TODO change ticks for when using normalised intensity
        //double[] tickPositions = { 0, 50, 100 };
        //string[] tickLabels = { "0", "%", "100" };
        //_plot.Plot.Axes.Left.SetTicks(tickPositions, tickLabels);

        Func<double, string> formatter = (y) => y.ToString("G2");
        ScottPlot.TickGenerators.NumericAutomatic generator = new()
        {
            LabelFormatter = formatter
        };
        formsPlot1.Plot.Axes.Left.TickGenerator = generator;

        formsPlot1.Plot.HideGrid();
        formsPlot1.Plot.Axes.SetLimits(0, xData.Max(), 0, yData.Max());
        //PlotConfigurationFactory.SetZoomBehaviour(formsPlot1);
        formsPlot1.Refresh();
        _verticalHair = formsPlot1.Plot.Add.VerticalLine(1);
    }

    private void ChromPlot_ZoomChanged(Object? sender, RenderDetails e)
    {
        if (_chromatogramScatter != null)
        {
            AxisLimits dataLimits = _chromatogramScatter.Data.GetLimits();
            AxisLimits limits = formsPlot1.Plot.Axes.GetLimits();
            double bottom = limits.Bottom < 0 ? 0 : limits.Bottom;
            double top = limits.Top > dataLimits.Top ? dataLimits.Top : limits.Top;
            double left = limits.Left < 0 ? 0 : limits.Left;
            double right = limits.Right > dataLimits.Right ? dataLimits.Right : limits.Right;
            formsPlot1.Plot.Axes.SetLimits(left, right, bottom, top);
        }
    }
    public void ChromPlot_RightClick(Object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {

            (double X, double _) = GetMousePosition(e);
            int nearestScan = _control.GetNearestScanNumber(X);
            _control.GetMsSpectrum(nearestScan);
        }
    }
    public (double, double) GetMousePosition(MouseEventArgs e)
    {
        Pixel mousePixel = new(e.X, e.Y);
        Coordinates mouseLocation = formsPlot1.Plot.GetCoordinates(mousePixel);
        return (mouseLocation.X, mouseLocation.Y);
        throw new NotImplementedException();
    }
    public void MouseMove_ShowTooltip(Object? sender, MouseEventArgs e)
    {
        if (_chromatogramScatter == null) return;
        (double mz, double _) = GetMousePosition(e);
        int nearestScan = _control.GetNearestScanNumber(mz);
        double basePeak = _control.GetBasePeakMass(nearestScan).GetValueOrDefault();
        formsPlot1.Plot.Clear<Annotation>();

        formsPlot1.Plot.Add.Annotation($"BP: {Math.Round(basePeak, 4)}\nScan: {nearestScan}");

        formsPlot1.Refresh();


    }
    public void ChromPlot_UpdateVerticalLineTracker(Object? sender, MouseEventArgs e)
    {
        if (_verticalHair != null)
        {

            Pixel mousePixel = new(e.X, e.Y);
            Coordinates mouseLocation = formsPlot1.Plot.GetCoordinates(mousePixel);
            _verticalHair.Position = mouseLocation.X;
            formsPlot1.Refresh();
        }
    }


}
