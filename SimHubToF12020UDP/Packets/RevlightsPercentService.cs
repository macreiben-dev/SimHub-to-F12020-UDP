using SimHub.Plugins;
using System;

namespace SimHubToF12020UDP.Packets
{
    public static class RevlightsPercentService
    {
        private const string GameDataRpms = "DataCorePlugin.GameData.Rpms";
        private const string CurrentGeatRedLineRpm = "DataCorePlugin.GameData.CarSettings_CurrentGearRedLineRPM";
        private const string CarSettingsMaxRpm = "DataCorePlugin.GameData.CarSettings_MaxRPM";
        private const string CarSettingsRpmShiftLight1 = "DataCorePlugin.GameData.CarSettings_RPMShiftLight1";
        private const string RpmShiftLight2 = "DataCorePlugin.GameData.CarSettings_RPMShiftLight2";

        public static double ComputeRevLightsPercent(PluginManager pluginManager)
        {
            double gameDataRpms = GetGameDataRpms(pluginManager);
            double currentGearRedLineRpm = GetCurrentGearRedLineRpm(pluginManager);
            double carSettingsMaxRpm = GetCarSettingsMaxRpm(pluginManager);

            //double v = Convert.ToDouble(pluginManager.GetPropertyValue("DataCorePlugin.GameData.CarSettings_CurrentGearRedLineRPM"));
            var redlinePercent = Math.Max(0, gameDataRpms - currentGearRedLineRpm)
                                / (carSettingsMaxRpm - currentGearRedLineRpm);

            double rpmShiftLight1 = GetRpmShiftLight1(pluginManager);
            double rpmShiftLight2 = GetRpmShiftLight2(pluginManager);

            var revLightsPercent = rpmShiftLight1 / 3.0
                                    + rpmShiftLight2 / 3.0
                                    + redlinePercent / 3.0;

            return revLightsPercent;
        }

        public static double GetRpmShiftLight2(PluginManager pluginManager)
        {
            return Convert.ToDouble(pluginManager.GetPropertyValue(RpmShiftLight2));
        }

        public static double GetRpmShiftLight1(PluginManager pluginManager)
        {
            return Convert.ToDouble(pluginManager.GetPropertyValue(CarSettingsRpmShiftLight1));
        }

        public static double GetCarSettingsMaxRpm(PluginManager pluginManager)
        {
            return Convert.ToDouble(pluginManager.GetPropertyValue(CarSettingsMaxRpm));
        }

        public static double GetCurrentGearRedLineRpm(PluginManager pluginManager)
        {
            return Convert.ToDouble(pluginManager.GetPropertyValue(CurrentGeatRedLineRpm));
        }

        public static double GetGameDataRpms(PluginManager pluginManager)
        {
            return Convert.ToDouble(pluginManager.GetPropertyValue(GameDataRpms));
        }
    }
}