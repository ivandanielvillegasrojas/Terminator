using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace Terminator.Engine
{
    class Effects
    {
        private static double[,] matrixEnemies = Engine.FactoryMatrix.getIdentity();
        private static double[,] matrixSalvation = Engine.FactoryMatrix.getIdentity();

        public static void enemies(List<Objects.Enemy> enemys)
        {
            matrixEnemies = Engine.OperationMatrixVector.xMxM(Engine.FactoryMatrix.getRotY(Math.PI / 50), matrixEnemies);

            foreach (Objects.Enemy enemy in enemys)
            {
                GL.NewList(enemy.indexList, ListMode.Compile);
                GL.LoadName(enemy.indexList);
                GL.BindTexture(TextureTarget.Texture2D, enemy.textureId);
                GL.Color3(Color.Transparent);
                GL.Translate(enemy.position[0], enemy.position[1], enemy.position[2]);
                foreach (Objects.Rectangle rectangle in enemy.rectangles)
                {
                    GL.Begin(BeginMode.Polygon);
                    for (int i = 0; i < rectangle.points.Count; i++)
                    {
                        if (i == 0)
                            GL.TexCoord2(0, 0);
                        if (i == 1)
                            GL.TexCoord2(1, 0);
                        if (i == 2)
                            GL.TexCoord2(1, 1);
                        if (i == 3)
                            GL.TexCoord2(0, 1);

                        double[] aux_dot = Engine.OperationMatrixVector.xVxM(matrixEnemies, rectangle.points[i]);
                        GL.Vertex3(aux_dot[0], aux_dot[1], aux_dot[2]);
                    }
                    GL.End();
                }
                GL.Translate(-enemy.position[0], -enemy.position[1], -enemy.position[2]);
                GL.EndList();

                if (enemy.isDead && enemy.position[1] != -10)
                    enemy.position[1]--;

                if (enemy.isDead && enemy.position[1] == -10)
                {
                    GL.DeleteLists(enemy.indexList, 1);
                }
            }
        }
    }
}
