namespace TopDownShooter.Levels
{
	public class LevelTestEnemies : GameLevel
	{
		private Enemy testEnemy;
		public override void Create()
		{
			Enemy testEnemy2 = Game.CreateEntity<Enemy>();
			testEnemy2.Pos = new Vector(350, 50);
		}
		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);

			if (testEnemy is null || !testEnemy.IsValid())
			{
				testEnemy = Game.CreateEntity<Enemy>();
				testEnemy.Pos = new Vector(250, -200);
				testEnemy.WalkTo(new Vector(250, 50));
			}
		}
	}
}