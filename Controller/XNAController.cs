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
using Interaction.Model;
using Interaction.View;

namespace Interaction.Controller
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class XNAController : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private InteractionModel m_model;
        private InteractionView v_view;
        private Camera camera;

        //Konstanter för logisk höjd och bredd på panelen
        public const float boardLogicWidth = 1.0f;
        public const float boardLogicHeight = 1.0f;

        //Konstanter för fönster-bredd och höjd
        public const int screenHeight = 800;
        public const int screenWidth = 800;
        public const float boardLogicBorder = 0.1f;
        public const float ballLogicDimention = 0.05f;
        public const float ballLogicSpeedX = 0.2f;
        public const float ballLogicSpeedY = 0.4f;
        public const float aimDimention = 0.2f;

        public XNAController()
        {
            graphics = new GraphicsDeviceManager(this);
            //Sätter storlek på fönstret
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
            // TODO: Add your initialization logic here
            m_model = new InteractionModel();

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
            v_view = new InteractionView(graphics.GraphicsDevice, m_model, camera, spriteBatch, Content);
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
            if (v_view.playerWantsToQuit())
                this.Exit();

            if (v_view.playerIsShooting())
                m_model.shootFired(v_view.getLogicalMousePossition(), v_view);

            //Uppdaterar view
            m_model.UpdateModel((float)gameTime.ElapsedGameTime.TotalSeconds);

            int width = (int)(m_model.Level.BoardWidth * camera.GetScale());
            int height = (int)(m_model.Level.BoardHeight * camera.GetScale());
            
            v_view.UpdateView((float)gameTime.ElapsedGameTime.TotalSeconds, 
                (int)(m_model.Level.BoardWidth * camera.GetScale()), (int)(m_model.Level.BoardHeight * camera.GetScale()));

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            v_view.Draw((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Draw(gameTime);
        }
    }
}
