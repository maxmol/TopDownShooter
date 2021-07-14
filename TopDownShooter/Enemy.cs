namespace TopDownShooter
{
	public class Enemy : Actor
	{
		private Vector DesiredPosition;

		public Enemy()
		{
			Texture = new("enemy.png");
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

				Vector oldPos = Pos;
				if (path.Length() > adjustedSpeed)
				{
					Pos += path.Normalized() * adjustedSpeed;

					if (CheckStuck() != null)
						Pos = oldPos;
				}
				else
				{
					Pos = DesiredPosition;
					
					if (CheckStuck() != null)
						Pos = oldPos;
					else
						DesiredPosition = null;
				}
			}
		}
	}
}