using System.IO;
using TopDownShooter.Entities;
using TopDownShooter.Utility;

namespace TopDownShooter.Levels
{
	// A level is also an entity
	public class GameLevel : Entity
	{
		protected Vector PlayerSpawnPos = Vector.Origin;
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

				ParseLine(line, group);
			}  
  
			file.Close();  
		}

		private void ParseLine(string str, string group)
		{
			string[] parameters = str.Split();
			
			if (parameters.Length < 3)
			{
				Log.Error($"Invalid entry in level data, skipping :\n{str}");
				return;
			}

			switch (group)
			{
				case "props":
					ParseProp(parameters);
					break;
				case "static":
					ParseStaticProp(parameters);
					break;
				case "beacons":
					ParseBeacon(parameters);
					break;
				default:
					Log.Error($"Invalid group in level data : '{group}'");
					return;
			}
		}

		private Vector ParsePos(string xStr, string yStr)
		{
			if (!float.TryParse(xStr, out float xPos) ||
			    !float.TryParse(yStr, out float yPos))
			{
				Log.Error($"Invalid position in level data : \n{xStr} {yStr}");
				return Vector.Origin;
			}

			return new Vector(xPos, yPos);
		}
		
		private void ParseProp(string[] parameters)
		{
			Prop prop = Game.CreateEntity<Prop>();
			if (int.TryParse(parameters[0], out int propTile))
				prop.Texture = new("tiles.png", propTile);

			prop.Pos = ParsePos(parameters[1], parameters[2]);
		}
		
		private void ParseStaticProp(string[] parameters)
		{
			StaticProp prop = Game.CreateEntity<StaticProp>();
			if (int.TryParse(parameters[0], out int propTile))
				prop.Texture = new("tiles.png", propTile);

			prop.Pos = ParsePos(parameters[1], parameters[2]);
		}

		private void ParseBeacon(string[] parameters)
		{
			string type = parameters[0];

			switch (type)
			{
				case "spawn":
					PlayerSpawnPos = ParsePos(parameters[1], parameters[2]);
					break;
				case "finish":
					Hostage hostage = Game.CreateEntity<Hostage>();
					hostage.Pos = ParsePos(parameters[1], parameters[2]);

					if (parameters.Length > 3)
					{
						if (int.TryParse(parameters[3], out int rot))
							hostage.Rotation = rot;
					}
					break;
				case "enemy":
					Enemy enemy = Game.CreateEntity<Enemy>();
					enemy.Pos = ParsePos(parameters[1], parameters[2]);
					
					int paramIndex = 3;
					while (parameters.Length >= paramIndex + 2)
					{
						enemy.AddWalkPos(ParsePos(parameters[paramIndex], parameters[paramIndex + 1]));
						paramIndex += 2;
					}
					
					break;
				case "shotgun":
					Shotgun shotgun = Game.CreateEntity<Shotgun>();
					shotgun.Pos = ParsePos(parameters[1], parameters[2]);
					
					break;
			}
		}

		protected Player CreatePlayer()
		{
			Player player = Game.CreateEntity<Player>();
			player.Pos = PlayerSpawnPos;
			Game.Camera.Pos = player.Pos;
			Game.Camera.Following = player;

			return player;
		}
	}
}