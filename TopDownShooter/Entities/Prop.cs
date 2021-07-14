namespace TopDownShooter.Entities
{
	public class Prop : StaticProp, ICanBeDestroyed
	{
		public void Die()
		{
			Delete();
		}
	}
}