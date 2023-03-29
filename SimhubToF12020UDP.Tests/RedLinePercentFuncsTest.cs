using NFluent;
using SimHubToF12020UDP.Packets;

namespace SimhubToF12020UDP.Tests;

public class RedLinePercentFuncsTest
{
    [Fact]
    public void GIVEN_gameDataRpms_is_one_AND_currentGearRedLineRpm_is_one_to_currentGearRedLineRpm_AND_carSettingsMaxRpm_is_2_THEN_redlinePercent_is_zero()
    {
        var gameDataRpms = 1.0;
        var currentGearRedLineRpm = 1.0;
        var carSettingsMaxRpm = 2.0;

        var actual = RedLinePercentFuncs.ComputeRedLinePercent(
            gameDataRpms, 
            currentGearRedLineRpm, 
            carSettingsMaxRpm);

        Check.That(actual).IsEqualTo(0.0);
    }

    [Fact]
    public void GIVEN_gameDataRpms_is_zero_AND_currentGearRedLineRpm_is_one_to_currentGearRedLineRpm_AND_carSettingsMaxRpm_is_2_THEN_redlinePercent_is_zero()
    {
        var gameDataRpms = 0.0;
        var currentGearRedLineRpm = 1.0;
        var carSettingsMaxRpm = 2.0;

        var actual = RedLinePercentFuncs.ComputeRedLinePercent(
            gameDataRpms,
            currentGearRedLineRpm,
            carSettingsMaxRpm);

        Check.That(actual).IsEqualTo(0.0);
    }
}
