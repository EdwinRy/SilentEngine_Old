using OpenTK;
using Silent.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent.Tools
{
    public static class MatrixMaths
    {
        static Matrix4 matrix = Matrix4.Identity;

        public static Matrix4 CreateTransformationMatrix(Vector3f translation, float rotationX, float rotationY, float rotationZ, float scale)
        {
            matrix += Matrix4.CreateTranslation(translation.X, translation.Y, translation.Z);
            matrix += Matrix4.CreateRotationX((float)Math.PI / 180 * rotationX);
            matrix += Matrix4.CreateRotationY((float)Math.PI / 180 * rotationY);
            matrix += Matrix4.CreateRotationZ((float)Math.PI / 180 * rotationZ);
            matrix += Matrix4.CreateScale(scale);

            return matrix;
        }

        public static float ToRadiants(int value)
        {

            return (float)(Math.PI / 180) * value;
        }
        
    }
}
