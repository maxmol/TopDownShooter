using System.Collections.Generic;
using System.Drawing;

namespace TopDownShooter
{
	public class Game
	{
		public Player Player { get; private set; }
		public Camera Camera { get; private set; }
		
		// A list of all game objects
		private List<Entity> _entities;
		public List<Entity> Entities
		{
			get => new List<Entity>(_entities); // make a copy
			set => _entities = value;
		}
		
		public Surface Surface { get; private set; }

		public double Time = 0;

		public Game()
		{
			Surface = new Surface();
			
			Entities = new List<Entity>();
			Player = CreateEntity<Player>();
			Camera = CreateEntity<Camera>();

			Enemy testEnemy = CreateEntity<Enemy>();
			testEnemy.Pos = new Vector(300, 50);
		}
		
		// Generic entity creation method
		public T CreateEntity<T>() where T : Entity, new()
		{
			// creates a new entity and saves it to the entity list
			T ent = new T();
			_entities.Add(ent);
			ent.Game = this;
			return ent;
		}

		public void RemoveEntity(Entity ent)
		{
			_entities.Remove(ent);
		}

		// Draws the game world
		// Loops through all game entities and calls their Draw methods
		public void Draw(float deltaTime)
		{
			Surface.DrawColoredRect(Color.Black, 0, 0, Surface.ScreenWidth(), Surface.ScreenHeight());

			foreach (Entity entity in Entities)
			{
				entity.Draw(Surface, Camera, deltaTime);
			}
		}
		
		// Updates all game entities
		public void Tick(float deltaTime)
		{
			foreach (Entity entity in Entities)
			{
				entity.Tick(deltaTime);
			}
		}
	}
}