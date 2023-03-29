using NFluent;
using NSubstitute;
using SimHub.Plugins;
using SimHubToF12020UDP.Packets;

namespace SimhubToF12020UDP.Tests;

public class RevlightsPercentServiceTests
{
    private const string GameDataRpms = "DataCorePlugin.GameData.Rpms";
    private const string CurrentGeatRedLineRpm = "DataCorePlugin.GameData.CarSettings_CurrentGearRedLineRPM";
    private const string CarSettingsMaxRpm = "DataCorePlugin.GameData.CarSettings_MaxRPM";
    private const string CarSettingsRpmShiftLight1 = "DataCorePlugin.GameData.CarSettings_RPMShiftLight1";
    private const string RpmShiftLight2 = "DataCorePlugin.GameData.CarSettings_RPMShiftLight2";


    [Fact]
    public void Then_map_rpmShiftLight1()
    {
        var pluginManager = Substitute.For<ISimHubPluginManagerRpm>();

        pluginManager.RpmShiftLight1.Returns(3.0);
        pluginManager.RpmShiftLight2.Returns(0.0);
        
        pluginManager.GameDataRpmsValue.Returns(1.0);

        pluginManager.CurrentGearRedLineRpmValue.Returns(0.0);
        pluginManager.CarSettingsMaxRpmValue.Returns(1.0);

        var actual = RevlightsPercentService.ComputeRevLightsPercent(pluginManager);

        Check.That(actual).IsEqualTo(1.3333333333333333);
    }

    [Fact]
    public void When_redlinePercent_is_zero_AND_rpmShiftLight2_is_zero_THEN_revLightPercent_is_based_on_rpmShiftLight1()
    {
        var pluginManager = Substitute.For<ISimHubPluginManagerRpm>();

        pluginManager.RpmShiftLight1.Returns(9.0);
        pluginManager.RpmShiftLight2.Returns(0.0);

        pluginManager.GameDataRpmsValue.Returns(1.0);

        pluginManager.CurrentGearRedLineRpmValue.Returns(1.0);
        pluginManager.CarSettingsMaxRpmValue.Returns(2.0);

        var actual = RevlightsPercentService.ComputeRevLightsPercent(pluginManager);

        Check.That(actual).IsEqualTo(3.0);
    }

    [Fact]
    public void When_redlinePercent_is_zero_AND_rpmShiftLight1_is_zero_THEN_revLightPercent_is_based_on_rpmShiftLight2()
    {
        var pluginManager = Substitute.For<ISimHubPluginManagerRpm>();

        pluginManager.RpmShiftLight1.Returns(0.0);
        pluginManager.RpmShiftLight2.Returns(9.0);

        pluginManager.GameDataRpmsValue.Returns(1.0);

        pluginManager.CurrentGearRedLineRpmValue.Returns(1.0);
        pluginManager.CarSettingsMaxRpmValue.Returns(2.0);

        var actual = RevlightsPercentService.ComputeRevLightsPercent(pluginManager);

        Check.That(actual).IsEqualTo(3.0);
    }
}
