using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Entities
{
    public class Vertex
    {

        //ID of the VAO used for rendering
        private int m_vaoID;
                                                                                                             
        //The number of vertices needed for rendering
        private int m_vertexCount;

        //The number of elements in the VAO simplifying enabling/disabling elements in rendering
        private int m_vaoLength;

        public Vertex(int vaoID,int vertexCount, int vaoLength)                
        {
            m_vaoID         = vaoID;
            m_vertexCount   = vertexCount;
            m_vaoLength     = vaoLength;
        }

        //Return VAO ID for rendering
        public int GetVAOID()
        {
            return m_vaoID;
        }

        //Return the number of vertices for rendering
        public int GetVertexCount()
        {
            return m_vertexCount;
        }

        //Return the number of VAO attributes
        public int GetVAOLength()
        {
            return m_vaoLength;
        }

    }
}
