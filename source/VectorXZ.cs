
using System;
using System.Runtime.CompilerServices;

namespace Godot;

//
// Summary:
//     2-element structure that can be used to represent positions in 2D space or any
//     other pair of numeric values.
[Serializable]
public struct VectorXZ : IEquatable<VectorXZ>
{
    //
    // Summary:
    //     Enumerated index values for the axes. Returned by Godot.VectorXZ.MaxAxisIndex
    //     and Godot.VectorXZ.MinAxisIndex.
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
    public float X;

    //
    // Summary:
    //     The vector's Z component. Also accessible by using the index position [1].
    public float Z;

    private static readonly VectorXZ _zero = new VectorXZ(0f, 0f);

    private static readonly VectorXZ _one = new VectorXZ(1f, 1f);

    private static readonly VectorXZ _inf = new VectorXZ(float.PositiveInfinity, float.PositiveInfinity);

    private static readonly VectorXZ _forward = new VectorXZ(0f, -1f);

    private static readonly VectorXZ _back = new VectorXZ(0f, 1f);

    private static readonly VectorXZ _right = new VectorXZ(1f, 0f);

    private static readonly VectorXZ _left = new VectorXZ(-1f, 0f);

    //
    // Summary:
    //     Access vector components using their index.
    //
    // Value:
    //     [0] is equivalent to Godot.VectorXZ.X, [1] is equivalent to Godot.VectorXZ.Z.
    //
    // Exceptions:
    //   T:System.ArgumentOutOfRangeException:
    //     index is not 0 or 1.
    public float this[int index]
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
    //     Equivalent to new VectorXZ(0, 0).
    public static VectorXZ Zero => _zero;

    //
    // Summary:
    //     One vector, a vector with all components set to 1.
    //
    // Value:
    //     Equivalent to new VectorXZ(1, 1).
    public static VectorXZ One => _one;

    //
    // Summary:
    //     Infinity vector, a vector with all components set to Godot.Mathf.Inf.
    //
    // Value:
    //     Equivalent to new VectorXZ(Mathf.Inf, Mathf.Inf).
    public static VectorXZ Inf => _inf;

    //
    // Summary:
    //     Forward unit vector. Represents the direction of forward.
    //
    // Value:
    //     Equivalent to new VectorXZ(0, -1).
    public static VectorXZ Forward => _forward;

    //
    // Summary:
    //     Back unit vector. Represents the direction of back.
    //
    // Value:
    //     Equivalent to new VectorXZ(0, 1).
    public static VectorXZ Back => _back;

    //
    // Summary:
    //     Right unit vector. Represents the direction of right.
    //
    // Value:
    //     Equivalent to new VectorXZ(1, 0).
    public static VectorXZ Right => _right;

    //
    // Summary:
    //     Left unit vector. Represents the direction of left.
    //
    // Value:
    //     Equivalent to new VectorXZ(-1, 0).
    public static VectorXZ Left => _left;

    //
    // Summary:
    //     Helper method for deconstruction into a tuple.
    public readonly void Deconstruct(out float x, out float z)
    {
        x = X;
        z = Z;
    }

    internal void Normalize()
    {
        float num = LengthSquared();
        if (num == 0f)
        {
            X = (Z = 0f);
            return;
        }

        float num2 = Mathf.Sqrt(num);
        X /= num2;
        Z /= num2;
    }

    //
    // Summary:
    //     Returns a new vector with all components in absolute values (i.e. positive).
    //
    //
    // Returns:
    //     A vector with Godot.Mathf.Abs(System.Single) called on each component.
    public readonly VectorXZ Abs()
    {
        return new VectorXZ(Mathf.Abs(X), Mathf.Abs(Z));
    }

    //
    // Summary:
    //     Returns this vector's angle with respect to the X axis, or (1, 0) vector, in
    //     radians. Equivalent to the result of Godot.Mathf.Atan2(System.Single,System.Single)
    //     when called with the vector's Godot.VectorXZ.Z and Godot.VectorXZ.X as parameters:
    //     Mathf.Atan2(v.Z, v.X).
    //
    // Returns:
    //     The angle of this vector, in radians.
    public readonly float Angle()
    {
        return Mathf.Atan2(Z, X);
    }

    //
    // Summary:
    //     Returns the angle to the given vector, in radians.
    //
    // Parameters:
    //   to:
    //     The other vector to compare this vector to.
    //
    // Returns:
    //     The angle between the two vectors, in radians.
    public readonly float AngleTo(VectorXZ to)
    {
        return Mathf.Atan2(Cross(to), Dot(to));
    }

    //
    // Summary:
    //     Returns the angle between the line connecting the two points and the X axis,
    //     in radians.
    //
    // Parameters:
    //   to:
    //     The other vector to compare this vector to.
    //
    // Returns:
    //     The angle between the two vectors, in radians.
    public readonly float AngleToPoint(VectorXZ to)
    {
        return Mathf.Atan2(to.Z - Z, to.X - X);
    }

    //
    // Summary:
    //     Returns the aspect ratio of this vector, the ratio of Godot.VectorXZ.X to Godot.VectorXZ.Z.
    //
    //
    // Returns:
    //     The Godot.VectorXZ.X component divided by the Godot.VectorXZ.Z component.
    public readonly float Aspect()
    {
        return X / Z;
    }

    //
    // Summary:
    //     Returns the vector "bounced off" from a plane defined by the given normal.
    //
    // Parameters:
    //   normal:
    //     The normal vector defining the plane to bounce off. Must be normalized.
    //
    // Returns:
    //     The bounced vector.
    public readonly VectorXZ Bounce(VectorXZ normal)
    {
        return -Reflect(normal);
    }

    //
    // Summary:
    //     Returns a new vector with all components rounded up (towards positive infinity).
    //
    //
    // Returns:
    //     A vector with Godot.Mathf.Ceil(System.Single) called on each component.
    public readonly VectorXZ Ceil()
    {
        return new VectorXZ(Mathf.Ceil(X), Mathf.Ceil(Z));
    }

    //
    // Summary:
    //     Returns a new vector with all components clamped between the components of min
    //     and max using Godot.Mathf.Clamp(System.Single,System.Single,System.Single).
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
    public readonly VectorXZ Clamp(VectorXZ min, VectorXZ max)
    {
        return new VectorXZ(Mathf.Clamp(X, min.X, max.X), Mathf.Clamp(Z, min.Z, max.Z));
    }

    //
    // Summary:
    //     Returns the cross product of this vector and with.
    //
    // Parameters:
    //   with:
    //     The other vector.
    //
    // Returns:
    //     The cross product value.
    public readonly float Cross(VectorXZ with)
    {
        return X * with.Z - Z * with.X;
    }

    //
    // Summary:
    //     Performs a cubic interpolation between vectors preA, this vector, b, and postB,
    //     by the given amount weight.
    //
    // Parameters:
    //   b:
    //     The destination vector.
    //
    //   preA:
    //     A vector before this vector.
    //
    //   postB:
    //     A vector after b.
    //
    //   weight:
    //     A value on the range of 0.0 to 1.0, representing the amount of interpolation.
    //
    //
    // Returns:
    //     The interpolated vector.
    public readonly VectorXZ CubicInterpolate(VectorXZ b, VectorXZ preA, VectorXZ postB, float weight)
    {
        return new VectorXZ(Mathf.CubicInterpolate(X, b.X, preA.X, postB.X, weight), Mathf.CubicInterpolate(Z, b.Z, preA.Z, postB.Z, weight));
    }

    //
    // Summary:
    //     Performs a cubic interpolation between vectors preA, this vector, b, and postB,
    //     by the given amount weight. It can perform smoother interpolation than Godot.VectorXZ.CubicInterpolate(Godot.VectorXZ,Godot.VectorXZ,Godot.VectorXZ,System.Single)
    //     by the time values.
    //
    // Parameters:
    //   b:
    //     The destination vector.
    //
    //   preA:
    //     A vector before this vector.
    //
    //   postB:
    //     A vector after b.
    //
    //   weight:
    //     A value on the range of 0.0 to 1.0, representing the amount of interpolation.
    //
    //
    //   t:
    //
    //   preAT:
    //
    //   postBT:
    //
    // Returns:
    //     The interpolated vector.
    public readonly VectorXZ CubicInterpolateInTime(VectorXZ b, VectorXZ preA, VectorXZ postB, float weight, float t, float preAT, float postBT)
    {
        return new VectorXZ(Mathf.CubicInterpolateInTime(X, b.X, preA.X, postB.X, weight, t, preAT, postBT), Mathf.CubicInterpolateInTime(Z, b.Z, preA.Z, postB.Z, weight, t, preAT, postBT));
    }

    //
    // Summary:
    //     Returns the point at the given t on a one-dimensional Bezier curve defined by
    //     this vector and the given control1, control2, and end points.
    //
    // Parameters:
    //   control1:
    //     Control point that defines the bezier curve.
    //
    //   control2:
    //     Control point that defines the bezier curve.
    //
    //   end:
    //     The destination vector.
    //
    //   t:
    //     A value on the range of 0.0 to 1.0, representing the amount of interpolation.
    //
    //
    // Returns:
    //     The interpolated vector.
    public readonly VectorXZ BezierInterpolate(VectorXZ control1, VectorXZ control2, VectorXZ end, float t)
    {
        return new VectorXZ(Mathf.BezierInterpolate(X, control1.X, control2.X, end.X, t), Mathf.BezierInterpolate(Z, control1.Z, control2.Z, end.Z, t));
    }

    //
    // Summary:
    //     Returns the derivative at the given t on the Bezier curve defined by this vector
    //     and the given control1, control2, and end points.
    //
    // Parameters:
    //   control1:
    //     Control point that defines the bezier curve.
    //
    //   control2:
    //     Control point that defines the bezier curve.
    //
    //   end:
    //     The destination value for the interpolation.
    //
    //   t:
    //     A value on the range of 0.0 to 1.0, representing the amount of interpolation.
    //
    //
    // Returns:
    //     The resulting value of the interpolation.
    public readonly VectorXZ BezierDerivative(VectorXZ control1, VectorXZ control2, VectorXZ end, float t)
    {
        return new VectorXZ(Mathf.BezierDerivative(X, control1.X, control2.X, end.X, t), Mathf.BezierDerivative(Z, control1.Z, control2.Z, end.Z, t));
    }

    //
    // Summary:
    //     Returns the normalized vector pointing from this vector to to.
    //
    // Parameters:
    //   to:
    //     The other vector to point towards.
    //
    // Returns:
    //     The direction from this vector to to.
    public readonly VectorXZ DirectionTo(VectorXZ to)
    {
        return new VectorXZ(to.X - X, to.Z - Z).Normalized();
    }

    //
    // Summary:
    //     Returns the squared distance between this vector and to. This method runs faster
    //     than Godot.VectorXZ.DistanceTo(Godot.VectorXZ), so prefer it if you need to compare
    //     vectors or need the squared distance for some formula.
    //
    // Parameters:
    //   to:
    //     The other vector to use.
    //
    // Returns:
    //     The squared distance between the two vectors.
    public readonly float DistanceSquaredTo(VectorXZ to)
    {
        return (X - to.X) * (X - to.X) + (Z - to.Z) * (Z - to.Z);
    }

    //
    // Summary:
    //     Returns the distance between this vector and to.
    //
    // Parameters:
    //   to:
    //     The other vector to use.
    //
    // Returns:
    //     The distance between the two vectors.
    public readonly float DistanceTo(VectorXZ to)
    {
        return Mathf.Sqrt((X - to.X) * (X - to.X) + (Z - to.Z) * (Z - to.Z));
    }

    //
    // Summary:
    //     Returns the dot product of this vector and with.
    //
    // Parameters:
    //   with:
    //     The other vector to use.
    //
    // Returns:
    //     The dot product of the two vectors.
    public readonly float Dot(VectorXZ with)
    {
        return X * with.X + Z * with.Z;
    }

    //
    // Summary:
    //     Returns a new vector with all components rounded down (towards negative infinity).
    //
    //
    // Returns:
    //     A vector with Godot.Mathf.Floor(System.Single) called on each component.
    public readonly VectorXZ Floor()
    {
        return new VectorXZ(Mathf.Floor(X), Mathf.Floor(Z));
    }

    //
    // Summary:
    //     Returns the inverse of this vector. This is the same as new VectorXZ(1 / v.X,
    //     1 / v.Z).
    //
    // Returns:
    //     The inverse of this vector.
    public readonly VectorXZ Inverse()
    {
        return new VectorXZ(1f / X, 1f / Z);
    }

    //
    // Summary:
    //     Returns true if this vector is finite, by calling Godot.Mathf.IsFinite(System.Single)
    //     on each component.
    //
    // Returns:
    //     Whether this vector is finite or not.
    public readonly bool IsFinite()
    {
        if (Mathf.IsFinite(X))
        {
            return Mathf.IsFinite(Z);
        }

        return false;
    }

    //
    // Summary:
    //     Returns true if the vector is normalized, and false otherwise.
    //
    // Returns:
    //     A bool indicating whether or not the vector is normalized.
    public readonly bool IsNormalized()
    {
        return Mathf.Abs(LengthSquared() - 1f) < 1E-06f;
    }

    //
    // Summary:
    //     Returns the length (magnitude) of this vector.
    //
    // Returns:
    //     The length of this vector.
    public readonly float Length()
    {
        return Mathf.Sqrt(X * X + Z * Z);
    }

    //
    // Summary:
    //     Returns the squared length (squared magnitude) of this vector. This method runs
    //     faster than Godot.VectorXZ.Length, so prefer it if you need to compare vectors
    //     or need the squared length for some formula.
    //
    // Returns:
    //     The squared length of this vector.
    public readonly float LengthSquared()
    {
        return X * X + Z * Z;
    }

    //
    // Summary:
    //     Returns the result of the linear interpolation between this vector and to by
    //     amount weight.
    //
    // Parameters:
    //   to:
    //     The destination vector for interpolation.
    //
    //   weight:
    //     A value on the range of 0.0 to 1.0, representing the amount of interpolation.
    //
    //
    // Returns:
    //     The resulting vector of the interpolation.
    public readonly VectorXZ Lerp(VectorXZ to, float weight)
    {
        return new VectorXZ(Mathf.Lerp(X, to.X, weight), Mathf.Lerp(Z, to.Z, weight));
    }

    //
    // Summary:
    //     Returns the vector with a maximum length by limiting its length to length.
    //
    // Parameters:
    //   length:
    //     The length to limit to.
    //
    // Returns:
    //     The vector with its length limited.
    public readonly VectorXZ LimitLength(float length = 1f)
    {
        VectorXZ result = this;
        float num = Length();
        if (num > 0f && length < num)
        {
            result /= num;
            result *= length;
        }

        return result;
    }

    //
    // Summary:
    //     Returns the axis of the vector's highest value. See Godot.VectorXZ.Axis. If both
    //     components are equal, this method returns Godot.VectorXZ.Axis.X.
    //
    // Returns:
    //     The index of the highest axis.
    public readonly Axis MaxAxisIndex()
    {
        if (!(X < Z))
        {
            return Axis.X;
        }

        return Axis.Z;
    }

    //
    // Summary:
    //     Returns the axis of the vector's lowest value. See Godot.VectorXZ.Axis. If both
    //     components are equal, this method returns Godot.VectorXZ.Axis.Z.
    //
    // Returns:
    //     The index of the lowest axis.
    public readonly Axis MinAxisIndex()
    {
        if (!(X < Z))
        {
            return Axis.Z;
        }

        return Axis.X;
    }

    //
    // Summary:
    //     Moves this vector toward to by the fixed delta amount.
    //
    // Parameters:
    //   to:
    //     The vector to move towards.
    //
    //   delta:
    //     The amount to move towards by.
    //
    // Returns:
    //     The resulting vector.
    public readonly VectorXZ MoveToward(VectorXZ to, float delta)
    {
        VectorXZ vector = this;
        VectorXZ vector2 = to - vector;
        float num = vector2.Length();
        if (num <= delta || num < 1E-06f)
        {
            return to;
        }

        return vector + vector2 / num * delta;
    }

    //
    // Summary:
    //     Returns the vector scaled to unit length. Equivalent to v / v.Length().
    //
    // Returns:
    //     A normalized version of the vector.
    public readonly VectorXZ Normalized()
    {
        VectorXZ result = this;
        result.Normalize();
        return result;
    }

    //
    // Summary:
    //     Returns a vector composed of the Godot.Mathf.PosMod(System.Single,System.Single)
    //     of this vector's components and mod.
    //
    // Parameters:
    //   mod:
    //     A value representing the divisor of the operation.
    //
    // Returns:
    //     A vector with each component Godot.Mathf.PosMod(System.Single,System.Single)
    //     by mod.
    public readonly VectorXZ PosMod(float mod)
    {
        VectorXZ result = default(VectorXZ);
        result.X = Mathf.PosMod(X, mod);
        result.Z = Mathf.PosMod(Z, mod);
        return result;
    }

    //
    // Summary:
    //     Returns a vector composed of the Godot.Mathf.PosMod(System.Single,System.Single)
    //     of this vector's components and modv's components.
    //
    // Parameters:
    //   modv:
    //     A vector representing the divisors of the operation.
    //
    // Returns:
    //     A vector with each component Godot.Mathf.PosMod(System.Single,System.Single)
    //     by modv's components.
    public readonly VectorXZ PosMod(VectorXZ modv)
    {
        VectorXZ result = default(VectorXZ);
        result.X = Mathf.PosMod(X, modv.X);
        result.Z = Mathf.PosMod(Z, modv.Z);
        return result;
    }

    //
    // Summary:
    //     Returns this vector projected onto another vector onNormal.
    //
    // Parameters:
    //   onNormal:
    //     The vector to project onto.
    //
    // Returns:
    //     The projected vector.
    public readonly VectorXZ Project(VectorXZ onNormal)
    {
        return onNormal * (Dot(onNormal) / onNormal.LengthSquared());
    }

    //
    // Summary:
    //     Returns this vector reflected from a plane defined by the given normal.
    //
    // Parameters:
    //   normal:
    //     The normal vector defining the plane to reflect from. Must be normalized.
    //
    // Returns:
    //     The reflected vector.
    public readonly VectorXZ Reflect(VectorXZ normal)
    {
        return 2f * Dot(normal) * normal - this;
    }

    //
    // Summary:
    //     Rotates this vector by angle radians.
    //
    // Parameters:
    //   angle:
    //     The angle to rotate by, in radians.
    //
    // Returns:
    //     The rotated vector.
    public readonly VectorXZ Rotated(float angle)
    {
        var (num, num2) = Mathf.SinCos(angle);
        return new VectorXZ(X * num2 - Z * num, X * num + Z * num2);
    }

    //
    // Summary:
    //     Returns this vector with all components rounded to the nearest integer, with
    //     halfway cases rounded towards the nearest multiple of two.
    //
    // Returns:
    //     The rounded vector.
    public readonly VectorXZ Round()
    {
        return new VectorXZ(Mathf.Round(X), Mathf.Round(Z));
    }

    //
    // Summary:
    //     Returns a vector with each component set to one or negative one, depending on
    //     the signs of this vector's components, or zero if the component is zero, by calling
    //     Godot.Mathf.Sign(System.Single) on each component.
    //
    // Returns:
    //     A vector with all components as either 1, -1, or 0.
    public readonly VectorXZ Sign()
    {
        VectorXZ result = default(VectorXZ);
        result.X = Mathf.Sign(X);
        result.Z = Mathf.Sign(Z);
        return result;
    }

    //
    // Summary:
    //     Returns the result of the spherical linear interpolation between this vector
    //     and to by amount weight. This method also handles interpolating the lengths if
    //     the input vectors have different lengths. For the special case of one or both
    //     input vectors having zero length, this method behaves like Godot.VectorXZ.Lerp(Godot.VectorXZ,System.Single).
    //
    //
    // Parameters:
    //   to:
    //     The destination vector for interpolation.
    //
    //   weight:
    //     A value on the range of 0.0 to 1.0, representing the amount of interpolation.
    //
    //
    // Returns:
    //     The resulting vector of the interpolation.
    public readonly VectorXZ Slerp(VectorXZ to, float weight)
    {
        float num = LengthSquared();
        float num2 = to.LengthSquared();
        if ((double)num == 0.0 || (double)num2 == 0.0)
        {
            return Lerp(to, weight);
        }

        float num3 = Mathf.Sqrt(num);
        float num4 = Mathf.Lerp(num3, Mathf.Sqrt(num2), weight);
        float num5 = AngleTo(to);
        return Rotated(num5 * weight) * (num4 / num3);
    }

    //
    // Summary:
    //     Returns this vector slid along a plane defined by the given normal.
    //
    // Parameters:
    //   normal:
    //     The normal vector defining the plane to slide on.
    //
    // Returns:
    //     The slid vector.
    public readonly VectorXZ Slide(VectorXZ normal)
    {
        return this - normal * Dot(normal);
    }

    //
    // Summary:
    //     Returns this vector with each component snapped to the nearest multiple of step.
    //     This can also be used to round to an arbitrary number of decimals.
    //
    // Parameters:
    //   step:
    //     A vector value representing the step size to snap to.
    //
    // Returns:
    //     The snapped vector.
    public readonly VectorXZ Snapped(VectorXZ step)
    {
        return new VectorXZ(Mathf.Snapped(X, step.X), Mathf.Snapped(Z, step.Z));
    }

    //
    // Summary:
    //     Returns a perpendicular vector rotated 90 degrees counter-clockwise compared
    //     to the original, with the same length.
    //
    // Returns:
    //     The perpendicular vector.
    public readonly VectorXZ Orthogonal()
    {
        return new VectorXZ(Z, 0f - X);
    }

    //
    // Summary:
    //     Constructs a new Godot.VectorXZ with the given components.
    //
    // Parameters:
    //   x:
    //     The vector's X component.
    //
    //   z:
    //     The vector's Z component.
    public VectorXZ(float x, float z)
    {
        X = x;
        Z = z;
    }

    //
    // Summary:
    //     Creates a unit VectorXZ rotated to the given angle. This is equivalent to doing
    //     VectorXZ(Mathf.Cos(angle), Mathf.Sin(angle)) or VectorXZ.Right.Rotated(angle).
    //
    //
    // Parameters:
    //   angle:
    //     Angle of the vector, in radians.
    //
    // Returns:
    //     The resulting vector.
    public static VectorXZ FromAngle(float angle)
    {
        (float Sin, float Cos) tuple = Mathf.SinCos(angle);
        var (z, _) = tuple;
        return new VectorXZ(tuple.Cos, z);
    }

    //
    // Summary:
    //     Adds each component of the Godot.VectorXZ with the components of the given Godot.VectorXZ.
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
    public static VectorXZ operator +(VectorXZ left, VectorXZ right)
    {
        left.X += right.X;
        left.Z += right.Z;
        return left;
    }

    //
    // Summary:
    //     Subtracts each component of the Godot.VectorXZ by the components of the given
    //     Godot.VectorXZ.
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
    public static VectorXZ operator -(VectorXZ left, VectorXZ right)
    {
        left.X -= right.X;
        left.Z -= right.Z;
        return left;
    }

    //
    // Summary:
    //     Returns the negative value of the Godot.VectorXZ. This is the same as writing
    //     new VectorXZ(-v.X, -v.Z). This operation flips the direction of the vector while
    //     keeping the same magnitude. With floats, the number zero can be either positive
    //     or negative.
    //
    // Parameters:
    //   vec:
    //     The vector to negate/flip.
    //
    // Returns:
    //     The negated/flipped vector.
    public static VectorXZ operator -(VectorXZ vec)
    {
        vec.X = 0f - vec.X;
        vec.Z = 0f - vec.Z;
        return vec;
    }

    //
    // Summary:
    //     Multiplies each component of the Godot.VectorXZ by the given System.Single.
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
    public static VectorXZ operator *(VectorXZ vec, float scale)
    {
        vec.X *= scale;
        vec.Z *= scale;
        return vec;
    }

    //
    // Summary:
    //     Multiplies each component of the Godot.VectorXZ by the given System.Single.
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
    public static VectorXZ operator *(float scale, VectorXZ vec)
    {
        vec.X *= scale;
        vec.Z *= scale;
        return vec;
    }

    //
    // Summary:
    //     Multiplies each component of the Godot.VectorXZ by the components of the given
    //     Godot.VectorXZ.
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
    public static VectorXZ operator *(VectorXZ left, VectorXZ right)
    {
        left.X *= right.X;
        left.Z *= right.Z;
        return left;
    }

    //
    // Summary:
    //     Multiplies each component of the Godot.VectorXZ by the given System.Single.
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
    public static VectorXZ operator /(VectorXZ vec, float divisor)
    {
        vec.X /= divisor;
        vec.Z /= divisor;
        return vec;
    }

    //
    // Summary:
    //     Divides each component of the Godot.VectorXZ by the components of the given Godot.VectorXZ.
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
    public static VectorXZ operator /(VectorXZ vec, VectorXZ divisorv)
    {
        vec.X /= divisorv.X;
        vec.Z /= divisorv.Z;
        return vec;
    }

    //
    // Summary:
    //     Gets the remainder of each component of the Godot.VectorXZ with the components
    //     of the given System.Single. This operation uses truncated division, which is
    //     often not desired as it does not work well with negative numbers. Consider using
    //     Godot.VectorXZ.PosMod(System.Single) instead if you want to handle negative numbers.
    //
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
    public static VectorXZ operator %(VectorXZ vec, float divisor)
    {
        vec.X %= divisor;
        vec.Z %= divisor;
        return vec;
    }

    //
    // Summary:
    //     Gets the remainder of each component of the Godot.VectorXZ with the components
    //     of the given Godot.VectorXZ. This operation uses truncated division, which is
    //     often not desired as it does not work well with negative numbers. Consider using
    //     Godot.VectorXZ.PosMod(Godot.VectorXZ) instead if you want to handle negative numbers.
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
    //     The remainder vector.
    public static VectorXZ operator %(VectorXZ vec, VectorXZ divisorv)
    {
        vec.X %= divisorv.X;
        vec.Z %= divisorv.Z;
        return vec;
    }

    //
    // Summary:
    //     Returns true if the vectors are exactly equal. Note: Due to floating-point precision
    //     errors, consider using Godot.VectorXZ.IsEqualApprox(Godot.VectorXZ) instead, which
    //     is more reliable.
    //
    // Parameters:
    //   left:
    //     The left vector.
    //
    //   right:
    //     The right vector.
    //
    // Returns:
    //     Whether or not the vectors are exactly equal.
    public static bool operator ==(VectorXZ left, VectorXZ right)
    {
        return left.Equals(right);
    }

    //
    // Summary:
    //     Returns true if the vectors are not equal. Note: Due to floating-point precision
    //     errors, consider using Godot.VectorXZ.IsEqualApprox(Godot.VectorXZ) instead, which
    //     is more reliable.
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
    public static bool operator !=(VectorXZ left, VectorXZ right)
    {
        return !left.Equals(right);
    }

    //
    // Summary:
    //     Compares two Godot.VectorXZ vectors by first checking if the X value of the left
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
    public static bool operator <(VectorXZ left, VectorXZ right)
    {
        if (left.X == right.X)
        {
            return left.Z < right.Z;
        }

        return left.X < right.X;
    }

    //
    // Summary:
    //     Compares two Godot.VectorXZ vectors by first checking if the X value of the left
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
    public static bool operator >(VectorXZ left, VectorXZ right)
    {
        if (left.X == right.X)
        {
            return left.Z > right.Z;
        }

        return left.X > right.X;
    }

    //
    // Summary:
    //     Compares two Godot.VectorXZ vectors by first checking if the X value of the left
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
    public static bool operator <=(VectorXZ left, VectorXZ right)
    {
        if (left.X == right.X)
        {
            return left.Z <= right.Z;
        }

        return left.X < right.X;
    }

    //
    // Summary:
    //     Compares two Godot.VectorXZ vectors by first checking if the X value of the left
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
    public static bool operator >=(VectorXZ left, VectorXZ right)
    {
        if (left.X == right.X)
        {
            return left.Z >= right.Z;
        }

        return left.X > right.X;
    }

    //
    // Summary:
    //     Returns true if the vector is exactly equal to the given object (obj). Note:
    //     Due to floating-point precision errors, consider using Godot.VectorXZ.IsEqualApprox(Godot.VectorXZ)
    //     instead, which is more reliable.
    //
    // Parameters:
    //   obj:
    //     The object to compare with.
    //
    // Returns:
    //     Whether or not the vector and the object are equal.
    public override readonly bool Equals(object obj)
    {
        if (obj is VectorXZ other)
        {
            return Equals(other);
        }

        return false;
    }

    //
    // Summary:
    //     Returns true if the vectors are exactly equal. Note: Due to floating-point precision
    //     errors, consider using Godot.VectorXZ.IsEqualApprox(Godot.VectorXZ) instead, which
    //     is more reliable.
    //
    // Parameters:
    //   other:
    //     The other vector.
    //
    // Returns:
    //     Whether or not the vectors are exactly equal.
    public readonly bool Equals(VectorXZ other)
    {
        if (X == other.X)
        {
            return Z == other.Z;
        }

        return false;
    }

    //
    // Summary:
    //     Returns true if this vector and other are approximately equal, by running Godot.Mathf.IsEqualApprox(System.Single,System.Single)
    //     on each component.
    //
    // Parameters:
    //   other:
    //     The other vector to compare.
    //
    // Returns:
    //     Whether or not the vectors are approximately equal.
    public readonly bool IsEqualApprox(VectorXZ other)
    {
        if (Mathf.IsEqualApprox(X, other.X))
        {
            return Mathf.IsEqualApprox(Z, other.Z);
        }

        return false;
    }

    //
    // Summary:
    //     Returns true if this vector's values are approximately zero, by running Godot.Mathf.IsZeroApprox(System.Single)
    //     on each component. This method is faster than using Godot.VectorXZ.IsEqualApprox(Godot.VectorXZ)
    //     with one value as a zero vector.
    //
    // Returns:
    //     Whether or not the vector is approximately zero.
    public readonly bool IsZeroApprox()
    {
        if (Mathf.IsZeroApprox(X))
        {
            return Mathf.IsZeroApprox(Z);
        }

        return false;
    }

    //
    // Summary:
    //     Serves as the hash function for Godot.VectorXZ.
    //
    // Returns:
    //     A hash code for this vector.
    public override readonly int GetHashCode()
    {
        return Z.GetHashCode() ^ X.GetHashCode();
    }

    //
    // Summary:
    //     Converts this Godot.VectorXZ to a string.
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
    //     Converts this Godot.VectorXZ to a string with the given format.
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
