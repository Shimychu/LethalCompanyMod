using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Shimy.LethalCompanyMod
{
    internal class ModMenu : MonoBehaviour
    {
        internal static ModMenu Instance;
        internal bool Visible = false;

        /*
         * Eventually scale this to monitor resolution but for now this is okay.
         */
        private const int MENU_WIDTH = 600;
        private const int MENU_HEIGHT = 800;
        private int MENU_X;
        private int MENU_Y;
        private int CENTER_X;
        private int ITEM_WIDTH = 300;


        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            ShimyModBase.mls.LogInfo("Mod Menu Awake.");
            MENU_X = Screen.width / 2;
            MENU_Y = Screen.height / 2;
            CENTER_X = MENU_X + ((MENU_WIDTH / 2) - (ITEM_WIDTH / 2));
        }

        void Update()
        {


        }

        void OnGUI()
        {
            if(!Visible) { return; }
            GUI.Box(new Rect(MENU_X, MENU_Y, MENU_WIDTH, MENU_HEIGHT), "Mod Menu");
            ShimyModBase.mls.LogInfo("Mod menu is opened.");
        }

        private Texture2D MakeText(int width, int height, Color color)
        {
            Color[] pix = new Color[width * height];
            for(int i = 0; i < pix.Length; i++)
            {
                pix[i] = color;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
    }
}
