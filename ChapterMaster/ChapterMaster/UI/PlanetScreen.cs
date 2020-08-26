using ChapterMaster.Render;
using ChapterMaster.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChapterMaster.UI
{
    public class PlanetScreen : Screen
    {
        int systemId;
        int planetId;
        public PlanetScreen(int screenId, string backgroundTexture, Align align, int systemId, int planetId) : base(screenId, backgroundTexture, align)
        {
            this.systemId = systemId;
            this.planetId = planetId;
        }

        public override void Render(SpriteBatch spriteBatch, ViewController view)
        {
            World.System system = ChapterMaster.sector.Systems[systemId];
            World.Planet planet = ChapterMaster.sector.Systems[systemId].Planets[planetId];
            Rect = align.GetRect(view);
            // background
            spriteBatch.Draw(ChapterMaster.UITextures[backgroundTexture], Rect, Color.White);
            // disposition height
            string disposition = "Disposition ???/100";
            int dH = (int) ChapterMaster.Caslon_Antique_Regular.MeasureString(disposition).Y;
            // planet type texture
            Vector2 pos = MathUtil.Add(Rect.Location, new Vector2(8, 8 + dH + 2));
            spriteBatch.Draw(ChapterMaster.PlanetTypeTextures[planet.GetTypeTexture()], pos, Color.Gray);
            RenderHelper.PrimitiveBuddy.Rectangle(MathUtil.VectorToRectangle(pos, new Vector2(128, 128)), Color.Gray);
            //disposition
            spriteBatch.DrawString(ChapterMaster.Courier_New, disposition,
            MathUtil.Add(Rect.Location, new Vector2(123, 9)), Color.White);
            RenderHelper.PrimitiveBuddy.Rectangle(MathUtil.VectorToRectangle(new Vector2(pos.X, Rect.Location.Y + 8), new Vector2(290, dH)), Color.Gray);
            // title
            Vector2 titlePos = MathUtil.Add(Rect.Location, new Vector2(128 + 8 + 1, 8 + dH + 2 + 2));
            string title = system.name + " " + Constants.PlanetNames[planetId] + "  (" + planet.GetTypeName() + ")";
            spriteBatch.DrawString(ChapterMaster.Caslon_Antique_Bold, title, titlePos, Color.Gray);
            int tH = (int)ChapterMaster.Caslon_Antique_Regular.MeasureString(title).Y; // title height
            // population
            spriteBatch.DrawString(ChapterMaster.Caslon_Antique_Regular, "Population: " + planet.Population, MathUtil.Offset(titlePos,0,tH + 2), Color.Gray);
            // defense force
            spriteBatch.DrawString(ChapterMaster.Caslon_Antique_Regular, "Defense Force: " + planet.Population, MathUtil.Offset(titlePos, 0, tH + tH + 2), Color.Gray);
        }
        public override void Update(ViewController view)
        {

        }
    }
}
