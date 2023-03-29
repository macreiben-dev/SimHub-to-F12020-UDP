namespace SimHubToF12020UDP.Packets
{
    public interface IPluginManagerRpmWrapper
    {
        double CarSettingsMaxRpmValue { get; }
        double CurrentGearRedLineRpmValue { get; }
        double GameDataRpmsValue { get; }
        double RpmShiftLight1 { get; }
        double RpmShiftLight2 { get; }
    }
}