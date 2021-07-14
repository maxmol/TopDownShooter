using System;
using System.Collections.Generic;
using System.Drawing;

namespace TopDownShooter
{
	public class Surface
	{
		private SolidBrush Brush;

		public Surface()
		{
			Brush = new SolidBrush(Color.White);
		}

		public void DrawTexturedRect(Texture texture, float x, float y, float w, float h)
		{
			Form1.Instance.Graphics.DrawImage(texture.GetBitmap(), x, y, w, h);
		}

		public void DrawTexturedRectRotated(Texture texture, float x, float y, float w, float h, float angle)
		{
			Vector center = new Vector(x, y);
			
			Vector up = Vector.Up.Rotate(angle) * (h/2);
			Vector right = Vector.Right.Rotate(angle) * (w/2);
			
			Vector upperLeft = center + up - right;
			Vector upperRight = center + up + right;
			Vector bottomLeft = center - up - right;

			Form1.Instance.Graphics.DrawImage(texture.GetBitmap(), new Point[] {
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
			DrawRect(x, y, w, h);
		}

		public void ClearScreen(Color color)
		{
			Form1.Instance.Graphics.Clear(color);
		}
		
		public int ScreenWidth()
		{
			return Form1.Instance.Size.Width;
		}

		public int ScreenHeight()
		{
			return Form1.Instance.Size.Height;
		}
		
		public void DrawText(string text, string font, float x, float y, int fontSize)
		{
			Font drawFont = new Font(font, fontSize);
			Form1.Instance.Graphics.DrawString(text, drawFont, Brush, x, y, new StringFormat());
			drawFont.Dispose();
		}

		// Checks if a point is out of screen bounds
		public bool IsVisible(Vector screenPos)
		{
			return screenPos.X > 0 && screenPos.Y > 0 && screenPos.X < ScreenWidth() && screenPos.Y < ScreenHeight();
		}
	}
}