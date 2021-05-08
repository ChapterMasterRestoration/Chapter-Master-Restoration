using ChapterMaster.Render;
using ChapterMaster.State;
using ChapterMaster.UI.Align;
using ChapterMaster.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ChapterMaster.UI
{
    public class PlanetScreen : Screen
    {
        int systemId;
        int planetId;
        public PlanetScreen(int screenId, string backgroundTexture, Align.Align align, int systemId, int planetId) : base(screenId, backgroundTexture, align)
        {
            this.systemId = systemId;
            this.planetId = planetId;
            AddButton(new Button("", "Attack", new RectAlign(this,new Vector2(200,270),20, 20), Attack));
        }

        public override void Render(SpriteBatch spriteBatch, ViewController view)
        {
            World.System system = ChapterMaster.Sector.Systems[systemId];
            World.Planet planet = ChapterMaster.Sector.Systems[systemId].Planets[planetId];
            Rect = align.GetRect(view);
            // background
            spriteBatch.Draw(Assets.UITextures[backgroundTexture], Rect, Color.White);
            // disposition height
            string disposition = "Disposition ???/100 ";
            int dH = (int) Assets.Caslon_Antique_Regular.MeasureString(disposition).Y;
            // planet type texture
            Vector2 pos = MathUtil.Add(Rect.Location, new Vector2(8, 8 + dH + 2));
            spriteBatch.Draw(Assets.PlanetTypeTextures[planet.GetTypeTexture()], pos, Color.Gray);
            RenderHelper.PrimitiveBuddy.Rectangle(MathUtil.VectorToRectangle(pos, new Vector2(128, 128)), Color.Gray);
            //disposition
            spriteBatch.DrawString(Assets.Courier_New, disposition,
            MathUtil.Add(Rect.Location, new Vector2(123, 9)), Color.White);
            RenderHelper.PrimitiveBuddy.Rectangle(MathUtil.VectorToRectangle(new Vector2(pos.X, Rect.Location.Y + 8), new Vector2(290, dH)), Color.Gray);
            // title
            Vector2 titlePos = MathUtil.Add(Rect.Location, new Vector2(128 + 8 + 1, 8 + dH + 2 + 2));
            string title = planet.GetName();
            spriteBatch.DrawString(Assets.Caslon_Antique_Bold, title, titlePos, Color.Gray);
            int tH = (int)Assets.Caslon_Antique_Regular.MeasureString(title).Y; // title height
            int cH = (int)Assets.Caslon_Antique_Regular.MeasureString(planet.FactionOwner).Y; // controller name height
            Vector2 controllerPos = MathUtil.Offset(titlePos, 0, tH);
            spriteBatch.DrawString(Assets.Caslon_Antique_Bold, planet.FactionOwner, controllerPos, Color.Gray);
            // population
            spriteBatch.DrawString(Assets.Caslon_Antique_Regular, "Population: " + planet.Population, MathUtil.Offset(controllerPos,0, cH + 2), Color.Gray);
            // defense force
            spriteBatch.DrawString(Assets.Caslon_Antique_Regular, "Defense Force: " + planet.Population, MathUtil.Offset(controllerPos, 0, cH + cH + 2), Color.Gray);
            foreach (Button button in Buttons)
            {
                button.Render(spriteBatch, view);
            }
        }
        public override void Update(ViewController view)
        {
            base.Update(view);
        }
        public void Attack(MouseState state, object sender)
        {
            GameState gameState = (GameState)Program.GameManager.GetState();
            Program.GameManager.ChangeState(new GroundCombatState(Program.GameManager, gameState.GraphicsDevice, gameState.ContentManager, ChapterMaster.Sector.Systems[systemId].Planets[planetId]));
        }
    }
}
