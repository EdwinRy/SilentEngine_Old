using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Entities
{
    public class Silent_Model
    {

        //Every model has to consist of a vertex and a texture
        public Silent_Vertex ModelVertex;
        public Silent_Texture ModelTexture;

        //Path to the model
        public string ModelPath;

        //List of all vertices of the model
        public float[] Vertices;
        //List of all texture coordinates of the model
        public float[] TextureCoords;
        //List of all normals of the model
        public float[] Normals;
        //List of all indices
        public int[] Indices;

        //The constructor takes in Vertex and Texture
        public Silent_Model(string modelPath = null)
        {
            //If the path to the model is already defined then don't change it
            if(modelPath!=null)
                this.ModelPath = modelPath; 
        }

        public static bool operator ==(Silent_Model m1, Silent_Model m2)
        {

            if (Object.ReferenceEquals(m1, null))
            {
                if (Object.ReferenceEquals(m2, null))
                {
                    return true;
                }

                return false;
            }

            return m1.Equals(m2);
        }

        public static bool operator !=(Silent_Model m1, Silent_Model m2)
        {
            return !(m1 == m2);
        }

    }
}
