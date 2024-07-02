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
using UnityEngine;
using System.IO;
using System.Reflection;
using LethalLib.Modules;

namespace Shimy.LethalCompanyMod
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class ShimyModBase : BaseUnityPlugin
    {
        private const string modGUID = "Shimy.LCShimyMod";
        private const string modName = "Shimy Mod";
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

            /*
             * Load asset bundle for scrap items
             */

            string assetDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "itemmod");
            AssetBundle bundle = AssetBundle.LoadFromFile(assetDir);

            //Create item
            Item watermelonItem = bundle.LoadAsset<Item>("Assets/LethalCompanyItems/Watermelon.asset");
            
            // Register item in game
            NetworkPrefabs.RegisterNetworkPrefab(watermelonItem.spawnPrefab);
            
            // Fix for audio issue
            Utilities.FixMixerGroups(watermelonItem.spawnPrefab);
            
            // Register item as a scrap
            Items.RegisterScrap(watermelonItem,1000,Levels.LevelTypes.All);

            TerminalNode node = ScriptableObject.CreateInstance<TerminalNode>();
            node.clearPreviousText = true;
            node.displayText = "This is a watermelon.";
            Items.RegisterShopItem(watermelonItem, null, null, node, 0);

            ShimyModBase.mls.LogInfo("Patched scrap items mod");
        }

        public void patchPlayerController()
        {
            harmony.PatchAll(typeof(PlayerControllerBPatch));
        }
    }
}

