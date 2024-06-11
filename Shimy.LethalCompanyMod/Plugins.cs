using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Shimy.LethalCompanyMod.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shimy.LethalCompanyMod
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class ShimyModBase : BaseUnityPlugin
    {
        private const string modGUID = "Shimy.LCInfiniteSprintMod";
        private const string modName = "Shimy Sprint Mod";
        private const string modVersion = "1.0.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static ShimyModBase Instance;

        internal ManualLogSource mls;

        internal ConfigController ConfigManager;

        void Awake()
        {
            if (Instance == null)
            {
                    Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogInfo("Shimy Sprint Mod.");

            harmony.PatchAll(typeof(ShimyModBase));
            harmony.PatchAll(typeof(PlayerControllerBPatch));

            /*
             * Configuration for the server settings including
             * Monsters
             * Spawn
             * Loot
             * Moons
             * Chat features
             * Maps
             * Player perks
             * Terminal Upgrades
             * Custom Server Settings
             */
            ConfigManager = new ConfigController(Config);
            ConfigManager.ServerName = "Shimy Config Manager.";

        }
    }
}

