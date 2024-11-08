using Emzedder.Common;

namespace Emzedder.Datafile;


public interface IDatafile
{
    bool InError { get; }
    public MSDatapoint[] GetTIC();
    public MSDatapoint[] GetBPC();
    public MSDatapoint[] GetXIC(double mass, double tolerance, MSUnits toleranceUnit);
}
