using System;
using System.Drawing;
using TopDownShooter.Graphics;
using TopDownShooter.Levels;
using TopDownShooter.Utility;

namespace TopDownShooter.Entities
{	public class Player : Actor
	{
		// Delay between shots
        private float ShootCooldown = 0.5f;
        
        // Last time a bullet was shot
        private double TimeLastShoot = 0;
        
        // Health points
        public int Health = 100;

        public Player()
		{
			Texture = new("player.png");
		}

		private void HandleMovement(float deltaTime)
		{
			// if we're already stuck in another object even without any movement applied, move away from the object
			Entity stuckIn = CheckStuck();
			if (stuckIn != null)
			{
				Pos += (Pos - stuckIn.Pos).Normalized();
				return;
			}
			
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

			Vector oldPos = Pos;
			Pos += move * Speed * deltaTime;
			
			if (CheckStuck() != null)
				Pos = oldPos;
		}

		private void PickUpTick()
		{
			foreach (Entity entity in Game.Entities)
			{
				if (entity is ICanBePickedUp pickable)
				{
					float dist = pickable.PickUpDistance();
					
					if (entity.Pos.DistToSqr(Pos) < dist * dist) 
						pickable.PickedUp(this);
				}
			}
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

			PickUpTick();
		}

		public override void TakeDamage(int damage)
		{
			Health = Math.Max(Health - damage, 0);
			
			if (Health == 0)
				Game.SetLevel<GameOver>();
		}
		
		public override void DrawHud(Surface surface)
		{
			surface.SetDrawColor(Color.White);
			surface.DrawText("HEALTH :", "Consolas", 8, surface.ScreenHeight() - 94, 16);
			
			surface.SetDrawColor(Color.Red);
			surface.DrawText(Health.ToString(), "Impact", 90, surface.ScreenHeight() - 100, 24);
		}
	}
}