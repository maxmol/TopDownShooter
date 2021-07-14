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
		}
	}
}