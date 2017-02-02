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
        

        public void Render(Silent_Entity entity, Shader shader)
        {
            GL.BindVertexArray(entity.EntityModel.ModelVertex.GetVAOID());

            for(int i = 0; i < entity.EntityModel.ModelVertex.GetVAOLength(); i++)
            {
                GL.EnableVertexAttribArray(i);
            }

            shader.LoadToTransformationMatrix(entity.EntityTransformationMatrix);

            GL.ActiveTexture(TextureUnit.Texture0);

            GL.BindTexture(TextureTarget.Texture2D, entity.EntityModel.ModelTexture.GetTextureID());

            GL.DrawElements(PrimitiveType.Triangles, entity.EntityModel.ModelVertex.GetVertexCount(), DrawElementsType.UnsignedInt, 0);

            for (int i = 0; i < entity.EntityModel.ModelVertex.GetVAOLength(); i++)
            {
                GL.DisableVertexAttribArray(i);
            }

            GL.BindVertexArray(0);
        }

    }
}
