using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FireAndExplotion.View.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using FireAndExplotion.Model;

namespace FireAndExplotion.View
{
    class ParticleCollection
    {
        private SmokeSystem smokeSystem;
        private ExplotionSystem explotion;
        private SplitterSystem splitterSystem;
        private SmokeTrailSystem smokeTrailSystem;

        Texture2D textureExplotion;
        Texture2D textureSplitter;
        Texture2D textureSmoke;

        internal ParticleCollection(Vector2 startPossition, int scale)
        {
            this.splitterSystem = new SplitterSystem(startPossition, scale);
            this.explotion = new ExplotionSystem(startPossition, scale);
            this.smokeSystem = new SmokeSystem(startPossition, scale);
            this.smokeTrailSystem = new SmokeTrailSystem(startPossition, scale);
        }

        internal void LoadContent(ContentManager content)
        {
            textureExplotion = content.Load<Texture2D>("explotion3");
            textureSplitter = content.Load<Texture2D>("fireball");
            textureSmoke = content.Load<Texture2D>("smoke");
        }

        internal void UpdateParticleCollection(float elapsedGameTime, int width, int height)
        {
            splitterSystem.Update(elapsedGameTime, width, height);
            explotion.Update(elapsedGameTime);
            smokeSystem.Update(elapsedGameTime);
            smokeTrailSystem.Update(elapsedGameTime);
        }

        internal void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            splitterSystem.Draw(spriteBatch, camera, textureSplitter);
            explotion.Draw(spriteBatch, camera, textureExplotion);
            smokeTrailSystem.Draw(spriteBatch, camera, textureSplitter, textureSmoke);
            smokeSystem.Draw(spriteBatch, camera, textureSmoke);
        }
    }
}
