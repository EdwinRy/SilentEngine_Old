using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Entities
{
    public class Model
    {

        private Vertex m_vertex;
        private Texture m_texture;

        public Model(Vertex vertex, Texture texture)
        {
            m_vertex = vertex;
            m_texture = texture;
        }

        public Vertex getVertex()
        {
            return m_vertex;
        }

        public Texture getTexture()
        {
            return m_texture;
        }

    }
}
