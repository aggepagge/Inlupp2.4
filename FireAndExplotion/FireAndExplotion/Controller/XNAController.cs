using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FireAndExplotion.Model;
using FireAndExplotion.View;

namespace FireAndExplotion.Controller
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class XNAController : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Variabel f�r ExplotionNodel (Model)
        FandE_Model m_FandE_Model;
        //Variabel f�r ExplotionView (View)
        FandE_View v_FandE_View;
        //Variabel f�r Camera-objektet
        Camera camera;

        //Konstanter f�r logisk h�jd och bredd p� panelen
        public const float boardLogicWidth = 1.0f;
        public const float boardLogicHeight = 1.0f;

        //Konstanter f�r f�nster-bredd och h�jd
        public const int screenHeight = 800;
        public const int screenWidth = 800;

        public XNAController()
        {
            graphics = new GraphicsDeviceManager(this);
            //S�tter storlek p� f�nstret
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            m_FandE_Model = new FandE_Model();
            this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            camera = new Camera(graphics.GraphicsDevice.Viewport);

            v_FandE_View = new FandE_View(graphics.GraphicsDevice, m_FandE_Model, camera, spriteBatch, Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (v_FandE_View.playerWantsToQuit())
                this.Exit();

            if(v_FandE_View.doRestartExplotion())
                v_FandE_View.restartExplotion(Content);

            //Uppdaterar view
            v_FandE_View.UpdateView((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            //Anropar draw-funktionen f�r View
            v_FandE_View.Draw((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Draw(gameTime);
        }
    }
}
