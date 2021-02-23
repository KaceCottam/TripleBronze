using System;
using BepInEx;
using HarmonyLib;

namespace TripleBronze
{
    [BepInPlugin(PluginInfo.Guid, PluginInfo.Name, PluginInfo.Version)]
    [BepInProcess("valheim.exe")]
    [BepInProcess("valheim_server.exe")]
    public class Plugin : BaseUnityPlugin
    {
        public void Awake()
        {
            new Harmony(PluginInfo.Guid).PatchAll();
        }
    }

    // TODO add multiplier configuration for bronze, and possibly for iron
    // idk yet
}
