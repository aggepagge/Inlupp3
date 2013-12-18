using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Interaction.Controller;
using System.Collections;

namespace Interaction.Model
{
    class InteractionModel
    {
        //Hämtar värden för konstanterna för bredd, höjd och ramen (logiska)
        private float border = XNAController.boardLogicBorder;
        private float boardLogicWidth = XNAController.boardLogicWidth;
        private float boardLogicHeight = XNAController.boardLogicHeight;

        //Prop för bollen som modellen hanterar
        //internal Ball Ball { get; set; }
        private static int NUMBER_OF_BALLS = 10;
        private List<Ball> balls;
        private float aimDimention; 

        //Prop för banan
        internal Level Level { get; private set; }
         
        //Konstruktor som initsierar bollen med konstanter från XNAController
        internal InteractionModel()
        {
            createBalls(XNAController.ballLogicSpeedX, XNAController.ballLogicSpeedY, XNAController.ballLogicDimention);
            this.aimDimention = XNAController.aimDimention;
            Level = new Level();
        }

        private void createBalls(float ballSpeedX, float ballSpeedY, float ballDimention)
        {
            balls = new List<Ball>(NUMBER_OF_BALLS);

            for (int i = 0; i < NUMBER_OF_BALLS; i++)
            {
                Random rand = new Random(i);

                float randNummerX = (float)(0.2f + ((float)(rand.NextDouble()) * ((boardLogicWidth - 0.2f) - 0.2f)));
                float randNummerY = (float)(0.2f + ((float)(rand.NextDouble()) * ((boardLogicHeight - 0.2f) - 0.2f)));

                Vector2 startPossition = new Vector2(randNummerX, randNummerY);

                balls.Add(new Ball(startPossition, randNummerX - ballSpeedX, randNummerY - ballSpeedY, ballDimention));
            }
        }

        //Ändrar bollens riktning om den är vid eller passerat ramen
        internal void UpdateModel(float elapsedGameTime)
        {
            foreach (Ball ball in balls)
            {
                if (ball.ballPosition.X + ball.BallDimention >= boardLogicWidth - border)
                    ball.ballSpeed.X = -ball.ballSpeed.X;

                if (ball.ballPosition.Y + ball.BallDimention >= boardLogicHeight - border)
                    ball.ballSpeed.Y = -ball.ballSpeed.Y;

                if (ball.ballPosition.X <= border)
                    ball.ballSpeed.X = -ball.ballSpeed.X;

                if (ball.ballPosition.Y <= border)
                    ball.ballSpeed.Y = -ball.ballSpeed.Y;

                ball.ballPosition.X += elapsedGameTime * ball.ballSpeed.X;
                ball.ballPosition.Y += elapsedGameTime * ball.ballSpeed.Y;
            }
        }

        internal int getNumberOfBalls()
        {
            return balls.Count;
        }

        internal List<Ball> getAllBalls()
        {
            return balls;
        }

        internal void shootFired(Vector2 mousePossition, IModelListener listener)
        {
            FloatRectangle mousePointerRect = FloatRectangle.createFromCenter(mousePossition, aimDimention / 4);
            int removeBall = -1;

            for (int i = 0; i < balls.Count; i++)
                if (balls[i].HasBeenShoot(mousePointerRect))
                    removeBall = i;

            if (removeBall > -1)
            {
                listener.shootFired(balls[removeBall].ballPosition);
                balls.RemoveAt(removeBall);
            }
        }
    }
}
