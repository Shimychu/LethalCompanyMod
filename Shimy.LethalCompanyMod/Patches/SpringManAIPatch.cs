using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Shimy.LethalCompanyMod.Patches
{
    [HarmonyPatch(typeof(SpringManAI))]
    internal class SpringManAIPatch
    {
        [HarmonyPatch("__initializeVariables")]
        [HarmonyPostfix]
        static void initializeSpeed(ref float ___currentChaseSpeed)
        {
            ___currentChaseSpeed = 12f;
        }

        [HarmonyPatch(nameof(SpringManAI.OnCollideWithPlayer))]
        [HarmonyPostfix]
        static void timeSinceLastHitPatch(SpringManAI __instance, ref float ___timeSinceHittingPlayer, ref bool ___stoppingMovement, Collider other)
        {
           if(!___stoppingMovement && __instance.currentBehaviourStateIndex == 1 && !(___timeSinceHittingPlayer >= 0f))
            {
                PlayerControllerB playerControllerB = __instance.MeetsStandardPlayerCollisionConditions(other);
                if (playerControllerB != null)
                {
                    ___timeSinceHittingPlayer = 0.4f;
                }
            }
        }

    }
}
