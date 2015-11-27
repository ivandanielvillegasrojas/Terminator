using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terminator.Objects
{
    class Template
    {
        public static Objects.Plane levelCompleted(string textureName, double width, double height, double positionX, double positionY, double positionZ)
        {
            Objects.Plane p = new Objects.Plane();
            Objects.Rectangle r = new Objects.Rectangle();

            p.textureName = @"Img\Texture\UserInterface\" + textureName;
            p.repeat = false;

           
            r.points[0][0] = 10 - positionX;
            r.points[0][1] = 10 - positionY;
            r.points[0][2] = -positionZ;

            r.points[1][0] = 10 - width - positionX;
            r.points[1][1] = 10 - positionY;
            r.points[1][2] = -positionZ;

            r.points[2][0] = 10 - width - positionX;
            r.points[2][1] = 10 - height - positionY;
            r.points[2][2] = -positionZ;

            r.points[3][0] = 10 - positionX;
            r.points[3][1] = 10 - height - positionY;
            r.points[3][2] = -positionZ;
            p.rectangles.Add(r);

            return p;
        }

        public static Objects.Plane levelCompleted(string textureName)
        {
            return levelCompleted(textureName, false, 0, 0);
        }

        public static Objects.Plane levelCompleted(string textureName, bool isButton, double positionX, double positionY)
        {
            Objects.Plane p = new Objects.Plane();
            Objects.Rectangle r = new Objects.Rectangle();

            p.textureName = @"Img\Texture\UserInterface\" + textureName;
            p.repeat = false;

            if (!isButton)
            {
                r.points[0][0] = 10;
                r.points[0][1] = 10;
                r.points[0][2] = 0;

                r.points[1][0] = -10;
                r.points[1][1] = 10;
                r.points[1][2] = 0;

                r.points[2][0] = -10;
                r.points[2][1] = -10;
                r.points[2][2] = 0;

                r.points[3][0] = 10;
                r.points[3][1] = -10;
                r.points[3][2] = 0;
                p.rectangles.Add(r);
            }
            else
            {
                r.points[0][0] = 10 - positionX;
                r.points[0][1] = 10 - positionY;
                r.points[0][2] = -0.1;

                r.points[1][0] = 7.5 - positionX;
                r.points[1][1] = 10 - positionY;
                r.points[1][2] = -0.1;

                r.points[2][0] = 7.5 - positionX;
                r.points[2][1] = 9 - positionY;
                r.points[2][2] = -0.1;

                r.points[3][0] = 10 - positionX;
                r.points[3][1] = 9 - positionY;
                r.points[3][2] = -0.1;
                p.rectangles.Add(r);
            }
            return p;
        }
    }
}

