using OpenTK;
using Silent.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Entities
{
    public class Silent_Light
    {
        //Position of the light in 3D space
        public Vector3f LightPosition = new Vector3f();

        //Colour of the light in RGB format
        public Vector3f LightColour = new Vector3f(1,1,1);

        //Translating light in 3D space
        public void Translate(Vector3f translation)
        {
            LightPosition = translation;
        }

        public void Translate(float translationX, float translationY, float translationZ)
        {
            LightPosition.X += translationX;
            LightPosition.Y += translationY;
            LightPosition.Z += translationZ;
        }

        //Change the colour of the light
        public void SetColour(Vector3f Colour)
        {
            LightColour = Colour;
        }

        public void SetColour(int ColourR, int ColourG, int ColourB)
        {
            LightColour.X = ColourR;
            LightColour.Y = ColourG;
            LightColour.Z = ColourB;
        }
    }
}
