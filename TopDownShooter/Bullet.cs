using System.Drawing;

namespace TopDownShooter
{
	public class Bullet : Sprite
	{
		// bullet direction
		private Vector _dir = Vector.Up;

		public Vector Direction
		{
			get => _dir;
			set
			{
				_dir = value.Normalized();
				Rotation = Vector.Origin.RotationBetween(_dir) - 180;
			}
		}

		public float Speed = 500f;
		
		public Bullet()
		{
			Texture = new("bullet.png");
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			Pos += Direction * Speed * deltaTime;

			// TODO: Optimize
			foreach (Entity entity in Game.Entities)
			{
				if (entity is IHasCollisionRect collidable)
				{
					if (collidable.GetRectangle().Contains(Pos))
					{
						if (entity is ICanBeDestroyed destructible)
							destructible.Die();
						
						Delete();
						return;
					}
				}
			}

			if (IsValid()) // the bullet can be deleted 
			{
				Vector scrPos = Pos.ToScreen(Game.Camera);
				if (scrPos.X < 0 ||
				    scrPos.Y < 0 ||
				    scrPos.X > Game.Surface.ScreenWidth() ||
				    scrPos.Y > Game.Surface.ScreenHeight())
				{
					Delete();
				}
			}
		}
	}
}