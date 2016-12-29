using Silent.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Tools
{
    public static class SilentModelFile
    {


        public static void LoadModel(
            string filePath,
            out Material material,
            out Model model,
            out string texturePath
            )
        {
            string[] modelFile = File.ReadAllLines(filePath);

            float ModelShiness = float.Parse(modelFile[0]);
            float ModelReflectivity = float.Parse(modelFile[1]);

            string[] verts = modelFile[2].Split(',');
            float[] Vertices = new float[verts.Length];
                      
            for(int i = 0;i<verts.Length;i++)
            {
                Vertices[i] = float.Parse(verts[i]);
            }

            string[] tex = modelFile[3].Split(',');
            float[] Textures = new float[tex.Length];

            for (int i = 0; i < tex.Length; i++)
            {
                Textures[i] = float.Parse(tex[i]);
            }

            string[] norm = modelFile[4].Split(',');
            float[] Normals = new float[norm.Length];

            for (int i = 0; i < norm.Length; i++)
            {
                Normals[i] = float.Parse(norm[i]);
            }

            string[] ind = modelFile[5].Split(',');
            int[] Indices = new int[ind.Length];

            for (int i = 0; i < ind.Length; i++)
            {
                Indices[i] = int.Parse(ind[i]);
            }

            texturePath = modelFile[6];

            material = new Material();
            model = new Model(filePath);
            material.MaterialShiness = ModelShiness;
            material.MaterialReflectivity = ModelReflectivity;

            model.Vertices = Vertices;
            model.TextureCoords = Textures;
            model.Normals = Normals;
            model.Indices = Indices;

        }

        public static void SaveModel(
            string filePath,
            string fileName,
            string texturePath,
            float ModelShiness,
            float ModelReflectivity,
            float[] Vertices,
            float[] Textures,
            float[] Normals,
            int[] Indices
            )
        {
            string[] content = new string[6];

            content[0] = ModelShiness.ToString();
            content[1] = ModelReflectivity.ToString();
            content[2] = string.Join(",", Vertices);
            content[3] = string.Join(",", Textures);
            content[4] = string.Join(",", Normals);
            content[5] = string.Join(",", Indices);
            content[6] = string.Join(",", texturePath);

            File.WriteAllLines(filePath+fileName+".SilentModel", content);
        }
    }
}
