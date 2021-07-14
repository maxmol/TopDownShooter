using System;
using System.IO;

namespace TopDownShooter
{
	// A level is also an entity
	public class GameLevel : Entity
	{
		private static string LevelsFolder = "Assets/Levels/";
		protected void ReadFromFile(string name)
		{
			StreamReader file = new StreamReader($"{LevelsFolder}{name}.level");
			string line;
			string group = "";
			while((line = file.ReadLine()) != null)
			{
				if (line[0] == '#') // allow comments
					continue;

				if (line[0] == ':')
				{
					group = line.Substring(1);
					continue;
				}

				GenerateEntityFromString(line, group);
			}  
  
			file.Close();  
		}

		private void GenerateEntityFromString(string str, string group)
		{
			string[] parameters = str.Split();
			
			if (parameters.Length < 3)
			{
				Log.Error($"Invalid entry in level data, skipping :\n{str}");
				return;
			}

			if (!float.TryParse(parameters[1], out float xPos) ||
			    !float.TryParse(parameters[2], out float yPos))
			{
				Log.Error($"Invalid position in level data : \n{str}");
				return;
			}


			Entity ent;
			switch (group)
			{
				case "props":
					Prop prop = Game.CreateEntity<Prop>();
					ent = prop;
					if (int.TryParse(parameters[0], out int propTile))
						prop.Texture = new("tiles.png", propTile);
					
					break;
				case "static":
					StaticProp staticProp = Game.CreateEntity<StaticProp>();
					if (int.TryParse(parameters[0], out int staticPropTile))
						staticProp.Texture = new("tiles.png", staticPropTile);

					ent = staticProp;
					break;
				default:
					Log.Error($"Invalid group in level data : '{group}'");
					return;
			}

			ent.Pos = new Vector(xPos, yPos);
		}

		protected Player CreatePlayer()
		{
			Player player = Game.CreateEntity<Player>();
			Game.Camera.Following = player;

			return player;
		}
	}
}