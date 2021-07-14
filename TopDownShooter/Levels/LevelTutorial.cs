namespace TopDownShooter.Levels
{
	public class LevelTutorial : GameLevel
	{
		public override void Create()
		{
			base.Create();
			
			CreatePlayer();
			ReadFromFile("tutorial");
			Game.Camera.Zoom = 2f;
		}
	}
}