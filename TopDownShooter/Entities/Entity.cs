using TopDownShooter.Graphics;
using TopDownShooter.Utility;

namespace TopDownShooter.Entities
{
	// Abstract game object class
	public abstract class Entity
	{
		// Position in the 2d world
		public Vector Pos = Vector.Origin;

		// Store reference to the game object
		public Game Game;
		
		// To check if the entity was not deleted
		public bool IsValid()
		{
			return Game is not null;
		}

		// Draw function renders our object on the 2d surface taking Camera position and Zoom into account
		public virtual void Draw(Surface surface, Camera camera, float deltaTime) {}
		
		// Entity's update logic must be in this method
		public virtual void Tick(float deltaTime) {}
		
		// Called when the entity was first created
		public virtual void Create() {}

		// Delete the entity from the game
		public void Delete()
		{
			Game.RemoveEntity(this);
			Game = null;
		}
	}
}