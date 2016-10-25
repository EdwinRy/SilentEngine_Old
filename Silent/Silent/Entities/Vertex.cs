using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Entities
{
    public class Vertex
    {

        private int m_vaoID;
        private int m_vertexCount;
        private int m_vaoLength;

        public Vertex(int vaoID,int vertexCount, int vaoLength)
        {
            m_vaoID         = vaoID;
            m_vertexCount   = vertexCount;
            m_vaoLength     = vaoLength;
        }

        public int getVAOID()
        {
            return m_vaoID;
        }

        public int getVertexCount()
        {
            return m_vertexCount;
        }

        public int getVAOLength()
        {
            return m_vaoLength;
        }

    }
}
