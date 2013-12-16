using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FireAndExplotion.View.Particles
{
    class Smoke
    {
        private Vector2 possition;
        private Vector2 speed;
        private Vector2 gravity;

        private float lifetime = 0;
        private static float MAX_LIFETIME = 1.6f;
        private static float TOTAL_START_TIME = 0.2f;
        private static float TOTAL_END_TIME = 4.0f;

        private float sizeIncrease;
        private static float minSpeed = 0.001f;
        private static float maxSpeed = 0.2f;
        private static float minSize = 0.01f;
        private static float maxSize = 0.10f;
        private float size;
        private float delayTimeSeconds;

        private float rotation = 0;
        private static float WAIT_TIME = 0.03f;
        private static float SPEED_DECREASE = 0.0001f;

        private float startOpacity;

        //Konstruktor som initsierar alla variabler
        public Smoke(int seed, Vector2 systemStartPossition, float scale)
        {
            //Nytt random-objekt
            Random rand = new Random(seed);
            //Farten sätts med random för extra variation
            speed = new Vector2((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1));
            speed.Normalize();

            //Initsiering av startpossition (Med Random för lite extra variation)
            possition = new Vector2(systemStartPossition.X + ((float)((rand.NextDouble() * 2 - 1) * (scale * 0.0001))), systemStartPossition.Y);

            //Farten sätts mellan minsta hastighet och största hastighet med random
            speed *= minSpeed + ((float)(rand.NextDouble()) * (minSpeed - maxSpeed));

            //Storleken sätts mellan minsta storlek och största storlek med random
            size = minSize + ((float)(rand.NextDouble()) * (minSize - maxSize));

            //Initsiering av väntetid innan animationen ska starta
            delayTimeSeconds = TOTAL_START_TIME + (float)(rand.NextDouble()) * TOTAL_END_TIME;

            //Räknar ut startopaciteten och sätter den mellan 0 och 1. Basserad på hur
            //sent i rökutvecklingen denna partickel har skapats
            startOpacity = delayTimeSeconds * 2 - 1;

            //Initsiering med hjälp av uträkning som ökar storleken på rök-partickeln
            sizeIncrease = (size * 100 * ((float)(rand.NextDouble()) * (float)(scale * 0.002))) / ((float)scale);

            //Vektor för gravitation 
            gravity = new Vector2(-0.8f, -1.0f);
        }

        //Uppdaterar rök-partickeln
        internal void Update(float elapseTimeSeconds)
        {
            delayTimeSeconds -= elapseTimeSeconds;

            //Kollar om animeringen ska uppdateras
            if (delayTimeSeconds <= 0.0f)
            {
                possition.X += speed.X * elapseTimeSeconds;
                possition.Y += speed.Y * elapseTimeSeconds;

                speed.X += gravity.X * elapseTimeSeconds;
                speed.Y += (gravity.Y * elapseTimeSeconds) + SPEED_DECREASE;

                lifetime += elapseTimeSeconds;

                //Ökar storleken
                size += sizeIncrease;

                //Uträkning av rotation av texturen
                rotation += WAIT_TIME;
                float circle = MathHelper.Pi * 2;
                rotation = rotation % circle;
            }
        }

        internal void Draw(SpriteBatch spriteBatch, Camera camera, Texture2D texture)
        {
            //Kollar om animeringen ska starta
            if (delayTimeSeconds <= 0.0f)
            {
                //Hämtar rektangel för visuella kordinater
                Rectangle splitterRect = camera.getVisualCoordinates(possition.X, possition.Y, size);

                //Variabler för uträkning av opacitet
                float t = lifetime / MAX_LIFETIME;
                float endValue = 0.0f;
                //Räknar bort opacitet beroende på hur länge sen röken startade
                float startValue = 1.0f - (startOpacity / TOTAL_END_TIME);

                if (t > 1.0f)
                    t = 1.0f;

                //Opaciteten ökas med t
                float opacity = endValue * t + (1.0f - t) * startValue;
                Color myColor = new Color(opacity, opacity, opacity, opacity);

                //Ritar ut texturen med rotation
                spriteBatch.Draw(texture, splitterRect, null, myColor, rotation, new Vector2(0, 0), SpriteEffects.None, 0);
            }
        }
    }
}
