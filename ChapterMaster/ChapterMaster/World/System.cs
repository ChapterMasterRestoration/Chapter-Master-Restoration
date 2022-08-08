using ChapterMaster.UI;
using ChapterMaster.UI.Align;
using Myra.Graphics2D;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
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
        public void OpenSystemScreen(ViewController view, int systemId, Desktop desktop)
        {
            if (!view.PlanetScreenOpen)
            {
                //SystemScreen planetsScreen = new SystemScreen(1, "systemscreen", systemId, new SystemScreenAlign(ChapterMaster.MainScreen.align,systemId));
                //ChapterMaster.MainScreen.AddChildScreen(planetsScreen);

                var SystemWindow = new Window
                {
                    Background = new TextureRegion(Assets.GetTexture("systemscreen" + Planets.Count)),
                    Title = name,
                    Width  = 560,
                    Height = 560
                };

                SystemWindow.Closed += (s, e) =>
                {
                    view.PlanetScreenOpen = false;
                    view.openSystem = -1;
                };

                var Panel = new Panel
                {

                };


                var System = new Image
                {
                    Background = new TextureRegion(Assets.SystemTextures[color]),
                    Width = 128,
                    Height = 128,
                    Margin = new Thickness((560/320)*50 - Constants.SystemSize / 2, 138, 0, 0)
                };

                Panel.AddChild(System);

                // Add Planets

                for(int i = 0; i < Planets.Count - 1; i++)
                {
                    var PlanetButton = new ImageButton
                    {
                        Background = new TextureRegion(Assets.PlanetTextures[Planet.TypeToTexture(Planets[i].Type)]),
                        OverImage = new TextureRegion(Assets.PlanetTextures[Planet.TypeToTexture(Planets[i].Type)]),
                        Width = 32,
                        Height = 32,
                        Margin = new Thickness(222 + 64 * i,180,0,0)
                    };
                    Debug.WriteLine("i=" + i);

                    PlanetButton.TouchDown += (s, e) =>
                    {
                        Planets[i].planetId = i;
                        Planets[i].OpenPlanetScreen(view, desktop, this);
                    };

                    var PlanetLabel = new Label
                    {
                        Text = Constants.PlanetNames[i],
                        Margin = new Thickness(222 + 64 * i + 16, 212)
                    };

                    Debug.WriteLine("planet i: " + i);
                    Panel.AddChild(PlanetLabel);
                    Panel.AddChild(PlanetButton);
                }


                SystemWindow.Content = Panel;

                SystemWindow.ShowModal(desktop);

                //return planetsScreen;
            }
            //return null;
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
                factions[Planets[planetindex].FactionOwner] += (float)1 / (float)Planets.Count;
            }
            return factions;
        }
    }
}
