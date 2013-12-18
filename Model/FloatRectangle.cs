using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Interaction.Model
{
    class FloatRectangle
    {
        Vector2 topLeft;
        Vector2 bottomRight;

        public FloatRectangle(Vector2 topLeft, Vector2 bottomRight)
        {
            this.topLeft = topLeft;
            this.bottomRight = bottomRight;
        }

        public static FloatRectangle createFromTopLeft(Vector2 theTopLeft, Vector2 boxSize)
        {
            Vector2 topLeft = theTopLeft;
            Vector2 bottomRight = theTopLeft + boxSize;

            return new FloatRectangle(topLeft, bottomRight);
        }

        public static FloatRectangle createFromCenter(Vector2 center, float size)
        {
            Vector2 topLeft = new Vector2(center.X - size / 2.0f, center.Y - size / 2.0f);
            Vector2 bottomRight = new Vector2(center.X + size / 2.0f, center.Y + size / 2.0f);

            return new FloatRectangle(topLeft, bottomRight);
        }

        public float Right
        {
            get { return bottomRight.X; }
        }

        public float Bottom
        {
            get { return bottomRight.Y; }
        }

        public float Left
        {
            get { return topLeft.X; }
        }

        public float Top
        {
            get { return topLeft.Y; }
        }

        internal bool isIntersecting(FloatRectangle other)
        {
            if (Right < other.Left)
                return false;
            if (Bottom < other.Top)
                return false;
            if (Left > other.Right)
                return false;
            if (Top > other.Bottom)
                return false;

            return true;
        }
    }
}
