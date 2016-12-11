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


        public void SetProjection(StaticShader shader, float FOV, float nearPlane, float farPlane)
        {
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 180 * 70, (Game.windowWidth / Game.windowHeight), 0.01f, 10000f);
            shader.startShader();
            shader.loadToProjectionMatrix(projection);
            shader.stopShader();

            
        }

        public void prepareToRender()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
        }
        

        public void render(Entity entity, StaticShader shader)
        {

            //TODO: Fix null pointer
            GL.BindVertexArray(entity.getModel().getVertex().getVAOID());

            for(int i = 0; i < entity.getModel().getVertex().getVAOLength(); i++)
            {
                GL.EnableVertexAttribArray(i);
            }



            //entity.transformationMatrix *= Matrix4.CreateRotationZ((float)Math.PI / 180 * 2);
            //entity.transformationMatrix *= Matrix4.Rotate(new Vector3(entity.position.X, entity.position.Y, entity.position.Z), (float)Math.PI / 180 * 2);

            //entity.transformationMatrix = Matrix4.CreateTranslation(0.75f,0,-2);

            shader.loadToTransformationMatrix(entity.transformationMatrix);

            GL.ActiveTexture(TextureUnit.Texture0);

            GL.BindTexture(TextureTarget.Texture2D, entity.getModel().getTexture().getTextureID());

            GL.DrawElements(PrimitiveType.Triangles, entity.getModel().getVertex().getVertexCount(), DrawElementsType.UnsignedInt, 0);


            for (int i = 0; i < entity.getModel().getVertex().getVAOLength(); i++)
            {
                GL.DisableVertexAttribArray(i);
            }

            GL.BindVertexArray(0);
        }

    }
}
