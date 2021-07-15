namespace TopDownShooter
{
	public interface ICanBeDamaged : IHasCollisionRect
	{
		public void TakeDamage(int damage);
	}
}