using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireAndExplotion.Model
{
    class FandE_Model
    {
        public Level Level { get; private set; }

        //Initsierar Level-objektet
        internal FandE_Model()
        {
            Level = new Level();
        }
    }
}
