using TopDownShooter.Levels;

namespace TopDownShooter
{
	public class Hostage : Sprite, ICanBePickedUp
	{
		public Hostage()
		{
			Texture = new("hostage.png");
		}
		
		public float PickUpDistance()
		{
			return 80f;
		}

		public void PickedUp()
		{
			Game.SetLevel<MissionAccomplished>();
		}
	}
}