using UnityEngine;

public static class MathUtils
{
    public const int AllLayers = ~0;

    public static Quaternion ShortestRotation(Quaternion a, Quaternion b)
    {
        if (Quaternion.Dot(a, b) < 0)

        {

            return a * Quaternion.Inverse(Multiply(b, -1));

        }
        else return a * Quaternion.Inverse(b);
    }

    public static Quaternion Multiply(Quaternion input, float scalar)
    {
        return new Quaternion(input.x * scalar, input.y * scalar, input.z * scalar, input.w * scalar);
    }

    public static void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp = lhs;
        lhs = rhs;
        rhs = temp;
    }

    public static bool Approximately(float a, float b, float tolerance = 1e-5f)
    {
        return Mathf.Abs(a - b) <= tolerance;
    }

    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static float Remap(this int value, int from1, int to1, int from2, int to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
