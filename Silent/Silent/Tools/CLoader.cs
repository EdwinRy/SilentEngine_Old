using Silent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Silent.Tools
{
    class ModelLoader
    {
        public static void LoadModel(string ModelPath, out Silent_Entity entity)
        {
            XmlDocument modelDoc = new XmlDocument();
            modelDoc.Load(ModelPath);
            entity = new Silent_Entity();

            entity.EntityMaterial = new Silent_Material()
            {
                //MaterialShiness = float.Parse(modelDoc.SelectSingleNode(@"COLLADA/library_effects/effect/profile_COMMON/technique/phong/emmision/color").InnerText)
                //MaterialShiness = float.Parse(modelDoc.GetElementById("shininess").InnerText),
                //MaterialReflectivity = float.Parse(modelDoc.GetElementById("index_of_refraction").InnerText)
            };

            entity.EntityModel = new Silent_Model()
            {
                
            };
            
        }
    }
}
