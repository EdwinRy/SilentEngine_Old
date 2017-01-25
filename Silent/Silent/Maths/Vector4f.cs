using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Maths
{
    public struct Vector4f
    {
        //Represents offset from the origin in 3D space
        public float X;
        public float Y;
        public float Z;
        public float W;

        public Vector4f(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        //Overloaded operators
        public static Vector4f operator +(Vector4f v1, Vector4f v2)
        {
            return (new Vector4f(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W));
        }

        public static Vector4f operator -(Vector4f v1, Vector4f v2)
        {
            return (new Vector4f(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W + v2.W));
        }


    }
}
