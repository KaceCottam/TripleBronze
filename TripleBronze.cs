using System;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace TripleBronze
{
    [BepInPlugin(GUID, PLUGIN_NAME, VERSION)]
    [BepInProcess("valheim.exe")]
    public class TripleBronze : BaseUnityPlugin
    {
        public void Awake()
        {
            var enabled      = base.Config.Bind("TripleBronze", "Enabled", true, "Determines whether or not the mod is enabled.");
            bronzeMultiplier = base.Config.Bind("TripleBronze", "BronzeMultiplier", 3U, "The normal recipe result for bronze is multiplied by this value.");
            debugMessages    = base.Config.Bind("TripleBronze", "DebugEnabled", false, "Enable debug messages in the console.");
            craftBarsInForge = base.Config.Bind("CraftBarsInForge", "Enabled", false, "Allows bypassing the smeltery by create.");
            coalPerBar       = base.Config.Bind("CraftBarsInForge", "CoalPerBar", 5U, "Can create bars using this many coal. Ores/Scrap are done in a 1:1 ratio.");

            if (enabled.Value == true)
            {
                new Harmony(GUID).PatchAll();
            }
        }

        private static ConfigEntry<uint> bronzeMultiplier;
        public static uint BronzeMultiplier => bronzeMultiplier?.Value ?? (uint)bronzeMultiplier.DefaultValue;

        private static ConfigEntry<bool> debugMessages;
        public static bool DebugMessagesEnabled => debugMessages?.Value ?? (bool)debugMessages.DefaultValue;

        private static ConfigEntry<bool> craftBarsInForge;
        public static bool CraftBarsInForgeEnabled => craftBarsInForge?.Value ?? (bool)craftBarsInForge.DefaultValue;

        private static ConfigEntry<uint> coalPerBar;
        public static uint CoalPerBar { get => coalPerBar?.Value ?? (uint)coalPerBar.DefaultValue; }

        public const string PLUGIN_NAME = "TripleBronze";

        public const string GUID = "LolmanXDXD.TripleBronze";

        public const string VERSION = "0.2.1";
    }
}
