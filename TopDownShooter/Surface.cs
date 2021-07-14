using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using unvell.D2DLib;

namespace TopDownShooter
{
	public class Surface
	{
		private D2DColor CurrentColor;

		public Surface()
		{
			CurrentColor = D2DColor.White;
		}

		public void DrawTexturedRect(Texture texture, float x, float y, float w, float h)
		{
			Form1.Instance.Direct2D.DrawBitmap(texture.GetBitmap(), new D2DRect(x, y, w, h));
		}

		public void DrawTexturedRectRotated(Texture texture, float x, float y, float w, float h, float angle)
		{
			Form1.Instance.Direct2D.PushTransform();
			Form1.Instance.Direct2D.RotateTransform(angle, new D2DPoint(x, y));
			DrawTexturedRect(texture, x - w/2, y - h/2, w, h);
			Form1.Instance.Direct2D.PopTransform();
		}

		public void SetDrawColor(Color color)
		{
			CurrentColor = new D2DColor((float)color.A/255, (float)color.R/255, (float)color.G/255, (float)color.B/255);
		}
		public void DrawRect(float x, float y, float w, float h)
		{
			Form1.Instance.Direct2D.DrawRectangle(x, y, w, h, CurrentColor);
		}
		
		public void DrawColoredRect(Color color, float x, float y, float w, float h)
		{
			SetDrawColor(color);
			DrawRect(x, y, w, h);
		}

		public void ClearScreen()
		{
			Form1.Instance.Direct2D.Clear(D2DColor.Black);
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
			Form1.Instance.Direct2D.DrawText(text, CurrentColor, font, fontSize, x, y);
		}

		// Checks if a point is out of screen bounds
		public bool IsVisible(Vector screenPos)
		{
			return screenPos.X > 0 && screenPos.Y > 0 && screenPos.X < ScreenWidth() && screenPos.Y < ScreenHeight();
		}
	}
}