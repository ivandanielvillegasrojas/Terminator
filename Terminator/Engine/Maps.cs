using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Terminator.Engine
{
    class Maps
    {
        public double[] position = new double[4];
        public double[] view = new double[4];
        public string path;
        public List<Objects.Box> boxs = new List<Objects.Box>();
        public List<Engine.Collision> collisions = new List<Engine.Collision>();
        public List<Objects.Enemy> enemys = new List<Objects.Enemy>();
        public List<Objects.Plane> planes = new List<Objects.Plane>();
        public List<Objects.Photo> photos = new List<Objects.Photo>();
        public float[] fogColor;
        public float fogDensity;
        public double quality;
        Random random;

        public Maps(int level, double quality)
        {
            this.quality = quality;
            random = new Random(DateTime.Now.Millisecond);
            switch (level)
            {
                case 1:
                    fogColor = new[] { 0.01f, 0.01f, 0.01f, 1.0f };
                    fogDensity = 0.04f;
                    path = @"Img\Texture\Level1\";
                    environment();
                    room(false);
                    level1();
                    break;
                case 2:
                    fogColor = new[] { 0.2f, 0.2f, 0.2f, 1.0f };
                    fogDensity = 0.04f;
                    path = @"Img\Texture\Level2\";
                    environment();
                    room(false);
                    level2();
                    break;
                case 3:
                    fogColor = new[] { 0.01f, 0.01f, 0.01f, 1.0f };
                    fogDensity = 0.05f;
                    path = @"Img\Texture\Level3\";
                    environment();
                    room(false);
                    level3();
                    break;
                case 4:
                    fogColor = new[] { 0.2f, 0.2f, 0.2f, 1.0f };
                    fogDensity = 0.04f;
                    path = @"Img\Texture\Level4\";
                    environment();
                    room(false);
                    level4();
                    break;
            }
        }

        public void environment()
        {
            double height = 1000;
            double width = 1000;
            Objects.Plane p;
            Objects.Rectangle r;

            p = new Objects.Plane();
            p.textureName = path + "panoramic1.jpg";
            r = new Objects.Rectangle();
            r.points[0][0] = width;
            r.points[0][1] = height;
            r.points[0][2] = -width;
            r.points[1][0] = -width;
            r.points[1][1] = height;
            r.points[1][2] = -width;
            r.points[2][0] = -width;
            r.points[2][1] = -0.2;
            r.points[2][2] = -width;
            r.points[3][0] = width;
            r.points[3][1] = -0.2;
            r.points[3][2] = -width;
            p.rectangles.Add(r);
            p.rotate = true;
            planes.Add(p);

            p = new Objects.Plane();
            p.textureName = path + "panoramic2.jpg";
            r = new Objects.Rectangle();
            r.points[0][0] = width;
            r.points[0][1] = height;
            r.points[0][2] = -width;
            r.points[1][0] = width;
            r.points[1][1] = height;
            r.points[1][2] = width;
            r.points[2][0] = width;
            r.points[2][1] = -0.2;
            r.points[2][2] = width;
            r.points[3][0] = width;
            r.points[3][1] = -0.2;
            r.points[3][2] = -width;
            p.rectangles.Add(r);
            planes.Add(p);

            p = new Objects.Plane();
            p.textureName = path + "panoramic3.jpg";
            r = new Objects.Rectangle();
            r.points[0][0] = -width;
            r.points[0][1] = height;
            r.points[0][2] = width;
            r.points[1][0] = width;
            r.points[1][1] = height;
            r.points[1][2] = width;
            r.points[2][0] = width;
            r.points[2][1] = -0.2;
            r.points[2][2] = width;
            r.points[3][0] = -width;
            r.points[3][1] = -0.2;
            r.points[3][2] = width;
            p.rectangles.Add(r);
            p.rotate = true;
            planes.Add(p);

            p = new Objects.Plane();
            p.textureName = path + "panoramic4.jpg";
            r = new Objects.Rectangle();
            r.points[0][0] = -width;
            r.points[0][1] = height;
            r.points[0][2] = width;
            r.points[1][0] = -width;
            r.points[1][1] = height;
            r.points[1][2] = -width;
            r.points[2][0] = -width;
            r.points[2][1] = -0.2;
            r.points[2][2] = -width;
            r.points[3][0] = -width;
            r.points[3][1] = -0.2;
            r.points[3][2] = width;
            p.rectangles.Add(r);
            planes.Add(p);

            p = new Objects.Plane();
            p.textureName = path + "panoramic5.jpg";
            r = new Objects.Rectangle();
            r.points[0][0] = -width;
            r.points[0][1] = height;
            r.points[0][2] = width;
            r.points[1][0] = width;
            r.points[1][1] = height;
            r.points[1][2] = width;
            r.points[2][0] = width;
            r.points[2][1] = height;
            r.points[2][2] = -width;
            r.points[3][0] = -width;
            r.points[3][1] = height;
            r.points[3][2] = -width;
            p.rectangles.Add(r);
            p.rotate = true;
            planes.Add(p);

            p = new Objects.Plane();
            p.textureName = path + "panoramic6.jpg";
            r = new Objects.Rectangle();
            r.points[0][0] = width;
            r.points[0][1] = -0.2;
            r.points[0][2] = width;
            r.points[1][0] = width;
            r.points[1][1] = -0.2;
            r.points[1][2] = -width;
            r.points[2][0] = -width;
            r.points[2][1] = -0.2;
            r.points[2][2] = -width;
            r.points[3][0] = -width;
            r.points[3][1] = -0.2;
            r.points[3][2] = width;
            p.rectangles.Add(r);
            planes.Add(p);
        }

        public void room(bool isRoof)
        {
            double width = 100;
            double height = 12;
            Objects.Plane p;
            Objects.Rectangle r;

            p = new Objects.Plane();
            p.textureName = path + "soil.jpg";
            for (double i = 0; i < width; i += quality)
            {
                for (double j = 0; j < width; j += quality)
                {
                    r = new Objects.Rectangle();

                    r.points[0][0] = j;
                    r.points[0][2] = i;
                    r.points[1][0] = j;
                    r.points[1][2] = i + quality;
                    r.points[2][0] = j + quality;
                    r.points[2][2] = i + quality;
                    r.points[3][0] = j + quality;
                    r.points[3][2] = i;
                    p.rectangles.Add(r);
                }
            }
            planes.Add(p);
 
            if (isRoof)
            {
                p = new Objects.Plane();
                p.textureName = path + "roof.jpg";
                for (double i = 0; i < width; i += quality)
                {
                    for (double j = 0; j < width; j += quality)
                    {
                        r = new Objects.Rectangle();

                        r.points[0][0] = j;
                        r.points[0][1] = height;
                        r.points[0][2] = i;
                        r.points[1][0] = j;
                        r.points[1][1] = height;
                        r.points[1][2] = i + quality;
                        r.points[2][0] = j + quality;
                        r.points[2][1] = height;
                        r.points[2][2] = i + quality;
                        r.points[3][0] = j + quality;
                        r.points[3][1] = height;
                        r.points[3][2] = i;
                        p.rectangles.Add(r);
                    }
                }
                planes.Add(p);
            }

            wallX(0, width, 0, height, path + "wall.jpg", false, false);
            wallX(0, width, width, height, path + "wall.jpg", false, false);
            wallZ(0, width, 0, height, path + "wall.jpg", false, false);
            wallZ(0, width, width, height, path + "wall.jpg", false, false);
        }

        public void level1()
        {
            double height = 12;
            string textureName = path + "wall.jpg";

            // Posicion inicial del personaje
            position[0] = 1;
            position[1] = 4;
            position[2] = 1;
            view[0] = 100;
            view[1] = 4;
            view[2] = 100;
            //--


            // Construccion de puertas
            Objects.Photo p;

            p = new Objects.Photo(0.1, 0, 5, "door1.png", 8, 1.5, true);
            photos.Add(p);
            p = new Objects.Photo(99.9, 0, 95, "door2.png", 8, 1.5, true);
            photos.Add(p);
            //--


            // Construccion de paredes X
            wallX(10, 90, 10, height, textureName, false, false);
            wallX(20, 80, 20, height, textureName, false, false);
            wallX(30, 70, 30, height, textureName, false, true);
            wallX(30, 80, 40, height, textureName, false, false);
            wallX(20, 40, 50, height, textureName, false, false);
            wallX(60, 80, 50, height, textureName, true, false);
            wallX(30, 40, 70, height, textureName, false, false);
            wallX(50, 80, 70, height, textureName, false, false);
            wallX(30, 90, 80, height, textureName, true, false);
            wallX(20, 80, 90, height, textureName, false, false);
            wallX(90, 100, 90, height, textureName, false, false);
            //--


            // Construccion de paredes Z
            wallZ(10, 90, 10, height, textureName, false, true);
            wallZ(20, 90, 20, height, textureName, false, false);
            wallZ(30, 40, 30, height, textureName, false, false);
            wallZ(60, 70, 30, height, textureName, true, false);
            wallZ(50, 70, 40, height, textureName, false, false);
            wallZ(50, 70, 50, height, textureName, true, false);
            wallZ(20, 40, 80, height, textureName, false, false);
            wallZ(50, 80, 80, height, textureName, false, false);
            wallZ(90, 100, 80, height, textureName, false, false);
            wallZ(10, 90, 90, height, textureName, false, false);
            //--


            // Construccion de enemigos
            generateEnemies(20);
            //--


            // Meta final
            end(95, 0, 95, "salvation" + random.Next(1, 4) + ".png", true);
            //--
        }

        public void level2()
        {
            double height = 12;
            string textureName = path + "wall.jpg";

            // Posicion inicial del personaje
            position[0] = 1;
            position[1] = 4;
            position[2] = 1;
            view[0] = 100;
            view[1] = 4;
            view[2] = 100;
            //--


            // Construccion de puertas
            Objects.Photo p;

            p = new Objects.Photo(0.1, 0, 5, "door2.png", 8, 1.5, true);
            photos.Add(p);
            p = new Objects.Photo(99.9, 0, 95, "door3.png", 8, 1.5, true);
            photos.Add(p);
            //--


            // Construccion de paredes X
            wallX(30, 50, 10, height, textureName, true, true);
            wallX(80, 90, 20, height, textureName, true, true);
            wallX(10, 20, 30, height, textureName, true, true);
            wallX(0, 30, 40, height, textureName, true, true);
            wallX(30, 50, 50, height, textureName, true, true);
            wallX(10, 70, 60, height, textureName, true, true);
            wallX(10, 80, 70, height, textureName, true, true);
            wallX(0, 90, 80, height, textureName, true, true);
            wallX(10, 100, 90, height, textureName, true, true);
            //--


            // Construccion de paredes Z
            wallZ(0, 30, 10, height, textureName, true, true);
            wallZ(60, 70, 10, height, textureName, true, true);
            wallZ(0, 20, 20, height, textureName, true, true);
            wallZ(20, 50, 30, height, textureName, true, true);
            wallZ(10, 50, 50, height, textureName, true, true);
            wallZ(0, 40, 60, height, textureName, true, true);
            wallZ(10, 60, 70, height, textureName, true, true);
            wallZ(20, 70, 80, height, textureName, true, true);
            wallZ(20, 80, 90, height, textureName, true, true);
            //--


            // Construccion de enemigos
            generateEnemies(30);
            //--


            // Meta final
            end(95, 0, 95, "salvation" + random.Next(1, 4) + ".png", true);
            //--
        }

        public void level3()
        {
            double height = 12;
            string textureName = path + "wall.jpg";

            // Posicion inicial del personaje
            position[0] = 95;
            position[1] = 4;
            position[2] = 5;
            view[0] = 100;
            view[1] = 4;
            view[2] = 100;
            //--


            // Construccion de puertas
            Objects.Photo p;

            p = new Objects.Photo(95, 0, 0.1, "door3.png", 8, 1.5, false);
            photos.Add(p);
            p = new Objects.Photo(5, 0, 99.9, "door4.png", 8, 1.5, false);
            photos.Add(p);
            //--


            // Construccion de paredes X
            wallX(0, 30, 10, height, textureName, true, true);
            wallX(60, 80, 10, height, textureName, true, true);
            wallX(20, 30, 20, height, textureName, true, true);
            wallX(0, 20, 60, height, textureName, true, true);
            wallX(50, 70, 70, height, textureName, true, true);
            wallX(30, 80, 80, height, textureName, true, true);
            wallX(0, 70, 90, height, textureName, true, true);
            wallX(80, 100, 90, height, textureName, true, true);
            //-


            // Construccion de paredes Z
            wallZ(20, 40, 20, height, textureName, true, true);
            wallZ(20, 80, 30, height, textureName, true, true);
            wallZ(0, 60, 40, height, textureName, true, true);
            wallZ(0, 70, 50, height, textureName, true, true);
            wallZ(10, 50, 60, height, textureName, true, true);
            wallZ(20, 70, 70, height, textureName, true, true);
            wallZ(10, 90, 80, height, textureName, true, true);
            wallZ(0, 80, 90, height, textureName, true, true);
            //--


            // Construccion de enemigos
            generateEnemies(35);
            //--


            // Meta final
            end(5, 0, 95, "salvation" + random.Next(1, 4) + ".png", true);
            //--
        }

        public void level4()
        {
            double height = 12;
            string textureName = path + "wall.jpg";

            // Posicion inicial del personaje
            position[0] = 98;
            position[1] = 4;
            position[2] = 5;
            view[0] = 100;
            view[1] = 4;
            view[2] = 100;
            //--


            // Construccion de puertas
            Objects.Photo p;

            p = new Objects.Photo(95, 0, 0.1, "door4.png", 8, 1.5, false);
            photos.Add(p);
            p = new Objects.Photo(5, 0, 99.9, "door5.png", 8, 1.5, false);
            photos.Add(p);
            //--


            // Construccion de paredes X
            wallX(0, 30, 10, height, textureName, true, true);
            wallX(60, 100, 10, height, textureName, true, true);
            wallX(0, 10, 20, height, textureName, true, true);
            wallX(30, 90, 20, height, textureName, true, true);
            wallX(40, 100, 30, height, textureName, true, true);
            wallX(60, 80, 40, height, textureName, true, true);
            wallX(20, 40, 60, height, textureName, true, true);
            wallX(70, 90, 60, height, textureName, true, true);
            wallX(10, 90, 70, height, textureName, true, true);
            wallX(20, 50, 80, height, textureName, true, true);
            wallX(70, 100, 80, height, textureName, true, true);
            wallX(0, 20, 90, height, textureName, true, true);
            wallX(60, 90, 90, height, textureName, true, true);
            //-


            // Construccion de paredes Z
            wallZ(20, 70, 10, height, textureName, true, true);
            wallZ(80, 90, 10, height, textureName, true, true);
            wallZ(20, 60, 20, height, textureName, true, true);
            wallZ(10, 50, 30, height, textureName, true, true);
            wallZ(80, 100, 30, height, textureName, true, true);
            wallZ(30, 60, 40, height, textureName, true, true);
            wallZ(0, 20, 50, height, textureName, true, true);
            wallZ(80, 100, 50, height, textureName, true, true);
            wallZ(40, 90, 60, height, textureName, true, true);
            wallZ(50, 60, 70, height, textureName, true, true);
            wallZ(40, 50, 80, height, textureName, true, true);
            wallZ(30, 60, 90, height, textureName, true, true);
            //--


            // Construccion de enemigos
            generateEnemies(40);
            //--


            // Meta final
            end(5, 0, 95, "salvation" + random.Next(1, 4) + ".png", true);
            //--
        }

        public void wallX(double x1, double x2, double z, double height, string textureName, bool hasColumnsInit, bool hasColumnsEnd)
        {
            int tree;
            Objects.Plane p;
            Objects.Rectangle r;
            Engine.Collision collision;
            Objects.Photo photo;

            collision = new Engine.Collision(x1, x2, z - 0.5, z + 0.5, 1);
            collisions.Add(collision);

            photo = new Objects.Photo(x1 + 5, 3.5, z + 0.1, "workArt" + random.Next(1, 19) + ".png", 4, 3, false);
            photos.Add(photo);

            tree = random.Next(1, 4);
            photo = new Objects.Photo(x1 + 8, 0, z + 0.2, "tree" + tree + ".png", 9, 2, false);
            photos.Add(photo);
            photo = new Objects.Photo(x1 + 3, 0, z + 0.2, "tree" + tree + ".png", 9, 2, false);
            photos.Add(photo);

            box(x1 + 1.1, 0, z + 4, "box" + random.Next(1, 5) + ".jpg", 2, 1);

            p = new Objects.Plane();
            p.textureName = textureName;
            for (double i = x1; i < x2; i += quality)
            {
                for (double j = 0; j < height; j += quality)
                {
                    r = new Objects.Rectangle();

                    r.points[0][0] = i;
                    r.points[0][1] = j;
                    r.points[0][2] = z;
                    r.points[1][0] = i + quality;
                    r.points[1][1] = j;
                    r.points[1][2] = z;
                    r.points[2][0] = i + quality;
                    r.points[2][1] = j + quality;
                    r.points[2][2] = z;
                    r.points[3][0] = i;
                    r.points[3][1] = j + quality;
                    r.points[3][2] = z;
                    p.rectangles.Add(r);
                }
            }
            if (hasColumnsInit)
            {
                for (double j = 0; j < height; j += quality)
                {
                    r = new Objects.Rectangle();

                    r.points[0][0] = x1;
                    r.points[0][1] = j;
                    r.points[0][2] = z - 0.2;
                    r.points[1][0] = x1;
                    r.points[1][1] = j;
                    r.points[1][2] = z + 0.2;
                    r.points[2][0] = x1;
                    r.points[2][1] = j + quality;
                    r.points[2][2] = z + 0.2;
                    r.points[3][0] = x1;
                    r.points[3][1] = j + quality;
                    r.points[3][2] = z - 0.2;
                    p.rectangles.Add(r);
                }
            }
            if (hasColumnsEnd)
            {
                for (double j = 0; j < height; j += quality)
                {
                    r = new Objects.Rectangle();
                    r.points[0][0] = x2;
                    r.points[0][1] = j;
                    r.points[0][2] = z - 0.2;
                    r.points[1][0] = x2;
                    r.points[1][1] = j;
                    r.points[1][2] = z + 0.2;
                    r.points[2][0] = x2;
                    r.points[2][1] = j + quality;
                    r.points[2][2] = z + 0.2;
                    r.points[3][0] = x2;
                    r.points[3][1] = j + quality;
                    r.points[3][2] = z - 0.2;
                    p.rectangles.Add(r);
                }
            }
            planes.Add(p);
        }

        public void wallZ(double z1, double z2, double x, double height, string textureName, bool hasColumnsInit, bool hasColumnsEnd)
        {
            int tree;
            Objects.Plane p;
            Objects.Rectangle r;
            Engine.Collision collision;
            Objects.Photo photo;

            collision = new Engine.Collision(x - 0.5, x + 0.5, z1, z2, 2);
            collisions.Add(collision);

            photo = new Objects.Photo(x + 0.1, 3.5, z2 - 5, "workArt" + random.Next(1, 19) + ".png", 4, 3, true);
            photos.Add(photo);

            tree = random.Next(1, 4);
            photo = new Objects.Photo(x + 0.2, 0, z2 - 8, "tree" + tree + ".png", 9, 2, true);
            photos.Add(photo);
            photo = new Objects.Photo(x + 0.2, 0, z2 - 3, "tree" + tree + ".png", 9, 2, true);
            photos.Add(photo);

            box(x + 4, 0, z2 - 2.1, "box" + random.Next(1, 5) + ".jpg", 3, 2);

            p = new Objects.Plane();
            p.textureName = textureName;
            for (double i = z1; i < z2; i += quality)
            {
                for (double j = 0; j < height; j += quality)
                {
                    r = new Objects.Rectangle();

                    r.points[0][0] = x;
                    r.points[0][1] = j;
                    r.points[0][2] = i;
                    r.points[1][0] = x;
                    r.points[1][1] = j;
                    r.points[1][2] = i + quality;
                    r.points[2][0] = x;
                    r.points[2][1] = j + quality;
                    r.points[2][2] = i + quality;
                    r.points[3][0] = x;
                    r.points[3][1] = j + quality;
                    r.points[3][2] = i;
                    p.rectangles.Add(r);
                }
            }
            if (hasColumnsInit)
            {
                for (double j = 0; j < height; j += quality)
                {
                    r = new Objects.Rectangle();

                    r.points[0][0] = x - 0.2;
                    r.points[0][1] = j;
                    r.points[0][2] = z1;
                    r.points[1][0] = x + 0.2;
                    r.points[1][1] = j;
                    r.points[1][2] = z1;
                    r.points[2][0] = x + 0.2;
                    r.points[2][1] = j + quality;
                    r.points[2][2] = z1;
                    r.points[3][0] = x - 0.2;
                    r.points[3][1] = j + quality;
                    r.points[3][2] = z1;
                    p.rectangles.Add(r);
                }
            }
            if (hasColumnsEnd)
            {
                for (double j = 0; j < height; j += quality)
                {
                    r = new Objects.Rectangle();

                    r.points[0][0] = x - 0.2;
                    r.points[0][1] = j;
                    r.points[0][2] = z2;
                    r.points[1][0] = x + 0.2;
                    r.points[1][1] = j;
                    r.points[1][2] = z2;
                    r.points[2][0] = x + 0.2;
                    r.points[2][1] = j + quality;
                    r.points[2][2] = z2;
                    r.points[3][0] = x - 0.2;
                    r.points[3][1] = j + quality;
                    r.points[3][2] = z2;
                    p.rectangles.Add(r);
                }
            }
            planes.Add(p);
        }


        public void enemy(double positionX, double positionY, double positionZ, string textureName, double height, double width)
        {
            Objects.Enemy e;
            Engine.Collision collision;

            e = new Objects.Enemy(positionX, positionY, positionZ, textureName, height, width);
            enemys.Add(e);
            enemys[enemys.Count - 1].indexCollision = collisions.Count;

            collision = new Engine.Collision(positionX - 2, positionX + 2, positionZ - 2, positionZ + 2, 3);
            collisions.Add(collision);
        }

        public void box(double positionX, double positionY, double positionZ, string textureName, double height, double width)
        {
            Objects.Box b;
            Engine.Collision collision;

            b = new Objects.Box(positionX, positionY, positionZ, textureName, height, width);
            boxs.Add(b);

            collision = new Engine.Collision(positionX - width - 1, positionX + width + 1, positionZ - width - 1, positionZ + width + 1, 5);
            collisions.Add(collision);
        }

        public void end(double positionX, double positionY, double positionZ, string textureName, bool isX)
        {
            Objects.Photo p;
            Engine.Collision collision;

            p = new Objects.Photo(positionX, positionY, positionZ, textureName, 6, 1, isX);
            photos.Add(p);

            collision = new Engine.Collision(positionX - 1, positionX + 1, positionZ - 1, positionZ + 1, 4);
            collisions.Add(collision);
        }

        public void generateEnemies(int amount)
        {
            int x, z;

            for (int i = 0; i < amount; i++)
            {
                x = random.Next(1, 19);
                z = random.Next(1, 19);

                // convertir a numeros impares
                if (x % 2 == 0)
                    x++;
                if (z % 2 == 0)
                    z++;
                //--

                int enemyNum = random.Next(1, 6);
                if (enemyNum == 3)
                    enemy(x * 5, 0, z * 5, "enemy3.png", 10, 2);
                else
                    enemy(x * 5, 0, z * 5, "enemy" + random.Next(1, 6) + ".png", random.Next(6, 9), 2);
            }
        }
    }
}