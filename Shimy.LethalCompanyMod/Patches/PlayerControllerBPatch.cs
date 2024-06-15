using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shimy.LethalCompanyMod.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class PlayerControllerBPatch
    {
        static bool allForOne = false;
        static bool infiniteSprint = false;
        static bool infiniteHP = false;
        static bool infiniteCarryWeight = false;

        public static void infiniteHPToggle(bool enable)
        {
            infiniteHP = enable;
        }

        public static void infiniteSprintToggle(bool enable)
        {
            infiniteSprint = enable;
        }

        public static void infiniteMuscleToggle(bool enable)
        {
            infiniteCarryWeight = enable;
        }

        public static void allForOneToggle(bool enable)
        {
            allForOne = enable;
        }

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void cheatPatch(ref float ___sprintMeter, ref int ___health, ref float ___carryWeight)
        {
            if(infiniteSprint || allForOne)
            {
                ___sprintMeter = 1f;
            }
            if(infiniteHP || allForOne)
            {
                ___health = 100;
            }
            if(infiniteCarryWeight || allForOne)
            {
                ___carryWeight = 1f;
            }

        }
    }
}
