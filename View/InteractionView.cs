using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Interaction.Model;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using Interaction.Controller;
using Microsoft.Xna.Framework.Audio;

namespace Interaction.View
{
    class InteractionView : Model.IModelListener
    {
        private InteractionModel m_model;
        private Camera camera;
        private GraphicsDevice graphDevice;
        private SpriteBatch spriteBatch;
        private ContentManager content;

        //Variabler för utritning av ramen
        private Texture2D borderBarierInner;
        private Texture2D borderBarier;
        private Texture2D textureExplotion;
        private Texture2D textureSplitter;
        private Rectangle borderLineInner;
        private Rectangle borderLine;
        private SoundEffect soundExplotion;

        //Variabler för den yttre panelern och den inre panelen som 
        //tillsammans blir ramen (En lite större svart ram och en lite mindre
        //vit inre ram). drawBorderWidth och drawBorderMargin extra bredd på 
        //de båda panelerna
        private int drawBorderWidth = 6;
        private int drawBorderMargin = 2;

        //Variabel för boll-texturen
        private Texture2D ballTexture;
        private Texture2D pointerTexture;

        private float aimDimention;

        private ArrayList explotions = new ArrayList();

        public InteractionView(GraphicsDevice graphDevice, InteractionModel model, Camera camera, SpriteBatch spriteBatch, ContentManager content)
        {
            this.graphDevice = graphDevice;
            this.m_model = model;
            this.camera = camera;
            this.spriteBatch = spriteBatch;
            this.aimDimention = XNAController.aimDimention;
            this.content = content;

            LoadContent();
        }

        internal void LoadContent()
        {
            ballTexture = content.Load<Texture2D>("ball3");
            pointerTexture = content.Load<Texture2D>("pointer");
            soundExplotion = content.Load<SoundEffect>("explosion_sound");
            textureExplotion = content.Load<Texture2D>("explotion3");
            textureSplitter = content.Load<Texture2D>("fireball");

            fillBorder();
        }

        //Skapar de båda panelerna som bildar ramen (Behövs bara göras en gång, men eftersom 
        //jag skapat möjlighet att ändra storlek på spelplanen med ctrl + f (för helskärm)
        //eller ctrl + c (för mindre spelplan) så anropas denna funktion från XNAController'n, 
        //vilket kanske borde göras via Camera-klassen istället
        private void fillBorder()
        {
            borderBarier = new Texture2D(graphDevice, 1, 1, false, SurfaceFormat.Color);
            borderBarier.SetData(new[] { Color.White });

            borderBarierInner = new Texture2D(graphDevice, 1, 1, false, SurfaceFormat.Color);
            borderBarierInner.SetData(new[] { Color.White });

            borderLine = camera.getBackgroundRectangle(drawBorderWidth);
            borderLineInner = camera.getBackgroundRectangle(drawBorderMargin);
        }

        public void shootFired(Vector2 possition)
        {
            explotions.Add(new ShootFired(possition, camera.GetScale(), soundExplotion.CreateInstance(), textureExplotion, textureSplitter));
        }

        internal bool playerWantsToQuit()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Escape);
        }

        internal bool playerIsShooting()
        {
            return Mouse.GetState().LeftButton == ButtonState.Pressed;
        }

        internal Vector2 getLogicalMousePossition()
        {
            return new Vector2(Mouse.GetState().X / camera.GetFloatScale(), Mouse.GetState().Y / camera.GetFloatScale());
        }

        internal void UpdateView(float elapsedGameTime, int width, int height)
        {
            foreach (ShootFired shot in explotions)
                shot.UpdateShoot(elapsedGameTime, width, height);
        }

        internal void Draw(float elapsedGameTime)
        {
            graphDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            spriteBatch.Draw(borderBarier, borderLine, Color.Black);
            spriteBatch.Draw(borderBarierInner, borderLineInner, Color.White);

            List<Ball> allBalls = m_model.getAllBalls();

            foreach(Ball ball in allBalls)
            {
                Vector2 ballViewCenter = camera.getVisualCoordinates(ball.ballPosition.X, ball.ballPosition.Y);

                Rectangle ballDestinationRectangle = new Rectangle((int)ballViewCenter.X,
                                                                   (int)ballViewCenter.Y,
                                                                   (int)(ball.BallDimention * camera.GetScale()),
                                                                   (int)(ball.BallDimention * camera.GetScale())
                                                                   );

                spriteBatch.Draw(ballTexture, ballDestinationRectangle, Color.White);
            }

            foreach (ShootFired shot in explotions)
                shot.DrawShoot(spriteBatch, camera);

            Vector2 pointerCenter = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Rectangle pointerDestinationRectangle = new Rectangle((int)(Mouse.GetState().X - ((aimDimention * camera.GetFloatScale()) / 4)),
                                                                  (int)(Mouse.GetState().Y - ((aimDimention * camera.GetFloatScale()) / 4)),
                                                                  (int)(aimDimention * camera.GetFloatScale()) / 2,
                                                                  (int)(aimDimention * camera.GetFloatScale()) / 2
                                                                 );

            spriteBatch.Draw(pointerTexture, pointerDestinationRectangle, Color.White);

            spriteBatch.End();
        }
    }
}
