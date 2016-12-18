using OpenTK;
using Silent.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Entities
{
    public class Light
    {
        public Vector3f position = new Vector3f();
        public Vector3f colour = new Vector3f(1,1,1);

        public void Translate(Vector3f translation)
        {
            position.X += translation.X;
            position.Y += translation.Y;
            position.Z += translation.Z;
        }

        public void Translate(float translationX, float translationY, float translationZ)
        {
            position.X += translationX;
            position.Y += translationY;
            position.Z += translationZ;
        }

        public void SetColour(Vector3f Colour)
        {
            colour = Colour;
        }

        public void SetColour(int ColourR, int ColourG, int ColourB)
        {
            colour.X = ColourR;
            colour.Y = ColourG;
            colour.Z = ColourB;
        }
    }
}
