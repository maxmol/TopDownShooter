using System;

namespace TopDownShooter
{	public class Player : Actor
	{
		// Direction facing
		protected Vector Direction = Vector.Right;
		
		// Delay between shots
        private float ShootCooldown = 0.5f;
        
        // Last time a bullet was shot
        private double TimeLastShoot = 0;
		
		public Player()
		{
			Texture = "player.png";
		}

		public void ShootBullet()
		{
			Bullet b = Game.CreateEntity<Bullet>();
			b.Pos = Pos + Direction * 40;
			b.Direction = Direction;
		}

		private void HandleMovement(float deltaTime)
		{
			Vector move = Vector.Origin;
			
			if (Input.IsDown(Button.Up))
				move += Vector.Up;
			if (Input.IsDown(Button.Right))
				move += Vector.Right;
			if (Input.IsDown(Button.Down))
				move -= Vector.Up;
			if (Input.IsDown(Button.Left))
				move -= Vector.Right;

			if (move.LengthSqr() > 0)
			{
				move = move.Normalized();
				Direction = move;
				Rotation = Vector.Origin.RotationBetween(move) + 90;
			}

			Pos += move * Speed * deltaTime;
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);
			HandleMovement(deltaTime);
			if (Input.Pressed(Button.Attack) && TimeLastShoot + ShootCooldown < Game.Time )
			{
				TimeLastShoot = Game.Time;
				ShootBullet();
			}
		}
	}
}