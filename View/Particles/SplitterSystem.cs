using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Interaction.Model;

namespace Interaction.View.Particles
{
    class SplitterSystem
    {
        //Skapar en array av Splitter
        private List<Splitter> particles;
        //statisk variabler för antalet splitter
        private static int MAX_PARTICLES = 100;

        //Skapar en array av SplitterGrov
        private List<SplitterGrov> particleGrov;
        //statisk variabler för antalet splitterGrov
        private static int MAX_GROV = 40;

        //Statiska variabler för starttid och sluttid
        //(Anger hur länge splittret + splitterGrov ska köras)
        private static float startRunTime = 0.0f;
        private static float endRunTime = 0.001f;

        //Konstruktor som initsierar arrayerna och particklarna i dessa
        internal SplitterSystem(Vector2 startPossition, float scale)
        {
            particles = new List<Splitter>(MAX_PARTICLES);

            for (int i = 0; i < MAX_PARTICLES; i++)
            {
                particles.Add(new Splitter(i, startPossition, startRunTime, endRunTime));
            }

            particleGrov = new List<SplitterGrov>(MAX_GROV);

            for (int i = 0; i < MAX_GROV; i++)
            {
                particleGrov.Add(new SplitterGrov(i, startPossition, scale, startRunTime, endRunTime));
            }
        }

        //Uppdaterar particklarna i arrayerna
        internal void Update(float elapsedGameTime, int width, int height)
        {
            foreach (Splitter splitter in particles.ToList())
            {
                if (!splitter.DeleateMe)
                    splitter.Update(elapsedGameTime);
                else
                    particles.Remove(splitter);
            }

            foreach (SplitterGrov splitterGrov in particleGrov.ToList())
            {
                if (!splitterGrov.DeleateMe)
                    splitterGrov.Update(elapsedGameTime, width, height);
                else
                    particleGrov.Remove(splitterGrov);
            }
        }

        //Anropar Draw-funktionen på alla objekt i arrayerna
        internal void Draw(SpriteBatch spriteBatch, Camera camera, Texture2D texture)
        {
            foreach (Splitter splitter in particles)
            {
                splitter.Draw(spriteBatch, camera, texture);
            }

            foreach (SplitterGrov splitterGrov in particleGrov)
            {
                splitterGrov.Draw(spriteBatch, camera, texture);
            }
        }
    }
}
