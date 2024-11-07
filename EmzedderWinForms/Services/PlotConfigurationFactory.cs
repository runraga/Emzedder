
using ScottPlot.WinForms;

namespace EmzedderWinForms.Services
{
    public static class PlotConfigurationFactory
    {
        public static void SetZoomBehaviour(FormsPlot plot)
        {
            var responses = plot.UserInputProcessor.UserActionResponses;
            var zoomResponse = plot.UserInputProcessor.UserActionResponses.Find(d => d is ScottPlot.Interactivity.UserActionResponses.MouseWheelZoom);

            plot.UserInputProcessor.UserActionResponses.Clear();

            plot.UserInputProcessor.UserActionResponses.Add(zoomResponse!);

            var zoomRectangle = ScottPlot.Interactivity.StandardMouseButtons.Left;
            var zoomRectangleResponse = new ScottPlot.Interactivity.UserActionResponses.MouseDragZoomRectangle(zoomRectangle);
            plot.UserInputProcessor.UserActionResponses.Add(zoomRectangleResponse);
        }
    }
}
