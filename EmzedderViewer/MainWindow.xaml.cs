using System.Windows;

using Emzedder.Datafile;

namespace EmzedderViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var datafile = new ThermoDatafile(@"C:\Users\runra\source\repos\Emzedder\Emzedder\TestRawDatafiles\1.raw");

            var bpc = datafile.GetBasePeakChromatogram();

            double[] dataX = bpc.Select(d => d.RetentionTime).ToArray();
            double[] dataY = bpc.Select(d => d.Intensity).ToArray();
            WpfPlot1.Plot.Add.Scatter(dataX, dataY);
            WpfPlot1.Refresh();

        }
    }


}