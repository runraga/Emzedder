using EmzedderWinForms.Controller;
using EmzedderWinForms.Listeners;

namespace EmzedderWinForms;

public partial class Form1 : Form
{
    private readonly DatafileController _control;
    private readonly ChromPlotListeners _chromPlot;
    public Form1()
    {
        InitializeComponent();
        _control = new DatafileController(this);
        _chromPlot = new ChromPlotListeners(formsPlot1, _control);
    }

    private void chooseDatafileButton_Click(object sender, EventArgs e)
    {
        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            string filePath = openFileDialog1.FileName;
            _control.OpenDatafile(filePath);

        }
    }
    public void PlotChromatogram(double[] xData, double[] yData)
    {
        _chromPlot.PlotChromatogram(xData, yData);
    }
    public void UpdateFileNameLabel(string text)
    {
        string fileName = text.Split(@"\").Last();
        datafilePathLabel.Text = fileName;
    }



}
