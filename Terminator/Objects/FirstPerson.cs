using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terminator.Objects
{
    class FirstPerson
    {
        public Objects.Rectangle r;
        public double[] position = { 0, 0, 0 };
        public string textureName;
        public int textureId;

        public FirstPerson(string textureName, OpenTK.GLControl gl)
        {
            this.textureName = @"Img\Texture\" + textureName;
 
            draw(gl);

        }

        public FirstPerson(string textureName, OpenTK.GLControl gl, double positionX, double positionY, double positionZ)
        {
            this.textureName = @"Img\Texture\" + textureName;
            position[0] = positionX;
            position[1] = positionY;
            position[2] = positionZ;

            draw(gl);

        }

        private void draw(OpenTK.GLControl gl)
        {
            r = new Objects.Rectangle();

            r.points[0][0] = 0;
            r.points[0][1] = gl.Size.Height - 1;
            r.points[0][2] = 0;

            r.points[1][0] = gl.Size.Width - 1;
            r.points[1][1] = gl.Size.Height - 1;
            r.points[1][2] = 0;

            r.points[2][0] = gl.Size.Width - 1;
            r.points[2][1] = 0;
            r.points[2][2] = 0;

            r.points[3][0] = 0;
            r.points[3][1] = 0;
            r.points[3][2] = 0;
        }
    }
}
