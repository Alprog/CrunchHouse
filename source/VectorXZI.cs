using System;
using System.Runtime.CompilerServices;

namespace Godot;

//
// Summary:
//     2-element structure that can be used to represent 2D grid coordinates or pairs
//     of integers.
[Serializable]
public struct VectorXZI : IEquatable<VectorXZI>
{
    //
    // Summary:
    //     Enumerated index values for the axes. Returned by Godot.VectorXZI.MaxAxisIndex
    //     and Godot.VectorXZI.MinAxisIndex.
    public enum Axis
    {
        //
        // Summary:
        //     The vector's X axis.
        X,
        //
        // Summary:
        //     The vector's Z axis.
        Z
    }

    //
    // Summary:
    //     The vector's X component. Also accessible by using the index position [0].
    public int X;

    //
    // Summary:
    //     The vector's Z component. Also accessible by using the index position [1].
    public int Z;

    private static readonly VectorXZI _zero = new VectorXZI(0, 0);

    private static readonly VectorXZI _one = new VectorXZI(1, 1);

    private static readonly VectorXZI _forward = new VectorXZI(0, -1);

    private static readonly VectorXZI _back = new VectorXZI(0, 1);

    private static readonly VectorXZI _right = new VectorXZI(1, 0);

    private static readonly VectorXZI _left = new VectorXZI(-1, 0);

    //
    // Summary:
    //     Access vector components using their index.
    //
    // Value:
    //     [0] is equivalent to Godot.VectorXZI.X, [1] is equivalent to Godot.VectorXZI.Z.
    //
    //
    // Exceptions:
    //   T:System.ArgumentOutOfRangeException:
    //     index is not 0 or 1.
    public int this[int index]
    {
        readonly get
        {
            return index switch
            {
                0 => X,
                1 => Z,
                _ => throw new ArgumentOutOfRangeException("index"),
            };
        }
        set
        {
            switch (index)
            {
                case 0:
                    X = value;
                    break;
                case 1:
                    Z = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("index");
            }
        }
    }

    //
    // Summary:
    //     Zero vector, a vector with all components set to 0.
    //
    // Value:
    //     Equivalent to new VectorXZI(0, 0).
    public static VectorXZI Zero => _zero;

    //
    // Summary:
    //     One vector, a vector with all components set to 1.
    //
    // Value:
    //     Equivalent to new VectorXZI(1, 1).
    public static VectorXZI One => _one;

    //
    // Summary:
    //     Forward unit vector. Represents the direction of forward.
    //
    // Value:
    //     Equivalent to new VectorXZI(0, -1).
    public static VectorXZI Forward => _forward;

    //
    // Summary:
    //     Back unit vector. Represents the direction of back.
    //
    // Value:
    //     Equivalent to new VectorXZI(0, 1).
    public static VectorXZI Back => _back;

    //
    // Summary:
    //     Right unit vector. Represents the direction of right.
    //
    // Value:
    //     Equivalent to new VectorXZI(1, 0).
    public static VectorXZI Right => _right;

    //
    // Summary:
    //     Left unit vector. Represents the direction of left.
    //
    // Value:
    //     Equivalent to new VectorXZI(-1, 0).
    public static VectorXZI Left => _left;

    //
    // Summary:
    //     Helper method for deconstruction into a tuple.
    public readonly void Deconstruct(out int x, out int z)
    {
        x = X;
        z = Z;
    }

    //
    // Summary:
    //     Returns a new vector with all components in absolute values (i.e. positive).
    //
    //
    // Returns:
    //     A vector with Godot.Mathf.Abs(System.Int32) called on each component.
    public readonly VectorXZI Abs()
    {
        return new VectorXZI(Mathf.Abs(X), Mathf.Abs(Z));
    }

    //
    // Summary:
    //     Returns the aspect ratio of this vector, the ratio of Godot.VectorXZI.X to Godot.VectorXZI.Z.
    //
    //
    // Returns:
    //     The Godot.VectorXZI.X component divided by the Godot.VectorXZI.Z component.
    public readonly float Aspect()
    {
        return (float)X / (float)Z;
    }

    //
    // Summary:
    //     Returns a new vector with all components clamped between the components of min
    //     and max using Godot.Mathf.Clamp(System.Int32,System.Int32,System.Int32).
    //
    // Parameters:
    //   min:
    //     The vector with minimum allowed values.
    //
    //   max:
    //     The vector with maximum allowed values.
    //
    // Returns:
    //     The vector with all components clamped.
    public readonly VectorXZI Clamp(VectorXZI min, VectorXZI max)
    {
        return new VectorXZI(Mathf.Clamp(X, min.X, max.X), Mathf.Clamp(Z, min.Z, max.Z));
    }

    //
    // Summary:
    //     Returns the length (magnitude) of this vector.
    //
    // Returns:
    //     The length of this vector.
    public readonly float Length()
    {
        int num = X * X;
        int num2 = Z * Z;
        return Mathf.Sqrt(num + num2);
    }

    //
    // Summary:
    //     Returns the squared length (squared magnitude) of this vector. This method runs
    //     faster than Godot.VectorXZI.Length, so prefer it if you need to compare vectors
    //     or need the squared length for some formula.
    //
    // Returns:
    //     The squared length of this vector.
    public readonly int LengthSquared()
    {
        int num = X * X;
        int num2 = Z * Z;
        return num + num2;
    }

    //
    // Summary:
    //     Returns the axis of the vector's highest value. See Godot.VectorXZI.Axis. If both
    //     components are equal, this method returns Godot.VectorXZI.Axis.X.
    //
    // Returns:
    //     The index of the highest axis.
    public readonly Axis MaxAxisIndex()
    {
        if (X >= Z)
        {
            return Axis.X;
        }

        return Axis.Z;
    }

    //
    // Summary:
    //     Returns the axis of the vector's lowest value. See Godot.VectorXZI.Axis. If both
    //     components are equal, this method returns Godot.VectorXZI.Axis.Z.
    //
    // Returns:
    //     The index of the lowest axis.
    public readonly Axis MinAxisIndex()
    {
        if (X >= Z)
        {
            return Axis.Z;
        }

        return Axis.X;
    }

    //
    // Summary:
    //     Returns a vector with each component set to one or negative one, depending on
    //     the signs of this vector's components, or zero if the component is zero, by calling
    //     Godot.Mathf.Sign(System.Int32) on each component.
    //
    // Returns:
    //     A vector with all components as either 1, -1, or 0.
    public readonly VectorXZI Sign()
    {
        VectorXZI result = this;
        result.X = Mathf.Sign(result.X);
        result.Z = Mathf.Sign(result.Z);
        return result;
    }

    //
    // Summary:
    //     Constructs a new Godot.VectorXZI with the given components.
    //
    // Parameters:
    //   x:
    //     The vector's X component.
    //
    //   z:
    //     The vector's Z component.
    public VectorXZI(int x, int z)
    {
        X = x;
        Z = z;
    }

    //
    // Summary:
    //     Adds each component of the Godot.VectorXZI with the components of the given Godot.VectorXZI.
    //
    //
    // Parameters:
    //   left:
    //     The left vector.
    //
    //   right:
    //     The right vector.
    //
    // Returns:
    //     The added vector.
    public static VectorXZI operator +(VectorXZI left, VectorXZI right)
    {
        left.X += right.X;
        left.Z += right.Z;
        return left;
    }

    //
    // Summary:
    //     Subtracts each component of the Godot.VectorXZI by the components of the given
    //     Godot.VectorXZI.
    //
    // Parameters:
    //   left:
    //     The left vector.
    //
    //   right:
    //     The right vector.
    //
    // Returns:
    //     The subtracted vector.
    public static VectorXZI operator -(VectorXZI left, VectorXZI right)
    {
        left.X -= right.X;
        left.Z -= right.Z;
        return left;
    }

    //
    // Summary:
    //     Returns the negative value of the Godot.VectorXZI. This is the same as writing
    //     new VectorXZI(-v.X, -v.Z). This operation flips the direction of the vector while
    //     keeping the same magnitude.
    //
    // Parameters:
    //   vec:
    //     The vector to negate/flip.
    //
    // Returns:
    //     The negated/flipped vector.
    public static VectorXZI operator -(VectorXZI vec)
    {
        vec.X = -vec.X;
        vec.Z = -vec.Z;
        return vec;
    }

    //
    // Summary:
    //     Multiplies each component of the Godot.VectorXZI by the given int.
    //
    // Parameters:
    //   vec:
    //     The vector to multiply.
    //
    //   scale:
    //     The scale to multiply by.
    //
    // Returns:
    //     The multiplied vector.
    public static VectorXZI operator *(VectorXZI vec, int scale)
    {
        vec.X *= scale;
        vec.Z *= scale;
        return vec;
    }

    //
    // Summary:
    //     Multiplies each component of the Godot.VectorXZI by the given int.
    //
    // Parameters:
    //   scale:
    //     The scale to multiply by.
    //
    //   vec:
    //     The vector to multiply.
    //
    // Returns:
    //     The multiplied vector.
    public static VectorXZI operator *(int scale, VectorXZI vec)
    {
        vec.X *= scale;
        vec.Z *= scale;
        return vec;
    }

    //
    // Summary:
    //     Multiplies each component of the Godot.VectorXZI by the components of the given
    //     Godot.VectorXZI.
    //
    // Parameters:
    //   left:
    //     The left vector.
    //
    //   right:
    //     The right vector.
    //
    // Returns:
    //     The multiplied vector.
    public static VectorXZI operator *(VectorXZI left, VectorXZI right)
    {
        left.X *= right.X;
        left.Z *= right.Z;
        return left;
    }

    //
    // Summary:
    //     Divides each component of the Godot.VectorXZI by the given int.
    //
    // Parameters:
    //   vec:
    //     The dividend vector.
    //
    //   divisor:
    //     The divisor value.
    //
    // Returns:
    //     The divided vector.
    public static VectorXZI operator /(VectorXZI vec, int divisor)
    {
        vec.X /= divisor;
        vec.Z /= divisor;
        return vec;
    }

    //
    // Summary:
    //     Divides each component of the Godot.VectorXZI by the components of the given Godot.VectorXZI.
    //
    //
    // Parameters:
    //   vec:
    //     The dividend vector.
    //
    //   divisorv:
    //     The divisor vector.
    //
    // Returns:
    //     The divided vector.
    public static VectorXZI operator /(VectorXZI vec, VectorXZI divisorv)
    {
        vec.X /= divisorv.X;
        vec.Z /= divisorv.Z;
        return vec;
    }

    //
    // Summary:
    //     Gets the remainder of each component of the Godot.VectorXZI with the components
    //     of the given int. This operation uses truncated division, which is often not
    //     desired as it does not work well with negative numbers. Consider using Godot.Mathf.PosMod(System.Int32,System.Int32)
    //     instead if you want to handle negative numbers.
    //
    // Parameters:
    //   vec:
    //     The dividend vector.
    //
    //   divisor:
    //     The divisor value.
    //
    // Returns:
    //     The remainder vector.
    public static VectorXZI operator %(VectorXZI vec, int divisor)
    {
        vec.X %= divisor;
        vec.Z %= divisor;
        return vec;
    }

    //
    // Summary:
    //     Gets the remainder of each component of the Godot.VectorXZI with the components
    //     of the given Godot.VectorXZI. This operation uses truncated division, which is
    //     often not desired as it does not work well with negative numbers. Consider using
    //     Godot.Mathf.PosMod(System.Int32,System.Int32) instead if you want to handle negative
    //     numbers.
    //
    // Parameters:
    //   vec:
    //     The dividend vector.
    //
    //   divisorv:
    //     The divisor vector.
    //
    // Returns:
    //     The remainder vector.
    public static VectorXZI operator %(VectorXZI vec, VectorXZI divisorv)
    {
        vec.X %= divisorv.X;
        vec.Z %= divisorv.Z;
        return vec;
    }

    //
    // Summary:
    //     Returns true if the vectors are equal.
    //
    // Parameters:
    //   left:
    //     The left vector.
    //
    //   right:
    //     The right vector.
    //
    // Returns:
    //     Whether or not the vectors are equal.
    public static bool operator ==(VectorXZI left, VectorXZI right)
    {
        return left.Equals(right);
    }

    //
    // Summary:
    //     Returns true if the vectors are not equal.
    //
    // Parameters:
    //   left:
    //     The left vector.
    //
    //   right:
    //     The right vector.
    //
    // Returns:
    //     Whether or not the vectors are not equal.
    public static bool operator !=(VectorXZI left, VectorXZI right)
    {
        return !left.Equals(right);
    }

    //
    // Summary:
    //     Compares two Godot.VectorXZI vectors by first checking if the X value of the left
    //     vector is less than the X value of the right vector. If the X values are exactly
    //     equal, then it repeats this check with the Z values of the two vectors. This
    //     operator is useful for sorting vectors.
    //
    // Parameters:
    //   left:
    //     The left vector.
    //
    //   right:
    //     The right vector.
    //
    // Returns:
    //     Whether or not the left is less than the right.
    public static bool operator <(VectorXZI left, VectorXZI right)
    {
        if (left.X == right.X)
        {
            return left.Z < right.Z;
        }

        return left.X < right.X;
    }

    //
    // Summary:
    //     Compares two Godot.VectorXZI vectors by first checking if the X value of the left
    //     vector is greater than the X value of the right vector. If the X values are exactly
    //     equal, then it repeats this check with the Z values of the two vectors. This
    //     operator is useful for sorting vectors.
    //
    // Parameters:
    //   left:
    //     The left vector.
    //
    //   right:
    //     The right vector.
    //
    // Returns:
    //     Whether or not the left is greater than the right.
    public static bool operator >(VectorXZI left, VectorXZI right)
    {
        if (left.X == right.X)
        {
            return left.Z > right.Z;
        }

        return left.X > right.X;
    }

    //
    // Summary:
    //     Compares two Godot.VectorXZI vectors by first checking if the X value of the left
    //     vector is less than or equal to the X value of the right vector. If the X values
    //     are exactly equal, then it repeats this check with the Z values of the two vectors.
    //     This operator is useful for sorting vectors.
    //
    // Parameters:
    //   left:
    //     The left vector.
    //
    //   right:
    //     The right vector.
    //
    // Returns:
    //     Whether or not the left is less than or equal to the right.
    public static bool operator <=(VectorXZI left, VectorXZI right)
    {
        if (left.X == right.X)
        {
            return left.Z <= right.Z;
        }

        return left.X < right.X;
    }

    //
    // Summary:
    //     Compares two Godot.VectorXZI vectors by first checking if the X value of the left
    //     vector is greater than or equal to the X value of the right vector. If the X
    //     values are exactly equal, then it repeats this check with the Z values of the
    //     two vectors. This operator is useful for sorting vectors.
    //
    // Parameters:
    //   left:
    //     The left vector.
    //
    //   right:
    //     The right vector.
    //
    // Returns:
    //     Whether or not the left is greater than or equal to the right.
    public static bool operator >=(VectorXZI left, VectorXZI right)
    {
        if (left.X == right.X)
        {
            return left.Z >= right.Z;
        }

        return left.X > right.X;
    }

    //
    // Summary:
    //     Converts this Godot.VectorXZI to a Godot.Vector2.
    //
    // Parameters:
    //   value:
    //     The vector to convert.
    public static implicit operator Vector2(VectorXZI value)
    {
        return new Vector2(value.X, value.Z);
    }

    //
    // Summary:
    //     Converts a Godot.Vector2 to a Godot.VectorXZI by truncating components' fractional
    //     parts (rounding towards zero). For a different behavior consider passing the
    //     result of Godot.Vector2.Ceil, Godot.Vector2.Floor or Godot.Vector2.Round to this
    //     conversion operator instead.
    //
    // Parameters:
    //   value:
    //     The vector to convert.
    public static explicit operator VectorXZI(VectorXZ value)
    {
        return new VectorXZI((int)value.X, (int)value.Z);
    }

    //
    // Summary:
    //     Returns true if the vector is equal to the given object (obj).
    //
    // Parameters:
    //   obj:
    //     The object to compare with.
    //
    // Returns:
    //     Whether or not the vector and the object are equal.
    public override readonly bool Equals(object obj)
    {
        if (obj is VectorXZI other)
        {
            return Equals(other);
        }

        return false;
    }

    //
    // Summary:
    //     Returns true if the vectors are equal.
    //
    // Parameters:
    //   other:
    //     The other vector.
    //
    // Returns:
    //     Whether or not the vectors are equal.
    public readonly bool Equals(VectorXZI other)
    {
        if (X == other.X)
        {
            return Z == other.Z;
        }

        return false;
    }

    //
    // Summary:
    //     Serves as the hash function for Godot.VectorXZI.
    //
    // Returns:
    //     A hash code for this vector.
    public override readonly int GetHashCode()
    {
        return Z.GetHashCode() ^ X.GetHashCode();
    }

    //
    // Summary:
    //     Converts this Godot.VectorXZI to a string.
    //
    // Returns:
    //     A string representation of this vector.
    public override readonly string ToString()
    {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 2);
        defaultInterpolatedStringHandler.AppendLiteral("(");
        defaultInterpolatedStringHandler.AppendFormatted(X);
        defaultInterpolatedStringHandler.AppendLiteral(", ");
        defaultInterpolatedStringHandler.AppendFormatted(Z);
        defaultInterpolatedStringHandler.AppendLiteral(")");
        return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    //
    // Summary:
    //     Converts this Godot.VectorXZI to a string with the given format.
    //
    // Returns:
    //     A string representation of this vector.
    public readonly string ToString(string format)
    {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 2);
        defaultInterpolatedStringHandler.AppendLiteral("(");
        defaultInterpolatedStringHandler.AppendFormatted(X.ToString(format));
        defaultInterpolatedStringHandler.AppendLiteral(", ");
        defaultInterpolatedStringHandler.AppendFormatted(Z.ToString(format));
        defaultInterpolatedStringHandler.AppendLiteral(")");
        return defaultInterpolatedStringHandler.ToStringAndClear();
    }
}
