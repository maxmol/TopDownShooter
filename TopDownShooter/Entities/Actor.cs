using System.Drawing;
using TopDownShooter.Utility;

namespace TopDownShooter.Entities
{
	public class Actor : Sprite, ICanBeDestroyed
	{
		// Direction facing
		protected Vector Direction = Vector.Right;
		protected int Size = 48;
		
		public enum Weapons
		{
			Default,
			Shotgun
		}

		public Weapons Weapon = Weapons.Default;
		public int Ammo = 0;
		
		
		// Movement speed (~ pixels per second)
		public float Speed = 200f;

		public virtual void TakeDamage(int damage)
		{
			Delete();
		}

		// Is the actor stuck in something
		// returns null if not stuck or the entity
		public Entity CheckStuck()
		{
			// Actors can't go through objects and walls
			foreach (var collidable in Game.FindEntities<IHasCollisionRect>()) // TODO: Optimize
			{
				if (collidable != this && collidable.GetRectangle().IntersectsWith(GetRectangle()))
				{
					return (Entity) collidable;
				}
			}

			return null;
		}
		
		public void ShootBullet()
		{
			switch (Weapon)
			{
				case Weapons.Default:
					Bullet bullet = Game.CreateEntity<Bullet>();
					bullet.Pos = Pos + Direction * 50;
					bullet.Direction = Direction;
					break;
				case Weapons.Shotgun:
					for (int angle = -5; angle <= 5; angle += 5)
					{
						Bullet b = Game.CreateEntity<Bullet>();
						b.Pos = Pos + Direction * 50;
						b.Direction = Direction.Rotate(angle);
					}
					
					Ammo--;
					break;
			}

			if (Weapon != Weapons.Default)
			{
				if (Ammo <= 0)
					Weapon = Weapons.Default;
			}
		}
		
		public Rectangle GetRectangle()
		{
			int halfSize = Size / 2;

			return new Rectangle(
				(int) Pos.X - halfSize,
				(int) Pos.Y - halfSize,
				Size,
				Size);
		}
	}
}