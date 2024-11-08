namespace Emzedder.Datafile;

internal class IsotopeDetectionService
{
    private readonly double ProtonMass = 1.003;

    public IsotopeGroup[]? IsotopeGroups { get; internal set; }
    private MSDatapoint[] CentroidPeaks { get; init; }

    private readonly HashSet<MSDatapoint> PeaksInGroup = [];

    public IsotopeDetectionService(MSDatapoint[] centroidPeaks)
    {
        CentroidPeaks = centroidPeaks;
    }
    internal IsotopeGroup[] GroupIntoIsotopeEnvelopes()
    {
        List<IsotopeGroup> isotopeGroups = [];
        MSDatapoint[] intensityOrdered = CentroidPeaks.OrderByDescending(p => p.Intensity).ToArray();
        foreach (MSDatapoint p in intensityOrdered)
        {
            if (PeaksInGroup.Contains(p))
            {
                continue;
            }
            int startingChargeState = 0;
            IsotopeGroup group = FindIsotopes(p, startingChargeState);
            if (PeaksInGroup.Contains(p))
            {
                IsotopeGroup groupDown = FindIsotopes(p, group.Charge, true);
                group.AddGroup(groupDown);
            }

            if (!group.IsEmpty())
            {

                isotopeGroups.Add(group);
            }

        }

        return [.. isotopeGroups];
    }
    public IsotopeGroup FindIsotopes(MSDatapoint currentPeak,
                                 int isotopeCharge,
                                 bool checkDown = false,
                                 MSDatapoint[]? overrideNeighbours = null)
    {

        MSDatapoint[] neighbours;
        if (overrideNeighbours is not null)
        {
            neighbours = overrideNeighbours;
        }
        else
        {
            neighbours = FindPeakNeighbours(currentPeak, checkDown);
        }

        if (neighbours.Length < 2 || (currentPeak.Intensity < 9e5))
        {
            if (isotopeCharge == 0)
            {
                return new IsotopeGroup(isotopeCharge);
            }
            else
            {
                IsotopeGroup isotopeGroup = new(isotopeCharge);
                isotopeGroup.AddIsotope(currentPeak);
                return isotopeGroup;
            }
        }



        for (int i = 1; i < neighbours.Length; i++)
        {
            if (PeaksInGroup.Contains(neighbours[i]))
            {
                continue;
            }
            MSDatapoint neighbour = neighbours[i];
            double currentPeakMz = currentPeak.Mz;
            double neighbourMz = neighbour.Mz;
            double diff = Math.Abs(neighbourMz - currentPeakMz);
            int charge = (int)Math.Round((ProtonMass / diff), 0);
            double chargeSpacing = (ProtonMass / diff) / charge;

            if (isotopeCharge > 0)
            {
                if (charge == isotopeCharge && chargeSpacing > 0.95 && chargeSpacing < 1.05)
                {
                    PeaksInGroup.Add(currentPeak);
                    IsotopeGroup isotopeGroup = FindIsotopes(neighbour, isotopeCharge, checkDown);
                    isotopeGroup.AddIsotope(currentPeak);
                    return isotopeGroup;
                }
                else
                {
                    continue;
                }
            }
            else
            {

                if (chargeSpacing > 0.99 && chargeSpacing < 1.01)
                {

                    IsotopeGroup group = FindIsotopes(neighbour, charge, checkDown);
                    //if only two isotopes incl. currentPeak - need to check for down peaks first
                    //then if none, check for other spaceings
                    //if ambiguous go for lower charge state
                    if (group.GetIsotopeGroup().Length == 1)
                    {
                        PeaksInGroup.Add(currentPeak);
                        IsotopeGroup groupDown = FindIsotopes(currentPeak, charge, true);
                        foreach (MSDatapoint centroidPeak in groupDown.GetIsotopeGroup())
                        {
                            //This is only a check so remove any peaks from the check HashSet
                            PeaksInGroup.Remove(centroidPeak);
                        }
                        if (groupDown.GetIsotopeGroup().Length == 0)
                        {
                            //remove current neighbour peak and try again
                            PeaksInGroup.Remove(currentPeak);
                            //PeaksInGroup.Contains(group.GetIsotopeGroup()[0]);
                            MSDatapoint[] neighbourRemoved = new MSDatapoint[neighbours.Length - 1];
                            Array.Copy(neighbours, 0, neighbourRemoved, 0, i);
                            Array.Copy(neighbours, i + 1, neighbourRemoved, i, neighbours.Length - i - 1);

                            IsotopeGroup removedNeighbourSearch = FindIsotopes(currentPeak, 0, checkDown, neighbourRemoved);


                            //if removed neighbour search is zero - return current
                            //if equal to current+1 return removed neigbour serach (i.e.lower charge state)
                            if (removedNeighbourSearch.GetIsotopeGroup().Length >= group.GetIsotopeGroup().Length + 1)
                            {
                                return removedNeighbourSearch;
                            }
                            else
                            {
                                PeaksInGroup.Add(currentPeak);
                                group.AddIsotope(currentPeak);
                                return group;
                            }


                        }
                        else
                        {
                            PeaksInGroup.Add(currentPeak);
                            group.AddIsotope(currentPeak);
                            return group;
                        }

                    }
                    else if (group.GetIsotopeGroup().Length > 1)
                    {
                        PeaksInGroup.Add(currentPeak);
                        group.AddIsotope(currentPeak);
                        return group;
                    }
                    continue;
                }
                else
                {
                    continue;
                }
            }

        }
        if (isotopeCharge == 0)
        {
            return new IsotopeGroup(isotopeCharge);
        }
        PeaksInGroup.Add(currentPeak);
        IsotopeGroup noNeighbours = new(isotopeCharge);
        noNeighbours.AddIsotope(currentPeak);
        return noNeighbours;


    }
    internal MSDatapoint[] FindPeakNeighbours(MSDatapoint target, bool checkDown)
    {
        Func<MSDatapoint, bool> findNeighbours;
        if (checkDown)
        {
            findNeighbours = q => q.Mz < target.Mz - 0.01 && q.Mz > target.Mz - 1.1;
        }
        else
        {
            findNeighbours = q => q.Mz < target.Mz + 1.1 && q.Mz > target.Mz + 0.01;

        }
        MSDatapoint[] neighbours = CentroidPeaks.Where(findNeighbours).ToArray();

        if (checkDown)
        {
            neighbours = [.. neighbours, target];
            Array.Reverse(neighbours);
            return neighbours;
        }
        else
        {
            return [target, .. neighbours];

        }

    }
}
