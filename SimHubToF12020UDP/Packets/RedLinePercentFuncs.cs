using System;

namespace SimHubToF12020UDP.Packets
{
    internal static class RedLinePercentFuncs
    {

        public static double ComputeRedLinePercent(double gameDataRpms, double currentGearRedLineRpm, double carSettingsMaxRpm)
        {

            //double v = Convert.ToDouble(pluginManager.GetPropertyValue("DataCorePlugin.GameData.CarSettings_CurrentGearRedLineRPM"));
            return Math.Max(0, gameDataRpms - currentGearRedLineRpm)
                                / (carSettingsMaxRpm - currentGearRedLineRpm);
        }
    }
}