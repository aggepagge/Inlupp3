using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Interaction.View.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Interaction.View
{
    class ShootFired
    {
        private ExplotionSystem explotion;
        private SplitterSystem splitter;

        private Texture2D textureExplotion;
        private Texture2D textureSplitter;
        private SoundEffectInstance explotionSoundInstance;

        internal ShootFired(Vector2 startPossition, float scale, SoundEffectInstance soundInstance, Texture2D explotion, Texture2D splitter)
        {
            this.splitter = new SplitterSystem(startPossition, scale);
            this.explotion = new ExplotionSystem(startPossition, scale);
            this.explotionSoundInstance = soundInstance;
            this.textureExplotion = explotion;
            this.textureSplitter = splitter;

            setSound();
        }

        private void setSound()
        {
            explotionSoundInstance.Volume = 0.5f;
            explotionSoundInstance.Play();
        }

        internal void UpdateShoot(float elapsedGameTime, int width, int height)
        {
            splitter.Update(elapsedGameTime, width, height);
            explotion.Update(elapsedGameTime);
        }

        internal void DrawShoot(SpriteBatch spriteBatch, Camera camera)
        {
            splitter.Draw(spriteBatch, camera, textureSplitter);
            explotion.Draw(spriteBatch, camera, textureExplotion);
        }
    }
}
