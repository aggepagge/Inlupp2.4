using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FireAndExplotion.View.Particles
{
    class SmokeTrailSystem
    {
        //Array för smoke-objekt
        private List<SmokeTrailParticle> smoketrailes;
        //Antal Smoke-objekt
        private const int MAX_SMOKETRAILS = 30;

        //Statiska variabler för starttid och sluttid
        //(Anger hur länge splittret + splitterGrov ska köras)
        private static float startRunTime = 0.0f;
        private static float endRunTime = 0.001f;

        //Initsierar arrayen med Smoke-objekt
        internal SmokeTrailSystem(Vector2 startPossition, int scale)
        {
            smoketrailes = new List<SmokeTrailParticle>(MAX_SMOKETRAILS);

            for (int i = 0; i < MAX_SMOKETRAILS; i++)
            {
                smoketrailes.Add(new SmokeTrailParticle(i, startPossition, startRunTime, endRunTime));
            }
        }

        //Uppdaterar alla Smoke-objekt i arrayen
        internal void Update(float elapsedGameTime)
        {
            foreach (SmokeTrailParticle smokeTrail in smoketrailes.ToList())
            {
                if (!smokeTrail.DeleateMe)
                    smokeTrail.Update(elapsedGameTime);
                else
                    smoketrailes.Remove(smokeTrail);
            }
        }

        //Anropar Draw-metoden för alla Smoke-objekt i arrayen
        internal void Draw(SpriteBatch spriteBatch, Camera camera, Texture2D texture, Texture2D textureSmoke)
        {
            foreach (SmokeTrailParticle smokeTrail in smoketrailes)
            {
                smokeTrail.Draw(spriteBatch, camera, texture, textureSmoke);
            }
        }
    }
}
