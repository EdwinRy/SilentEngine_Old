using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Maths
{
    public struct Vector3f
    {

        public float X;
        public float Y;
        public float Z;

        public Vector3f(float x = 0, float y = 0, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
