using ChapterMaster.Render;
using ChapterMaster.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.UI
{
    public class PlanetsScreen : Screen
    {
        public int systemId;
        public PlanetsScreen(int screenId, string backgroundTexture, int systemId, Align align) : base(screenId, backgroundTexture, align)
        {
            this.screenId = screenId;
            this.backgroundTexture = backgroundTexture;
            this.systemId = systemId;
        }
        public override void Render(SpriteBatch spriteBatch, ViewController view)
        {
            World.System system = ChapterMaster.sector.Systems[systemId];
            // TODO: replace with align
            Rect = view.TransformedOriginRect(system.x, 
                                              system.y, 300, 100, false);
            spriteBatch.Draw(ChapterMaster.UITextures[backgroundTexture], Rect, Color.White);
            Vector2 stringSize = ChapterMaster.caslon_antique_regular.MeasureString(system.name + " System");
            Debug.WriteLine(stringSize.X);
            spriteBatch.DrawString(ChapterMaster.caslon_antique_regular, system.name + " System", MathUtil.Add(Rect.Location,new Vector2(Rect.Width/2 - stringSize.X - 5, 2)), Color.White);
            // TODO: replace with align component
            Point position = Rect.Location + new Point(0, 20);
            Vector2 pos = new Vector2(position.X, position.Y);
            // TODO: better way to pass the system's color
            RenderHelper.DrawStar(spriteBatch,pos,ChapterMaster.sector.Systems[systemId].color);
            //for (int noPlanet = 0; noPlanet < system.Planets.Count; noPlanet ++)
            //{
                // calculate orbit arc
                float r = 40; // TODO: Implement.
                float x = (float) Math.Sqrt(Math.Pow((double) r, 2) - Math.Pow((double) 20, 2));
                float startAngle = (float) Math.Acos(x / r);
                float endAngle = 2 * startAngle;
                MonoGame.Primitives2D.DrawArc(spriteBatch, 
                    pos + new Vector2(Constants.SystemSize/2, Constants.SystemSize / 2), 
                    r,40,startAngle,endAngle, Color.White,1);
            //}
            foreach (Button button in Buttons)
            {
                button.Render(spriteBatch, view);
            }
            foreach (Screen screen in Screens)
            {
                screen.Render(spriteBatch, view);
            }
        }
    }
}
