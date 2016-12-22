using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Maths
{
    public struct Vector3f
    {
        //Represents offset from the origin in 3D space
        public float X;
        public float Y;
        public float Z;

        public Vector3f(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        //Overloaded operators
        public static Vector3f operator +(Vector3f v1, Vector3f v2)
        {
            return (new Vector3f(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z));
        }

        public static Vector3f operator -(Vector3f v1, Vector3f v2)
        {
            return (new Vector3f(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z));
        }


    }
}
