using System;
using System.Drawing;

namespace TopDownShooter
{
	// 2D point on a surface
	public partial class Vector
	{
		public static readonly Vector Origin = new Vector(0, 0);
		public static readonly Vector Right = new Vector(1, 0);
		public static readonly Vector Up = new Vector(0, -1);

		// Coordinates can't be changed individually
		// A new instance of Vector must always be used
		public float X { get; }
		public float Y { get; }

		public Vector(float x, float y)
		{
			X = x;
			Y = y;
		}

		// Override operators for vector addition and subtraction
		public static Vector operator +(Vector a, Vector b)
		{
			return new Vector(a.X + b.X, a.Y + b.Y);
		}
		
		public static Vector operator -(Vector a, Vector b)
		{
			return new Vector(a.X - b.X, a.Y - b.Y);
		}

		public static Vector operator -(Vector a)
		{
			return new Vector(-a.X, -a.Y);
		}
		
		// Override operator for vector scaling
		public static Vector operator *(Vector vec, float scalar)
		{
			return new Vector(vec.X * scalar, vec.Y * scalar);
		}

		// Distance between two points
		public float Distance(Vector to)
		{
			return (float) Math.Sqrt(DistToSqr(to));
		}
		
		// Distance squared
		// A lot faster than Distance, this should be used when distance is calculated every frame
		public float DistToSqr(Vector to)
		{
			float xDiff = to.X - this.X;
			float yDiff = to.Y - this.Y;
			return xDiff * xDiff + yDiff * yDiff;
		}
		
		public float Length() {
			return (float) Math.Sqrt(X * X + Y * Y);
		}

		public float LengthSqr()
		{
			return X * X + Y * Y;
		}

		public Vector ToScreen(Camera camera)
		{
			float x = X - camera.Pos.X + camera.ViewWidth()/2;
			float y = Y - camera.Pos.Y + camera.ViewHeight()/2;
			x *= camera.Zoom;
			y *= camera.Zoom;

			return new Vector(x, y);
		} 

		// Rotate a vector by an angle
		public Vector Rotate(float angle)
		{
			double radians = (Math.PI / 180) * angle;
			float cos = (float) Math.Cos(radians);
			float sin = (float) Math.Sin(radians);
			float x = cos * X - sin * Y;
			float y = sin * X + cos * Y;

			return new Vector(x, y);
		}
		
		// Rotation in degrees between two points on a plane
		public float RotationBetween(Vector vec) {
			double angle = Math.Atan2(vec.Y - Y, vec.X - X);
			angle -= Math.PI/2.0;
			angle *= 180/ Math.PI; // convert from radians

			return (float) angle % 360;
		}
	
		// Get the directional vector with length of 1
		public Vector Normalized() {
			float len = Length();

			if (len == 0)
				return Vector.Origin;
			
			return new Vector(X / len, Y / len);
		}
		
		// Smooth transition from one vector to another
		public Vector Lerp(float fraction, Vector to)
		{
			return new Vector(X + (to.X - X) * fraction, Y + (to.Y - Y) * fraction);
		}
		
		// cast from Vector to Point
		public static implicit operator Point(Vector vec)
		{
			return new Point((int) vec.X, (int) vec.Y);
		}
	}
}