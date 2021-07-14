using System.Collections.Generic;
using System.Drawing;
using TopDownShooter.Entities;
using TopDownShooter.Graphics;
using TopDownShooter.Levels;

namespace TopDownShooter
{
	public class Game
	{
		public Camera Camera { get; private set; }
		
		// A list of all game objects
		private List<Entity> _entities;
		public List<Entity> Entities
		{
			get => new(_entities); // make a copy
			set => _entities = value;
		}
		
		// Dictionary that stores lists of entities of each class separately to quickly find them
		//private Dictionary<string, Entity> EntitiesByClassName = new ();
		
		// Get all entities that are of specific class
		public List<T> FindEntities<T>()
		{
			List<T> entities = new();
			foreach (var ent in _entities)
			{
				if (ent is T found)
				{
					entities.Add(found);
				}
			}

			return entities;
		}

		public Surface Surface { get; private set; }

		public double Time = 0;

		public Game()
		{
			Surface = new Surface();
			
			Entities = new List<Entity>();
			Camera = CreateEntity<Camera>();
		}
		
		// Generic entity creation method
		public T CreateEntity<T>() where T : Entity, new()
		{
			// creates a new entity and saves it to the entity list
			T ent = new T();
			_entities.Add(ent);
			ent.Game = this;
			ent.Create();
			return ent;
		}

		public void RemoveEntity(Entity ent)
		{
			_entities.Remove(ent);
		}

		public void RemoveAllEntities()
		{
			_entities.Clear();
			Camera = CreateEntity<Camera>();
		}

		public void SetLevel<T>() where T : GameLevel, new()
		{
			RemoveAllEntities();
			CreateEntity<T>();
		}

		// Draws the game world
		// Loops through all game entities and calls their Draw methods
		public void Draw(float deltaTime)
		{
			Surface.ClearScreen();

			foreach (Entity entity in Entities)
			{
				entity.Draw(Surface, Camera, deltaTime);
			}

			float fps = 1 / deltaTime;
			
			Surface.SetDrawColor(Color.White);
			Surface.DrawText("FPS: " + fps, "Consolas", 16, 16,  12);
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