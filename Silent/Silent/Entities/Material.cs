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
        //vec3 Colour and value
        public Vector4f Emission = new Vector4f();
        public Vector4f Ambient = new Vector4f();
        public Vector4f Diffuse = new Vector4f();
        public Vector4f Specular = new Vector4f();

        public float Shiness = 0;
        public float IndexOfRefraxion = 0;
    }
}
