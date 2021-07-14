using System.Drawing;

namespace TopDownShooter.Levels
{
	public class GameOver : GameLevel
	{
		public override void Draw(Surface surface, Camera camera, float deltaTime)
		{
			base.Draw(surface, camera, deltaTime);
			
			Game.Surface.SetDrawColor(Color.Red);
			Game.Surface.DrawText("GAME OVER", "Consolas", 32, 200, 24);
			
			Game.Surface.SetDrawColor(Color.White);
			Game.Surface.DrawText("Press Enter to continue", "Consolas", 32, 300, 24);
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);

			if (Input.Pressed(Button.Attack))
			{
				Game.SetLevel<MainMenu>();
			}
		}
	}
}