namespace TopDownShooter
{
	// Camera entity stores our view point as its position and describes our view
	public class Camera : Entity
	{
		// View scaling
		public float Zoom = 1f;

		public Entity Following;

		public float ViewWidth()
		{
			return Game.Surface.ScreenWidth() / Zoom;
		}
		public float ViewHeight()
		{
			return Game.Surface.ScreenHeight() / Zoom;
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);

			if (Following is not null)
			{
				//Pos = Pos.Lerp(deltaTime * 5, Following.Pos);
				Pos = Following.Pos;
			}
		}
	}
}