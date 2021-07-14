namespace TopDownShooter.Entities
{
	public class Prop : StaticProp, ICanBeDestroyed
	{
		public void TakeDamage(int damage)
		{
			Delete();
		}
	}
}