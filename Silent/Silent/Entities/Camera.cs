using OpenTK;
using Silent.Graphics.Shaders;
using Silent.Maths;
using System;

namespace Silent.Entities
{
    public class Camera
    {
        //The amount of cameras in the game
        public static int cameraCount = 0;

        //Name of the camera if not changed it will be named Camera *number of cameras*
        public string cameraName;

        public Camera()
        {
            //Constructor increases number of cameras
            cameraName = "Camera" + cameraCount;
            cameraCount += 1;
        }

        //View Matrix
        public Matrix4 view = Matrix4.Identity; // A 4 by 4 matrix filled with 1s
        public Vector3f position = new Vector3f(); //Positon in the 3D space
        public Vector3f rotationAxis = new Vector3f();  //Axis of rotation
        public float rotationAngle = 0; //Rotation angle
        public float yaw = 0;
        public float roll = 0;
        //------------------------------------------------

        //Projection Matrix
        Matrix4 projection;
        public static float fov = 70; //The view angle
        public static float nearPlane = 0.1f; //Nearest render distance
        public static float farPlane = 10000; //Furthest render distance
        //------------------------------------------------

        //Set the projection of the camera
        public void SetCameraProjectionMatrix(Shader shader, int windowWidth, int windowHeight)
        {
            //Set the projection matrix to a new created 4*4 projection matrix
            projection = Matrix4.CreatePerspectiveFieldOfView(
                (float)Math.PI / 180 * fov, //FOV in radians (divide PI by 180 and multiply by x to get its radians value)
                (windowWidth / windowHeight), //Aspect Ratio of the screen
                nearPlane, //Close plane, if the vertices are closer than this they won't be rendered 
                farPlane  //Far plane - vertices further will not be rendered
                );

            //Start the shader to enable loading uniform variables
            shader.StartShader();
            //Load the projection matrix of the camera into the shader
            shader.LoadToProjectionMatrix(projection);
            //Stop the shader
            shader.StopShader();
        }
       
        //Set the position of the camera in 3D space 
        public void SetCameraViewMatrix(Shader shader)
        {
            //Enable shader features
            shader.StartShader();
            //Set current matrix to 4*4 matrix filled with 1s
            view = Matrix4.Identity;
            //Load the matrix into uniform variable
            shader.LoadToViewMatrix(view);
            //Stop the shader
            shader.StopShader();
        }
        
        
        //Apply translation onto the camera in 3D space
        public void Translate(Vector3f applyTranslation)
        {
            /*
            position.X -= applyTranslation.X;  //Not Needed since the operator for custom Class
            position.Y -= applyTranslation.Y;  //Vector3f was overloaded to support easier was of subtraction 
            position.Z -= applyTranslation.Z;  //See Vector3f.cs for more information
             */

            position -= applyTranslation; //Overloaded operator

            //apply the transformation onto the view matrix (basically move the camera, or rather the world around it) 
            view *= Matrix4.CreateTranslation(-applyTranslation.X, -applyTranslation.Y, -applyTranslation.Z);
        }

        //Overloaded method, does the same thing as the one that takes in Vector3f
        public void Translate(float positionX, float positionY, float positionZ)
        {
            position.X -= positionX;
            position.Y -= positionY;
            position.Z -= positionZ;
            view *= Matrix4.CreateTranslation(-positionX, -positionY, -positionZ);
        }

        //Rotate the camera in the 3D space (the world around the camera)
        public void Rotate(Vector3f rotationAxis, float rotation)
        {
            /*
            this.rotationAxis.X += rotationAxis.X; //Same as with Translation
            this.rotationAxis.Y += rotationAxis.Y;
            this.rotationAxis.Z += rotationAxis.Z;
            */

            this.rotationAxis += rotationAxis; //Overloaded operator

            this.rotationAngle += rotation; //Increase the camera rotation angle
            //Apply the translation onto the view matrix
            view *= Matrix4.CreateFromAxisAngle(new Vector3(rotationAxis.X, rotationAxis.Y, rotationAxis.Z), rotation);
        }

        //Overloaded method for rotating
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
