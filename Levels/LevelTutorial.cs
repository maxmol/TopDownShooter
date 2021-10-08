using System.Drawing;
using TopDownShooter.Entities;
using TopDownShooter.Graphics;

namespace TopDownShooter.Levels
{
	public class LevelTutorial : GameLevel
	{
		public override void Create()
		{
			base.Create();
			
			ReadFromFile("tutorial");
			CreatePlayer();
			Game.Camera.Zoom = 1f;
		}

		public override void DrawHud(Surface surface)
		{
			surface.SetDrawColor(Color.Gold);
			surface.DrawText("Movement: W, A, S, D\nAttack: Enter", "Consolas", 64, 32, 32);
		}
	}
}