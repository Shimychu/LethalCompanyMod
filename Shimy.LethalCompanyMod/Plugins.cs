using BepInEx;
using BepInEx.Logging;
using GameNetcodeStuff;
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

        internal static ShimyModBase Instance;

        internal static ManualLogSource mls;

        internal ConfigController ConfigManager;

        internal PlayerControllerB Player;

        internal ModMenu Menu;

        void Awake()
        {
            if (Instance == null)
            {
                    Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogInfo("Shimy Sprint Mod.");

            harmony.PatchAll(typeof(ShimyModBase));
            harmony.PatchAll(typeof(TerminalPatch));
            harmony.PatchAll(typeof(PlayerControllerBPatch));
            harmony.PatchAll(typeof(SpringManAIPatch));

            /*
             * Set the instance of player controller so that we have access to playercontroler.
             */

            this.Player = GameNetworkManager.Instance.localPlayerController;
            //harmony.PatchAll(typeof(QuickMenuManagerPatch));

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

            /*
             * Create mod menu
             */

            var menuGameObject = new UnityEngine.GameObject("ModMenu");
            UnityEngine.Object.DontDestroyOnLoad(menuGameObject);
            menuGameObject.hideFlags = UnityEngine.HideFlags.HideAndDontSave;
            menuGameObject.AddComponent<ModMenu>();
            Menu = (ModMenu)menuGameObject.GetComponent("ModMenu");
            ShimyModBase.mls.LogInfo("Mod menu Created.");

        }

        public void patchPlayerController()
        {
            harmony.PatchAll(typeof(PlayerControllerBPatch));
        }
    }
}

