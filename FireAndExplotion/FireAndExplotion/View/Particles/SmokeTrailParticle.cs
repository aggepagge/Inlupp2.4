using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FireAndExplotion.View.Particles
{
    class SmokeTrailParticle
    {
        private Vector2 systemStartPossition;
        private Vector2 possition;
        private Vector2 speed;
        private Vector2 gravity;
        private float lifetime = 0;
        private static float NEW_SMOKE_TIME = 0.001f;
        private static float minSpeed = 1.0f;
        private static float maxSpeed = 1.6f;
        private static float minSize = 0.02f;
        private static float maxSize = 0.06f;

        private float size;
        private float delayTimeSeconds;
        private float createSmoke = 0;

        private List<SmokeTrailSmoke> smoke = new List<SmokeTrailSmoke>();

        //Konstruktor som tar seed, startpossition och start samt 
        //slut-tid för hur länge annimeringen ska köras
        public SmokeTrailParticle(int seed, Vector2 systemStartPossition, float startRunTime, float endRunTime)
        {
            this.systemStartPossition = systemStartPossition;
            possition = new Vector2(systemStartPossition.X, systemStartPossition.Y);

            Random rand = new Random(seed);
            //Initsiering av fart-vektorn med random-fart. Detta för att ge en mer ojämn fördelning
            speed = new Vector2((float)(rand.NextDouble() - 0.50), (float)(rand.NextDouble() * - 1));
            speed.Normalize();

            //Farten sätts till en random-fart mellan lägsta och högsta farten
            speed *= minSpeed + ((float)(rand.NextDouble()) * (minSpeed - maxSpeed));

            //Random-initsiering av storlek mellan minsta och största storlek
            size = minSize + ((float)(rand.NextDouble()) * (minSize - maxSize));

            //För hur länge animeringen ska vänta innan den startar
            delayTimeSeconds = startRunTime + (float)(rand.NextDouble()) * endRunTime;

            //gravitationen i X och Y-led
            gravity = new Vector2(0.0f, 0.3f);
        }

        internal void Update(float elapseTimeSeconds)
        {
            delayTimeSeconds -= elapseTimeSeconds;
            createSmoke += elapseTimeSeconds;

            //Kollar om uppdateringen ska starta
            if (delayTimeSeconds <= 0.0f)
            {
                possition.X += speed.X * elapseTimeSeconds;
                possition.Y += speed.Y * elapseTimeSeconds;

                speed.X += gravity.X * elapseTimeSeconds;
                speed.Y += gravity.Y * elapseTimeSeconds;

                lifetime += elapseTimeSeconds;

                if (createSmoke > NEW_SMOKE_TIME)
                {
                    smoke.Add(new SmokeTrailSmoke(possition));
                    createSmoke = 0.0f;
                }

                foreach(SmokeTrailSmoke smokeTrail in smoke)
                {
                    smokeTrail.Update(elapseTimeSeconds);
                }
            }
        }

        internal void Draw(SpriteBatch spriteBatch, Camera camera, Texture2D texture, Texture2D smokeTexture)
        {
            //Kollar om animeringen ska starta
            if (delayTimeSeconds <= 0.0f)
            {
                //Hämtar visuella kordinater från camera-klassen
                Rectangle splitterRect = camera.getVisualCoordinates(possition.X, possition.Y, size);

                spriteBatch.Draw(texture, splitterRect, Color.White);

                foreach (SmokeTrailSmoke smokeTrail in smoke)
                {
                    smokeTrail.Draw(spriteBatch, camera, smokeTexture);
                }
            }
        }
    }
}
