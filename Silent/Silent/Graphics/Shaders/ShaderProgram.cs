using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics.OpenGL4;
using OpenTK;

namespace Silent.Graphics.Shaders
{
    public abstract class ShaderProgram
    {
        private int m_shaderID;
        private int m_vertexShaderID;
        private int m_fragmentShaderID;

        public ShaderProgram(string vertexShaderFile = "Graphics/Shaders/VertexShader.txt", string fragmentShaderFile = "Graphics/Shaders/FragmentShader.txt")
        {
            m_vertexShaderID = LoadShader(vertexShaderFile, ShaderType.VertexShader);
            m_fragmentShaderID = LoadShader(fragmentShaderFile, ShaderType.FragmentShader);
            m_shaderID = GL.CreateProgram();
            GL.AttachShader(m_shaderID, m_vertexShaderID);
            GL.AttachShader(m_shaderID, m_fragmentShaderID);
            BindAttributes();
            GL.LinkProgram(m_shaderID);
            GL.ValidateProgram(m_shaderID);
            GetAllUniformLocations();
        }

        public void LoadToFloat(int location, float value)
        {
            GL.Uniform1(location, value);
        }

        public void LoadToVector3(int location, Vector3 value)
        {
            GL.Uniform3(location, value);
        }

        public void LoadToMatrix(int location, Matrix4 value)
        {
            GL.UniformMatrix4(location, false, ref value);
        }

        protected abstract void GetAllUniformLocations();

        protected int GetUniformLocation(string uniformName)
        {
            return GL.GetUniformLocation(m_shaderID, uniformName);
        }

        protected abstract void BindAttributes();

        protected void BindAttribute(int attribute, string variableName)
        {
            GL.BindAttribLocation(m_shaderID, attribute, variableName);

        }

        public void StartShader()
        {
            GL.UseProgram(m_shaderID);
        }

        public void StopShader()
        {
            GL.UseProgram(0);
        }

        public void CleanUpShader()
        {
            StopShader();
            GL.DetachShader(m_shaderID, m_vertexShaderID);
            GL.DetachShader(m_shaderID, m_fragmentShaderID);
            GL.DeleteShader(m_vertexShaderID);
            GL.DeleteShader(m_fragmentShaderID);
            GL.DeleteProgram(m_shaderID);
        }

        private static int LoadShader(string file, ShaderType type) {
            //string[] lines = System.IO.File.ReadAllLines(file);
            string lines = File.ReadAllText(file);
            int shaderID = GL.CreateShader(type);
            GL.ShaderSource(shaderID, lines);
            GL.CompileShader(shaderID);
            return shaderID;
        }
    }
}
