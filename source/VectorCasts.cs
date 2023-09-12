
using System.Numerics;

namespace Godot;

public static class VectorCasts
{
    public static Vector2 ToVector2(this Vector3 vector)
    {
        return new Vector2(vector.X, vector.Y);
    }

    public static Vector3 ToVector3(this Vector2 vector)
    {
        return new Vector3(vector.X, vector.Y, 0);
    }

    public static VectorXZ ToVectorXZ(this Vector3 vector)
    {
        return new VectorXZ(vector.X, vector.Z);
    }

    public static Vector3 ToVector3(this VectorXZ vector)
    {
        return new Vector3(vector.X, 0, vector.Z);
    }

    //---------------------------------------------------

    public static Vector2I ToVector2I(this Vector3I vector)
    {
        return new Vector2I(vector.X, vector.Y);
    }

    public static Vector3I ToVector3I(this Vector2I vector)
    {
        return new Vector3I(vector.X, vector.Y, 0);
    }

    public static VectorXZI ToVectorXZI(this Vector3I vector)
    {
        return new VectorXZI(vector.X, vector.Z);
    }

    public static Vector3I ToVector3I(this VectorXZI vector)
    {
        return new Vector3I(vector.X, 0, vector.Z);
    }

    //---------------------------------------------------

    public static Vector2 ToVector2(this Vector2I vector)
    {
        return new Vector2(vector.X, vector.Y);
    }

    public static VectorXZ ToVectorXZ(this VectorXZI vector)
    {
        return new VectorXZ(vector.X, vector.Z);
    }

    public static Vector3 ToVector3(this Vector3I vector)
    {
        return new Vector3(vector.X, vector.Y, vector.Z);
    }

    public static Vector2I ToVector2I(this Vector2 vector)
    {
        return new Vector2I((int)vector.X, (int)vector.Y);
    }

    public static VectorXZI ToVectorXZI(this VectorXZ vector)
    {
        return new VectorXZI((int)vector.X, (int)vector.Z);
    }

    public static Vector3I ToVector3I(this Vector3 vector)
    {
        return new Vector3I((int)vector.X, (int)vector.Y, (int)vector.Z);
    }
}