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

    [Fact]
    public void GIVEN_gameDataRpms_is_four_AND_currentGearRedLineRpm_is_three_AND_carSettingsMaxRpm_is_four_THEN_redlinePercent_is_one()
    {
        var gameDataRpms = 4.0;
        var currentGearRedLineRpm = 3.0;
        var carSettingsMaxRpm = 4.0;

        var actual = RedLinePercentFuncs.ComputeRedLinePercent(
            gameDataRpms,
            currentGearRedLineRpm,
            carSettingsMaxRpm);

        Check.That(actual).IsEqualTo(1.0);
    }

    [Fact]
    public void GIVEN_gameDataRpms_is_four_AND_currentGearRedLineRpm_is_three_AND_carSettingsMaxRpm_is_five_THEN_redlinePercent_is_onePointFive()
    {
        var gameDataRpms = 4.0;
        var currentGearRedLineRpm = 3.0;
        var carSettingsMaxRpm = 5.0;

        var actual = RedLinePercentFuncs.ComputeRedLinePercent(
            gameDataRpms,
            currentGearRedLineRpm,
            carSettingsMaxRpm);

        Check.That(actual).IsEqualTo(0.5);
    }


    [Fact]
    public void GIVEN_redlinePercentFuns_injectable_AND_gameDataRpms_is_four_AND_currentGearRedLineRpm_is_three_AND_carSettingsMaxRpm_is_five_THEN_redlinePercent_is_onePointFive()
    {
        var gameDataRpms = 4.0;
        var currentGearRedLineRpm = 3.0;
        var carSettingsMaxRpm = 5.0;

        var actual = new RedLinePercentFuncs().ComputeRedLinePercentValue(
            gameDataRpms,
            currentGearRedLineRpm,
            carSettingsMaxRpm);

        Check.That(actual).IsEqualTo(0.5);
    }
}
