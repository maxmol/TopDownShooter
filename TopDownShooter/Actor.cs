namespace TopDownShooter
{
	public class Actor : Sprite
	{
		public float Size = 32f;
		
		// Movement speed (~ pixels per second)
		public float Speed = 200f;

		public void Die()
		{
			Delete();
		}
	}
}