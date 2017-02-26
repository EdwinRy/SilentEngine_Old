using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Silent.Maths;
using Silent.Entities;

namespace Silent.Tools
{
    public static class OBJModelLoader
    {

        public static void Load(string path,out Silent_Model model)
        {
            StreamReader file = new StreamReader(path);

            string CurrentLine;

            List<Vector3f> vertices = new List<Vector3f>();
            List<Vector2f> textures = new List<Vector2f>();
            List<Vector3f> normals = new List<Vector3f>();
            List<int> indices = new List<int>();

            float[] ArrayOfVertices = null;
            float[] ArrayOfNormals = null;
            float[] ArrayOfTextures = null;
            int[] ArrayOfIndices = null;


            while (true)
            {
                CurrentLine = file.ReadLine();
                string[] currentLine = CurrentLine.Split(' ');

                if (CurrentLine.StartsWith("v "))
                {
                    vertices.Add(new Vector3f(float.Parse(currentLine[1]), float.Parse(currentLine[2]), float.Parse(currentLine[3])));
                }

                if (CurrentLine.StartsWith("vt "))
                {
                    textures.Add(new Vector2f(float.Parse(currentLine[1]), float.Parse(currentLine[2])));
                }

                if (CurrentLine.StartsWith("vn "))
                {
                    normals.Add(new Vector3f(float.Parse(currentLine[1]), float.Parse(currentLine[2]), float.Parse(currentLine[3])));
                }
                if (CurrentLine.StartsWith("f "))
                {
                    ArrayOfTextures = new float[vertices.Count() * 2];
                    ArrayOfNormals = new float[vertices.Count() * 3];
                    break;
                }
            }

            while (CurrentLine != null)
            {
                if (!CurrentLine.StartsWith("f "))
                {
                    CurrentLine = file.ReadLine();
                    continue;
                }

                string[] currentLine = CurrentLine.Split(' ');
                string[] vertex1 = currentLine[1].Split('/');
                string[] vertex2 = currentLine[2].Split('/');
                string[] vertex3 = currentLine[3].Split('/');

                ProcessFace(vertex1, indices, textures, normals, ArrayOfTextures, ArrayOfNormals);
                ProcessFace(vertex2, indices, textures, normals, ArrayOfTextures, ArrayOfNormals);
                ProcessFace(vertex3, indices, textures, normals, ArrayOfTextures, ArrayOfNormals);

                CurrentLine = file.ReadLine();

            }
            file.Close();


            ArrayOfVertices = new float[vertices.Count*3];
            ArrayOfIndices = new int[indices.Count];

            int vertexPointer = 0;

            foreach(Vector3f vertex in vertices)
            {
                ArrayOfVertices[vertexPointer++] = vertex.X;
                ArrayOfVertices[vertexPointer++] = vertex.Y;
                ArrayOfVertices[vertexPointer++] = vertex.Z;
            }

            for(int i = 0; i < indices.Count; i++)
            {
                ArrayOfIndices[i] = indices[i];
            }

             /*
            foreach(float item in indices)
            {
                Console.WriteLine(item);
            }  */

            model = new Silent_Model();
            model.Vertices = ArrayOfVertices;
            model.TextureCoords = ArrayOfTextures;
            model.Normals = ArrayOfNormals;
            model.Indices = ArrayOfIndices;

        }

        private static void ProcessFace(
            string[] vertex,
            List<int> indices,
            List<Vector2f> textures,
            List<Vector3f>normals,
            float[] textureArray,
            float[] normalArray)
        {

            int currentVertexPointer = int.Parse(vertex[0]) - 1;

            indices.Add(currentVertexPointer);

            Vector2f currentTex = textures[int.Parse(vertex[1]) - 1];
            textureArray[currentVertexPointer * 2] = currentTex.X;
            textureArray[currentVertexPointer * 2+1] = 1 - currentTex.Y;

            Vector3f currentNorm = normals[int.Parse(vertex[2]) - 1];
            normalArray[currentVertexPointer * 3] = currentNorm.X;
            normalArray[currentVertexPointer * 3+1] = currentNorm.Y;
            normalArray[currentVertexPointer * 3+2] = currentNorm.Z;

        }
         
        
    }
}
