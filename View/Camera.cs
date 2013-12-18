using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Interaction.Controller;
using Microsoft.Xna.Framework;

namespace Interaction.View
{
    class Camera
    {
        //Variabler för visuell bredd och höjd
        private int screenWidth;
        private int screenHeight;

        //Variabler för uträkning av skalan
        private float scaleX;
        private float scaleY;

        //Variabler för marginaler i höjd eller bredd 
        //för om förnstret har en ojämn form
        private int widthMargin = 0;
        private int heightMargin = 0;

        //Variabler för visuella kordinater för ram och boll
        private float visualBorderSizeX;
        private float visualBorderSizeY;

        internal Camera(Viewport viewPort)
        {
            this.screenWidth = viewPort.Width;
            this.screenHeight = viewPort.Height;

            this.scaleX = (float)screenWidth / XNAController.boardLogicWidth;
            this.scaleY = (float)screenHeight / XNAController.boardLogicHeight;

            //Sätter höjd och bredd att vara densamma
            if (scaleY < scaleX)
            {
                scaleX = scaleY;
            }
            else if (scaleY > scaleX)
            {
                scaleY = scaleX;
            }

            if (screenHeight < screenWidth)
            {
                widthMargin = (screenWidth - screenHeight) / 2;
            }
            else if (screenHeight > screenWidth)
            {
                heightMargin = (screenHeight - screenWidth) / 2;
            }

            this.visualBorderSizeX = (float)screenWidth / (XNAController.boardLogicWidth / XNAController.boardLogicBorder);
            this.visualBorderSizeY = (float)screenHeight / (XNAController.boardLogicHeight / XNAController.boardLogicBorder);
        }

        //Skapar en rektangel i Visuell storlek
        internal Rectangle getLogicalCoordinates(float visualX, float visualY, float visualDimention)
        {
            return new Rectangle(
                                    (int)(((visualX + widthMargin) / scaleX) - ((visualDimention / scaleX) / 2)),
                                    (int)(((visualY + heightMargin) / scaleY) - ((visualDimention / scaleX) / 2)),
                                    (int)(visualDimention / scaleX),
                                    (int)(visualDimention / scaleY)
                                );
        }

        internal Rectangle getExplotionRectangle(float modelX, float modelY, float modelDimention)
        {
            return new Rectangle(
                                    (int)((modelX * scaleX) + (int)(widthMargin)),
                                    (int)((modelY * scaleY) + (int)(heightMargin)),
                                    (int)(modelDimention * scaleX),
                                    (int)(modelDimention * scaleY)
                                );
        }

        //Skapar en rektangel i Visuell storlek
        internal Rectangle getVisualRectangle(float modelX, float modelY, float modelDimention)
        {
            return new Rectangle(
                                    (int)((modelX * scaleX) + (int)(widthMargin)) - (int)((modelDimention * scaleX) / 2),
                                    (int)((modelY * scaleY) + (int)(heightMargin)) - (int)((modelDimention * scaleX) / 2),
                                    (int)(modelDimention * scaleX),
                                    (int)(modelDimention * scaleY)
                                );
        }

        //Funktion som skapar en rektangel som är fönsterstorleken minus ramstorleken
        //och adderar drawBorderThiknes till bredd och höd
        internal Rectangle getBackgroundRectangle(int drawBorderThiknes)
        {
            return new Rectangle(
                                    (int)(widthMargin + (visualBorderSizeX - drawBorderThiknes)),
                                    (int)(heightMargin + (visualBorderSizeY - drawBorderThiknes)),
                                    (int)(((screenWidth - (visualBorderSizeX * 2)) - widthMargin) + drawBorderThiknes * 2),
                                    (int)(((screenHeight - (visualBorderSizeY * 2)) - heightMargin) + drawBorderThiknes * 2)
                                );
        }

        //Funktion som returnerar visuella kordinater för de logiska 
        //kordinater som tas som argument
        internal Vector2 getVisualCoordinates(float modelX, float modelY)
        {
            Vector2 view = new Vector2(widthMargin + (modelX * scaleX), heightMargin + (modelY * scaleY));

            return view;
        }

        //Returnerar skalan
        internal int GetScale()
        {
            return (int)scaleX;
        }

        //Returnerar skalan i float
        internal float GetFloatScale()
        {
            return scaleX;
        }
    }
}
