using Silent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Silent.Tools
{
    public class ModelLoader
    {
        public static void LoadModel(string ModelPath, out Silent_Model model, out Silent_Material material)
        {
            XmlDocument modelDoc = new XmlDocument();
            modelDoc.Load(ModelPath);
            model = new Silent_Model();
            material = new Silent_Material();
            /*
            material = new Silent_Material()
            {
                //MaterialShiness = float.Parse(modelDoc.SelectSingleNode(@"COLLADA/library_effects/effect/profile_COMMON/technique/phong/emmision/color").InnerText)
                //MaterialShiness = float.Parse(modelDoc.GetElementById("shininess").InnerText),
                //MaterialReflectivity = float.Parse(modelDoc.GetElementById("index_of_refraction").InnerText)
            };  */

            //XmlNode node = doc.SelectSingleNode("root/DGFields/DGField[@text_id='Test.ChangeRank']");
            model = new Silent_Model();

            string[] vertArr = modelDoc.SelectSingleNode("COLLADA/library_geometries/geometry/mesh/source[@id='Cube-mesh-positions']/float_array").InnerText.Split(' ');
            
        }

        private void setVertices(string[] vertArr, out float[] verts)
        {
            for(int i = 0; i < vertArr.Length; i++)
            {

            }
        }
    }
}
