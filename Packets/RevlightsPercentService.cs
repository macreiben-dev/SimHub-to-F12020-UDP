using SimHub.Plugins;
using System;

namespace SimHubToF12020UDP.Packets
{
    internal static class RevlightsPercentService
    {

        private static double ComputeRevLightsPercent(PluginManager pluginManager)
        {
            double gameDataRpms = Convert.ToDouble(pluginManager.GetPropertyValue("DataCorePlugin.GameData.Rpms"));

            double currentGearRedLineRpm = Convert.ToDouble(pluginManager.GetPropertyValue("DataCorePlugin.GameData.CarSettings_CurrentGearRedLineRPM"));

            double carSettingsMaxRpm = Convert.ToDouble(pluginManager.GetPropertyValue("DataCorePlugin.GameData.CarSettings_MaxRPM"));

            //double v = Convert.ToDouble(pluginManager.GetPropertyValue("DataCorePlugin.GameData.CarSettings_CurrentGearRedLineRPM"));
            var redlinePercent = Math.Max(0, gameDataRpms - currentGearRedLineRpm)
                                / (carSettingsMaxRpm - currentGearRedLineRpm);

            double rpmShiftLight1 = Convert.ToDouble(pluginManager.GetPropertyValue("DataCorePlugin.GameData.CarSettings_RPMShiftLight1"));

            double rpmShiftLight2 = Convert.ToDouble(pluginManager.GetPropertyValue("DataCorePlugin.GameData.CarSettings_RPMShiftLight2"));

            var revLightsPercent = rpmShiftLight1 / 3.0
                                    + rpmShiftLight2 / 3.0
                                    + redlinePercent / 3.0;
            return revLightsPercent;
        }
    }
}