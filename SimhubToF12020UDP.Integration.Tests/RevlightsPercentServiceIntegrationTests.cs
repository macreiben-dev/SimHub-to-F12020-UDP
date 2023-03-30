using NFluent;
using NSubstitute;
using SimHubToF12020UDP.Packets;

namespace SimhubToF12020UDP.Tests;

public class RevlightsPercentServiceIntegrationTests
{
    private ISimHubPluginManagerRpm _pluginManager;
    private IRedLinePercentFuncs _redlinePercentFuncs;

    public RevlightsPercentServiceIntegrationTests()
    {
        _pluginManager = Substitute.For<ISimHubPluginManagerRpm>();
        _redlinePercentFuncs = new RedLinePercentFuncs();
    }

    public RevlightsPercentService GetTarget()
    {
        return new RevlightsPercentService();
    }

    [Fact]
    public void GIVEN_onely_rpmshift1Percent_not_zero_AND_rpmshift2Percent_is_zero_THEN_compute_rpm_percent()
    {
        var originalGameDataRpmsValue = 5952.8417177905867;
        var originalCurrentGearRedLineRpmValue = 7125.0004638671871;
        var originalCarSettingsMaxRpmValue = 7500.00048828125;
        var originalRpmShitLight1Percent = 0.43712177364775023;
        var originalRpmShitLight2Percent = 0.0;

        _pluginManager.RpmShiftLight1.Returns(originalRpmShitLight1Percent);
        _pluginManager.RpmShiftLight2.Returns(originalRpmShitLight2Percent);

        _pluginManager.GameDataRpmsValue.Returns(originalGameDataRpmsValue);

        _pluginManager.CurrentGearRedLineRpmValue.Returns(originalCurrentGearRedLineRpmValue);
        _pluginManager.CarSettingsMaxRpmValue.Returns(originalCarSettingsMaxRpmValue);

        var actual = GetTarget().ComputeRevLightsPercent(
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

        _pluginManager.RpmShiftLight1.Returns(originalRpmShitLight1Percent);
        _pluginManager.RpmShiftLight2.Returns(originalRpmShitLight2Percent);

        _pluginManager.GameDataRpmsValue.Returns(originalGameDataRpmsValue);

        _pluginManager.CurrentGearRedLineRpmValue.Returns(originalCurrentGearRedLineRpmValue);
        _pluginManager.CarSettingsMaxRpmValue.Returns(originalCarSettingsMaxRpmValue);

        var actual = GetTarget().ComputeRevLightsPercent(
            _pluginManager,
            _redlinePercentFuncs);

        Check.That(actual).IsEqualTo(0.36332935084527229);
    }

    [Fact]
    public void GIVEN_onely_rpmshift1Percent_not_zero_AND_rpmshift2Percent_not_zero_AND_redLignePercent_is_not_zero_THEN_compute_rpm_percent()
    {
        var originalGameDataRpmsValue = 7154.0813303418781;
        var originalCurrentGearRedLineRpmValue = 7125.0004638671871;
        var originalCarSettingsMaxRpmValue = 7500.00048828125;

        var originalRpmShitLight1Percent = 1.0;
        var originalRpmShitLight2Percent = 1.0;

        _pluginManager.RpmShiftLight1.Returns(originalRpmShitLight1Percent);
        _pluginManager.RpmShiftLight2.Returns(originalRpmShitLight2Percent);

        _pluginManager.GameDataRpmsValue.Returns(originalGameDataRpmsValue);

        _pluginManager.CurrentGearRedLineRpmValue.Returns(originalCurrentGearRedLineRpmValue);
        _pluginManager.CarSettingsMaxRpmValue.Returns(originalCarSettingsMaxRpmValue);

        var actual = GetTarget().ComputeRevLightsPercent(
            _pluginManager,
            _redlinePercentFuncs);

        Check.That(actual).IsEqualTo(0.69251632407236041);
    }
}
