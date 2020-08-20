﻿using ChapterMaster.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.Render
{
    class RenderHelper
    {
        public static void DrawStar(SpriteBatch spriteBatch, Rectangle rect, int color, Align align = null, ViewController view = null)
        {
            spriteBatch.Draw(ChapterMaster.SystemTextures[color], rect, Color.White);
        }
        public static void DrawStar(SpriteBatch spriteBatch,Vector2 position, int color,Align align = null, ViewController view = null)
        {
            if (align == null)
            {
                spriteBatch.Draw(ChapterMaster.SystemTextures[color], position, Color.White);
            } else if(align != null && view != null)
            {
                spriteBatch.Draw(ChapterMaster.SystemTextures[color], align.GetRect(view), Color.White);
            }
        }
    }
}