using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class MatrixData
    {
        public float m00, m01, m02, m03;
        public float m10, m11, m12, m13;
        public float m20, m21, m22, m23;
        public float m30, m31, m32, m33;

        public static Matrix4x4 ToMatrix4x4(MatrixData data)
        {
            return new Matrix4x4(
                new (data.m00, data.m10, data.m20, data.m30),
                new (data.m01, data.m11, data.m21, data.m31),
                new (data.m02, data.m12, data.m22, data.m32),
                new (data.m03, data.m13, data.m23, data.m33)
            );
        }
        
        public static MatrixData FromMatrix4x4(Matrix4x4 matrix)
        {
            return new MatrixData
            {
                m00 = matrix.m00, m01 = matrix.m01, m02 = matrix.m02, m03 = matrix.m03,
                m10 = matrix.m10, m11 = matrix.m11, m12 = matrix.m12, m13 = matrix.m13,
                m20 = matrix.m20, m21 = matrix.m21, m22 = matrix.m22, m23 = matrix.m23,
                m30 = matrix.m30, m31 = matrix.m31, m32 = matrix.m32, m33 = matrix.m33
            };
        }
    }
}