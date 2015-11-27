using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terminator.Engine
{
    class Light
    {
        public float[] positionLigth = new float[4];
        public float[] directionLigth = new float[4];
        public bool turnOn;

        public Light()
        {
            positionLigth[3] = 1;
            turnOn = true;
        }
    }
}
