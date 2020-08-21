using ChapterMaster.Render;
using ChapterMaster.Util;
using ChapterMaster.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChapterMaster.UI
{
    public class PlanetsScreen : Screen
    {
        public int systemId;
        List<PlanetAlign> planetAligns = new List<PlanetAlign>();
        public PlanetsScreen(int screenId, string backgroundTexture, int systemId, Align align) : base(screenId, backgroundTexture, align)
        {
            this.screenId = screenId;
            this.backgroundTexture = backgroundTexture;
            this.systemId = systemId;
        }
        // Implement planets as buttons?
        public override void Render(SpriteBatch spriteBatch, ViewController view)
        {
            World.System system = ChapterMaster.sector.Systems[systemId];
            Rect = align.GetRect(view);
            // TODO: replace with align
            spriteBatch.Draw(ChapterMaster.UITextures[backgroundTexture], Rect, Color.White);
            Vector2 stringSize = ChapterMaster.caslon_antique_regular.MeasureString(system.name + " System");
            //Debug.WriteLine(stringSize.X);
            spriteBatch.DrawString(ChapterMaster.caslon_antique_regular, system.name + " System", MathUtil.Add(Rect.Location, new Vector2(Rect.Width / 2 - stringSize.X - 5, 2)), Color.White);
            // TODO: replace with align component
            Point position = Rect.Location + new Point(50 - Constants.SystemSize / 2, 120 - Constants.SystemSize/2);
            Vector2 pos = new Vector2(position.X, position.Y);
            // TODO: better way to pass the system's color
            RenderHelper.DrawStar(spriteBatch, pos, ChapterMaster.sector.Systems[systemId].color);
            planetAligns.Clear();
            for (int noPlanet = 0; noPlanet < system.Planets.Count; noPlanet++)
            {
                // calculate orbit arc
                //float r = 40; // TODO: Implement.
                //float x = (float) Math.Sqrt(Math.Pow((double) r, 2) - Math.Pow((double) 20, 2));
                //float startAngle = (float) Math.Acos(x / r);
                //float endAngle = 2 * startAngle;
                PlanetAlign planetAlign = new PlanetAlign(noPlanet, pos, 80, 42, 120 - Constants.SystemSize);
                RenderHelper.DrawPlanet(spriteBatch, new Vector2(),
                    Planet.TypeToTexture(system.Planets[noPlanet].Type), planetAlign, view);
                planetAligns.Add(planetAlign);

            }
            foreach (Button button in Buttons)
            {
                button.Render(spriteBatch, view);
            }
            foreach (Screen screen in Screens)
            {
                screen.Render(spriteBatch, view);
            }
        }
        public override void Update(ViewController view)
        {
            base.Update(view);
            foreach (PlanetAlign planetAlign in planetAligns)
            {
                if (planetAlign.GetRect(view).Contains(new Point(view.GetMouse().X, view.GetMouse().Y)))
                {
                    if (view.GetMouse().LeftButton == ButtonState.Pressed)
                    {
                        ChapterMaster.sector.Systems[systemId].Planets[planetAlign.planetNo].OpenPlanetScreen(view);
                    }
                }
            }
        }
    }
}
