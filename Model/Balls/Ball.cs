using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Interaction.Model
{
    class Ball
    {
        //Vector2 för bollens possition i X och Y-led (Logisk poss)
        internal Vector2 ballPosition;
        //Vector2 för bollens fart i X och Y-led (Logisk poss)
        internal Vector2 ballSpeed;

        //Prop för bollens dimention (Logisk dimention)
        internal float BallDimention { get; private set; }

        //Konstruktor som tar logisk fart i X och Y-led samt bollens logiska dimention
        internal Ball(Vector2 startPossition, float speedX, float speedY, float ballDimention)
        {
            this.ballPosition = startPossition;
            this.ballSpeed = new Vector2(speedX, speedY);
            this.BallDimention = ballDimention;
        }

        internal bool HasBeenShoot(FloatRectangle collidingRect)
        {
            FloatRectangle ballRect = new FloatRectangle(ballPosition, new Vector2(ballPosition.X + BallDimention, ballPosition.Y + BallDimention));

            if (ballRect.isIntersecting(collidingRect))
                return true;

            return false;
        }

        //Funktion för att öka bollens fart
        internal void speedUp()
        {
            if(ballSpeed.X > 0)
                ballSpeed.X += 0.01f;
            else
                ballSpeed.X -= 0.01f;
            if(ballSpeed.Y > 0)
                ballSpeed.Y += 0.01f;
            else
                ballSpeed.Y -= 0.01f;

            //Om X och Y-kordinaterna båda är samma så adderas 0.3 till Y så 
            //bollen inte åker i en rak linje
            if (ballSpeed.Y == ballSpeed.X)
                ballSpeed.Y = ballSpeed.Y + 0.3f;
        }

        //funktion som minskar bollens fart
        internal void speedDown()
        {
            if (ballSpeed.X > 0)
                ballSpeed.X -= 0.01f;
            else
                ballSpeed.X += 0.01f;
            if (ballSpeed.Y > 0)
                ballSpeed.Y -= 0.01f;
            else
                ballSpeed.Y += 0.01f;
        }
    }
}
