namespace TopDownShooter
{
	public interface ICanBeDestroyed : IHasCollisionRect
	{
		public void TakeDamage(int damage);
	}
}