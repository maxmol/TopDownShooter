using System.Drawing;

namespace TopDownShooter
{
	public class StaticProp : Sprite, IHasCollisionRect
	{
		protected int Width = 0;
		protected int Height = 0;
		
		public StaticProp()
		{
			Texture = new("box.png");
		}
		
		public override void Create()
		{
			base.Create();
			Width = Texture.GetWidth();
			Height = Texture.GetHeight();
		}

		public Rectangle GetRectangle()
		{
			return new Rectangle(
				(int) Pos.X - Width/2,
				(int) Pos.Y - Height/2,
				Width,
				Height);
		}
	}
}