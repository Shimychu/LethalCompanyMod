using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;

namespace Shimy.LethalCompanyMod.Patches
{
    [HarmonyPatch(typeof(Terminal))]
    internal class TerminalPatch
    {
        //[HarmonyPatch("TextPostProcess")]
        //[HarmonyPostfix]
        //static void ProcessTextTest(string modifiedDisplayText)
        //{
        //    string debugText = "Re printing text : " + modifiedDisplayText;
        //    ShimyModBase.mls.LogInfo(debugText);
            
        //}

        [HarmonyPatch("ParsePlayerSentence")]
        [HarmonyPostfix]
        static void ProcessTextTest2(ref TMP_InputField ___screenText, ref int ___textAdded)
        {
            string debugText = "Re printing text : " + ___screenText.text.Substring(___screenText.text.Length - ___textAdded);
            ShimyModBase.mls.LogInfo(debugText);

        }
    }
}
