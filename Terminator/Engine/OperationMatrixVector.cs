using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terminator.Engine
{
    class OperationMatrixVector
    {
        public static double[] distance(double[] v1, double[] v2)
        {
            double[] v = new double[4];

            v[0] = v1[0] - v2[0];
            v[1] = v1[1] - v2[1];
            v[2] = v1[2] - v2[2];
            v[3] = 1;

            return v;
        }

        public static double[] normalize(double[] v)
        {
            double modulo;
            double inverseModulo;

            modulo = Math.Sqrt(v[0] * v[0] + v[1] * v[1] + v[2] * v[2]);
            inverseModulo = 1.0 / modulo;
            v[0] *= inverseModulo;
            v[1] *= inverseModulo;
            v[2] *= inverseModulo;

            return v;
        }

        public static float[] normalize(float[] v)
        {
            float modulo;
            float inverseModulo;

            modulo = (float) Math.Sqrt(v[0] * v[0] + v[1] * v[1] + v[2] * v[2]);
            inverseModulo = 1.0f / modulo;
            v[0] *= inverseModulo;
            v[1] *= inverseModulo;
            v[2] *= inverseModulo;

            return v;
        }

        public static double[] xVxS(double[] v, double s)
        {
            for (int i = 0; i < 3; i++)
            {
                v[i] *= s;
            }

            return v;
        }

        public static double[] xVxM(double[,] m, double[] v)
        {
            int il = m.GetLength(0);
            int jl = m.GetLength(0);
            double[] vector = new double[4];

            for (int i = 0; i < il; i++)
            {
                vector[i] = 0;
                for (int j = 0; j < jl; j++)
                {
                    vector[i] += (m[i, j] * v[j]);
                }
            }

            return vector;
        }

        public static double[,] xMxM(double[,] m2, double[,] m1)
        {
            int il = m1.GetLength(0);
            int jl = m1.GetLength(0);
            int kl = m1.GetLength(0);
            double[,] matrix = new double[4, 4];

            for (int i = 0; i < il; i++)
            {
                for (int j = 0; j < jl; j++)
                {
                    for (int k = 0; k < kl; k++)
                    {
                        matrix[i, k] += (m2[i, j] * m1[j, k]);
                    }
                }
            }

            return matrix;
        }
    }
}
