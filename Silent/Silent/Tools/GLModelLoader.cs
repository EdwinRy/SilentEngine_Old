using Silent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;

namespace Silent.Tools
{
    public class GLModelLoader
    {

        //create list to store vbos
        private List<int> vbos = new List<int>();

        //create list to store vbos
        private List<int> vaos = new List<int>();                                         


        private int vaoLength;

        public GLModelLoader()
        {
            vaoLength = 0;
        }

        public Texture loadTexture(string texturePath)
        {
            Bitmap bmp = new Bitmap(texturePath);
            int texID = GL.GenTexture();

            GL.ClearColor(Color.MidnightBlue);
            GL.Enable(EnableCap.Texture2D);

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out texID);
            GL.BindTexture(TextureTarget.Texture2D, texID);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);
            /*
            int texID = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, texID);

            Bitmap bmp = new Bitmap(texturePath);
            //bmp.Save(texturePath);

            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                bmp.Width, bmp.Height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra,
                PixelType.UnsignedByte, bmpData.Scan0);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            bmp.UnlockBits(bmpData);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);



            GL.BindTexture(TextureTarget.Texture2D, 0);
            */
            return new Texture(texID);
        }

        public Vertex load(float[] vertex_data, int[] indices, float[] texture_data, float[] normals)
        {
            int vao = createVAO();
                                                                             
            bindIndicesBuffer(indices);

            storeDataInVBO(3, vertex_data);
            storeDataInVBO(2, texture_data);
            storeDataInVBO(3, normals);


            unbindVAO();

            return new Vertex(vao, vertex_data.Length, vaoLength); 

        }

        private int createVAO()
        {

            //generate a new vao
            int vaoID = GL.GenVertexArray();

            //add vao to be deleted later on
            vaos.Add(vaoID);

            //bind the vao                                                                         
            GL.BindVertexArray(vaoID);

            //return the ID                                                       
            return vaoID;

        }

        private void unbindVAO()
        {

            //Binds to null
            GL.BindVertexArray(0);
        }

        private void storeDataInVBO(int dataSize, float[] data)
        {
            //create new vbo
            int vbo = createVBO();

            //store the data into vbo
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);

            //Put the VBO into a VAO
            GL.VertexAttribPointer(vaoLength, dataSize, VertexAttribPointerType.Float, false, 0, 0);

            //Increase the size of the VAO (how many elements are in the VAO)
            vaoLength += 1;

            unbindVBO();
        }

        private int createVBO()
        {

            int vboID = GL.GenBuffer();
            vbos.Add(vboID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            return vboID;

        }

        private void unbindVBO()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private void bindIndicesBuffer(int[] indices)
        {
            //generate new VBO
            int vboID = GL.GenBuffer();

            //add the VBO into the list of VBOs
            vbos.Add(vboID);

            //Bind the VBO ot be used
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vboID);

            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length, indices, BufferUsageHint.StaticDraw);
        }

        public void cleanUp()
        {
            foreach (int vbo in vbos)
            {
                //Delete VBOs
                GL.DeleteBuffer(vbo);
            }

            foreach (int vao in vaos)
            {
                //Delete VAOs
                GL.DeleteVertexArray(vao);
            }
        }

    }
}
