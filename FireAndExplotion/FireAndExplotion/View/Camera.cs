using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FireAndExplotion.Controller;

namespace FireAndExplotion.View
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
        private float widthMargin = 0;
        private float heightMargin = 0;

        internal Camera(Viewport viewPort)
        {
            this.screenWidth = viewPort.Width;
            this.screenHeight = viewPort.Height;

            this.scaleX = (float)screenWidth / XNAController.boardLogicWidth;
            this.scaleY = (float)screenHeight / XNAController.boardLogicHeight;

            //Sätter höjd och bredd att vara densamma
            if (scaleY < scaleX)
            {
                widthMargin = (screenWidth - screenHeight) / 2;
                scaleX = scaleY;
            }
            else if (scaleY > scaleX)
            {
                heightMargin = (screenHeight - screenWidth) / 2;
                scaleY = scaleX;
            }
        }

        //Skapar en rektangel i Visuell storlek
        internal Rectangle getVisualCoordinates(float modelX, float modelY, float modelDimention)
        {
            return new Rectangle(
                                    (int)((modelX * scaleX) + (int)(widthMargin)) - (int)((modelDimention * scaleX) / 2),
                                    (int)((modelY * scaleY) + (int)(heightMargin)) - (int)((modelDimention * scaleX) / 2),
                                    (int)(modelDimention * scaleX),
                                    (int)(modelDimention * scaleY)
                                );
        }

        internal Rectangle getExplotionCoordinates(float modelX, float modelY, float modelDimention)
        {
            return new Rectangle(
                                    (int)((modelX * scaleX) + (int)(widthMargin)),
                                    (int)((modelY * scaleY) + (int)(heightMargin)),
                                    (int)(modelDimention * scaleX),
                                    (int)(modelDimention * scaleY)
                                );
        }

        //Returnerar skalan
        internal int GetScale()
        {
            return (int)scaleX;
        }

        //Returnerar skalan i float
        internal float GetDimention()
        {
            return scaleX;
        }
    }
}
