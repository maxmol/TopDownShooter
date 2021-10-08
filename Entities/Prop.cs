namespace TopDownShooter.Entities
{
	public class Prop : StaticProp, ICanBeDamaged
	{
		public void TakeDamage(int damage)
		{
			Delete();
		}
	}
}