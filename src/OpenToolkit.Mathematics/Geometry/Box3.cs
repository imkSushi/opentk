//
// Box3.cs
//
// Copyright (C) 2019 OpenTK
//
// This software may be modified and distributed under the terms
// of the MIT license. See the LICENSE file for details.
//

using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace OpenToolkit.Mathematics
{
    /// <summary>
    /// Defines a 3d box.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Box3 : IEquatable<Box3>
    {
        /// <summary>
        /// The left boundary of the structure.
        /// </summary>
        public float Left;

        /// <summary>
        /// The right boundary of the structure
        /// </summary>
        public float Right;

        /// <summary>
        /// The top boundary of the structure
        /// </summary>
        public float Top;

        /// <summary>
        /// The bottom boundary of the structure
        /// </summary>
        public float Bottom;

        /// <summary>
        /// The back boundary of the structure
        /// </summary>
        public float Back;

        /// <summary>
        /// The front boundary of the structure
        /// </summary>
        public float Front;

        /// <summary>
        /// Creates a new instance of <see cref="Box3"/> struct.
        /// </summary>
        /// <param name="left">The position of the left boundary.</param>
        /// <param name="top">The position of the top boundary.</param>
        /// <param name="front">The position of the front boundary.</param>
        /// <param name="right">The position of the right boundary.</param>
        /// <param name="bottom">The position of the bottom boundary.</param>
        /// <param name="back">The position of the back boundary.</param>
        public Box3(float left, float top, float front, float right, float bottom, float back)
        {
            Left = left;
            Top = top;
            Front = front;
            Right = right;
            Bottom = bottom;
            Back = back;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Box3"/> struct.
        /// </summary>
        /// <param name="frontTopLeft">An OpenToolkit.Vector3 describing the front-top-left corner of the Box3.</param>
        /// <param name="backBottomRight">An OpenToolkit.Vector3 describing the back-bottom-right corner of the Box3.</param>
        public Box3(Vector3 frontTopLeft, Vector3 backBottomRight)
        {
            Left = frontTopLeft.X;
            Top = frontTopLeft.Y;
            Front = frontTopLeft.Z;
            Right = backBottomRight.X;
            Bottom = backBottomRight.Y;
            Back = backBottomRight.Z;
        }

        /// <summary>
        /// Creates a new Box3 with the specified dimensions.
        /// </summary>
        /// <param name="left">The position of the left boundary.</param>
        /// <param name="top">The position of the top boundary.</param>
        /// <param name="front">The position of the front boundary.</param>
        /// <param name="width">The width of the box.</param>
        /// <param name="height">The height of the box.</param>
        /// <param name="depth">The depth of the box.</param>
        /// <returns>A new OpenToolkit.Box3 with the specified dimensions.</returns>
        public static Box3 FromDimensions(float left, float top, float front, float width, float height, float depth)
        {
            return new Box3(left, top, front, left + width, top + height, front + depth);
        }

        /// <summary>
        /// Creates a new box3 with the specified dimensions.
        /// </summary>
        /// <param name="position">The position of the front top left corner.</param>
        /// <param name="size">The size of the box.</param>
        /// <returns>A new OpenToolkit.Box3 with the specified dimensions</returns>
        public static Box3 FromDimensions(Vector3 position, Vector3 size)
        {
            return FromDimensions(position.X, position.Y, position.Z, size.X, size.Y, size.Z);
        }

        /// <summary>
        /// Gets a float describing the width of the Box3 structure.
        /// </summary>
        public float Width => Math.Abs(Right - Left);

        /// <summary>
        /// Gets a float describing the height of the Box3 structure.
        /// </summary>
        public float Height => Math.Abs(Bottom - Top);

        /// <summary>
        /// Gets a float describing the depth of the box3 structure.
        /// </summary>
        public float Depth => Math.Abs(Back - Front);
        
        /// <summary>
        /// Returns whether the box contains the specified point.
        /// </summary>
        /// <param name="point">The point to query.</param>
        /// <param name="closedRegion">Whether to include the box boundary in the test region.</param>
        /// <returns>Whether this box contains the point.</returns>
        public bool Contains(Vector3 point, bool closedRegion = true)
        {
            var containsX = closedRegion == Left <= Right
                ? point.X >= Left != point.X > Right
                : point.X > Left != point.X >= Right;

            var containsY = closedRegion == Top <= Bottom
                ? point.Y >= Top != point.Y > Bottom
                : point.Y > Top != point.Y >= Bottom;

            var containsZ = closedRegion == Front <= Back
                ? point.Z >= Front != point.Z > Back
                : point.Z > Front != point.Z >= Back;

            return containsX && containsY && containsZ;
        }

        /// <summary>
        /// Returns a Box3 translated by the given amount.
        /// </summary>
        /// <param name="point">The distance to translate the box.</param>
        /// <returns>The translated box.</returns>
        public Box3 Translated(Vector3 point)
        {
            return new Box3(Left + point.X, Top + point.Y,Front + point.Z , Right + point.X, Bottom + point.Y, Front + point.Z);
        }

        /// <summary>
        /// Translates this Box3 by the given amount.
        /// </summary>
        /// <param name="point">The distance to translate the box.</param>
        public void Translate(Vector3 point)
        {
            Left += point.X;
            Right += point.X;
            Top += point.Y;
            Bottom += point.Y;
            Front += point.Z;
            Back += point.Z;
        }

        /// <summary>
        /// Equality comparator.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        public static bool operator ==(Box3 left, Box3 right)
        {
            return MathHelper.ApproximatelyEqualEpsilon(left.Bottom, right.Bottom, 0.0001f)
                   && MathHelper.ApproximatelyEqualEpsilon(left.Top, right.Top, 0.0001f)
                   && MathHelper.ApproximatelyEqualEpsilon(left.Left, right.Left, 0.0001f)
                   && MathHelper.ApproximatelyEqualEpsilon(left.Right, right.Right, 0.0001f)
                   && MathHelper.ApproximatelyEqualEpsilon(left.Front, right.Front, 0.0001f)
                   && MathHelper.ApproximatelyEqualEpsilon(left.Back, right.Back, 0.0001f);
        }

        /// <summary>
        /// Inequality comparator.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        public static bool operator !=(Box3 left, Box3 right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public bool Equals(Box3 other)
        {
            return this == other;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is Box3 && Equals(obj);
        }
        
        /// <inheritdoc/>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Left.GetHashCode();
                hashCode = (hashCode * 397) ^ Right.GetHashCode();
                hashCode = (hashCode * 397) ^ Top.GetHashCode();
                hashCode = (hashCode * 397) ^ Bottom.GetHashCode();
                hashCode = (hashCode * 397) ^ Front.GetHashCode();
                hashCode = (hashCode * 397) ^ Back.GetHashCode();
                return hashCode;
            }
        }

        private static readonly string ListSeparator = CultureInfo.CurrentCulture.TextInfo.ListSeparator;

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format("({0}{6} {1}{6} {2}) - ({3}{6} {4}{6} {5})", Left, Top, Front, Right, Bottom, Back, ListSeparator);
        }
    }
}