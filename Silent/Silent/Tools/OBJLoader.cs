using Silent.Entities;
using Silent.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;


namespace Silent.Tools
{
    public class OBJLoader
    {

        public Model loadObjModel(string filePath = "EngineAssets/SampleCube.obj", string texturePath = "EngineAssets/SampleTexture.png")
        {

            string[] lines = System.IO.File.ReadAllLines(filePath);
            float[] vertices = null;
            float[] textures = null;
            float[] normals = null;
            int[] indices = null;

            

            List<Vector3d> temp_vertices = new List<Vector3d>();
            List<Vector2d> temp_textures = new List<Vector2d>();
            List<Vector3d> temp_normals = new List<Vector3d>();
            List<int> temp_indices = new List<int>();

            bool texturesNormals_initialized = false;

            GLModelLoader loader = new GLModelLoader();

            

            foreach (string line in lines)
            {
                //Vertices
                if (line.StartsWith("v "))
                {
                    string[] currentLine = line.Split(' ');
                    //Console.WriteLine("Split line:", currentLine[1]);

                    foreach(string element in currentLine)
                    {
                        //Console.WriteLine(element);
                    }

                    temp_vertices.Add(
                        new Vector3d(
                            float.Parse(currentLine[1]), 
                            double.Parse(currentLine[2]),
                            float.Parse(currentLine[3])
                            ));

                }

                //Textures
                if (line.StartsWith("vt "))
                {
                    string[] currentLine = line.Split(' ');
                    foreach (string element in currentLine)
                    {
                        //Console.WriteLine(element);
                    }
                    temp_textures.Add(
                        new Vector2d(
                            float.Parse(currentLine[1]),
                            float.Parse(currentLine[2])));
                }

                //Normals
                if (line.StartsWith("vn "))
                {

                    string[] currentLine = line.Split(' ');
                    foreach (string element in currentLine)
                    {
                        //Console.WriteLine(element);
                    }
                    temp_normals.Add(
                        new Vector3d(
                            float.Parse(currentLine[1]),
                            float.Parse(currentLine[2]),
                            float.Parse(currentLine[3])));

                }

                //Triangles
                if (line.StartsWith("f "))
                {
                    if (!texturesNormals_initialized)
                    {
                        textures = new float[temp_vertices.Count * 2];
                        normals = new float[temp_vertices.Count * 3];

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

            float[] randVert = {-0.5f, 0.5f, 0f,-0.5f, -0.5f, 0f,0.5f, -0.5f, 0f,0.5f, 0.5f, 0f };
            int[] randIndices = { 0, 1, 3, 3, 1, 2 };
            float[] texCoord = {
                0,0,
                0,1,
                1,1,
                1,0
            };

            //vertex = loader.load(vertices, indices, textures, normals);
            vertex = loader.load(randVert, randIndices, texCoord, normals);
            texture = loader.loadTexture(texturePath);

            return new Model(vertex, texture);


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
