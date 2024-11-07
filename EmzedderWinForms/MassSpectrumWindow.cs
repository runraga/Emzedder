using EmzedderWinForms.Controller;
using EmzedderWinForms.Listeners;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EmzedderWinForms
{
    public partial class MassSpectrumWindow : Form
    {
        private DatafileController Controller;
        private MsPlotListeners PlotListeners;
        public MassSpectrumWindow(DatafileController dfController)
        {
            InitializeComponent();
            Controller = dfController;
            Controller.AddSpectrumWindow(this);
            PlotListeners = new MsPlotListeners(massSpecPlot, dfController);

        }
        public void PlotSpectrum(double[] xData, double[] yData)
        {
            PlotListeners.PlotSpectrum(xData, yData);
        }
    }
}
