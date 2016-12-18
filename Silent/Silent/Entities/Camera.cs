using OpenTK;
using Silent.GameSystem;
using Silent.Graphics.Shaders;
using Silent.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Entities
{
    public class Camera
    {
        public static int cameraCount = 0;
        public string cameraName = "Camera"+cameraCount;

        public Camera()
        {
            cameraCount += 1;
        }

        //View Matrix
        public Matrix4 view = Matrix4.Identity;
        public Vector3f position = new Vector3f();
        public Vector3f rotationAxis = new Vector3f();
        public float rotationAngle = 0;
        public float yaw;
        public float roll;
        //------------------------------------------------

        //Projection Matrix
        Matrix4 projection;
        public static float fov = 70;
        public static float nearPlane = 0.1f;
        public static float farPlane = 10000;
        //------------------------------------------------

        public void SetCameraProjectionMatrix(StaticShader shader, int windowWidth, int windowHeight)
        {
            projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 180 * fov, (windowWidth / windowHeight), nearPlane, farPlane);
            shader.StartShader();
            shader.LoadToProjectionMatrix(projection);
            shader.StopShader();
        }
        
        public void SetCameraViewMatrix(StaticShader shader)
        {
            //view = Matrix4.Identity;
            shader.StartShader();
            view = Matrix4.Identity;
            shader.LoadToViewMatrix(view);
            shader.StopShader();
        }
        
        
        public void Translate(Vector3f applyTranslation)
        {

            position.X -= applyTranslation.X;
            position.Y -= applyTranslation.Y;
            position.Z -= applyTranslation.Z;

            view *= Matrix4.CreateTranslation(-applyTranslation.X, -applyTranslation.Y, -applyTranslation.Z);
        }

        public void Translate(float positionX, float positionY, float positionZ)
        {
            position.X -= positionX;
            position.Y -= positionY;
            position.Z -= positionZ;
            view *= Matrix4.CreateTranslation(-positionX, -positionY, -positionZ);
        }

        public void Rotate(Vector3f rotationAxis, float rotation)
        {
            this.rotationAxis.X += rotationAxis.X;
            this.rotationAxis.Y += rotationAxis.Y;
            this.rotationAxis.Z += rotationAxis.Z;
            this.rotationAngle += rotation;
            view *= Matrix4.CreateFromAxisAngle(new Vector3(rotationAxis.X, rotationAxis.Y, rotationAxis.Z), rotation);
        }

        public void Rotate(float rotationAxisX, float rotationAxisY, float rotationAxisZ, float rotation)
        {
            this.rotationAxis.X += rotationAxisX;
            this.rotationAxis.Y += rotationAxisY;
            this.rotationAxis.Z += rotationAxisZ;
            this.rotationAngle += rotation;
            view *= Matrix4.CreateFromAxisAngle(new Vector3(rotationAxisX, rotationAxisY, rotationAxisZ), rotation);
        }
        

    }
}
