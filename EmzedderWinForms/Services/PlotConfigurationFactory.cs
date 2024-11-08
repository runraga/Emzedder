
using ScottPlot;

namespace EmzedderWinForms.Services;

public static class PlotConfigurationFactory
{
    public static void SetZoomBehaviour(IPlotControl plot)
    {
        List<ScottPlot.Interactivity.IUserActionResponse> responses = plot.UserInputProcessor.UserActionResponses;
        ScottPlot.Interactivity.IUserActionResponse? zoomResponse = plot.UserInputProcessor.UserActionResponses.Find(d => d is ScottPlot.Interactivity.UserActionResponses.MouseWheelZoom);

        plot.UserInputProcessor.UserActionResponses.Clear();

        plot.UserInputProcessor.UserActionResponses.Add(zoomResponse!);

        ScottPlot.Interactivity.MouseButton zoomRectangle = ScottPlot.Interactivity.StandardMouseButtons.Left;
        ScottPlot.Interactivity.UserActionResponses.MouseDragZoomRectangle zoomRectangleResponse = new(zoomRectangle);
        plot.UserInputProcessor.UserActionResponses.Add(zoomRectangleResponse);
    }
}
