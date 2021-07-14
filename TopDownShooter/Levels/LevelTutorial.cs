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
	}
}