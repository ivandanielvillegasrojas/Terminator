using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Terminator.Objects
{
    class Plane
    {
        public Color color;
        public List<Objects.Rectangle> rectangles;
        public string textureName;
        public bool repeat;
        public bool rotate;

        public Plane()
        {
            rectangles = new List<Objects.Rectangle>();
            color = Color.Transparent;
            textureName = null;
            repeat = true;
            rotate = false;
        }
    }
}
