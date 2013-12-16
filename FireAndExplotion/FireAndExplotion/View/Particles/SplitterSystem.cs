using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FireAndExplotion.View.Particles
{
    class SplitterSystem
    {
        //Skapar en array av Splitter
        private Splitter[] particles;
        //statisk variabler för antalet splitter
        private static int MAX_PARTICLES = 100;

        //Skapar en array av SplitterGrov
        private SplitterGrov[] particleGrov;
        //statisk variabler för antalet splitterGrov
        private static int MAX_GROV = 40;

        //Statiska variabler för starttid och sluttid
        //(Anger hur länge splittret + splitterGrov ska köras)
        private static float startRunTime = 0.0f;
        private static float endRunTime = 0.001f;

        //Konstruktor som initsierar arrayerna och particklarna i dessa
        internal SplitterSystem(Vector2 startPossition, int scale)
        {
            particles = new Splitter[MAX_PARTICLES];

            for (int i = 0; i < MAX_PARTICLES; i++)
            {
                particles[i] = new Splitter(i, startPossition, startRunTime, endRunTime);
            }

            particleGrov = new SplitterGrov[MAX_GROV];

            for (int i = 0; i < MAX_GROV; i++)
            {
                particleGrov[i] = new SplitterGrov(i, startPossition, scale, startRunTime, endRunTime);
            }
        }

        //Uppdaterar particklarna i arrayerna
        internal void Update(float elapsedGameTime)
        {
            for (int i = 0; i < MAX_PARTICLES; i++)
            {
                particles[i].Update(elapsedGameTime);
            }

            for (int i = 0; i < MAX_GROV; i++)
            {
                particleGrov[i].Update(elapsedGameTime);
            }
        }

        //Anropar Draw-funktionen på alla objekt i arrayerna
        internal void Draw(SpriteBatch spriteBatch, Camera camera, Texture2D texture)
        {
            for (int i = 0; i < MAX_PARTICLES; i++)
            {
                particles[i].Draw(spriteBatch, camera, texture);
            }

            for (int i = 0; i < MAX_GROV; i++)
            {
                particleGrov[i].Draw(spriteBatch, camera, texture);
            }
        }
    }
}
