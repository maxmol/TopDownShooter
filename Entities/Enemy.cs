using System;
using System.Collections.Generic;
using TopDownShooter.Utility;

namespace TopDownShooter.Entities
{
	public class Enemy : Actor
	{
		private enum States
		{
			Idle,
			Attack
		}

		private States State = States.Idle;
		private Entity Attacking;
		private double ReloadCooldown = 0;
		private Vector DesiredPosition;

		public Enemy()
		{
			Texture = new("enemy.png");
			Speed = 100f;
		}

		private List<Vector> WalkPattern = new();
		private int NextWalkToIndex = 0;

		public void WalkTo(Vector pos)
		{
			DesiredPosition = pos;
		}

		public void AddWalkPos(Vector pos)
		{
			WalkPattern.Add(pos);

			if (DesiredPosition == null)
			{
				WalkTo(pos);
			}
		}

		private void ContinuePatternWalking()
		{
			if (WalkPattern.Count > 1)
			{
				NextWalkToIndex = (NextWalkToIndex + 1) % WalkPattern.Count;
				WalkTo(WalkPattern[NextWalkToIndex]);
			}
		}

		private bool CanSee(Entity entity)
		{
			Trace eye = new Trace(Game, Pos, entity.Pos);
			eye.Ignore.Add(this);
			eye.Ignore.Add(entity);

			return !eye.Run().Hit;
		}
		private void LookForPlayer()
		{
			List<Player> players = Game.FindEntities<Player>();
			if (players.Count > 0)
			{
				Player player = players[0];

				if (player.Pos.DistToSqr(Pos) < 512 * 512)
				{
					if (CanSee(player))
					{
						State = States.Attack;
						Attacking = player;
						ReloadCooldown = Game.Time + new Random().NextDouble();
						WalkPattern.Clear();
					}
				}
			}
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);

			if (State == States.Idle)
			{
				LookForPlayer();
			}
			else if (State == States.Attack)
			{
				if (Attacking == null || !Attacking.IsValid())
				{
					State = States.Idle;
				}
				else
				{
					WalkTo(Attacking.Pos);

					if (ReloadCooldown < Game.Time)
					{
						ShootBullet();
						double random = new Random().NextDouble();
						ReloadCooldown = Game.Time + 1 + random;
					}
				}
			}

			if (DesiredPosition is not null)
			{
				// enemies walk just in straight lines
				Vector path = DesiredPosition - Pos;
				
				Direction = path.Normalized();
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

					ContinuePatternWalking();
				}
			}
		}
	}
}