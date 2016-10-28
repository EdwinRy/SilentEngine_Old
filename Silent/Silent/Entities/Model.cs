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
        private Vertex m_vertex;
        private Texture m_texture;

        //The constructor takes in Vertex and Texture
        public Model(Vertex vertex, Texture texture)
        {
            m_vertex = vertex;
            m_texture = texture;
        }

        //Return Vertex for rendering
        public Vertex getVertex()
        {
            return m_vertex;
        }

        //Return Texture for rendering
        public Texture getTexture()
        {
            return m_texture;
        }

    }
}
