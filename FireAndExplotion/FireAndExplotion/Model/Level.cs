using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FireAndExplotion.Controller;

namespace FireAndExplotion.Model
{
    class Level
    {
        //Prop för startpossition (För explotion)
        public Vector2 StartPossition { get; private set; }

        //Initsierar startpossitionerna
        internal Level()
        {
            StartPossition = new Vector2(XNAController.boardLogicWidth / 2, XNAController.boardLogicHeight / 2);
        }
    }
}
