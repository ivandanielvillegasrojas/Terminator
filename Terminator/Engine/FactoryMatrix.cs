using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terminator.Engine
{
    public static class FactoryMatrix
    {
        public static double[,] getIdentity()
        {
            double[,] matrix = new double[4, 4];

            matrix[0, 0] = 1;
            matrix[0, 1] = 0;
            matrix[0, 2] = 0;
            matrix[0, 3] = 0;
            matrix[1, 0] = 0;
            matrix[1, 1] = 1;
            matrix[1, 2] = 0;
            matrix[1, 3] = 0;
            matrix[2, 0] = 0;
            matrix[2, 1] = 0;
            matrix[2, 2] = 1;
            matrix[2, 3] = 0;
            matrix[3, 0] = 0;
            matrix[3, 1] = 0;
            matrix[3, 2] = 0;
            matrix[3, 3] = 1;

            return matrix;
        }

        public static double[,] getReflexion(bool x, bool y, bool z)
        {
            double[,] matrix = getIdentity();

            if (x)
                matrix[0, 0] = -1;
            if (y)
                matrix[1, 1] = -1;
            if (z)
                matrix[2, 2] = -1;

            return matrix;
        }

        public static double[,] getTrans(double tx, double ty, double tz)
        {
            double[,] matrix = getIdentity();

            matrix[0, 3] = tx;
            matrix[1, 3] = ty;
            matrix[2, 3] = tz;

            return matrix;
        }

        public static double[,] getScale(double sx, double sy, double sz)
        {
            double[,] matrix = getIdentity();

            matrix[0, 0] = sx;
            matrix[1, 1] = sy;
            matrix[2, 2] = sz;

            return matrix;
        }

        public static double[,] getRotX(double tita)
        {
            double[,] matrix = getIdentity();
            double cosx = Math.Cos(tita);
            double sinx = Math.Sin(tita);

            matrix[1, 1] = cosx;
            matrix[1, 2] = -sinx;
            matrix[2, 1] = sinx;
            matrix[2, 2] = cosx;

            return matrix;
        }

        public static double[,] getRotY(double tita)
        {
            double[,] matrix = getIdentity();
            double cosx = Math.Cos(tita);
            double sinx = Math.Sin(tita);

            matrix[0, 0] = cosx;
            matrix[0, 2] = sinx;
            matrix[2, 0] = -sinx;
            matrix[2, 2] = cosx;

            return matrix;
        }

        public static double[,] getRotZ(double tita)
        {
            double[,] matrix = getIdentity();
            double cosx = Math.Cos(tita);
            double sinx = Math.Sin(tita);

            matrix[0, 0] = cosx;
            matrix[0, 1] = -sinx;
            matrix[1, 0] = sinx;
            matrix[1, 1] = cosx;

            return matrix;
        }
    }
}