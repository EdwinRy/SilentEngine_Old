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
