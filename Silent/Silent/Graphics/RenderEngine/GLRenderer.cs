using Silent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Silent.Graphics.RenderEngine
{
    class GLRenderer
    {
        /*
        public void prepareToRender()
        {
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
        }
        */

        public void render(Entity entity)
        {
            //TODO: Fix null pointer
            GL.BindVertexArray(entity.getModel().getVertex().getVAOID());

            for(int i = 0; i < entity.getModel().getVertex().getVAOLength(); i++)
            {
                GL.EnableVertexAttribArray(i);
            }

            GL.ActiveTexture(TextureUnit.Texture0);

            GL.BindTexture(TextureTarget.Texture2D, entity.getModel().getTexture().getTextureID());

            GL.DrawElements(PrimitiveType.Triangles, entity.getModel().getVertex().getVertexCount(), DrawElementsType.UnsignedInt, 0);

            /*
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.AliceBlue);
            GL.Vertex2(-0.5f, 0.5f);
            GL.Color3(Color.AntiqueWhite);
            GL.Vertex2(-0.5f, -0.5f);
            GL.Color3(Color.Red);
            GL.Vertex2(0.5f, -0.5f);
            GL.End();
            */

            for (int i = 0; i < entity.getModel().getVertex().getVAOLength(); i++)
            {
                GL.DisableVertexAttribArray(i);
            }

            GL.BindVertexArray(0);
        }

    }
}
