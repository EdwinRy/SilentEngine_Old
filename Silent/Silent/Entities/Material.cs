using Silent.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Entities
{
    public class Silent_Material
    {
        //Defines the path to the texture the model is using
        public string TexturePath;

        //vec3 Colour and value
        public Vector4f Emission;
        public Vector4f Ambient;
        public Vector4f Diffuse;
        public Vector4f Specular;

        public float Shiness;
        public float IndexOfRefraxion;
    }
}
