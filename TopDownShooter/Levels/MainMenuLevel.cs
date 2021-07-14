using System.Collections.Generic;
using System.Drawing;

namespace TopDownShooter.Levels
{
	public class MainMenuLevel : GameLevel
	{
		private bool LevelSelection = false;
		private int SelectedLevel = 0;
		private List<string> LevelNames = new()
		{
			"TEST ENEMIES",
			"TUTORIAL",
		};

		public override void Draw(Surface surface, Camera camera, float deltaTime)
		{
			base.Draw(surface, camera, deltaTime);

			Game.Surface.SetDrawColor(Color.Goldenrod);
			
			if (LevelSelection)
			{
				
				Game.Surface.DrawText("CHOOSE SCENARIO :", "Consolas",  64, 200, 24);
				
				for (var i = 0; i < LevelNames.Count; i++)
				{
					if (SelectedLevel == i)
						Game.Surface.SetDrawColor(Color.Red);
					
					Game.Surface.DrawText($"{i + 1} - {LevelNames[i]}", "Consolas",  64, 300 + 50*i, 24);
					
					if (SelectedLevel == i)
						Game.Surface.SetDrawColor(Color.Goldenrod);
				}
			}
			else
			{
				Game.Surface.DrawText("PRESS ENTER TO START THE GAME", "Consolas", 32, 400, 24);
			}
		}

		private void SelectLevel(int selected)
		{
			switch (selected)
			{
				case 0:
					Game.SetLevel<LevelTestEnemies>();
					break;
				case 1:
					Game.SetLevel<LevelTutorial>();
					break;
			}
		}
		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);

			if (Input.Pressed(Button.Attack))
			{
				if (!LevelSelection)
					LevelSelection = true;
				else
					SelectLevel(SelectedLevel);
			}

			if (Input.Pressed(Button.Down))
			{
				SelectedLevel = (SelectedLevel + 1) % LevelNames.Count;
			}
			
			if (Input.Pressed(Button.Up))
			{
				SelectedLevel = SelectedLevel > 0 ? SelectedLevel - 1 : LevelNames.Count - 1;
			}
		}
	}
}