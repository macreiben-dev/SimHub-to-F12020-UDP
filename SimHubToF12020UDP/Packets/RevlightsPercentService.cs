using SimHub.Plugins;
using System;

namespace SimHubToF12020UDP.Packets
{
    public static class RevlightsPercentService
    {
        public static double ComputeRevLightsPercent(PluginManager pluginManager)
        {
            double gameDataRpms = PluginManagerRpmWrapper.GetGameDataRpms(pluginManager);
            double currentGearRedLineRpm = PluginManagerRpmWrapper.GetCurrentGearRedLineRpm(pluginManager);
            double carSettingsMaxRpm = PluginManagerRpmWrapper.GetCarSettingsMaxRpm(pluginManager);

            //double v = Convert.ToDouble(pluginManager.GetPropertyValue("DataCorePlugin.GameData.CarSettings_CurrentGearRedLineRPM"));
            var redlinePercent = Math.Max(0, gameDataRpms - currentGearRedLineRpm)
                                / (carSettingsMaxRpm - currentGearRedLineRpm);

            double rpmShiftLight1 = PluginManagerRpmWrapper.GetRpmShiftLight1(pluginManager);
            double rpmShiftLight2 = PluginManagerRpmWrapper.GetRpmShiftLight2(pluginManager);

            var revLightsPercent = rpmShiftLight1 / 3.0
                                    + rpmShiftLight2 / 3.0
                                    + redlinePercent / 3.0;

            return revLightsPercent;
        }

        public static double ComputeRevLightsPercent(ISimHubPluginManagerRpm pluginManager)
        {
            double gameDataRpms = pluginManager.GameDataRpmsValue;
            double currentGearRedLineRpm = pluginManager.CurrentGearRedLineRpmValue;
            double carSettingsMaxRpm = pluginManager.CarSettingsMaxRpmValue;
            double redlinePercent = RedLinePercentFuncs.ComputeRedLinePercent(gameDataRpms, currentGearRedLineRpm, carSettingsMaxRpm);

            double rpmShiftLight1 = pluginManager.RpmShiftLight1;
            double rpmShiftLight2 = pluginManager.RpmShiftLight2;

            var revLightsPercent = rpmShiftLight1 / 3.0
                                    + rpmShiftLight2 / 3.0
                                    + redlinePercent / 3.0;

            return revLightsPercent;
        }
    }
}