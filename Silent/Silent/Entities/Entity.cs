using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Entities
{
    public class Entity
    {

        private string m_EntityName;

        //Every entity has to have a model consisting of Vertex and Texture
        private Model m_model;


        //The constructor takes in the model
        public Entity(Model model,string name = "SampleText")
        {
            m_model = model;
        }

        //The entity returns the model to enable rendering the model
        public Model getModel()
        {
            return m_model;
        }

        public string getEntityName()
        {
            return m_EntityName;
        }

        public string 

    }
}
