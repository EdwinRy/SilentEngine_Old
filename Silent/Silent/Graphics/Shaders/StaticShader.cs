using OpenTK;
using Silent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Graphics.Shaders
{
    public class StaticShader : ShaderProgram
    {

        private const string M_VERTEXSHADER = "Graphics/Shaders/VertexShader.txt";
        private const string M_FRAGMENTSHADER = "Graphics/Shaders/FragmentShader.txt";

        private int transformationMatrix;
        private int projectionMatrix;
        private int viewMatrix;
        private int lightPosition;
        private int lightColour;
        private int shiness;
        private int reflectivity;

        public StaticShader() : base(M_VERTEXSHADER, M_FRAGMENTSHADER)
        {
            ;
        }
        protected override void BindAttributes()
        {
            base.BindAttribute(0, "position");
            base.BindAttribute(1, "textureCoords");
            base.BindAttribute(2, "normals");
        }

        protected override void GetAllUniformLocations()
        {
            transformationMatrix = base.GetUniformLocation("transformationMatrix");
            projectionMatrix = base.GetUniformLocation("projectionMatrix");
            viewMatrix = base.GetUniformLocation("viewMatrix");
            lightPosition = base.GetUniformLocation("lightPosition");
            lightColour = base.GetUniformLocation("lightColour");
            shiness = base.GetUniformLocation("shiness");
            reflectivity = base.GetUniformLocation("reflectivity");
        }
        
        public void LoadEntityShiness(float shiness, float reflection)
        {
            base.LoadToFloat(this.shiness, shiness);
            base.LoadToFloat(this.reflectivity, reflection);
        }

        public void LoadToTransformationMatrix(Matrix4 matrix)
        {
            base.LoadToMatrix(transformationMatrix, matrix);
        }

        public void LoadToProjectionMatrix(Matrix4 matrix)
        {
            base.LoadToMatrix(projectionMatrix, matrix);
        }

        public void LoadToViewMatrix(Matrix4 matrix)
        {
            base.LoadToMatrix(viewMatrix, matrix);
            
        }


        public void LoadLight(Silent_Light light)
        {
            base.LoadToVector3(lightPosition, new Vector3(light.LightPosition.X, light.LightPosition.Y, light.LightPosition.Z));
            base.LoadToVector3(lightColour, new Vector3(light.LightColour.X, light.LightColour.Y, light.LightColour.Z));
        }
    }
}
