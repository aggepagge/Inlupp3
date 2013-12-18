using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Interaction.Model;

namespace Interaction.View.Particles
{
    class SplitterGrov
    {
        private Vector2 systemStartPossition;
        private Vector2 possition;
        private Vector2 speed;
        private Vector2 gravity;
        private static float minSpeed = 1.6f;
        private static float maxSpeed = 9.0f;
        private static float minSize = 0.01f;
        private static float maxSize = 0.02f;

        private float size;
        private int visualSize;
        private int visualPossitionX;
        private int visualPossitionY;
        private float sizeIncrease;
        private float speedIncreaseX;
        private float speedIncreaseY;
        private float delayTimeSeconds;

        internal bool DeleateMe { get; private set; }

        //SplitterGrov åker i en rak bana (som dock påverkas av gravitation med gravity-variabeln)
        //Farten och storleken (X och Y-led) ökas med tiden vilket gör att SplitterGrov-objektet ser
        //ut att komma mot betraktaren.
        public SplitterGrov(int seed, Vector2 systemStartPossition, float scale, float startRunTime, float endRunTime)
        {
            this.systemStartPossition = systemStartPossition;
            possition = new Vector2(systemStartPossition.X, systemStartPossition.Y);

            Random rand = new Random(seed);
            //Initsiering av fart-vektorn med random-fart. Detta för att ge en mer ojämn fördelning
            speed = new Vector2((float)(rand.NextDouble() * 2 - 1), (float)(rand.NextDouble() * 2 - 1));
            speed.Normalize();

            //farten multipliceras med Random-farten mellan start och slut-fart. 
            speed *= minSpeed + ((float)(rand.NextDouble()) * (minSpeed - maxSpeed));

            //Random-initsiering av storlek mellan minsta och största storlek
            size = minSize + ((float)(rand.NextDouble()) * (minSize - maxSize));

            //För hur länge animeringen ska vänta innan den startar
            delayTimeSeconds = startRunTime + (float)(rand.NextDouble()) * endRunTime;

            //Uträkning för storleksökning och fartökning
            sizeIncrease = size * 80 / scale;
            speedIncreaseX = speed.X * 80 / scale;
            speedIncreaseY = speed.Y * 80 / scale;

            //gravitationen i X och Y-led
            gravity = new Vector2(0.0f, 20.4f);

            DeleateMe = false;
            visualSize = (int)(scale * size);
            visualPossitionX = (int)(scale * possition.X);
            visualPossitionY = (int)(scale * possition.Y); 
        }

        internal void Update(float elapseTimeSeconds, int width, int height)
        {
            delayTimeSeconds -= elapseTimeSeconds;

            //Kollar om uppdateringen ska starta
            if (delayTimeSeconds <= 0.0f)
            {
                possition.X += speed.X * elapseTimeSeconds;
                possition.Y += speed.Y * elapseTimeSeconds;

                speed.X += gravity.X * elapseTimeSeconds;
                speed.Y += gravity.Y * elapseTimeSeconds;

                //Addering av fartökning och storleksökning
                size += sizeIncrease;
                speed.X += speedIncreaseX;
                speed.Y += speedIncreaseY;

                toRemove(width, height);
            }
        }

        private void toRemove(int width, int height)
        {
            if (   visualPossitionX + visualSize < 0
                || visualPossitionY + visualSize > height
                || visualPossitionX > width
               ) 
                DeleateMe = true;
        }

        internal void Draw(SpriteBatch spriteBatch, Camera camera, Texture2D texture)
        {
            //Kollar om animeringen ska starta
            if (delayTimeSeconds <= 0.0f)
            {
                //Hämtar visuella kordinater från camera-klassen
                Rectangle splitterRect = camera.getVisualRectangle(possition.X, possition.Y, size);

                spriteBatch.Draw(texture, splitterRect, Color.White);
            }
        }
    }
}
