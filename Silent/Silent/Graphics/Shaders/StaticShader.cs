using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Graphics.Shaders
{
    class StaticShader : ShaderProgram
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
        protected override void bindAttributes()
        {
            base.bindAttribute(0, "position");
            base.bindAttribute(1, "textureCoords");
        }

        protected override void getAllUniformLocations()
        {
            transformationMatrix = base.getUniformLocation("transformationMatrix");
            projectionMatrix = base.getUniformLocation("projectionMatrix");
            viewMatrix = base.getUniformLocation("viewMatrix");
        }
        
        public void loadToTransformationMatrix(Matrix4 matrix)
        {
            base.loadToMatrix(transformationMatrix, matrix);
        }

        public void loadToProjectionMatrix(Matrix4 matrix)
        {
            base.loadToMatrix(projectionMatrix, matrix);
        }

        public void loadToViewMatrix(Matrix4 matrix)
        {
            base.loadToMatrix(viewMatrix, matrix);
        }
    }
}
