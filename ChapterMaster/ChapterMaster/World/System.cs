using ChapterMaster.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChapterMaster.World
{
    public class System
    {
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
        public PlanetsScreen OpenPlanetsScreen(ViewController view, int systemId)
        {
            if (!view.PlanetScreenOpen)
            {
                PlanetsScreen planetsScreen = new PlanetsScreen(1, "planetsscreen", systemId, new PlanetScreenAlign(systemId));
                ChapterMaster.MainScreen.AddChildScreen(planetsScreen);
                return planetsScreen;
            }
            return null;
        }
        public void ClosePlanetsScreen(ViewController view)
        {

        }
    }
}
