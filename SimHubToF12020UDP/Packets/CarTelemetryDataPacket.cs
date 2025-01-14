﻿using SimHub.Plugins;
using SimHubToF12020UDP.DataStructures;
using System;
using System.Runtime.InteropServices;

namespace SimHubToF12020UDP.Packets
{
    internal static class CarTelemetryDataPacket
    {
        private static uint FrameCount = 0;

        public static byte[] Read()
        {
            var pluginManager = PluginManager.GetInstance();

            var start = pluginManager.GetPropertyValue("DataCorePlugin.GameRunning");

            if (!Convert.ToBoolean(start))
            {
                return new byte[0];
            }

            FrameCount = (FrameCount + 1) % uint.MaxValue;

            var header = new PacketHeader
            {
                m_packetFormat = 2020,
                m_gameMajorVersion = 1,
                m_gameMinorVersion = 16,
                m_packetVersion = 1,
                m_sessionUID = 0,
                m_sessionTime = 0,
                m_frameIdentifier = FrameCount,
                m_playerCarIndex = 0,
                m_secondaryPlayerCarIndex = 0,
                m_packetId = 6
            };

            var packet = new PacketCarTelemetryData
            {
                m_header = header,
                m_carTelemetryData = new CarTelemetryData[22],
                m_buttonStatus = 0,
                m_mfdPanelIndex = 255,
                m_mfdPanelIndexSecondaryPlayer = 255,
                m_suggestedGear = 0
            };

            var gear = pluginManager.GetPropertyValue("DataCorePlugin.GameData.Gear");
            gear = (string)gear == "N" ? 0 : ((string)gear == "R" ? -1 : gear);

            var redlinePercent = Math.Max(0, Convert.ToDouble(pluginManager.GetPropertyValue("DataCorePlugin.GameData.Rpms")) - Convert.ToDouble(pluginManager.GetPropertyValue("DataCorePlugin.GameData.CarSettings_CurrentGearRedLineRPM")))
                                / (Convert.ToDouble(pluginManager.GetPropertyValue("DataCorePlugin.GameData.CarSettings_MaxRPM")) - Convert.ToDouble(pluginManager.GetPropertyValue("DataCorePlugin.GameData.CarSettings_CurrentGearRedLineRPM")));
            var revLightsPercent = Convert.ToDouble(pluginManager.GetPropertyValue("DataCorePlugin.GameData.CarSettings_RPMShiftLight1")) / 3.0
                                    + Convert.ToDouble(pluginManager.GetPropertyValue("DataCorePlugin.GameData.CarSettings_RPMShiftLight2")) / 3.0
                                    + redlinePercent / 3.0;

            packet.m_carTelemetryData[0] = new CarTelemetryData
            {
                m_speed = Utils.ClampIntegerValue<ushort>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.SpeedKmh")),
                m_throttle = Utils.ClampFloatingValue<float>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.Throttle")) / 100f,
                m_steer = 0,
                m_brake = Utils.ClampFloatingValue<float>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.Brake")) / 100f,
                m_clutch = Utils.ClampIntegerValue<byte>(Math.Min(100, Convert.ToDouble(pluginManager.GetPropertyValue("DataCorePlugin.GameData.Clutch"))) / 100),     // Clutch value is different each game, some returns percentage, some returns raw value (3700). We don't have MaxClutch property so unable to find out the percentage
                m_gear = Utils.ClampIntegerValue<sbyte>(gear),
                m_engineRPM = Utils.ClampIntegerValue<ushort>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.Rpms")),
                m_drs = Utils.ClampIntegerValue<byte>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.DRSEnabled")),
                m_revLightsPercent = Utils.ClampIntegerValue<byte>(revLightsPercent * 100),
                m_brakesTemperature = new ushort[4]
                {
                    Utils.ClampIntegerValue<ushort>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.BrakeTemperatureRearLeft")),
                    Utils.ClampIntegerValue<ushort>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.BrakeTemperatureRearRight")),
                    Utils.ClampIntegerValue<ushort>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.BrakeTemperatureFrontLeft")),
                    Utils.ClampIntegerValue<ushort>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.BrakeTemperatureFrontRight")),
                },
                m_tyresSurfaceTemperature = new byte[4]
                {
                    Utils.ClampIntegerValue<byte>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureRearLeft")),
                    Utils.ClampIntegerValue<byte>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureRearRight")),
                    Utils.ClampIntegerValue<byte>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureFrontLeft")),
                    Utils.ClampIntegerValue<byte>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureFrontRight")),
                },
                m_tyresInnerTemperature = new byte[4]
                {
                    Utils.ClampIntegerValue<byte>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureRearLeftInner")),
                    Utils.ClampIntegerValue<byte>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureRearRightInner")),
                    Utils.ClampIntegerValue<byte>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureFrontLeftInner")),
                    Utils.ClampIntegerValue<byte>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.TyreTemperatureFrontRightInner")),
                },
                m_engineTemperature = Utils.ClampIntegerValue<ushort>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.OilTemperature")),
                m_tyresPressure = new float[4]
                {
                    Utils.ClampFloatingValue<float>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.TyrePressureRearLeft")),
                    Utils.ClampFloatingValue<float>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.TyrePressureRearRight")),
                    Utils.ClampFloatingValue<float>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.TyrePressureFrontLeft")),
                    Utils.ClampFloatingValue<float>(pluginManager.GetPropertyValue("DataCorePlugin.GameData.TyrePressureFrontRight")),
                },
                m_surfaceType = new byte[4] { 0, 0, 0, 0 },
            };

            int size = Marshal.SizeOf(packet);
            byte[] arr = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(packet, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }
    }
}
