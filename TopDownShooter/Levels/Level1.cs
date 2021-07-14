namespace TopDownShooter.Levels
{
	public class Level1 : GameLevel
	{
		public override void Create()
		{
			base.Create();
			
			ReadFromFile("1");
			CreatePlayer();
		}
	}
}