using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terminator.Engine
{
    class Camera
    {
        public double[] position = new double[4];
        public double[] view = new double[4];
        public double lookSpeedKeyX, lookSpeedKeyY, lookSpeedMouseX, lookSpeedMouseY, lookSpeed;
        public double moveSpeed;
        public bool levelCompleted;
        public bool isCollision;
        private int idCollision, idCollisionAux;
        private Engine.Sound sound;
        private Random random;

        public Camera(double positionX, double positionY, double positionZ, double viewX, double viewY, double viewZ, double lookSpeedKeyX, double lookSpeedKeyY, double lookSpeedMouseX, double lookSpeedMouseY, double moveSpeed)
        {
            // Esto es para un truco, paredes atravesables
            isCollision = true;
            //--


            // Efecto de golpe contra enemigo
            idCollisionAux = -1;
            sound = new Engine.Sound();
            random = new Random(DateTime.Now.Millisecond);
            //--


            position[0] = positionX;
            position[1] = positionY;
            position[2] = positionZ;
            position[3] = 1;
            view[0] = viewX;
            view[1] = viewY;
            view[2] = viewZ;
            view[3] = 1;
            this.lookSpeedKeyX = lookSpeedKeyX;
            this.lookSpeedKeyY = lookSpeedKeyY;
            this.lookSpeedMouseX = lookSpeedMouseX;
            this.lookSpeedMouseY = lookSpeedMouseY;
            this.moveSpeed = moveSpeed;
            levelCompleted = true;
        }

        private void lookDown()
        {
            if (view[1] > -100.0)
                view[1] -= lookSpeed;
        }

        private void lookLeft()
        {
            view = OperationMatrixVector.xVxM(FactoryMatrix.getTrans(-position[0], -position[1], -position[2]), view);
            view = OperationMatrixVector.xVxM(FactoryMatrix.getRotY(0.01 * lookSpeed), view);
            view = OperationMatrixVector.xVxM(FactoryMatrix.getTrans(position[0], position[1], position[2]), view);
        }

        private void lookRight()
        {
            view = OperationMatrixVector.xVxM(FactoryMatrix.getTrans(-position[0], -position[1], -position[2]), view);
            view = OperationMatrixVector.xVxM(FactoryMatrix.getRotY(-0.01 * lookSpeed), view);
            view = OperationMatrixVector.xVxM(FactoryMatrix.getTrans(position[0], position[1], position[2]), view);
        }

        private void lookUp()
        {
            if (view[1] < 250)
                view[1] += lookSpeed;
        }

        public void lookDownKey()
        {
            lookSpeed = lookSpeedKeyY;
            lookDown();
        }

        public void lookLeftKey()
        {
            lookSpeed = lookSpeedKeyX;
            lookLeft();
        }

        public void lookRightKey()
        {
            lookSpeed = lookSpeedKeyX;
            lookRight();
        }

        public void lookUpKey()
        {
            lookSpeed = lookSpeedKeyY;
            lookUp();
        }

        public void lookDownMouse(int mov)
        {
            lookSpeed = lookSpeedMouseY * mov;
            lookDown();
        }

        public void lookLeftMouse(int mov)
        {
            lookSpeed = lookSpeedMouseX * mov;
            lookLeft();
        }

        public void lookRightMouse(int mov)
        {
            lookSpeed = lookSpeedMouseX * mov;
            lookRight();
        }

        public void lookUpMouse(int mov)
        {
            lookSpeed = lookSpeedMouseY* mov;
            lookUp();
        }

        public void moveBack(List<Collision> colitions)
        {
            double[] vector = new double[4];

            vector = OperationMatrixVector.distance(view, position);
            vector = OperationMatrixVector.normalize(vector);
            vector = OperationMatrixVector.xVxM(FactoryMatrix.getRotY(Constant.ANGLE180), vector);
            vector = OperationMatrixVector.xVxS(vector, moveSpeed);
            move(vector, colitions);
        }

        public void moveForward(List<Collision> colitions)
        {
            double[] vector = new double[4];

            vector = OperationMatrixVector.distance(view, position);
            vector = OperationMatrixVector.normalize(vector);
            vector = OperationMatrixVector.xVxS(vector, moveSpeed);
            move(vector, colitions);
        }

        public void moveLeft(List<Collision> colitions)
        {
            double[] vector = new double[4];

            vector = OperationMatrixVector.distance(view, position);
            vector = OperationMatrixVector.normalize(vector);
            vector = OperationMatrixVector.xVxM(FactoryMatrix.getRotY(Constant.ANGLE90), vector);
            vector = OperationMatrixVector.xVxS(vector, moveSpeed);
            move(vector, colitions);
        }

        public void moveRight(List<Collision> colitions)
        {
            double[] vector = new double[4];

            vector = OperationMatrixVector.distance(view, position);
            vector = OperationMatrixVector.normalize(vector);
            vector = OperationMatrixVector.xVxM(FactoryMatrix.getRotY(-Constant.ANGLE90), vector);
            vector = OperationMatrixVector.xVxS(vector, moveSpeed);
            move(vector, colitions);
        }

        public void move(double[] vector, List<Collision> colitions)
        {
            int typeColition;

            typeColition = colition(position[0] + vector[0], position[2] + vector[2], colitions);

            if (typeColition == 4)
                levelCompleted = true;

            if (typeColition == 3 && idCollision != idCollisionAux)
            {
                idCollisionAux = idCollision;
                sound.play("collision" + random.Next(1, 4) + ".wav", false);
            }

            if (typeColition == 0 || isCollision == false)
            {
                position[1] = Engine.Constant.PLAYER_HEIGHT;
                position[0] += vector[0];
                view[0] += vector[0];
                position[2] += vector[2];
                view[2] += vector[2];
            }
            else
            {
                if (typeColition == 1)
                {
                    position[0] += vector[0];
                    view[0] += vector[0];
                }
                if (typeColition == 2)
                {
                    position[2] += vector[2];
                    view[2] += vector[2];
                }
                if (typeColition == 5)
                {
                    position[1] = 6;
                    position[0] += vector[0];
                    view[0] += vector[0];
                    position[2] += vector[2];
                    view[2] += vector[2];
                }
            }
        }

        public int colition(double positionX, double positionZ, List<Collision> colitions)
        {
            int nrColition = 0;

            idCollision = 0;
            foreach (Collision colition in colitions)
            {
                idCollision++;
                if (positionX > colition.position[0] && positionX < colition.position[1] && positionZ > colition.position[2] && positionZ < colition.position[3])
                {
                    if (nrColition == 0)
                        nrColition = colition.type;
                    else
                        return -1;
                }

                if (colition.type == 1)
                {
                    if (((positionX > colition.position[0] && positionX < colition.position[0] + 1) || (positionX > colition.position[1] - 1 && positionX < colition.position[1])) && positionZ > colition.position[2] - 0.5 && positionZ < colition.position[3] + 0.5)
                    {
                        if (nrColition == 0)
                        {
                            if (colition.type == 1)
                                nrColition = 2;
                            else
                                nrColition = 1;
                        }
                        else
                            return -1;
                    }
                }
                if (colition.type == 2)
                {
                    if (((positionZ > colition.position[2] && positionZ < colition.position[2] + 1) || (positionZ > colition.position[3] - 1 && positionZ < colition.position[3])) && positionX > colition.position[0] - 0.5 && positionX < colition.position[1] + 0.5)
                    {
                        if (nrColition == 0)
                        {
                            if (colition.type == 1)
                                nrColition = 2;
                            else
                                nrColition = 1;
                        }
                        else
                            return -1;
                    }
                }
            }
            return nrColition;
        }
    }
}
