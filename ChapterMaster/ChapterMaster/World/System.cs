using ChapterMaster.UI;
using ChapterMaster.UI.Align;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChapterMaster.World
{
    public class System
    {
        public int id; // TO DO: assign in constructor
        public int color;
        public string name;
        public int x, y;
        public int numberOfLanes = 0;
        public List<Planet> Planets = new List<Planet>();
        public bool forgeProne;
        public string DeJureFactionOwner;

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
        public Dictionary<string, float> FindOwners()
        {
            Dictionary<string, float> factions = new Dictionary<string, float>();
            for (int planetindex = 0; planetindex < Planets.Count; planetindex++)
            {
                if (!factions.ContainsKey(Planets[planetindex].FactionOwner))
                {
                    factions.Add(Planets[planetindex].FactionOwner, 0);
                }
                factions[Planets[planetindex].FactionOwner] = (float)1 / (float)Planets.Count;
            }
            return factions;
        }
    }
}
