namespace SimHubToF12020UDP.Packets
{
    public interface IRedLinePercentFuncs
    {
        double ComputeRedLinePercentValue(double gameDataRpms, double currentGearRedLineRpm, double carSettingsMaxRpm);
    }
}