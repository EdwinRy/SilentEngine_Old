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
            
        }
    }
}
