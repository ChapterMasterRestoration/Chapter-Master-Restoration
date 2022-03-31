using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.UI;

namespace ChapterMaster.State
{
    using Asset = Tuple<string, int>;
    public class LoadingState : State
    {
        GameManager gameManager;
        Stack<Asset> assetsToLoad = new Stack<Asset>();
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        PrimitiveBuddy.Primitive primitive;
        int progress;

        public LoadingState(GameManager gameManager, GraphicsDevice graphicsDevice, ContentManager contentManager) : base(gameManager, graphicsDevice, contentManager)
        {
            this.gameManager = gameManager;
            this.graphicsDevice = graphicsDevice;
            assetsToLoad.Push(new Asset("Fleet Textures", 17));
            assetsToLoad.Push(new Asset("Background", 15));
            assetsToLoad.Push(new Asset("Planet Climate Textures", 18));
            assetsToLoad.Push(new Asset("Planet Textures", 10));
            assetsToLoad.Push(new Asset("System Textures", 5));

            assetsToLoad.Push(new Asset("Button Textures", 5));
            assetsToLoad.Push(new Asset("UI Textures", 25));
            assetsToLoad.Push(new Asset("Fonts", 5));

            assetsToLoad.Push(new Asset("Faction Icons", 11));
            // deleted 52, 36, 45, 10, 17, 18, 19, 20 bro wtf
            // 0 is the border
            // 1 to 9 is faction icons
            Loader.CONTENT_ROOT = contentManager.RootDirectory;
            spriteBatch = new SpriteBatch(graphicsDevice);
            #region Load Fonts
            Assets.CaslonAntiqueRegular = contentManager.Load<SpriteFont>("font/caslon-antique.regular");
            Assets.CaslonAntiqueBold = contentManager.Load<SpriteFont>("font/caslon-antique.bold");
            Assets.ARJULIAN = contentManager.Load<SpriteFont>("font/ARJULIAN");
            Assets.CourierNew = contentManager.Load<SpriteFont>("font/cour");

            byte[] ttfData = File.ReadAllBytes("Content/font/caslon-antique.bold.ttf");
            Assets.CaslonAntiqueBoldFSS = new FontSystem();
            Assets.CaslonAntiqueBoldFSS.AddFont(ttfData);

            #endregion
            primitive = new PrimitiveBuddy.Primitive(graphicsDevice, spriteBatch);
            Assets.LoadingScreen = Loader.LoadPNG("loading/loading1");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Assets.LoadingScreen, new Rectangle(0, 0, GameManager.GetWidth(), GameManager.GetHeight()), Color.White);
            DrawProgressBar(spriteBatch, GameManager.GetWidth() / 2, GameManager.GetHeight() / 2, 200, 80, progress, Color.Gray, Color.Yellow);
            spriteBatch.End();
        }

        private void DrawProgressBar(SpriteBatch spriteBatch, int x, int y, int width, int height, int progress, Color background, Color fill)
        {
            primitive.Rectangle(new Rectangle(x, y, width, height), background);
            primitive.Rectangle(new Rectangle(x+2, y+2, (width-2)*(progress/100), height-2), fill);
        }

        public override void Update(GameTime gameTime)
        {
            if (assetsToLoad.Count > 0)
            {
                Asset asset = assetsToLoad.Pop();
                switch (asset.Item1)
                {
                    case "Fleet Textures":
                        for (int faction = 0; faction < Constants.FleetTexture.Length; faction++)
                        {
                            Assets.FleetTextures[faction] = new Texture2D[Constants.FleetStateLimit[faction]];
                            for (int state = 0; state < Constants.FleetStateLimit[faction]; state++)
                            {
                                Assets.FleetTextures[faction][state] = Loader.LoadPNG("spr_fleet_" + Constants.FleetTexture[faction] + "_" + state);
                            }
                        }
                        break;
                    case "Background":
                        Assets.Background = Loader.LoadPNG("background/bg_space");
                        break;
                    case "Planet Climate Textures":
                        for (int i = 1; i < Assets.PlanetTypeTextures.Length; i++)
                        {
                            Assets.PlanetTypeTextures[i] = Loader.LoadPNG("ui/planet" + i);
                        }
                        break;
                    case "Planet Textures":
                        for (int i = 0; i < Assets.PlanetTextures.Length; i++)
                        {
                            Assets.PlanetTextures[i] = Loader.LoadPNG("spr_planets_" + i);
                        }
                        break;
                    case "System Textures":
                        for (int i = 0; i < Assets.SystemTextures.Length; i++)
                        {
                            Assets.SystemTextures[i] = Loader.LoadPNG("spr_star_" + i);
                        }
                        break;
                    case "Button Textures":
                        for (int i = 0; i < 4; i++)
                        {
                            Assets.ButtonTextures.Add("ui_but_"+i,Loader.LoadPNG("spr_ui_but_" + (i + 1) + "_0"));
                        }
                        Assets.ButtonTextures.Add("pin_button", Loader.LoadPNG("spr_pin_button"));
                        Assets.ButtonTextures.Add("new_game",Loader.LoadPNG("spr_mm_butts_0"));
                        Assets.ButtonTextures.Add("load_game",Loader.LoadPNG("spr_mm_butts_1"));
                        Assets.ButtonTextures.Add("about", Loader.LoadPNG("spr_mm_butts_2"));
                        Assets.ButtonTextures.Add("exit", Loader.LoadPNG("spr_mm_butts_3"));
                        Assets.ButtonTextures.Add("back",Loader.LoadPNG("spr_mm_butts_4"));
                        Assets.ButtonTextures.Add("space_marine",Loader.LoadPNG("spr_master_splash_0"));
                        Assets.ButtonTextures.Add("textbox", Loader.LoadPNG("spr_new_banner_0"));
                        Assets.ButtonTextures.Add("creation_arrow_right", Loader.LoadPNG("spr_creation_arrow_1"));
                        Assets.ButtonTextures.Add("creation_arrow_left", Loader.LoadPNG("spr_creation_arrow_0"));
                        break;
                    case "UI Textures":
                        Assets.UITextures.Add("mapframe", Loader.LoadPNG("spr_new_ui_1"));
                        Assets.UITextures.Add("systemscreen1", Loader.LoadPNG("spr_star_screen_1"));
                        Assets.UITextures.Add("systemscreen2", Loader.LoadPNG("spr_star_screen_2"));
                        Assets.UITextures.Add("systemscreen3", Loader.LoadPNG("spr_star_screen_3"));
                        Assets.UITextures.Add("systemscreen4", Loader.LoadPNG("spr_star_screen_4"));
                        Assets.UITextures.Add("planetscreen", Loader.LoadPNG("spr_planet_screen_1")); // modified texture by removing extra space
                        Assets.UITextures.Add("title_splash", Loader.LoadPNG("ui/title_splash"));
                        Assets.UITextures.Add("spr_rock_bg_0", Loader.LoadPNG("ui/spr_rock_bg_0"));
                        Assets.UITextures.Add("force_background", Loader.LoadPNG("ui/force_background_r"));
                        Assets.UITextures.Add("ledger_background", Loader.LoadPNG("ui/force_background"));
                        // combat
                        Assets.UITextures.Add("bg_combat_grass", Loader.LoadPNG("combat/Zelda Textures/Zelda Texture - Grass 11x Scale"));
                        Assets.UITextures.Add("gc_spacemarine", Loader.LoadPNG("combat/troop/groundcombat_spacemarine")); // spr_mar_collision_0
                        Assets.UITextures.Add("gc_squadleader", Loader.LoadPNG("combat/troop/groundcombat_squadleader"));
                        Assets.UITextures.Add("order_move_arrow", Loader.LoadPNG("combat/order_move_arrow")); // Move this to a different loading stage
                        
                        //Assets.UITextures.Add("", Loader.LoadPNG(""));
                        Assets.UITextures.Add("campaign_picker", Loader.LoadPNG("spr_popup_medium_0")); // Move this to the UI folder, you utter beefbroth.
                        Assets.UITextures.Add("faction_creator_background", Loader.LoadPNG("spr_settings_bg_0"));
                        Assets.UITextures.Add("black_background", Loader.LoadPNG("background/black_background"));
                        break;
                    case "Fonts":
                        break;
                    case "Faction Icons":
                        Assets.UITextures.Add("faction_icon_selected", Loader.LoadPNG("factions/spr_icon_old_0"));
                        for(int i = 1; i < 10; i++)
                        {
                            Assets.IconTextures.Add("founding_chapter_" + i, Loader.LoadPNG("factions/spr_icon_old_" + i));
                        }
                        Assets.IconTextures.Add("successor_chapter_1", Loader.LoadPNG("factions/spr_icon_old_11"));
                        Assets.IconTextures.Add("successor_chapter_2", Loader.LoadPNG("factions/spr_icon_old_12"));
                        Assets.IconTextures.Add("successor_chapter_3", Loader.LoadPNG("factions/spr_icon_old_13"));
                        Assets.IconTextures.Add("successor_chapter_4", Loader.LoadPNG("factions/spr_icon_old_15"));
                        break;
                    default:
                        throw new Exception($"Asset type {asset.Item1} is not recognized");
                }
                progress += asset.Item2;
            } else
            {
                Debug.WriteLine("Switching to game state");
                gameManager.ChangeState(new MenuState(gameManager, gameManager.GraphicsDevice, gameManager.Content));
            }
        }

        public override void Resize(GameWindow window)
        {

        }

        public override Desktop GetDesktop()
        {
            throw new NotImplementedException();
        }
    }
}
