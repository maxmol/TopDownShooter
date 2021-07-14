namespace TopDownShooter.Levels
{
	public class LevelTestEnemies : GameLevel
	{
		private Enemy testEnemy;
		public override void Create()
		{
			CreatePlayer();
			Game.Camera.Zoom = 5f;
			
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

			// a little camera animation on level start
			if (Game.Camera.Zoom > 1f)
				Game.Camera.Zoom -= deltaTime * 5f;
		}
	}
}