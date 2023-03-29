using SimHub.Plugins;

namespace SimhubToF12020UDP.Tests;

public class RevlightsPercentServiceTests
{
    private const string GameDataRpms = "DataCorePlugin.GameData.Rpms";
    private const string CurrentGeatRedLineRpm = "DataCorePlugin.GameData.CarSettings_CurrentGearRedLineRPM";
    private const string CarSettingsMaxRpm = "DataCorePlugin.GameData.CarSettings_MaxRPM";
    private const string CarSettingsRpmShiftLight1 = "DataCorePlugin.GameData.CarSettings_RPMShiftLight1";
    private const string RpmShiftLight2 = "DataCorePlugin.GameData.CarSettings_RPMShiftLight2";


    [Fact]
    public void Explore()
    {
        PluginManager pluginManager = new PluginManager();

        pluginManager.AddProperty<double>(GameDataRpms, default(Type), 1000);
    }
}
