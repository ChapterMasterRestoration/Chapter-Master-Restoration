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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.World
{
    public enum Type
    {
        LAVA = 0, // 1,2
        TEMPERATE = 1, // 9
        DESERT = 2, // 3
        FORGE = 3, // 4
        HIVE = 4, // 5
        DEATH = 5, // 6
        AGRI = 6, // 7
        NECRON = 7, // 14 // special!
        FEUDAL = 8, // 8
        DESERT2 = 9, // 3 like desert for now
        ICE = 10, // 10
        WATER = 11, // 10 same as ice?
        DEAD = 12, // 11
        DESERT3 = 13, // 3 like desert for now
        DAEMON = 14, // 12
        SHRINE = 15, // 17
        SPACEHULK = 16 // 15 placeholder
    }
    /* -1 = undefined 13 = eldar craftworld, */
    
    public class Planet
    {
        public int systemId;
        public int planetId;
        public Type Type;
        public int Population;
        public string FactionOwner; // Planetary ownership could be extended later on with combat. Dictionary<string Faction, float controlpercentage>

        public Planet(Type Type, int systemId, int planetId, string FactionOwner = "Imperium") // Ave Imperator.
        {
            this.Type = Type;
            this.systemId = systemId;
            this.planetId = planetId;
            this.FactionOwner = FactionOwner;
        }
        public static int TypeToTexture(Type type)
        {
            if (type == Type.TEMPERATE) return (int) Type.FEUDAL; // Same bloody texture.
            return (int)type;
        }
        public string GetTypeName()
        {
            return Constants.PlanetTypeNames[(int)Type];
        }
        public int GetTypeTexture()
        {
            return Constants.PlanetTypeTextures[(int)Type];
        }
        public string GetName()
        {
            return ChapterMaster.Sector.Systems[systemId].name + " " + Constants.PlanetNames[planetId] + "  (" + GetTypeName() + ")";
        }
        public System GetSystem()
        {
            return ChapterMaster.Sector.Systems[systemId];
        }
        public void OpenPlanetScreen(ViewController view, Desktop desktop, System system)
        {
            //Debug.WriteLine("planet in system " + systemId);
            //parentScreen.AddChildScreen(new PlanetScreen(2, "planetscreen", new PlanetScreenAlign(parentScreen, planetId), systemId, planetId));

            var PlanetWindow = new Window
            {
                Background = new TextureRegion(Assets.GetTexture("planetscreen"), new Rectangle(0,0,320, 294)),
                //Title = system.name + " " + Constants.PlanetNames[planetId],
                Width = 560,
                Height = 560

            };

            var Panel = new Panel
            {

            };

            var Columns = new HorizontalStackPanel
            {

            };

            var DispositionCol = new VerticalStackPanel
            {
                Spacing = 10
            };

            var Disposition = new Label
            {
                Text = "Disposition ??/100",
                Border = new SolidBrush(new Color(128, 128, 128)),
                BorderThickness = new Thickness(4),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(14, 0, 0, 0)
            };
            DispositionCol.AddChild(Disposition);

            var PlanetTypeImage = new Image
            {
                Background = new TextureRegion(Assets.PlanetTypeTextures[GetTypeTexture()]),
                Width = 128,
                Height = 128,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(14,0,0,0)
            };

            DispositionCol.AddChild(PlanetTypeImage);



            Columns.AddChild(DispositionCol);

            // Info Col

            var InfoCol = new VerticalStackPanel
            {
                Spacing = 5,
                Margin = new Thickness(0,38,0,0)
            };

            var Title = new Label
            {
                Text = system.name + " " + Constants.PlanetNames[planetId], 
                HorizontalAlignment = HorizontalAlignment.Left
            };

            InfoCol.AddChild(Title);

            var Controller = new Label
            {
                Text = "Faction: " + FactionOwner,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            InfoCol.AddChild(Controller);

            var PopulationLabel = new Label
            {
                Text = "Population: " + Population, 
                HorizontalAlignment = HorizontalAlignment.Left
            };

            InfoCol.AddChild(PopulationLabel);

            var DefenseForceLabel = new Label
            {
                Text = "Defense Force: " + Population,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            InfoCol.AddChild(DefenseForceLabel);



            Columns.AddChild(InfoCol);


            Panel.AddChild(Columns);

            var QuickStrikes = new ImageTextButton
            {
                Text = "Quick Strikes",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(14,0,0,14)
            };

            Panel.AddChild(QuickStrikes);

            var StartCampaign = new ImageTextButton
            {
                Text = "Start Campaign",
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 0, 14)
            };

            Panel.AddChild(StartCampaign);

            

            PlanetWindow.Content = Panel;
            PlanetWindow.ShowModal(desktop); // TODO: spawn to the left of system screen
        }
        public void ClosePlanetScreen(ViewController view)
        {
            //Debug.WriteLine("closing planet in system " + systemId);
        }
    }
}
