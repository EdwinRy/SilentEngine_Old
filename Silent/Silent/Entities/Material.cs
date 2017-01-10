using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Entities
{
    public class Silent_Material
    {
        //Defines how shiny the object is
        public float MaterialShiness = 10;

        //Defines how reflective the object is
        public float MaterialReflectivity = 0;

        //Defines the path to the texture the model is using
        public string TexturePath = "EngineAssets/SampleTexture.png";

    }
}
