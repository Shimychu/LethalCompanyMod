using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shimy.LethalCompanyMod.Patches
{
    [HarmonyPatch(typeof(QuickMenuManager))]
    internal class QuickMenuManagerPatch
    {
        [HarmonyPatch(nameof(QuickMenuManager.OpenQuickMenu))]
        [HarmonyPrefix]
        public static void OpenMenu()
        {
            ShimyModBase.Instance.Menu.Visible = true;
            ShimyModBase.mls.LogInfo("Mod Menu Open.");
        }

        [HarmonyPatch(nameof(QuickMenuManager.CloseQuickMenu))]
        [HarmonyPrefix]
        public static void CloseMenu()
        {
            ShimyModBase.Instance.Menu.Visible = false;
            ShimyModBase.mls.LogInfo("Mod Menu Close.");
        }
        [HarmonyPatch(nameof(QuickMenuManager.LeaveGameConfirm))]
        [HarmonyPrefix]
        public static void CloseMenuOnLeaveGame()
        {
            ShimyModBase.Instance.Menu.Visible = false;
        }
    }
}
