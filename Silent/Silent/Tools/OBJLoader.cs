using Silent.Entities;
using Silent.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;


namespace Silent.Entities
{
    class OBJLoader
    {

        public static void loadObjModel(string fileName,GLModelLoader loader,string texturePath, out Model model)
        {
            string[] lines = System.IO.File.ReadAllLines(fileName);
            float[] vertices = null;
            float[] textures = null;
            float[] normals = null;
            int[] indices = null;

            List<Vector3d> temp_vertices = new List<Vector3d>();
            List<Vector2d> temp_textures = new List<Vector2d>();
            List<Vector3d> temp_normals = new List<Vector3d>();
            List<int> temp_indices = new List<int>();

            bool texturesNormals_initialized = false;

            foreach (string line in lines)
            {
                //Vertices
                if (line.StartsWith("v"))
                {
                    string[] currentLine = line.Split(' ');
                    Console.WriteLine(currentLine);
                    temp_vertices.Add(
                        new Vector3d(
                            float.Parse(currentLine[1]), 
                            float.Parse(currentLine[2]),
                            float.Parse(currentLine[3])));

                }

                //Textures
                if (line.StartsWith("vt"))
                {
                    string[] currentLine = line.Split(' ');
                    Console.WriteLine(currentLine);
                    temp_textures.Add(
                        new Vector2d(
                            float.Parse(currentLine[1]),
                            float.Parse(currentLine[2])));
                }

                //Normals
                if (line.StartsWith("vn"))
                {

                    string[] currentLine = line.Split(' ');
                    Console.WriteLine(currentLine);
                    temp_normals.Add(
                        new Vector3d(
                            float.Parse(currentLine[1]),
                            float.Parse(currentLine[2]),
                            float.Parse(currentLine[3])));

                }

                //Triangles
                if (line.StartsWith("f"))
                {
                    if (!texturesNormals_initialized)
                    {
                        textures = new float[vertices.Length * 2];
                        normals = new float[vertices.Length * 3];

                        texturesNormals_initialized = true;
                    }
                    string[] currentLine = line.Split(' ');
                    string[] vertex1 = currentLine[1].Split('/');
                    string[] vertex2 = currentLine[2].Split('/');
                    string[] vertex3 = currentLine[3].Split('/');

                    processVertices(vertex1, temp_indices, temp_textures, temp_normals, textures, normals);
                    processVertices(vertex2, temp_indices, temp_textures, temp_normals, textures, normals);
                    processVertices(vertex3, temp_indices, temp_textures, temp_normals, textures, normals);
                }
            }

            vertices = new float[temp_vertices.Count() * 3];
            indices = new int[temp_indices.Count()];

            int vertPointer = 0;

            foreach (Vector3d loop_vertex in temp_vertices)
            {
                vertices[vertPointer++] = (float) loop_vertex.X;
                vertices[vertPointer++] = (float)loop_vertex.Y;
                vertices[vertPointer++] = (float)loop_vertex.Z;
            }
            for (int i = 0; i < indices.Length; i++)
            {
                indices[i] = temp_indices[i];
            }


            Vertex vertex;
            Texture texture;

            vertex = loader.load(vertices, indices, textures, normals);
            texture = loader.loadTexture(texturePath);

            model = new Model(vertex, texture);


        }

        private static void processVertices(string[] vertexData, List<int> temp_indices, List<Vector2d> temp_textures,
            List<Vector3d> temp_normals, float[] textureArray, float[] normalsArray)
        {
            int currentVertexPointer = int.Parse(vertexData[0]) - 1;
            temp_indices.Add(currentVertexPointer);
            Vector2d currentTex = temp_textures[int.Parse(vertexData[1]) - 1];
            textureArray[currentVertexPointer * 2] = (float)currentTex.X;
            textureArray[currentVertexPointer * 2 + 1] = 1 - (float) currentTex.Y;
            Vector3d currentNorm = temp_normals[int.Parse(vertexData[2]) - 1];
            normalsArray[currentVertexPointer * 3] = (float) currentNorm.X;
            normalsArray[currentVertexPointer * 3 + 1] = (float) currentNorm.Y;
            normalsArray[currentVertexPointer * 3 + 2] = (float) currentNorm.Z;
        }

    }
}
