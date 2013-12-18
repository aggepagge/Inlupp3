using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Interaction.Controller;

namespace Interaction.Model
{
    class Level
    {
        //Prop för startpossition (För explotion)
        public Vector2 StartPossition { get; private set; }
        public float BoardWidth { get; private set; }
        public float BoardHeight { get; private set; }

        //Initsierar startpossitionerna
        internal Level()
        {
            BoardWidth = XNAController.boardLogicWidth;
            BoardHeight = XNAController.boardLogicHeight;
            StartPossition = new Vector2(BoardWidth / 2, BoardHeight * 0.9f);
        }
    }
}
