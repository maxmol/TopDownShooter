namespace TopDownShooter.Levels
{
	public class LevelMansion : GameLevel
	{
		public override void Create()
		{
			base.Create();
			
			ReadFromFile("mansion");
			CreatePlayer();
		}
	}
}