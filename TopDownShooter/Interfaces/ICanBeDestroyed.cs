namespace TopDownShooter
{
	public interface ICanBeDestroyed : IHasCollisionRect
	{
		public void Die();
	}
}