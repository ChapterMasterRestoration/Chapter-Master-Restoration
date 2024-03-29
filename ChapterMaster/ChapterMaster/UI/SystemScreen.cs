﻿using ChapterMaster.Render;
using ChapterMaster.UI.Align;
using ChapterMaster.Util;
using ChapterMaster.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChapterMaster.UI
{
    public class SystemScreen : Screen
    {
        public int systemId;
        public bool notInWorld; // TODO: implement System Screen rendering indpendently
        List<PlanetAlign> planetAligns = new List<PlanetAlign>();
        InvisibleButton exitButton;
        public SystemScreen(int screenId, string backgroundTexture, int systemId, Align.Align align) : base(screenId, backgroundTexture, align)
        {
            this.screenId = screenId;
            this.backgroundTexture = backgroundTexture;
            this.systemId = systemId;
            exitButton = new InvisibleButton(new CornerAlign(Corner.BOTTOMRIGHT,64,25),ExitScreen);
            AddButton(exitButton);
        }
        // Implement planets as buttons?
        public override void Render(SpriteBatch spriteBatch, ViewController view)
        {
            World.System system = ChapterMaster.Sector.Systems[systemId];
            ((SystemScreenAlign)align).pinned = notInWorld;
            Rect = align.GetRect(view);
            exitButton.position = MathUtil.Add(Rect.Location, new Vector2(247, 261));
            //pinButton.position = new Vector2(247, 20);
            // TODO: replace with align
            spriteBatch.Draw(Assets.UITextures[backgroundTexture + system.Planets.Count], Rect, Color.White); // TODO: This will go awry if a system has 5 planets. Add a new System Screen. Most likely. Crashes when you have 0 planets. Need to implement minimum.
            Vector2 stringSize = Assets.CaslonAntiqueBold.MeasureString(system.name + " System");
            //Debug.WriteLine(stringSize.X);
            spriteBatch.DrawString(Assets.CaslonAntiqueBold, system.name + " System", MathUtil.Add(Rect.Location, new Vector2(80, 12)), Color.Gray);
            // TODO: replace with align component
            Point position = Rect.Location + new Point(50 - Constants.SystemSize / 2, 120 - Constants.SystemSize/2);
            Vector2 pos = new Vector2(position.X, position.Y);
            // TODO: better way to pass the system's color
            RenderHelper.DrawStar(spriteBatch, pos, ChapterMaster.Sector.Systems[systemId].color);
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
                spriteBatch.DrawString(Assets.CaslonAntiqueBold, Constants.PlanetNames[system.Planets[noPlanet].planetId], planetAlign.planetPos + new Vector2(16,32), Color.Gray);
                planetAligns.Add(planetAlign);

            }
            foreach (Button button in Buttons)
            {
                //button.Render(spriteBatch, view);
            }
            foreach (Screen screen in Screens)
            {
                //screen.Render(spriteBatch, view);
            }
        }
        bool pinKeyPressed = false;
        public override void Update(ViewController view)
        {
            base.Update(view);
            int mouseX = Mouse.GetState().X;
            int mouseY = Mouse.GetState().Y;
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (Rect.Contains(new Point(mouseX, mouseY)))
                {
                    Predicate<UI.Screen> predicate = delegate (UI.Screen screen) { return screen is UI.PlanetScreen; };
                    Screens.RemoveAll(predicate);
                } else
                {
                    bool overPlanetScreen = false;
                    for(int i = 0; i < Screens.Count; i ++)
                    {
                        if(Screens[i] is PlanetScreen)
                        {
                            if(Screens[i].Rect.Contains(new Point(mouseX, mouseY)))
                                overPlanetScreen = true;
                        }
                    }
                    if(!overPlanetScreen) // TODO implement
                        ChapterMaster.Sector.Systems[systemId].CloseSystemScreen(view);
                }
            }
            foreach (PlanetAlign planetAlign in planetAligns)
            {
                if (planetAlign.GetRect(view).Contains(new Point(view.GetMouse().X, view.GetMouse().Y)))
                {
                    if (view.GetMouse().LeftButton == ButtonState.Pressed)
                    {
                        WasModified = Parent.WasModified = true;
                        //ChapterMaster.Sector.Systems[systemId].Planets[planetAlign.planetNo].OpenPlanetScreen(view);
                    }
                }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.P)) pinKeyPressed = false;
            if(Keyboard.GetState().IsKeyDown(Keys.P) && !pinKeyPressed)
            {
                notInWorld = !notInWorld;
                pinKeyPressed = true;
            }
        }
        public override void ExitScreen(MouseState mouseState, object sender)
        {
            base.ExitScreen(mouseState, sender);
            ChapterMaster.Sector.Systems[systemId].CloseSystemScreen(ChapterMaster.ViewController);
        }
    }
}
