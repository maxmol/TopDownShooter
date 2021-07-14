using System.Drawing;

namespace TopDownShooter
{
	public class Prop : StaticProp, ICanBeDestroyed
	{
		public void Die()
		{
			Delete();
		}
	}
}