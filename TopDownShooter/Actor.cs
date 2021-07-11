namespace TopDownShooter
{
	public class Actor : Sprite
	{
		public float Size = 32f;

		public void Die()
		{
			Delete();
		}
	}
}