using System.Drawing;

namespace TopDownShooter.Levels
{
	public class MissionAccomplished : GameOver
	{
		public override void Draw(Surface surface, Camera camera, float deltaTime)
		{
			Game.Surface.SetDrawColor(Color.Chartreuse);
			Game.Surface.DrawText("MISSION ACCOMPLISHED", "Consolas", 32, 200, 24);
			
			Game.Surface.SetDrawColor(Color.White);
			Game.Surface.DrawText("Press Enter to continue", "Consolas", 32, 300, 24);
		}
	}
}