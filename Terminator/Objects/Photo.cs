using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terminator.Objects
{
    class Photo
    {
        public List<Objects.Rectangle> rectangles;
        public double[] position = { 0, 0, 0 };
        public string textureName;

        public Photo(double positionX, double positionY, double positionZ, string textureName, double height, double width, bool isX)
        {
            this.textureName = @"Img\Texture\Photo\" + textureName;
            position[0] = positionX;
            position[1] = positionY;
            position[2] = positionZ;

            rectangles = new List<Objects.Rectangle>();
            body(height, width, isX);
        }

        public void body(double height, double width, bool isX)
        {
            Objects.Rectangle r;

            r = new Objects.Rectangle();
            if (isX)
            {
                position[0] -= width;
                r.points[0][0] = width;
                r.points[0][1] = height;
                r.points[0][2] = -width;
                r.points[1][0] = width;
                r.points[1][1] = height;
                r.points[1][2] = width;
                r.points[2][0] = width;
                r.points[2][2] = width;
                r.points[3][0] = width;
                r.points[3][2] = -width;
                
            }
            else
            {
                position[2] -= width;
                r.points[0][0] = -width;
                r.points[0][1] = height;
                r.points[0][2] = width;
                r.points[1][0] = width;
                r.points[1][1] = height;
                r.points[1][2] = width;
                r.points[2][0] = width;
                r.points[2][2] = width;
                r.points[3][0] = -width;
                r.points[3][2] = width;
            }
            rectangles.Add(r); 
        }
    }
}
