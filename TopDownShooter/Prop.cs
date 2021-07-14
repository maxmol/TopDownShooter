using System.Drawing;

namespace TopDownShooter
{
	public class Prop : StaticProp, ICanBeDestroyed
	{
		public Prop()
		{
			Texture = new("box.png");
		}

		public void Die()
		{
			Delete();
		}
	}
}