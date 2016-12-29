using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Maths
{
    public struct Vector2f
    {
        //Represents offset from the origin in 3D space
        public float X;
        public float Y;

        public Vector2f(float x, float y)
        {
            X = x;
            Y = y;
        }

        //Overloaded operators
        public static Vector2f operator +(Vector2f v1, Vector2f v2)
        {
            return (new Vector2f(v1.X + v2.X, v1.Y + v2.Y));
        }

        public static Vector2f operator -(Vector2f v1, Vector2f v2)
        {
            return (new Vector2f(v1.X - v2.X, v1.Y - v2.Y));
        }
    }
}
