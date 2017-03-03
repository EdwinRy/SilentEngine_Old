using Silent.Entities;
using Silent.Maths;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Silent.Tools
{
    public class ModelLoader
    {

        public static void LoadModel(string ModelPath, string TexturePath, out Silent_Model model, out Silent_Material material)
        {
            model = new Silent_Model();
            material = new Silent_Material();

            if (ModelPath.EndsWith(".dae")) LoadC(ModelPath, TexturePath, out model,out material);
            if (ModelPath.EndsWith(".obj")) LoadOBJ(ModelPath, TexturePath, out model, out material);

        }

        private static void LoadC(string ModelPath, string TexturePath, out Silent_Model model, out Silent_Material material)
        {
            GLModelLoader GLLoader = new GLModelLoader();

            XmlDocument modelDoc = new XmlDocument();
            modelDoc.Load(ModelPath);

            model = new Silent_Model();
            material = new Silent_Material();

            //Get the vertices of the model
            SetProperty(
                modelDoc.SelectSingleNode("COLLADA/library_geometries/geometry/mesh/source[@id='Cube-mesh-positions']/float_array").InnerText.Split(' '),
                out model.Vertices
                );

            //Get the normals of the model
            SetProperty(
                modelDoc.SelectSingleNode("COLLADA/library_geometries/geometry/mesh/source[@id='Cube-mesh-normals']/float_array").InnerText.Split(' '),

                out model.Normals
                );

            //Get the texture coordinates of the model
            SetProperty(
                modelDoc.SelectSingleNode("COLLADA/library_geometries/geometry/mesh/source[@id='Cube-mesh-map-0-array']/float_array").InnerText.Split(' '),
                out model.TextureCoords
                );

            //Get the indices

            SetProperty(
                modelDoc.SelectSingleNode("COLLADA/library_geometries/geometry/mesh/polylist/p").InnerText.Split(' '),
                out model.Indices
                );

            model.ModelVertex = GLLoader.Load(model.Vertices, model.Indices, model.TextureCoords, model.Normals);
            model.ModelTexture = GLLoader.LoadTexture(TexturePath);

        }

        private static void LoadOBJ(string ModelPath, string TexturePath, out Silent_Model model, out Silent_Material material)
        {
            GLModelLoader GLLoader = new GLModelLoader();
            StreamReader file = new StreamReader(ModelPath);
            material = new Silent_Material();
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


            ArrayOfVertices = new float[vertices.Count * 3];
            ArrayOfIndices = new int[indices.Count];

            int vertexPointer = 0;

            foreach (Vector3f vertex in vertices)
            {
                ArrayOfVertices[vertexPointer++] = vertex.X;
                ArrayOfVertices[vertexPointer++] = vertex.Y;
                ArrayOfVertices[vertexPointer++] = vertex.Z;
            }

            for (int i = 0; i < indices.Count; i++)
            {
                ArrayOfIndices[i] = indices[i];
            }

            model = new Silent_Model();
            model.Vertices = ArrayOfVertices;
            model.TextureCoords = ArrayOfTextures;
            model.Normals = ArrayOfNormals;
            model.Indices = ArrayOfIndices;

            model.ModelVertex = GLLoader.Load(model.Vertices, model.Indices, model.TextureCoords, model.Normals);
            model.ModelTexture = GLLoader.LoadTexture(TexturePath);

        }

        private static void ProcessFace(
            string[] vertex,
            List<int> indices,
            List<Vector2f> textures,
            List<Vector3f> normals,
            float[] textureArray,
            float[] normalArray)
        {

            int currentVertexPointer = int.Parse(vertex[0]) - 1;

            indices.Add(currentVertexPointer);

            Vector2f currentTex = textures[int.Parse(vertex[1]) - 1];
            textureArray[currentVertexPointer * 2] = currentTex.X;
            textureArray[currentVertexPointer * 2 + 1] = 1 - currentTex.Y;

            Vector3f currentNorm = normals[int.Parse(vertex[2]) - 1];
            normalArray[currentVertexPointer * 3] = currentNorm.X;
            normalArray[currentVertexPointer * 3 + 1] = currentNorm.Y;
            normalArray[currentVertexPointer * 3 + 2] = currentNorm.Z;

        }

        private static void SetProperty(string[] verticesStrArray, out float[] vertices)
        {
            vertices = new float[verticesStrArray.Length];

            for(int i = 0; i < verticesStrArray.Length; i++)
            {
                vertices[i] = float.Parse(verticesStrArray[i]);
            }

            vertices = null;
        }


        private static void SetProperty(string[] verticesStrArray, out int[] vertices)
        {
            vertices = new int[verticesStrArray.Length];

            for (int i = 0; i < verticesStrArray.Length; i++)
            {
                vertices[i] = int.Parse(verticesStrArray[i]);
            }

            vertices = null;
        }

    }
}
