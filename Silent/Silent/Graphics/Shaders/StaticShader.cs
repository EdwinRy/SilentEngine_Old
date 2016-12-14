using OpenTK;
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
    }
}
