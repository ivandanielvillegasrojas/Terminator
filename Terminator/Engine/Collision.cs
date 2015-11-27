using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terminator.Engine
{
    class Collision
    {
        public int type;
        public double[] position = { 0, 0, 0, 0};

        /**
         * param name="type":
         *  = 1 -> equivale a pared en eje X
         *  = 2 -> equivale a pared en eje Z 
         *  = 3 -> equivale a un objeto que detiene el movimiento
         *  = 4 -> equivale al objeto que finaliza el nivel
        **/
        public Collision(double x1, double x2, double z1, double z2, int type)
        {
            position[0] = x1;
            position[1] = x2;
            position[2] = z1;
            position[3] = z2;
            this.type = type;
        }
    }
}
