namespace TopDownShooter.Levels
{
	public class Level1 : GameLevel
	{
		public override void Create()
		{
			base.Create();
			
			CreatePlayer();
			ReadFromFile("1");
		}
	}
}