using Silent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

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
            GL.BindVertexArray(entity.getModel().getVertex().getVAOID());

            for(int i = 0; i < entity.getModel().getVertex().getVAOLength(); i++)
            {
                GL.EnableVertexAttribArray(i);
            }

            GL.DrawElements(PrimitiveType.Triangles, entity.getModel().getVertex().getVertexCount(), DrawElementsType.UnsignedByte, 0);

            for (int i = 0; i < entity.getModel().getVertex().getVAOLength(); i++)
            {
                GL.DisableVertexAttribArray(i);
            }

            GL.BindVertexArray(0);
        }

    }
}
