using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FireAndExplotion.View.Particles
{
    class ExplotionSystem
    {
        //Array för smoke-objekt
        private Explotion[] explotions;
        //Antal Smoke-objekt
        private const int MAX_EXPLOTIONS = 40;

        //Initsierar arrayen med Smoke-objekt
        internal ExplotionSystem(Vector2 startPossition, int scale)
        {
            explotions = new Explotion[MAX_EXPLOTIONS];

            for (int i = 0; i < MAX_EXPLOTIONS; i++)
            {
                explotions[i] = new Explotion(i, startPossition, scale);
            }
        }

        //Uppdaterar alla Smoke-objekt i arrayen
        internal void Update(float elapsedGameTime)
        {
            for (int i = 0; i < MAX_EXPLOTIONS; i++)
            {
                explotions[i].Update(elapsedGameTime);
            }
        }

        //Anropar Draw-metoden för alla Smoke-objekt i arrayen
        internal void Draw(SpriteBatch spriteBatch, Camera camera, Texture2D texture)
        {
            for (int i = 0; i < MAX_EXPLOTIONS; i++)
            {
                explotions[i].Draw(spriteBatch, camera, texture);
            }
        }
    }
}
