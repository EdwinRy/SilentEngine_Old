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

        //View Matrix
        public Vector3f position = new Vector3f();
        public Vector3f rotationAxis = new Vector3f();
        public float rotationAngle = 0;
        public float yaw;
        public float roll;
        //------------------------------------------------

        //Projection Matrix
        public static float fov = 70;
        public static float nearPlane = 0.01f;
        public static float farPlane = 10000;
        //------------------------------------------------

        public void SetCameraProjectionMatrix(StaticShader shader, int windowWidth, int windowHeight)
        {
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 180 * fov, (windowWidth / windowHeight), nearPlane, farPlane);
            shader.startShader();
            shader.loadToProjectionMatrix(projection);
            shader.stopShader();
        }
        
        public void SetCameraViewMatrix(StaticShader shader)
        {
            Matrix4 view = Matrix4.Identity;
            shader.startShader();
            shader.loadToViewMatrix(view);
            shader.stopShader();
        }
        
    }
}
