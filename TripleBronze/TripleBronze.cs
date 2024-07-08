using System;
using BepInEx;
using BepInEx.Configuration;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;

namespace TripleBronze
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class TripleBronze : BaseUnityPlugin
    {
        public const string PluginGUID = "com.KaceCottam.TripleBronze";
        public const string PluginName = "TripleBronze";
        public const string PluginVersion = "1.0.0";

        private ConfigEntry<bool> Enabled;
        private ConfigEntry<int> BronzeMultiplier;

        private void CreateConfigValues() {
            // Serverside configuration
            ConfigurationManagerAttributes isAdminOnly = new ConfigurationManagerAttributes { IsAdminOnly = true };
            Enabled = Config.Bind("Server", "EnableTripleBronze", true, new ConfigDescription("Determines whether or not the mod is enabled.", null, isAdminOnly));
            BronzeMultiplier = Config.Bind("Server", "BronzeMultiplier", 3, new ConfigDescription("The normal recipe result for bronze is multiplied by this value.", null, isAdminOnly));
        }

        private void Awake()
        {
            CreateConfigValues();
            ItemManager.OnItemsRegistered += AddRecipes;
        }

        private void AddRecipes()
        {
            if (!Enabled.Value) {
                ItemManager.OnItemsRegistered -= AddRecipes;
                return;
            }

            Jotunn.Logger.LogInfo("Adding Recipes...");
            try
            {
                var no_removed = ObjectDB.instance.m_recipes.RemoveAll((Recipe r) => r.name == "Recipe_Bronze" || r.name == "Recipe_Bronze5");
                if (no_removed != 2) {
                    Jotunn.Logger.LogWarning($"Failed to remove {no_removed} vanilla bronze recipes.");
                }
                Jotunn.Logger.LogInfo("Removed vanilla bronze recipes.");
            } catch (Exception e) {
                Jotunn.Logger.LogWarning($"Failed to remove vanilla bronze recipes: {e.Message}");
            }
            RecipeConfig bronzeConfig = new RecipeConfig();
            bronzeConfig.Name = "TripleBronze_Recipe_Bronze";
            bronzeConfig.Item = "Bronze";
            bronzeConfig.Amount = BronzeMultiplier.Value;
            bronzeConfig.CraftingStation = CraftingStations.Forge;
            bronzeConfig.MinStationLevel = 2;
            bronzeConfig.AddRequirement(new RequirementConfig("Copper", 2));
            bronzeConfig.AddRequirement(new RequirementConfig("Tin", 1));
            ItemManager.Instance.AddRecipe(new CustomRecipe(bronzeConfig));
            Jotunn.Logger.LogInfo("Added Bronze x3 Recipe.");

            RecipeConfig bronze5Config = new RecipeConfig();
            bronze5Config.Name = "TripleBronze_Recipe_Bronze5";
            bronze5Config.Item = "Bronze";
            bronze5Config.Amount = BronzeMultiplier.Value * 5;
            bronze5Config.CraftingStation = CraftingStations.Forge;
            bronze5Config.MinStationLevel = 2;
            bronze5Config.AddRequirement(new RequirementConfig("Copper", 10));
            bronze5Config.AddRequirement(new RequirementConfig("Tin", 5));
            ItemManager.Instance.AddRecipe(new CustomRecipe(bronze5Config));
            Jotunn.Logger.LogInfo("Added Bronze x15 Recipe.");

            Jotunn.Logger.LogInfo("All Recipes Registered.");
            ItemManager.OnItemsRegistered -= AddRecipes;
        }
    }
}

