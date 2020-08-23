using ChapterMaster.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChapterMaster.World
{
    public class System
    {
        public int id; // TODO: assign in constructor
        public int color;
        public string name;
        public int x, y;
        public int numberOfLanes = 0;
        public List<Planet> Planets = new List<Planet>();
        public System(int color, int x, int y)
        {
            this.color = color;
            this.x = x;
            this.y = y;
        }
        public System(int color, string name, int x, int y)
        {
            this.color = color;
            this.x = x;
            this.y = y;
            this.name = name;
        }
        public SystemScreen OpenSystemScreen(ViewController view, int systemId)
        {
            if (!view.PlanetScreenOpen)
            {
                SystemScreen planetsScreen = new SystemScreen(1, "systemscreen", systemId, new SystemScreenAlign(ChapterMaster.MainScreen.align,systemId));
                ChapterMaster.MainScreen.AddChildScreen(planetsScreen);
                return planetsScreen;
            }
            return null;
        }
        public void CloseSystemScreen(ViewController view)
        {
            Predicate<UI.Screen> predicate = delegate (UI.Screen screen) {
                if (screen is UI.SystemScreen) {
                    Debug.WriteLine($"found system screen in {id}");
                    if (((SystemScreen)screen).systemId == id)
                    {
                        Debug.WriteLine($"closing system screen for {id}");
                        return true;
                    }
                    else return false;
                } else { return false; }
            };
            ChapterMaster.MainScreen.Screens.RemoveAll(predicate);
            view.PlanetScreenOpen = false;
            view.openSystem = -1;
        }
    }
}
