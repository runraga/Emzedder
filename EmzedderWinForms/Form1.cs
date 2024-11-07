using Accord;
using EmzedderWinForms.Controller;
using EmzedderWinForms.Listeners;
using ScottPlot;

namespace EmzedderWinForms
{
    public partial class Form1 : Form
    {
        private DatafileController Control { get; set; }
        private readonly ChromPlotListeners ChromPlot;

        public Form1()
        {
            InitializeComponent();
            Control = new DatafileController(this);
            ChromPlot = new ChromPlotListeners(formsPlot1, Control);
        }

        private void chooseDatafileButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                Control.OpenDatafile(filePath);

            }
        }
        public void PlotChromatogram(double[] xData, double[] yData)
        {
            ChromPlot.PlotChromatogram(xData, yData);
        }
        public void UpdateFileNameLabel(string text)
        {
            string fileName = text.Split(@"\").Last();
            datafilePathLabel.Text = fileName;
        }



    }
}
