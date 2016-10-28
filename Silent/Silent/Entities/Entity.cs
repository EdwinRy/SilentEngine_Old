using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Entities
{
    public class Entity
    {

        //Every entity has to have a model consisting of Vertex and Texture
        private Model m_model;


        //The constructor takes in the model
        public Entity(Model model)
        {
            m_model = model;
        }

        //The entity returns the model to enable rendering the model
        public Model getModel()
        {
            return m_model;
        }


    }
}
