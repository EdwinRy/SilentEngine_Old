using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Entities
{
    public class Model
    {

        //Every model has to consist of a vertex and a texture
        public Vertex ModelVertex;
        public Texture ModelTexture;

        public string ModelPath;

        public float[] Vertices;
        public float[] TextureCoords;
        public float[] Normals;
        public int[] Indices;

        //The constructor takes in Vertex and Texture
        public Model(string modelPath)
        {
            this.ModelPath = modelPath; 
        }

    }
}
