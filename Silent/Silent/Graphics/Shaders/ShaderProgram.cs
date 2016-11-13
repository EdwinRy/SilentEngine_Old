using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace Silent.Graphics.Shaders
{
    public abstract class ShaderProgram
    {
        private int m_shaderID;
        private int m_vertexShaderID;
        private int m_fragmentShaderID;

        public ShaderProgram(string vertexShaderFile = "C:/Users/Edwin/Source/Repos/SilentEngine/Silent/Silent/Graphics/Shaders/VertexShader.txt", string fragmentShaderFile = "C:/Users/Edwin/Source/Repos/SilentEngine/Silent/Silent/Graphics/Shaders/VertexShader.txt")
        {
            m_vertexShaderID = loadShader(vertexShaderFile, ShaderType.VertexShader);
            m_fragmentShaderID = loadShader(fragmentShaderFile, ShaderType.FragmentShader);
            m_shaderID = GL.CreateProgram();
            GL.AttachShader(m_shaderID, m_vertexShaderID);
            GL.AttachShader(m_shaderID, m_fragmentShaderID);
            bindAttributes();
            GL.LinkProgram(m_shaderID);
            GL.ValidateProgram(m_shaderID);
            getAllUniformLocations();
        }

        protected abstract void getAllUniformLocations();

        protected int getUniformLocation(string uniformName)
        {
            return GL.GetUniformLocation(m_shaderID, uniformName);
        }

        protected abstract void bindAttributes();

        protected void bindAttribute(int attribute, string variableName)
        {
            GL.BindAttribLocation(m_shaderID, attribute, variableName);
        }

        public void startShader()
        {
            GL.UseProgram(m_shaderID);
        }

        public void stopShader()
        {
            GL.UseProgram(0);
        }

        public void cleanUpShader()
        {
            stopShader();
            GL.DetachShader(m_shaderID, m_vertexShaderID);
            GL.DetachShader(m_shaderID, m_fragmentShaderID);
            GL.DeleteShader(m_vertexShaderID);
            GL.DeleteShader(m_fragmentShaderID);
            GL.DeleteProgram(m_shaderID);
        }

        private static int loadShader(string file, ShaderType type) {
            /*
            StringBuilder shaderSource = new StringBuilder();
            try {
                
                
                StreamReader reader = new StreamReader(file);
                string line;
                while ((line = reader.ReadLine()) != null) {
                    shaderSource.Append(line).Append("//\n");
                }
                reader.Close();
                
            }catch (IOException e) {
                Console.WriteLine(e.StackTrace);
            }
            */
            string[] lines = System.IO.File.ReadAllLines(file.ToString());
            int shaderID = GL.CreateShader(type);
            GL.ShaderSource(shaderID, lines.ToString());
            GL.CompileShader(shaderID);
            Console.WriteLine(GL.GetError());
            return shaderID;
        }
    }
}
