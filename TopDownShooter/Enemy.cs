namespace TopDownShooter
{
	public class Enemy : Actor
	{
		private Vector DesiredPosition;

		public Enemy()
		{
			Texture = "player.png";
		}

		public void WalkTo(Vector pos)
		{
			DesiredPosition = pos;
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);

			if (DesiredPosition is not null)
			{
				// enemies walk just in straight lines
				Vector path = DesiredPosition - Pos;

				Rotation = Vector.Origin.RotationBetween(path) + 90;

				float adjustedSpeed = deltaTime * Speed;

				if (path.Length() > adjustedSpeed)
				{
					Pos += path.Normalized() * adjustedSpeed;
				}
				else
				{
					Pos = DesiredPosition;
					DesiredPosition = null;
				}
			}
		}
	}
}