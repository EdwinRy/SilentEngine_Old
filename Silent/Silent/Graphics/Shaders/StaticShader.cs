using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Graphics.Shaders
{
    class StaticShader : ShaderProgram
    {

        private const string M_VERTEXSHADER = "VertexShader.txt";
        private const string M_FRAGMENTSHADER = "FragmentShader.txt";

        protected override void bindAttributes()
        {
            bindAttribute(0, "position");
        }

        protected override void getAllUniformLocations()
        {
            
        }
    }
}
