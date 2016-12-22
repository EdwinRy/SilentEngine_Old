using Silent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using Silent.Graphics.Shaders;
using OpenTK;
using Silent.GameSystem;

namespace Silent.Graphics.RenderEngine
{
    class GLRenderer
    {

        public void PrepareToRender()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
        }
        

        public void Render(Entity entity, StaticShader shader)
        {

            GL.BindVertexArray(entity.GetModel().getVertex().getVAOID());

            for(int i = 0; i < entity.GetModel().getVertex().getVAOLength(); i++)
            {
                GL.EnableVertexAttribArray(i);
            }

            shader.LoadToTransformationMatrix(entity.transformationMatrix);

            GL.ActiveTexture(TextureUnit.Texture0);

            GL.BindTexture(TextureTarget.Texture2D, entity.GetModel().getTexture().getTextureID());

            GL.DrawElements(PrimitiveType.Triangles, entity.GetModel().getVertex().getVertexCount(), DrawElementsType.UnsignedInt, 0);

            for (int i = 0; i < entity.GetModel().getVertex().getVAOLength(); i++)
            {
                GL.DisableVertexAttribArray(i);
            }

            GL.BindVertexArray(0);
        }

    }
}
