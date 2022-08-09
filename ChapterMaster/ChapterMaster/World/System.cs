using ChapterMaster.UI;
using ChapterMaster.UI.Align;
using Microsoft.Xna.Framework;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using ChapterMaster.Render;

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
                    //Title = name,
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

                var Title = new Label
                {
                    Text = name,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(14, -12, 0, 0)
                };

                Panel.AddChild(Title);


                var System = new Image
                {
                    Background = new TextureRegion(Assets.SystemTextures[color]),
                    Width = 128,
                    Height = 128,
                    Margin = new Thickness((560/320)*50 - Constants.SystemSize / 2, 138, 0, 0)
                };



                Panel.AddChild(System);

                // Add Planets

                for(int i = 0; i < Planets.Count; i++)
                {
                    var PlanetButton = new ImageButton
                    {
                        Background = new TextureRegion(Assets.PlanetTextures[Planet.TypeToTexture(Planets[i].Type)]),
                        OverImage = new TextureRegion(Assets.PlanetTextures[Planet.TypeToTexture(Planets[i].Type)]),
                        Width = 32,
                        Height = 32,
                        Margin = new Thickness(222 + 64 * i, 180, 0, 0)
                    };
                    PlanetButton.UserData["id"] = i.ToString();

                    PlanetButton.TouchDown += (s, e) =>
                    {
                        int id = Int32.Parse(((ImageButton)s).UserData["id"]);
                        Planets[id].OpenPlanetScreen(view, desktop, this);
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

        
        public void OpenStartCampaign(ViewController view, Desktop desktop, Planet planet)
        {
            var StartCampaignWindow = new Window()
            {
                Width = view.viewPortWidth,
                Height = view.viewPortHeight,
            };

            var Panel = new Panel
            {

            };

            var Title = new Label
            {
                Text = name + " Campaign",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0,-12,0,0)
            };

            Panel.AddChild(Title);

            int currentPlanet = planet.planetId;

            // Planet Provinces

            var PlanetProvinces = new Panel()
            {
                Width = 600,
                Height = 600,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };

            var PlanetImage = new Image()
            {
                Background = new TextureRegion(Assets.PlanetTextures[Planet.TypeToTexture(planet.Type)], new Rectangle(0, 0, 32, 32)),
                Width = 600,
                Height = 600,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Id = "Planet"
            };
            
            Panel.AddChild(PlanetImage);

            for (int i = 0; i < 2; i++)
            {
                var ProvinceButton = new ImageTextButton()
                {
                    Background = new SolidBrush(Color.FromNonPremultiplied(200, 100, 0, 40)),
                    Text = "Province  " + (i+1),
                    Width = 300,
                    Height = 100,
                    Margin = new Thickness(0,200 + 200*i,0,0),
                    HorizontalAlignment = HorizontalAlignment.Center,
                };

                PlanetProvinces.AddChild(ProvinceButton);
            }

            Panel.AddChild(PlanetProvinces);

            var PlanetList = new VerticalStackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Right
            };


            for (int i = 0; i < Planets.Count; i++)
            {
                var PlanetButton = new ImageTextButton
                {
                    Text = Planets[i].GetName(),
                };

                PlanetButton.UserData["id"] = i.ToString();

                PlanetButton.TouchDown += (s, e) =>
                {
                    currentPlanet = Int32.Parse(((ImageTextButton)s).UserData["id"]);
                    PlanetImage.Background = new TextureRegion(Assets.PlanetTextures[Planet.TypeToTexture(Planets[currentPlanet].Type)], new Rectangle(0, 0, 32, 32));
                };

                PlanetList.AddChild(PlanetButton);
            }

            Panel.AddChild(PlanetList);

            StartCampaignWindow.Content = Panel;
            StartCampaignWindow.ShowModal(desktop);
        }
    }
}
