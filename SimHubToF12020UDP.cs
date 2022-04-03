﻿using GameReaderCommon;
using SimHub.Plugins;
using System.Text.RegularExpressions;

namespace SimHubToF12020UDPPlugin
{
    [PluginDescription("Broadcast UDP data in F1 2020 format. Used for Thrustmasters SF1000 wheel")]
    [PluginAuthor("Maxhyt")]
    [PluginName("F12020 UDP Broadcast")]
    public class SimHubToF12020UDP : IPlugin, IWPFSettings
    {
        public SimHubToF12020UDPSettings Settings;

        /// <summary>
        /// Instance of the current plugin manager
        /// </summary>
        public PluginManager PluginManager { get; set; }

        /// <summary>
        /// Called at plugin manager stop, close/dispose anything needed here ! 
        /// Plugins are rebuilt at game change
        /// </summary>
        /// <param name="pluginManager"></param>
        public void End(PluginManager pluginManager)
        {
            // Save settings
            this.SaveCommonSettings("GeneralSettings", Settings);
        }

        /// <summary>
        /// Returns the settings control, return null if no settings control is required
        /// </summary>
        /// <param name="pluginManager"></param>
        /// <returns></returns>
        public System.Windows.Controls.Control GetWPFSettingsControl(PluginManager pluginManager)
        {
            return new SettingsControl(this);
        }

        /// <summary>
        /// Called once after plugins startup
        /// Plugins are rebuilt at game change
        /// </summary>
        /// <param name="pluginManager"></param>
        public void Init(PluginManager pluginManager)
        {
            SimHub.Logging.Current.Info("Starting plugin");

            // Load settings
            Settings = this.ReadCommonSettings("GeneralSettings", () => new SimHubToF12020UDPSettings());

            UDPServer.Instance.Init();
        }
    }
}