namespace Emzedder.Datafile;

public class MSDatapoint : IEquatable<MSDatapoint>
{
    public double Mz { get; init; }
    public double Intensity { get; init; }

    public bool Equals(MSDatapoint? other)
    {
        if (other == null) return false;
        return Mz == other.Mz && Intensity == other.Intensity;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Mz, Intensity);
    }

}
