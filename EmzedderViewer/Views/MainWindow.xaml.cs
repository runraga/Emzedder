using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Emzedder.Datafile;
using EmzedderViewer.Listeners;
using EmzedderViewer.ModelViews;
using ScottPlot.Plottables;

namespace EmzedderViewer.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly ChromatogramViewModel ChromVM = new ChromatogramViewModel();
        private DatafileNameLabelListeners DatafileNameLabelListeners { get; init; }
        private ChromPlotListeners ChromPlotListeners { get; init; }


        public MainWindow()
        {
            DataContext = ChromVM;
            InitializeComponent();
            DatafileNameLabelListeners = new DatafileNameLabelListeners(DatafileLabel, ChromVM);
            ChromPlotListeners = new ChromPlotListeners(WpfPlot1, ChromVM);

        }



    }


}