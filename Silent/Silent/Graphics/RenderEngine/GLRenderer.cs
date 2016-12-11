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


        public void GLRenderer1(StaticShader shader)
        {
            float aspectRation = (float)Game.windowWidth / (float)Game.windowHeight;
            float y_scale = (float) (1f / Math.Tan(Math.PI / 180 * (90 / 2f))) * aspectRation;
            float x_scale = y_scale / aspectRation;
            float frustum_length = 10000 - 0.01f;

            Matrix4 projection1 = new Matrix4();
            projection1.M11 = x_scale;
            projection1.M22 = y_scale;
            projection1.M33 = -((1000 - 0.01f) / frustum_length);
            projection1.M34 = -1;
            projection1.M43 = -((2 * 0.01f * 10000) / frustum_length);
            projection1.M44 = 0;

            Matrix4 projection2 = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 180 * 70, (Game.windowWidth / Game.windowHeight), 0.01f, 10000f);

            Console.WriteLine("Projection1:" + projection1);
            Console.WriteLine("Projection2:" + projection2);

            

            shader.startShader();
            shader.loadToProjectionMatrix(projection2);
            shader.stopShader();
        }

        public void prepareToRender()
        {
            GL.Enable(EnableCap.DepthTest);
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
            entity.transformationMatrix *= Matrix4.Rotate(new Vector3(entity.position.X, entity.position.Y, entity.position.Z), (float)Math.PI / 180 * 2);

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
