using Silent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTING
{
    class Terrain  : Silent_Entity
    {
        public override void OnLoadEntity()
        {
            this.Translate(0, 0, 0);
            this.EntityModelPath = "Assets/Terrain.obj";
            this.EntityMaterial.TexturePath = "Assets/TerrainTexture.jpg";
        }

    }
}
