using ThermoFisher.CommonCore.Data.Business;
using ThermoFisher.CommonCore.Data.Interfaces;

namespace Emzedder.Datafile;

internal class ChromatogramSettings : IChromatogramSettings, IChromatogramSettingsEx
{
    public double DelayInMin { get; } = 0;

    public string Filter { get; set; } = "MS";

    public double FragmentMass { get; } = 0;

    public bool IncludeReference { get; } = false;

    public int MassRangeCount { get; } = 1;

    public ThermoFisher.CommonCore.Data.Business.Range[] MassRanges { get; } = [new ThermoFisher.CommonCore.Data.Business.Range(350, 1500)];

    public TraceType Trace { get; set; } = TraceType.BasePeak;

    public string[] CompoundNames { get; set; } = [];
}
