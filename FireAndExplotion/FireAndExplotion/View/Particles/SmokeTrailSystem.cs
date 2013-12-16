using System;
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
        private SmokeTrailParticle[] smoketrailes;
        //Antal Smoke-objekt
        private const int MAX_SMOKETRAILS = 30;

        //Statiska variabler för starttid och sluttid
        //(Anger hur länge splittret + splitterGrov ska köras)
        private static float startRunTime = 0.0f;
        private static float endRunTime = 0.001f;

        //Initsierar arrayen med Smoke-objekt
        internal SmokeTrailSystem(Vector2 startPossition, int scale)
        {
            smoketrailes = new SmokeTrailParticle[MAX_SMOKETRAILS];

            for (int i = 0; i < MAX_SMOKETRAILS; i++)
            {
                smoketrailes[i] = new SmokeTrailParticle(i, startPossition, startRunTime, endRunTime);
            }
        }

        //Uppdaterar alla Smoke-objekt i arrayen
        internal void Update(float elapsedGameTime)
        {
            for (int i = 0; i < MAX_SMOKETRAILS; i++)
            {
                smoketrailes[i].Update(elapsedGameTime);
            }
        }

        //Anropar Draw-metoden för alla Smoke-objekt i arrayen
        internal void Draw(SpriteBatch spriteBatch, Camera camera, Texture2D texture, Texture2D textureSmoke)
        {
            for (int i = 0; i < MAX_SMOKETRAILS; i++)
            {
                smoketrailes[i].Draw(spriteBatch, camera, texture, textureSmoke);
            }
        }
    }
}
