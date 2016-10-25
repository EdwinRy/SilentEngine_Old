using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Entities
{
    public class Texture
    {

        private int m_textureID;

        public Texture(int textureID)
        {
            m_textureID = textureID;
        }

        public int getTextureID()
        {
            return m_textureID;
        }
    }
}
