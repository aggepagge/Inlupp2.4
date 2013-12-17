using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FireAndExplotion.View.Particles
{
    class SmokeSystem
    {
        //Array för smoke-objekt
        private List<Smoke> particles;
        //Antal Smoke-objekt
        private const int MAX_PARTICLES = 1000;

        //Initsierar arrayen med Smoke-objekt
        internal SmokeSystem(Vector2 startPossition, int scale)
        {
            particles = new List<Smoke>(MAX_PARTICLES);

            for (int i = 0; i < MAX_PARTICLES; i++)
            {
                particles.Add(new Smoke(i, startPossition, scale));
            }
        }

        //Uppdaterar alla Smoke-objekt i arrayen
        internal void Update(float elapsedGameTime)
        {
            foreach (Smoke smoke in particles.ToList())
            {
                if (!smoke.DeleateMe)
                    smoke.Update(elapsedGameTime);
                else
                    particles.Remove(smoke);
            }
        }

        //Anropar Draw-metoden för alla Smoke-objekt i arrayen
        internal void Draw(SpriteBatch spriteBatch, Camera camera, Texture2D texture)
        {
            foreach (Smoke smoke in particles)
            {
                smoke.Draw(spriteBatch, camera, texture);
            }
        }
    }
}
