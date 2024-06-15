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
        static string lastTextInput;
        //[HarmonyPatch("TextPostProcess")]
        //[HarmonyPostfix]
        //static void ProcessTextTest(string modifiedDisplayText)
        //{
        //    string debugText = "Re printing text : " + modifiedDisplayText;
        //    ShimyModBase.mls.LogInfo(debugText);

        //}
        static string RemovePunctuation(string s)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in s)
            {
                if (!char.IsPunctuation(c))
                {
                    stringBuilder.Append(c);
                }
            }
            return stringBuilder.ToString().ToLower().Trim();
        }

        [HarmonyPatch("ParsePlayerSentence")]
        [HarmonyPostfix]
        static void ProcessTextTest2(ref TMP_InputField ___screenText, ref int ___textAdded)
        {
            string s = ___screenText.text.Substring(___screenText.text.Length - ___textAdded);
            lastTextInput = RemovePunctuation(s);
            ShimyModBase.mls.LogInfo(lastTextInput);

        }

        [HarmonyPatch(nameof(Terminal.OnSubmit))]
        [HarmonyPostfix]
        static void ProcessTextOnSubmit(ref int ___groupCredits)
        {
            ShimyModBase.mls.LogInfo("OnSubmit Patch");
            if (lastTextInput == "poweroverwhelming")
            {
                ShimyModBase.mls.LogInfo("Infinite Hp toggle");
                PlayerControllerBPatch.infiniteHPToggle(true);
            }
            if (lastTextInput == "foodforthought")
            {
                ShimyModBase.mls.LogInfo("Infinite Sprint toggle");
                PlayerControllerBPatch.infiniteSprintToggle(true);
            }
            if (lastTextInput == "showmethemoney")
            {
                ___groupCredits = ___groupCredits + 25000;
            }
            if (lastTextInput == "muscleman")
            {
                ShimyModBase.mls.LogInfo("Infinite Weight toggle");
                PlayerControllerBPatch.infiniteMuscleToggle(true);
            }
            if (lastTextInput == "allforone")
            {
                ShimyModBase.mls.LogInfo("All for one toggle");
                PlayerControllerBPatch.allForOneToggle(true);
            }
        }

    }
}
