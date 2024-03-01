using MelonLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace WindowFix
{
    
    public class Class1:MelonMod
    {
        static bool ToggleUI = false;
        static int Height = 0;
        static int Width = 0;
        static bool fullscreen;
        [Obsolete]
        public override void OnApplicationLateStart()
        {
            if(File.Exists("WinFix.cfg"))
            {
                string file = File.ReadAllText("WinFix.cfg");
                Height = int.Parse(file.Split('\n')[0]);
                Width = int.Parse(file.Split('\n')[1]);
                fullscreen = bool.Parse(file.Split('\n')[2]);
                Screen.fullScreen = fullscreen;
                Screen.SetResolution(Width, Height, fullscreen);
            }
        }
        public override void OnGUI()
        {
            int x = Screen.width - 30;
            int y = Screen.height - 10;
            GUI.Label(new Rect(x, y, 20, 10),"Press INSERT to toggle WindowFix UI");
            if (ToggleUI)
            {
                GUI.Window(0, new Rect(Screen.width / 2, Screen.height / 2, 256, 165), DrawUI,"WindowFix");
            }
        }
        public override void OnUpdate()
        {
            if(Input.GetKey(KeyCode.Insert))
            {
                ToggleUI = !ToggleUI;
            }
            
        }
        
        public void DrawUI(int unknown)
        {
            GUILayout.Label("Width");
            Width = int.Parse(GUILayout.TextField(Width.ToString()));
            GUILayout.Label("Height");
            Height = int.Parse(GUILayout.TextField(Height.ToString()));


            if(GUILayout.Button("Fullscreen? " + fullscreen))
            {
                fullscreen = !fullscreen;
            }
            if(GUILayout.Button("Save settings"))
            {
                string setting = $"{Height}\n{Width}\n{(fullscreen)}";
                System.IO.File.WriteAllText("WinFix.cfg", setting);
                if (File.Exists("WinFix.cfg"))
                {
                    string file = File.ReadAllText("WinFix.cfg");
                    Height = int.Parse(file.Split('\n')[0]);
                    Width = int.Parse(file.Split('\n')[1]);
                    bool newfullscreen = bool.Parse(file.Split('\n')[2]);
                    Screen.fullScreen = newfullscreen;
                    Screen.SetResolution(Width, Height, newfullscreen);
                }
            }
        }
        public bool IntToBool(int value)
        {
            if (value == 1)
                return true;
            else
                return false;
        }
        public int BoolToInt(bool value)
        {
            return value ? 1 : 0;
        }
    }
}
