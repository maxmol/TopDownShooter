using System;
using System.Collections.Generic;
using System.Drawing;

namespace TopDownShooter
{
	public class Surface
	{
		private static class ImageLoader
		{
			private static Dictionary<string, Bitmap> Cache = new();
			private static string ImagesFolder = "Assets/Sprites/";
			private static readonly Bitmap ErrorBitmap = LoadBitmap("cross.png");
		
			public static Bitmap LoadBitmap(string path)
			{
				if (Cache.TryGetValue(path, out Bitmap cachedBitmap))
					return cachedBitmap;
			
				try
				{
					string fullPath = ImagesFolder + path;
					Bitmap bmp = new Bitmap(fullPath);
					Log.Info("Loaded " + fullPath);
					Cache[path] = bmp;
					return bmp;
				}
				catch (Exception e)
				{
					Log.Error("Error loading material " + path);
					Log.Warning(e.Message);
				}

				return ErrorBitmap;
			}
		}

		private SolidBrush Brush;

		public Surface()
		{
			Brush = new SolidBrush(Color.White);
		}

		public void DrawTexturedRect(string imagePath, float x, float y, float w, float h)
		{
			Bitmap bitmap = ImageLoader.LoadBitmap(imagePath);
			Form1.Instance.Graphics.DrawImage(bitmap, x, y, w, h);
		}
		
		public void DrawTexturedRectRotated(string imagePath, float x, float y, float w, float h, float angle)
		{
			Bitmap bitmap = ImageLoader.LoadBitmap(imagePath);
			
			Vector center = new Vector(x, y);
			
			Vector up = Vector.Up.Rotate(angle) * (h/2);
			Vector right = Vector.Right.Rotate(angle) * (w/2);
			
			Vector upperLeft = center + up - right;
			Vector upperRight = center + up + right;
			Vector bottomLeft = center - up - right;

			Form1.Instance.Graphics.DrawImage(bitmap, new Point[] {
				upperLeft,
				upperRight,
				bottomLeft
			});
		}
		
		public void SetDrawColor(Color color)
		{
			Brush.Color = color;
		}

		public void DrawRect(float x, float y, float w, float h)
		{
			Form1.Instance.Graphics.FillRectangle(Brush, x, y, w, h);
		}
		
		public void DrawColoredRect(Color color, float x, float y, float w, float h)
		{
			SetDrawColor(color);
			DrawRect(x, y, h, w);
		}
		
		public int ScreenWidth()
		{
			return Form1.Instance.ClientSize.Width;
		}

		public int ScreenHeight()
		{
			return Form1.Instance.ClientSize.Height;
		}

		public int TextureWidth(string imagePath)
		{
			return ImageLoader.LoadBitmap(imagePath).Width;
		}

		public int TextureHeight(string imagePath)
		{
			return ImageLoader.LoadBitmap(imagePath).Height;
		}
		
	}
	
	public partial class Vector
	{
		// cast from Vector to Point
		public static implicit operator Point(Vector vec)
		{
			return new Point((int) vec.X, (int) vec.Y);
		}
	}
}