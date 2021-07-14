using System.Drawing;

namespace TopDownShooter.Entities
{
	public class Actor : Sprite, ICanBeDestroyed
	{
		protected int Size = 48;

		public Rectangle GetRectangle()
		{
			int halfSize = Size / 2;

			return new Rectangle(
				(int) Pos.X - halfSize,
				(int) Pos.Y - halfSize,
				Size,
				Size);
		}
		
		// Movement speed (~ pixels per second)
		public float Speed = 200f;

		public virtual void Die()
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
	}
}