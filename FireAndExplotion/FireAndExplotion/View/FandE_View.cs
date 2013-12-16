using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FireAndExplotion.Model;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using FireAndExplotion.View.Particles;

namespace FireAndExplotion.View
{
    class FandE_View
    {
        internal FandE_Model m_fande_Model;
        internal Camera camera;
        internal GraphicsDevice graphDevice;
        internal SpriteBatch spriteBatch;

        //Variabel för texturer
        Texture2D textureExplotion;
        Texture2D textureSplitter;
        Texture2D textureSmoke;

        private SmokeSystem smokeSystem;
        private ExplotionSystem explotion;
        private SplitterSystem splitterSystem;
        private SmokeTrailSystem smokeTrailSystem;

        //Konstruktor som initsierar objektet
        internal FandE_View(GraphicsDevice graphDevice, FandE_Model fande_Model, Camera camera, SpriteBatch spriteBatch, ContentManager content)
        {
            this.graphDevice = graphDevice;
            this.m_fande_Model = fande_Model;
            this.camera = camera;
            this.spriteBatch = spriteBatch;

            this.splitterSystem = new SplitterSystem(fande_Model.Level.StartPossition, camera.GetScale());
            this.explotion = new ExplotionSystem(fande_Model.Level.StartPossition, camera.GetScale());
            this.smokeSystem = new SmokeSystem(fande_Model.Level.StartPossition, camera.GetScale());
            this.smokeTrailSystem = new SmokeTrailSystem(fande_Model.Level.StartPossition, camera.GetScale());

            LoadContent(content);
        }

        internal void LoadContent(ContentManager content)    
        {
            textureExplotion = content.Load<Texture2D>("explotion3");
            textureSplitter = content.Load<Texture2D>("fireball");
            textureSmoke = content.Load<Texture2D>("smoke");
        }

        //Funktion för att starta om rök-simuleringen
        internal void restart(float newX, float newY)
        {
            int scale = camera.GetScale();
            this.splitterSystem = new SplitterSystem(new Vector2(newX / scale, newY / scale), scale);
            this.explotion = new ExplotionSystem(new Vector2(newX / scale, newY / scale), scale);
            this.smokeSystem = new SmokeSystem(new Vector2(newX / scale, newY / scale), scale);
            this.smokeTrailSystem = new SmokeTrailSystem(new Vector2(newX / scale, newY / scale), scale);
        }

        //Uppdaterar explotionen med förfluten tid
        internal void UpdateView(float elapsedGameTime)
        {
            splitterSystem.Update(elapsedGameTime);
            explotion.Update(elapsedGameTime);
            smokeSystem.Update(elapsedGameTime);
            smokeTrailSystem.Update(elapsedGameTime);
        }

        //Anropar spritbatchen för utritning samt alla Draw-funktioner
        internal void Draw(float elapsedGameTime)
        {
            graphDevice.Clear(Color.White);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            splitterSystem.Draw(spriteBatch, camera, textureSplitter);
            explotion.Draw(spriteBatch, camera, textureExplotion);
            smokeTrailSystem.Draw(spriteBatch, camera, textureSplitter, textureSmoke);
            smokeSystem.Draw(spriteBatch, camera, textureSmoke);

            spriteBatch.End();
        }
    }
}
