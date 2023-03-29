using SimHub.Plugins;
using System;

namespace SimHubToF12020UDP.Packets
{
    internal class PluginManagerRpmWrapper : IPluginManagerRpmWrapper
    {
        private const string GameDataRpms = "DataCorePlugin.GameData.Rpms";
        private const string CurrentGearRedLineRpm = "DataCorePlugin.GameData.CarSettings_CurrentGearRedLineRPM";
        private const string CarSettingsMaxRpm = "DataCorePlugin.GameData.CarSettings_MaxRPM";
        private const string CarSettingsRpmShiftLight1 = "DataCorePlugin.GameData.CarSettings_RPMShiftLight1";
        private const string CarSettingsRpmShiftLight2 = "DataCorePlugin.GameData.CarSettings_RPMShiftLight2";
        
        private readonly double _rpmShiftLight1;
        private readonly double _rpmShiftLight2;
        private readonly double _carSettingsMaxRpm;
        private readonly double _currentGearRedLineRpm;
        private readonly double _gameDataRpms;

        public PluginManagerRpmWrapper(PluginManager pluginManager)
        {
            _rpmShiftLight1 = GetRpmShiftLight1(pluginManager);
            _rpmShiftLight2 = GetRpmShiftLight2(pluginManager);

            _carSettingsMaxRpm = GetCarSettingsMaxRpm(pluginManager);
            _currentGearRedLineRpm = GetCurrentGearRedLineRpm(pluginManager);

            _gameDataRpms = GetGameDataRpms(pluginManager);
        }

        public double RpmShiftLight1 => _rpmShiftLight1;

        public double RpmShiftLight2 => _rpmShiftLight2;

        public double CarSettingsMaxRpmValue => _carSettingsMaxRpm;

        public double CurrentGearRedLineRpmValue => _currentGearRedLineRpm;

        public double GameDataRpmsValue => _gameDataRpms;

        public static double GetCarSettingsMaxRpm(PluginManager pluginManager)
        {
            return Convert.ToDouble(pluginManager.GetPropertyValue(CarSettingsMaxRpm));
        }

        public static double GetCurrentGearRedLineRpm(PluginManager pluginManager)
        {
            return Convert.ToDouble(pluginManager.GetPropertyValue(CurrentGearRedLineRpm));
        }

        public static double GetGameDataRpms(PluginManager pluginManager)
        {
            return Convert.ToDouble(pluginManager.GetPropertyValue(GameDataRpms));
        }

        public static double GetRpmShiftLight1(PluginManager pluginManager)
        {
            return Convert.ToDouble(pluginManager.GetPropertyValue(CarSettingsRpmShiftLight1));
        }

        public static double GetRpmShiftLight2(PluginManager pluginManager)
        {
            return Convert.ToDouble(pluginManager.GetPropertyValue(CarSettingsRpmShiftLight2));
        }
    }
}