using TopDownShooter.Entities;

namespace TopDownShooter
{
	public interface ICanBePickedUp
	{
		public float PickUpDistance();
		public void PickedUp(Player player);
	}
}