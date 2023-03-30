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

    private ISimHubPluginManagerRpm _pluginManager;
    private IRedLinePercentFuncs _redlinePercentFuncs;

    public RevlightsPercentServiceTests()
    {
        _pluginManager = Substitute.For<ISimHubPluginManagerRpm>();
        _redlinePercentFuncs = Substitute.For<IRedLinePercentFuncs>();
    }

    [Fact]
    public void Then_map_rpmShiftLight1()
    {
        _pluginManager.RpmShiftLight1.Returns(3.0);
        _pluginManager.RpmShiftLight2.Returns(0.0);

        _pluginManager.GameDataRpmsValue.Returns(1.0);

        _pluginManager.CurrentGearRedLineRpmValue.Returns(0.0);
        _pluginManager.CarSettingsMaxRpmValue.Returns(1.0);

        _redlinePercentFuncs.ComputeRedLinePercentValue(1.0, 0.0, 1.0)
            .Returns(0.0);

        var actual = RevlightsPercentService.ComputeRevLightsPercent(_pluginManager, _redlinePercentFuncs);

        Check.That(actual).IsEqualTo(1);
    }

    [Fact]
    public void When_redlinePercent_is_zero_AND_rpmShiftLight2_is_zero_THEN_revLightPercent_is_based_on_rpmShiftLight1()
    {
        _pluginManager.RpmShiftLight1.Returns(9.0);
        _pluginManager.RpmShiftLight2.Returns(0.0);

        _pluginManager.GameDataRpmsValue.Returns(1.0);

        _pluginManager.CurrentGearRedLineRpmValue.Returns(1.0);
        _pluginManager.CarSettingsMaxRpmValue.Returns(2.0);

        var actual = RevlightsPercentService.ComputeRevLightsPercent(_pluginManager, _redlinePercentFuncs);

        Check.That(actual).IsEqualTo(3.0);
    }

    [Fact]
    public void When_redlinePercent_is_zero_AND_rpmShiftLight1_is_zero_THEN_revLightPercent_is_based_on_rpmShiftLight2()
    {
        _pluginManager.RpmShiftLight1.Returns(0.0);
        _pluginManager.RpmShiftLight2.Returns(9.0);

        _pluginManager.GameDataRpmsValue.Returns(1.0);

        _pluginManager.CurrentGearRedLineRpmValue.Returns(1.0);
        _pluginManager.CarSettingsMaxRpmValue.Returns(2.0);


        var actual = RevlightsPercentService.ComputeRevLightsPercent(_pluginManager, _redlinePercentFuncs);

        Check.That(actual).IsEqualTo(3.0);
    }

    [Fact]
    public void GIVEN_onely_rpmshift1Percent_not_zero_AND_rpmshift2Percent_is_zero_THEN_compute_rpm_percent()
    {
        var originalGameDataRpmsValue = 5952.8417177905867;
        var originalCurrentGearRedLineRpmValue = 7125.0004638671871;
        var originalCarSettingsMaxRpmValue = 7500.00048828125;
        var originalRpmShitLight1Percent = 0.43712177364775023;
        var originalRpmShitLight2Percent = 0.0;
        var originalRedLinePercent = 0.0;

        _pluginManager.RpmShiftLight1.Returns(originalRpmShitLight1Percent);
        _pluginManager.RpmShiftLight2.Returns(originalRpmShitLight2Percent);

        _pluginManager.GameDataRpmsValue.Returns(originalGameDataRpmsValue);

        _pluginManager.CurrentGearRedLineRpmValue.Returns(originalCurrentGearRedLineRpmValue);
        _pluginManager.CarSettingsMaxRpmValue.Returns(originalCarSettingsMaxRpmValue);

        _redlinePercentFuncs.ComputeRedLinePercentValue(
            originalGameDataRpmsValue,
            originalCurrentGearRedLineRpmValue,
            originalCarSettingsMaxRpmValue).Returns(originalRedLinePercent);

        var actual = RevlightsPercentService.ComputeRevLightsPercent(
            _pluginManager,
            _redlinePercentFuncs);

        Check.That(actual).IsEqualTo(0.1457072578825834);
    }

    [Fact]
    public void GIVEN_onely_rpmshift1Percent_not_zero_AND_rpmshift2Percent_not_zero_THEN_compute_rpm_percent()
    {
        var originalGameDataRpmsValue = 5952.8417177905867;
        var originalCurrentGearRedLineRpmValue = 7125.0004638671871;
        var originalCarSettingsMaxRpmValue = 7500.00048828125;

        var originalRpmShitLight1Percent = 1.0;
        var originalRpmShitLight2Percent = 0.089988052535816959;
        var originalRedLinePercent = 0.0;

        _pluginManager.RpmShiftLight1.Returns(originalRpmShitLight1Percent);
        _pluginManager.RpmShiftLight2.Returns(originalRpmShitLight2Percent);

        _pluginManager.GameDataRpmsValue.Returns(originalGameDataRpmsValue);

        _pluginManager.CurrentGearRedLineRpmValue.Returns(originalCurrentGearRedLineRpmValue);
        _pluginManager.CarSettingsMaxRpmValue.Returns(originalCarSettingsMaxRpmValue);

        _redlinePercentFuncs.ComputeRedLinePercentValue(
            originalGameDataRpmsValue,
            originalCurrentGearRedLineRpmValue,
            originalCarSettingsMaxRpmValue)
            .Returns(originalRedLinePercent);

        var actual = RevlightsPercentService.ComputeRevLightsPercent(
            _pluginManager,
            _redlinePercentFuncs);

        Check.That(actual).IsEqualTo(0.36332935084527229);
    }

    [Fact]
    public void GIVEN_onely_rpmshift1Percent_not_zero_AND_rpmshift2Percent_not_zero_AND_redLignePercent_is_not_zero_THEN_compute_rpm_percent()
    {
        var originalGameDataRpmsValue = 0.0;
        var originalCurrentGearRedLineRpmValue = 0.0;
        var originalCarSettingsMaxRpmValue = 0.0;

        var originalRpmShitLight1Percent = 1.0;
        var originalRpmShitLight2Percent = 1.0;
        var originalRedLinePercent = 0.16493808596406331;

        _pluginManager.RpmShiftLight1.Returns(originalRpmShitLight1Percent);
        _pluginManager.RpmShiftLight2.Returns(originalRpmShitLight2Percent);

        _pluginManager.GameDataRpmsValue.Returns(originalGameDataRpmsValue);

        _pluginManager.CurrentGearRedLineRpmValue.Returns(originalCurrentGearRedLineRpmValue);
        _pluginManager.CarSettingsMaxRpmValue.Returns(originalCarSettingsMaxRpmValue);

        _redlinePercentFuncs.ComputeRedLinePercentValue(
            originalGameDataRpmsValue,
            originalCurrentGearRedLineRpmValue,
            originalCarSettingsMaxRpmValue)
            .Returns(originalRedLinePercent);

        var actual = RevlightsPercentService.ComputeRevLightsPercent(
            _pluginManager,
            _redlinePercentFuncs);

        Check.That(actual).IsEqualTo(0.72164602865468774);
    }
}

