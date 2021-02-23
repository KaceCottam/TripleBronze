using System;
using HarmonyLib;
using BepInEx;
using System.Collections.Generic;

namespace TripleBronze
{
    [HarmonyPatch]
    public static class Patches
    {
        public static BepInEx.Logging.ManualLogSource logger = BepInEx.Logging.Logger.CreateLogSource(PluginInfo.Name);
        private static bool flag = false; // dumb fix

        [HarmonyPostfix]
        [HarmonyPatch(typeof(ObjectDB), "Awake")]
        public static void Awake_MyPatch(ref ObjectDB __instance)
        {
            if (flag) return;
            foreach (var recipe in __instance.m_recipes)
            {
                if (recipe.m_item?.m_itemData.m_shared.m_name != @"$item_bronze")
                {
                    continue;
                }
                //logger.LogInfo($"Found bronze: {recipe.m_item.m_itemData.m_shared.m_name}");
                recipe.m_amount *= 3;
                flag = true;
            }
        }
    }
}
