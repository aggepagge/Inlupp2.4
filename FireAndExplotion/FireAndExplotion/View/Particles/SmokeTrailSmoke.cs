using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FireAndExplotion.View.Particles
{
    class SmokeTrailSmoke
    {
        private Vector2 possition;

        private float lifetime = 0;
        private static float MAX_LIFETIME = 1.6f;
        private static float SIZE_INCREESE = 0.16f;
        private float sizeIncreese = 0;

        private static float minSize = 0.04f;
        private static float maxSize = 0.1f;
        private float size;

        internal SmokeTrailSmoke(Vector2 possition)
        {
            this.possition = possition;

            Random rand = new Random();

            //Random-initsiering av storlek mellan minsta och största storlek
            size = minSize + ((float)(rand.NextDouble()) * (minSize - maxSize));
        }

        internal void Update(float elapseTimeSeconds)
        {
            lifetime += elapseTimeSeconds;
            sizeIncreese += elapseTimeSeconds;

            if (sizeIncreese > SIZE_INCREESE)
            {
                size += elapseTimeSeconds;
                sizeIncreese = 0;
            }
        }

        internal void Draw(SpriteBatch spriteBatch, Camera camera, Texture2D texture, float startOpacity)
        {
            //Hämtar visuella kordinater från camera-klassen
            Rectangle smokeRect = camera.getVisualCoordinates(possition.X, possition.Y, size);

            //Variabler för uträkning av opacitet
            float t = lifetime / MAX_LIFETIME;
            float endValue = 0.0f;
            float startValue = startOpacity;

            if (t > 1.0f)
                t = 1.0f;

            //Opaciteten ökas med t
            float opacity = endValue * t + (1.0f - t) * startValue;
            Color myColor = new Color(opacity, opacity, opacity, opacity);

            spriteBatch.Draw(texture, smokeRect, myColor);
        }
    }
}
