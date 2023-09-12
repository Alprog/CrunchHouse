
using System;
using System.Runtime.CompilerServices;

namespace Godot;

//
// Summary:
//     2D axis-aligned bounding box. RectXZ consists of a position, a size, and several
//     utility functions. It is typically used for fast overlap tests.
[Serializable]
public struct RectXZ : IEquatable<RectXZ>
{
    private VectorXZ _position;

    private VectorXZ _size;

    //
    // Summary:
    //     Beginning corner. Typically has values lower than Godot.RectXZ.End.
    //
    // Value:
    //     Directly uses a private field.
    public VectorXZ Position
    {
        readonly get
        {
            return _position;
        }
        set
        {
            _position = value;
        }
    }

    //
    // Summary:
    //     Size from Godot.RectXZ.Position to Godot.RectXZ.End. Typically all components are
    //     positive. If the size is negative, you can use Godot.RectXZ.Abs to fix it.
    //
    // Value:
    //     Directly uses a private field.
    public VectorXZ Size
    {
        readonly get
        {
            return _size;
        }
        set
        {
            _size = value;
        }
    }

    //
    // Summary:
    //     Ending corner. This is calculated as Godot.RectXZ.Position plus Godot.RectXZ.Size.
    //     Setting this value will change the size.
    //
    // Value:
    //     Getting is equivalent to value = Godot.RectXZ.Position + Godot.RectXZ.Size, setting
    //     is equivalent to Godot.RectXZ.Size = value - Godot.RectXZ.Position
    public VectorXZ End
    {
        readonly get
        {
            return _position + _size;
        }
        set
        {
            _size = value - _position;
        }
    }

    //
    // Summary:
    //     The area of this Godot.RectXZ. See also Godot.RectXZ.HasArea.
    public readonly float Area => _size.X * _size.Z;

    //
    // Summary:
    //     Returns a Godot.RectXZ with equivalent position and size, modified so that the
    //     top-left corner is the origin and width and height are positive.
    //
    // Returns:
    //     The modified Godot.RectXZ.
    public readonly RectXZ Abs()
    {
        VectorXZ end = End;
        return new RectXZ(new VectorXZ(Mathf.Min(_position.X, end.X), Mathf.Min(_position.Z, end.Z)), _size.Abs());
    }

    //
    // Summary:
    //     Returns the intersection of this Godot.RectXZ and b. If the rectangles do not
    //     intersect, an empty Godot.RectXZ is returned.
    //
    // Parameters:
    //   b:
    //     The other Godot.RectXZ.
    //
    // Returns:
    //     The intersection of this Godot.RectXZ and b, or an empty Godot.RectXZ if they do
    //     not intersect.
    public readonly RectXZ Intersection(RectXZ b)
    {
        RectXZ rect = b;
        if (!Intersects(rect))
        {
            return default(RectXZ);
        }

        rect._position.X = Mathf.Max(b._position.X, _position.X);
        rect._position.Z = Mathf.Max(b._position.Z, _position.Z);
        VectorXZ vector = b._position + b._size;
        VectorXZ vector2 = _position + _size;
        rect._size.X = Mathf.Min(vector.X, vector2.X) - rect._position.X;
        rect._size.Z = Mathf.Min(vector.Z, vector2.Z) - rect._position.Z;
        return rect;
    }

    //
    // Summary:
    //     Returns true if this Godot.RectXZ is finite, by calling Godot.Mathf.IsFinite(System.Single)
    //     on each component.
    //
    // Returns:
    //     Whether this vector is finite or not.
    public bool IsFinite()
    {
        if (_position.IsFinite())
        {
            return _size.IsFinite();
        }

        return false;
    }

    //
    // Summary:
    //     Returns true if this Godot.RectXZ completely encloses another one.
    //
    // Parameters:
    //   b:
    //     The other Godot.RectXZ that may be enclosed.
    //
    // Returns:
    //     A bool for whether or not this Godot.RectXZ encloses b.
    public readonly bool Encloses(RectXZ b)
    {
        if (b._position.X >= _position.X && b._position.Z >= _position.Z && b._position.X + b._size.X < _position.X + _size.X)
        {
            return b._position.Z + b._size.Z < _position.Z + _size.Z;
        }

        return false;
    }

    //
    // Summary:
    //     Returns this Godot.RectXZ expanded to include a given point.
    //
    // Parameters:
    //   to:
    //     The point to include.
    //
    // Returns:
    //     The expanded Godot.RectXZ.
    public readonly RectXZ Expand(VectorXZ to)
    {
        RectXZ result = this;
        VectorXZ position = result._position;
        VectorXZ vector = result._position + result._size;
        if (to.X < position.X)
        {
            position.X = to.X;
        }

        if (to.Z < position.Z)
        {
            position.Z = to.Z;
        }

        if (to.X > vector.X)
        {
            vector.X = to.X;
        }

        if (to.Z > vector.Z)
        {
            vector.Z = to.Z;
        }

        result._position = position;
        result._size = vector - position;
        return result;
    }

    //
    // Summary:
    //     Returns the center of the Godot.RectXZ, which is equal to Godot.RectXZ.Position
    //     + (Godot.RectXZ.Size / 2).
    //
    // Returns:
    //     The center.
    public readonly VectorXZ GetCenter()
    {
        return _position + _size * 0.5f;
    }

    //
    // Summary:
    //     Returns a copy of the Godot.RectXZ grown by the specified amount on all sides.
    //
    //
    // Parameters:
    //   by:
    //     The amount to grow by.
    //
    // Returns:
    //     The grown Godot.RectXZ.
    public readonly RectXZ Grow(float by)
    {
        RectXZ result = this;
        result._position.X -= by;
        result._position.Z -= by;
        result._size.X += by * 2f;
        result._size.Z += by * 2f;
        return result;
    }

    //
    // Summary:
    //     Returns a copy of the Godot.RectXZ grown by the specified amount on each side
    //     individually.
    //
    // Parameters:
    //   left:
    //     The amount to grow by on the left side.
    //
    //   top:
    //     The amount to grow by on the top side.
    //
    //   right:
    //     The amount to grow by on the right side.
    //
    //   bottom:
    //     The amount to grow by on the bottom side.
    //
    // Returns:
    //     The grown Godot.RectXZ.
    public readonly RectXZ GrowIndividual(float left, float top, float right, float bottom)
    {
        RectXZ result = this;
        result._position.X -= left;
        result._position.Z -= top;
        result._size.X += left + right;
        result._size.Z += top + bottom;
        return result;
    }

    //
    // Summary:
    //     Returns a copy of the Godot.RectXZ grown by the specified amount on the specified
    //     Godot.Side.
    //
    // Parameters:
    //   side:
    //     The side to grow.
    //
    //   by:
    //     The amount to grow by.
    //
    // Returns:
    //     The grown Godot.RectXZ.
    public readonly RectXZ GrowSide(Side side, float by)
    {
        RectXZ rect = this;
        return rect.GrowIndividual((side == Side.Left) ? by : 0f, (Side.Top == side) ? by : 0f, (Side.Right == side) ? by : 0f, (Side.Bottom == side) ? by : 0f);
    }

    //
    // Summary:
    //     Returns true if the Godot.RectXZ has area, and false if the Godot.RectXZ is linear,
    //     empty, or has a negative Godot.RectXZ.Size. See also Godot.RectXZ.Area.
    //
    // Returns:
    //     A bool for whether or not the Godot.RectXZ has area.
    public readonly bool HasArea()
    {
        if (_size.X > 0f)
        {
            return _size.Z > 0f;
        }

        return false;
    }

    //
    // Summary:
    //     Returns true if the Godot.RectXZ contains a point, or false otherwise.
    //
    // Parameters:
    //   point:
    //     The point to check.
    //
    // Returns:
    //     A bool for whether or not the Godot.RectXZ contains point.
    public readonly bool HasPoint(VectorXZ point)
    {
        if (point.X < _position.X)
        {
            return false;
        }

        if (point.Z < _position.Z)
        {
            return false;
        }

        if (point.X >= _position.X + _size.X)
        {
            return false;
        }

        if (point.Z >= _position.Z + _size.Z)
        {
            return false;
        }

        return true;
    }

    //
    // Summary:
    //     Returns true if the Godot.RectXZ overlaps with b (i.e. they have at least one
    //     point in common). If includeBorders is true, they will also be considered overlapping
    //     if their borders touch, even without intersection.
    //
    // Parameters:
    //   b:
    //     The other Godot.RectXZ to check for intersections with.
    //
    //   includeBorders:
    //     Whether or not to consider borders.
    //
    // Returns:
    //     A bool for whether or not they are intersecting.
    public readonly bool Intersects(RectXZ b, bool includeBorders = false)
    {
        if (includeBorders)
        {
            if (_position.X > b._position.X + b._size.X)
            {
                return false;
            }

            if (_position.X + _size.X < b._position.X)
            {
                return false;
            }

            if (_position.Z > b._position.Z + b._size.Z)
            {
                return false;
            }

            if (_position.Z + _size.Z < b._position.Z)
            {
                return false;
            }
        }
        else
        {
            if (_position.X >= b._position.X + b._size.X)
            {
                return false;
            }

            if (_position.X + _size.X <= b._position.X)
            {
                return false;
            }

            if (_position.Z >= b._position.Z + b._size.Z)
            {
                return false;
            }

            if (_position.Z + _size.Z <= b._position.Z)
            {
                return false;
            }
        }

        return true;
    }

    //
    // Summary:
    //     Returns a larger Godot.RectXZ that contains this Godot.RectXZ and b.
    //
    // Parameters:
    //   b:
    //     The other Godot.RectXZ.
    //
    // Returns:
    //     The merged Godot.RectXZ.
    public readonly RectXZ Merge(RectXZ b)
    {
        RectXZ result = default(RectXZ);
        result._position.X = Mathf.Min(b._position.X, _position.X);
        result._position.Z = Mathf.Min(b._position.Z, _position.Z);
        result._size.X = Mathf.Max(b._position.X + b._size.X, _position.X + _size.X);
        result._size.Z = Mathf.Max(b._position.Z + b._size.Z, _position.Z + _size.Z);
        result._size -= result._position;
        return result;
    }

    //
    // Summary:
    //     Constructs a Godot.RectXZ from a position and size.
    //
    // Parameters:
    //   position:
    //     The position.
    //
    //   size:
    //     The size.
    public RectXZ(VectorXZ position, VectorXZ size)
    {
        _position = position;
        _size = size;
    }

    //
    // Summary:
    //     Constructs a Godot.RectXZ from a position, width, and height.
    //
    // Parameters:
    //   position:
    //     The position.
    //
    //   width:
    //     The width.
    //
    //   height:
    //     The height.
    public RectXZ(VectorXZ position, float width, float height)
    {
        _position = position;
        _size = new VectorXZ(width, height);
    }

    //
    // Summary:
    //     Constructs a Godot.RectXZ from x, y, and size.
    //
    // Parameters:
    //   x:
    //     The position's X coordinate.
    //
    //   y:
    //     The position's Z coordinate.
    //
    //   size:
    //     The size.
    public RectXZ(float x, float y, VectorXZ size)
    {
        _position = new VectorXZ(x, y);
        _size = size;
    }

    //
    // Summary:
    //     Constructs a Godot.RectXZ from x, y, width, and height.
    //
    // Parameters:
    //   x:
    //     The position's X coordinate.
    //
    //   y:
    //     The position's Z coordinate.
    //
    //   width:
    //     The width.
    //
    //   height:
    //     The height.
    public RectXZ(float x, float y, float width, float height)
    {
        _position = new VectorXZ(x, y);
        _size = new VectorXZ(width, height);
    }

    //
    // Summary:
    //     Returns true if the Godot.Rect2s are exactly equal. Note: Due to floating-point
    //     precision errors, consider using Godot.RectXZ.IsEqualApprox(Godot.RectXZ) instead,
    //     which is more reliable.
    //
    // Parameters:
    //   left:
    //     The left rect.
    //
    //   right:
    //     The right rect.
    //
    // Returns:
    //     Whether or not the rects are exactly equal.
    public static bool operator ==(RectXZ left, RectXZ right)
    {
        return left.Equals(right);
    }

    //
    // Summary:
    //     Returns true if the Godot.Rect2s are not equal. Note: Due to floating-point precision
    //     errors, consider using Godot.RectXZ.IsEqualApprox(Godot.RectXZ) instead, which
    //     is more reliable.
    //
    // Parameters:
    //   left:
    //     The left rect.
    //
    //   right:
    //     The right rect.
    //
    // Returns:
    //     Whether or not the rects are not equal.
    public static bool operator !=(RectXZ left, RectXZ right)
    {
        return !left.Equals(right);
    }

    //
    // Summary:
    //     Returns true if this rect and obj are equal.
    //
    // Parameters:
    //   obj:
    //     The other object to compare.
    //
    // Returns:
    //     Whether or not the rect and the other object are exactly equal.
    public override readonly bool Equals(object obj)
    {
        if (obj is RectXZ other)
        {
            return Equals(other);
        }

        return false;
    }

    //
    // Summary:
    //     Returns true if this rect and other are equal.
    //
    // Parameters:
    //   other:
    //     The other rect to compare.
    //
    // Returns:
    //     Whether or not the rects are exactly equal.
    public readonly bool Equals(RectXZ other)
    {
        if (_position.Equals(other._position))
        {
            return _size.Equals(other._size);
        }

        return false;
    }

    //
    // Summary:
    //     Returns true if this rect and other are approximately equal, by running Godot.VectorXZ.IsEqualApprox(Godot.VectorXZ)
    //     on each component.
    //
    // Parameters:
    //   other:
    //     The other rect to compare.
    //
    // Returns:
    //     Whether or not the rects are approximately equal.
    public readonly bool IsEqualApprox(RectXZ other)
    {
        if (_position.IsEqualApprox(other._position))
        {
            return _size.IsEqualApprox(other.Size);
        }

        return false;
    }

    //
    // Summary:
    //     Serves as the hash function for Godot.RectXZ.
    //
    // Returns:
    //     A hash code for this rect.
    public override readonly int GetHashCode()
    {
        return _position.GetHashCode() ^ _size.GetHashCode();
    }

    //
    // Summary:
    //     Converts this Godot.RectXZ to a string.
    //
    // Returns:
    //     A string representation of this rect.
    public override readonly string ToString()
    {
        DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
        defaultInterpolatedStringHandler.AppendFormatted(_position);
        defaultInterpolatedStringHandler.AppendLiteral(", ");
        defaultInterpolatedStringHandler.AppendFormatted(_size);
        return defaultInterpolatedStringHandler.ToStringAndClear();
    }

    //
    // Summary:
    //     Converts this Godot.RectXZ to a string with the given format.
    //
    // Returns:
    //     A string representation of this rect.
    public readonly string ToString(string format)
    {
        return _position.ToString(format) + ", " + _size.ToString(format);
    }
}
